using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int totalNumLevelsUnlocked;


    public LevelData(LevelManager levelManager)
    {
        totalNumLevelsUnlocked = levelManager.GetNumLevelsUnlocked();
    }
    
}
