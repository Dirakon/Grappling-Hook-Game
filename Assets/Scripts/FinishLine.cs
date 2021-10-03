using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnFinish(){
        Debug.Log("YOU WON!");
        GameMaster.singleton.LevelRestart();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
