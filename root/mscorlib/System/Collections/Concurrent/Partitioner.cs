using System;
using System.Collections.Generic;

namespace System.Collections.Concurrent
{
	// Token: 0x02000ABE RID: 2750
	public abstract class Partitioner<TSource>
	{
		// Token: 0x06006599 RID: 26009
		public abstract IList<IEnumerator<TSource>> GetPartitions(int partitionCount);

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x0600659A RID: 26010 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool SupportsDynamicPartitions
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600659B RID: 26011 RVA: 0x0015A009 File Offset: 0x00158209
		public virtual IEnumerable<TSource> GetDynamicPartitions()
		{
			throw new NotSupportedException("Dynamic partitions are not supported by this partitioner.");
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x000025BE File Offset: 0x000007BE
		protected Partitioner()
		{
		}
	}
}
