using System;
namespace PropterHocPluginBase
{
    public interface IState : IEnumerable<KeyValuePair<string, string>>
    {
        public string this[string key] { get; set; }
    }
}

