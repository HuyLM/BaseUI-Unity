using System;
using UnityEngine;

namespace GameSystem.Common.UnityInspector {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ItemFieldAttribute : PropertyAttribute {

    }
}