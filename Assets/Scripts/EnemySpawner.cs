using System.Collections;
using Units.Enemies;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public float spawnInterval;
    [SerializeField] private float mapHalfWidth = 24.1f;
    [SerializeField] private float mapHalfHeight = 13.83f;
    
    private SwarmManager _swarmManager;

    private void Start()
    {
        _swarmManager = FindObjectOfType<SwarmManager>();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            #region FindSpawnPoint
            
            Vector3 spawnPoint = new Vector3(mapHalfWidth,mapHalfHeight,0);
            if (Random.value < 0.5) // vertical or horizontal edge
            {
                spawnPoint.x = Random.Range(-mapHalfWidth, mapHalfWidth);
                spawnPoint.y *= Random.value < 0.5 ? -1 : 1;
            }
            else
            {
                spawnPoint.y = Random.Range(-mapHalfHeight, mapHalfHeight);
                spawnPoint.x *= Random.value < 0.5 ? -1 : 1;
            }
            #endregion
            
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            
            if (_swarmManager != null)
                _swarmManager.RegisterEnemy(enemy.GetComponent<Enemy>());
        }
    }
}