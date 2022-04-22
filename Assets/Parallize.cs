using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallize : MonoBehaviour
{
    public Transform Camera;
    public float moveRate;
    private float startPonitX,startPonitY;
    public bool lockY;//false
    // Start is called before the first frame update
    void Start()
    {
        startPonitX= transform.position.x;
        startPonitY= transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(lockY)
        transform.position = new Vector2(startPonitX+Camera.position.x*moveRate,transform.position.y);
        else
        {
            transform.position = new Vector2(startPonitX+Camera.position.x*moveRate,startPonitY+transform.position.y*moveRate);
        }
    }
}
