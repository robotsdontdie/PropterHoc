using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Features.Indexed;
using Newtonsoft.Json.Linq;
using Serilog;

namespace PropterHoc
{
    public class CommandConfig
    {
        public CommandConfig(string commandId, string[] args)
        {
            CommandId = commandId;
            Args = args;
        }

        public string CommandId { get; }

        public string[] Args { get; }
    }
}
