using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AtoLib.Common
{
    public class PoolRegister : MonoBehaviour
    {
        public enum StartupPoolMode
        {
            None,
            Awake,
            Start
        }
        [Serializable]
        public class Object
        {
            public GameObject prefab;
            public int size;
        }
        public PoolRegister.StartupPoolMode startupPoolMode;
        public PoolRegister.Object[] pools;
        private void Awake()
        {
            if (this.startupPoolMode == PoolRegister.StartupPoolMode.Awake)
            {
                this.RegisterPools();
            }
        }
        private void Start()
        {
            if (this.startupPoolMode == PoolRegister.StartupPoolMode.Start)
            {
                this.RegisterPools();
            }
        }
        public void RegisterPools()
        {
            this.RegisterPools(this.pools);
        }
        private void RegisterPools(PoolRegister.Object[] pools)
        {
            if (pools == null || pools.Length == 0)
            {
                return;
            }
            for (int i = 0; i < pools.Length; i++)
            {
                PoolRegister.Object @object = pools[i];
                if (@object != null && @object.prefab != null)
                {
                    @object.prefab.RegisterPool(@object.size);
                }
            }
        }
        public void UnRegisterPools(bool destroyOnFinished = true)
        {
            PoolRegister.Object[] array = this.pools;
            for (int i = 0; i < array.Length; i++)
            {
                PoolRegister.Object @object = array[i];
                if (@object != null && @object.prefab != null)
                {
                    @object.prefab.UnRegisterPool();
                }
            }
            if (destroyOnFinished)
            {
                UnityEngine.Object.Destroy(base.gameObject);
                return;
            }
            base.enabled = (false);
        }
    }
}
