using System;

namespace PdEventBus
{
	public interface IEvent<out T>
		where T : struct
	{
		IDisposable Subscribe(Action<T> callback);
	}

	public interface IEvent
	{
		IDisposable Subscribe(Action<object> observer);
	}
}