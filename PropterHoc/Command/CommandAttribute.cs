using System;
using PropterHocPluginBase;

namespace PropterHoc
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CommandAttribute : Attribute, INamed
    {
        public CommandAttribute(string name, params string[] aliases)
        {
            Name = name;
            Aliases = aliases;
        }

        public string Name { get; }

        public string[] Aliases { get; }
    }
}