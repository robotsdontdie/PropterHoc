using Newtonsoft.Json;

namespace PropterHoc
{
    public class ProfileSettingsDto
    {
        public ProfileSettingsDto()
        {
            Modules = new List<string>();
        }

        [JsonProperty("modules")]
        public List<string> Modules { get; }
    }
}
