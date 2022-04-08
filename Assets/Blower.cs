using UnityEngine;


public static class Vector2Extension
{

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}

public class Blower : MonoBehaviour
{

    public Transform arm;
    [SerializeField]
    public float force = 75f;

    [SerializeField]
    public float radius = 1f;

    [SerializeField]
    public float maxDistance = 10f;

    [SerializeField]
    public Collider2D feet;

    [SerializeField]
    public LayerMask ground;

    private GameObject[] myLine;
    
    static Color RED = new Color(1f, 0f, 0f);
    static Color GREEN = new Color(0f, 1f, 0f);

    private void Start()
    {
        myLine = new GameObject[3];
        myLine[0] = new GameObject();
        myLine[0].AddComponent<LineRenderer>();
        myLine[1] = new GameObject();
        myLine[1].AddComponent<LineRenderer>();
        myLine[2] = new GameObject();
        myLine[2].AddComponent<LineRenderer>();
    }


    void FixedUpdate()
    {
        Vector2 vecToMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 mouseDir = vecToMouse.normalized;
        Vector2 blowAnchor = new Vector2(transform.position.x, transform.position.y) + mouseDir * 0.2f;
        Vector2 blowVec = blowAnchor + mouseDir * maxDistance;

        //join the blower sprite
        float angle = Vector2.Angle(mouseDir,Vector2.up);
         if(transform.localScale.x<0){
            if(vecToMouse.x>0)
        arm.eulerAngles = new Vector3(0,0,-angle+90);
        else
        arm.eulerAngles = new Vector3(0,0,angle+90);
        }
        if(transform.localScale.x>0){
        if(vecToMouse.x>0)
        arm.eulerAngles = new Vector3(0,0,-angle-90);
        else
        arm.eulerAngles = new Vector3(0,0,angle-90);
        }
        

        if (Input.GetButton("Fire1"))
        {
            DrawLine(myLine[0].GetComponent<LineRenderer>(), blowAnchor + mouseDir.Rotate(90f) * radius, blowVec + mouseDir.Rotate(90f) * radius, RED);
            DrawLine(myLine[1].GetComponent<LineRenderer>(), blowAnchor, blowVec, RED);
            DrawLine(myLine[2].GetComponent<LineRenderer>(), blowAnchor + mouseDir.Rotate(-90f) * radius, blowVec + mouseDir.Rotate(-90f) * radius, RED);

            var allCollisions = Physics2D.CircleCastAll(blowAnchor, radius, mouseDir, maxDistance);
            Debug.Log("Fire hit " + allCollisions.Length + " entities");

            foreach (var collision in allCollisions)
            {
                var collisionBody = collision.rigidbody;
                if (collisionBody == null) continue;

                // var attenuation = Mathf.Clamp(collision.distance * collision.distance, 1f, 100f);  // <- DOESN'T WORK!

                var distance = Mathf.Clamp(Vector2.Distance(blowAnchor, new Vector2(collision.transform.position.x, collision.transform.position.y)), 1f, maxDistance);
                var attenuation = distance; // * distance;
                if (collision.transform.gameObject != this.gameObject)
                    collisionBody.AddForce(mouseDir * force * (1 / attenuation));
                else if (!feet.IsTouchingLayers(ground))
                    collisionBody.AddForce(-mouseDir * force);
            }
        }
        else
        {
            DrawLine(myLine[0].GetComponent<LineRenderer>(), blowAnchor + mouseDir.Rotate(90f) * radius, blowVec + mouseDir.Rotate(90f) * radius, GREEN);
            DrawLine(myLine[1].GetComponent<LineRenderer>(), blowAnchor, blowVec, GREEN);
            DrawLine(myLine[2].GetComponent<LineRenderer>(), blowAnchor + mouseDir.Rotate(-90f) * radius, blowVec + mouseDir.Rotate(-90f) * radius, GREEN);
        }
    }



    void DrawLine(LineRenderer lr, Vector3 start, Vector3 end, Color color)
    {
        // myLine.transform.position = start;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }


}