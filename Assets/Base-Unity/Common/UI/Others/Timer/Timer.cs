using System;
using UnityEngine;
using DG.Tweening;

namespace AtoLib.UI
{
    public class Timer : MonoBehaviour
    {
        public bool stopOnDisable = false;

        private Tween tween;

        private void OnDisable()
        {
            if (stopOnDisable)
            {
                Stop();
            }
        }

        private void OnDestroy()
        {
            Stop();
        }

        public void Stop()
        {
            tween?.Kill();
        }

        public void Countdown(TimeSpan duration, Action<TimeSpan> onUpdate, Action onComplete, bool ignoreTimescale = false)
        {
            Stop();

            tween = DOVirtual.Float((float)duration.TotalSeconds, 0f, (float)duration.TotalSeconds, value =>
            {
                onUpdate?.Invoke(new TimeSpan((long)value * TimeSpan.TicksPerSecond));
            }).SetUpdate(ignoreTimescale)
            .SetEase(Ease.Linear)
            .OnComplete(() => onComplete?.Invoke());
        }

        public void Countdown(float duration, Action<float> onUpdate, Action onComplete, bool ignoreTimescale = false)
        {
            Stop();

            tween = DOVirtual.Float(duration, 0f, duration, value =>
            {
                onUpdate?.Invoke(value);
            }).SetUpdate(ignoreTimescale)
            .SetEase(Ease.Linear)
            .OnComplete(() => onComplete?.Invoke());
        }

        public void Tick(Action<TimeSpan> onUpdate, bool ignoreTimescale = false)
        {
            Stop();

            tween = DOVirtual.Float(0f, float.MaxValue, float.MaxValue, value =>
            {
                onUpdate?.Invoke(new TimeSpan((long)value * TimeSpan.TicksPerSecond));
            }).SetUpdate(ignoreTimescale)
            .SetEase(Ease.Linear);
        }

        public void Tick(Action<float> onUpdate, bool ignoreTimescale = false)
        {
            Stop();

            tween = DOVirtual.Float(0f, float.MaxValue, float.MaxValue, value =>
            {
                onUpdate?.Invoke(value);
            }).SetUpdate(ignoreTimescale)
            .SetEase(Ease.Linear);
        }
    }
}