using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level",order =1)]
public class Level : ScriptableObject
{

    // [SerializeField] private int coinNumber;
    // public int CoinNumber => coinNumber;
    [SerializeField] private Vector3 startPosition;
    public Vector3 StartPosition => startPosition;
    [SerializeField] private List<Vector3> enemyPositions;
    public List<Vector3> EnemyPositions => enemyPositions;
    [SerializeField] private GameObject levelPrefab;
    public GameObject LevelPrefab => levelPrefab;

}
