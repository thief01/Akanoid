using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Patterns
{
    public class Pool<T> where T : MonoBehaviour
    {
        private readonly Vector3 SAFE_POINT = new Vector3(-25, 0, 0);
        public static Pool<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Pool<T>();
                }

                return instance;
            }
        }

        private static Pool<T> instance;
        
        public int CurrentPoolObjects => pooledObjects.Count;
        public T[] PooledObjects => pooledObjects.ToArray();

        // public UnityEvent OnObjectDestroyed = new UnityEvent();
        public UnityEvent OnObjectSpawned = new UnityEvent();

        private List<T> pooledObjects = new List<T>();
        private GameObject prefab;
        private Transform poolParrent;
        
        public void InitPool(int items, GameObject gameObject)
        {
            ClearPool();
            if (poolParrent == null)
            {
                poolParrent = new GameObject().transform;
                poolParrent.name = typeof(Pool<T>).ToString();
            }
            prefab = gameObject;
            for (int i = 0; i < items; i++)
            {
                GameObject g = GameObject.Instantiate(gameObject, poolParrent);
                pooledObjects.Add(g.GetComponent<T>());
                g.SetActive(false);
            }
        }

        public T GetObject()
        {
            if (pooledObjects.Count == 0)
            {
                Debug.LogWarning("No objects in pool");
                return default(T);
            }

            OnObjectSpawned.Invoke();
            T t = pooledObjects[0];
            t.transform.position = SAFE_POINT;
            t.gameObject.SetActive(true);
            pooledObjects.RemoveAt(0);
            return t;
        }

        public void BackToPool(T t)
        {
            t.gameObject.SetActive(false);
            pooledObjects.Add(t);
            // OnObjectDestroyed.Invoke();
        }

        public void ClearPool()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (pooledObjects[i] != null)
                {
                    GameObject.Destroy(pooledObjects[i].gameObject);
                }
            }
            pooledObjects.Clear();
        }
    }
}