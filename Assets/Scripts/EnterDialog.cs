using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnterDialog : MonoBehaviour
{
    public GameObject enterDialog;

  
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
           enterDialog.SetActive(true);
          //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerExit2D(Collider2D other) {
         if(other.gameObject.CompareTag("Player"))
            enterDialog.SetActive(false);
    }
}
