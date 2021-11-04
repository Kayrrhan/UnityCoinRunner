using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels = new List<Level>();
    [SerializeField] private Transform levelParent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Vector3 enemyStartPosition;
    [SerializeField] private CoinCounter coinCounter;
    [SerializeField] private GameObject inGameUIParent;
    [SerializeField] private GameObject nextLevelButton;
    private Level currentLevel;

    private Level GetLevelByName(string levelName){
        Level result = levels.Find(level => level.name == levelName);
        if (result) return result;
        throw new System.ArgumentException("Couldn't find any level named "+levelName);
    }

    public void DestroyLevels(){
        enemy.isChasingPlayer = false;
        int childCount = levelParent.childCount;
        for(int i=0;i<childCount;++i){
            Destroy(levelParent.GetChild(i).gameObject);
        }
    }

    public void SetLevel(Level level){
        DestroyLevels();
        Instantiate(level.LevelPrefab, levelParent);
        playerTransform.position = level.StartPosition;
        if (level.EnemyPositions.Count > 1){
            enemy.transform.position = level.EnemyPositions[0];
            enemy.SetPositions(level.EnemyPositions);
            enemy.SetEndPosition(level.EnemyPositions[1]);
        }else{
            List<Vector3> list = new List<Vector3>();
            list.Add(Vector3.zero); 
            enemy.transform.position = Vector3.zero;
            enemy.SetPositions(list);
            enemy.SetEndPosition(Vector3.zero);
        }
        if (levels.FindIndex(l => l.name == level.name) == levels.Count-1){
            nextLevelButton.SetActive(false);
        }else{
            nextLevelButton.SetActive(true);
        }
        coinCounter.SetCounter(level);
        enemy.isChasingPlayer = true;
        currentLevel = level;
    }

    private void SetRandomLevel(){
        if (levels.Count == 0) return;
        Level level = levels[Random.Range(0,levels.Count)];
        SetLevel(level);
    }

    public void ResetLevel(){
        DestroyLevels();
        inGameUIParent.SetActive(true);
        SetLevel(currentLevel);
    }

    public void LoadNextLevel(){
        int levelIndex = levels.FindIndex(l => l.name == currentLevel.name);
        if (levelIndex != levels.Count-1){
            Level nextLevel = levels[levelIndex+1];
            SetLevel(nextLevel);
        }
    }
}
