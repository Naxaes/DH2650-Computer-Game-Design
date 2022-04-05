using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    /*
     * - Public Variables -
     * transform: the Transform component of the enemy.
     * playerTransform: the Transform component of the player or other character that this enemy should chase.
     * shouldChase: set to true if the enemy should chase the player, false if it should move back and forth only.
     * roamingRange: the standard movement range of the enemy, i.e. the distance it should roam before changing direction.
     * visionRange: the vision/sight range of the enemy. It determines how near the player has to be before the enemy will chase it.
     * speed: the speed at which the enemy should move.
     */
    public Transform transform;
    public Transform playerTransform;
    public bool shouldChase = true;
    public float roamingRange = 7.0f;
    public float visionRange = 3.0f;
    public float speed = 2.0f;
    public int direction = 1;

    float counter;
    float distance;
    bool isChasing;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {   
        // Does not chase at start
        isChasing = false;

        // Reset counter
        counter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {   
        // Calculate the direction from enemy to player
        Vector2 dir = playerTransform.position - transform.position;
        dir.Normalize();
        movement = dir;

        // Calculate the horizontal distance between enemy and player
        distance = Mathf.Abs(transform.position.x - playerTransform.position.x);
        if (shouldChase && distance <= visionRange)
        {   
            // Go chase
            isChasing = true;
        } else
        {   
            // Don't chase
            isChasing = false;
        }
    }

    void FixedUpdate()
    {
        if (isChasing)
        {
            chasePlayer(movement);
        }
        else
        {   
            // Check that we haven't roamed too far
            if (counter <= roamingRange)
            {
                transform.position = new Vector2(transform.position.x - (direction * speed), transform.position.y);
                // Increase counter with distance traveled
                counter += speed;
            }
            // If we have roamed too far, flip and move the other way
            if (counter > roamingRange)
            {
                Flip();
            }
        }
    }

    void chasePlayer(Vector2 playerDirection)
    {
        transform.position = new Vector2(transform.position.x + (playerDirection * speed).x, transform.position.y);
        if (transform.position.x > playerTransform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }

    void Flip()
    {
        direction *= -1;
        counter = 0.0f;
    }
}
