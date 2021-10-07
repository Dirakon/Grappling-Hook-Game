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
    void initCharacter(Character character){

        if (whoToFollow == null){
        whoToFollow=character;
        transform.position = whoToFollow.transform.position+offset;
        }
    }

    // Start is called before the first frame update
    void Awake(){
        standartSize = desiredSize;
        float aspectRatio = Screen.width/(float)Screen.height;
        if (aspectRatio > 1)
            aspectRatio = 1/aspectRatio;
        Camera.main.orthographicSize = standartSize/aspectRatio;
    }
    void Start()
    {
        GameMaster.singleton.onCharacterChosen+=initCharacter;
    }

    // Update is called once per frame
    void Update()
    {
        if (whoToFollow == null)
            return;
        Vector3 desiredPosition = (whoToFollow.objectToRotateAround == null?
        whoToFollow.transform : whoToFollow.objectToRotateAround.transform ).position +offset; 
        float aspectRatio = Screen.width/(float)Screen.height;
        if (aspectRatio > 1)
            aspectRatio = 1/aspectRatio;
        desiredSize  = (whoToFollow.objectToRotateAround == null? standartSize :Mathf.Max(standartSize, whoToFollow.currentRotatingDistance*2));
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
