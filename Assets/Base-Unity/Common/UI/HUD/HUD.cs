using Ftech.Lib.UnityInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ftech.Lib.UI
{
    [DisallowMultipleComponent]
    public class HUD : MonoBehaviour
    {
        [SerializeField] private int order;
        [SerializeField] private Frame defaultFrame;
        [Header("[Frames]")]
        [SerializeField] private Frame[] frames;


        [ReadOnly] private readonly List<Frame> activeFrames = new List<Frame>();
        [ReadOnly] private readonly List<Frame> loadedFrames = new List<Frame>();


        protected virtual void Start()
        {
            foreach (Frame frame in frames)
            { // ???
                InitializeFrame(frame);
            }

            if (defaultFrame)
            {
                if (activeFrames.Contains(defaultFrame))
                    return;
                Frame newDefaultFrame = Instantiate(defaultFrame, transform);
                loadedFrames.Add(newDefaultFrame);
                Show(newDefaultFrame);
            }
        }

        protected virtual void OnEnable()
        {
            HUDManager.Add(this);
        }

        protected virtual void OnDisable()
        {
            HUDManager.Remove(this);
        }

        protected virtual void OnDestroy()
        {
            foreach (Frame frame in loadedFrames)
            {
                if (frame == null)
                    continue;
                frame.OnShowed.RemoveListener(OnFrameShowed);
                frame.OnHidden.RemoveListener(OnFrameHidden);
            }
        }

        public virtual bool OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && GetActiveFrameCount() > 0)
            {
                Back();
                return true;
            }
            return false;
        }

        #region Get States

        public int Order { get => order; }

        public int GetFrameCount()
        {
            return frames.Length;
        }

        public int GetActiveFrameCount()
        {
            return activeFrames.Count;
        }

        public Frame GetFrameOnTop()
        {
            if (activeFrames.Count < 1)
                return null;
            return activeFrames[activeFrames.Count - 1];
        }

        public F GetFrameOnTop<F>() where F : Frame
        {
            return GetFrameOnTop() as F;
            ;
        }

        public bool IsFrameOnTop(Frame target)
        {
            return GetFrameOnTop() == target;
        }

        public IEnumerable<Frame> GetFrames()
        {
            foreach (Frame frame in frames)
            {
                yield return frame;
            }
        }

        public IEnumerable<Frame> GetLoadedFrames()
        {
            foreach (Frame frame in loadedFrames)
            {
                yield return frame;
            }
        }

        public IEnumerable<Frame> GetActiveFrames()
        {
            foreach (Frame frame in activeFrames)
            {
                yield return frame;
            }
        }

        public F GetFrame<F>() where F : Frame
        {
            foreach (Frame frame in GetLoadedFrames())
            {
                if (frame is F)
                {
                    if (!frame.GetType().IsSubclassOf(typeof(F)))
                    {
                        return frame as F;
                    }
                }
            }

            foreach (Frame frame in GetFrames())
            {
                if (frame is F)
                {
                    if (!frame.GetType().IsSubclassOf(typeof(F)))
                    {
                        Frame loadFrame = Instantiate(frame, transform);
                        if (loadFrame != null)
                        {
                            loadedFrames.Add(loadFrame);
                            return loadFrame as F;
                        }
                    }
                }
            }

            Debug.LogError($"[Frame] {typeof(F).Name} can't load");
            return null;
        }

        public F GetActiveFrame<F>() where F : Frame
        {
            foreach (Frame frame in GetActiveFrames())
            {
                if (frame is F)
                {
                    return frame as F;
                }
            }
            return null;
        }

        public bool ContainsFrame<F>() where F : Frame
        {
            foreach (Frame frame in GetLoadedFrames())
            {
                if (frame is F)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsFrame(Frame target)
        {
            foreach (Frame frame in GetLoadedFrames())
            {
                if (frame == target)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsActiveFrame<F>() where F : Frame
        {
            foreach (Frame frame in GetActiveFrames())
            {
                if (frame is F)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsActiveFrame(Frame target)
        {
            foreach (Frame frame in GetActiveFrames())
            {
                if (frame == target)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Show & Hide & Pause

        public F Show<F>(Action onCompleted = null, bool instant = false, bool hideCurrent = false, bool pauseCurrent = false) where F : Frame
        {
            return Show(GetFrame<F>(), onCompleted, instant, hideCurrent, pauseCurrent) as F;
        }

        public virtual Frame Show(Frame frame, Action onCompleted = null, bool instant = false, bool hideCurrent = false, bool pauseCurrent = false)
        {
            if (frame == null)
                return null;
            if (IsFrameOnTop(frame))
                return frame;

            if (!frame.Initialized)
            {
                InitializeFrame(frame);
            }

            if (frame.Hud != this)
            {
                Debug.LogWarningFormat("[HUD] The trying to open a frame {0} is not within the scope of the current hud {1}.", frame.name, GetType().Name);
                return null;
            }

            if (ContainsActiveFrame(frame))
            {
                Debug.LogWarningFormat("[HUD] The trying to open a frame {0} has been opened before.", frame.name);
                activeFrames.Remove(frame);
            }

            if (hideCurrent)
            {
                Hide();
            }
            else if (pauseCurrent)
            {
                Pause();
            }

            return frame.Show(onCompleted, instant);
        }

        public F Hide<F>(Action onCompleted = null, bool instant = false) where F : Frame
        {
            return Hide(GetFrame<F>(), onCompleted, instant) as F;
        }

        public Frame Hide(Action onCompleted = null, bool instant = false)
        {
            return Hide(GetFrameOnTop(), onCompleted, instant);
        }

        public virtual Frame Hide(Frame frame, Action onCompleted = null, bool instant = false)
        {
            if (frame == null)
                return null;
            if (!ContainsActiveFrame(frame))
            {
                Debug.LogWarningFormat("[HUD] The frame {0} has not been opened before.", frame.name);
                return null;
            }

            //if (IsFrameOnTop(frame)) {
            //    Debug.LogWarningFormat("[HUD] The closing a frame {0} is not on the top.", frame.name);
            //}

            return frame.Hide(onCompleted, instant);
        }

        public virtual Frame Pause(Frame frame, Action onCompleted = null, bool instant = false)
        {
            if (frame == null)
                return null;
            if (!ContainsActiveFrame(frame))
            {
                Debug.LogWarningFormat("[HUD] The frame {0} has not been opened before.", frame.name);
                return null;
            }

            return frame.Pause(onCompleted, instant);
        }

        public virtual Frame Resume(Frame frame, Action onCompleted = null, bool instant = false)
        {
            if (frame == null)
                return null;
            if (!ContainsActiveFrame(frame))
            {
                Debug.LogWarningFormat("[HUD] The frame {0} has not been opened before.", frame.name);
                return null;
            }

            return frame.Resume(onCompleted, instant);
        }

        public int PauseAll()
        {
            int hideCount = activeFrames.Count;
            for (int i = activeFrames.Count - 1; i >= 0; i--)
            {
                activeFrames[i].Pause(null, true);
            }
            return hideCount;
        }

        public int ResumeAll()
        {
            int hideCount = activeFrames.Count;
            for (int i = activeFrames.Count - 1; i >= 0; i--)
            {
                activeFrames[i].Resume(null, true);
            }
            return hideCount;
        }

        public int HideAll()
        {
            int hideCount = activeFrames.Count;
            for (int i = activeFrames.Count - 1; i >= 0; i--)
            {
                activeFrames[i].Hide(null, true);
            }
            return hideCount;
        }

        public F HideTo<F>() where F : Frame
        {
            for (int i = activeFrames.Count - 1; i >= 0; i--)
            {
                if (activeFrames[i] is F)
                {
                    activeFrames[i].Resume();
                    break;
                }
                else
                {
                    activeFrames[i].Hide(null, true);
                }
            }
            return Show<F>();
        }

        #endregion

        protected virtual void InitializeFrame(Frame frame)
        {
            if (frame == null)
                return;
            frame.gameObject.SetActive(false);
            frame.Initialize(this);
            frame.OnShowed.AddListener(OnFrameShowed);
            frame.OnHidden.AddListener(OnFrameHidden);
        }

        protected virtual void OnFrameShowed(Frame frame)
        {
            if (frame == null)
                return;
            if (frame == GetFrameOnTop())
                return;
            if (!ContainsFrame(frame))
                return;

            frame.transform.SetAsLastSibling();
            activeFrames.Add(frame);
        }

        protected virtual void OnFrameHidden(Frame frame)
        {
            if (frame == null)
                return;
            if (!ContainsFrame(frame))
                return;

            bool isFrameOnTop = frame == GetFrameOnTop();
            activeFrames.Remove(frame);

            if (isFrameOnTop)
            {
                GetFrameOnTop()?.Resume();
            }
        }

        public virtual void Back()
        {
            Frame frameOnTop = GetFrameOnTop();
            if (frameOnTop == null)
                return;

            frameOnTop.Back();
        }

        #region Extensions

        public void Show(Frame frame)
        {
            Show(frame, null);
        }

        public void HideAndShow(Frame frame)
        {
            Show(frame, null, false, true);
        }

        public void PauseAndShow(Frame frame)
        {
            Show(frame, null, false, false, true);
        }

        public void HideAndShowInstant(Frame frame)
        {
            Show(frame, null, true, true);
        }

        public void PauseAndShowInstant(Frame frame)
        {
            Show(frame, null, true, false, true);
        }

        public void Hide(Frame frame)
        {
            Hide(frame, null, false);
        }

        public void HideInstant(Frame frame)
        {
            Hide(frame, null, true);
        }

        public void Hide()
        {
            Hide(null, false);
        }

        public void Pause(Frame frame)
        {
            Pause(frame, null, false);
        }

        public void Pause()
        {
            Pause(GetFrameOnTop(), null, false);
        }

        public void Resume(Frame frame)
        {
            Resume(frame, null, false);
        }

        public void Resume()
        {
            Resume(GetFrameOnTop(), null, false);
        }

        #endregion
    }

    public abstract class HUD<T> : HUD where T : HUD<T>
    {
        private static T instance;

        public static bool HasInstance => instance != null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        Debug.LogErrorFormat("[SINGLETON] Class {0} must be added to scene before run!", typeof(T));
                    }
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else if (instance != this)
            {
                Debug.LogWarningFormat("[SINGLETON] Class {0} is initialized multiple times!", typeof(T));
                Destroy(this.gameObject);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            instance = null;
        }
    }
}
