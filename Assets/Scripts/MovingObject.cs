using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : PartOfObstacle
{

    [SerializeField] private bool startsInstantly = false;
    [SerializeField] private float movingSpeed;

    void Update()
    {
        if (!startsInstantly && !GameMaster.singleton.firstInputEntered)
            return;
        transform.position += transform.right * movingSpeed * Time.deltaTime;
    }
}
