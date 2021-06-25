using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ftech.Lib.Common
{
    public class EventListenerBase : MonoBehaviour
    {
        protected Action<bool> listener;
        public void SetListener<T>(Action<T> Action) where T : IEventParams
        {
            this.listener = delegate (bool active)
            {
                if (active)
                {
                    Singleton<EventDispatcher>.Instance.AddListener<T>(Action);
                    return;
                }
                Singleton<EventDispatcher>.Instance.RemoveListener<T>(Action);
            }
            ;
            this.listener(true);
        }
        public void SetListener<T>(Action Action) where T : IEventParams
        {
            this.listener = delegate (bool active)
            {
                if (active)
                {
                    Singleton<EventDispatcher>.Instance.AddListener<T>(Action);
                    return;
                }
                Singleton<EventDispatcher>.Instance.RemoveListener<T>(Action);
            }
            ;
            this.listener(true);
        }
        public void SetListener(int key, Action Action)
        {
            this.listener = delegate (bool active)
            {
                if (active)
                {
                    Singleton<EventDispatcher>.Instance.AddListener(key, Action);
                    return;
                }
                Singleton<EventDispatcher>.Instance.RemoveListener(key, Action);
            }
            ;
            this.listener(true);
        }
        public void SetListener(int key, Action<object> Action)
        {
            this.listener = delegate (bool active)
            {
                if (active)
                {
                    Singleton<EventDispatcher>.Instance.AddListener(key, Action);
                    return;
                }
                Singleton<EventDispatcher>.Instance.RemoveListener(key, Action);
            }
            ;
            this.listener(true);
        }

        internal class EventObserver<K, V> where V : class
        {
            private readonly Dictionary<K, Dictionary<int, Action<V>>> observerDictionary = new Dictionary<K, Dictionary<int, Action<V>>>();
            public bool EnableDebugLog
            {
                get
                {
                    return false;
                }
            }
            protected string LogPrefix
            {
                get;
                set;
            }
            protected void DebugLog(string msg)
            {
                if (!this.EnableDebugLog)
                {
                    return;
                }
                // Debug.Log("[EventDispatcher][" + this.LogPrefix + "] " + msg, this.EnableDebugLog, Logs.Color.Magenta);
                this.LogPrefix = string.Empty;
            }
            public void AddListener(K key, Action<V> eventCallback)
            {
                this.AddByHashCode(key, eventCallback.GetHashCode(), eventCallback);
            }
            public void AddListener(K key, Action eventCallback)
            {
                int hashCode = eventCallback.GetHashCode();
                Action<V> action = delegate (V _object)
                {
                    eventCallback();
                }
                ;
                this.AddByHashCode(key, hashCode, action);
            }
            public void RemoveListener(K key, Action<V> eventCallback)
            {
                this.RemoveByHashCode(key, eventCallback.GetHashCode());
            }
            public void RemoveListener(K key, Action eventCallback)
            {
                this.RemoveByHashCode(key, eventCallback.GetHashCode());
            }
            /// <summary> Remove ALL callback listener by `key`. Be carefully, use at OnDestroy only to free RAM. </summary> 
            public void RemoveListener(K key)
            {
                this.RemoveByKey(key);
            }
            /// <summary> Remove all listener. Be carefully, use at OnDestroy or OnApplicationQuiting only to free RAM. </summary> 
            public void RemoveAllListener()
            {
                this.observerDictionary.Clear();
            }
            protected void AddByHashCode(K key, int hashCode, Action<V> action)
            {
                if (this.EnableDebugLog)
                {
                    this.DebugLog(string.Format("Register Key: {0}", key));
                }
                Dictionary<int, Action<V>> dictionary;
                if (!this.observerDictionary.TryGetValue(key, out dictionary))
                {
                    dictionary = new Dictionary<int, Action<V>>();
                    this.observerDictionary[key] = dictionary;
                }
                dictionary[hashCode] = action;
            }
            protected void RemoveByHashCode(K key, int hashCode)
            {
                if (this.EnableDebugLog)
                {
                    this.DebugLog(string.Format("UnRegister Key: {0}", key));
                }
                Dictionary<int, Action<V>> dictionary;
                if (this.observerDictionary.TryGetValue(key, out dictionary))
                {
                    dictionary.Remove(hashCode);
                }
            }
            private void RemoveByKey(K key)
            {
                if (this.observerDictionary.Remove(key) && this.EnableDebugLog)
                {
                    this.DebugLog(string.Format("UnRegister All of Key: {0}", key));
                }
            }
            public void Dispatch(K key, V obj = null)
            {
                Dictionary<int, Action<V>> dictionary;
                if (this.observerDictionary.TryGetValue(key, out dictionary))
                {
                    Dictionary<int, Action<V>>.ValueCollection values = dictionary.Values;
                    if (this.EnableDebugLog)
                    {
                        this.DebugLog(string.Format("Dispatch total_events={0}, key={1}, action=[{2}])", values.Count, key, obj));
                    }
                    using (Dictionary<int, Action<V>>.ValueCollection.Enumerator enumerator = values.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            enumerator.Current(obj);
                        }
                        return;
                    }
                }
                if (this.EnableDebugLog)
                {
                    this.DebugLog(string.Format("No dispatch to send: (key:{0}, action:{1})", key, obj));
                }
            }
        }

        internal sealed class IntEventObserver : EventObserver<int, object>
        {
            public void SetLogPrefix(string prefix)
            {
                base.LogPrefix = prefix;
            }
        }

        /// <summary> Send multi params to dispatcher </summary>
        internal sealed class ParamsEventObserver : EventObserver<Type, IEventParams>
        {
            public void AddListener<T>(Action<T> eventCallback) where T : IEventParams
            {
                base.LogPrefix = typeof(T).FullName;
                Action<IEventParams> action = delegate (IEventParams param)
                {
                    eventCallback((T)param);
                }
                ;
                base.AddByHashCode(typeof(T), eventCallback.GetHashCode(), action);
            }
            public void RemoveListener<T>(Action<T> eventCallback) where T : IEventParams
            {
                base.LogPrefix = typeof(T).FullName;
                base.RemoveByHashCode(typeof(T), eventCallback.GetHashCode());
            }
            public void AddListener<T>(Action eventCallback) where T : IEventParams
            {
                base.LogPrefix = typeof(T).FullName;
                Action<IEventParams> action = delegate (IEventParams param)
                {
                    eventCallback();
                }
                ;
                base.AddByHashCode(typeof(T), eventCallback.GetHashCode(), action);
            }
            public void RemoveListener<T>(Action eventCallback) where T : IEventParams
            {
                base.LogPrefix = typeof(T).FullName;
                base.RemoveByHashCode(typeof(T), eventCallback.GetHashCode());
            }
            public void RemoveListener<T>() where T : IEventParams
            {
                base.LogPrefix = typeof(T).FullName;
                base.RemoveListener(typeof(T));
            }
            public void Dispatch<T>() where T : IEventParams
            {
                base.LogPrefix = typeof(T).FullName;
                base.Dispatch(typeof(T), null);
            }
            public void Dispatch<T>(T param) where T : IEventParams
            {
                base.LogPrefix = typeof(T).FullName;
                base.Dispatch(typeof(T), param);
            }
        }

        internal sealed class StringEventObserver : EventObserver<string, object>
        {
            public void SetLogPrefix(string prefix)
            {
                base.LogPrefix = prefix;
            }
        }
    }
}