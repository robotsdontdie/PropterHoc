using System.Diagnostics.CodeAnalysis;

namespace PropterHoc
{
    public record CommandResult
    {
        [SetsRequiredMembers]
        public CommandResult(CommandConfig config)
            : this()
        {
            Config = config;
        }

        public CommandResult()
        {
            StartTime = default;
            EndTime = default;
            State = CommandState.NotStarted;
            Status = CommandStatus.None;
        }

        required public CommandConfig Config { get; init; }

        public DateTime? StartTime { get; init; }

        public DateTime? EndTime { get; init; }

        public CommandState State { get; init; }

        public CommandStatus Status { get; init; }

        public CommandResult Success()
        {
            return this with
            {
                EndTime = DateTime.Now,
                State = CommandState.Completed,
                Status = Combine(CommandStatus.Success),
            };
        }

        private CommandStatus Combine(CommandStatus status)
        {
            return status > Status
                ? status
                : Status;
        }
    }
}