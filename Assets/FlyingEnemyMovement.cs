using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
    /*
    * - Public Variables -
    * roamingRange: the standard movement range of the enemy, i.e. the distance it should roam before changing direction.
    * allowedVerticalDisplacement: the enemy will be allowed to be displaced roamingRange/allowedVerticalDisplacement distance from starting point.
    * visionRange: the vision/sight range of the enemy. It determines how near the player has to be before the enemy will chase it.
    * speed: the speed at which the enemy should move.
    * epsilon: a variable which could be modified to change smoothness of enemy position reset.
    * direction: the direction the enemy will start moving in. 1 makes it start going left, -1 makes it start going right.
    * airResistance: determines how quickly the velocity applied by the blower will be reduced over time.
    * frequency: determines how many trigonometric waves the enemy will go through while roaming.
    * amplitude: determines how far away from starting vertical position the enemy will fly in its trigonometric waves.
    *            this is closely related to and dependent on allowedVerticalDisplacement. Too high amplitude will force
    *            the enemy to reset to original position, if allowedVerticalDisplacement is too low. I haven't quite yet
    *            figured out a nice way to compose these two... For now, they are dependent on each other to ensure truly smooth movement.
    */
    public float roamingRange = 15.0f;
    public float allowedVerticalDisplacement = 3.0f;
    public float speed = 0.05f;
    public float epsilon = 0.1f;
    public int direction = 1;
    public float airResistance = 0.006f;
    public float frequency = 2f;
    public float amplitude = 0.05f;

    float counter;
    float distance;
    bool isMovingBack;
    int directionMemory;
    Vector3 startPosition;
    Vector3 startDirection;
    Rigidbody2D rb;
    float xComponent;
    float yComponent;

    /*
     * Start. Called before first frame update.
     * This function is simply used to set
     * variables needed upon start.
     */
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        // Remember starting direction
        directionMemory = direction;
        // Initialize starting position
        startPosition = transform.position;
        // Is not moving back at start
        isMovingBack = false;
        // Reset counter
        counter = 0.0f;
    }

    /*
     * Update. Called once per frame.
     * This function performs calculations and
     * comparisons in order to set the appropriate
     * boolean variables for this frame.
     */
    void Update()
    {
        xComponent = rb.velocity.x;
        yComponent = rb.velocity.y;
        
        if (xComponent != 0)
        {
            if (xComponent > 0)
            {
                xComponent -= airResistance;
            }
            else
            {
                xComponent += airResistance;
            }
        }
        if (yComponent != 0)
        {
            if (yComponent > 0)
            {
                yComponent -= airResistance;
            }
            else
            {
                yComponent += airResistance;
            }
        }
        rb.velocity = new Vector2(xComponent, yComponent);
        
        
        if (direction != 0)
        {
            transform.localScale = new Vector3(direction * (-1.2337f), 1.2337f, 1.2337f);
        }
    }

    /*
   * FixedUpdate.
   * Here we set the state of the enemy
   * depending on boolean variables.
   */
    void FixedUpdate()
    {
        if (isMovingBack)
        {
            ResetPosition();
            Debug.Log("Reseting");
        }
        else
        {
            Roam();
            Debug.Log("Roaming");
        }

        Debug.DrawLine(startPosition, startPosition - directionMemory * new Vector3(roamingRange, 0, 0), new Color(1.0f, 1.0f, 0.0f));
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
        if (DistanceCheck())
        {
            // Move back to starting position
            isMovingBack = true;
            Flip();
            return;
        }
        // Check that we haven't roamed too far
        if (counter <= roamingRange)
        {
            transform.position = TrigFlight();
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
        if (startDirection.x > 0)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
        // Move towards starting position
        transform.position = new Vector2(transform.position.x + (startDirection * speed * 1.5f).x, transform.position.y + (startDirection * speed * 1.5f).y);
        // Stop moving towards starting position when we're close enough
        if (Mathf.Abs(transform.position.x - startPosition.x) < epsilon && Mathf.Abs(transform.position.y - startPosition.y) < epsilon)
        {
            transform.position = new Vector2(startPosition.x, transform.position.y);
            isMovingBack = false;
            counter = 0.0f;
            direction = directionMemory;
            Debug.Log("Home");
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
     * Function to determine when,
     * i.e. at which distance from the
     * starting position, the enemy should
     * start to move back when displaced.
     */
    bool DistanceCheck()
    {
        bool check = (transform.position.x > startPosition.x + roamingRange * 1.5) || (transform.position.x < startPosition.x - roamingRange * 1.5);
        check = check || (transform.position.y > startPosition.y + roamingRange / allowedVerticalDisplacement);
        check = check || (transform.position.y < startPosition.y - roamingRange / allowedVerticalDisplacement);
        return check;
    }

    /*
     * Makes the enemy fly according
     * to a trigonometric function according to
     * provided amplitude and frequency.
     */
    Vector2 TrigFlight()
    {
        Vector3 pos;
        if (direction == 1)
        {
            pos = transform.position - transform.right * speed;
            pos += amplitude * transform.up * Mathf.Cos(Time.time * frequency);
            return new Vector2(pos.x, pos.y);
        } 
        else
        {
            pos = transform.position + transform.right * speed;
            pos += amplitude * transform.up * Mathf.Cos(Time.time * frequency);
            return new Vector2(pos.x, pos.y);
        }
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
            Destroy(gameObject);
        }
        if (other.tag == "spike")
        {
            Destroy(gameObject);
        }
    }
}
