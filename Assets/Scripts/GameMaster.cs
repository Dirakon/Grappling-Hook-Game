using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameMaster singleton;
    public InputSystem inputSystem;
    [SerializeField]
    private SpawnPoint spawnPoint;
    [SerializeField]
    private GameObject heroPrefab;
    void Awake()
    {
        singleton = this;
    }
    public Obstacle FindTheClosestGrabbableObstacleToThePoint(Vector2 point,float maxDistance = float.MaxValue, float minDistance =-1f){
        Obstacle obstacle=null;
        float theValue = float.MaxValue;
        foreach(Obstacle itObstacle in Obstacle.obstacles){
            float value = ((Vector2)itObstacle.transform.position - point).magnitude;
            if (value < theValue && value < maxDistance && value > minDistance){
                theValue = value;
                obstacle = itObstacle;
            }
        }
        return obstacle;
    }
    public void LevelRestart()
    {
        Obstacle.obstacles = new LinkedList<Obstacle>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    void Start()
    {
        spawnPoint.StartCoroutine(spawnPoint.Spawn(heroPrefab));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
