using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject Menu;
    public static bool isPaused;
    
    void Start()
    {
        Menu.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(isPaused){
                Menu.SetActive(false);
                ResumeGame();
            } else {
                Menu.SetActive(true);
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        Menu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame() {
        Menu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}   