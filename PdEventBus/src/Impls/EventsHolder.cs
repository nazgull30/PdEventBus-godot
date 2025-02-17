using System;

namespace PdEventBus.Impls
{
	public static class EventsHolder
	{
		private static readonly object Locker = new();

		private static Holder _head;
		private static Holder _tail;

		public static void RegisterEvent<T>() where T : struct
		{
			lock (Locker)
			{
				var holder = new Holder(typeof(T), Event<T>.Instance);
				_head ??= holder;
				_tail = _tail?.Attach(holder) ?? holder;
			}
		}

		public static IEvent Find(Type eventType) => _head?.Find(eventType);

		private class Holder
		{
			public readonly Type EventType;
			public readonly IEvent Event;

			private Holder _next;

			public Holder(Type eventType, IEvent @event)
			{
				EventType = eventType;
				Event = @event;
			}

			public Holder Attach(Holder holder)
			{
				_next = holder;
				return holder;
			}

			public IEvent Find(Type type)
				=> EventType == type ? Event : _next?.Find(type);
		}
	}
}