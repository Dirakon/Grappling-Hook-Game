using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameMaster singleton;
    void Awake()
    {
        singleton = this;
    }
    public void LevelRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
