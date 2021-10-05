using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : PartOfObstacle
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnFinish(){
        LevelManager.wasLastLevelWon=true;
        GameMaster.singleton.StartLevel("LevelSelector");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
