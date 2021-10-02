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
        obstacles.AddLast(this);
    }
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Player"){
            col.gameObject.GetComponent<Character>().Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
