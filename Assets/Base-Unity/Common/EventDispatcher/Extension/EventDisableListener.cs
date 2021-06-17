namespace AtoLib.Common
{
    public class EvenDisableListener : EventListenerBase
    {
        private void OnEnable()
        {
            if (this.listener != null)
            {
                this.listener(true);
            }
        }
        private void OnDisable()
        {
            if (this.listener != null)
            {
                this.listener(false);
            }
        }
    }
}