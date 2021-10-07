using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] float waitTimeUntilNextLevelLoad = 1f;
    //[SerializeField] AudioClip winSound;

    LevelManager levelManager;
    Canvas levelCompleteCanvas;
    AudioSource audioSource;

    int currentLevelNum;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        if (!levelManager) { Debug.LogError("No LevelLoader found!"); }
        audioSource = GetComponent<AudioSource>();
        currentLevelNum = levelManager.GetCurrentLevelNum();

        var canvasInScene = FindObjectsOfType<Canvas>();
        foreach (var canvas in canvasInScene)
        {
            if (canvas.tag == "LevelComplete")
            {
                levelCompleteCanvas = canvas;
                break;
            }
        }
        if (!levelCompleteCanvas) { Debug.LogError("No Level Complete Canvas found!"); }
        else { levelCompleteCanvas.enabled = false; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Destroy(GetComponent<Collider2D>());
        audioSource.Play();
        yield return new WaitForSeconds(waitTimeUntilNextLevelLoad);

        levelManager.TryToAddToUnlockedLevels();

        Time.timeScale = 0;
        levelCompleteCanvas.enabled = true;
    }
}
