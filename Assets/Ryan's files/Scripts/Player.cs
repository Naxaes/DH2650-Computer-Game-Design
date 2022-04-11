using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public LayerMask ground;
    public Collider2D coll;
    public Animator anime;
    public int projectileDamageDuration = 500;

    public int heart;
    public Text heartNumber;

    private Rigidbody2D rb;
    private bool shouldDelay;
    private int delayCounter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shouldDelay = false;
        delayCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        delayCounter++;
        if (delayCounter == projectileDamageDuration)
        {
            shouldDelay = false;
        }
        if (coll.IsTouchingLayers(ground) && !shouldDelay)
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
            shouldDelay = true;
            delayCounter = 0;
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

