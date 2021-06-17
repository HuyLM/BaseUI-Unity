using System;
using UnityEngine;

namespace AtoLib.Common
{
    public static class EventDispatcherExtension
    {
        public static void AddListener(this MonoBehaviour mono, int key, Action<object> action, bool untilDisable = true)
        {
            if (untilDisable)
            {
                EventDispatcherExtension.GetOrAddComponent<EvenDisableListener>(mono, key, action);
                return;
            }
            EventDispatcherExtension.GetOrAddComponent<EventDestroyListener>(mono, key, action);
        }
        public static void AddListener(this MonoBehaviour mono, int key, Action action, bool untilDisable = true)
        {
            if (untilDisable)
            {
                EventDispatcherExtension.GetOrAddComponent<EvenDisableListener>(mono, key, action);
                return;
            }
            EventDispatcherExtension.GetOrAddComponent<EventDestroyListener>(mono, key, action);
        }
        /// <summary>
        /// Short call of "EventDispatcher.Instance.AddListener", this will auto add a component into caller gameobject
        /// <para>If "untilDisable" = true, observer will use OnEnable to register listener and OnDisable to remove listener, else use Awake and OnDestroy</para>
        /// </summary>
        /// <param name="untilDisable">If true, observer will use OnDisable to remove listener, else will use OnDestroy</param>
        public static void AddListener<T>(this MonoBehaviour mono, Action<T> action, bool untilDisable = true) where T : IEventParams
        {
            if (untilDisable)
            {
                EventDispatcherExtension.GetOrAddComponent<T, EvenDisableListener>(mono, action);
                return;
            }
            EventDispatcherExtension.GetOrAddComponent<T, EventDestroyListener>(mono, action);
        }
        /// <summary>
        /// Short call of "EventDispatcher.Instance.AddListener", this will auto add a component into caller gameobject
        /// <para>If "untilDisable" = true, observer will use OnEnable to register listener and OnDisable to remove listener, else use Awake and OnDestroy</para>
        /// </summary>
        /// <param name="untilDisable">If true, observer will use OnDisable to remove listener, else will use OnDestroy</param>
        public static void AddListener<T>(this MonoBehaviour mono, Action action, bool untilDisable = true) where T : IEventParams
        {
            if (untilDisable)
            {
                EventDispatcherExtension.GetOrAddComponent<T, EvenDisableListener>(mono, action);
                return;
            }
            EventDispatcherExtension.GetOrAddComponent<T, EventDestroyListener>(mono, action);
        }
        /// <summary>
        /// Short call of "EventDispatcher.Instance.Dispatch" with key is T as IEventParams
        /// </summary>
        public static void Dispatch<T>(this MonoBehaviour mono) where T : IEventParams
        {
            Singleton<EventDispatcher>.Instance.Dispatch<T>();
        }
        /// <summary>
        /// Short call of "EventDispatcher.Instance.Dispatch" with key is T as IEventParams
        /// </summary>
        public static void Dispatch<T>(this MonoBehaviour mono, T para) where T : IEventParams
        {
            Singleton<EventDispatcher>.Instance.Dispatch<T>(para);
        }
        /// <summary>
        /// Short call of "EventDispatcher.Instance.Dispatch" with key is int
        /// </summary>
        public static void Dispatch(this MonoBehaviour mono, int key)
        {
            Singleton<EventDispatcher>.Instance.Dispatch(key, null);
        }
        /// <summary>
        /// Short call of "EventDispatcher.Instance.Dispatch" with key is int
        /// </summary>
        public static void Dispatch(this MonoBehaviour mono, int key, object param)
        {
            Singleton<EventDispatcher>.Instance.Dispatch(key, param);
        }
        private static void GetOrAddComponent<TS>(MonoBehaviour mono, int key, Action<object> action) where TS : EventListenerBase
        {
            EventDispatcherExtension.GetOrAddComponent<TS>(mono).SetListener(key, action);
        }
        private static void GetOrAddComponent<TS>(MonoBehaviour mono, int key, Action action) where TS : EventListenerBase
        {
            EventDispatcherExtension.GetOrAddComponent<TS>(mono).SetListener(key, action);
        }
        private static void GetOrAddComponent<T, TS>(MonoBehaviour mono, Action<T> action) where T : IEventParams where TS : EventListenerBase
        {
            EventDispatcherExtension.GetOrAddComponent<TS>(mono).SetListener<T>(action);
        }
        private static void GetOrAddComponent<T, TS>(MonoBehaviour mono, Action action) where T : IEventParams where TS : EventListenerBase
        {
            EventDispatcherExtension.GetOrAddComponent<TS>(mono).SetListener<T>(action);
        }
        private static T GetOrAddComponent<T>(MonoBehaviour mono) where T : EventListenerBase
        {
            T t = mono.GetComponent<T>();
            if (null == t)
            {
                t = mono.gameObject.AddComponent<T>();
            }
            return t;
        }
    }
}