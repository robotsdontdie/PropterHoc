using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Features.Indexed;
using Newtonsoft.Json.Linq;
using PropterHocPluginBase;
using Serilog;

namespace PropterHoc
{
    public class Session : ISession
    {
        public static readonly CommandIdProvider DefaultCommandIdProvider =
            () => DateTime.Now.GetSortableUniqueFilename();

        private readonly IIndex<string, Func<CommandConfig, CommandResultBuilder, ILogger, Command>> commandIndex;
        private readonly CommandIdProvider commandIdProvider;

        public Session(
            string sessionId,
            Profile profile,
            IIndex<string, Func<CommandConfig, CommandResultBuilder, ILogger, Command>> commandIndex,
            CommandIdProvider commandIdProvider,
            ILogger logger)
        {
            SessionId = sessionId;
            Profile = profile;
            State = Profile.PersistedState;
            this.commandIndex = commandIndex;
            this.commandIdProvider = commandIdProvider;
            Logger = logger;
        }

        public delegate string CommandIdProvider();

        public string SessionId { get; }

        public string SessionDir => Path.Combine(Profile.ProfileDir, SessionId);

        public Profile Profile { get; }

        public IState State { get; }

        public ILogger Logger { get; }

        public bool ExecuteCommand(string[] args)
        {
            if (args.Length == 0)
            {
                return false;
            }

            string commandName = args[0];

            if (commandName == "q"
                || commandName == "quit")
            {
                return true;
            }

            if (commandIndex.TryGetValue(commandName, out var command))
            {
                string commandId = commandIdProvider();
                var config = new CommandConfig(commandId, args);
                var resultBuilder = new CommandResultBuilder(config);

                string commandLogFile = Path.Combine(
                    Profile.GetCommandDir(commandId),
                    "log.txt");
                var logger = new LoggerConfiguration()
                    .WriteTo.Logger(Logger)
                    .WriteTo.File(commandLogFile)
                    .CreateLogger();
                command(config, resultBuilder, logger).Execute(args);
            }
            else
            {
                Logger.Error($"Unknown command {commandName}.");
            }

            return false;
        }
    }
}
