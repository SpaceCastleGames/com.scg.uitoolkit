using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scg.uitoolkit.runtime
{


    public class TextDropdownAttribute : PropertyAttribute
    {

        private string methodName;

        public TextDropdownAttribute(string methodName)
        {
            this.MethodName = methodName;
        }

        public string MethodName { get => methodName; set => methodName = value; }
    }
}