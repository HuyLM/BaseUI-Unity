
using AtoLib.Helper;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AtoLib.UI
{
    public class BaseProgressBar : MonoBehaviour
    {
        [SerializeField] protected Image imgCurrentValueLerp;
        [SerializeField] protected Image imgCurrentValueReal;
        [SerializeField] protected RangeFloatValue updateSpeedSecondRange;
        [SerializeField] protected bool useSetWidth;

        protected float maxWidth;
        protected float distance;
        protected bool isCompleted;
        bool isLoaded;
        protected Action onCompleted;
        private bool isUseLerp;

        protected virtual void Start()
        {
            isUseLerp = imgCurrentValueLerp != null;

            if (!isLoaded)
            {
                if (useSetWidth)
                {
                    maxWidth = imgCurrentValueReal.rectTransform.rect.width;
                }
                isLoaded = true;
            }
        }

        public void FillBar(Image img, float fillAmount)
        {
            if (!isLoaded)
            {
                if (useSetWidth)
                {
                    maxWidth = imgCurrentValueReal.rectTransform.rect.width;
                }
                isLoaded = true;
            }

            if (useSetWidth)
            {
                img.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fillAmount * maxWidth);
            }
            else
            {
                img.fillAmount = fillAmount;
            }
        }

        public void FillBar(float startPct, float endPct)
        {
            ForceFillBar(startPct);
            StartFillBar(endPct);
        }

        public virtual void StartFillBar(float pct)
        {
            if (!gameObject.activeInHierarchy)
            {
                return;
            }
            if (isUseLerp)
            {
                StopAllCoroutines();
                StartCoroutine(ChangingBar(pct));
            }

            FillBar(imgCurrentValueReal, pct);
        }

        protected virtual IEnumerator ChangingBar(float pct)
        {
            isCompleted = false;
            WaitForSeconds deltaTime = new WaitForSeconds(UnityEngine.Time.fixedDeltaTime);
            yield return deltaTime;
            float preChange = imgCurrentValueLerp.rectTransform.rect.width / maxWidth;
            distance = Mathf.Abs(pct - preChange);
            float elapsed = 0f;
            float updateSpeedSecond = updateSpeedSecondRange.GetRatioValue(distance);
            while (elapsed < distance)
            {
                elapsed += updateSpeedSecond * UnityEngine.Time.fixedDeltaTime;
                float fillAmount = Mathf.Lerp(preChange, pct, elapsed / distance);
                FillBar(imgCurrentValueLerp, fillAmount);
                yield return deltaTime;
            }
            FillBar(imgCurrentValueLerp, pct);
            LerpCompleted();
        }

        protected virtual void LerpCompleted()
        {
            isCompleted = true;
            if (onCompleted != null)
            {
                Action onAction = onCompleted;
                onCompleted = null;
                onAction.Invoke();
            }
        }

        public void ForceFillBar(float pct)
        {
            if (isUseLerp)
            {
                FillBar(imgCurrentValueLerp, pct);
            }
            FillBar(imgCurrentValueReal, pct);

        }

        public void AddOnComplete(Action onComplete)
        {
            this.onCompleted = onComplete;
        }

        public void RemoveOnComplete()
        {
            this.onCompleted = null;
        }
    }
}