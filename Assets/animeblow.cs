using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animeblow : MonoBehaviour
{
     public Animator anime;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
            anime.SetBool("isBlowing",true);
        else  
             anime.SetBool("isBlowing",false);
    }

}
