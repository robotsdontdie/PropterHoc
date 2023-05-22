using System;

namespace PropterHoc
{
    [Command("state", "s")]
    public class StateCommand : Command
    {
        public override void Execute(string[] args)
        {
            foreach ((string name, var variable) in Session.State)
            {
                Logger.Information($"  {name} -> {variable}");
            }

            ResultBuilder.Success();
        }
    }
}