using System;
using UnityEngine;

namespace Ftech.Lib.Common.UnityInspector.Editor {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class MinMaxSliderAttribute : PropertyAttribute {
        public readonly float minValue;
        public readonly float maxValue;
        public readonly float snapValue;

        public MinMaxSliderAttribute(float minValue, float maxValue, float snapValue = 0f) {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.snapValue = snapValue;
        }

        public MinMaxSliderAttribute(int minValue, int maxValue, int snapValue = 0) {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.snapValue = snapValue;
        }
    }
}