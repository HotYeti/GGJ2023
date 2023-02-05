using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public AudioClip music;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.volume = 0.04f;
        audioSource.Play();
    }
}