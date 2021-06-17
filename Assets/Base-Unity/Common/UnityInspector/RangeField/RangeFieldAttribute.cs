using System;
using UnityEngine;

namespace GameSystem.Common.UnityInspector {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RangeFieldAttribute : PropertyAttribute {
        public readonly float minValue;
        public readonly float maxValue;
        public readonly float snapValue;

        public RangeFieldAttribute(float minValue, float maxValue, float snapValue = 0f) {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.snapValue = snapValue;
        }

        public RangeFieldAttribute(int minValue, int maxValue, int snapValue = 0) {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.snapValue = snapValue;
        }
    }
}