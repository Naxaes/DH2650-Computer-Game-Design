using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    /*
     * - Public Variables -
     * transform: the Transform component of the enemy that should move.
     * isMoving: set to true if the enemy should move, false if the enemy should stay still.
     * range: the range of the enemy, i.e. the distance it should move before changing direction.
     * speed: the speed at which the enemy should move.
     */
    public Transform transform;
    public bool isMoving = true;
    public float range = 7.0f;
    public float speed = 0.005f;

    float counter;
    int direction;

    // Start is called before the first frame update
    void Start()
    {   
        // Reset counter
        counter = 0.0f;

        // Reset direction
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (counter <= range)
            {
                transform.position = new Vector2(transform.position.x - speed * direction, transform.position.y);
                counter += speed;
            }

            if (counter > range)
            {
                direction *= -1;
                counter = 0.0f;
            }
        }
    }
}
