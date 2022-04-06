using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource loseSound;
    [SerializeField] private AudioSource knivesHitSound;
    [SerializeField] private AudioSource knivesThrowSound;
    [SerializeField] private AudioSource woodCrackSound;
    [SerializeField] private AudioSource enterWoodSound;
    [SerializeField] private AudioSource appleCutSound;
    public AudioSource LoseSound => loseSound;
    public AudioSource KnivesHitSound => knivesHitSound;
    public AudioSource WoodCrackSound => woodCrackSound;
    public AudioSource EnterWoodSound => enterWoodSound;
    public AudioSource KnivesThrowSound => knivesThrowSound;
    public AudioSource AppleCutSound => appleCutSound;

    public void PlaySound(AudioSource sound)
    {
        if (sound.isPlaying)
            sound.Stop();
        sound.Play();
    }
}
