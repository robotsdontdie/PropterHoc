using Newtonsoft.Json;

namespace PropterHoc
{
    /// <summary>
    /// The DTO for the root settings file.
    /// </summary>
    public class RootSettingsDto
    {
        /// <summary>
        /// Gets or sets the active profile.
        /// </summary>
        [JsonProperty("activeProfile")]
        [JsonRequired]
        public string ActiveProfile { get; set; } = "";
    }
}
