using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public LayerMask ground;
    public Collider2D coll;
    public Animator anime;

    public int heart;
    public Text heartNumber;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coll.IsTouchingLayers(ground))
        {
            if (anime.GetBool("isHurt"))
                anime.SetBool("isHurt", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "collections"){
            Destroy(other.gameObject);
            heart += 1;
            heartNumber.text = heart.ToString();
        }
        if(other.tag == "acid"){
            heart -= 1;
            heartNumber.text = heart.ToString();
            anime.SetBool("isHurt",true);
            Invoke("jumpHurt",0.01f);
        }
        if(other.tag == "spike"){
            heart -= 1;
            heartNumber.text = heart.ToString();
            anime.SetBool("isHurt",true);
            Invoke("jumpHurt",0.01f);
        }
        if(other.tag == "projectile")
        {
            Destroy(other.gameObject);
            heart -= 1;
            heartNumber.text = heart.ToString();
            anime.SetBool("isHurt", true);
            Invoke("jumpHurt", 0.01f);
            Debug.Log("I've been shot!! ARGHH!");
        }
    }

    void jumpHurt() {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce*Time.deltaTime);
    }
}

