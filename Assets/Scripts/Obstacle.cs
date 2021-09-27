using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[SelectionBase]
public class Obstacle : MonoBehaviour
{
    public bool isGrabbable = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Player"){
            GameMaster.singleton.LevelRestart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
