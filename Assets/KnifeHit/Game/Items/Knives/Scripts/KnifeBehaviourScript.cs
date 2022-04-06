using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D),typeof(Rigidbody2D))]
public class KnifeBehaviourScript : MonoBehaviour, IPooledObject
{
    public Action onKnifeHitKnife;
    public bool isStaticKnife = false;
    [SerializeField] KnifeInfo knifeInfo;
    [SerializeField] private ParticleSystem knifeSparks;
    [Inject] private SoundController soundController;
    private Transform _transform;
    private Rigidbody2D _rigidbody2D;
    private bool _canEnterlog;
    private Collider2D _collider2D;
  
    public void TryMove()
    {
        if (isStaticKnife == true) return;
        soundController.PlaySound(soundController.KnivesThrowSound);
        _collider2D.enabled = true;
        _transform.DOMoveY(
            knifeInfo.endPoint, knifeInfo.speed)
            .SetEase(knifeInfo.ease);
    }
    public void GetFromPool()
    {
        _transform.parent = null;
        _transform.rotation = new Quaternion(0f,0f,0f,0f);
        CheckOnStatic();
    }

    private void Awake()
    {
        _transform = this.transform;
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
        _collider2D = this.GetComponent<Collider2D>();
        Vibration.Init();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isStaticKnife == true) return;

        _transform.DOKill(true);
        WoodPieceScript wood = null;
        KnifeBehaviourScript knife = null;
      
        if (collision.gameObject.TryGetComponent<KnifeBehaviourScript>(out knife))
        {
            _transform.DOKill(true);
            isStaticKnife = true;
            _canEnterlog = false;

            soundController.PlaySound(soundController.KnivesHitSound);
            ParticleSystem newParticle = Instantiate(knifeSparks);
            newParticle.transform.position = collision.transform.position;
            newParticle.Play();
            Destroy(newParticle, 0.3f);
            Vibration.Vibrate(700);
            StartFlip();
            onKnifeHitKnife?.Invoke();
        }

        if (collision.gameObject.TryGetComponent<WoodPieceScript>(out wood))
        {
            if (_canEnterlog == false) return;
            EnterWood(wood.transform);
        }      
    }

    private void OnDisable()
    {
        _transform.DOKill(true);
    }

    private async void EnterWood(Transform parent)
    {
        soundController.PlaySound(soundController.EnterWoodSound);
        _transform.SetParent(parent);
        _canEnterlog = false;
        await UniTask.Delay(10);
        isStaticKnife = true;
        Vibration.Vibrate(200);
    }

    private void CheckOnStatic()
    {
        _collider2D.enabled = isStaticKnife;
        _canEnterlog = !isStaticKnife;
    }

    private async void StartFlip()
    {
        await UniTask.Delay(10);
        Tween rotateTween = _transform.DOLocalRotate(new Vector3(0, 0, 360),
            0.4f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);

        _rigidbody2D.AddForce(new Vector2(0, -20f),
            ForceMode2D.Impulse); 
    }
}
