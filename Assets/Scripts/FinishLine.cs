using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : PartOfObstacle
{
    public void OnFinish()
    {
        LevelManager.wasLastLevelWon = true;
        GameMaster.singleton.StartLevel("LevelSelector");
    }
}
