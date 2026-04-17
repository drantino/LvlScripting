using UnityEngine;

[CreateAssetMenu(fileName = "AudioEffectSO", menuName = "Scriptable Objects/AudioEffectSO")]
public class AudioEffectSO : ScriptableObject
{
    public string clipName;
    public AudioClip audioClip;
}
