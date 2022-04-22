using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController2D controller;
    public Animator anime;

    public float runSpeed = 40f;

    float horizontalMove = 0f;

    bool jump = false;
    bool crouch = false;
    bool dashInput = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        anime.SetFloat("isRunning",Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if(Input.GetButtonDown("Crouch"))
        {
            crouch = true;

        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if(Input.GetButtonDown("Fire2"))
        {
            dashInput = true;
        }



    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, dashInput);
        jump = false;
        dashInput = false;
    }
}
