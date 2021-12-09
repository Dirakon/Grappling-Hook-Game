using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfObstacle : MonoBehaviour
{
    [SerializeField] protected Obstacle father;

    void Awake()
    {
        if (father == null)
            father = GetComponent<Obstacle>();
    }
}
