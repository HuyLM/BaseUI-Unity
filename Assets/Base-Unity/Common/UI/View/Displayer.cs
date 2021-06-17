using UnityEngine;

namespace AtoLib.UI
{
    public abstract class Displayer<TModel> : MonoBehaviour
    {
        public TModel Model
        {
            get; private set;
        }

        public abstract void Show();

        public Displayer<TModel> SetModel(TModel model)
        {
            Model = model;
            return this;
        }
    }
}
