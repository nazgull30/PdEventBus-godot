using System;

namespace PdEventBus.Impls
{
	public class PdEventBus : IEventBus
	{
		public IEvent<T> GetStream<T>() where T : struct => Event<T>.Instance;

		public IEvent GetStream(Type type) => EventsHolder.Find(type);

		public void Fire<T>() where T : struct => Event<T>.Fire();

		public void Fire<T>(in T arg) where T : struct => Event<T>.Fire(arg);
	}
}