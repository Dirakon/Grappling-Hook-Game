using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == "Player"){
            col.gameObject.GetComponent<Character>().Die();
        }else{
            Destroy(gameObject);
        }
    }

private Rigidbody2D rigidbody2d;
public float speed;
public Vector3 direction;
void Awake(){
    rigidbody2d=GetComponent<Rigidbody2D>();
}
    // Update is called once per frame
    void Update()
    {
        rigidbody2d.velocity =direction*speed;
    }
}
