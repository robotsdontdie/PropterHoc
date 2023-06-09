using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Features.Indexed;
using Newtonsoft.Json.Linq;
using PropterHocPluginBase;
using Serilog;

namespace PropterHoc
{
    public class SessionBuilder
    {
        private readonly Dictionary<string, Type> commands;
        private readonly Dictionary<string, Type> steps;
        private readonly Dictionary<string, Type> metaSteps;

        private readonly ContainerBuilder containerBuilder;

        public SessionBuilder(ContainerBuilder containerBuilder)
        {
            this.containerBuilder = containerBuilder;
            commands = new Dictionary<string, Type>();
            steps = new Dictionary<string, Type>();
            metaSteps = new Dictionary<string, Type>();
        }

        public SessionBuilder WithCommandsFromAssembly(Assembly assembly)
        {
            var commandTypes = FindSubtypesWithAttribute<Command, CommandAttribute>(assembly);
            foreach ((var type, var attribute) in commandTypes)
            {
                containerBuilder.RegisterCommand(type, attribute);
            }

            return this;
        }

        public SessionBuilder WithStepsFromAssembly(Assembly assembly)
        {
            var stepTypes = FindSubtypesWithAttribute<Step, StepAttribute>(assembly);
            foreach ((var type, var attribute) in stepTypes)
            {
                containerBuilder.RegisterStep(type, attribute);
            }

            var metaStepTypes = FindSubtypesWithAttribute<MetaStep, MetaStepAttribute>(assembly);
            foreach ((var type, var attribute) in metaStepTypes)
            {
                containerBuilder.RegisterMetaStep(type, attribute);
            }

            return this;
        }

        private IEnumerable<(Type Type, TAttribute Attribute)> FindSubtypesWithAttribute<TParentType, TAttribute>(Assembly assembly)
            where TAttribute : Attribute
        {
            foreach (var type in assembly.GetTypes())
            {
                if (!typeof(TParentType).IsAssignableFrom(type))
                {
                    continue;
                }

                if (type.IsAbstract)
                {
                    continue;
                }

                var attribute = type.GetCustomAttribute<TAttribute>();
                if (attribute is null)
                {
                    Log.Logger.Warning($"{typeof(TParentType)} type {type.Name} is missing attribute.");
                    continue;
                }

                if (type.IsAbstract)
                {
                    Log.Logger.Warning($"{typeof(TParentType)} type {type.Name} has an attribute but is abstract.");
                    continue;
                }

                yield return (type, attribute);
            }
        }
    }
}
