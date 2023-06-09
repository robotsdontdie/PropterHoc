using System;
namespace PropterHocPluginBase
{
    public interface INamed
    {
        public string Name { get; }

        public string[] Aliases { get; }
    }
}

