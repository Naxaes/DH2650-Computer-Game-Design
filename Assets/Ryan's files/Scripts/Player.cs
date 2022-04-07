using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;

    public float speed;
    public float jumpForce;
    public LayerMask ground;
    public Collider2D coll;
    public Animator anime;

    public int heart;
    public Text heartNumber;

    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
    }

    void Movement(){
         float horizontalMove = Input.GetAxis("Horizontal");
         float faceDirection = Input.GetAxisRaw("Horizontal");
    
        //transform.position = new Vector3(transform.position.x+speed,transform.position.y,transform.position.z);

          if(horizontalMove != 0 ){
           //Time.deltaTime is making different computer have a same gap of time
             rb.velocity = new Vector2(horizontalMove * speed * Time.deltaTime,rb.velocity.y);
             Debug.Log("jump");
         }
        if(faceDirection != 0 ){
            transform.localScale = new Vector3(faceDirection*-0.1024608f,transform.localScale.y,transform.localScale.z);
        }

        if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground)){
            rb.velocity = new Vector2(rb.velocity.x, 2*jumpForce* Time.deltaTime);   
            Debug.Log("jump");
        }
        if(coll.IsTouchingLayers(ground))
        {
            if(anime.GetBool("isHurt"))
            anime.SetBool("isHurt",false);
        }

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "collections"){
            Destroy(other.gameObject);
            heart +=1;
            heartNumber.text = heart.ToString();
        }

        if(other.tag == "acid"){
            heart -=1;
            heartNumber.text = heart.ToString();
            anime.SetBool("isHurt",true);
            Invoke("jumpHurt",0.01f);
        }
        if(other.tag == "spike"){
            heart -=1;
            heartNumber.text = heart.ToString();
            anime.SetBool("isHurt",true);
            Invoke("jumpHurt",0.01f);
        }
        
    }

    void jumpHurt() {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce*Time.deltaTime);
    }
}

