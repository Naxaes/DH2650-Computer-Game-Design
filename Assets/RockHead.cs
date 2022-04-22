using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHead : MonoBehaviour
{
    private Rigidbody2D rig;

    private float timer;
    private const float RockHeadWaitingTimeSpan = 2f;
    private const float RockHeadMoveBackTimeSpan = 0.3f;

    private bool isGroundHit = false;
    private Vector3 orignalPosition;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        orignalPosition = rig.position;
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void FixedUpdate() {
        if(isGroundHit){
           timer +=Time.deltaTime;
            
            if(timer >= RockHeadWaitingTimeSpan && transform.position != orignalPosition)
            {
                rig.isKinematic = true;
                rig.gravityScale = 1f;
                transform.position = Vector3.SmoothDamp(transform.position,orignalPosition,ref velocity,RockHeadMoveBackTimeSpan);
            }
            else if(transform.position == orignalPosition){
                isGroundHit = false;
                timer = 0;
             }  
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
        rig.isKinematic = false;
        rig.gravityScale = 2f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            Vector3 PlayerLocalScale = other.gameObject.transform.localScale;
            other.gameObject.transform.localScale = new Vector3(PlayerLocalScale.x,PlayerLocalScale.y*0.1f,PlayerLocalScale.z);

            float colliderSize = other.gameObject.GetComponent<CircleCollider2D>().radius;
            other.gameObject.GetComponent<CircleCollider2D>().radius =  colliderSize*0.1f;

            Vector2 colliderOffset = other.gameObject.GetComponent<CircleCollider2D>().offset;
            other.gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(colliderOffset.x,colliderOffset.y-5) ;


             Destroy(other.gameObject,0.2f);

        }
        if(other.gameObject.layer == 6){
            isGroundHit = true;
        }
    }

}
