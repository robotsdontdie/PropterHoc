using PropterHocPluginBase;

namespace PropterHoc
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MetaStepAttribute : Attribute, INamed
    {
        public MetaStepAttribute(string name, params string[] aliases)
        {
            Name = name;
            Aliases = aliases;
        }

        public string Name { get; }

        public string[] Aliases { get; }
    }
}