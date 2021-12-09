using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] AudioSource bulletSound;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawnPositionAndDirection;
    [SerializeField] float bulletSpeed, reloadTime;
    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
    }
    public void ShootAt(Vector3 position)
    {
        bulletSound.Play();
        Bullet bullet = Instantiate(this.bullet, bulletSpawnPositionAndDirection.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.Initialize(
            (position - bulletSpawnPositionAndDirection.position).normalized,
            bulletSpeed
        );
    }
}
