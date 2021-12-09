using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameMaster : MonoBehaviour
{
    public static GameMaster singleton;
    public bool firstInputEntered = false;
    bool deathCalled = false;
    public Action onSlowDeathCalled;
    public InputSystem inputSystem;
    [SerializeField]
    private SpawnPoint spawnPoint;
    public Action<Character> onCharacterChosen;
    [SerializeField]
    private GameObject heroPrefab;
    void Awake()
    {
        singleton = this;
    }
    public Obstacle FindTheClosestGrabbableObstacleToThePoint(Vector2 point, float maxDistance = float.MaxValue, float minDistance = -1f)
    {
        Obstacle obstacle = null;
        float theValue = float.MaxValue;
        foreach (Obstacle itObstacle in Obstacle.obstacles)
        {
            float value = ((Vector2)itObstacle.transform.position - point).magnitude;
            if (value < theValue && value < maxDistance && value > minDistance)
            {
                theValue = value;
                obstacle = itObstacle;
            }
        }
        return obstacle;
    }
    IEnumerator delayedRestart(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartLevel(SceneManager.GetActiveScene().name);
    }
    public void StartLevel(string level)
    {
        Obstacle.obstacles = new LinkedList<Obstacle>();
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
    public void LevelRestart(float seconds = 0)
    {
        if (deathCalled)
            return;
        deathCalled = true;
        if (onSlowDeathCalled != null)
            onSlowDeathCalled.Invoke();
        StartCoroutine(delayedRestart(seconds));
    }
    public void SpawnHero(SpawnPoint spawnPoint)
    {
        spawnPoint.StartCoroutine(spawnPoint.Spawn(heroPrefab));
    }
    void Start()
    {
        if (spawnPoint != null)
            spawnPoint.StartCoroutine(spawnPoint.Spawn(heroPrefab));
    }
}
