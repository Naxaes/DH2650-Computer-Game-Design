using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{

    public GameObject particle;
    public int maxActivePowerOfTwo;
    public float spawnsPerSecond;
    public float spawnOffsetX;

    private Rigidbody2D[] allParticles;
    private int nextToKill = 0;
    private bool filled = false;
    private int frames = 0;
    private int maxActive;
    private int maxActiveMask;
    private float timer;

    void Start()
    {
        maxActive = Mathf.RoundToInt(Mathf.Pow(2, maxActivePowerOfTwo));
        maxActiveMask = maxActive - 1;
        allParticles = new Rigidbody2D[maxActive];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (filled && frames++ == 16)
        {
            frames = 0;
            FreeNonVisible();
        }


        timer += Time.fixedDeltaTime;
        int count = Mathf.FloorToInt(timer * spawnsPerSecond);
        for (int i = 0; i < count; i++)
        {
            var force = new Vector2(2.0f * Random.value - 1.0f, 2.0f * Random.value - 1.0f) * 0.01f;
            if (filled)
            {
                Rigidbody2D rb = allParticles[nextToKill];
                rb.position = transform.position + spawnOffsetX * (2.0f * Random.value - 1.0f) * Vector3.right + spawnOffsetX * (2.0f * Random.value - 1.0f) * Vector3.up;
                rb.velocity = Vector3.zero;
            }
            else
            {
                Rigidbody2D rb = Instantiate(particle, transform).GetComponent<Rigidbody2D>();
                allParticles[nextToKill] = rb;
                rb.position = transform.position + spawnOffsetX * (2.0f * Random.value - 1.0f) * Vector3.right + spawnOffsetX * (2.0f * Random.value - 1.0f) * Vector3.up;
                rb.velocity = Vector3.zero;
            }
            nextToKill = (nextToKill + 1) & maxActiveMask;

            if (!filled && nextToKill == 0)
                filled = true;

            timer = 0f;
        }
    }

    private void FreeNonVisible()
    {
        int offset = 0;
        for (int i = 0; i < maxActive; i++)
        {
            Rigidbody2D rb = allParticles[i];
            if (Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(rb.position)))
            {
                int j = (nextToKill + offset) & maxActiveMask;
                (allParticles[j], allParticles[i]) = (allParticles[i], allParticles[j]);
            }
        }
    }
}
