using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000B0A RID: 2826
	internal sealed class QueueDebugView<T>
	{
		// Token: 0x060067FE RID: 26622 RVA: 0x001609D6 File Offset: 0x0015EBD6
		public QueueDebugView(Queue<T> queue)
		{
			if (queue == null)
			{
				throw new ArgumentNullException("queue");
			}
			this._queue = queue;
		}

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x060067FF RID: 26623 RVA: 0x001609F3 File Offset: 0x0015EBF3
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._queue.ToArray();
			}
		}

		// Token: 0x04003C48 RID: 15432
		private readonly Queue<T> _queue;
	}
}
