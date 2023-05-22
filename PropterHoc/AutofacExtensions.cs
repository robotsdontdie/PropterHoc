using System;
using System.Reflection;
using Autofac;
using Autofac.Core.Registration;
using Serilog;

namespace PropterHoc
{
    public static class AutofacExtensions
    {
#pragma warning disable SA1311 // Static readonly fields should begin with upper-case letter
        private static readonly Dictionary<string, Type> registeredCommands;
        private static readonly Dictionary<string, Type> registeredSteps;
        private static readonly Dictionary<string, Type> registeredMetaSteps;
#pragma warning restore SA1311 // Static readonly fields should begin with upper-case letter

        static AutofacExtensions()
        {
            registeredCommands = new Dictionary<string, Type>();
            registeredSteps = new Dictionary<string, Type>();
            registeredMetaSteps = new Dictionary<string, Type>();
        }

        public static void RegisterCommand(this ContainerBuilder containerBuilder, Type commandType, CommandAttribute commandAttribute)
        {
            RegisterInternal<Command, CommandAttribute>(
                containerBuilder,
                commandType,
                commandAttribute,
                registeredCommands);
        }

        public static void RegisterStep(this ContainerBuilder containerBuilder, Type stepType, StepAttribute stepAttribute)
        {
            RegisterInternal<Step, StepAttribute>(
                containerBuilder,
                stepType,
                stepAttribute,
                registeredSteps);
        }

        public static void RegisterMetaStep(this ContainerBuilder containerBuilder, Type metaStepType, MetaStepAttribute metaStepAttribute)
        {
            RegisterInternal<MetaStep, MetaStepAttribute>(
                containerBuilder,
                metaStepType,
                metaStepAttribute,
                registeredMetaSteps);
        }

        private static void RegisterInternal<TBaseType, TAttribute>(
            this ContainerBuilder containerBuilder,
            Type type,
            TAttribute attribute,
            Dictionary<string, Type> existingRegistrations)
            where TAttribute : Attribute, INamed
        {
            var names = attribute.Aliases.Prepend(attribute.Name);
            foreach (string name in names)
            {
                if (existingRegistrations.TryGetValue(name, out var existing))
                {
                    Log.Logger.Warning($"Name '{name}' for {typeof(TBaseType).Name} type {type.Name} is already used by type {existing.Name}.");
                }
                else
                {
                    containerBuilder.RegisterType(type)
                        .Named(name, typeof(TBaseType))
                        .WithParameter(new TypedParameter(typeof(TAttribute), attribute))
                        .InstancePerDependency();
                }
            }
        }
    }
}
