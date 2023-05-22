using Newtonsoft.Json;

namespace PropterHoc
{
    public class GlobalConfigDto
    {
        [JsonProperty("settingsRoot")]
        [JsonRequired]
        public string SettingsRoot { get; set; } = "";

        [JsonProperty("rootSettingsFileName")]
        public string RootSettingsFileName { get; set; } = "";
    }
}
