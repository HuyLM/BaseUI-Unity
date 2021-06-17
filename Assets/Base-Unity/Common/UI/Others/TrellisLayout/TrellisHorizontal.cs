
using UnityEngine;
using UnityEngine.UI;
namespace AtoLib.UI
{
    public class TrellisHorizontal : MonoBehaviour
    {
        [SerializeField] private HorizontalLayoutGroup horizontalLayout;
        [SerializeField] private RectTransform myRectTransform;
        private bool isHight;
        public bool IsHight { get => isHight; }


        public void AddItem(Transform item)
        {
            Vector3 curScale = item.localScale;
            item.SetParent(transform);
            item.localScale = curScale;
        }

        public void Set(float w, float h, float spacing, bool isHight)
        {
            this.isHight = isHight;
            horizontalLayout.spacing = spacing;
            myRectTransform.sizeDelta = new Vector2(w, h);
            horizontalLayout.childAlignment = TextAnchor.UpperLeft;
            horizontalLayout.childControlHeight = false;
            horizontalLayout.childControlWidth = false;
            horizontalLayout.childForceExpandHeight = true;
            horizontalLayout.childForceExpandWidth = false;
        }

    }
}