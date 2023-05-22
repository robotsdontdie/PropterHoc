using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace PropterHoc
{
    public static class Unreal
    {
        public abstract class StateWrapper
        {
            private readonly PersistedState state;

            protected StateWrapper(PersistedState state)
            {
                this.state = state;
            }

            public string Get(string name) => state[name];

            public bool GetBool(string name) => string.Equals(state[name], "true", StringComparison.OrdinalIgnoreCase);

            public string Set(string name, string value) => state[name] = value;

            protected StateVariable CreateVariable(string name, string description)
                => new StateVariable(state, name, description);
        }

        public class StateVariable
        {
            private readonly PersistedState state;

            public StateVariable(PersistedState state, string name, string description)
            {
                this.state = state;
                Name = name;
                Description = description;
            }

            public string Name { get; }

            public string Description { get; }

            public string Value
            {
                get => state[Name];
                set => state[Name] = value;
            }
        }

        public class UnrealState : StateWrapper
        {
            public UnrealState(PersistedState state)
                : base(state)
            {
                Platform = CreateVariable("platform", "The Unreal platform.");
                Configuration = CreateVariable("configuration", "The Unreal configuration");
                Target = CreateVariable("target", "The Unreal target.");
            }

            public StateVariable Platform { get; }

            public StateVariable Configuration { get; }

            public StateVariable Target { get; }
        }

        public class UnrealModule
        {
            public void Setup()
            {
            }
        }

        public abstract class UnrealStep : Step
        {
        }

        [Step("build")]
        public class UnrealBuild : UnrealStep
        {
            public override void Execute()
            {
                Console.WriteLine($"Building in step {Config.StepId}.");
            }
        }
    }
}