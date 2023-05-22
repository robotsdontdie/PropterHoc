using System.Collections;
using Newtonsoft.Json;

namespace PropterHoc
{
    public class PersistedState : IState
    {
        private readonly string statePath;
        private readonly VariableStateDto dto;

        public PersistedState(string statePath)
        {
            this.statePath = statePath.CheckNotNullOrWhiteSpace();
            dto = JsonConvert.DeserializeObject<VariableStateDto>(
                File.ReadAllText(statePath))
                ?? throw new JsonSerializationException("variable state cannot be null.");
        }

        public IEnumerable<KeyValuePair<string, string>> Variables => dto.Variables;

        public string this[string key]
        {
            get
            {
                return dto.Variables[key];
            }

            set
            {
                dto.Variables[key] = value;
                File.WriteAllText(
                    statePath,
                    JsonConvert.SerializeObject(dto, Formatting.Indented));
            }
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return dto.Variables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dto.Variables.GetEnumerator();
        }
    }
}