using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints; // Drag your spawn point Transforms here
    private float enemyInterval = 3.5f;
    
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) {
            yield return new WaitForSeconds(enemyInterval);
            
            // Choose a random spawn point from the array
            Transform chosenSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            
            // Spawn at the chosen point's position and rotation
            Instantiate(enemyPrefab, chosenSpawnPoint.position, Quaternion.identity);
        }
    }
}