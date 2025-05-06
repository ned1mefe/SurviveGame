using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class PoolManager : MonoBehaviour
    {
        [Serializable] private class Pool
        {
            public GameObject prefab;
            public int initialSize;
        }
        [SerializeField] private List<Pool> poolConfigs;
        
        private readonly Dictionary<string, Queue<GameObject>> _poolsDictionary = new();
        
        public static PoolManager Instance;
        
        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            InstantiatePools();
        }

        private void InstantiatePools()
        {
            foreach (var pool in poolConfigs)
            {
                if (!pool.prefab.TryGetComponent<IPoolable>(out var poolable))
                {
                    Debug.LogWarning($"{pool.prefab.name} is not poolable.");
                    continue;
                }
                var key = poolable.GetPoolTag();
                
                if (_poolsDictionary.ContainsKey(key))
                {
                    Debug.LogWarning($"Pool with key {key} already exists.");
                    continue;
                }

                var objectQueue = new Queue<GameObject>();

                for (int i = 0; i < pool.initialSize; i++)
                {
                    GameObject obj = Instantiate(pool.prefab, transform);
                    obj.SetActive(false);
                    objectQueue.Enqueue(obj);
                }

                _poolsDictionary.Add(key, objectQueue);
            }
        }
        
        public GameObject GetFromPool(string key, Vector3 position, Quaternion rotation)
        {
            if (!_poolsDictionary.ContainsKey(key))
            {
                Debug.LogError($"No pool found with key {key}");
                return null;
            }

            GameObject obj;

            if (_poolsDictionary[key].Count > 0)
            {
                obj = _poolsDictionary[key].Dequeue();
            }
            else
            {
                var prefab = poolConfigs.Find(p => p.prefab.name == key)?.prefab;
                if (prefab == null)
                {
                    Debug.LogError($"No prefab found for key {key}");
                    return null;
                }

                obj = Instantiate(prefab, transform);
            }

            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);

            if (obj.TryGetComponent<IPoolable>(out var poolable))
            {
                poolable.OnGetFromPool();
            }
            else
            {
                Debug.LogWarning($"{key} is not a poolable object.");
            }
            
            return obj;
        }

        public void ReturnToPool(GameObject gameObj)
        {
            if (!gameObj.TryGetComponent<IPoolable>(out var poolable))
            {
                Debug.LogWarning("Returned object does not implement IPoolable. Destroying.");
                Destroy(gameObj);
                return;
            }

            string key = poolable.GetPoolTag();

            if (!_poolsDictionary.ContainsKey(key))
            {
                Debug.LogWarning($"Trying to return object to unknown pool: {key}");
                Destroy(gameObj);
                return;
            }

            poolable.OnReturnToPool();
            gameObj.SetActive(false);
            _poolsDictionary[key].Enqueue(gameObj);
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}
