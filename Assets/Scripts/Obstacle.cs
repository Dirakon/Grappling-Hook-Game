using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[SelectionBase]
public class Obstacle : MonoBehaviour
{
    public Material[] materialsForLaser;
    public static LinkedList<Obstacle> obstacles = new LinkedList<Obstacle>();
    public Action<Character> onHookStart, onHookEnd;

    public bool isGrabbable = false, allowsZoomingIn = false, allowsZoomingOut = false;
    void Awake()
    {
        if (obstacles.First == null)
        {
            obstacles = new LinkedList<Obstacle>();
        }
        obstacles.AddLast(this);
    }
    public void OnHookStart(Character character)
    {
        if (onHookStart != null)
            onHookStart.Invoke(character);
    }
    public void OnHookEnd(Character character)
    {
        if (onHookEnd != null)
            onHookEnd.Invoke(character);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            FinishLine finishLine = GetComponent<FinishLine>();
            if (finishLine != null)
            {
                finishLine.OnFinish();
            }
            else
            {
                col.gameObject.GetComponent<Character>().Die();
            }
        }
    }
}
