using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{   
    /*
     * - Public Variables:
     * ground: the ground layermask.
     */
    public LayerMask ground;

    Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {   
        // Get collider of this projectile
        coll = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * Function to determine what should happen
     * when this projectile collides with different
     * colliders in the scene.
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (coll.IsTouchingLayers(ground))
        {
            Destroy(gameObject);
            Debug.Log("Destroyed");
        }
        if (other.tag == "acid")
        {
            Destroy(gameObject);
            Debug.Log("Destroyed");
        }
        if (other.tag == "spike")
        {
            Destroy(gameObject);
            Debug.Log("Destroyed");
        }
        if (other.tag == "enemy")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Debug.Log("Enemy Defeated");
        }
    }
}
