using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Character characterToControl;
    [SerializeField]
    private float maxSizeToChooseAnObstacle;
    void Start()
    {
        if (Input.touchCount != 0 && !GameMaster.singleton.inputSystem.inputForbidden)
            stage = AWAITING_SECOND_TOUCH;
    }
    const int AWAITING_FIRST_TOUCH = 0, TRYING_TO_THROW_HOOK = 1, AWAITING_SECOND_TOUCH = 2, ZOOMING = 3, WAIT_FOR_THE_SUFFERING_TO_END = 4, JUST_SPAWNED=5, TUTORIAL_WAIT_FOR_EMPTY = 6;
    int stage = JUST_SPAWNED;
    // Update is called once per frame
    void Update()
    {
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
                    stage = WAIT_FOR_THE_SUFFERING_TO_END;
                }
                else if (characterToControl.ThrowHook(
                    GameMaster.singleton.FindTheClosestGrabbableObstacleToThePoint(
                        Camera.main.ScreenToWorldPoint(GameMaster.singleton.inputSystem.mainTouchPosition)
                            )
                        )
                    )
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
                }else if (characterToControl.objectToRotateAround == null){
                    stage = WAIT_FOR_THE_SUFFERING_TO_END;
                }
                break;
            case ZOOMING:
                if (!GameMaster.singleton.inputSystem.fingerPresent[1])
                {
                    stage = AWAITING_SECOND_TOUCH;
                }
                else if (characterToControl.objectToRotateAround == null)
                {
                    stage = WAIT_FOR_THE_SUFFERING_TO_END;
                }
                else
                {
                    characterToControl.ZoomChange(GameMaster.singleton.inputSystem.distanceDelta);
                }

                break;
            case WAIT_FOR_THE_SUFFERING_TO_END:
                if (!GameMaster.singleton.inputSystem.fingerPresent[0])
                {
                    stage = AWAITING_FIRST_TOUCH;
                }
                break;
            case JUST_SPAWNED:
                if (characterToControl.objectToRotateAround == null){
                    stage = AWAITING_FIRST_TOUCH;
                }
                else if (GameMaster.singleton.inputSystem.fingerPresent[0]){
                    stage=AWAITING_SECOND_TOUCH;
                }

                break;
            case TUTORIAL_WAIT_FOR_EMPTY:
                Debug.Log(GameMaster.singleton.inputSystem.fingerPresent[0]);
                if (!GameMaster.singleton.inputSystem.fingerPresent[0]){
                    characterToControl.RealeaseHook();
                    stage=AWAITING_FIRST_TOUCH;
                }

                break;
        }
    }
}
