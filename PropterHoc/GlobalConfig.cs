namespace PropterHoc
{
    public class GlobalConfig
    {
        public const string DefaultRootSettingsFileName = "settings.json";

        public GlobalConfig(GlobalConfigDto dto)
        {
            SettingsRoot = dto.SettingsRoot
                .Replace("~", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile))
                .AndThen(Path.GetFullPath);

            RootSettingsFileName = dto.RootSettingsFileName
                .OrIfNullOrWhiteSpace(DefaultRootSettingsFileName);
        }

        public string SettingsRoot { get; }

        public string RootSettingsFileName { get; }

        public string RootSettigsPath => Path.Combine(SettingsRoot, RootSettingsFileName);
    }
}
