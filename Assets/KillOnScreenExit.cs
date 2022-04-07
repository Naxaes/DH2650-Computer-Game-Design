using UnityEngine;

public class KillOnScreenExit : MonoBehaviour
{
    void Update()
    {
        if (!Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(transform.position)))
        {
            Destroy(gameObject);
        }
    }
}
