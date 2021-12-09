using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Gun gun;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject gameObjectToRotate;
    [SerializeField] float degreesToRotate, speedToRotate, oscilationSpeed, agroZone;
    [SerializeField] private Vector3[] offsets;
    [SerializeField] private int coordinateToRotate;

    void Start()
    {
        GameMaster.singleton.onCharacterChosen += TargetChosen;
    }
    bool isLookingLeft = true;
    Character target;
    void TargetChosen(Character character)
    {
        target = character;
        StartCoroutine(Idle());
    }
    void OnDrawGizmos()
    {

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, agroZone);
    }
    IEnumerator RotateToDifferentSide()
    {
        float sign = isLookingLeft ? -1 : 1;
        float degreesLeft = degreesToRotate;
        while (degreesLeft > 0)
        {
            float degrees = Time.deltaTime * speedToRotate;
            if (degreesLeft - degrees < 0)
            {
                degrees = degreesLeft;
            }
            degreesLeft -= degrees;
            degrees *= sign;
            Vector3 euler = gameObjectToRotate.transform.rotation.eulerAngles;
            euler[coordinateToRotate] += degrees;
            gameObjectToRotate.transform.rotation = Quaternion.Euler(euler);
            yield return null;
        }
        isLookingLeft = !isLookingLeft;
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    IEnumerator Oscillation()
    {
        Vector3 original = transform.position;
        while (true)
        {
            foreach (var offset in offsets)
            {
                while ((transform.position - (offset + original)).magnitude > 0.01f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, (offset + original), Time.deltaTime * oscilationSpeed);
                    yield return null;
                }
            }
        }
    }
    IEnumerator Idle()
    {
        while (!GameMaster.singleton.firstInputEntered) { yield return null; }
        StartCoroutine(Oscillation());
        while (true)
        {
            if ((transform.position - target.transform.position).magnitude > agroZone)
            {
                yield return null;
                continue;
            }
            if (isLookingLeft)
            {
                if (target.transform.position.x > transform.position.x)
                {
                    yield return RotateToDifferentSide();
                    continue;
                }
            }
            else
            {
                if (target.transform.position.x < transform.position.x)
                {
                    yield return RotateToDifferentSide();
                    continue;
                }
            }
            gun.ShootAt(target.transform.position);
            yield return gun.Reload();
            yield return null;
        }
    }
}
