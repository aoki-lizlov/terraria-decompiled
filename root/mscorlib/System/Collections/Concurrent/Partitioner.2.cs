using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Concurrent
{
	// Token: 0x02000AC0 RID: 2752
	public static class Partitioner
	{
		// Token: 0x0600659D RID: 26013 RVA: 0x0015A153 File Offset: 0x00158353
		public static OrderablePartitioner<TSource> Create<TSource>(IList<TSource> list, bool loadBalance)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (loadBalance)
			{
				return new Partitioner.DynamicPartitionerForIList<TSource>(list);
			}
			return new Partitioner.StaticIndexRangePartitionerForIList<TSource>(list);
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x0015A173 File Offset: 0x00158373
		public static OrderablePartitioner<TSource> Create<TSource>(TSource[] array, bool loadBalance)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (loadBalance)
			{
				return new Partitioner.DynamicPartitionerForArray<TSource>(array);
			}
			return new Partitioner.StaticIndexRangePartitionerForArray<TSource>(array);
		}

		// Token: 0x0600659F RID: 26015 RVA: 0x0015A193 File Offset: 0x00158393
		public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source)
		{
			return Partitioner.Create<TSource>(source, EnumerablePartitionerOptions.None);
		}

		// Token: 0x060065A0 RID: 26016 RVA: 0x0015A19C File Offset: 0x0015839C
		public static OrderablePartitioner<TSource> Create<TSource>(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((partitionerOptions & ~EnumerablePartitionerOptions.NoBuffering) != EnumerablePartitionerOptions.None)
			{
				throw new ArgumentOutOfRangeException("partitionerOptions");
			}
			return new Partitioner.DynamicPartitionerForIEnumerable<TSource>(source, partitionerOptions);
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x0015A1C4 File Offset: 0x001583C4
		public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive)
		{
			int num = 3;
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			long num2 = (toExclusive - fromInclusive) / (long)(PlatformHelper.ProcessorCount * num);
			if (num2 == 0L)
			{
				num2 = 1L;
			}
			return Partitioner.Create<Tuple<long, long>>(Partitioner.CreateRanges(fromInclusive, toExclusive, num2), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x0015A203 File Offset: 0x00158403
		public static OrderablePartitioner<Tuple<long, long>> Create(long fromInclusive, long toExclusive, long rangeSize)
		{
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			if (rangeSize <= 0L)
			{
				throw new ArgumentOutOfRangeException("rangeSize");
			}
			return Partitioner.Create<Tuple<long, long>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x060065A3 RID: 26019 RVA: 0x0015A232 File Offset: 0x00158432
		private static IEnumerable<Tuple<long, long>> CreateRanges(long fromInclusive, long toExclusive, long rangeSize)
		{
			bool shouldQuit = false;
			long i = fromInclusive;
			while (i < toExclusive && !shouldQuit)
			{
				long num = i;
				long num2;
				try
				{
					num2 = checked(i + rangeSize);
				}
				catch (OverflowException)
				{
					num2 = toExclusive;
					shouldQuit = true;
				}
				if (num2 > toExclusive)
				{
					num2 = toExclusive;
				}
				yield return new Tuple<long, long>(num, num2);
				i += rangeSize;
			}
			yield break;
		}

		// Token: 0x060065A4 RID: 26020 RVA: 0x0015A250 File Offset: 0x00158450
		public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive)
		{
			int num = 3;
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			int num2 = (toExclusive - fromInclusive) / (PlatformHelper.ProcessorCount * num);
			if (num2 == 0)
			{
				num2 = 1;
			}
			return Partitioner.Create<Tuple<int, int>>(Partitioner.CreateRanges(fromInclusive, toExclusive, num2), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x060065A5 RID: 26021 RVA: 0x0015A28D File Offset: 0x0015848D
		public static OrderablePartitioner<Tuple<int, int>> Create(int fromInclusive, int toExclusive, int rangeSize)
		{
			if (toExclusive <= fromInclusive)
			{
				throw new ArgumentOutOfRangeException("toExclusive");
			}
			if (rangeSize <= 0)
			{
				throw new ArgumentOutOfRangeException("rangeSize");
			}
			return Partitioner.Create<Tuple<int, int>>(Partitioner.CreateRanges(fromInclusive, toExclusive, rangeSize), EnumerablePartitionerOptions.NoBuffering);
		}

		// Token: 0x060065A6 RID: 26022 RVA: 0x0015A2BB File Offset: 0x001584BB
		private static IEnumerable<Tuple<int, int>> CreateRanges(int fromInclusive, int toExclusive, int rangeSize)
		{
			bool shouldQuit = false;
			int i = fromInclusive;
			while (i < toExclusive && !shouldQuit)
			{
				int num = i;
				int num2;
				try
				{
					num2 = checked(i + rangeSize);
				}
				catch (OverflowException)
				{
					num2 = toExclusive;
					shouldQuit = true;
				}
				if (num2 > toExclusive)
				{
					num2 = toExclusive;
				}
				yield return new Tuple<int, int>(num, num2);
				i += rangeSize;
			}
			yield break;
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x0015A2DC File Offset: 0x001584DC
		private static int GetDefaultChunkSize<TSource>()
		{
			int num;
			if (default(TSource) != null || Nullable.GetUnderlyingType(typeof(TSource)) != null)
			{
				num = 128;
			}
			else
			{
				num = 512 / IntPtr.Size;
			}
			return num;
		}

		// Token: 0x04003B94 RID: 15252
		private const int DEFAULT_BYTES_PER_UNIT = 128;

		// Token: 0x04003B95 RID: 15253
		private const int DEFAULT_BYTES_PER_CHUNK = 512;

		// Token: 0x02000AC1 RID: 2753
		private abstract class DynamicPartitionEnumerator_Abstract<TSource, TSourceReader> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
		{
			// Token: 0x060065A8 RID: 26024 RVA: 0x0015A325 File Offset: 0x00158525
			protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex)
				: this(sharedReader, sharedIndex, false)
			{
			}

			// Token: 0x060065A9 RID: 26025 RVA: 0x0015A330 File Offset: 0x00158530
			protected DynamicPartitionEnumerator_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex, bool useSingleChunking)
			{
				this._sharedReader = sharedReader;
				this._sharedIndex = sharedIndex;
				this._maxChunkSize = (useSingleChunking ? 1 : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>.s_defaultMaxChunkSize);
			}

			// Token: 0x060065AA RID: 26026
			protected abstract bool GrabNextChunk(int requestedChunkSize);

			// Token: 0x17001196 RID: 4502
			// (get) Token: 0x060065AB RID: 26027
			// (set) Token: 0x060065AC RID: 26028
			protected abstract bool HasNoElementsLeft { get; set; }

			// Token: 0x17001197 RID: 4503
			// (get) Token: 0x060065AD RID: 26029
			public abstract KeyValuePair<long, TSource> Current { get; }

			// Token: 0x060065AE RID: 26030
			public abstract void Dispose();

			// Token: 0x060065AF RID: 26031 RVA: 0x00047E00 File Offset: 0x00046000
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001198 RID: 4504
			// (get) Token: 0x060065B0 RID: 26032 RVA: 0x0015A357 File Offset: 0x00158557
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060065B1 RID: 26033 RVA: 0x0015A364 File Offset: 0x00158564
			public bool MoveNext()
			{
				if (this._localOffset == null)
				{
					this._localOffset = new Partitioner.SharedInt(-1);
					this._currentChunkSize = new Partitioner.SharedInt(0);
					this._doublingCountdown = 3;
				}
				if (this._localOffset.Value < this._currentChunkSize.Value - 1)
				{
					this._localOffset.Value++;
					return true;
				}
				int num;
				if (this._currentChunkSize.Value == 0)
				{
					num = 1;
				}
				else if (this._doublingCountdown > 0)
				{
					num = this._currentChunkSize.Value;
				}
				else
				{
					num = Math.Min(this._currentChunkSize.Value * 2, this._maxChunkSize);
					this._doublingCountdown = 3;
				}
				this._doublingCountdown--;
				if (this.GrabNextChunk(num))
				{
					this._localOffset.Value = 0;
					return true;
				}
				return false;
			}

			// Token: 0x060065B2 RID: 26034 RVA: 0x0015A445 File Offset: 0x00158645
			// Note: this type is marked as 'beforefieldinit'.
			static DynamicPartitionEnumerator_Abstract()
			{
			}

			// Token: 0x04003B96 RID: 15254
			protected readonly TSourceReader _sharedReader;

			// Token: 0x04003B97 RID: 15255
			protected static int s_defaultMaxChunkSize = Partitioner.GetDefaultChunkSize<TSource>();

			// Token: 0x04003B98 RID: 15256
			protected Partitioner.SharedInt _currentChunkSize;

			// Token: 0x04003B99 RID: 15257
			protected Partitioner.SharedInt _localOffset;

			// Token: 0x04003B9A RID: 15258
			private const int CHUNK_DOUBLING_RATE = 3;

			// Token: 0x04003B9B RID: 15259
			private int _doublingCountdown;

			// Token: 0x04003B9C RID: 15260
			protected readonly int _maxChunkSize;

			// Token: 0x04003B9D RID: 15261
			protected readonly Partitioner.SharedLong _sharedIndex;
		}

		// Token: 0x02000AC2 RID: 2754
		private class DynamicPartitionerForIEnumerable<TSource> : OrderablePartitioner<TSource>
		{
			// Token: 0x060065B3 RID: 26035 RVA: 0x0015A451 File Offset: 0x00158651
			internal DynamicPartitionerForIEnumerable(IEnumerable<TSource> source, EnumerablePartitionerOptions partitionerOptions)
				: base(true, false, true)
			{
				this._source = source;
				this._useSingleChunking = (partitionerOptions & EnumerablePartitionerOptions.NoBuffering) > EnumerablePartitionerOptions.None;
			}

			// Token: 0x060065B4 RID: 26036 RVA: 0x0015A470 File Offset: 0x00158670
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				IEnumerable<KeyValuePair<long, TSource>> enumerable = new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this._source.GetEnumerator(), this._useSingleChunking, true);
				for (int i = 0; i < partitionCount; i++)
				{
					array[i] = enumerable.GetEnumerator();
				}
				return array;
			}

			// Token: 0x060065B5 RID: 26037 RVA: 0x0015A4C1 File Offset: 0x001586C1
			public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
			{
				return new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable(this._source.GetEnumerator(), this._useSingleChunking, false);
			}

			// Token: 0x17001199 RID: 4505
			// (get) Token: 0x060065B6 RID: 26038 RVA: 0x00003FB7 File Offset: 0x000021B7
			public override bool SupportsDynamicPartitions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04003B9E RID: 15262
			private IEnumerable<TSource> _source;

			// Token: 0x04003B9F RID: 15263
			private readonly bool _useSingleChunking;

			// Token: 0x02000AC3 RID: 2755
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable, IDisposable
			{
				// Token: 0x060065B7 RID: 26039 RVA: 0x0015A4DC File Offset: 0x001586DC
				internal InternalPartitionEnumerable(IEnumerator<TSource> sharedReader, bool useSingleChunking, bool isStaticPartitioning)
				{
					this._sharedReader = sharedReader;
					this._sharedIndex = new Partitioner.SharedLong(-1L);
					this._hasNoElementsLeft = new Partitioner.SharedBool(false);
					this._sourceDepleted = new Partitioner.SharedBool(false);
					this._sharedLock = new object();
					this._useSingleChunking = useSingleChunking;
					if (!this._useSingleChunking)
					{
						int num = ((PlatformHelper.ProcessorCount > 4) ? 4 : 1);
						this._fillBuffer = new KeyValuePair<long, TSource>[num * Partitioner.GetDefaultChunkSize<TSource>()];
					}
					if (isStaticPartitioning)
					{
						this._activePartitionCount = new Partitioner.SharedInt(0);
						return;
					}
					this._activePartitionCount = null;
				}

				// Token: 0x060065B8 RID: 26040 RVA: 0x0015A56D File Offset: 0x0015876D
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					if (this._disposed)
					{
						throw new ObjectDisposedException("Can not call GetEnumerator on partitions after the source enumerable is disposed");
					}
					return new Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerator(this._sharedReader, this._sharedIndex, this._hasNoElementsLeft, this._activePartitionCount, this, this._useSingleChunking);
				}

				// Token: 0x060065B9 RID: 26041 RVA: 0x0015A5A6 File Offset: 0x001587A6
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060065BA RID: 26042 RVA: 0x0015A5B0 File Offset: 0x001587B0
				private void TryCopyFromFillBuffer(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					actualNumElementsGrabbed = 0;
					KeyValuePair<long, TSource>[] fillBuffer = this._fillBuffer;
					if (fillBuffer == null)
					{
						return;
					}
					if (this._fillBufferCurrentPosition >= this._fillBufferSize)
					{
						return;
					}
					Interlocked.Increment(ref this._activeCopiers);
					int num = Interlocked.Add(ref this._fillBufferCurrentPosition, requestedChunkSize);
					int num2 = num - requestedChunkSize;
					if (num2 < this._fillBufferSize)
					{
						actualNumElementsGrabbed = ((num < this._fillBufferSize) ? num : (this._fillBufferSize - num2));
						Array.Copy(fillBuffer, num2, destArray, 0, actualNumElementsGrabbed);
					}
					Interlocked.Decrement(ref this._activeCopiers);
				}

				// Token: 0x060065BB RID: 26043 RVA: 0x0015A639 File Offset: 0x00158839
				internal bool GrabChunk(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					actualNumElementsGrabbed = 0;
					if (this._hasNoElementsLeft.Value)
					{
						return false;
					}
					if (this._useSingleChunking)
					{
						return this.GrabChunk_Single(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
					}
					return this.GrabChunk_Buffered(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
				}

				// Token: 0x060065BC RID: 26044 RVA: 0x0015A66C File Offset: 0x0015886C
				internal bool GrabChunk_Single(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					object sharedLock = this._sharedLock;
					bool flag2;
					lock (sharedLock)
					{
						if (this._hasNoElementsLeft.Value)
						{
							flag2 = false;
						}
						else
						{
							try
							{
								if (this._sharedReader.MoveNext())
								{
									this._sharedIndex.Value = checked(this._sharedIndex.Value + 1L);
									destArray[0] = new KeyValuePair<long, TSource>(this._sharedIndex.Value, this._sharedReader.Current);
									actualNumElementsGrabbed = 1;
									flag2 = true;
								}
								else
								{
									this._sourceDepleted.Value = true;
									this._hasNoElementsLeft.Value = true;
									flag2 = false;
								}
							}
							catch
							{
								this._sourceDepleted.Value = true;
								this._hasNoElementsLeft.Value = true;
								throw;
							}
						}
					}
					return flag2;
				}

				// Token: 0x060065BD RID: 26045 RVA: 0x0015A758 File Offset: 0x00158958
				internal bool GrabChunk_Buffered(KeyValuePair<long, TSource>[] destArray, int requestedChunkSize, ref int actualNumElementsGrabbed)
				{
					this.TryCopyFromFillBuffer(destArray, requestedChunkSize, ref actualNumElementsGrabbed);
					if (actualNumElementsGrabbed == requestedChunkSize)
					{
						return true;
					}
					if (this._sourceDepleted.Value)
					{
						this._hasNoElementsLeft.Value = true;
						this._fillBuffer = null;
						return actualNumElementsGrabbed > 0;
					}
					object sharedLock = this._sharedLock;
					lock (sharedLock)
					{
						if (this._sourceDepleted.Value)
						{
							return actualNumElementsGrabbed > 0;
						}
						try
						{
							if (this._activeCopiers > 0)
							{
								SpinWait spinWait = default(SpinWait);
								while (this._activeCopiers > 0)
								{
									spinWait.SpinOnce();
								}
							}
							while (actualNumElementsGrabbed < requestedChunkSize)
							{
								if (!this._sharedReader.MoveNext())
								{
									this._sourceDepleted.Value = true;
									break;
								}
								this._sharedIndex.Value = checked(this._sharedIndex.Value + 1L);
								destArray[actualNumElementsGrabbed] = new KeyValuePair<long, TSource>(this._sharedIndex.Value, this._sharedReader.Current);
								actualNumElementsGrabbed++;
							}
							KeyValuePair<long, TSource>[] fillBuffer = this._fillBuffer;
							if (!this._sourceDepleted.Value && fillBuffer != null && this._fillBufferCurrentPosition >= fillBuffer.Length)
							{
								for (int i = 0; i < fillBuffer.Length; i++)
								{
									if (!this._sharedReader.MoveNext())
									{
										this._sourceDepleted.Value = true;
										this._fillBufferSize = i;
										break;
									}
									this._sharedIndex.Value = checked(this._sharedIndex.Value + 1L);
									fillBuffer[i] = new KeyValuePair<long, TSource>(this._sharedIndex.Value, this._sharedReader.Current);
								}
								this._fillBufferCurrentPosition = 0;
							}
						}
						catch
						{
							this._sourceDepleted.Value = true;
							this._hasNoElementsLeft.Value = true;
							throw;
						}
					}
					return actualNumElementsGrabbed > 0;
				}

				// Token: 0x060065BE RID: 26046 RVA: 0x0015A974 File Offset: 0x00158B74
				public void Dispose()
				{
					if (!this._disposed)
					{
						this._disposed = true;
						this._sharedReader.Dispose();
					}
				}

				// Token: 0x04003BA0 RID: 15264
				private readonly IEnumerator<TSource> _sharedReader;

				// Token: 0x04003BA1 RID: 15265
				private Partitioner.SharedLong _sharedIndex;

				// Token: 0x04003BA2 RID: 15266
				private volatile KeyValuePair<long, TSource>[] _fillBuffer;

				// Token: 0x04003BA3 RID: 15267
				private volatile int _fillBufferSize;

				// Token: 0x04003BA4 RID: 15268
				private volatile int _fillBufferCurrentPosition;

				// Token: 0x04003BA5 RID: 15269
				private volatile int _activeCopiers;

				// Token: 0x04003BA6 RID: 15270
				private Partitioner.SharedBool _hasNoElementsLeft;

				// Token: 0x04003BA7 RID: 15271
				private Partitioner.SharedBool _sourceDepleted;

				// Token: 0x04003BA8 RID: 15272
				private object _sharedLock;

				// Token: 0x04003BA9 RID: 15273
				private bool _disposed;

				// Token: 0x04003BAA RID: 15274
				private Partitioner.SharedInt _activePartitionCount;

				// Token: 0x04003BAB RID: 15275
				private readonly bool _useSingleChunking;
			}

			// Token: 0x02000AC4 RID: 2756
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, IEnumerator<TSource>>
			{
				// Token: 0x060065BF RID: 26047 RVA: 0x0015A990 File Offset: 0x00158B90
				internal InternalPartitionEnumerator(IEnumerator<TSource> sharedReader, Partitioner.SharedLong sharedIndex, Partitioner.SharedBool hasNoElementsLeft, Partitioner.SharedInt activePartitionCount, Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable enumerable, bool useSingleChunking)
					: base(sharedReader, sharedIndex, useSingleChunking)
				{
					this._hasNoElementsLeft = hasNoElementsLeft;
					this._enumerable = enumerable;
					this._activePartitionCount = activePartitionCount;
					if (this._activePartitionCount != null)
					{
						Interlocked.Increment(ref this._activePartitionCount.Value);
					}
				}

				// Token: 0x060065C0 RID: 26048 RVA: 0x0015A9CC File Offset: 0x00158BCC
				protected override bool GrabNextChunk(int requestedChunkSize)
				{
					if (this.HasNoElementsLeft)
					{
						return false;
					}
					if (this._localList == null)
					{
						this._localList = new KeyValuePair<long, TSource>[this._maxChunkSize];
					}
					return this._enumerable.GrabChunk(this._localList, requestedChunkSize, ref this._currentChunkSize.Value);
				}

				// Token: 0x1700119A RID: 4506
				// (get) Token: 0x060065C1 RID: 26049 RVA: 0x0015AA19 File Offset: 0x00158C19
				// (set) Token: 0x060065C2 RID: 26050 RVA: 0x0015AA28 File Offset: 0x00158C28
				protected override bool HasNoElementsLeft
				{
					get
					{
						return this._hasNoElementsLeft.Value;
					}
					set
					{
						this._hasNoElementsLeft.Value = true;
					}
				}

				// Token: 0x1700119B RID: 4507
				// (get) Token: 0x060065C3 RID: 26051 RVA: 0x0015AA38 File Offset: 0x00158C38
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this._currentChunkSize == null)
						{
							throw new InvalidOperationException("MoveNext must be called at least once before calling Current.");
						}
						return this._localList[this._localOffset.Value];
					}
				}

				// Token: 0x060065C4 RID: 26052 RVA: 0x0015AA65 File Offset: 0x00158C65
				public override void Dispose()
				{
					if (this._activePartitionCount != null && Interlocked.Decrement(ref this._activePartitionCount.Value) == 0)
					{
						this._enumerable.Dispose();
					}
				}

				// Token: 0x04003BAC RID: 15276
				private KeyValuePair<long, TSource>[] _localList;

				// Token: 0x04003BAD RID: 15277
				private readonly Partitioner.SharedBool _hasNoElementsLeft;

				// Token: 0x04003BAE RID: 15278
				private readonly Partitioner.SharedInt _activePartitionCount;

				// Token: 0x04003BAF RID: 15279
				private Partitioner.DynamicPartitionerForIEnumerable<TSource>.InternalPartitionEnumerable _enumerable;
			}
		}

		// Token: 0x02000AC5 RID: 2757
		private abstract class DynamicPartitionerForIndexRange_Abstract<TSource, TCollection> : OrderablePartitioner<TSource>
		{
			// Token: 0x060065C5 RID: 26053 RVA: 0x0015AA8C File Offset: 0x00158C8C
			protected DynamicPartitionerForIndexRange_Abstract(TCollection data)
				: base(true, false, true)
			{
				this._data = data;
			}

			// Token: 0x060065C6 RID: 26054
			protected abstract IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TCollection data);

			// Token: 0x060065C7 RID: 26055 RVA: 0x0015AAA0 File Offset: 0x00158CA0
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				IEnumerable<KeyValuePair<long, TSource>> orderableDynamicPartitions_Factory = this.GetOrderableDynamicPartitions_Factory(this._data);
				for (int i = 0; i < partitionCount; i++)
				{
					array[i] = orderableDynamicPartitions_Factory.GetEnumerator();
				}
				return array;
			}

			// Token: 0x060065C8 RID: 26056 RVA: 0x0015AAE6 File Offset: 0x00158CE6
			public override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions()
			{
				return this.GetOrderableDynamicPartitions_Factory(this._data);
			}

			// Token: 0x1700119C RID: 4508
			// (get) Token: 0x060065C9 RID: 26057 RVA: 0x00003FB7 File Offset: 0x000021B7
			public override bool SupportsDynamicPartitions
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04003BB0 RID: 15280
			private TCollection _data;
		}

		// Token: 0x02000AC6 RID: 2758
		private abstract class DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSourceReader> : Partitioner.DynamicPartitionEnumerator_Abstract<TSource, TSourceReader>
		{
			// Token: 0x060065CA RID: 26058 RVA: 0x0015AAF4 File Offset: 0x00158CF4
			protected DynamicPartitionEnumeratorForIndexRange_Abstract(TSourceReader sharedReader, Partitioner.SharedLong sharedIndex)
				: base(sharedReader, sharedIndex)
			{
			}

			// Token: 0x1700119D RID: 4509
			// (get) Token: 0x060065CB RID: 26059
			protected abstract int SourceCount { get; }

			// Token: 0x060065CC RID: 26060 RVA: 0x0015AB00 File Offset: 0x00158D00
			protected override bool GrabNextChunk(int requestedChunkSize)
			{
				while (!this.HasNoElementsLeft)
				{
					long num = Volatile.Read(ref this._sharedIndex.Value);
					if (this.HasNoElementsLeft)
					{
						return false;
					}
					long num2 = Math.Min((long)(this.SourceCount - 1), num + (long)requestedChunkSize);
					if (Interlocked.CompareExchange(ref this._sharedIndex.Value, num2, num) == num)
					{
						this._currentChunkSize.Value = (int)(num2 - num);
						this._localOffset.Value = -1;
						this._startIndex = (int)(num + 1L);
						return true;
					}
				}
				return false;
			}

			// Token: 0x1700119E RID: 4510
			// (get) Token: 0x060065CD RID: 26061 RVA: 0x0015AB87 File Offset: 0x00158D87
			// (set) Token: 0x060065CE RID: 26062 RVA: 0x00004088 File Offset: 0x00002288
			protected override bool HasNoElementsLeft
			{
				get
				{
					return Volatile.Read(ref this._sharedIndex.Value) >= (long)(this.SourceCount - 1);
				}
				set
				{
				}
			}

			// Token: 0x060065CF RID: 26063 RVA: 0x00004088 File Offset: 0x00002288
			public override void Dispose()
			{
			}

			// Token: 0x04003BB1 RID: 15281
			protected int _startIndex;
		}

		// Token: 0x02000AC7 RID: 2759
		private class DynamicPartitionerForIList<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, IList<TSource>>
		{
			// Token: 0x060065D0 RID: 26064 RVA: 0x0015ABA7 File Offset: 0x00158DA7
			internal DynamicPartitionerForIList(IList<TSource> source)
				: base(source)
			{
			}

			// Token: 0x060065D1 RID: 26065 RVA: 0x0015ABB0 File Offset: 0x00158DB0
			protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(IList<TSource> _data)
			{
				return new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerable(_data);
			}

			// Token: 0x02000AC8 RID: 2760
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
			{
				// Token: 0x060065D2 RID: 26066 RVA: 0x0015ABB8 File Offset: 0x00158DB8
				internal InternalPartitionEnumerable(IList<TSource> sharedReader)
				{
					this._sharedReader = sharedReader;
					this._sharedIndex = new Partitioner.SharedLong(-1L);
				}

				// Token: 0x060065D3 RID: 26067 RVA: 0x0015ABD4 File Offset: 0x00158DD4
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					return new Partitioner.DynamicPartitionerForIList<TSource>.InternalPartitionEnumerator(this._sharedReader, this._sharedIndex);
				}

				// Token: 0x060065D4 RID: 26068 RVA: 0x0015ABE7 File Offset: 0x00158DE7
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x04003BB2 RID: 15282
				private readonly IList<TSource> _sharedReader;

				// Token: 0x04003BB3 RID: 15283
				private Partitioner.SharedLong _sharedIndex;
			}

			// Token: 0x02000AC9 RID: 2761
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, IList<TSource>>
			{
				// Token: 0x060065D5 RID: 26069 RVA: 0x0015ABEF File Offset: 0x00158DEF
				internal InternalPartitionEnumerator(IList<TSource> sharedReader, Partitioner.SharedLong sharedIndex)
					: base(sharedReader, sharedIndex)
				{
				}

				// Token: 0x1700119F RID: 4511
				// (get) Token: 0x060065D6 RID: 26070 RVA: 0x0015ABF9 File Offset: 0x00158DF9
				protected override int SourceCount
				{
					get
					{
						return this._sharedReader.Count;
					}
				}

				// Token: 0x170011A0 RID: 4512
				// (get) Token: 0x060065D7 RID: 26071 RVA: 0x0015AC08 File Offset: 0x00158E08
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this._currentChunkSize == null)
						{
							throw new InvalidOperationException("MoveNext must be called at least once before calling Current.");
						}
						return new KeyValuePair<long, TSource>((long)(this._startIndex + this._localOffset.Value), this._sharedReader[this._startIndex + this._localOffset.Value]);
					}
				}
			}
		}

		// Token: 0x02000ACA RID: 2762
		private class DynamicPartitionerForArray<TSource> : Partitioner.DynamicPartitionerForIndexRange_Abstract<TSource, TSource[]>
		{
			// Token: 0x060065D8 RID: 26072 RVA: 0x0015AC61 File Offset: 0x00158E61
			internal DynamicPartitionerForArray(TSource[] source)
				: base(source)
			{
			}

			// Token: 0x060065D9 RID: 26073 RVA: 0x0015AC6A File Offset: 0x00158E6A
			protected override IEnumerable<KeyValuePair<long, TSource>> GetOrderableDynamicPartitions_Factory(TSource[] _data)
			{
				return new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerable(_data);
			}

			// Token: 0x02000ACB RID: 2763
			private class InternalPartitionEnumerable : IEnumerable<KeyValuePair<long, TSource>>, IEnumerable
			{
				// Token: 0x060065DA RID: 26074 RVA: 0x0015AC72 File Offset: 0x00158E72
				internal InternalPartitionEnumerable(TSource[] sharedReader)
				{
					this._sharedReader = sharedReader;
					this._sharedIndex = new Partitioner.SharedLong(-1L);
				}

				// Token: 0x060065DB RID: 26075 RVA: 0x0015AC8E File Offset: 0x00158E8E
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x060065DC RID: 26076 RVA: 0x0015AC96 File Offset: 0x00158E96
				public IEnumerator<KeyValuePair<long, TSource>> GetEnumerator()
				{
					return new Partitioner.DynamicPartitionerForArray<TSource>.InternalPartitionEnumerator(this._sharedReader, this._sharedIndex);
				}

				// Token: 0x04003BB4 RID: 15284
				private readonly TSource[] _sharedReader;

				// Token: 0x04003BB5 RID: 15285
				private Partitioner.SharedLong _sharedIndex;
			}

			// Token: 0x02000ACC RID: 2764
			private class InternalPartitionEnumerator : Partitioner.DynamicPartitionEnumeratorForIndexRange_Abstract<TSource, TSource[]>
			{
				// Token: 0x060065DD RID: 26077 RVA: 0x0015ACA9 File Offset: 0x00158EA9
				internal InternalPartitionEnumerator(TSource[] sharedReader, Partitioner.SharedLong sharedIndex)
					: base(sharedReader, sharedIndex)
				{
				}

				// Token: 0x170011A1 RID: 4513
				// (get) Token: 0x060065DE RID: 26078 RVA: 0x0015ACB3 File Offset: 0x00158EB3
				protected override int SourceCount
				{
					get
					{
						return this._sharedReader.Length;
					}
				}

				// Token: 0x170011A2 RID: 4514
				// (get) Token: 0x060065DF RID: 26079 RVA: 0x0015ACC0 File Offset: 0x00158EC0
				public override KeyValuePair<long, TSource> Current
				{
					get
					{
						if (this._currentChunkSize == null)
						{
							throw new InvalidOperationException("MoveNext must be called at least once before calling Current.");
						}
						return new KeyValuePair<long, TSource>((long)(this._startIndex + this._localOffset.Value), this._sharedReader[this._startIndex + this._localOffset.Value]);
					}
				}
			}
		}

		// Token: 0x02000ACD RID: 2765
		private abstract class StaticIndexRangePartitioner<TSource, TCollection> : OrderablePartitioner<TSource>
		{
			// Token: 0x060065E0 RID: 26080 RVA: 0x0015AD19 File Offset: 0x00158F19
			protected StaticIndexRangePartitioner()
				: base(true, true, true)
			{
			}

			// Token: 0x170011A3 RID: 4515
			// (get) Token: 0x060065E1 RID: 26081
			protected abstract int SourceCount { get; }

			// Token: 0x060065E2 RID: 26082
			protected abstract IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex);

			// Token: 0x060065E3 RID: 26083 RVA: 0x0015AD24 File Offset: 0x00158F24
			public override IList<IEnumerator<KeyValuePair<long, TSource>>> GetOrderablePartitions(int partitionCount)
			{
				if (partitionCount <= 0)
				{
					throw new ArgumentOutOfRangeException("partitionCount");
				}
				int num = this.SourceCount / partitionCount;
				int num2 = this.SourceCount % partitionCount;
				IEnumerator<KeyValuePair<long, TSource>>[] array = new IEnumerator<KeyValuePair<long, TSource>>[partitionCount];
				int num3 = -1;
				for (int i = 0; i < partitionCount; i++)
				{
					int num4 = num3 + 1;
					if (i < num2)
					{
						num3 = num4 + num;
					}
					else
					{
						num3 = num4 + num - 1;
					}
					array[i] = this.CreatePartition(num4, num3);
				}
				return array;
			}
		}

		// Token: 0x02000ACE RID: 2766
		private abstract class StaticIndexRangePartition<TSource> : IEnumerator<KeyValuePair<long, TSource>>, IDisposable, IEnumerator
		{
			// Token: 0x060065E4 RID: 26084 RVA: 0x0015AD91 File Offset: 0x00158F91
			protected StaticIndexRangePartition(int startIndex, int endIndex)
			{
				this._startIndex = startIndex;
				this._endIndex = endIndex;
				this._offset = startIndex - 1;
			}

			// Token: 0x170011A4 RID: 4516
			// (get) Token: 0x060065E5 RID: 26085
			public abstract KeyValuePair<long, TSource> Current { get; }

			// Token: 0x060065E6 RID: 26086 RVA: 0x00004088 File Offset: 0x00002288
			public void Dispose()
			{
			}

			// Token: 0x060065E7 RID: 26087 RVA: 0x00047E00 File Offset: 0x00046000
			public void Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060065E8 RID: 26088 RVA: 0x0015ADB2 File Offset: 0x00158FB2
			public bool MoveNext()
			{
				if (this._offset < this._endIndex)
				{
					this._offset++;
					return true;
				}
				this._offset = this._endIndex + 1;
				return false;
			}

			// Token: 0x170011A5 RID: 4517
			// (get) Token: 0x060065E9 RID: 26089 RVA: 0x0015ADE9 File Offset: 0x00158FE9
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04003BB6 RID: 15286
			protected readonly int _startIndex;

			// Token: 0x04003BB7 RID: 15287
			protected readonly int _endIndex;

			// Token: 0x04003BB8 RID: 15288
			protected volatile int _offset;
		}

		// Token: 0x02000ACF RID: 2767
		private class StaticIndexRangePartitionerForIList<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, IList<TSource>>
		{
			// Token: 0x060065EA RID: 26090 RVA: 0x0015ADF6 File Offset: 0x00158FF6
			internal StaticIndexRangePartitionerForIList(IList<TSource> list)
			{
				this._list = list;
			}

			// Token: 0x170011A6 RID: 4518
			// (get) Token: 0x060065EB RID: 26091 RVA: 0x0015AE05 File Offset: 0x00159005
			protected override int SourceCount
			{
				get
				{
					return this._list.Count;
				}
			}

			// Token: 0x060065EC RID: 26092 RVA: 0x0015AE12 File Offset: 0x00159012
			protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
			{
				return new Partitioner.StaticIndexRangePartitionForIList<TSource>(this._list, startIndex, endIndex);
			}

			// Token: 0x04003BB9 RID: 15289
			private IList<TSource> _list;
		}

		// Token: 0x02000AD0 RID: 2768
		private class StaticIndexRangePartitionForIList<TSource> : Partitioner.StaticIndexRangePartition<TSource>
		{
			// Token: 0x060065ED RID: 26093 RVA: 0x0015AE21 File Offset: 0x00159021
			internal StaticIndexRangePartitionForIList(IList<TSource> list, int startIndex, int endIndex)
				: base(startIndex, endIndex)
			{
				this._list = list;
			}

			// Token: 0x170011A7 RID: 4519
			// (get) Token: 0x060065EE RID: 26094 RVA: 0x0015AE34 File Offset: 0x00159034
			public override KeyValuePair<long, TSource> Current
			{
				get
				{
					if (this._offset < this._startIndex)
					{
						throw new InvalidOperationException("MoveNext must be called at least once before calling Current.");
					}
					return new KeyValuePair<long, TSource>((long)this._offset, this._list[this._offset]);
				}
			}

			// Token: 0x04003BBA RID: 15290
			private volatile IList<TSource> _list;
		}

		// Token: 0x02000AD1 RID: 2769
		private class StaticIndexRangePartitionerForArray<TSource> : Partitioner.StaticIndexRangePartitioner<TSource, TSource[]>
		{
			// Token: 0x060065EF RID: 26095 RVA: 0x0015AE74 File Offset: 0x00159074
			internal StaticIndexRangePartitionerForArray(TSource[] array)
			{
				this._array = array;
			}

			// Token: 0x170011A8 RID: 4520
			// (get) Token: 0x060065F0 RID: 26096 RVA: 0x0015AE83 File Offset: 0x00159083
			protected override int SourceCount
			{
				get
				{
					return this._array.Length;
				}
			}

			// Token: 0x060065F1 RID: 26097 RVA: 0x0015AE8D File Offset: 0x0015908D
			protected override IEnumerator<KeyValuePair<long, TSource>> CreatePartition(int startIndex, int endIndex)
			{
				return new Partitioner.StaticIndexRangePartitionForArray<TSource>(this._array, startIndex, endIndex);
			}

			// Token: 0x04003BBB RID: 15291
			private TSource[] _array;
		}

		// Token: 0x02000AD2 RID: 2770
		private class StaticIndexRangePartitionForArray<TSource> : Partitioner.StaticIndexRangePartition<TSource>
		{
			// Token: 0x060065F2 RID: 26098 RVA: 0x0015AE9C File Offset: 0x0015909C
			internal StaticIndexRangePartitionForArray(TSource[] array, int startIndex, int endIndex)
				: base(startIndex, endIndex)
			{
				this._array = array;
			}

			// Token: 0x170011A9 RID: 4521
			// (get) Token: 0x060065F3 RID: 26099 RVA: 0x0015AEAF File Offset: 0x001590AF
			public override KeyValuePair<long, TSource> Current
			{
				get
				{
					if (this._offset < this._startIndex)
					{
						throw new InvalidOperationException("MoveNext must be called at least once before calling Current.");
					}
					return new KeyValuePair<long, TSource>((long)this._offset, this._array[this._offset]);
				}
			}

			// Token: 0x04003BBC RID: 15292
			private volatile TSource[] _array;
		}

		// Token: 0x02000AD3 RID: 2771
		private class SharedInt
		{
			// Token: 0x060065F4 RID: 26100 RVA: 0x0015AEEF File Offset: 0x001590EF
			internal SharedInt(int value)
			{
				this.Value = value;
			}

			// Token: 0x04003BBD RID: 15293
			internal volatile int Value;
		}

		// Token: 0x02000AD4 RID: 2772
		private class SharedBool
		{
			// Token: 0x060065F5 RID: 26101 RVA: 0x0015AF00 File Offset: 0x00159100
			internal SharedBool(bool value)
			{
				this.Value = value;
			}

			// Token: 0x04003BBE RID: 15294
			internal volatile bool Value;
		}

		// Token: 0x02000AD5 RID: 2773
		private class SharedLong
		{
			// Token: 0x060065F6 RID: 26102 RVA: 0x0015AF11 File Offset: 0x00159111
			internal SharedLong(long value)
			{
				this.Value = value;
			}

			// Token: 0x04003BBF RID: 15295
			internal long Value;
		}

		// Token: 0x02000AD6 RID: 2774
		[CompilerGenerated]
		private sealed class <CreateRanges>d__6 : IEnumerable<Tuple<long, long>>, IEnumerable, IEnumerator<Tuple<long, long>>, IDisposable, IEnumerator
		{
			// Token: 0x060065F7 RID: 26103 RVA: 0x0015AF20 File Offset: 0x00159120
			[DebuggerHidden]
			public <CreateRanges>d__6(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060065F8 RID: 26104 RVA: 0x00004088 File Offset: 0x00002288
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060065F9 RID: 26105 RVA: 0x0015AF3C File Offset: 0x0015913C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					i += rangeSize;
				}
				else
				{
					this.<>1__state = -1;
					shouldQuit = false;
					i = fromInclusive;
				}
				if (i >= toExclusive || shouldQuit)
				{
					return false;
				}
				long num2 = i;
				long num3;
				try
				{
					num3 = checked(i + rangeSize);
				}
				catch (OverflowException)
				{
					num3 = toExclusive;
					shouldQuit = true;
				}
				if (num3 > toExclusive)
				{
					num3 = toExclusive;
				}
				this.<>2__current = new Tuple<long, long>(num2, num3);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170011AA RID: 4522
			// (get) Token: 0x060065FA RID: 26106 RVA: 0x0015B004 File Offset: 0x00159204
			Tuple<long, long> IEnumerator<Tuple<long, long>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060065FB RID: 26107 RVA: 0x00047E00 File Offset: 0x00046000
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170011AB RID: 4523
			// (get) Token: 0x060065FC RID: 26108 RVA: 0x0015B004 File Offset: 0x00159204
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060065FD RID: 26109 RVA: 0x0015B00C File Offset: 0x0015920C
			[DebuggerHidden]
			IEnumerator<Tuple<long, long>> IEnumerable<Tuple<long, long>>.GetEnumerator()
			{
				Partitioner.<CreateRanges>d__6 <CreateRanges>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<CreateRanges>d__ = this;
				}
				else
				{
					<CreateRanges>d__ = new Partitioner.<CreateRanges>d__6(0);
				}
				<CreateRanges>d__.fromInclusive = fromInclusive;
				<CreateRanges>d__.toExclusive = toExclusive;
				<CreateRanges>d__.rangeSize = rangeSize;
				return <CreateRanges>d__;
			}

			// Token: 0x060065FE RID: 26110 RVA: 0x0015B067 File Offset: 0x00159267
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Tuple<System.Int64,System.Int64>>.GetEnumerator();
			}

			// Token: 0x04003BC0 RID: 15296
			private int <>1__state;

			// Token: 0x04003BC1 RID: 15297
			private Tuple<long, long> <>2__current;

			// Token: 0x04003BC2 RID: 15298
			private int <>l__initialThreadId;

			// Token: 0x04003BC3 RID: 15299
			private long fromInclusive;

			// Token: 0x04003BC4 RID: 15300
			public long <>3__fromInclusive;

			// Token: 0x04003BC5 RID: 15301
			private long rangeSize;

			// Token: 0x04003BC6 RID: 15302
			public long <>3__rangeSize;

			// Token: 0x04003BC7 RID: 15303
			private long toExclusive;

			// Token: 0x04003BC8 RID: 15304
			public long <>3__toExclusive;

			// Token: 0x04003BC9 RID: 15305
			private bool <shouldQuit>5__2;

			// Token: 0x04003BCA RID: 15306
			private long <i>5__3;
		}

		// Token: 0x02000AD7 RID: 2775
		[CompilerGenerated]
		private sealed class <CreateRanges>d__9 : IEnumerable<Tuple<int, int>>, IEnumerable, IEnumerator<Tuple<int, int>>, IDisposable, IEnumerator
		{
			// Token: 0x060065FF RID: 26111 RVA: 0x0015B06F File Offset: 0x0015926F
			[DebuggerHidden]
			public <CreateRanges>d__9(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06006600 RID: 26112 RVA: 0x00004088 File Offset: 0x00002288
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06006601 RID: 26113 RVA: 0x0015B08C File Offset: 0x0015928C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					i += rangeSize;
				}
				else
				{
					this.<>1__state = -1;
					shouldQuit = false;
					i = fromInclusive;
				}
				if (i >= toExclusive || shouldQuit)
				{
					return false;
				}
				int num2 = i;
				int num3;
				try
				{
					num3 = checked(i + rangeSize);
				}
				catch (OverflowException)
				{
					num3 = toExclusive;
					shouldQuit = true;
				}
				if (num3 > toExclusive)
				{
					num3 = toExclusive;
				}
				this.<>2__current = new Tuple<int, int>(num2, num3);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170011AC RID: 4524
			// (get) Token: 0x06006602 RID: 26114 RVA: 0x0015B154 File Offset: 0x00159354
			Tuple<int, int> IEnumerator<Tuple<int, int>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006603 RID: 26115 RVA: 0x00047E00 File Offset: 0x00046000
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170011AD RID: 4525
			// (get) Token: 0x06006604 RID: 26116 RVA: 0x0015B154 File Offset: 0x00159354
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006605 RID: 26117 RVA: 0x0015B15C File Offset: 0x0015935C
			[DebuggerHidden]
			IEnumerator<Tuple<int, int>> IEnumerable<Tuple<int, int>>.GetEnumerator()
			{
				Partitioner.<CreateRanges>d__9 <CreateRanges>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<CreateRanges>d__ = this;
				}
				else
				{
					<CreateRanges>d__ = new Partitioner.<CreateRanges>d__9(0);
				}
				<CreateRanges>d__.fromInclusive = fromInclusive;
				<CreateRanges>d__.toExclusive = toExclusive;
				<CreateRanges>d__.rangeSize = rangeSize;
				return <CreateRanges>d__;
			}

			// Token: 0x06006606 RID: 26118 RVA: 0x0015B1B7 File Offset: 0x001593B7
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Tuple<System.Int32,System.Int32>>.GetEnumerator();
			}

			// Token: 0x04003BCB RID: 15307
			private int <>1__state;

			// Token: 0x04003BCC RID: 15308
			private Tuple<int, int> <>2__current;

			// Token: 0x04003BCD RID: 15309
			private int <>l__initialThreadId;

			// Token: 0x04003BCE RID: 15310
			private int fromInclusive;

			// Token: 0x04003BCF RID: 15311
			public int <>3__fromInclusive;

			// Token: 0x04003BD0 RID: 15312
			private int rangeSize;

			// Token: 0x04003BD1 RID: 15313
			public int <>3__rangeSize;

			// Token: 0x04003BD2 RID: 15314
			private int toExclusive;

			// Token: 0x04003BD3 RID: 15315
			public int <>3__toExclusive;

			// Token: 0x04003BD4 RID: 15316
			private bool <shouldQuit>5__2;

			// Token: 0x04003BD5 RID: 15317
			private int <i>5__3;
		}
	}
}
