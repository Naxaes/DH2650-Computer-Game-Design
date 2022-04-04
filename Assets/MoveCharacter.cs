using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    public Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector2(transform.position.x - 0.1f, transform.position.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x + 0.1f, transform.position.y);
        }
    }
}
