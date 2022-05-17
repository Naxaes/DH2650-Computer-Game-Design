using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    /*
     * - Public Variables -
     * target: the target the enemy should shoot at.
     * projectile: the projectile GameObject that will be fired.
     * horizontalPower: the power, or speed, at which the projectile will be fired in x-direction.
     * verticalPower: the power, or speed, at which the projectile will be fired in positive y-direction.
     * gravityScale: the gravity-scale, or heaviness, of the projectile to be fired.
     * interval: the time between consecutive shots.
     */
    public GameObject target;
    public GameObject projectile;
    public float horizontalPower = 12f;
    public float verticalPower = 0.0f;
    public float gravityScale = 0.1f;
    public int interval = 3;

    public AudioSource audioSource;
    public float volume = 0.1f;

    int direction;
    EnemyMovement movement;
    private float nextTime;
    Vector3 projectileMotion;

    void Start()
    {
        nextTime = Time.time;
        movement = GetComponent<EnemyMovement>();
        audioSource.Stop();
    }

    void Update()
    {
        FireProjectilesInIntervals();
    }

    void FireProjectilesInIntervals()
    {
        Transform targetTransform = target.transform;
        direction = movement.direction;
        if (Time.time >= nextTime)
        {
            nextTime += interval;
            Quaternion rotation = transform.rotation;
            if (direction == -1)
            {
                rotation *= Quaternion.Euler(0, 180f, 0);
            }
            GameObject newProjectile = Instantiate(projectile, transform.position - direction * 1.01f * targetTransform.right, rotation) as GameObject;
            if (!newProjectile.GetComponent<Rigidbody2D>())
            {
                newProjectile.AddComponent<Rigidbody2D>();
            }
            Rigidbody2D projectileRB = newProjectile.GetComponent<Rigidbody2D>();
            projectileRB.gravityScale = gravityScale;
            projectileRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            projectileMotion = new Vector3(targetTransform.right.x, targetTransform.right.y - direction * verticalPower, targetTransform.right.z);
            projectileRB.AddForce(projectileMotion * horizontalPower * -direction, ForceMode2D.Impulse);

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
