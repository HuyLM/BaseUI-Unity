using System;
using static Ftech.Lib.Common.EventListenerBase;

namespace Ftech.Lib.Common
{
    /// <summary>
    /// Use this when you want to dispatch a message to other listener in your system, to do reduce instance and reduce reference together.
    /// <para>Call "SetEnableDebugLog" to show debug log</para>
    /// </summary>
    public class EventDispatcher : Singleton<EventDispatcher>
    {
        private IntEventObserver intEventObserver;
        private ParamsEventObserver paramsEventObserver;
        private IntEventObserver IntEventObserver
        {
            get
            {
                if (this.intEventObserver != null)
                {
                    return this.intEventObserver;
                }
                this.intEventObserver = new IntEventObserver();
                return this.intEventObserver;
            }
        }
        private ParamsEventObserver ParamsEventObserver
        {
            get
            {
                if (this.paramsEventObserver != null)
                {
                    return this.paramsEventObserver;
                }
                this.paramsEventObserver = new ParamsEventObserver();
                return this.paramsEventObserver;
            }
        }
        /// <summary> Becarefull. Should use when your game has only one scene, or all your game logic place in only one scene (except logoscene).</summary>
        public void RemoveAllListener()
        {
            if (this.paramsEventObserver != null)
            {
                this.paramsEventObserver.RemoveAllListener();
            }
            GC.Collect();
        }
        protected override void OnDispose()
        {
            if (this.paramsEventObserver != null)
            {
                this.paramsEventObserver = null;
            }
        }
        /// <summary>IntEventObserver listener</summary> 
        public void Dispatch(int key, object value = null)
        {
            this.IntEventObserver.SetLogPrefix(key.ToString());
            this.IntEventObserver.Dispatch(key, value);
        }
        /// <summary>IntEventObserver listener</summary> 
        public void AddListener(int key, Action<object> eventCallback)
        {
            this.IntEventObserver.SetLogPrefix(key.ToString());
            this.IntEventObserver.AddListener(key, eventCallback);
        }
        /// <summary>IntEventObserver listener</summary> 
        public void AddListener(int key, Action eventCallback)
        {
            this.IntEventObserver.SetLogPrefix(key.ToString());
            this.IntEventObserver.AddListener(key, eventCallback);
        }
        /// <summary>IntEventObserver listener</summary> 
        public void RemoveListener(int key, Action<object> eventCallback)
        {
            this.IntEventObserver.SetLogPrefix(key.ToString());
            this.IntEventObserver.RemoveListener(key, eventCallback);
        }
        /// <summary>IntEventObserver listener</summary> 
        public void RemoveListener(int key, Action eventCallback)
        {
            this.IntEventObserver.SetLogPrefix(key.ToString());
            this.IntEventObserver.RemoveListener(key, eventCallback);
        }
        /// <summary> Remove ALL callback listener by `key`. Be carefully, use at OnDestroy only to free RAM. </summary> 
        public void RemoveListener(int key)
        {
            this.IntEventObserver.SetLogPrefix(key.ToString());
            this.IntEventObserver.RemoveListener(key);
        }
        /// <summary>ParamsEventObserver listener</summary> 
        public void Dispatch<T>() where T : IEventParams
        {
            this.ParamsEventObserver.Dispatch<T>();
        }
        /// <summary>ParamsEventObserver listener</summary> 
        public void Dispatch<T>(T eventParams) where T : IEventParams
        {
            this.ParamsEventObserver.Dispatch<T>(eventParams);
        }
        /// <summary>ParamsEventObserver listener</summary> 
        public void AddListener<T>(Action<T> eventCallback) where T : IEventParams
        {
            this.ParamsEventObserver.AddListener<T>(eventCallback);
        }
        /// <summary>ParamsEventObserver listener</summary> 
        public void AddListener<T>(Action eventCallback) where T : IEventParams
        {
            this.ParamsEventObserver.AddListener<T>(eventCallback);
        }
        /// <summary>ParamsEventObserver listener</summary> 
        public void RemoveListener<T>(Action<T> eventCallback) where T : IEventParams
        {
            this.ParamsEventObserver.RemoveListener<T>(eventCallback);
        }
        /// <summary>ParamsEventObserver listener</summary> 
        public void RemoveListener<T>(Action eventCallback) where T : IEventParams
        {
            this.ParamsEventObserver.RemoveListener<T>(eventCallback);
        }
        /// <summary> Remove ALL callback listener by `key`. Be carefully, use at OnDestroy only to free RAM. </summary> 
        public void RemoveListener<T>() where T : IEventParams
        {
            this.ParamsEventObserver.RemoveListener<T>();
        }
    }
}