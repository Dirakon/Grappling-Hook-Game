using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumRobotScript : MonoBehaviour
{
    [SerializeField]
    float minSpeed = 1f,maxSpeed=7.5f;
    [SerializeField]

    float collisionRechargeTime = 0.5f;    // Start is called before the first frame update
    float speed;
    void Start()
    {
        
    }
    void Awake(){
        speed = Random.Range(minSpeed, maxSpeed);
    }

float lastCollisionTime = 0;

    public void OnCollisionEnter2D(Collision2D col){
    if (lastCollisionTime <= 0 && col.transform.position.y >= transform.position.y){
        lastCollisionTime =collisionRechargeTime;
        transform.RotateAround(transform.position,Vector3.up, 180);
    }
}
    // Update is called once per frame
    void Update()
    {
        lastCollisionTime  -=Time.deltaTime;
        transform.Translate(transform.right*speed*Time.deltaTime,Space.World);
    }
}
