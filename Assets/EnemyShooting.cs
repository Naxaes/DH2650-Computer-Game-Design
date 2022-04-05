using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    /*
     * - Public Variables -
     * target: the target the enemy should shoot at.
     * enemy: the GameObject of the enemy that should be shooting.
     * projectile: the projectile GameObject that will be fired.
     * power: the power, or speed, at which the projectile will be fired.
     * interval: the time between consecutive shots.
     */
    public Transform target;
    public GameObject enemy;
    public GameObject projectile;
    public float power = 20f;
    public int interval = 2;

    int direction;
    EnemyMovement movement;
    private float nextTime = 0.0f;

    void Start()
    {
        movement = enemy.GetComponent<EnemyMovement>();
    }

    void Update()
    {
        direction = movement.direction;
        if (Time.time >= nextTime)
        {
            nextTime += interval;
            if (projectile)
            {
                Quaternion rotation = transform.rotation;
                if (direction == -1)
                {
                    rotation *= Quaternion.Euler(0, 180f, 0);
                }
                GameObject newProjectile = Instantiate(projectile, transform.position + target.right, rotation) as GameObject;
                if (!newProjectile.GetComponent<Rigidbody>())
                {
                    newProjectile.AddComponent<Rigidbody>();
                }
                Rigidbody projectileRB = newProjectile.GetComponent<Rigidbody>();
                projectileRB.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
                projectileRB.AddForce(target.right * power * -direction, ForceMode.VelocityChange);
            }
        }
    }
}
