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
        LoadLevel();

        Scene scene = SceneManager.GetActiveScene();
        
        if (scene.name == "Main Menu"){
            ResetLevel();
        }
    }

    public void ChangeScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void PlayLevel(){
        Debug.Log(levelCurrent);
        string LevelName = "Level " + levelCurrent.ToString();
        ChangeScene(LevelName);
    }

    LevelData levelData;
    int levelCurrent = 1;

    //berguna untuk Check Save File ada atau tidak
    public void CheckSaveFile()
    {
        if (File.Exists(Application.dataPath + "/Level.json")) LoadLevel();
        else SaveLevel();
    }

		//berguna untuk save level ke json
    private void SaveLevel()
    {
        levelData = new LevelData();
        levelData.level = levelCurrent;
        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(Application.dataPath + "/Level.json", json);
    }

		//berguna untuk load level dari json
    private void LoadLevel()
    {
        string json;
        json = File.ReadAllText(Application.dataPath + "/Level.json");
        LevelData levelData = JsonUtility.FromJson<LevelData>(json);
        levelCurrent = levelData.level;
    }

		//berguna untuk mengganti nilai level / assign level
    public void ChangeLevel(int newLevelUnlocked)
    {
        levelCurrent = newLevelUnlocked;
        SaveLevel();
    }

		//berguna untuk reset level
    public void ResetLevel()
    {
        levelCurrent = 1;
        SaveLevel();
    }

    public void LevelFinishIncrement(){
        levelCurrent += 1;
        ChangeLevel(levelCurrent);

        Debug.Log(levelCurrent);
        
        if (levelCurrent == 3){
            ChangeScene("Main Menu");
            
        } else {
            PlayLevel();
        }
    }
}