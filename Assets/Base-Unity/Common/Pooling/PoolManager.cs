using UnityEngine;
using System.Collections.Generic;
namespace Ftech.Lib.Common
{
    public sealed class PoolManager : SingletonFreeAlive<PoolManager>
    {
        private Dictionary<GameObject, List<GameObject>> pooledObjects = new Dictionary<GameObject, List<GameObject>>();
        private Dictionary<GameObject, GameObject> spawnedObjects = new Dictionary<GameObject, GameObject>();
        /// <summary>
        /// Call this to register gameobject with Pool, so the game object will be add to pooled when recycle, else it will be destroy.
        /// <para>If your scrip inhenrit from interface "IPoolable", you no longer need to call this method.</para>
        /// </summary>
        public void RegisterPool(GameObject prefab, int initialPoolSize)
        {
            if (prefab == null)
            {
                return;
            }
            List<GameObject> list = null;
            if (pooledObjects.ContainsKey(prefab))
            {
                pooledObjects.TryGetValue(prefab, out list);
            }
            if (list == null)
            {
                list = new List<GameObject>();
               pooledObjects.Add(prefab, list);
            }
            if (initialPoolSize <= list.Count)
            {
                return;
            }
            while (list.Count < initialPoolSize)
            {
                GameObject gameObject = Object.Instantiate<GameObject>(prefab, transform);
                gameObject.SetActive(false);
                list.Add(gameObject);
            }
        }
        public T Spawn<T>(T prefab, Transform parent, Vector3 position, Vector3 scale, Quaternion rotation) where T : Component, IPoolable
        {
            return this.Spawn<T>(prefab, parent, position, scale, rotation, true);
        }
        public T Spawn<T>(T prefab, Transform parent, Vector3 position, Vector3 scale, Quaternion rotation, bool createPoolIfNeed) where T : Component
        {
            if (prefab == null || prefab.gameObject == null)
            {
                Debug.LogError("[PoolManager] Cannot spawn a null object. Return value=null");
                return default(T);
            }
            return this.Spawn(prefab.gameObject, parent, position, scale, rotation, createPoolIfNeed).GetComponent<T>();
        }
        public GameObject Spawn(GameObject prefab, Transform parent, Vector3 position, Vector3 scale, Quaternion rotation, bool createPoolIfNeed)
        {
            if (prefab == null)
            {
                Debug.LogError("[PoolManager] Cannot spawn a null prefab!");
                return null;
            }
            if (!createPoolIfNeed)
            {
                createPoolIfNeed = (prefab.GetComponent<IPoolable>() != null);
            }
            GameObject gameObject = null;
            List<GameObject> list;
            if (!pooledObjects.TryGetValue(prefab, out list))
            {
                return GetObject(gameObject, prefab, parent, position, scale, rotation, createPoolIfNeed, createPoolIfNeed);
            }
            if (list.Count > 0)
            {
                while (gameObject == null && list.Count > 0)
                {
                    gameObject = list[0];
                    list.RemoveAt(0);
                }
                if (gameObject != null)
                {
                    return GetObject(gameObject, prefab, parent, position, scale, rotation, true, false);
                }
            }
            return GetObject(gameObject, prefab, parent, position, scale, rotation, true, false);
        }
        private GameObject GetObject(GameObject obj, GameObject prefab, Transform parent, Vector3 position, Vector3 scale, Quaternion rotation, bool addToSpawns = true, bool createPool = false)
        {
            if (createPool)
            {
                this.RegisterPool(prefab, 0);
            }
            if (obj == null)
            {
                obj = Object.Instantiate<GameObject>(prefab, position, rotation, parent);
            }
            obj.transform.SetParent(parent);
            obj.transform.position = (position);
            obj.transform.localScale = (scale);
            obj.transform.localRotation = (rotation);
            obj.SetActive(true);
            if (addToSpawns && !spawnedObjects.ContainsKey(obj))
            {
                spawnedObjects.Add(obj, prefab);
            }
            IPoolable component = obj.GetComponent<IPoolable>();
            if (component != null)
            {
                component.OnSpawnCallback();
            }
            return obj;
        }
        public void Recycle<T>(T obj) where T : Component
        {
            this.Recycle(obj.gameObject);
        }
        public void Recycle(GameObject obj)
        {
            GameObject prefab;
            if (spawnedObjects.TryGetValue(obj, out prefab))
            {
                this.Recycle(obj, prefab);
                return;
            }
            Debug.Log("[PoolManager] Destroy '" + obj.name + "' cause of it never register with pooled.");
            Object.Destroy(obj);
        }
        private void Recycle(GameObject obj, GameObject prefab)
        {
            pooledObjects[prefab].Add(obj);
            spawnedObjects.Remove(obj);
            obj.transform.SetParent(transform);
            IPoolable component = obj.GetComponent<IPoolable>();
            if (component != null)
            {
                component.OnRecycleCallback();
            }
            obj.SetActive(false);
        }
        public void RecycleAll<T>(T prefab) where T : Component
        {
            this.RecycleAll(prefab.gameObject);
        }
        public void RecycleAll(GameObject prefab)
        {
            List<GameObject> list = new List<GameObject>();
            foreach (KeyValuePair<GameObject, GameObject> current in spawnedObjects)
            {
                if (current.Value == prefab)
                {
                    list.Add(current.Key);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                this.Recycle(list[i]);
            }
        }
        public void RecycleAll()
        {
            List<GameObject> list = new List<GameObject>();
            list.AddRange(spawnedObjects.Keys);
            for (int i = 0; i < list.Count; i++)
            {
                this.Recycle(list[i]);
            }
        }
        /// <summary>
        /// To check if gameobject has been registered with pool, no need to register again.
        /// </summary>
        public bool IsRegistered(GameObject obj)
        {
            if (obj == null)
            {
                Debug.LogError("[PoolManager] Cannot check 'IsPoolable' with null game object.");
                return false;
            }
            return this.HasPooled(obj) || this.HasSpawned(obj) || obj.GetComponent<IPoolable>() != null;
        }
        /// <summary>
        /// To check if gameobject can be spawn from pool, no need to instantiate new gameobject.
        /// </summary>
        public bool HasPooled(GameObject obj)
        {
            return pooledObjects.ContainsKey(obj);
        }
        /// <summary>
        /// To check if gameobject has been spawned (currently active in scene)
        /// </summary>
        public bool HasSpawned(GameObject obj)
        {
            return spawnedObjects.ContainsKey(obj);
        }
        /// <summary>
        /// Destroy gameobject which is register with pool (include pooled and spawned)
        /// </summary>
        public void UnRegisterPool(GameObject prefab)
        {
            if (!this.IsRegistered(prefab))
            {
                return;
            }
            this.RecycleAll(prefab);
            this.DestroyPooled(prefab);
        }
        /// <summary>
        /// Destroy gameobject which is register with pool (include pooled and spawned)
        /// </summary>
        public void UnRegisterPool<T>(T prefab) where T : Component
        {
            this.UnRegisterPool(prefab.gameObject);
        }
        /// <summary>
        /// Destroy all gameobject which is currently in pooled (currently disable in scene)
        /// <para>No action with spawned (currently active in scene)</para>
        /// </summary>
        public void DestroyPooled(GameObject prefab)
        {
            List<GameObject> list;
            if (pooledObjects.TryGetValue(prefab, out list))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Object.Destroy(list[i]);
                }
                list.Clear();
            }
        }
        /// <summary>
        /// Destroy all gameobject which is currently in pooled (currently disable in scene)
        /// <para>No action with spawned (currently active in scene)</para>
        /// </summary>
        public void DestroyPooled<T>(T prefab) where T : Component
        {
            this.DestroyPooled(prefab.gameObject);
        }
        /// <summary>
        /// Destroy all gameobject which is managing by PoolManager, include all spawned and pooled.
        /// </summary>
        public void DestroyAll()
        {
            try
            {
                this.RecycleAll();
                List<GameObject> list = new List<GameObject>();
                list.AddRange(pooledObjects.Keys);
                for (int i = 0; i < list.Count; i++)
                {
                    this.DestroyPooled(list[i]);
                }
            }
            catch
            {
            }
        }
    }
}