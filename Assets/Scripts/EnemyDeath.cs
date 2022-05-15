using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip deathSound;
    public float volume = 0.1f;

    private void OnDestroy()
    {
        Debug.Log(deathSound);
        if (this != null && audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound, volume);
        }
    }
}
