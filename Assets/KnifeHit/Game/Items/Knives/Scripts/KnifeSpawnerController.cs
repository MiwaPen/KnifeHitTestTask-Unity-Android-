using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class KnifeSpawnerController : MonoBehaviour
{
    private KnifeBehaviourScript _currentKnife = null;
    public int _knifeCount { get; private set; }
    [SerializeField] private int _throwDelay;
    [Inject] private KnivesPooler knivesPooler;
    private bool _canSpawnNewKnife;
    private Transform _spawner;
    public Action OnKnivesCounterChange;

    void Awake()
    {
        _spawner = this.transform;
    }

    public void ThrowKnife()
    {
        if (_currentKnife == null) return;

        _currentKnife.TryMove();
        _currentKnife = null;
        _knifeCount--;
        OnKnivesCounterChange?.Invoke();
        TrySpawnNewKnife();
    }

    public void EnableKnifeSpawner()
    {
        _canSpawnNewKnife = true;
        SpawnKnife();
    }
    public void DisableKnifeSpawner()
    {
        _canSpawnNewKnife = false;
    }

    public void SetMaxCount(int count)
    {
        _knifeCount = count;
        OnKnivesCounterChange?.Invoke();
    }

    public void TrySpawnNewKnife()
    {
        if (_canSpawnNewKnife == false || _knifeCount <= 0 ) return;

        SpawnWithDelay();     
    }

    private async void SpawnWithDelay()
    {
        await UniTask.Delay(_throwDelay);
        if (_canSpawnNewKnife == false) return;
        SpawnKnife();
    }

    private void SpawnKnife()
    {
        GameObject newKnife = knivesPooler.GetFromPool(_spawner.position);
        _currentKnife = newKnife.GetComponent<KnifeBehaviourScript>();
    }
}
