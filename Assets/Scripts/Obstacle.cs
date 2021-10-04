using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[SelectionBase]
public class Obstacle : MonoBehaviour
{
    public static LinkedList<Obstacle> obstacles = new LinkedList<Obstacle>();
    public bool isGrabbable = false,allowsZoomingIn=false,allowsZoomingOut=false;
    // Start is called before the first frame update
    void Awake(){
        if (obstacles.First == null){
            obstacles = new LinkedList<Obstacle>();
        }
        obstacles.AddLast(this);
    }
    void Start()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Player"){
            FinishLine finishLine = GetComponent<FinishLine>();
            if (finishLine!=null)
            {
                finishLine.OnFinish();
            }else{
            col.gameObject.GetComponent<Character>().Die();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
