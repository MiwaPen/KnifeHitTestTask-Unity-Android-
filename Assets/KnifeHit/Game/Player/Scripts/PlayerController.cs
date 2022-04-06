using UnityEngine;
using Zenject;
public class PlayerController : MonoBehaviour
{
    [Inject] private KnifeSpawnerController _knifeSpawner;
    private bool _canThrowKnife = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryThrowKnife();
        }
    }
    public void TryThrowKnife()
    {
        if (_canThrowKnife == false) return;
        _knifeSpawner.ThrowKnife();
    }

    public void EnablePlayerController()
    {
        _canThrowKnife = true;
    }

    public void DisablePlayerController()
    {
        _canThrowKnife = false;
    }
}
