using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip deathSound;
    public float volume = 0.1f;
    private bool onScreen;

    private void Update()
    {
        onScreen = GetComponent<Renderer>().isVisible;
    }

    private void OnDestroy()
    {
        if (onScreen)
        {
            if (this != null && audioSource != null && deathSound != null)
            {
                audioSource.PlayOneShot(deathSound, volume);
            }
        }
            
    }
}
