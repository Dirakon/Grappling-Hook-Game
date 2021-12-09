using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMer : MonoBehaviour
{
    public static BGMer singleton;
    void Awake(){
        if (singleton != null){
            Destroy(gameObject);
        }else{
            DontDestroyOnLoad(gameObject);
            singleton = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
