using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x0200096A RID: 2410
	[Serializable]
	public abstract class Stream : MarshalByRefObject, IDisposable, IAsyncDisposable
	{
		// Token: 0x0600571F RID: 22303 RVA: 0x0012745A File Offset: 0x0012565A
		internal SemaphoreSlim EnsureAsyncActiveSemaphoreInitialized()
		{
			return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._asyncActiveSemaphore, () => new SemaphoreSlim(1, 1));
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06005720 RID: 22304
		public abstract bool CanRead { get; }

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06005721 RID: 22305
		public abstract bool CanSeek { get; }

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06005722 RID: 22306 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool CanTimeout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06005723 RID: 22307
		public abstract bool CanWrite { get; }

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06005724 RID: 22308
		public abstract long Length { get; }

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06005725 RID: 22309
		// (set) Token: 0x06005726 RID: 22310
		public abstract long Position { get; set; }

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06005727 RID: 22311 RVA: 0x00127486 File Offset: 0x00125686
		// (set) Token: 0x06005728 RID: 22312 RVA: 0x00127486 File Offset: 0x00125686
		public virtual int ReadTimeout
		{
			get
			{
				throw new InvalidOperationException("Timeouts are not supported on this stream.");
			}
			set
			{
				throw new InvalidOperationException("Timeouts are not supported on this stream.");
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06005729 RID: 22313 RVA: 0x00127486 File Offset: 0x00125686
		// (set) Token: 0x0600572A RID: 22314 RVA: 0x00127486 File Offset: 0x00125686
		public virtual int WriteTimeout
		{
			get
			{
				throw new InvalidOperationException("Timeouts are not supported on this stream.");
			}
			set
			{
				throw new InvalidOperationException("Timeouts are not supported on this stream.");
			}
		}

		// Token: 0x0600572B RID: 22315 RVA: 0x00127494 File Offset: 0x00125694
		public Task CopyToAsync(Stream destination)
		{
			int copyBufferSize = this.GetCopyBufferSize();
			return this.CopyToAsync(destination, copyBufferSize);
		}

		// Token: 0x0600572C RID: 22316 RVA: 0x001274B0 File Offset: 0x001256B0
		public Task CopyToAsync(Stream destination, int bufferSize)
		{
			return this.CopyToAsync(destination, bufferSize, CancellationToken.None);
		}

		// Token: 0x0600572D RID: 22317 RVA: 0x001274C0 File Offset: 0x001256C0
		public Task CopyToAsync(Stream destination, CancellationToken cancellationToken)
		{
			int copyBufferSize = this.GetCopyBufferSize();
			return this.CopyToAsync(destination, copyBufferSize, cancellationToken);
		}

		// Token: 0x0600572E RID: 22318 RVA: 0x001274DD File Offset: 0x001256DD
		public virtual Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			return this.CopyToAsyncInternal(destination, bufferSize, cancellationToken);
		}

		// Token: 0x0600572F RID: 22319 RVA: 0x001274F0 File Offset: 0x001256F0
		private async Task CopyToAsyncInternal(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			byte[] buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
			try
			{
				for (;;)
				{
					int num = await this.ReadAsync(new Memory<byte>(buffer), cancellationToken).ConfigureAwait(false);
					if (num == 0)
					{
						break;
					}
					await destination.WriteAsync(new ReadOnlyMemory<byte>(buffer, 0, num), cancellationToken).ConfigureAwait(false);
				}
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(buffer, false);
			}
		}

		// Token: 0x06005730 RID: 22320 RVA: 0x0012754C File Offset: 0x0012574C
		public void CopyTo(Stream destination)
		{
			int copyBufferSize = this.GetCopyBufferSize();
			this.CopyTo(destination, copyBufferSize);
		}

		// Token: 0x06005731 RID: 22321 RVA: 0x00127568 File Offset: 0x00125768
		public virtual void CopyTo(Stream destination, int bufferSize)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			byte[] array = ArrayPool<byte>.Shared.Rent(bufferSize);
			try
			{
				int num;
				while ((num = this.Read(array, 0, array.Length)) != 0)
				{
					destination.Write(array, 0, num);
				}
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06005732 RID: 22322 RVA: 0x001275C4 File Offset: 0x001257C4
		private int GetCopyBufferSize()
		{
			int num = 81920;
			if (this.CanSeek)
			{
				long length = this.Length;
				long position = this.Position;
				if (length <= position)
				{
					num = 1;
				}
				else
				{
					long num2 = length - position;
					if (num2 > 0L)
					{
						num = (int)Math.Min((long)num, num2);
					}
				}
			}
			return num;
		}

		// Token: 0x06005733 RID: 22323 RVA: 0x00127609 File Offset: 0x00125809
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005734 RID: 22324 RVA: 0x000A484D File Offset: 0x000A2A4D
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06005735 RID: 22325 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06005736 RID: 22326
		public abstract void Flush();

		// Token: 0x06005737 RID: 22327 RVA: 0x00127618 File Offset: 0x00125818
		public Task FlushAsync()
		{
			return this.FlushAsync(CancellationToken.None);
		}

		// Token: 0x06005738 RID: 22328 RVA: 0x00127625 File Offset: 0x00125825
		public virtual Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(delegate(object state)
			{
				((Stream)state).Flush();
			}, this, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06005739 RID: 22329 RVA: 0x00127658 File Offset: 0x00125858
		[Obsolete("CreateWaitHandle will be removed eventually.  Please use \"new ManualResetEvent(false)\" instead.")]
		protected virtual WaitHandle CreateWaitHandle()
		{
			return new ManualResetEvent(false);
		}

		// Token: 0x0600573A RID: 22330 RVA: 0x00127660 File Offset: 0x00125860
		public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.BeginReadInternal(buffer, offset, count, callback, state, false, true);
		}

		// Token: 0x0600573B RID: 22331 RVA: 0x00127674 File Offset: 0x00125874
		internal IAsyncResult BeginReadInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously, bool apm)
		{
			if (!this.CanRead)
			{
				throw Error.GetReadNotSupported();
			}
			SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
			Task task = null;
			if (serializeAsynchronously)
			{
				task = semaphoreSlim.WaitAsync();
			}
			else
			{
				semaphoreSlim.Wait();
			}
			Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(true, apm, delegate
			{
				Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
				int num;
				try
				{
					num = readWriteTask2._stream.Read(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
				}
				finally
				{
					if (!readWriteTask2._apm)
					{
						readWriteTask2._stream.FinishTrackingAsyncOperation();
					}
					readWriteTask2.ClearBeginState();
				}
				return num;
			}, state, this, buffer, offset, count, callback);
			if (task != null)
			{
				this.RunReadWriteTaskWhenReady(task, readWriteTask);
			}
			else
			{
				this.RunReadWriteTask(readWriteTask);
			}
			return readWriteTask;
		}

		// Token: 0x0600573C RID: 22332 RVA: 0x001276F0 File Offset: 0x001258F0
		public virtual int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Stream.ReadWriteTask activeReadWriteTask = this._activeReadWriteTask;
			if (activeReadWriteTask == null)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndRead was called multiple times with the same IAsyncResult.");
			}
			if (activeReadWriteTask != asyncResult)
			{
				throw new InvalidOperationException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndRead was called multiple times with the same IAsyncResult.");
			}
			if (!activeReadWriteTask._isRead)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndRead was called multiple times with the same IAsyncResult.");
			}
			int result;
			try
			{
				result = activeReadWriteTask.GetAwaiter().GetResult();
			}
			finally
			{
				this.FinishTrackingAsyncOperation();
			}
			return result;
		}

		// Token: 0x0600573D RID: 22333 RVA: 0x0012776C File Offset: 0x0012596C
		public Task<int> ReadAsync(byte[] buffer, int offset, int count)
		{
			return this.ReadAsync(buffer, offset, count, CancellationToken.None);
		}

		// Token: 0x0600573E RID: 22334 RVA: 0x0012777C File Offset: 0x0012597C
		public virtual Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.BeginEndReadAsync(buffer, offset, count);
			}
			return Task.FromCanceled<int>(cancellationToken);
		}

		// Token: 0x0600573F RID: 22335 RVA: 0x00127798 File Offset: 0x00125998
		public virtual ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<byte> arraySegment;
			if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
			{
				return new ValueTask<int>(this.ReadAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count, cancellationToken));
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			return Stream.<ReadAsync>g__FinishReadAsync|44_0(this.ReadAsync(array, 0, buffer.Length, cancellationToken), array, buffer);
		}

		// Token: 0x06005740 RID: 22336 RVA: 0x00127800 File Offset: 0x00125A00
		private Task<int> BeginEndReadAsync(byte[] buffer, int offset, int count)
		{
			if (!this.HasOverriddenBeginEndRead())
			{
				return (Task<int>)this.BeginReadInternal(buffer, offset, count, null, null, true, false);
			}
			return TaskFactory<int>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, new Stream.ReadWriteParameters
			{
				Buffer = buffer,
				Offset = offset,
				Count = count
			}, (Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state) => stream.BeginRead(args.Buffer, args.Offset, args.Count, callback, state), (Stream stream, IAsyncResult asyncResult) => stream.EndRead(asyncResult));
		}

		// Token: 0x06005741 RID: 22337 RVA: 0x0012788D File Offset: 0x00125A8D
		public virtual IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.BeginWriteInternal(buffer, offset, count, callback, state, false, true);
		}

		// Token: 0x06005742 RID: 22338 RVA: 0x001278A0 File Offset: 0x00125AA0
		internal IAsyncResult BeginWriteInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously, bool apm)
		{
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
			SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
			Task task = null;
			if (serializeAsynchronously)
			{
				task = semaphoreSlim.WaitAsync();
			}
			else
			{
				semaphoreSlim.Wait();
			}
			Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(false, apm, delegate
			{
				Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
				int num;
				try
				{
					readWriteTask2._stream.Write(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
					num = 0;
				}
				finally
				{
					if (!readWriteTask2._apm)
					{
						readWriteTask2._stream.FinishTrackingAsyncOperation();
					}
					readWriteTask2.ClearBeginState();
				}
				return num;
			}, state, this, buffer, offset, count, callback);
			if (task != null)
			{
				this.RunReadWriteTaskWhenReady(task, readWriteTask);
			}
			else
			{
				this.RunReadWriteTask(readWriteTask);
			}
			return readWriteTask;
		}

		// Token: 0x06005743 RID: 22339 RVA: 0x0012791C File Offset: 0x00125B1C
		private void RunReadWriteTaskWhenReady(Task asyncWaiter, Stream.ReadWriteTask readWriteTask)
		{
			if (asyncWaiter.IsCompleted)
			{
				this.RunReadWriteTask(readWriteTask);
				return;
			}
			asyncWaiter.ContinueWith(delegate(Task t, object state)
			{
				Stream.ReadWriteTask readWriteTask2 = (Stream.ReadWriteTask)state;
				readWriteTask2._stream.RunReadWriteTask(readWriteTask2);
			}, readWriteTask, default(CancellationToken), TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x06005744 RID: 22340 RVA: 0x00127973 File Offset: 0x00125B73
		private void RunReadWriteTask(Stream.ReadWriteTask readWriteTask)
		{
			this._activeReadWriteTask = readWriteTask;
			readWriteTask.m_taskScheduler = TaskScheduler.Default;
			readWriteTask.ScheduleAndStart(false);
		}

		// Token: 0x06005745 RID: 22341 RVA: 0x0012798E File Offset: 0x00125B8E
		private void FinishTrackingAsyncOperation()
		{
			this._activeReadWriteTask = null;
			this._asyncActiveSemaphore.Release();
		}

		// Token: 0x06005746 RID: 22342 RVA: 0x001279A4 File Offset: 0x00125BA4
		public virtual void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Stream.ReadWriteTask activeReadWriteTask = this._activeReadWriteTask;
			if (activeReadWriteTask == null)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndWrite was called multiple times with the same IAsyncResult.");
			}
			if (activeReadWriteTask != asyncResult)
			{
				throw new InvalidOperationException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndWrite was called multiple times with the same IAsyncResult.");
			}
			if (activeReadWriteTask._isRead)
			{
				throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or EndWrite was called multiple times with the same IAsyncResult.");
			}
			try
			{
				activeReadWriteTask.GetAwaiter().GetResult();
			}
			finally
			{
				this.FinishTrackingAsyncOperation();
			}
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x00127A20 File Offset: 0x00125C20
		public Task WriteAsync(byte[] buffer, int offset, int count)
		{
			return this.WriteAsync(buffer, offset, count, CancellationToken.None);
		}

		// Token: 0x06005748 RID: 22344 RVA: 0x00127A30 File Offset: 0x00125C30
		public virtual Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.BeginEndWriteAsync(buffer, offset, count);
			}
			return Task.FromCanceled(cancellationToken);
		}

		// Token: 0x06005749 RID: 22345 RVA: 0x00127A4C File Offset: 0x00125C4C
		public virtual ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			ArraySegment<byte> arraySegment;
			if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
			{
				return new ValueTask(this.WriteAsync(arraySegment.Array, arraySegment.Offset, arraySegment.Count, cancellationToken));
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			buffer.Span.CopyTo(array);
			return new ValueTask(this.FinishWriteAsync(this.WriteAsync(array, 0, buffer.Length, cancellationToken), array));
		}

		// Token: 0x0600574A RID: 22346 RVA: 0x00127AC8 File Offset: 0x00125CC8
		private async Task FinishWriteAsync(Task writeTask, byte[] localBuffer)
		{
			try
			{
				await writeTask.ConfigureAwait(false);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(localBuffer, false);
			}
		}

		// Token: 0x0600574B RID: 22347 RVA: 0x00127B14 File Offset: 0x00125D14
		private Task BeginEndWriteAsync(byte[] buffer, int offset, int count)
		{
			if (!this.HasOverriddenBeginEndWrite())
			{
				return (Task)this.BeginWriteInternal(buffer, offset, count, null, null, true, false);
			}
			return TaskFactory<VoidTaskResult>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, new Stream.ReadWriteParameters
			{
				Buffer = buffer,
				Offset = offset,
				Count = count
			}, (Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state) => stream.BeginWrite(args.Buffer, args.Offset, args.Count, callback, state), delegate(Stream stream, IAsyncResult asyncResult)
			{
				stream.EndWrite(asyncResult);
				return default(VoidTaskResult);
			});
		}

		// Token: 0x0600574C RID: 22348
		public abstract long Seek(long offset, SeekOrigin origin);

		// Token: 0x0600574D RID: 22349
		public abstract void SetLength(long value);

		// Token: 0x0600574E RID: 22350
		public abstract int Read(byte[] buffer, int offset, int count);

		// Token: 0x0600574F RID: 22351 RVA: 0x00127BA4 File Offset: 0x00125DA4
		public virtual int Read(Span<byte> buffer)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			int num2;
			try
			{
				int num = this.Read(array, 0, buffer.Length);
				if ((ulong)num > (ulong)((long)buffer.Length))
				{
					throw new IOException("Stream was too long.");
				}
				new Span<byte>(array, 0, num).CopyTo(buffer);
				num2 = num;
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return num2;
		}

		// Token: 0x06005750 RID: 22352 RVA: 0x00127C20 File Offset: 0x00125E20
		public virtual int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) == 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06005751 RID: 22353
		public abstract void Write(byte[] buffer, int offset, int count);

		// Token: 0x06005752 RID: 22354 RVA: 0x00127C44 File Offset: 0x00125E44
		public virtual void Write(ReadOnlySpan<byte> buffer)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			try
			{
				buffer.CopyTo(array);
				this.Write(array, 0, buffer.Length);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06005753 RID: 22355 RVA: 0x00127CA0 File Offset: 0x00125EA0
		public virtual void WriteByte(byte value)
		{
			this.Write(new byte[] { value }, 0, 1);
		}

		// Token: 0x06005754 RID: 22356 RVA: 0x00127CC1 File Offset: 0x00125EC1
		public static Stream Synchronized(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (stream is Stream.SyncStream)
			{
				return stream;
			}
			return new Stream.SyncStream(stream);
		}

		// Token: 0x06005755 RID: 22357 RVA: 0x00004088 File Offset: 0x00002288
		[Obsolete("Do not call or override this method.")]
		protected virtual void ObjectInvariant()
		{
		}

		// Token: 0x06005756 RID: 22358 RVA: 0x00127CE4 File Offset: 0x00125EE4
		internal IAsyncResult BlockingBeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			Stream.SynchronousAsyncResult synchronousAsyncResult;
			try
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(this.Read(buffer, offset, count), state);
			}
			catch (IOException ex)
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(ex, state, false);
			}
			if (callback != null)
			{
				callback(synchronousAsyncResult);
			}
			return synchronousAsyncResult;
		}

		// Token: 0x06005757 RID: 22359 RVA: 0x00127D2C File Offset: 0x00125F2C
		internal static int BlockingEndRead(IAsyncResult asyncResult)
		{
			return Stream.SynchronousAsyncResult.EndRead(asyncResult);
		}

		// Token: 0x06005758 RID: 22360 RVA: 0x00127D34 File Offset: 0x00125F34
		internal IAsyncResult BlockingBeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			Stream.SynchronousAsyncResult synchronousAsyncResult;
			try
			{
				this.Write(buffer, offset, count);
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(state);
			}
			catch (IOException ex)
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(ex, state, true);
			}
			if (callback != null)
			{
				callback(synchronousAsyncResult);
			}
			return synchronousAsyncResult;
		}

		// Token: 0x06005759 RID: 22361 RVA: 0x00127D7C File Offset: 0x00125F7C
		internal static void BlockingEndWrite(IAsyncResult asyncResult)
		{
			Stream.SynchronousAsyncResult.EndWrite(asyncResult);
		}

		// Token: 0x0600575A RID: 22362 RVA: 0x00003FB7 File Offset: 0x000021B7
		private bool HasOverriddenBeginEndRead()
		{
			return true;
		}

		// Token: 0x0600575B RID: 22363 RVA: 0x00003FB7 File Offset: 0x000021B7
		private bool HasOverriddenBeginEndWrite()
		{
			return true;
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x00127D84 File Offset: 0x00125F84
		public virtual ValueTask DisposeAsync()
		{
			ValueTask valueTask;
			try
			{
				this.Dispose();
				valueTask = default(ValueTask);
				valueTask = valueTask;
			}
			catch (Exception ex)
			{
				valueTask = new ValueTask(Task.FromException(ex));
			}
			return valueTask;
		}

		// Token: 0x0600575D RID: 22365 RVA: 0x000543BD File Offset: 0x000525BD
		protected Stream()
		{
		}

		// Token: 0x0600575E RID: 22366 RVA: 0x00127DC4 File Offset: 0x00125FC4
		// Note: this type is marked as 'beforefieldinit'.
		static Stream()
		{
		}

		// Token: 0x0600575F RID: 22367 RVA: 0x00127DD0 File Offset: 0x00125FD0
		[CompilerGenerated]
		internal static async ValueTask<int> <ReadAsync>g__FinishReadAsync|44_0(Task<int> readTask, byte[] localBuffer, Memory<byte> localDestination)
		{
			int num2;
			try
			{
				int num = await readTask.ConfigureAwait(false);
				new Span<byte>(localBuffer, 0, num).CopyTo(localDestination.Span);
				num2 = num;
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(localBuffer, false);
			}
			return num2;
		}

		// Token: 0x040034B4 RID: 13492
		public static readonly Stream Null = new Stream.NullStream();

		// Token: 0x040034B5 RID: 13493
		private const int DefaultCopyBufferSize = 81920;

		// Token: 0x040034B6 RID: 13494
		[NonSerialized]
		private Stream.ReadWriteTask _activeReadWriteTask;

		// Token: 0x040034B7 RID: 13495
		[NonSerialized]
		private SemaphoreSlim _asyncActiveSemaphore;

		// Token: 0x0200096B RID: 2411
		private struct ReadWriteParameters
		{
			// Token: 0x040034B8 RID: 13496
			internal byte[] Buffer;

			// Token: 0x040034B9 RID: 13497
			internal int Offset;

			// Token: 0x040034BA RID: 13498
			internal int Count;
		}

		// Token: 0x0200096C RID: 2412
		private sealed class ReadWriteTask : Task<int>, ITaskCompletionAction
		{
			// Token: 0x06005760 RID: 22368 RVA: 0x00127E23 File Offset: 0x00126023
			internal void ClearBeginState()
			{
				this._stream = null;
				this._buffer = null;
			}

			// Token: 0x06005761 RID: 22369 RVA: 0x00127E34 File Offset: 0x00126034
			public ReadWriteTask(bool isRead, bool apm, Func<object, int> function, object state, Stream stream, byte[] buffer, int offset, int count, AsyncCallback callback)
				: base(function, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach)
			{
				this._isRead = isRead;
				this._apm = apm;
				this._stream = stream;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
				if (callback != null)
				{
					this._callback = callback;
					this._context = ExecutionContext.Capture();
					base.AddCompletionAction(this);
				}
			}

			// Token: 0x06005762 RID: 22370 RVA: 0x00127E9C File Offset: 0x0012609C
			private static void InvokeAsyncCallback(object completedTask)
			{
				Stream.ReadWriteTask readWriteTask = (Stream.ReadWriteTask)completedTask;
				AsyncCallback callback = readWriteTask._callback;
				readWriteTask._callback = null;
				callback(readWriteTask);
			}

			// Token: 0x06005763 RID: 22371 RVA: 0x00127EC4 File Offset: 0x001260C4
			void ITaskCompletionAction.Invoke(Task completingTask)
			{
				ExecutionContext context = this._context;
				if (context == null)
				{
					AsyncCallback callback = this._callback;
					this._callback = null;
					callback(completingTask);
					return;
				}
				this._context = null;
				ContextCallback contextCallback = Stream.ReadWriteTask.s_invokeAsyncCallback;
				if (contextCallback == null)
				{
					contextCallback = (Stream.ReadWriteTask.s_invokeAsyncCallback = new ContextCallback(Stream.ReadWriteTask.InvokeAsyncCallback));
				}
				ExecutionContext.RunInternal(context, contextCallback, this);
			}

			// Token: 0x17000E3E RID: 3646
			// (get) Token: 0x06005764 RID: 22372 RVA: 0x00003FB7 File Offset: 0x000021B7
			bool ITaskCompletionAction.InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x040034BB RID: 13499
			internal readonly bool _isRead;

			// Token: 0x040034BC RID: 13500
			internal readonly bool _apm;

			// Token: 0x040034BD RID: 13501
			internal Stream _stream;

			// Token: 0x040034BE RID: 13502
			internal byte[] _buffer;

			// Token: 0x040034BF RID: 13503
			internal readonly int _offset;

			// Token: 0x040034C0 RID: 13504
			internal readonly int _count;

			// Token: 0x040034C1 RID: 13505
			private AsyncCallback _callback;

			// Token: 0x040034C2 RID: 13506
			private ExecutionContext _context;

			// Token: 0x040034C3 RID: 13507
			private static ContextCallback s_invokeAsyncCallback;
		}

		// Token: 0x0200096D RID: 2413
		private sealed class NullStream : Stream
		{
			// Token: 0x06005765 RID: 22373 RVA: 0x00127F1A File Offset: 0x0012611A
			internal NullStream()
			{
			}

			// Token: 0x17000E3F RID: 3647
			// (get) Token: 0x06005766 RID: 22374 RVA: 0x00003FB7 File Offset: 0x000021B7
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000E40 RID: 3648
			// (get) Token: 0x06005767 RID: 22375 RVA: 0x00003FB7 File Offset: 0x000021B7
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000E41 RID: 3649
			// (get) Token: 0x06005768 RID: 22376 RVA: 0x00003FB7 File Offset: 0x000021B7
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000E42 RID: 3650
			// (get) Token: 0x06005769 RID: 22377 RVA: 0x0000408D File Offset: 0x0000228D
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x17000E43 RID: 3651
			// (get) Token: 0x0600576A RID: 22378 RVA: 0x0000408D File Offset: 0x0000228D
			// (set) Token: 0x0600576B RID: 22379 RVA: 0x00004088 File Offset: 0x00002288
			public override long Position
			{
				get
				{
					return 0L;
				}
				set
				{
				}
			}

			// Token: 0x0600576C RID: 22380 RVA: 0x00127F22 File Offset: 0x00126122
			public override void CopyTo(Stream destination, int bufferSize)
			{
				StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			}

			// Token: 0x0600576D RID: 22381 RVA: 0x00127F2C File Offset: 0x0012612C
			public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
			{
				StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCanceled(cancellationToken);
			}

			// Token: 0x0600576E RID: 22382 RVA: 0x00004088 File Offset: 0x00002288
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x0600576F RID: 22383 RVA: 0x00004088 File Offset: 0x00002288
			public override void Flush()
			{
			}

			// Token: 0x06005770 RID: 22384 RVA: 0x00127F4B File Offset: 0x0012614B
			public override Task FlushAsync(CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCanceled(cancellationToken);
			}

			// Token: 0x06005771 RID: 22385 RVA: 0x00127F62 File Offset: 0x00126162
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (!this.CanRead)
				{
					throw Error.GetReadNotSupported();
				}
				return base.BlockingBeginRead(buffer, offset, count, callback, state);
			}

			// Token: 0x06005772 RID: 22386 RVA: 0x00127F7F File Offset: 0x0012617F
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				return Stream.BlockingEndRead(asyncResult);
			}

			// Token: 0x06005773 RID: 22387 RVA: 0x00127F95 File Offset: 0x00126195
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (!this.CanWrite)
				{
					throw Error.GetWriteNotSupported();
				}
				return base.BlockingBeginWrite(buffer, offset, count, callback, state);
			}

			// Token: 0x06005774 RID: 22388 RVA: 0x00127FB2 File Offset: 0x001261B2
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream.BlockingEndWrite(asyncResult);
			}

			// Token: 0x06005775 RID: 22389 RVA: 0x0000408A File Offset: 0x0000228A
			public override int Read(byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x06005776 RID: 22390 RVA: 0x0000408A File Offset: 0x0000228A
			public override int Read(Span<byte> buffer)
			{
				return 0;
			}

			// Token: 0x06005777 RID: 22391 RVA: 0x00127FC8 File Offset: 0x001261C8
			public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				return Stream.NullStream.s_zeroTask;
			}

			// Token: 0x06005778 RID: 22392 RVA: 0x00127FCF File Offset: 0x001261CF
			public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
			{
				return new ValueTask<int>(0);
			}

			// Token: 0x06005779 RID: 22393 RVA: 0x0011B48C File Offset: 0x0011968C
			public override int ReadByte()
			{
				return -1;
			}

			// Token: 0x0600577A RID: 22394 RVA: 0x00004088 File Offset: 0x00002288
			public override void Write(byte[] buffer, int offset, int count)
			{
			}

			// Token: 0x0600577B RID: 22395 RVA: 0x00004088 File Offset: 0x00002288
			public override void Write(ReadOnlySpan<byte> buffer)
			{
			}

			// Token: 0x0600577C RID: 22396 RVA: 0x00127FD7 File Offset: 0x001261D7
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCanceled(cancellationToken);
			}

			// Token: 0x0600577D RID: 22397 RVA: 0x00127FF0 File Offset: 0x001261F0
			public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return default(ValueTask);
				}
				return new ValueTask(Task.FromCanceled(cancellationToken));
			}

			// Token: 0x0600577E RID: 22398 RVA: 0x00004088 File Offset: 0x00002288
			public override void WriteByte(byte value)
			{
			}

			// Token: 0x0600577F RID: 22399 RVA: 0x0000408D File Offset: 0x0000228D
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x06005780 RID: 22400 RVA: 0x00004088 File Offset: 0x00002288
			public override void SetLength(long length)
			{
			}

			// Token: 0x06005781 RID: 22401 RVA: 0x0012801B File Offset: 0x0012621B
			// Note: this type is marked as 'beforefieldinit'.
			static NullStream()
			{
			}

			// Token: 0x040034C4 RID: 13508
			private static readonly Task<int> s_zeroTask = Task.FromResult<int>(0);
		}

		// Token: 0x0200096E RID: 2414
		private sealed class SynchronousAsyncResult : IAsyncResult
		{
			// Token: 0x06005782 RID: 22402 RVA: 0x00128028 File Offset: 0x00126228
			internal SynchronousAsyncResult(int bytesRead, object asyncStateObject)
			{
				this._bytesRead = bytesRead;
				this._stateObject = asyncStateObject;
			}

			// Token: 0x06005783 RID: 22403 RVA: 0x0012803E File Offset: 0x0012623E
			internal SynchronousAsyncResult(object asyncStateObject)
			{
				this._stateObject = asyncStateObject;
				this._isWrite = true;
			}

			// Token: 0x06005784 RID: 22404 RVA: 0x00128054 File Offset: 0x00126254
			internal SynchronousAsyncResult(Exception ex, object asyncStateObject, bool isWrite)
			{
				this._exceptionInfo = ExceptionDispatchInfo.Capture(ex);
				this._stateObject = asyncStateObject;
				this._isWrite = isWrite;
			}

			// Token: 0x17000E44 RID: 3652
			// (get) Token: 0x06005785 RID: 22405 RVA: 0x00003FB7 File Offset: 0x000021B7
			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000E45 RID: 3653
			// (get) Token: 0x06005786 RID: 22406 RVA: 0x00128076 File Offset: 0x00126276
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return LazyInitializer.EnsureInitialized<ManualResetEvent>(ref this._waitHandle, () => new ManualResetEvent(true));
				}
			}

			// Token: 0x17000E46 RID: 3654
			// (get) Token: 0x06005787 RID: 22407 RVA: 0x001280A2 File Offset: 0x001262A2
			public object AsyncState
			{
				get
				{
					return this._stateObject;
				}
			}

			// Token: 0x17000E47 RID: 3655
			// (get) Token: 0x06005788 RID: 22408 RVA: 0x00003FB7 File Offset: 0x000021B7
			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06005789 RID: 22409 RVA: 0x001280AA File Offset: 0x001262AA
			internal void ThrowIfError()
			{
				if (this._exceptionInfo != null)
				{
					this._exceptionInfo.Throw();
				}
			}

			// Token: 0x0600578A RID: 22410 RVA: 0x001280C0 File Offset: 0x001262C0
			internal static int EndRead(IAsyncResult asyncResult)
			{
				Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
				if (synchronousAsyncResult == null || synchronousAsyncResult._isWrite)
				{
					throw new ArgumentException("IAsyncResult object did not come from the corresponding async method on this type.");
				}
				if (synchronousAsyncResult._endXxxCalled)
				{
					throw new ArgumentException("EndRead can only be called once for each asynchronous operation.");
				}
				synchronousAsyncResult._endXxxCalled = true;
				synchronousAsyncResult.ThrowIfError();
				return synchronousAsyncResult._bytesRead;
			}

			// Token: 0x0600578B RID: 22411 RVA: 0x00128110 File Offset: 0x00126310
			internal static void EndWrite(IAsyncResult asyncResult)
			{
				Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
				if (synchronousAsyncResult == null || !synchronousAsyncResult._isWrite)
				{
					throw new ArgumentException("IAsyncResult object did not come from the corresponding async method on this type.");
				}
				if (synchronousAsyncResult._endXxxCalled)
				{
					throw new ArgumentException("EndWrite can only be called once for each asynchronous operation.");
				}
				synchronousAsyncResult._endXxxCalled = true;
				synchronousAsyncResult.ThrowIfError();
			}

			// Token: 0x040034C5 RID: 13509
			private readonly object _stateObject;

			// Token: 0x040034C6 RID: 13510
			private readonly bool _isWrite;

			// Token: 0x040034C7 RID: 13511
			private ManualResetEvent _waitHandle;

			// Token: 0x040034C8 RID: 13512
			private ExceptionDispatchInfo _exceptionInfo;

			// Token: 0x040034C9 RID: 13513
			private bool _endXxxCalled;

			// Token: 0x040034CA RID: 13514
			private int _bytesRead;

			// Token: 0x0200096F RID: 2415
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x0600578C RID: 22412 RVA: 0x0012815A File Offset: 0x0012635A
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x0600578D RID: 22413 RVA: 0x000025BE File Offset: 0x000007BE
				public <>c()
				{
				}

				// Token: 0x0600578E RID: 22414 RVA: 0x00128166 File Offset: 0x00126366
				internal ManualResetEvent <get_AsyncWaitHandle>b__12_0()
				{
					return new ManualResetEvent(true);
				}

				// Token: 0x040034CB RID: 13515
				public static readonly Stream.SynchronousAsyncResult.<>c <>9 = new Stream.SynchronousAsyncResult.<>c();

				// Token: 0x040034CC RID: 13516
				public static Func<ManualResetEvent> <>9__12_0;
			}
		}

		// Token: 0x02000970 RID: 2416
		private sealed class SyncStream : Stream, IDisposable
		{
			// Token: 0x0600578F RID: 22415 RVA: 0x0012816E File Offset: 0x0012636E
			internal SyncStream(Stream stream)
			{
				if (stream == null)
				{
					throw new ArgumentNullException("stream");
				}
				this._stream = stream;
			}

			// Token: 0x17000E48 RID: 3656
			// (get) Token: 0x06005790 RID: 22416 RVA: 0x0012818B File Offset: 0x0012638B
			public override bool CanRead
			{
				get
				{
					return this._stream.CanRead;
				}
			}

			// Token: 0x17000E49 RID: 3657
			// (get) Token: 0x06005791 RID: 22417 RVA: 0x00128198 File Offset: 0x00126398
			public override bool CanWrite
			{
				get
				{
					return this._stream.CanWrite;
				}
			}

			// Token: 0x17000E4A RID: 3658
			// (get) Token: 0x06005792 RID: 22418 RVA: 0x001281A5 File Offset: 0x001263A5
			public override bool CanSeek
			{
				get
				{
					return this._stream.CanSeek;
				}
			}

			// Token: 0x17000E4B RID: 3659
			// (get) Token: 0x06005793 RID: 22419 RVA: 0x001281B2 File Offset: 0x001263B2
			public override bool CanTimeout
			{
				get
				{
					return this._stream.CanTimeout;
				}
			}

			// Token: 0x17000E4C RID: 3660
			// (get) Token: 0x06005794 RID: 22420 RVA: 0x001281C0 File Offset: 0x001263C0
			public override long Length
			{
				get
				{
					Stream stream = this._stream;
					long length;
					lock (stream)
					{
						length = this._stream.Length;
					}
					return length;
				}
			}

			// Token: 0x17000E4D RID: 3661
			// (get) Token: 0x06005795 RID: 22421 RVA: 0x00128208 File Offset: 0x00126408
			// (set) Token: 0x06005796 RID: 22422 RVA: 0x00128250 File Offset: 0x00126450
			public override long Position
			{
				get
				{
					Stream stream = this._stream;
					long position;
					lock (stream)
					{
						position = this._stream.Position;
					}
					return position;
				}
				set
				{
					Stream stream = this._stream;
					lock (stream)
					{
						this._stream.Position = value;
					}
				}
			}

			// Token: 0x17000E4E RID: 3662
			// (get) Token: 0x06005797 RID: 22423 RVA: 0x00128298 File Offset: 0x00126498
			// (set) Token: 0x06005798 RID: 22424 RVA: 0x001282A5 File Offset: 0x001264A5
			public override int ReadTimeout
			{
				get
				{
					return this._stream.ReadTimeout;
				}
				set
				{
					this._stream.ReadTimeout = value;
				}
			}

			// Token: 0x17000E4F RID: 3663
			// (get) Token: 0x06005799 RID: 22425 RVA: 0x001282B3 File Offset: 0x001264B3
			// (set) Token: 0x0600579A RID: 22426 RVA: 0x001282C0 File Offset: 0x001264C0
			public override int WriteTimeout
			{
				get
				{
					return this._stream.WriteTimeout;
				}
				set
				{
					this._stream.WriteTimeout = value;
				}
			}

			// Token: 0x0600579B RID: 22427 RVA: 0x001282D0 File Offset: 0x001264D0
			public override void Close()
			{
				Stream stream = this._stream;
				lock (stream)
				{
					try
					{
						this._stream.Close();
					}
					finally
					{
						base.Dispose(true);
					}
				}
			}

			// Token: 0x0600579C RID: 22428 RVA: 0x0012832C File Offset: 0x0012652C
			protected override void Dispose(bool disposing)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					try
					{
						if (disposing)
						{
							((IDisposable)this._stream).Dispose();
						}
					}
					finally
					{
						base.Dispose(disposing);
					}
				}
			}

			// Token: 0x0600579D RID: 22429 RVA: 0x00128388 File Offset: 0x00126588
			public override void Flush()
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Flush();
				}
			}

			// Token: 0x0600579E RID: 22430 RVA: 0x001283D0 File Offset: 0x001265D0
			public override int Read(byte[] bytes, int offset, int count)
			{
				Stream stream = this._stream;
				int num;
				lock (stream)
				{
					num = this._stream.Read(bytes, offset, count);
				}
				return num;
			}

			// Token: 0x0600579F RID: 22431 RVA: 0x0012841C File Offset: 0x0012661C
			public override int Read(Span<byte> buffer)
			{
				Stream stream = this._stream;
				int num;
				lock (stream)
				{
					num = this._stream.Read(buffer);
				}
				return num;
			}

			// Token: 0x060057A0 RID: 22432 RVA: 0x00128464 File Offset: 0x00126664
			public override int ReadByte()
			{
				Stream stream = this._stream;
				int num;
				lock (stream)
				{
					num = this._stream.ReadByte();
				}
				return num;
			}

			// Token: 0x060057A1 RID: 22433 RVA: 0x001284AC File Offset: 0x001266AC
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				bool flag = this._stream.HasOverriddenBeginEndRead();
				Stream stream = this._stream;
				IAsyncResult asyncResult;
				lock (stream)
				{
					asyncResult = (flag ? this._stream.BeginRead(buffer, offset, count, callback, state) : this._stream.BeginReadInternal(buffer, offset, count, callback, state, true, true));
				}
				return asyncResult;
			}

			// Token: 0x060057A2 RID: 22434 RVA: 0x00128520 File Offset: 0x00126720
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream stream = this._stream;
				int num;
				lock (stream)
				{
					num = this._stream.EndRead(asyncResult);
				}
				return num;
			}

			// Token: 0x060057A3 RID: 22435 RVA: 0x00128578 File Offset: 0x00126778
			public override long Seek(long offset, SeekOrigin origin)
			{
				Stream stream = this._stream;
				long num;
				lock (stream)
				{
					num = this._stream.Seek(offset, origin);
				}
				return num;
			}

			// Token: 0x060057A4 RID: 22436 RVA: 0x001285C4 File Offset: 0x001267C4
			public override void SetLength(long length)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.SetLength(length);
				}
			}

			// Token: 0x060057A5 RID: 22437 RVA: 0x0012860C File Offset: 0x0012680C
			public override void Write(byte[] bytes, int offset, int count)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Write(bytes, offset, count);
				}
			}

			// Token: 0x060057A6 RID: 22438 RVA: 0x00128654 File Offset: 0x00126854
			public override void Write(ReadOnlySpan<byte> buffer)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Write(buffer);
				}
			}

			// Token: 0x060057A7 RID: 22439 RVA: 0x0012869C File Offset: 0x0012689C
			public override void WriteByte(byte b)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.WriteByte(b);
				}
			}

			// Token: 0x060057A8 RID: 22440 RVA: 0x001286E4 File Offset: 0x001268E4
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				bool flag = this._stream.HasOverriddenBeginEndWrite();
				Stream stream = this._stream;
				IAsyncResult asyncResult;
				lock (stream)
				{
					asyncResult = (flag ? this._stream.BeginWrite(buffer, offset, count, callback, state) : this._stream.BeginWriteInternal(buffer, offset, count, callback, state, true, true));
				}
				return asyncResult;
			}

			// Token: 0x060057A9 RID: 22441 RVA: 0x00128758 File Offset: 0x00126958
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.EndWrite(asyncResult);
				}
			}

			// Token: 0x040034CD RID: 13517
			private Stream _stream;
		}

		// Token: 0x02000971 RID: 2417
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060057AA RID: 22442 RVA: 0x001287AC File Offset: 0x001269AC
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060057AB RID: 22443 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x060057AC RID: 22444 RVA: 0x000A5ABE File Offset: 0x000A3CBE
			internal SemaphoreSlim <EnsureAsyncActiveSemaphoreInitialized>b__4_0()
			{
				return new SemaphoreSlim(1, 1);
			}

			// Token: 0x060057AD RID: 22445 RVA: 0x001287B8 File Offset: 0x001269B8
			internal void <FlushAsync>b__37_0(object state)
			{
				((Stream)state).Flush();
			}

			// Token: 0x060057AE RID: 22446 RVA: 0x001287C8 File Offset: 0x001269C8
			internal int <BeginReadInternal>b__40_0(object <p0>)
			{
				Stream.ReadWriteTask readWriteTask = Task.InternalCurrent as Stream.ReadWriteTask;
				int num;
				try
				{
					num = readWriteTask._stream.Read(readWriteTask._buffer, readWriteTask._offset, readWriteTask._count);
				}
				finally
				{
					if (!readWriteTask._apm)
					{
						readWriteTask._stream.FinishTrackingAsyncOperation();
					}
					readWriteTask.ClearBeginState();
				}
				return num;
			}

			// Token: 0x060057AF RID: 22447 RVA: 0x0012882C File Offset: 0x00126A2C
			internal IAsyncResult <BeginEndReadAsync>b__45_0(Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state)
			{
				return stream.BeginRead(args.Buffer, args.Offset, args.Count, callback, state);
			}

			// Token: 0x060057B0 RID: 22448 RVA: 0x00128849 File Offset: 0x00126A49
			internal int <BeginEndReadAsync>b__45_1(Stream stream, IAsyncResult asyncResult)
			{
				return stream.EndRead(asyncResult);
			}

			// Token: 0x060057B1 RID: 22449 RVA: 0x00128854 File Offset: 0x00126A54
			internal int <BeginWriteInternal>b__48_0(object <p0>)
			{
				Stream.ReadWriteTask readWriteTask = Task.InternalCurrent as Stream.ReadWriteTask;
				int num;
				try
				{
					readWriteTask._stream.Write(readWriteTask._buffer, readWriteTask._offset, readWriteTask._count);
					num = 0;
				}
				finally
				{
					if (!readWriteTask._apm)
					{
						readWriteTask._stream.FinishTrackingAsyncOperation();
					}
					readWriteTask.ClearBeginState();
				}
				return num;
			}

			// Token: 0x060057B2 RID: 22450 RVA: 0x001288B8 File Offset: 0x00126AB8
			internal void <RunReadWriteTaskWhenReady>b__49_0(Task t, object state)
			{
				Stream.ReadWriteTask readWriteTask = (Stream.ReadWriteTask)state;
				readWriteTask._stream.RunReadWriteTask(readWriteTask);
			}

			// Token: 0x060057B3 RID: 22451 RVA: 0x001288D8 File Offset: 0x00126AD8
			internal IAsyncResult <BeginEndWriteAsync>b__58_0(Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state)
			{
				return stream.BeginWrite(args.Buffer, args.Offset, args.Count, callback, state);
			}

			// Token: 0x060057B4 RID: 22452 RVA: 0x001288F8 File Offset: 0x00126AF8
			internal VoidTaskResult <BeginEndWriteAsync>b__58_1(Stream stream, IAsyncResult asyncResult)
			{
				stream.EndWrite(asyncResult);
				return default(VoidTaskResult);
			}

			// Token: 0x040034CE RID: 13518
			public static readonly Stream.<>c <>9 = new Stream.<>c();

			// Token: 0x040034CF RID: 13519
			public static Func<SemaphoreSlim> <>9__4_0;

			// Token: 0x040034D0 RID: 13520
			public static Action<object> <>9__37_0;

			// Token: 0x040034D1 RID: 13521
			public static Func<object, int> <>9__40_0;

			// Token: 0x040034D2 RID: 13522
			public static Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult> <>9__45_0;

			// Token: 0x040034D3 RID: 13523
			public static Func<Stream, IAsyncResult, int> <>9__45_1;

			// Token: 0x040034D4 RID: 13524
			public static Func<object, int> <>9__48_0;

			// Token: 0x040034D5 RID: 13525
			public static Action<Task, object> <>9__49_0;

			// Token: 0x040034D6 RID: 13526
			public static Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult> <>9__58_0;

			// Token: 0x040034D7 RID: 13527
			public static Func<Stream, IAsyncResult, VoidTaskResult> <>9__58_1;
		}

		// Token: 0x02000972 RID: 2418
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CopyToAsyncInternal>d__28 : IAsyncStateMachine
		{
			// Token: 0x060057B5 RID: 22453 RVA: 0x00128918 File Offset: 0x00126B18
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Stream stream = this;
				try
				{
					if (num > 1)
					{
						buffer = ArrayPool<byte>.Shared.Rent(bufferSize);
					}
					try
					{
						ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
						if (num == 0)
						{
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
							num = (num2 = -1);
							goto IL_00A6;
						}
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter3;
						if (num == 1)
						{
							ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter4;
							configuredValueTaskAwaiter3 = configuredValueTaskAwaiter4;
							configuredValueTaskAwaiter4 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (num2 = -1);
							goto IL_012E;
						}
						IL_0033:
						configuredValueTaskAwaiter = stream.ReadAsync(new Memory<byte>(buffer), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredValueTaskAwaiter.IsCompleted)
						{
							num = (num2 = 0);
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, Stream.<CopyToAsyncInternal>d__28>(ref configuredValueTaskAwaiter, ref this);
							return;
						}
						IL_00A6:
						int result = configuredValueTaskAwaiter.GetResult();
						if (result == 0)
						{
							goto IL_0152;
						}
						configuredValueTaskAwaiter3 = destination.WriteAsync(new ReadOnlyMemory<byte>(buffer, 0, result), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredValueTaskAwaiter3.IsCompleted)
						{
							num = (num2 = 1);
							ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter4 = configuredValueTaskAwaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, Stream.<CopyToAsyncInternal>d__28>(ref configuredValueTaskAwaiter3, ref this);
							return;
						}
						IL_012E:
						configuredValueTaskAwaiter3.GetResult();
						goto IL_0033;
					}
					finally
					{
						if (num < 0)
						{
							ArrayPool<byte>.Shared.Return(buffer, false);
						}
					}
					IL_0152:;
				}
				catch (Exception ex)
				{
					num2 = -2;
					buffer = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				buffer = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060057B6 RID: 22454 RVA: 0x00128AE8 File Offset: 0x00126CE8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040034D8 RID: 13528
			public int <>1__state;

			// Token: 0x040034D9 RID: 13529
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040034DA RID: 13530
			public int bufferSize;

			// Token: 0x040034DB RID: 13531
			public Stream <>4__this;

			// Token: 0x040034DC RID: 13532
			public CancellationToken cancellationToken;

			// Token: 0x040034DD RID: 13533
			public Stream destination;

			// Token: 0x040034DE RID: 13534
			private byte[] <buffer>5__2;

			// Token: 0x040034DF RID: 13535
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__1;

			// Token: 0x040034E0 RID: 13536
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__2;
		}

		// Token: 0x02000973 RID: 2419
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FinishWriteAsync>d__57 : IAsyncStateMachine
		{
			// Token: 0x060057B7 RID: 22455 RVA: 0x00128AF8 File Offset: 0x00126CF8
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
						if (num != 0)
						{
							configuredTaskAwaiter = writeTask.ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								num = (num2 = 0);
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, Stream.<FinishWriteAsync>d__57>(ref configuredTaskAwaiter, ref this);
								return;
							}
						}
						else
						{
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (num2 = -1);
						}
						configuredTaskAwaiter.GetResult();
					}
					finally
					{
						if (num < 0)
						{
							ArrayPool<byte>.Shared.Return(localBuffer, false);
						}
					}
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060057B8 RID: 22456 RVA: 0x00128BD4 File Offset: 0x00126DD4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040034E1 RID: 13537
			public int <>1__state;

			// Token: 0x040034E2 RID: 13538
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040034E3 RID: 13539
			public Task writeTask;

			// Token: 0x040034E4 RID: 13540
			public byte[] localBuffer;

			// Token: 0x040034E5 RID: 13541
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000974 RID: 2420
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <<ReadAsync>g__FinishReadAsync|44_0>d : IAsyncStateMachine
		{
			// Token: 0x060057B9 RID: 22457 RVA: 0x00128BE4 File Offset: 0x00126DE4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				int num3;
				try
				{
					try
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter;
						if (num != 0)
						{
							configuredTaskAwaiter = readTask.ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								num = (num2 = 0);
								ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, Stream.<<ReadAsync>g__FinishReadAsync|44_0>d>(ref configuredTaskAwaiter, ref this);
								return;
							}
						}
						else
						{
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							num = (num2 = -1);
						}
						int result = configuredTaskAwaiter.GetResult();
						new Span<byte>(localBuffer, 0, result).CopyTo(localDestination.Span);
						num3 = result;
					}
					finally
					{
						if (num < 0)
						{
							ArrayPool<byte>.Shared.Return(localBuffer, false);
						}
					}
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult(num3);
			}

			// Token: 0x060057BA RID: 22458 RVA: 0x00128CE8 File Offset: 0x00126EE8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040034E6 RID: 13542
			public int <>1__state;

			// Token: 0x040034E7 RID: 13543
			public AsyncValueTaskMethodBuilder<int> <>t__builder;

			// Token: 0x040034E8 RID: 13544
			public Task<int> readTask;

			// Token: 0x040034E9 RID: 13545
			public byte[] localBuffer;

			// Token: 0x040034EA RID: 13546
			public Memory<byte> localDestination;

			// Token: 0x040034EB RID: 13547
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
