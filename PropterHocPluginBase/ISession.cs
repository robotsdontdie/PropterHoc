using System;
namespace PropterHocPluginBase
{
	public interface ISession
	{
        IState State { get; }
    }
}

