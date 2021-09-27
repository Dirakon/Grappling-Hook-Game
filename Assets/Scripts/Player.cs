using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Character whoToControl;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0)){
                whoToControl.ThrowHook(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }


        #else
        if (Input.touchCount == 1 ){
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began){
                Debug.Log("touched");
                whoToControl.ThrowHook(Camera.main.ScreenToWorldPoint(touch.position));
            }

        }
        #endif
    }
}
