using Ftech.Lib.Common;
using UnityEngine;

namespace Ftech.Lib.Helper
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] private DestroyType type = DestroyType.Destroy;
        [SerializeField] private float timeLife = 1f;
        [SerializeField] private bool activeOnEnable = false;

        private bool isDestroyed;

        private void OnEnable()
        {
            isDestroyed = true;
            if (activeOnEnable)
            {
                StartAutoDestroy(timeLife, type);
            }
        }

        public void StartAutoDestroy(float timeLife, DestroyType type)
        {
            this.type = type;
            isDestroyed = false;
            this.InvokeExtension(Destroy, timeLife);
        }

        public void StopAutoDestroy()
        {
            this.CancelInvokeExtension(Destroy);
        }

        public void ForceDestroy()
        {
            StopAutoDestroy();
            Destroy();
        }

        public void ForceDestroy(DestroyType type)
        {
            this.type = type;
            StopAutoDestroy();
            Destroy();
        }

        private void Destroy() // Invoke
        {
            if (isDestroyed)
            {
                return;
            }
            OnPreDestroy();
            if (type == DestroyType.Destroy)
            {
#if UNITY_EDITOR
                DestroyImmediate(gameObject);
#else
                Destroy(gameObject);
#endif
            }
            else if (type == DestroyType.Disable)
            {
                gameObject.SetActive(false);
            }
            else if (type == DestroyType.Pool)
            {
                gameObject.Recycle();
            }

            isDestroyed = true;
        }

        protected virtual void OnPreDestroy()
        {

        }

        public enum DestroyType
        {
            Destroy, Disable, Pool, Custom
        }
    }

}
