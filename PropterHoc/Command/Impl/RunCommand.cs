using System;
using System.ComponentModel.Design;
using Autofac.Features.Indexed;
using Serilog;

namespace PropterHoc
{
    [Command("run", "r")]
    public class RunCommand : Command
    {
        public static readonly StepIdProvider DefaultStepIdProvider =
            () => DateTime.Now.GetSortableUniqueFilename();

        private readonly IIndex<string, Func<RunCommand, StepConfig, ILogger, Step>> stepIndex;
        private readonly IIndex<string, MetaStep> metaStepIndex;
        private readonly StepIdProvider stepIdProvider;

        public RunCommand(
            IIndex<string, Func<RunCommand, StepConfig, ILogger, Step>> stepIndex,
            IIndex<string, MetaStep> metaStepIndex,
            StepIdProvider stepIdProvider)
        {
            this.stepIndex = stepIndex;
            this.metaStepIndex = metaStepIndex;
            this.stepIdProvider = stepIdProvider;
        }

        public delegate string StepIdProvider();

        public override void Execute(string[] args)
        {
            var result = new CommandResult(Config);
            Logger.Information($"Command ID: {Config.CommandId}");

            LinkedList<string> remainingArgs = new LinkedList<string>(args);

            if (remainingArgs.First is null)
            {
                return;
            }

            if (remainingArgs.First.Value == "run" || remainingArgs.First.Value == "r")
            {
                remainingArgs.RemoveFirst();
            }

            var realSteps = new List<Step>();
            foreach (string arg in remainingArgs)
            {
                if (metaStepIndex.TryGetValue(arg, out var metaStep))
                {
                    foreach (string aliasOutput in metaStep.Steps)
                    {
                        remainingArgs.AddFirst(aliasOutput);
                    }
                }
                else if (stepIndex.TryGetValue(arg, out var step))
                {
                    string stepId = stepIdProvider();
                    var config = new StepConfig(stepId);
                    string stepLogFile = Path.Combine(
                        Session.Profile.GetStepDir(stepId),
                        "log.txt");
                    var logger = new LoggerConfiguration()
                        .WriteTo.Logger(Logger)
                        .WriteTo.File(stepLogFile)
                        .CreateLogger();
                    realSteps.Add(step(this, config, Logger));
                }
                else if (TryParseSetVariableStep(arg, out string varName, out string varValue))
                {
                    var config = new StepConfig(stepIdProvider());
                    var setVarStep = new SetVariableStep(varName, varValue)
                    {
                        Config = config,
                        Session = Session,
                        Command = this,
                    };

                    realSteps.Add(setVarStep);
                }
                else
                {
                    throw new InvalidOperationException($"Could not parse step {arg}");
                }
            }

            Logger.Information($"Executing run command:\n{string.Join(" ", realSteps)}");

            foreach (var realStep in realSteps)
            {
                realStep.Execute();
            }
        }

        private bool TryParseSetVariableStep(string input, out string name, out string value)
        {
            name = "";
            value = "";

            if (!input.StartsWith("--"))
            {
                return false;
            }

            int equalsIndex = input.IndexOf('=');
            if (equalsIndex < 0)
            {
                return false;
            }

            name = input[2..equalsIndex];
            value = input[(equalsIndex + 1)..];

            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return true;
        }
    }
}