using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public const string LAST_LEVEL_PLAYED = "lastPlayedLevel";
    public static bool wasLastLevelWon = false;
    [SerializeField] private LevelNode[] levelNodesInOrder;
    void Start()
    {
        int lastPlayedLevel = PlayerPrefs.GetInt(LAST_LEVEL_PLAYED, 0);
        if (wasLastLevelWon)
        {
            PlayerPrefs.SetInt(levelNodesInOrder[lastPlayedLevel].level, 1);
            PlayerPrefs.Save();
            wasLastLevelWon = false;
        }
        for (int index = 0; index < levelNodesInOrder.Length; ++index)
        {
            LevelNode levelNode = levelNodesInOrder[index];
            levelNode.index = index;
            levelNode.InitializeStatus(PlayerPrefs.HasKey(levelNode.level));
            levelNode.onLevelBeingJumped += OnLevelBeingJumped;
        }
        if (lastPlayedLevel == 0 && !PlayerPrefs.HasKey(levelNodesInOrder[lastPlayedLevel].level))
        {
            levelNodesInOrder[lastPlayedLevel].JumpToLevel();
            return;
        }
        GameMaster.singleton.SpawnHero(
            levelNodesInOrder[lastPlayedLevel].spawnPoint
        );
    }
    void OnLevelBeingJumped(LevelNode level)
    {
        PlayerPrefs.SetInt(LAST_LEVEL_PLAYED, level.index);
        PlayerPrefs.Save();
    }
}
