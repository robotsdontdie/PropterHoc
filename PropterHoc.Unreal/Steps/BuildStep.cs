using System;
using PropterHocPluginBase;

namespace PropterHoc.Unreal
{
    [Step("build")]
    public class UnrealBuild : Step
    {
        public override void Execute()
        {
            Console.WriteLine($"Building in step {Config.StepId}.");
        }
    }
}

