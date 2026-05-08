using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Concurrent
{
	// Token: 0x02000ABB RID: 2747
	public abstract class OrderablePartitioner<TSource> : Partitioner<TSource>
	{
		// Token: 0x06006584 RID: 25988 RVA: 0x00159FEC File Offset: 0x001581EC
		protected OrderablePartitioner(bool keysOrderedInEachPartition, bool keysOrderedAcrossPartitions, bool keysNormalized)
		{
			this.KeysOrderedInEachPartition = keysOrderedInEachPartition;
			this.KeysOrderedAcrossPartitions = keysOrderedAcrossPartitions;
			this.KeysNormalized = keysNormalized;
		}

		// Token: 0x06006585 RID: 25989
		public abstract IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount);

		// Token: 0x06006586 RID: 25990 RVA: 0x0015A009 File Offset: 0x00158209
		public virtual IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
		{
			throw new NotSupportedException("Dynamic partitions are not supported by this partitioner.");
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06006587 RID: 25991 RVA: 0x0015A015 File Offset: 0x00158215
		// (set) Token: 0x06006588 RID: 25992 RVA: 0x0015A01D File Offset: 0x0015821D
		public bool KeysOrderedInEachPartition
		{
			[CompilerGenerated]
			get
			{
				return this.<KeysOrderedInEachPartition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<KeysOrderedInEachPartition>k__BackingField = value;
			}
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06006589 RID: 25993 RVA: 0x0015A026 File Offset: 0x00158226
		// (set) Token: 0x0600658A RID: 25994 RVA: 0x0015A02E File Offset: 0x0015822E
		public bool KeysOrderedAcrossPartitions
		{
			[CompilerGenerated]
			get
			{
				return this.<KeysOrderedAcrossPartitions>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<KeysOrderedAcrossPartitions>k__BackingField = value;
			}
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x0600658B RID: 25995 RVA: 0x0015A037 File Offset: 0x00158237
		// (set) Token: 0x0600658C RID: 25996 RVA: 0x0015A03F File Offset: 0x0015823F
		public bool KeysNormalized
		{
			[CompilerGenerated]
			get
			{
				return this.<KeysNormalized>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<KeysNormalized>k__BackingField = value;
			}
		}

		// Token: 0x0600658D RID: 25997 RVA: 0x0015A048 File Offset: 0x00158248
		public override IList<IEnumerator<TSource>> GetPartitions(int partitionCount)
		{
			IList<IEnumerator<KeyValuePair<long, TSource>>> orderablePartitions = this.GetOrderablePartitions(partitionCount);
			if (orderablePartitions.Count != partitionCount)
			{
				throw new InvalidOperationException("GetPartitions returned an incorrect number of partitions.");
			}
			IEnumerator<TSource>[] array = new IEnumerator<TSource>[partitionCount];
			for (int i = 0; i < partitionCount; i++)
			{
				array[i] = new OrderablePartitioner<TSource>.EnumeratorDropIndices(orderablePartitions[i]);
			}
			return array;
		}

		// Token: 0x0600658E RID: 25998 RVA: 0x0015A094 File Offset: 0x00158294
		public override IEnumerable<TSource> GetDynamicPartitions()
		{
			return new OrderablePartitioner<TSource>.EnumerableDropIndices(this.GetOrderableDynamicPartitions());
		}

		// Token: 0x04003B8C RID: 15244
		[CompilerGenerated]
		private bool <KeysOrderedInEachPartition>k__BackingField;

		// Token: 0x04003B8D RID: 15245
		[CompilerGenerated]
		private bool <KeysOrderedAcrossPartitions>k__BackingField;

		// Token: 0x04003B8E RID: 15246
		[CompilerGenerated]
		private bool <KeysNormalized>k__BackingField;

		// Token: 0x02000ABC RID: 2748
		private class EnumerableDropIndices : IEnumerable<TSource>, IEnumerable, IDisposable
		{
			// Token: 0x0600658F RID: 25999 RVA: 0x0015A0A1 File Offset: 0x001582A1
			public EnumerableDropIndices(IEnumerable<KeyValuePair<long, TSource>> source)
			{
				this._source = source;
			}

			// Token: 0x06006590 RID: 26000 RVA: 0x0015A0B0 File Offset: 0x001582B0
			public IEnumerator<TSource> GetEnumerator()
			{
				return new OrderablePartitioner<TSource>.EnumeratorDropIndices(this._source.GetEnumerator());
			}

			// Token: 0x06006591 RID: 26001 RVA: 0x0015A0C2 File Offset: 0x001582C2
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06006592 RID: 26002 RVA: 0x0015A0CC File Offset: 0x001582CC
			public void Dispose()
			{
				IDisposable disposable = this._source as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x04003B8F RID: 15247
			private readonly IEnumerable<KeyValuePair<long, TSource>> _source;
		}

		// Token: 0x02000ABD RID: 2749
		private class EnumeratorDropIndices : IEnumerator<TSource>, IDisposable, IEnumerator
		{
			// Token: 0x06006593 RID: 26003 RVA: 0x0015A0EE File Offset: 0x001582EE
			public EnumeratorDropIndices(IEnumerator<KeyValuePair<long, TSource>> source)
			{
				this._source = source;
			}

			// Token: 0x06006594 RID: 26004 RVA: 0x0015A0FD File Offset: 0x001582FD
			public bool MoveNext()
			{
				return this._source.MoveNext();
			}

			// Token: 0x17001193 RID: 4499
			// (get) Token: 0x06006595 RID: 26005 RVA: 0x0015A10C File Offset: 0x0015830C
			public TSource Current
			{
				get
				{
					KeyValuePair<long, TSource> keyValuePair = this._source.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x17001194 RID: 4500
			// (get) Token: 0x06006596 RID: 26006 RVA: 0x0015A12C File Offset: 0x0015832C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06006597 RID: 26007 RVA: 0x0015A139 File Offset: 0x00158339
			public void Dispose()
			{
				this._source.Dispose();
			}

			// Token: 0x06006598 RID: 26008 RVA: 0x0015A146 File Offset: 0x00158346
			public void Reset()
			{
				this._source.Reset();
			}

			// Token: 0x04003B90 RID: 15248
			private readonly IEnumerator<KeyValuePair<long, TSource>> _source;
		}
	}
}
