using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "Gameplay/Config/New LevelInfo")]
public class LevelInfo : ScriptableObject
{
    [SerializeField] private int levelStartKnivesCount;
    [SerializeField] private int levelMaxKnivesCount;
    [SerializeField] private float levelStartLogSpeed;
    [SerializeField] private float levelMaxLogSpeed;
    [SerializeField] private float rotationChangeStep;

    public int LevelStartKnivesCount => levelStartKnivesCount;
    public int LevelMaxKnivesCount => levelMaxKnivesCount;
    public float LevelStartLogSpeed => levelStartLogSpeed;
    public float LevelMaxLogSpeed => levelMaxLogSpeed;
    public float RotationChangeStep => rotationChangeStep;
}
