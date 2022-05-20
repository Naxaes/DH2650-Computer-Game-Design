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
    public bool IsSmall;
    public float roamingRange = 7.0f;
    public float visionRange = 3.0f;
    public float speed = 0.05f;
    public float epsilon = 0.05f;
    public int direction = 1;
    

    private Animator anime;
    
    float counter;
    float distance;
    bool isChasing;
    bool isMovingBack;
    int directionMemory;
    bool isAlive;
    Vector2 movement;
    Vector3 startPosition;
    Vector3 startDirection;

    /*
     * Start. Called before first frame update.
     * This function is simply used to set
     * variables needed upon start.
     */
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

        isAlive = true;
        anime = GetComponent<Animator>();
        
    }

    /*
     * Update. Called once per frame.
     * This function performs calculations and
     * comparisons in order to set the appropriate
     * boolean variables for this frame.
     */
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

        if(direction != 0){
            if(IsSmall){
                transform.localScale = new Vector3(direction*(-0.7f),0.7f,0.7f);
            }
            else
            transform.localScale = new Vector3(direction*(-1.2337f),1.2337f,1.2337f);
        }
    }

    /*
     * FixedUpdate.
     * Here we set the state of the enemy
     * depending on boolean variables.
     */
    void FixedUpdate()
    {
        
        if(isAlive){
        if (isChasing)
        {
            ChasePlayer(movement);
            Debug.Log("Chasing");
        }
        else if (isMovingBack)
        {
            ResetPosition();
            Debug.Log("Reseting");
        }
        else
        {
            Roam();
            Debug.Log("Roaming");
        }
        }

        Debug.DrawLine(startPosition, startPosition - directionMemory * new Vector3(roamingRange, 0, 0), new Color(1.0f, 1.0f, 0.0f));
        Debug.DrawLine(transform.position - new Vector3(visionRange * 0.5f, 0, 0), transform.position + new Vector3(visionRange * 0.5f, 0, 0), new Color(1.0f, 0.0f, 1.0f));
    }

    /*
     * Function to make the enemy
     * roam back and forth according to the roaming range.
     * This function also makes sure that the enemy
     * starts reseting its position if it gets blown too far away
     * whilst roaming.
     */
    void Roam()
    {   
        if ((transform.position.x > startPosition.x + roamingRange) || (transform.position.x < startPosition.x - roamingRange))
        {
            // Move back to starting position
            isChasing = false;
            isMovingBack = true;
            Flip();
            return;
        }
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

    /*
     * Function to reset the position of the enemy,
     * i.e. move it back to its starting position and 
     * commence roaming.
     */
    void ResetPosition()
    {   
        // Determine direction to starting position
        startDirection = startPosition - transform.position;
        startDirection.Normalize();
        if(startDirection.x > 0)
        {
            direction = -1;
        } 
        else
        {
            direction = 1;
        }
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
            Debug.Log("Home");
        }
    }

    /*
     * Function to move the enemy towards the 
     * direction of the player when chasing.
     */
    void ChasePlayer(Vector2 playerDirection)
    {   
        // Move towards the direction of the player
        transform.position = new Vector2(transform.position.x + (playerDirection * speed).x, transform.position.y);
        DirectionCheck();
    }

    /*
     * Function used to set the direction to right/left
     * depending on where the player is when chasing.
     */
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
    
    /*
     * Function to flip the direction
     * and reset the counter when roaming range
     * has been reached.
     */
    void Flip()
    {
        direction *= -1;
        counter = 0.0f;
    }

    /*
     * Function to determine what should happen
     * when this enemy collides with different
     * colliders in the scene.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.tag == "acid")
        {
            anime.SetTrigger("isDead");
            isAlive = false;
        }
        if (other.tag == "spike")
        {
            anime.SetTrigger("isDead");
            isAlive = false;
        }
        if (other.tag == "projectile")
        {
            anime.SetTrigger("isDead");
            isAlive = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("acid"))
        {
            anime.SetTrigger("isDead");
            isAlive = false;
        }
    }

    private void Death(){
        Destroy(gameObject);
    }
}
