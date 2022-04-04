using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /*
     * - Public Variables -
     * transform: the Transform component of the GameObject that the camera should follow.
     * horizontalOffset: the camera offset in x-direction. The camera is offset 'horizontalOffset' units to the positive x-direction (right).
     */
    public Transform followTransform;
    public int horizontalOffset = 5;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(followTransform.position.x + horizontalOffset, followTransform.position.y, this.transform.position.z);

    }
}
