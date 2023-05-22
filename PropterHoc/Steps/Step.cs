namespace PropterHoc
{
    public abstract class Step
    {
        required public Session Session { get; init; }

        required public RunCommand Command { get; init; }

        required public StepConfig Config { get; init; }

        public abstract void Execute();
    }
}