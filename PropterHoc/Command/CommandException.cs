using System;
using Serilog;

namespace PropterHoc
{
    public class CommandException : Exception
    {
        public CommandException(CommandResult result, string message, Exception inner)
            : base(message, inner)
        {
            Result = result;
        }

        public CommandResult Result { get; }
    }
}