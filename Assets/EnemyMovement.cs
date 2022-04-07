using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    /*
     * - Public Variables -
     * playerTransform: the Transform component of the player or other character that this enemy should chase.
     * shouldChase: set to true if the enemy should chase the player, false if it should move back and forth only.
     * roamingRange: the standard movement range of the enemy, i.e. the distance it should roam before changing direction.
     * visionRange: the vision/sight range of the enemy. It determines how near the player has to be before the enemy will chase it.
     * speed: the speed at which the enemy should move.
     * epsilon: a variable which could be modified to change smoothness of enemy position reset.
     * direction: the direction the enemy will start moving in. 1 makes it start going left, -1 makes it start going right.
     */
    public Transform playerTransform;
    public bool shouldChase = true;
    public float roamingRange = 7.0f;
    public float visionRange = 3.0f;
    public float speed = 0.05f;
    public float epsilon = 0.005f;
    public int direction = 1;

    float counter;
    float distance;
    bool isChasing;
    bool isMovingBack;
    int directionMemory;
    Vector2 movement;
    Vector3 startPosition;
    Vector3 startDirection;

    // Start is called before the first frame update
    void Start()
    {   
        // Remember starting direction
        directionMemory = direction;
        // Initialize starting position
        startPosition = transform.position;
        // Does not chase at start
        isChasing = false;
        // Is not moving back at start
        isMovingBack = false;
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
        if (isChasing == true)
        {
            if (!(shouldChase && distance <= visionRange))
            {   
                // Move back to starting position
                isChasing = false;
                isMovingBack = true;
                Flip();
                return;
            } 
        }
        if (shouldChase && distance <= visionRange)
        {   
            // Go chase
            isChasing = true;
            isMovingBack = false;
        } 
        else
        {   
            // Go roam
            isChasing = false;
        }
    }

    void FixedUpdate()
    {
        if (isChasing)
        {
            ChasePlayer(movement);
        }
        else if (isMovingBack)
        {
            ResetPosition();
        }
        else
        {
            Roam();
        }

        Debug.DrawLine(startPosition, startPosition - directionMemory * new Vector3(roamingRange, 0, 0), new Color(1.0f, 1.0f, 0.0f));
        Debug.DrawLine(transform.position - new Vector3(visionRange * 0.5f, 0, 0), transform.position + new Vector3(visionRange * 0.5f, 0, 0), new Color(1.0f, 0.0f, 1.0f));
    }

    void Roam()
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

    void ResetPosition()
    {   
        // Determine direction to starting position
        startDirection = startPosition - transform.position;
        startDirection.Normalize();
        // Move towards starting position
        transform.position = new Vector2(transform.position.x + (startDirection * speed).x, transform.position.y);
        // Stop moving towards starting position when we're close enough
        if (Mathf.Abs(transform.position.x - startPosition.x) < epsilon)
        {
            transform.position = new Vector2(startPosition.x, transform.position.y);
            isMovingBack = false;
            isChasing = false;
            counter = 0.0f;
            direction = directionMemory;
        }
    }

    void ChasePlayer(Vector2 playerDirection)
    {   
        // Move towards the direction of the player
        transform.position = new Vector2(transform.position.x + (playerDirection * speed).x, transform.position.y);
        DirectionCheck();
    }

    void DirectionCheck()
    {
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
