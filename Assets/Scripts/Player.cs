using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public GameObject gameOverPanel;
    public int HurtDuration = 50;

    public int heart;
    public Text heartNumber;
    public Image totalHealthBar;
    public Image currentHealthBar;


    private Animator anime;
    private bool shouldDelay;
    private int delayCounter;
    private AudioClip ouchSound;

    public AudioSource audioSource;
    public AudioClip projectileCollisionSound;
    public float volume = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        ouchSound = GetComponent<AudioSource>().clip;
        shouldDelay = false;
        delayCounter = 0;

        totalHealthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldDelay)
            delayCounter++;

        if (delayCounter == HurtDuration)
        {
            shouldDelay = false;
            delayCounter = 0;
            anime.SetBool("isHurt", false);
        }

        if (heart <= 0)
        {   
            anime.SetTrigger("isDead");
            
        }

        currentHealthBar.fillAmount = heart/ 10f;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shouldDelay && collision.gameObject.CompareTag("acid"))
        {
            shouldDelay = true;
            heart -= 1;
            anime.SetBool("isHurt", true);
            audioSource.PlayOneShot(ouchSound, volume);
        }

        if (!shouldDelay && collision.gameObject.CompareTag("spike"))
        {
            shouldDelay = true;
            heart -= 1;
            anime.SetBool("isHurt", true);
            audioSource.PlayOneShot(ouchSound, volume);
        }

        if (!shouldDelay && collision.gameObject.CompareTag("enemy"))
        {
            shouldDelay = true;
            heart -= 1;
            anime.SetBool("isHurt", true);
            audioSource.PlayOneShot(ouchSound, volume);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("heart"))
        {
            if (heart < 10)
            {
                heart += 1;
                audioSource.PlayOneShot(audioSource.clip, volume);
                Destroy(other.gameObject);
            }
            
        }
        else if (other.CompareTag("projectile"))
        {
            shouldDelay = true;
            delayCounter = 0;
            Destroy(other.gameObject);
            heart -= 1;
            anime.SetBool("isHurt", true);
            audioSource.PlayOneShot(projectileCollisionSound, volume);
            Debug.Log("I've been shot!! ARGHH!");
            GetComponent<AudioSource>().Play();
        }
    }

    private void Death(){
        gameOverPanel.SetActive(true);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<CharacterController2D>().enabled = false;
        GetComponentInChildren<Blower>().enabled = false;
        GetComponent<Animator>().enabled = false;
    }
}

