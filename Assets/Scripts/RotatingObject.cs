using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : PartOfObstacle
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool startsInstantly = false;
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (father != null)
        {
            father.OnCollisionEnter2D(col);
        }
    }

    void Update()
    {
        if (!startsInstantly && !GameMaster.singleton.firstInputEntered)
            return;
        if (father == null)
        {
            transform.RotateAround(transform.position, Vector3.back, Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.RotateAround(father.transform.position, father.transform.forward, Time.deltaTime * rotationSpeed);
        }
    }
}
