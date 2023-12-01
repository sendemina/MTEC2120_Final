using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private List<AudioClip> oneShotAudioClips;

    private void Awake()
    {
        // Ensure only one AudioManager instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudioAtPoint(AudioClip clip, Vector3 position, float volume = 1.0f)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
    }

    public void PlayOneShotAudio(int index)
    {
        if (index >= 0 && index < oneShotAudioClips.Count)
        {
            AudioClip clip = oneShotAudioClips[index];
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }

    public void PlayRandomAudioFromList(List<AudioClip> clips)
    {
        if (clips.Count > 0)
        {
            int randomIndex = Random.Range(0, clips.Count);
            AudioClip clip = clips[randomIndex];
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
