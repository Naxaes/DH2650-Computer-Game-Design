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
     * horizontalPower: the power, or speed, at which the projectile will be fired in x-direction.
     * verticalPower: the power, or speed, at which the projectile will be fired in positive y-direction.
     * gravityScale: the gravity-scale, or heaviness, of the projectile to be fired.
     * interval: the time between consecutive shots.
     */
    public Transform target;
    public GameObject enemy;
    public GameObject projectile;
    public float horizontalPower = 12f;
    public float verticalPower = 0.08f;
    public float gravityScale = 0.1f;
    public int interval = 3;

    int direction;
    EnemyMovement movement;
    private float nextTime = 0.0f;
    Vector3 projectileMotion;

    void Start()
    {
        movement = enemy.GetComponent<EnemyMovement>();
    }

    void Update()
    {
        FireProjectilesInIntervals();
    }

    void FireProjectilesInIntervals()
    {
        direction = movement.direction;
        if (Time.time >= nextTime)
        {
            nextTime += interval;
            Quaternion rotation = transform.rotation;
            if (direction == -1)
            {
                rotation *= Quaternion.Euler(0, 180f, 0);
            }
            GameObject newProjectile = Instantiate(projectile, transform.position - direction * target.right, rotation) as GameObject;
            if (!newProjectile.GetComponent<Rigidbody2D>())
            {
                newProjectile.AddComponent<Rigidbody2D>();
            }
            Rigidbody2D projectileRB = newProjectile.GetComponent<Rigidbody2D>();
            projectileRB.gravityScale = gravityScale;
            projectileRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            projectileMotion = new Vector3(target.right.x, target.right.y - direction * verticalPower, target.right.z);
            projectileRB.AddForce(projectileMotion * horizontalPower * -direction, ForceMode2D.Impulse);
        }
    }
}
