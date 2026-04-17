using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource myAudioSource;
    public string currentPlayingAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
}
