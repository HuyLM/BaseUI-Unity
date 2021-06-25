using System;
using UnityEngine;

namespace Ftech.Lib.Common.UnityInspector.Editor {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class LayerFieldAttribute : PropertyAttribute {
    }
}