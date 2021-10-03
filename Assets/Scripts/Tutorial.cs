using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField ]GameObject[] stage1,stage2,stage3;
    [SerializeField] float secondsToWait,secondsToWait2, speedOfSlowingDown;
    IEnumerator AutoTutorial(){
        GameMaster.singleton.inputSystem.ForbidInput();
        yield return new WaitForSeconds(secondsToWait);
        float t = 1;
        while (t > 0){
            t -= Time.unscaledDeltaTime*speedOfSlowingDown;
            if (t < 0)
                t = 0;
            Time.timeScale = t;
            yield return null;
        }
        GameMaster.singleton.PauseGame();
        GameMaster.singleton.inputSystem.UnforbidInput();
        foreach(var obj in stage1){
            obj.SetActive(true);
        }
        while (!GameMaster.singleton.inputSystem.fingerPresent[0]){
            yield return null;
        }
        GameMaster.singleton.inputSystem.ForbidInput();
        GameMaster.singleton.ResumeGame();
        foreach(var obj in stage1){
            obj.SetActive(false);
        }
        yield return new WaitForSeconds(secondsToWait2);
        t = 1;
        while (t > 0){
            t -= Time.unscaledDeltaTime*speedOfSlowingDown;
            if (t < 0)
                t = 0;
            Time.timeScale = t;
            yield return null;
        }
        GameMaster.singleton.inputSystem.UnforbidInput();
        GameMaster.singleton.PauseGame();
        foreach(var obj in stage2){
            obj.SetActive(true);
        }
        while (!GameMaster.singleton.inputSystem.fingerPresent[0]){
            yield return null;
        }
        foreach(var obj in stage2){
            obj.SetActive(false);
        }
        foreach(var obj in stage3){
            obj.SetActive(true);
        }
        GameMaster.singleton.ResumeGame();
    }
    void Awake()
    {
    }
    void Start(){
        StartCoroutine(AutoTutorial());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
