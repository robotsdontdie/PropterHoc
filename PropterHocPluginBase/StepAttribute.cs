using System;
using System.Xml.Linq;

namespace PropterHocPluginBase
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]

    public class StepAttribute : Attribute, INamed
    {
        public StepAttribute(string name, params string[] aliases)
        {
            Name = name;
            Aliases = aliases;
        }

        public string Name { get; }

        public string[] Aliases { get; }
    }
}

