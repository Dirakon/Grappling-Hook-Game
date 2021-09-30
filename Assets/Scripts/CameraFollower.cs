using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    [SerializeField]
    private Character whoToFollow;
    [SerializeField]
    private float speed,zoomSpeed;
    [SerializeField]
    private Vector3 offset;
    private float standartSize;
    [SerializeField] private float desiredSize = 5f;
    [SerializeField] private Camera camera;
    // Start is called before the first frame update
    void Awake(){
        standartSize = desiredSize;
        float aspectRatio = Mathf.Min(Screen.width/(float)Screen.height,Screen.height/(float)Screen.width);
        Camera.main.orthographicSize = standartSize/aspectRatio;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = (whoToFollow.objectToRotateAround == null?
        whoToFollow.transform : whoToFollow.objectToRotateAround ).position +offset; 
        float aspectRatio = Mathf.Min(Screen.width/(float)Screen.height,Screen.height/(float)Screen.width);
        desiredSize  = (whoToFollow.objectToRotateAround == null? standartSize :Mathf.Max(standartSize, whoToFollow.currentRotatingDistance*3));
        float currentSize = camera.orthographicSize*aspectRatio;
        float sign = Mathf.Sign(desiredSize-currentSize);
        currentSize += Time.deltaTime*zoomSpeed*sign;
        if (Mathf.Sign(desiredSize-currentSize) != sign){
            // Overshot a bit
            currentSize = desiredSize;
        }
        Camera.main.orthographicSize = currentSize /aspectRatio;
        transform.position = Vector3.MoveTowards(transform.position,desiredPosition,Time.deltaTime*speed);
    }
}
