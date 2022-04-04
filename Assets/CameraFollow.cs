using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    public int horizontalOffset = 5;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(followTransform.position.x + horizontalOffset, followTransform.position.y, this.transform.position.z);

    }
}
