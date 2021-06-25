using System.Collections;
using UnityEngine;

namespace Ftech.Lib.Helper
{
    [RequireComponent(typeof(TrailRenderer))]
    public class HideTrail : MonoBehaviour
    {
        private float time;
        private TrailRenderer trail;
        bool isInit;

        private void Awake()
        {
            isInit = true;
            time = Trail.time;
        }

        protected TrailRenderer Trail
        {
            get
            {
                if (trail == null)
                {
                    trail = GetComponent<TrailRenderer>();
                }
                return trail;
            }
        }
        public void Hide()
        {
            Trail.Clear();
            Trail.time = -1;
        }

        public void Show()
        {
            if (gameObject.activeInHierarchy)
                StartCoroutine(IShow());
        }

        private IEnumerator IShow()
        {
            yield return Yielder.EndOfFrame;
            yield return Yielder.EndOfFrame;

            if (isInit)
            {
                Trail.time = time;
            }
        }
    }
}
