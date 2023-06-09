using System;

namespace PropterHocPluginBase
{
    public abstract class Step : IStep
    {
        required public ISession Session { get; init; }

        required public IRunCommand Command { get; init; }

        required public StepConfig Config { get; init; }

        public abstract void Execute();
    }
}

