using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[ExecuteInEditMode]
public class LevelNode : PartOfObstacle
{
    // Start is called before the first frame update
    public string level;
    public int index;
    public SpawnPoint spawnPoint;
    [SerializeField] private GameObject[] thingsToActivate,thingsToDeactivate;
    [SerializeField] private float distanceForActivation;
    public void InitializeStatus(bool status){
        if (status){
            foreach(var go in thingsToActivate){
                go.SetActive(true);
            }
            foreach(var go in thingsToDeactivate){
                go.SetActive(false);
            }
        }
    }
    Character rotatingCharacter=null;
    void OnHookStart(Character character){
rotatingCharacter=character;
    }
    void OnHookEnd(Character character){
        rotatingCharacter=null;
    }
    void Start()
    {
        father.onHookEnd+=OnHookEnd;
        father.onHookStart+=OnHookStart;
    }
    void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceForActivation);
    }
    public Action<LevelNode> onLevelBeingJumped;
    public void JumpToLevel(){
        if (onLevelBeingJumped != null)
            onLevelBeingJumped.Invoke(this);
        GameMaster.singleton.StartLevel(level);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotatingCharacter!= null && rotatingCharacter.currentRotatingDistance <= distanceForActivation){
            JumpToLevel();
        }
    }
}
