using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatfom : MonoBehaviour
{

    public Transform upLeftPoint,downRightPoint;
    private float UpY,DownY,LeftX,RightX;
    public float speed;
    public bool isLeftAndRight;
    // Start is called before the first frame update
    void Start()
    {

        UpY = upLeftPoint.position.y;
        DownY = downRightPoint.position.y;
        LeftX = upLeftPoint.position.x;
        RightX = downRightPoint.position.x;
        Destroy(upLeftPoint.gameObject);
        Destroy(downRightPoint.gameObject);
    }

    // Update is called once per frame
     void Update()
    {
        Movement();
    }

    void Movement(){
        if(!isLeftAndRight){
        transform.position = new Vector3(transform.position.x,transform.position.y-speed,transform.position.z);
            if(transform.position.y<DownY){
                speed = -speed;
            }
            if(transform.position.y>UpY){
                speed = -speed;
            }
        }
        if(isLeftAndRight){
        transform.position = new Vector3(transform.position.x+speed,transform.position.y,transform.position.z);
            if(transform.position.x<LeftX){
                speed = -speed;
            }
            if(transform.position.x>RightX){
                speed = -speed;
            }
        }

    }
}
