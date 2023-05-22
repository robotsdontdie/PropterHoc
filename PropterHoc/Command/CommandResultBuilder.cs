using System;
using System.Diagnostics.CodeAnalysis;
using Serilog;

namespace PropterHoc
{
    public class CommandResultBuilder
    {
        public CommandResultBuilder(CommandConfig config)
        {
            Config = config;
            State = CommandState.NotStarted;
            Status = CommandStatus.None;
        }

        public CommandConfig Config { get; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public CommandState State { get; set; }

        public CommandStatus Status { get; set; }

        public CommandResultBuilder Start()
        {
            StartTime = DateTime.Now;
            State = CommandState.Started;
            return this;
        }

        public CommandResult Build()
        {
            return new CommandResult(Config)
            {
                StartTime = StartTime,
                EndTime = EndTime,
                State = State,
                Status = Status,
            };
        }

        public CommandResultBuilder Success()
        {
            EndTime = DateTime.Now;
            State = CommandState.Completed;
            Status = CommandStatus.Success;
            return this;
        }
    }
}