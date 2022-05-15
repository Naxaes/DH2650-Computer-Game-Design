using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{   
    /*
     * - Public Variables:
     * ground: the ground layermask.
     */
    public LayerMask ground;
    private Animator anime;

    Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {   
        // Get collider of this projectile
        coll = gameObject.GetComponent<Collider2D>();
        anime = gameObject.GetComponent<Animator>();

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
            anime.SetTrigger("isBurst");
            coll.isTrigger = false;
            Debug.Log("Destroyed");
        }
        if (other.CompareTag("acid"))
        {
            anime.SetTrigger("isBurst");
             coll.isTrigger = false;
            Debug.Log("Destroyed");
        }
        if (other.CompareTag("spike"))
        {
            anime.SetTrigger("isBurst");
            coll.isTrigger = false;
            Debug.Log("Destroyed");
        }
        if (other.CompareTag("enemy"))
        {
            
            anime.SetTrigger("isBurst");
            coll.isTrigger = false;
            Debug.Log("Enemy Defeated");
        }
    }

    private void Burst(){
        Destroy(gameObject);
    }
}
