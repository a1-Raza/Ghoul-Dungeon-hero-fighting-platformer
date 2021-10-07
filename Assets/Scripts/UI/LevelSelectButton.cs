using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectButton : MonoBehaviour
{
    int levelButtonNum; //serialized for debugging

    LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        levelButtonNum = int.Parse(GetComponentInChildren<TMP_Text>().text);
        //Debug.Log(levelManager.GetNumLevelsUnlocked());
        if (levelButtonNum > levelManager.GetNumLevelsUnlocked())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
