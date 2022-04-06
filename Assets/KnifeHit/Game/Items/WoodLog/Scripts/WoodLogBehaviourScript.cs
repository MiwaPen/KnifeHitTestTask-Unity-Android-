using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class WoodLogBehaviourScript : MonoBehaviour
{
    private enum Direction
    {
        LEFT,
        RIGHT
    }

    Direction direction;

    public Action OnBreakWoodLog;
    public Action OnKnifeEnterWood;
    public int logHP;
    [Header("RotationDuration(S)")]
    public float rotationDuration;
    [SerializeField] private List<Transform> appleSpawnPositions;
    [SerializeField] private ParticleSystem woodParticle;
    [SerializeField] private List<GameObject> Knives;
    [SerializeField] private List<WoodPieceScript> WoodPieces;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private AppleBehaviour applePrefab = null;
    [SerializeField] private AppleInfo appleInfo;
    [Inject] private SoundController soundController;
    [Inject] private DiContainer diContainer;
    private Transform _transform;
    private AppleBehaviour _currentApple = null;
   

    private void Awake()
    {
        _transform = this.transform;
        Vibration.Init();
    }
    private void Start()
    {
        foreach(GameObject obj in Knives)
        {
            obj.SetActive(false);
        }
        TrySpawnKnives();
    }

    private void SetRandomDirection()
    {
        if (GetRandomValue() <= 50)
            direction = Direction.LEFT;
        else
            direction = Direction.RIGHT;
    }

    public void LogShake()
    {
        _transform.DOShakePosition(_shakeDuration, new Vector3(0,0.1f),0,0) ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        KnifeBehaviourScript knife = null;

        if (collision.gameObject.TryGetComponent<KnifeBehaviourScript>(out knife))
        {
            if (knife.isStaticKnife == true) return;
            ParticleSystem newParticle = Instantiate(woodParticle);
            newParticle.transform.position = collision.transform.position;
            newParticle.Play();
            Destroy(newParticle.gameObject, 0.3f);
            logHP--;
            LogShake();
            CheckHp();
            OnKnifeEnterWood?.Invoke();
        }
    }

    private  async void CheckHp()
    {
        if (logHP > 0) return;

        await UniTask.Delay(10);

       
        StopRorate();
        foreach (WoodPieceScript piece in WoodPieces)
        {
            piece.SplitPiece();
            Destroy(piece.gameObject, 0.5f);
        }

        foreach (GameObject piece in Knives)
        {
            piece.transform.parent = null;
            piece.GetComponent<Rigidbody2D>().gravityScale = 4;
            Destroy(piece.gameObject, 0.5f);
        }

        if (_currentApple != null)
        {
            _currentApple.transform.parent = null;
            _currentApple.GetComponent<Collider2D>().enabled = false;
            _currentApple.GetComponent<Rigidbody2D>().gravityScale = 4f;
            Destroy(_currentApple.gameObject, 0.5f);
            _currentApple = null;
        }

        soundController.PlaySound(soundController.WoodCrackSound);
        Vibration.Vibrate(500);
        await UniTask.Delay(1000);
        OnBreakWoodLog?.Invoke(); 
        
    }
 
    public AppleBehaviour TrySpawnApple()
    {
        int spawnChanse = GetRandomValue();

        if (spawnChanse > appleInfo.spawnChanse) return null;
        
        Transform applePos = appleSpawnPositions[GetRandomValue(appleSpawnPositions.Count)];
        GameObject newApple = diContainer.InstantiatePrefab(applePrefab);
        _currentApple = newApple.GetComponent<AppleBehaviour>();
        _currentApple.transform.position = applePos.position;
        _currentApple.transform.rotation = applePos.rotation;
        _currentApple.transform.SetParent(this.transform);
        return _currentApple;
    }

    private void TrySpawnKnives()
    {
        int spawnCount = GetRandomValue(Knives.Count+1);
        if (spawnCount > Knives.Count) spawnCount = Knives.Count;
        for (int i= 0; i < spawnCount; i++)
        {
            Knives[i].SetActive(true);
        }
    }

    public void StartRotate()
    {
        SetRandomDirection();
        int rotationValue = 360;

        if (direction == Direction.RIGHT)
            rotationValue = -360;

        Tween rotateTween = _transform.DOLocalRotate(new Vector3(0, 0, rotationValue),
            rotationDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }

    public void StopRorate()
    {
        _transform.DOKill(true);
    }

    private int GetRandomValue(int maxValue = 101)
    {
        var random = new System.Random();
        int randomValue = random.Next(0, maxValue);
        return randomValue;
    }

    private void OnDisable()
    {
        _transform.DOKill(true);
    }

    public void DestroyLog()
    {
        if (this.gameObject == null) return;
        Destroy(this.gameObject);
    }
}
