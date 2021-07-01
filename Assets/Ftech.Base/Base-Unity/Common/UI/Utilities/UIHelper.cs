using UnityEngine;
using UnityEngine.UI;

namespace Ftech.Lib.Common.Utilities
{
    public static class UIHelper
    {
        public const string UIFastDefaultMaterial = "Materials/UIFastDefault";
        public const string UIFastMaskMaterial = "Materials/UIFastMask";
        public const string UIFastGreyMaterial = "Materials/UIFastGrey";
        public const string UIFastGreyMaskMaterial = "Materials/UIFastGreyMask";

        private static Material fastDefaultMaterial;
        private static Material fastGreyMaterial;
        private static Material fastMaskMaterial;
        private static Material fastGreyMaskMaterial;
        static Vector3[] corners = new Vector3[4];


        public static void SetPerfectSize(this Image image, Vector2 size)
        {
            Sprite activeSprite = image.overrideSprite ? image.overrideSprite : image.sprite;
            if (activeSprite != null)
            {
                Vector2 spriteSize = activeSprite.rect.size;
                Vector2 perfectSize = size;

                if (spriteSize.x / spriteSize.y >= size.x / size.y)
                {
                    perfectSize.y = spriteSize.y / (spriteSize.x / size.x);
                }
                else
                {
                    perfectSize.x = spriteSize.x / (spriteSize.y / size.y);
                }

                image.rectTransform.anchorMax = image.rectTransform.anchorMin;
                image.rectTransform.sizeDelta = perfectSize;
            }
        }

        public static void SetAlpha(this Graphic graphic, float alpha)
        {
            Color color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }

        public static void SetGray(this Graphic graphic, bool isOn, bool isMask = false)
        {
            if (isMask)
            {
                if (isOn)
                {
                    if (fastGreyMaskMaterial == null)
                    {
                        fastGreyMaskMaterial = Resources.Load<Material>(UIFastGreyMaskMaterial);
                    }
                    graphic.material = fastGreyMaskMaterial;
                }
                else
                {
                    if (fastMaskMaterial == null)
                    {
                        fastMaskMaterial = Resources.Load<Material>(UIFastMaskMaterial);
                    }
                    graphic.material = fastMaskMaterial;
                }
            }
            else
            {
                if (isOn)
                {
                    if (fastGreyMaterial == null)
                    {
                        fastGreyMaterial = Resources.Load<Material>(UIFastGreyMaterial);
                    }
                    graphic.material = fastGreyMaterial;
                }
                else
                {
                    if (fastDefaultMaterial == null)
                    {
                        fastDefaultMaterial = Resources.Load<Material>(UIFastDefaultMaterial);
                    }
                    graphic.material = fastDefaultMaterial;
                }
            }
        }

        public static Bounds TransformBoundsTo(this RectTransform source, Transform target)
        {
            var bounds = new Bounds();
            if (source != null)
            {
                source.GetWorldCorners(corners);

                var vMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
                var vMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);

                var matrix = target.worldToLocalMatrix;
                for (int j = 0; j < 4; j++)
                {
                    Vector3 v = matrix.MultiplyPoint3x4(corners[j]);
                    vMin = Vector3.Min(v, vMin);
                    vMax = Vector3.Max(v, vMax);
                }

                bounds = new Bounds(vMin, Vector3.zero);
                bounds.Encapsulate(vMax);
            }
            return bounds;
        }

        public static float NormalizeScrollDistance(this ScrollRect scrollRect, int axis, float distance)
        {
            var viewport = scrollRect.viewport;
            var viewRect = viewport != null ? viewport : scrollRect.GetComponent<RectTransform>();
            var viewBounds = new Bounds(viewRect.rect.center, viewRect.rect.size);

            var content = scrollRect.content;
            var contentBounds = content != null ? content.TransformBoundsTo(viewRect) : new Bounds();

            var hiddenLength = contentBounds.size[axis] - viewBounds.size[axis];
            return distance / hiddenLength;
        }

        public static float GetVerticalNormalizedPositionAt(this ScrollRect scrollRect, RectTransform target)
        {
            RectTransform view = scrollRect.viewport != null ? scrollRect.viewport : scrollRect.GetComponent<RectTransform>();

            Rect viewRect = view.rect;
            Bounds elementBounds = target.TransformBoundsTo(view);
            float offset = viewRect.center.y - elementBounds.center.y;

            float scrollPos = scrollRect.verticalNormalizedPosition - scrollRect.NormalizeScrollDistance(1, offset);
            return Mathf.Clamp(scrollPos, 0f, 1f);
        }

        public static float GetHorizotalNormalizedPositionAt(this ScrollRect scrollRect, RectTransform target)
        {
            RectTransform view = scrollRect.viewport != null ? scrollRect.viewport : scrollRect.GetComponent<RectTransform>();

            Rect viewRect = view.rect;
            Bounds elementBounds = target.TransformBoundsTo(view);
            float offset = viewRect.center.x - elementBounds.center.x;

            float scrollPos = scrollRect.horizontalNormalizedPosition - scrollRect.NormalizeScrollDistance(0, offset);
            return Mathf.Clamp(scrollPos, 0f, 1f);
        }

        public static RectTransform RectTransform(this Component component)
        {
            return component.transform as RectTransform;
        }

        public static void SetLeft(this RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        }

        public static void SetRight(this RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(right, rt.offsetMax.y);
        }

        public static void SetTop(this RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, top);
        }

        public static void SetBottom(this RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }
    }
}
