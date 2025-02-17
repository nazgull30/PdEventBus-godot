using System;

namespace PdEventBus.Impls
{
	public static class EventExtensions
	{
		public static IEvent<T> Where<T>(this IEvent<T> @event, Func<T, bool> filter)
			where T : struct
			=> new EventWhere<T>(@event, filter);

		private class EventWhere<T> : IEvent<T> where T : struct
		{
			private readonly IEvent<T> _event;
			private readonly Func<T, bool> _filter;

			public EventWhere(IEvent<T> @event, Func<T, bool> filter)
			{
				_event = @event;
				_filter = filter;
			}

			public IDisposable Subscribe(Action<T> callback) => _event.Subscribe(arg =>
			{
				if (!_filter(arg))
					return;

				callback(arg);
			});
		}
	}
}