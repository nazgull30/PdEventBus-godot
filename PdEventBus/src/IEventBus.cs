using System;

namespace PdEventBus
{
	public interface IEventBus
	{
		IEvent<T> GetStream<T>() where T : struct;

		IEvent GetStream(Type type);

		void Fire<T>() where T : struct;

		void Fire<T>(in T arg) where T : struct;
	}
}