using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Character characterToControl;
    [SerializeField]
    private CameraFollower cameraToControl;
    void Start()
    {

    }
    const int AWAITING_FIRST_TOUCH = 0, TRYING_TO_THROW_HOOK = 1, AWAITING_SECOND_TOUCH = 2, ZOOMING = 3, INVALID_DOUBLE_FINGER = 4;
    int stage = AWAITING_FIRST_TOUCH;
    // Update is called once per frame
    void Update()
    {
        Debug.Log(stage);
        switch (stage)
        {
            case AWAITING_FIRST_TOUCH:
                if (GameMaster.singleton.inputSystem.fingerPresent[0])
                {
                    stage = TRYING_TO_THROW_HOOK;
                }
                break;
            case TRYING_TO_THROW_HOOK:
                if (!GameMaster.singleton.inputSystem.fingerPresent[0])
                {
                    stage = AWAITING_FIRST_TOUCH;
                }
                else if (GameMaster.singleton.inputSystem.fingerPresent[1])
                {
                    stage = INVALID_DOUBLE_FINGER;
                }
                else if (characterToControl.ThrowHook(Camera.main.ScreenToWorldPoint(GameMaster.singleton.inputSystem.mainTouchPosition)))
                {
                    stage = AWAITING_SECOND_TOUCH;
                }
                break;
            case AWAITING_SECOND_TOUCH:
                if (GameMaster.singleton.inputSystem.fingerPresent[1])
                    stage = ZOOMING;
                else if (!GameMaster.singleton.inputSystem.fingerPresent[0])
                {
                    stage = AWAITING_FIRST_TOUCH;
                    characterToControl.RealeaseHook();
                }
                break;
            case ZOOMING:
                if (!GameMaster.singleton.inputSystem.fingerPresent[1])
                {
                    stage = AWAITING_SECOND_TOUCH;
                }
                else if (characterToControl.objectToRotateAround == null){
                    stage = INVALID_DOUBLE_FINGER;
                }
                else
                {
                    characterToControl.ZoomChange(GameMaster.singleton.inputSystem.distanceDelta);
                }

                break;
            case INVALID_DOUBLE_FINGER:
                if (!GameMaster.singleton.inputSystem.fingerPresent[0])
                {
                    stage = AWAITING_FIRST_TOUCH;
                }
                break;
        }
    }
}
