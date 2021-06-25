using System;
using UnityEngine;

namespace Ftech.Lib.Common.UnityInspector.Editor {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class TextureFieldAttribute : PropertyAttribute {
        public readonly float size;

        public TextureFieldAttribute(float size = 64f) {
            this.size = size;
        }
    }
}