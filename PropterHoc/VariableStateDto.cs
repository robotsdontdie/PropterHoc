using Newtonsoft.Json;

namespace PropterHoc
{
    public class VariableStateDto
    {
        public VariableStateDto()
        {
            Variables = new Dictionary<string, string>();
        }

        [JsonProperty("variables")]
        public Dictionary<string, string> Variables { get; }
    }
}
