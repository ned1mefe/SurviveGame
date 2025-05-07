using System.Collections;
using Pooling;
using Units.Enemies;
using UnityEngine;

namespace Units
{
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

                spawnInterval *= 0.99f;

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
            
                if (!enemyPrefab.TryGetComponent<IPoolable>(out var poolable))
                {
                    Debug.LogWarning("Selected prefab is not poolable");
                    continue;
                }
            
                //Debug.Log($"{enemyPrefab.name} spawned at {(int)spawnPoint.x}, {(int)spawnPoint.y}");

                GameObject enemyObj = PoolManager.Instance.GetFromPool(poolable.GetPoolTag(), spawnPoint, Quaternion.identity);

                if (enemyObj.TryGetComponent<Enemy>(out var enemyComponent) && _swarmManager is not null)
                {
                    _swarmManager.RegisterEnemy(enemyComponent);
                }
                else
                {
                    Debug.LogWarning("Spawned object doesn't have an Enemy component");
                }
            }
        }
    }
}