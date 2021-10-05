using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] stage0,stage1, stage2, stage3;
    [SerializeField] float secondsToWait0,secondsToWait, secondsToWait2, speedOfSlowingDown;
    void SetStageActivness(GameObject[] stage,bool activness){
        foreach (var obj in stage)
        {
            obj.SetActive(activness);
        }

    }
    IEnumerator SlowDownAndStopTheGame()
    {
        float t = 1;
        while (t > 0)
        {
            t -= Time.unscaledDeltaTime * speedOfSlowingDown;
            if (t < 0)
                t = 0;
            Time.timeScale = t;
            yield return null;
        }
        GameMaster.singleton.PauseGame();

    }
    IEnumerator WaitForTheFirstFinger(){
        while (!GameMaster.singleton.inputSystem.fingerPresent[0]) { yield return null; }
    }
    IEnumerator TutorialStageZero()
    {
        GameMaster.singleton.inputSystem.ForbidInput();
        yield return new WaitForSeconds(secondsToWait0);
        yield return SlowDownAndStopTheGame();

        GameMaster.singleton.inputSystem.UnforbidInput();
        SetStageActivness(stage0,true);
        yield return WaitForTheFirstFinger();

        SetStageActivness(stage0,false);
        GameMaster.singleton.ResumeGame();
    }
    IEnumerator TutorialStageOne()
    {
        yield return new WaitForSeconds(secondsToWait);
        yield return SlowDownAndStopTheGame();

        SetStageActivness(stage1,true);
        while (Input.touchCount!=0){yield return null;}

        GameMaster.singleton.inputSystem.ForbidInput();
        SetStageActivness(stage1,false);
        GameMaster.singleton.ResumeGame();
    }
    IEnumerator TutorialStageTwo()
    {
        yield return new WaitForSeconds(secondsToWait2);
        yield return SlowDownAndStopTheGame();

        GameMaster.singleton.inputSystem.UnforbidInput();
        SetStageActivness(stage2,true);
        yield return WaitForTheFirstFinger();
        
        SetStageActivness(stage2,false);
        GameMaster.singleton.ResumeGame();
    }
    IEnumerator TutorialStageThree()
    {
        SetStageActivness(stage3,true);
        yield return null;

    }
    IEnumerator AutoTutorial()
    {
        yield return TutorialStageZero();
        yield return TutorialStageOne();
        yield return TutorialStageTwo();
        yield return TutorialStageThree();
    }
    void Awake()
    {
    }
    void HaltAll(){
        GameMaster.singleton.ResumeGame();
        StopAllCoroutines();
    }
    void Start()
    {
        GameMaster.singleton.onSlowDeathCalled+=HaltAll;
        StartCoroutine(AutoTutorial());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
