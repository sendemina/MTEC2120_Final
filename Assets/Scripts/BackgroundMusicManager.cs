using System.Collections;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioClip[] backgroundMusicClips; // List of audio clips for background music
    public float fadeDuration = 2.0f; // Duration of the fade transition in seconds

    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private bool isFading = false;
    private float targetVolume = 1.0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; // We'll handle looping manually
        PlayNextClip();
    }

    private void Update()
    {
        if (!isFading && !audioSource.isPlaying)
        {
            PlayNextClip();
        }
    }

    private void PlayNextClip()
    {
        int nextClipIndex = (currentClipIndex + 1) % backgroundMusicClips.Length;
        AudioClip nextClip = backgroundMusicClips[nextClipIndex];

        // Crossfade to the next clip
        StartCoroutine(CrossfadeToClip(nextClip, fadeDuration));

        currentClipIndex = nextClipIndex;
    }

    private IEnumerator CrossfadeToClip(AudioClip newClip, float duration)
    {
        isFading = true;

        // Fade out the current clip
        float startVolume = audioSource.volume;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }
        audioSource.volume = 0f;
        audioSource.Stop();

        // Change the audio clip and start playing
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in the new clip
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, targetVolume, t / duration);
            yield return null;
        }
        audioSource.volume = targetVolume;

        isFading = false;
    }
}

