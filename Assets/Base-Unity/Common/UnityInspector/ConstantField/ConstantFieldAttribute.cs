using System;
using UnityEngine;

namespace Ftech.Lib.Common.UnityInspector.Editor {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ConstantFieldAttribute : PropertyAttribute {
        public readonly Type type;

        public ConstantFieldAttribute(Type type) {
            this.type = type;
        }
    }
}

