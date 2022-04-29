using UnityEngine;

public class DisableCollision : MonoBehaviour
{
    public string TagToIgnore;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagToIgnore))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
