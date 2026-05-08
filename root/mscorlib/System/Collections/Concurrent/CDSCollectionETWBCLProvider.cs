using System;
using System.Diagnostics.Tracing;

namespace System.Collections.Concurrent
{
	// Token: 0x02000AAF RID: 2735
	[EventSource(Name = "System.Collections.Concurrent.ConcurrentCollectionsEventSource", Guid = "35167F8E-49B2-4b96-AB86-435B59336B5E")]
	internal sealed class CDSCollectionETWBCLProvider : EventSource
	{
		// Token: 0x060064F5 RID: 25845 RVA: 0x0007B6EC File Offset: 0x000798EC
		private CDSCollectionETWBCLProvider()
		{
		}

		// Token: 0x060064F6 RID: 25846 RVA: 0x0015808B File Offset: 0x0015628B
		[Event(1, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPushFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(1, spinCount);
			}
		}

		// Token: 0x060064F7 RID: 25847 RVA: 0x001580A0 File Offset: 0x001562A0
		[Event(2, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPopFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(2, spinCount);
			}
		}

		// Token: 0x060064F8 RID: 25848 RVA: 0x001580B5 File Offset: 0x001562B5
		[Event(3, Level = EventLevel.Warning)]
		public void ConcurrentDictionary_AcquiringAllLocks(int numOfBuckets)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(3, numOfBuckets);
			}
		}

		// Token: 0x060064F9 RID: 25849 RVA: 0x001580CA File Offset: 0x001562CA
		[Event(4, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryTakeSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(4);
			}
		}

		// Token: 0x060064FA RID: 25850 RVA: 0x001580DE File Offset: 0x001562DE
		[Event(5, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryPeekSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(5);
			}
		}

		// Token: 0x060064FB RID: 25851 RVA: 0x001580F2 File Offset: 0x001562F2
		// Note: this type is marked as 'beforefieldinit'.
		static CDSCollectionETWBCLProvider()
		{
		}

		// Token: 0x04003B63 RID: 15203
		public static CDSCollectionETWBCLProvider Log = new CDSCollectionETWBCLProvider();

		// Token: 0x04003B64 RID: 15204
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x04003B65 RID: 15205
		private const int CONCURRENTSTACK_FASTPUSHFAILED_ID = 1;

		// Token: 0x04003B66 RID: 15206
		private const int CONCURRENTSTACK_FASTPOPFAILED_ID = 2;

		// Token: 0x04003B67 RID: 15207
		private const int CONCURRENTDICTIONARY_ACQUIRINGALLLOCKS_ID = 3;

		// Token: 0x04003B68 RID: 15208
		private const int CONCURRENTBAG_TRYTAKESTEALS_ID = 4;

		// Token: 0x04003B69 RID: 15209
		private const int CONCURRENTBAG_TRYPEEKSTEALS_ID = 5;
	}
}
