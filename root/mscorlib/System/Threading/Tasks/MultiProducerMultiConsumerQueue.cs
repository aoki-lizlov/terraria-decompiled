using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading.Tasks
{
	// Token: 0x0200031E RID: 798
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class MultiProducerMultiConsumerQueue<T> : ConcurrentQueue<T>, IProducerConsumerQueue<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06002315 RID: 8981 RVA: 0x0007EA76 File Offset: 0x0007CC76
		void IProducerConsumerQueue<T>.Enqueue(T item)
		{
			base.Enqueue(item);
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x0007EA7F File Offset: 0x0007CC7F
		bool IProducerConsumerQueue<T>.TryDequeue(out T result)
		{
			return base.TryDequeue(out result);
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x0007EA88 File Offset: 0x0007CC88
		bool IProducerConsumerQueue<T>.IsEmpty
		{
			get
			{
				return base.IsEmpty;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x0007EA90 File Offset: 0x0007CC90
		int IProducerConsumerQueue<T>.Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x0007EA90 File Offset: 0x0007CC90
		int IProducerConsumerQueue<T>.GetCountSafe(object syncObj)
		{
			return base.Count;
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x0007EA98 File Offset: 0x0007CC98
		public MultiProducerMultiConsumerQueue()
		{
		}
	}
}
