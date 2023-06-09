using PropterHocPluginBase;

namespace PropterHoc
{
    public class SetVariableStep : Step
    {
        private readonly string name;
        private readonly string value;

        public SetVariableStep(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public override void Execute()
        {
            Session.State[name] = value;
        }
    }
}