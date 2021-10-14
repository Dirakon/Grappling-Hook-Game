using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : PartOfObstacle
{

    [SerializeField] private bool startsInstantly = false;
        [SerializeField] private float movingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (!startsInstantly && !GameMaster.singleton.firstInputEntered)
            return;
        transform.position += transform.right*movingSpeed*Time.deltaTime;
    }
}
