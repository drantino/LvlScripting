using UnityEngine;
using System.Collections.Generic;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;
    public AudioSource myAudioSource;
    public List<AudioEffectSO> audioClips;
    public Dictionary<string, AudioClip> soundEffects;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.volume = TwoDGameState.Instance.settings.Volume;
        if (soundEffects == null)
        {
            BuildDictionary();
        }
    }
    public void BuildDictionary()
    {
        soundEffects = new();
        if (audioClips.Count > 0)
        {
            foreach (AudioEffectSO soundEffect in audioClips)
            {
                soundEffects.TryAdd(soundEffect.name,soundEffect.audioClip);
            }
        }
    }
    public void PlaySoundByName(string name)
    {
        if (soundEffects.ContainsKey(name))
        {
            myAudioSource.clip = soundEffects[name];
            myAudioSource.Play();
        }
        else
        {
            Debug.LogWarning($"No audioclip by {name}.");
        }
    }
}
