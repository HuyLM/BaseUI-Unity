
using System;
using UnityEngine;
namespace AtoLib.Helper
{
    [Serializable]
    public struct RangeIntValue
    {
        public int startValue;
        public int endValue;

        public int GetRandomValue()
        {
            return RandomHelper.RandomInRange(this);
        }
    }

    [Serializable]
    public struct RangeFloatValue
    {
        public float startValue;
        public float endValue;

        public float GetRandomValue()
        {
            return RandomHelper.RandomInRange(this);
        }

        public float GetRatioValue(float ratio)
        {
            ratio = Mathf.Clamp01(ratio);
            return startValue + (endValue - startValue) * ratio;
        }
    }
}
