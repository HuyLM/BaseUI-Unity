using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Ftech.Lib.Common.UnityInspector.Editor {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class PopupAttribute : PropertyAttribute {
        public readonly string[] options;

        public PopupAttribute(string[] options) {
            this.options = options;
        }

        public PopupAttribute(IEnumerable<string> options) {
            this.options = options.ToArray();
        }
    }
}