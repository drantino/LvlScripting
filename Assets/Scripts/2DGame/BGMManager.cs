using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    public AudioSource myAudioSource;
    public List<AudioEffectSO> bgmClips;
    public Dictionary<string, AudioClip> bgms;
    public string currentPlayingAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.volume = TwoDGameState.Instance.settings.Volume;
        if (bgms  == null)
        {
            BuildDictionary();
        }
        if(currentPlayingAudio == "")
        {
            PlayBGMByName("MainMenuBGM");
        }
    }
    public void BuildDictionary()
    {
        bgms = new();
        if (bgmClips.Count > 0)
        {
            foreach (AudioEffectSO soundEffect in bgmClips)
            {
                bgms.TryAdd(soundEffect.name, soundEffect.audioClip);
            }
        }
    }
    public void PlayBGMByName(string name)
    {
        if (currentPlayingAudio != name && bgms.ContainsKey(name))
        {
            myAudioSource.clip = bgms[name];
            myAudioSource.Play();
            currentPlayingAudio = name;
        }
        else
        {
            Debug.LogWarning($"No audioclip by {name}.");
        }
    }
}
