using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Threading.Tasks
{
	// Token: 0x0200031D RID: 797
	internal interface IProducerConsumerQueue<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06002310 RID: 8976
		void Enqueue(T item);

		// Token: 0x06002311 RID: 8977
		bool TryDequeue(out T result);

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06002312 RID: 8978
		bool IsEmpty { get; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06002313 RID: 8979
		int Count { get; }

		// Token: 0x06002314 RID: 8980
		int GetCountSafe(object syncObj);
	}
}
