using System;
using UnityEngine;

namespace Ftech.Lib.UI
{
    [DisallowMultipleComponent]
    public class Frame : MonoBehaviour
    {
        [Header("[Events]")]
        [SerializeField] private FrameEvent onShowed;
        [SerializeField] private FrameEvent onHidden;
        [SerializeField] private FrameEvent onPaused;
        [SerializeField] private FrameEvent onResumed;

        private bool initialized;
        private bool showed;
        private bool paused;
        private HUD hud;
        private string namePreviousFrame;

        public FrameEvent OnShowed { get { return onShowed; } }
        public FrameEvent OnHidden { get { return onHidden; } }
        public FrameEvent OnPaused { get { return onPaused; } }
        public FrameEvent OnResumed { get { return onResumed; } }

        public HUD Hud { get => hud; private set => hud = value; }
        public bool Initialized { get => initialized; private set => initialized = value; }
        public bool Showed
        {
            get => showed;
            private set
            {
                showed = value;

                if (showed)
                {
                    OnShowed?.Invoke(this);
                }
                else
                {
                    OnHidden?.Invoke(this);
                }
            }
        }
        public bool Paused
        {
            get => paused;
            private set
            {
                paused = value;

                if (paused)
                {
                    OnPaused?.Invoke(this);
                }
                else
                {
                    OnResumed?.Invoke(this);
                }
            }
        }

        public Frame Initialize(HUD hud)
        {
            if (!Initialized)
            {
                this.Hud = hud;

                Initialized = true;
                Showed = false;
                Paused = false;

                OnInitialize(hud);
            }
            return this;
        }

        public Frame Show(Action onCompleted = null, bool instant = false)
        {
            Showed = true;
            Paused = false;
            OnShow(onCompleted, instant);
            return this;
        }

        public Frame Hide(Action onCompleted = null, bool instant = false)
        {
            Showed = false;
            OnHide(onCompleted, instant);
            return this;
        }

        public Frame Pause(Action onCompleted = null, bool instant = false)
        {
            if (!Paused)
            {
                Paused = true;
                //onPaused?.Invoke(this);

                OnPause(onCompleted, instant);
            }
            return this;
        }

        public Frame Resume(Action onCompleted = null, bool instant = false)
        {
            if (Paused)
            {
                Paused = false;
                onResumed?.Invoke(this);

                OnResume(onCompleted, instant);
            }
            return this;
        }

        public virtual Frame Back()
        {
            Hide();
            return this;
        }

        protected virtual void OnInitialize(HUD hud) { }

        protected virtual void OnShow(Action onCompleted = null, bool instant = false)
        {
            gameObject.SetActive(true);
            onCompleted?.Invoke();
        }

        protected virtual void OnHide(Action onCompleted = null, bool instant = false)
        {
            gameObject.SetActive(false);
            onCompleted?.Invoke();
        }

        protected virtual void OnPause(Action onCompleted = null, bool instant = false)
        {
            onCompleted?.Invoke();
        }

        protected virtual void OnResume(Action onCompleted = null, bool instant = false)
        {
            onCompleted?.Invoke();
        }

        // For-Tracking
        public virtual string GetCurrentNameFrame()
        {
            return "Frame-Class";
        }

        public Frame SetPreviousNameFrame(string name)
        {
            namePreviousFrame = name;
            return this;
        }

        public virtual string GetPreviousNameFrame()
        {
            return string.IsNullOrEmpty(namePreviousFrame) ? "null" : namePreviousFrame;
        }
    }
}
