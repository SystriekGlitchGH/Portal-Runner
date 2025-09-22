using UnityEngine;

public class AudioOnce : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundClip;
    private bool hasPlayed = false;

    private void OnTriggerEnter2D(Collider2D player) {
        if (!hasPlayed && player.CompareTag("Player"))
        {
            audioSource.PlayOneShot(soundClip);
            hasPlayed = true;
        }
    }
}
