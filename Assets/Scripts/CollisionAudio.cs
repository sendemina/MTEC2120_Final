using UnityEngine;

public class CollisionAudio : MonoBehaviour
{
    public AudioClip collisionSound; // Assign your audio clip in the Unity inspector
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if an AudioSource component is not attached, and if not, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the collision sound clip to the AudioSource component
        audioSource.clip = collisionSound;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if there's a collision and the AudioSource is not already playing
        if (collisionSound != null && !audioSource.isPlaying)
        {
            // Play the collision sound
            audioSource.Play();
        }
    }
}