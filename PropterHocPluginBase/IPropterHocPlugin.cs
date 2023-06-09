using System;
using Autofac;

namespace PropterHocPluginBase
{
	public interface IPropterHocPlugin
	{
        void Init(ContainerBuilder builder);
    }
}

