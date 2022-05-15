using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Restart : MonoBehaviour
{   
    public Button restartButton;

    void Start(){
        Debug.Log("Start Button");
        Button btn = restartButton.GetComponent<Button>();
        btn.onClick.AddListener(RestartLevel);

    }
    void RestartLevel(){
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
 
}