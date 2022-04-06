using UnityEngine;

[CreateAssetMenu(fileName ="AppleInfo",menuName ="Gameplay/Items/New AppleInfo")]
public class AppleInfo : ScriptableObject
{
    [SerializeField] private int _spawnChanse;
    [SerializeField] private int _appleValue;

    public int spawnChanse => this._spawnChanse;
    public int appleValue => this._appleValue;
}
