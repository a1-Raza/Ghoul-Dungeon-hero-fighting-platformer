using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    int totalNumOfLevels = 10;

    int numOfLevelsUnlocked = 1;

    int currentSceneIndex;
    int levelNumToLoad;
    int currentLevelNum;

    LevelSelectButton levelSelectButton;

    private void Awake()
    {
        Time.timeScale = 1;
        LoadLevels();
    }

    private void Start()
    {
        if (GetComponent<LevelSelectButton>())
        { 
            levelNumToLoad = int.Parse(GetComponentInChildren<TMP_Text>().text); 
        }

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Contains("Level ") && !(sceneName == "Level Select"))
        {
            //Debug.Log(sceneName);
            currentLevelNum = int.Parse(sceneName.Replace("Level ", ""));
        }
        else if (sceneName == "Splash")
        {
            StartCoroutine(LoadMainMenuFromSplash());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Level Select");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SaveSystem.ResetLevels(this);
            SaveSystem.LoadLevels();
            SceneManager.LoadScene("Level Select");
        }
    }

    void SaveLevels()
    {
        SaveSystem.SaveLevels(this);
    }
    void LoadLevels()
    {
        var data = SaveSystem.LoadLevels();

        numOfLevelsUnlocked = data.totalNumLevelsUnlocked;
    }

    IEnumerator LoadMainMenuFromSplash()
    {
        yield return new WaitForSeconds(3);
        LoadNextScene();
    }

    public int GetNumLevelsUnlocked() { return numOfLevelsUnlocked; }

    public void TryToAddToUnlockedLevels() 
    { 
        if (numOfLevelsUnlocked >= totalNumOfLevels) { return; }
        if (currentLevelNum < numOfLevelsUnlocked) { return; }

        numOfLevelsUnlocked++;
        SaveLevels();
        //Debug.Log("You have unlocked Level " + numOfLevelsUnlocked);
    }

    public int GetCurrentLevelNum()
    {
        return currentLevelNum;
    }

    public void LoadLevelByNumber()
    {
        SceneManager.LoadScene("Level " + levelNumToLoad);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("Level Select");
    }
}
