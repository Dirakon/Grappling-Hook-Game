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
    public void MakeFollowX(Character x){
        if (whoToFollow == null){
        whoToFollow=x;
        transform.position = whoToFollow.transform.position+offset;
        }
    }
    // Start is called before the first frame update
    void Awake(){
        standartSize = desiredSize;
        float aspectRatio = Screen.width/(float)Screen.height;
        Camera.main.orthographicSize = standartSize/aspectRatio;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (whoToFollow == null)
            return;
        Vector3 desiredPosition = (whoToFollow.objectToRotateAround == null?
        whoToFollow.transform : whoToFollow.objectToRotateAround.transform ).position +offset; 
        float aspectRatio = Screen.width/(float)Screen.height;
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
