using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Character>().Die();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Rigidbody2D rigidbody2d;
    private float speed;
    private Vector3 direction;
    public void Initialize(Vector3 directionNormalized, float speed){
        this.speed = speed;
        direction=directionNormalized;
    }
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rigidbody2d.velocity = direction * speed;
    }
}
