using System;
using System.Diagnostics;
using System.Threading;

namespace System.Buffers
{
	// Token: 0x02000B2C RID: 2860
	internal sealed class ConfigurableArrayPool<T> : ArrayPool<T>
	{
		// Token: 0x060068E2 RID: 26850 RVA: 0x00163848 File Offset: 0x00161A48
		internal ConfigurableArrayPool()
			: this(1048576, 50)
		{
		}

		// Token: 0x060068E3 RID: 26851 RVA: 0x00163858 File Offset: 0x00161A58
		internal ConfigurableArrayPool(int maxArrayLength, int maxArraysPerBucket)
		{
			if (maxArrayLength <= 0)
			{
				throw new ArgumentOutOfRangeException("maxArrayLength");
			}
			if (maxArraysPerBucket <= 0)
			{
				throw new ArgumentOutOfRangeException("maxArraysPerBucket");
			}
			if (maxArrayLength > 1073741824)
			{
				maxArrayLength = 1073741824;
			}
			else if (maxArrayLength < 16)
			{
				maxArrayLength = 16;
			}
			int id = this.Id;
			ConfigurableArrayPool<T>.Bucket[] array = new ConfigurableArrayPool<T>.Bucket[Utilities.SelectBucketIndex(maxArrayLength) + 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new ConfigurableArrayPool<T>.Bucket(Utilities.GetMaxSizeForBucket(i), maxArraysPerBucket, id);
			}
			this._buckets = array;
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x060068E4 RID: 26852 RVA: 0x001638DD File Offset: 0x00161ADD
		private int Id
		{
			get
			{
				return this.GetHashCode();
			}
		}

		// Token: 0x060068E5 RID: 26853 RVA: 0x001638E8 File Offset: 0x00161AE8
		public override T[] Rent(int minimumLength)
		{
			if (minimumLength < 0)
			{
				throw new ArgumentOutOfRangeException("minimumLength");
			}
			if (minimumLength == 0)
			{
				return Array.Empty<T>();
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			int num = Utilities.SelectBucketIndex(minimumLength);
			T[] array;
			if (num < this._buckets.Length)
			{
				int num2 = num;
				for (;;)
				{
					array = this._buckets[num2].Rent();
					if (array != null)
					{
						break;
					}
					if (++num2 >= this._buckets.Length || num2 == num + 2)
					{
						goto IL_0086;
					}
				}
				if (log.IsEnabled())
				{
					log.BufferRented(array.GetHashCode(), array.Length, this.Id, this._buckets[num2].Id);
				}
				return array;
				IL_0086:
				array = new T[this._buckets[num]._bufferLength];
			}
			else
			{
				array = new T[minimumLength];
			}
			if (log.IsEnabled())
			{
				int hashCode = array.GetHashCode();
				int num3 = -1;
				log.BufferRented(hashCode, array.Length, this.Id, num3);
				log.BufferAllocated(hashCode, array.Length, this.Id, num3, (num >= this._buckets.Length) ? ArrayPoolEventSource.BufferAllocatedReason.OverMaximumSize : ArrayPoolEventSource.BufferAllocatedReason.PoolExhausted);
			}
			return array;
		}

		// Token: 0x060068E6 RID: 26854 RVA: 0x001639E0 File Offset: 0x00161BE0
		public override void Return(T[] array, bool clearArray = false)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				return;
			}
			int num = Utilities.SelectBucketIndex(array.Length);
			if (num < this._buckets.Length)
			{
				if (clearArray)
				{
					Array.Clear(array, 0, array.Length);
				}
				this._buckets[num].Return(array);
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			if (log.IsEnabled())
			{
				log.BufferReturned(array.GetHashCode(), array.Length, this.Id);
			}
		}

		// Token: 0x04003C6A RID: 15466
		private const int DefaultMaxArrayLength = 1048576;

		// Token: 0x04003C6B RID: 15467
		private const int DefaultMaxNumberOfArraysPerBucket = 50;

		// Token: 0x04003C6C RID: 15468
		private readonly ConfigurableArrayPool<T>.Bucket[] _buckets;

		// Token: 0x02000B2D RID: 2861
		private sealed class Bucket
		{
			// Token: 0x060068E7 RID: 26855 RVA: 0x00163A52 File Offset: 0x00161C52
			internal Bucket(int bufferLength, int numberOfBuffers, int poolId)
			{
				this._lock = new SpinLock(Debugger.IsAttached);
				this._buffers = new T[numberOfBuffers][];
				this._bufferLength = bufferLength;
				this._poolId = poolId;
			}

			// Token: 0x17001249 RID: 4681
			// (get) Token: 0x060068E8 RID: 26856 RVA: 0x001638DD File Offset: 0x00161ADD
			internal int Id
			{
				get
				{
					return this.GetHashCode();
				}
			}

			// Token: 0x060068E9 RID: 26857 RVA: 0x00163A84 File Offset: 0x00161C84
			internal T[] Rent()
			{
				T[][] buffers = this._buffers;
				T[] array = null;
				bool flag = false;
				bool flag2 = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index < buffers.Length)
					{
						array = buffers[this._index];
						T[][] array2 = buffers;
						int index = this._index;
						this._index = index + 1;
						array2[index] = null;
						flag2 = array == null;
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
				if (flag2)
				{
					array = new T[this._bufferLength];
					ArrayPoolEventSource log = ArrayPoolEventSource.Log;
					if (log.IsEnabled())
					{
						log.BufferAllocated(array.GetHashCode(), this._bufferLength, this._poolId, this.Id, ArrayPoolEventSource.BufferAllocatedReason.Pooled);
					}
				}
				return array;
			}

			// Token: 0x060068EA RID: 26858 RVA: 0x00163B40 File Offset: 0x00161D40
			internal void Return(T[] array)
			{
				if (array.Length != this._bufferLength)
				{
					throw new ArgumentException("The buffer is not associated with this pool and may not be returned to it.", "array");
				}
				bool flag = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index != 0)
					{
						T[][] buffers = this._buffers;
						int num = this._index - 1;
						this._index = num;
						buffers[num] = array;
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
			}

			// Token: 0x04003C6D RID: 15469
			internal readonly int _bufferLength;

			// Token: 0x04003C6E RID: 15470
			private readonly T[][] _buffers;

			// Token: 0x04003C6F RID: 15471
			private readonly int _poolId;

			// Token: 0x04003C70 RID: 15472
			private SpinLock _lock;

			// Token: 0x04003C71 RID: 15473
			private int _index;
		}
	}
}
