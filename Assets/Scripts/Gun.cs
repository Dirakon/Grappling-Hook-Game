using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnPositionAndDirection;
    [SerializeField] float bulletSpeed,reloadTime;
    public IEnumerator Reload(){
        yield return new WaitForSeconds(reloadTime);
    }
    void Start()
    {
        
    }
    public void ShootAt(Vector3 position){
        Bullet bullet = Instantiate(this.bullet,bulletSpawnPositionAndDirection.position,Quaternion.identity).GetComponent<Bullet>();
        bullet.speed = bulletSpeed;
        bullet.direction = (position-bulletSpawnPositionAndDirection.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
