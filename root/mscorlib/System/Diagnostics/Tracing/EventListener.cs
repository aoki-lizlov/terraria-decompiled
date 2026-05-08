using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A4D RID: 2637
	public class EventListener : IDisposable
	{
		// Token: 0x060060CF RID: 24783 RVA: 0x000025BE File Offset: 0x000007BE
		public EventListener()
		{
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x0000408A File Offset: 0x0000228A
		public static int EventSourceIndex(EventSource eventSource)
		{
			return 0;
		}

		// Token: 0x060060D1 RID: 24785 RVA: 0x00004088 File Offset: 0x00002288
		public void EnableEvents(EventSource eventSource, EventLevel level)
		{
		}

		// Token: 0x060060D2 RID: 24786 RVA: 0x00004088 File Offset: 0x00002288
		public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword)
		{
		}

		// Token: 0x060060D3 RID: 24787 RVA: 0x00004088 File Offset: 0x00002288
		public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> arguments)
		{
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x00004088 File Offset: 0x00002288
		public void DisableEvents(EventSource eventSource)
		{
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x00004088 File Offset: 0x00002288
		protected internal virtual void OnEventSourceCreated(EventSource eventSource)
		{
		}

		// Token: 0x060060D6 RID: 24790 RVA: 0x00004088 File Offset: 0x00002288
		protected internal virtual void OnEventWritten(EventWrittenEventArgs eventData)
		{
		}

		// Token: 0x060060D7 RID: 24791 RVA: 0x00004088 File Offset: 0x00002288
		public virtual void Dispose()
		{
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060060D8 RID: 24792 RVA: 0x0014D480 File Offset: 0x0014B680
		// (remove) Token: 0x060060D9 RID: 24793 RVA: 0x0014D4B8 File Offset: 0x0014B6B8
		public event EventHandler<EventSourceCreatedEventArgs> EventSourceCreated
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventSourceCreatedEventArgs> eventHandler = this.EventSourceCreated;
				EventHandler<EventSourceCreatedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventSourceCreatedEventArgs> eventHandler3 = (EventHandler<EventSourceCreatedEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventSourceCreatedEventArgs>>(ref this.EventSourceCreated, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventSourceCreatedEventArgs> eventHandler = this.EventSourceCreated;
				EventHandler<EventSourceCreatedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventSourceCreatedEventArgs> eventHandler3 = (EventHandler<EventSourceCreatedEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventSourceCreatedEventArgs>>(ref this.EventSourceCreated, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060060DA RID: 24794 RVA: 0x0014D4F0 File Offset: 0x0014B6F0
		// (remove) Token: 0x060060DB RID: 24795 RVA: 0x0014D528 File Offset: 0x0014B728
		public event EventHandler<EventWrittenEventArgs> EventWritten
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventWrittenEventArgs> eventHandler = this.EventWritten;
				EventHandler<EventWrittenEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventWrittenEventArgs> eventHandler3 = (EventHandler<EventWrittenEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventWrittenEventArgs>>(ref this.EventWritten, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventWrittenEventArgs> eventHandler = this.EventWritten;
				EventHandler<EventWrittenEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventWrittenEventArgs> eventHandler3 = (EventHandler<EventWrittenEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventWrittenEventArgs>>(ref this.EventWritten, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x04003A54 RID: 14932
		[CompilerGenerated]
		private EventHandler<EventSourceCreatedEventArgs> EventSourceCreated;

		// Token: 0x04003A55 RID: 14933
		[CompilerGenerated]
		private EventHandler<EventWrittenEventArgs> EventWritten;
	}
}
