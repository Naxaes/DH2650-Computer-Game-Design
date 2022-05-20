using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimedShooting : MonoBehaviour
{
    /*
     * - Public Variables -
     * target: the target the enemy should shoot at.
     * projectile: the projectile GameObject that will be fired.
     * power: the power at which the projectile should be fired.
     * gravityScale: the gravity-scale, or heaviness, of the projectile to be fired.
     * interval: the time between consecutive shots.
     */
    public GameObject target;
    public GameObject projectile;
    public float gravityScale = 0.1f;
    public float power = 2.0f;
    public int interval = 3;

    public AudioSource audioSource;
    public float volume = 0.1f;

    private float nextTime;
    Vector3 projectileMotion;

    void Start()
    {
        nextTime = Time.time;
        audioSource.Stop();
    }

    void Update()
    {
        FireProjectilesInIntervals();
    }

    void FireProjectilesInIntervals()
    {   
        if (Time.time >= nextTime)
        {
            nextTime += interval;
            Vector3 targetPos = target.transform.position;
            Vector3 projectileStartPos = 0.75f * transform.position + 0.25f * targetPos;
            GameObject newProjectile = Instantiate(projectile, projectileStartPos, transform.rotation) as GameObject;
            if (!newProjectile.GetComponent<Rigidbody2D>())
            {
                newProjectile.AddComponent<Rigidbody2D>();
            }
            Rigidbody2D projectileRB = newProjectile.GetComponent<Rigidbody2D>();
            projectileRB.gravityScale = gravityScale;
            projectileRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            projectileMotion = Vector3.Normalize(targetPos - transform.position) * power;
            projectileRB.AddForce(projectileMotion, ForceMode2D.Impulse);

            PlayProjectileSound();
        }
    }

    void PlayProjectileSound()
    {
        if (GetComponent<Renderer>().isVisible)
        {
            StartCoroutine(AudioHelper.FadeIn(audioSource, 0, volume));
            StartCoroutine(AudioHelper.FadeOut(audioSource, 0.35f, 0));
        }
    }
}