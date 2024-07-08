using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Start() {
        Time.timeScale = 1;
    }

    public void ChangeScene(string sceneName){
        SceneManager.LoadScene(sceneName);
        Debug.Log("CLOCK");
    }
}