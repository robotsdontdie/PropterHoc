using System.Reflection;
using Autofac;
using Newtonsoft.Json;
using Serilog;

namespace PropterHoc
{
    public class Initter
    {
        public const string GlobalConfigFilename = ".phconfig";

        public Session Init()
        {
            string configPath = FindConfigFile();

            var configDto = JsonConvert.DeserializeObject<GlobalConfigDto>(File.ReadAllText(configPath))
                ?? throw new Exception("Config cannot be null.");
            var config = new GlobalConfig(configDto);

            var rootSettingsPath = config.RootSettigsPath;
            var rootSettingsDto = JsonConvert.DeserializeObject<RootSettingsDto>(File.ReadAllText(rootSettingsPath))
                ?? throw new Exception("Session settings cannot be null.");
            var rootSettings = new RootSettings(config, rootSettingsDto);

            var profileName = rootSettings.ActiveProfileName;
            var profileDir = rootSettings.GetProfileSettingsDir(profileName);
            var profileSettingsPath = Path.Combine(profileDir, "profile.json");
            var statePath = Path.Combine(profileDir, "state.json");

            var profileSettingsDto = JsonConvert.DeserializeObject<ProfileSettingsDto>(File.ReadAllText(profileSettingsPath))
                ?? throw new Exception("Profile settings cannot be null.");
            var profileSettings = new ProfileSettings(rootSettings, profileSettingsDto);

            var state = new PersistedState(statePath);

            var profile = new Profile(profileName, profileSettings, state);

            string sessionId = DateTime.Now.GetSortableUniqueFilename();

            ContainerBuilder builder = new ContainerBuilder();
            new SessionBuilder(builder)
                .WithCommandsFromAssembly(Assembly.GetExecutingAssembly())
                .WithStepsFromAssembly(Assembly.GetExecutingAssembly());

            builder.RegisterType<Session>()
                .WithParameter("sessionId", sessionId);
            builder.RegisterInstance<Profile>(profile);

            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            builder.RegisterInstance(log).As<ILogger>();

            builder.RegisterInstance(Session.DefaultCommandIdProvider).As<Session.CommandIdProvider>();
            builder.RegisterInstance(RunCommand.DefaultStepIdProvider).As<RunCommand.StepIdProvider>();

            return builder.Build().Resolve<Session>();
        }

        private string FindConfigFile()
        {
            string? directory = Environment.CurrentDirectory;

            while (!string.IsNullOrWhiteSpace(directory))
            {
                string candidate = Path.Combine(directory, GlobalConfigFilename);
                if (File.Exists(candidate))
                {
                    return candidate;
                }

                directory = Path.GetDirectoryName(directory);
            }

            throw new Exception("Could not locate global config file");
        }
    }
}
