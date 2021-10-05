using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfObstacle : MonoBehaviour
{
    [SerializeField] protected Obstacle father;
    // Start is called before the first frame update
    void Awake(){
        if (father == null)
            father = GetComponent<Obstacle>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
