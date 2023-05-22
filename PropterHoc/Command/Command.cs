using System;
using Serilog;

namespace PropterHoc
{
    /// <summary>
    /// An instance of a command being run.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Gets the session this command is running in.
        /// </summary>
        required public Session Session { get; init; }

        required public CommandConfig Config { get; init; }

        required public CommandResultBuilder ResultBuilder { protected get; init; }

        public CommandResult Result => ResultBuilder.Build();

        required public ILogger Logger { get; init; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments to the command, including the command name itself.</param>
        public abstract void Execute(string[] args);
    }
}