using System.Collections;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource musicSource;

    private bool hasPlayed = false;

    void Start()
    {
        // Đầu game: đảm bảo nhạc TẮT
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasPlayed) return;

        if (other.CompareTag("Player"))
        {
            Play();
            Debug.Log("Player entered trigger, playing music.");
        }
    }

    void Play()
    {
        if (musicSource == null) return;

        musicSource.Play();
        hasPlayed = true;
    }
}
