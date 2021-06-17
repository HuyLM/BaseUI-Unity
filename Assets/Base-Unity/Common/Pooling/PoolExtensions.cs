using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AtoLib.Common
{
    public static class PoolExtensions
    {
        /// <summary>
        /// To check if gameobject has been registered with pool, no need to register again.
        /// </summary>
        public static bool IsRegistered(this GameObject prefab)
        {
            return SingletonFree<PoolManager>.Instance.IsRegistered(prefab.gameObject);
        }
        /// <summary>
        /// To check if gameobject can be spawn from pool, no need to instantiate new gameobject.
        /// </summary>
        public static bool HasPooled(this GameObject obj)
        {
            return SingletonFree<PoolManager>.Instance.HasPooled(obj);
        }
        /// <summary>
        /// To check if gameobject has been spawned (currently active in scene)
        /// </summary>
        public static bool HasSpawned(this GameObject obj)
        {
            return SingletonFree<PoolManager>.Instance.HasSpawned(obj);
        }
        /// <summary>
        /// Call this to register gameobject with Pool, so the game object will be add to pooled when recycle, else it will be destroy.
        /// <para>If your scrip inhenrit from interface "IPoolable", you no longer need to call this method.</para>
        /// </summary>
        public static void RegisterPool<T>(this T prefab) where T : Component
        {
            SingletonFree<PoolManager>.Instance.RegisterPool(prefab.gameObject, 0);
        }
        /// <summary>
        /// Call this to register gameobject with Pool, so the game object will be add to pooled when recycle, else it will be destroy.
        /// <para>If your scrip inhenrit from interface "IPoolable", you no longer need to call this method.</para>
        /// </summary>
        public static void RegisterPool<T>(this T prefab, int initialPoolSize) where T : Component
        {
            SingletonFree<PoolManager>.Instance.RegisterPool(prefab.gameObject, initialPoolSize);
        }
        /// <summary>
        /// Call this to register gameobject with Pool, so the game object will be add to pooled when recycle, else it will be destroy.
        /// <para>If your scrip inhenrit from interface "IPoolable", you no longer need to call this method.</para>
        /// </summary>
        public static void RegisterPool(this GameObject prefab)
        {
            SingletonFree<PoolManager>.Instance.RegisterPool(prefab, 0);
        }
        /// <summary>
        /// Call this to register gameobject with Pool, so the game object will be add to pooled when recycle, else it will be destroy.
        /// <para>If your scrip inhenrit from interface "IPoolable", you no longer need to call this method.</para>
        /// </summary>
        public static void RegisterPool(this GameObject prefab, int initialPoolSize)
        {
            SingletonFree<PoolManager>.Instance.RegisterPool(prefab, initialPoolSize);
        }
        /// <summary>
        /// Destroy gameobject which is register with pool (include pooled and spawned)
        /// </summary>
        public static void UnRegisterPool(this GameObject prefab)
        {
            SingletonFree<PoolManager>.Instance.UnRegisterPool(prefab);
        }
        /// <summary>
        /// Destroy gameobject which is register with pool (include pooled and spawned)
        /// </summary>
        public static void UnRegisterPool<T>(this T prefab) where T : Component
        {
            SingletonFree<PoolManager>.Instance.UnRegisterPool(prefab.gameObject);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="scale">defaul value = Vector3.one</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Transform parent, Vector3 position, Vector3 scale, Quaternion rotation) where T : Component, IPoolable
        {
            return SingletonFree<PoolManager>.Instance.Spawn<T>(prefab, parent, position, scale, rotation);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="scale">defaul value = Vector3.one</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Transform parent, Vector3 position, Vector3 scale) where T : Component, IPoolable
        {
            return prefab.Spawn(parent, position, scale, Quaternion.identity);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="position">defaul value = Vector3.zero</param>
        public static T Spawn<T>(this T prefab, Transform parent, Vector3 position) where T : Component, IPoolable
        {
            return prefab.Spawn(parent, position, Vector3.one, Quaternion.identity);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        public static T Spawn<T>(this T prefab, Transform parent) where T : Component, IPoolable
        {
            return prefab.Spawn(parent, Vector3.zero, Vector3.one, Quaternion.identity);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        public static T Spawn<T>(this T prefab) where T : Component, IPoolable
        {
            return prefab.Spawn(null, Vector3.zero, Vector3.one, Quaternion.identity);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component, IPoolable
        {
            return prefab.Spawn(parent, position, Vector3.one, rotation);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Transform parent, Quaternion rotation) where T : Component, IPoolable
        {
            return prefab.Spawn(parent, Vector3.zero, Vector3.one, rotation);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="scale">defaul value = Vector3.one</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Vector3 position, Vector3 scale, Quaternion rotation) where T : Component, IPoolable
        {
            return prefab.Spawn(null, position, scale, rotation);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="scale">defaul value = Vector3.one</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Vector3 position, Vector3 scale) where T : Component, IPoolable
        {
            return prefab.Spawn(null, position, scale, Quaternion.identity);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Vector3 position, Quaternion rotation) where T : Component, IPoolable
        {
            return prefab.Spawn(null, position, Vector3.one, rotation);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Vector3 position) where T : Component, IPoolable
        {
            return prefab.Spawn(null, position, Vector3.one, Quaternion.identity);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Quaternion rotation) where T : Component, IPoolable
        {
            return prefab.Spawn(null, Vector3.zero, Vector3.one, rotation);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="scale">defaul value = Vector3.one</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Transform parent, Vector3 position, Vector3 scale, Quaternion rotation, bool registerPoolIfNeed = true) where T : Component
        {
            return SingletonFree<PoolManager>.Instance.Spawn<T>(prefab, parent, position, scale, rotation, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="scale">defaul value = Vector3.one</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Transform parent, Vector3 position, Vector3 scale, bool registerPoolIfNeed = true) where T : Component
        {
            return prefab.Spawn(parent, position, scale, Quaternion.identity, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="position">defaul value = Vector3.zero</param>
        public static T Spawn<T>(this T prefab, Transform parent, Vector3 position, bool registerPoolIfNeed = true) where T : Component
        {
            return prefab.Spawn(parent, position, Vector3.one, Quaternion.identity, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        public static T Spawn<T>(this T prefab, Transform parent, bool registerPoolIfNeed = true) where T : Component
        {
            return prefab.Spawn(parent, Vector3.zero, Vector3.one, Quaternion.identity, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        public static T Spawn<T>(this T prefab, bool registerPoolIfNeed = true) where T : Component
        {
            return prefab.Spawn(null, Vector3.zero, Vector3.one, Quaternion.identity, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Transform parent, Vector3 position, Quaternion rotation, bool registerPoolIfNeed = true) where T : Component
        {
            return prefab.Spawn(parent, position, Vector3.one, rotation, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        public static T Spawn<T>(this T prefab, Transform parent, Quaternion rotation, bool registerPoolIfNeed = true) where T : Component
        {
            return prefab.Spawn(parent, Vector3.zero, Vector3.one, rotation, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="scale">defaul value = Vector3.one</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Vector3 position, Vector3 scale, Quaternion rotation, bool registerPoolIfNeed = true) where T : Component
        {
            return prefab.Spawn(null, position, scale, rotation, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="scale">defaul value = Vector3.one</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Vector3 position, Vector3 scale, bool registerPoolIfNeed = true) where T : Component
        {
            return prefab.Spawn(null, position, scale, Quaternion.identity, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Vector3 position, Quaternion rotation, bool registerPoolIfNeed = true) where T : Component
        {
            return prefab.Spawn(null, position, Vector3.one, rotation, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Vector3 position, bool registerPoolIfNeed = true) where T : Component
        {
            return prefab.Spawn(null, position, Vector3.one, Quaternion.identity, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static T Spawn<T>(this T prefab, Quaternion rotation, bool registerPoolIfNeed = true) where T : Component
        {
            return prefab.Spawn(null, Vector3.zero, Vector3.one, rotation, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="scale">defaul value = Vector3.one</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Transform parent, Vector3 position, Vector3 scale, Quaternion rotation, bool registerPoolIfNeed = true)
        {
            return SingletonFree<PoolManager>.Instance.Spawn(prefab, parent, position, scale, rotation, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="scale">defaul value = Vector3.one</param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Transform parent, Vector3 position, Vector3 scale, bool registerPoolIfNeed = true)
        {
            return prefab.Spawn(parent, position, scale, Quaternion.identity, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="position">defaul value = Vector3.zero</param>
        public static GameObject Spawn(this GameObject prefab, Transform parent, Vector3 position, bool registerPoolIfNeed = true)
        {
            return prefab.Spawn(parent, position, Vector3.one, Quaternion.identity, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        public static GameObject Spawn(this GameObject prefab, Transform parent, bool registerPoolIfNeed = true)
        {
            return prefab.Spawn(parent, Vector3.zero, Vector3.one, Quaternion.identity, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        public static GameObject Spawn(this GameObject prefab, bool registerPoolIfNeed = true)
        {
            return prefab.Spawn(null, Vector3.zero, Vector3.one, Quaternion.identity, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Transform parent, Vector3 position, Quaternion rotation, bool registerPoolIfNeed = true)
        {
            return prefab.Spawn(parent, position, Vector3.one, rotation, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="parent">default value = null</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        public static GameObject Spawn(this GameObject prefab, Transform parent, Quaternion rotation, bool registerPoolIfNeed = true)
        {
            return prefab.Spawn(parent, Vector3.zero, Vector3.one, rotation, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="scale">defaul value = Vector3.one</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Vector3 position, Vector3 scale, Quaternion rotation, bool registerPoolIfNeed = true)
        {
            return prefab.Spawn(null, position, scale, rotation, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="scale">defaul value = Vector3.one</param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Vector3 position, Vector3 scale, bool registerPoolIfNeed = true)
        {
            return prefab.Spawn(null, position, scale, Quaternion.identity, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Vector3 position, Quaternion rotation, bool registerPoolIfNeed = true)
        {
            return prefab.Spawn(null, position, Vector3.one, rotation, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="position">defaul value = Vector3.zero</param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Vector3 position, bool registerPoolIfNeed = true)
        {
            return prefab.Spawn(null, position, Vector3.one, Quaternion.identity, registerPoolIfNeed);
        }
        /// <summary>
        /// Instantiate (clone) a game object from "prefab" object.
        /// <para>Source object is "IPoolable"</para>
        /// <para>If "prefab" is inherit from interface "IPoolable", or was register by "CreatePool" before, output gameObject will be add into pooled list and can be reuse when recycle.</para>
        /// </summary>
        /// <param name="rotation">default value = Quaternion.identity</param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Quaternion rotation, bool registerPoolIfNeed = true)
        {
            return prefab.Spawn(null, Vector3.zero, Vector3.one, rotation, registerPoolIfNeed);
        }
        /// <summary>
        /// Remove game object from the scene (Disable or Destroy)
        ///             <para>If game object is "IPoolable" (or was register by "RegisterPool"), it will be set to DISABLE, else it will be DESTROY </para>
        /// </summary>
        public static void Recycle<T>(this T obj) where T : Component
        {
            SingletonFree<PoolManager>.Instance.Recycle<T>(obj);
        }
        /// <summary>
        /// Remove game object from the scene (Disable or Destroy)
        ///             <para>If game object is "IPoolable" (or was register by "RegisterPool"), it will be set to DISABLE, else it will be DESTROY </para>
        /// </summary>
        public static void Recycle(this GameObject obj)
        {
            SingletonFree<PoolManager>.Instance.Recycle(obj);
        }
        /// <summary>
        /// Remove game object from the scene (Disable or Destroy)
        ///             <para>If game object is "IPoolable" (or was register by "RegisterPool"), it will be set to DISABLE, else it will be DESTROY </para>
        /// </summary>
        public static void RecycleAll<T>(this T prefab) where T : Component
        {
            SingletonFree<PoolManager>.Instance.RecycleAll<T>(prefab);
        }
        /// <summary>
        /// Remove game object from the scene (Disable or Destroy)
        ///             <para>If game object is "IPoolable" (or was register by "RegisterPool"), it will be set to DISABLE, else it will be DESTROY </para>
        /// </summary>
        public static void RecycleAll(this GameObject prefab)
        {
            SingletonFree<PoolManager>.Instance.RecycleAll(prefab);
        }
    }
}
