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
    [SerializeField]
    public float force = 75f;

    [SerializeField]
    public float radius = 1f;

    [SerializeField]
    public float maxDistance = 10f;


    static Color RED = new Color(1f, 0f, 0f);
    static Color GREEN = new Color(0f, 1f, 0f);


    void FixedUpdate()
    {
        Vector2 vecToMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 mouseDir = vecToMouse.normalized;
        Vector2 blowAnchor = new Vector2(transform.position.x, transform.position.y) + mouseDir * 0.2f;
        Vector2 blowVec = mouseDir * maxDistance;

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.DrawRay(blowAnchor + mouseDir.Rotate(90f) * radius, blowVec, RED);
            Debug.DrawRay(blowAnchor, blowVec, RED);
            Debug.DrawRay(blowAnchor + mouseDir.Rotate(-90f) * radius, blowVec, RED);

            var allCollisions = Physics2D.CircleCastAll(blowAnchor, radius, mouseDir, maxDistance);
            Debug.Log("Fire hit " + allCollisions.Length + " entities");

            foreach (var collision in allCollisions)
            {
                var collisionBody = collision.rigidbody;

                // var attenuation = Mathf.Clamp(collision.distance * collision.distance, 1f, 100f);  // <- DOESN'T WORK!

                var distance = Mathf.Clamp(Vector2.Distance(blowAnchor, new Vector2(collision.transform.position.x, collision.transform.position.y)), 1f, maxDistance);
                var attenuation = distance * distance;
                if (collision.transform.gameObject != this.gameObject)
                    collisionBody.AddForce(mouseDir * force * (1 / attenuation));
                else
                    collisionBody.AddForce(-mouseDir * force);
            }
        }
        else
        {
            Debug.DrawRay(blowAnchor + mouseDir.Rotate(90f) * radius, blowVec, GREEN);
            Debug.DrawRay(blowAnchor, blowVec, GREEN);
            Debug.DrawRay(blowAnchor + mouseDir.Rotate(-90f) * radius, blowVec, GREEN);
        }
    }
}