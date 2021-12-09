using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMer : MonoBehaviour
{
    public static BGMer singleton;
    void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
        }
    }
}
