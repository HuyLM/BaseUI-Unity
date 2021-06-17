using System;
using UnityEngine;

namespace AtoLib
{
    #region Normal class - NONE MonoBehaviour
    /// <summary> 
    /// Singleton class which is not MonoBehavior. 
    /// <para> Call T.Instance.Preload() at the first script startup of application to prepare init. </para>
    /// </summary>
    public class Singleton<T> : IDisposable where T : new()
    {
        /// <summary> This is local variable. Use 'Instance' from outsite. </summary>
        private static T __instance;
        /// <summary> 
        ///  If you want to check null, use this property instead of calling to "Instance" 
        ///  because "Instance" can auto create an emty gameobject, so "Instance" can never null 
        ///  </summary> */
        public static bool Initialized
        {
            get
            {
                return Singleton<T>.__instance != null;
            }
        }
        /// <summary> 
        /// This will auto create new if instance is null.
        /// <para> If you want to check null, use 'if (T.Initialized)' instead of 'if (T.Instance != null)' </para>
        /// </summary>
        public static T Instance
        {
            get
            {
                if (Singleton<T>.__instance != null)
                {
                    return Singleton<T>.__instance;
                }
                Singleton<T>.__instance = Activator.CreateInstance<T>();
                return Singleton<T>.__instance;
            }
        }
        /// <summary>
        /// Constructor will call automaticaly on the first call of "Instance"
        /// <para>If you want to create your custom constructor, note that deliver from this "base" </para>
        /// </summary>
        public Singleton()
        {
            this.Initialize();
            Debug.Log("[Singleton][" + typeof(T).Name + "] Initialized");
        }
        ~Singleton()
        {
            this.Dispose();
        }
        /// <summary> 
        /// This function will be call automaticaly only one times (on the first call of "Instance")
        /// <para>Put your custom initialize here. No need to call "base.Initialize" </para>
        /// </summary>
        protected virtual void Initialize()
        {
        }
        /// <summary> 
        /// This method is empty function, just use to prepare initialize the "Instance" to improve Ram/ CPU (to preload or decompress all asset inside)
        /// <para>Call this at the first application script (exp: BasePreload.cs)</para>
        /// <para>Be carefully if you override this and put your custom initialzation here, 
        /// because this function can be call many times on any where, so the initialization inside will be init many times too</para>
        /// </summary>
        public virtual void Preload()
        {
        }
        /// <summary>
        /// Call this method to manual release (destroy) the instance.
        /// </summary>
        public void Dispose()
        {
            if (Singleton<T>.__instance == null)
            {
                return;
            }
            this.OnDispose();
            Singleton<T>.__instance = default(T);
            GC.SuppressFinalize(this);
            //Debug.Log("[Singleton][" + typeof(T).Name + "] Destroyed.", Logs.Settings.CoreLogEnable, Logs.Color.Green);
        }
        /// <summary>
        /// Override this and set all field which are reference type to null to release RAM because GC will not care for type of this Singleton.
        /// </summary>
        protected virtual void OnDispose()
        {
        }
    }

    #endregion

    #region MonoBehaviour
    /** <summary> Base Singleton class which is MonoBehavior </summary> */
    public abstract class SingletonMono<T> : MonoBehaviour where T : Component
    {
        protected static T instance;

        /**<summary> If you want to check null, use 'if (T.Initialized)' instead of 'if (T.Instance != null)' </summary> */
        public static bool Initialized
        {
            get { return instance != null; }
        }

        protected virtual void Awake()
        {
            if (instance == null)
                instance = this as T;
            else if (this != instance)
            {
                Debug.LogWarningFormat("[MonoSingleton] Class {0} is initialized multiple times", typeof(T).FullName);
                DestroyImmediate(gameObject);
                return;
            }

            OnAwake();
        }

        protected abstract void OnAwake();

        /**<summary> Call T.Instance.Preload() at the first script startup to pre init. </summary>*/
        public virtual void Preload() { }

        protected virtual void OnDestroy()
        {
            instance = null;
        }
    }

    /** <summary> 
     * <para> "Instance" = Find object in scene. </para>
     * <para> Must be added to scene before run </para>
     * </summary> */
    public class SingletonBind<T> : SingletonMono<T> where T : Component
    {
        /**<summary> If you want to check null, use 'if (T.Initialized)' instead of 'if (T.Instance != null)' </summary> */
        public static T Instance
        {
            get
            {
                if (instance != null)
                    return instance;
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    Debug.LogErrorFormat("[Singleton] Class {0} must be added to scene before run!", typeof(T));
                }
                return instance;
            }
        }

        protected override void OnAwake() { }
    }

    /** <summary> 
     * <para> "Instance" = Find object in scene. </para>
     * <para> Must be added to scene before run </para>
     * <para> Instance is DontDestroyOnLoad </para>
     * </summary> */
    public class SingletonBindAlive<T> : SingletonBind<T> where T : Component
    {
        protected override void Awake()
        {
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
    }

    /** <summary> 
     * <para>"Instance" = new GameObject if can not find it on scene. </para>
     * <para> No scene reference variables. </para>
     * </summary> */
    public class SingletonFree<T> : SingletonMono<T> where T : Component
    {
        /**<summary> If you want to check null, use 'if (T.Initialized)' instead of 'if (T.Instance != null)' </summary> */
        public static T Instance
        {
            get
            {
                if (instance != null)
                    return instance;
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    Debug.LogFormat("[Singleton] Class {0} not found! Create empty instance", typeof(T));
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }
                return instance;
            }
        }

        protected override void OnAwake() { }
    }

    /** <summary> 
     * <para> "Instance" = new GameObject if can not find it on scene. </para>
     * <para> No scene reference variables. </para>
     * <para> Instance is DontDestroyOnLoad </para>
     * </summary> */
    public class SingletonFreeAlive<T> : SingletonFree<T> where T : Component
    {
        protected override void Awake()
        {
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }

        public static bool HasInstance => instance != null;
    }

    /** <summary> 
     * <para> "Instance" = Instantiate from Resources folder when be called at runtime.</para>
     * <para> Place your prefab in Resources: "Prefabs/T/T", T is the name of class </para> 
     * </summary> */
    public class SingletonResource<T> : SingletonMono<T> where T : Component
    {
        protected static string ResourcePath
        {
            get => string.Format("Prefabs/{0}/{1}", typeof(T).Name, typeof(T).Name);
        }

        /**<summary> If you want to check null, use 'if (T.Initialized)' instead of 'if (T.Instance != null)' </summary> */
        public static T Instance
        {
            get
            {
                if (instance != null)
                    return instance;
                var g = Resources.Load<GameObject>(ResourcePath);
                if (g == null)
                {
                    Debug.LogErrorFormat("[{0}] Wrong resources path: {1}!", typeof(T).Name, ResourcePath);
                    return instance;
                }

                instance = Instantiate(g).GetComponent<T>();
                if (instance == null)
                {
                    Debug.LogErrorFormat("[{0}] Component not found in object: {1}!", typeof(T).Name, ResourcePath);
                }

                return instance;
            }
        }

        protected override void OnAwake() { }
    }

    /** <summary>
     * <para> "Instance" = Instantiate from Resources folder when be called at runtime. </para>
     * <para> Place your prefab in Resources: "Prefabs/T/T", T is the name of class </para> 
     * <para> Instance is DontDestroyOnLoad</para>
     * </summary> */
    public class SingletonResourceAlive<T> : SingletonResource<T> where T : Component
    {
        protected override void Awake()
        {
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
    }
    #endregion

    #region ScriptableObject
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        static T _instance = null;
        public static T Instance
        {
            get
            {
                if (!_instance)
                    _instance = Resources.FindObjectsOfTypeAll<T>()[0];
                return _instance;
            }
        }
    }
    #endregion

}