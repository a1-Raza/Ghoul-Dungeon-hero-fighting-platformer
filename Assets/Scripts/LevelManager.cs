using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    int totalNumOfLevels = 4;

    int numOfLevelsUnlocked = 1;

    int currentSceneIndex;
    int levelNumToLoad;
    int currentLevelNum;

    [SerializeField] Canvas pauseScreen;

    //LevelSelectButton levelSelectButton;

    private void Awake()
    {
        Time.timeScale = 1;
        LoadLevels();
    }

    private void Start()
    {
        if (pauseScreen)
        {
            pauseScreen.enabled = false;
        }

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
        bool isPlayingLevel = SceneManager.GetActiveScene().name.Contains("Level ") && SceneManager.GetActiveScene().name != "Level Select";

        if (isPlayingLevel && Input.GetAxisRaw("Pause") != 0)
        {
            if (!pauseScreen.enabled)
            {
                PauseGame();
            }
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

    public void LoadOptionsMenu()
    {
        SceneManager.LoadScene("Options");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetData()
    {
        SaveSystem.ResetLevels(this);
        SaveSystem.LoadLevels();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.enabled = true;
    }

    public void ResumeGame()
    {
        pauseScreen.enabled = false;
        Time.timeScale = 1;
    }
}
