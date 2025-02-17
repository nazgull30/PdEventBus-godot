using System;

namespace PdEventBus.Impls
{
	public class Event<T> : IEvent<T>, IEvent, IDisposable
		where T : struct
	{
		public static readonly Event<T> Instance = new(_ =>
		{
		});

		private static event Action<T> Observers;

		private readonly Action<T> _observer;

		private Event(Action<T> observer)
		{
			_observer = observer;
			Observers += observer;
		}

		public IDisposable Subscribe(Action<T> callback) => new Event<T>(callback);

		public void Dispose() => Observers -= _observer;

		IDisposable IEvent.Subscribe(Action<object> callback) => Subscribe(arg => callback(arg));

		public static void Fire() => Observers(default);

		public static void Fire(in T arg) => Observers(arg);
	}
}