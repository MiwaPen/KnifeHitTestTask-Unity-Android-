using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "KnifeInfo", menuName = "Gameplay/Items/New KnifeInfo")]
public class KnifeInfo : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private Ease _ease;
    [SerializeField] private float _endPoint;

    public float speed => this._speed;
    public Ease ease => this._ease;
    public float endPoint => this._endPoint;

}
