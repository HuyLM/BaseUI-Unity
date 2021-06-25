using System;
using UnityEngine;

namespace Ftech.Lib.UnityInspector {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ReadOnlyAttribute : PropertyAttribute {
    }
}