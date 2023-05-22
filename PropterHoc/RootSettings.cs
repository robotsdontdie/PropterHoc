namespace PropterHoc
{
    public class RootSettings
    {
        public RootSettings(GlobalConfig globalConfig, RootSettingsDto dto)
        {
            GlobalConfig = globalConfig;
            ActiveProfileName = dto.ActiveProfile;
        }

        public GlobalConfig GlobalConfig { get; }

        public string RootDir => GlobalConfig.SettingsRoot;

        public string ProfilesDir => Path.Combine(RootDir, "profiles");

        public string ActiveProfileName { get; }

        public string ActiveProfileSettingsDir => GetProfileSettingsDir(ActiveProfileName);

        public string GetProfileSettingsDir(string profileName) => Path.Combine(ProfilesDir, ActiveProfileName);
    }
}
