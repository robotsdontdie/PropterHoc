namespace PropterHoc
{
    public class ProfileSettings
    {
        public ProfileSettings(RootSettings rootSettings, ProfileSettingsDto dto)
        {
            RootSettings = rootSettings;
            ModuleNames = dto.Modules.ToReadOnlyList();
        }

        public RootSettings RootSettings { get; }

        public IReadOnlyList<string> ModuleNames { get; }
    }
}
