using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x02000961 RID: 2401
	public sealed class BufferedStream : Stream
	{
		// Token: 0x060056D9 RID: 22233 RVA: 0x001251AB File Offset: 0x001233AB
		internal SemaphoreSlim LazyEnsureAsyncActiveSemaphoreInitialized()
		{
			return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._asyncActiveSemaphore, () => new SemaphoreSlim(1, 1));
		}

		// Token: 0x060056DA RID: 22234 RVA: 0x001251D7 File Offset: 0x001233D7
		public BufferedStream(Stream stream)
			: this(stream, 4096)
		{
		}

		// Token: 0x060056DB RID: 22235 RVA: 0x001251E8 File Offset: 0x001233E8
		public BufferedStream(Stream stream, int bufferSize)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", SR.Format("'{0}' must be greater than zero.", "bufferSize"));
			}
			this._stream = stream;
			this._bufferSize = bufferSize;
			if (!this._stream.CanRead && !this._stream.CanWrite)
			{
				throw new ObjectDisposedException(null, "Cannot access a closed Stream.");
			}
		}

		// Token: 0x060056DC RID: 22236 RVA: 0x0012525B File Offset: 0x0012345B
		private void EnsureNotClosed()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException(null, "Cannot access a closed Stream.");
			}
		}

		// Token: 0x060056DD RID: 22237 RVA: 0x00125271 File Offset: 0x00123471
		private void EnsureCanSeek()
		{
			if (!this._stream.CanSeek)
			{
				throw new NotSupportedException("Stream does not support seeking.");
			}
		}

		// Token: 0x060056DE RID: 22238 RVA: 0x0012528B File Offset: 0x0012348B
		private void EnsureCanRead()
		{
			if (!this._stream.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading.");
			}
		}

		// Token: 0x060056DF RID: 22239 RVA: 0x001252A5 File Offset: 0x001234A5
		private void EnsureCanWrite()
		{
			if (!this._stream.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing.");
			}
		}

		// Token: 0x060056E0 RID: 22240 RVA: 0x001252C0 File Offset: 0x001234C0
		private void EnsureShadowBufferAllocated()
		{
			if (this._buffer.Length != this._bufferSize || this._bufferSize >= 81920)
			{
				return;
			}
			byte[] array = new byte[Math.Min(this._bufferSize + this._bufferSize, 81920)];
			Buffer.BlockCopy(this._buffer, 0, array, 0, this._writePos);
			this._buffer = array;
		}

		// Token: 0x060056E1 RID: 22241 RVA: 0x00125323 File Offset: 0x00123523
		private void EnsureBufferAllocated()
		{
			if (this._buffer == null)
			{
				this._buffer = new byte[this._bufferSize];
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060056E2 RID: 22242 RVA: 0x0012533E File Offset: 0x0012353E
		public Stream UnderlyingStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060056E3 RID: 22243 RVA: 0x00125346 File Offset: 0x00123546
		public int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060056E4 RID: 22244 RVA: 0x0012534E File Offset: 0x0012354E
		public override bool CanRead
		{
			get
			{
				return this._stream != null && this._stream.CanRead;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060056E5 RID: 22245 RVA: 0x00125365 File Offset: 0x00123565
		public override bool CanWrite
		{
			get
			{
				return this._stream != null && this._stream.CanWrite;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x060056E6 RID: 22246 RVA: 0x0012537C File Offset: 0x0012357C
		public override bool CanSeek
		{
			get
			{
				return this._stream != null && this._stream.CanSeek;
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x060056E7 RID: 22247 RVA: 0x00125393 File Offset: 0x00123593
		public override long Length
		{
			get
			{
				this.EnsureNotClosed();
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				return this._stream.Length;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x060056E8 RID: 22248 RVA: 0x001253B5 File Offset: 0x001235B5
		// (set) Token: 0x060056E9 RID: 22249 RVA: 0x001253E4 File Offset: 0x001235E4
		public override long Position
		{
			get
			{
				this.EnsureNotClosed();
				this.EnsureCanSeek();
				return this._stream.Position + (long)(this._readPos - this._readLen + this._writePos);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
				}
				this.EnsureNotClosed();
				this.EnsureCanSeek();
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				this._readPos = 0;
				this._readLen = 0;
				this._stream.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x060056EA RID: 22250 RVA: 0x00125440 File Offset: 0x00123640
		public override async ValueTask DisposeAsync()
		{
			try
			{
				if (this._stream != null)
				{
					object obj = null;
					try
					{
						await base.FlushAsync().ConfigureAwait(false);
					}
					catch (object obj)
					{
					}
					await this._stream.DisposeAsync().ConfigureAwait(false);
					object obj2 = obj;
					if (obj2 != null)
					{
						Exception ex = obj2 as Exception;
						if (ex == null)
						{
							throw obj2;
						}
						ExceptionDispatchInfo.Capture(ex).Throw();
					}
					obj = null;
				}
			}
			finally
			{
				this._stream = null;
				this._buffer = null;
			}
		}

		// Token: 0x060056EB RID: 22251 RVA: 0x00125484 File Offset: 0x00123684
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._stream != null)
				{
					try
					{
						this.Flush();
					}
					finally
					{
						this._stream.Dispose();
					}
				}
			}
			finally
			{
				this._stream = null;
				this._buffer = null;
				base.Dispose(disposing);
			}
		}

		// Token: 0x060056EC RID: 22252 RVA: 0x001254E4 File Offset: 0x001236E4
		public override void Flush()
		{
			this.EnsureNotClosed();
			if (this._writePos > 0)
			{
				this.FlushWrite();
				return;
			}
			if (this._readPos < this._readLen)
			{
				if (this._stream.CanSeek)
				{
					this.FlushRead();
				}
				if (this._stream.CanWrite)
				{
					this._stream.Flush();
				}
				return;
			}
			if (this._stream.CanWrite)
			{
				this._stream.Flush();
			}
			this._writePos = (this._readPos = (this._readLen = 0));
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x00125572 File Offset: 0x00123772
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			this.EnsureNotClosed();
			return this.FlushAsyncInternal(cancellationToken);
		}

		// Token: 0x060056EE RID: 22254 RVA: 0x00125594 File Offset: 0x00123794
		private async Task FlushAsyncInternal(CancellationToken cancellationToken)
		{
			SemaphoreSlim sem = this.LazyEnsureAsyncActiveSemaphoreInitialized();
			await sem.WaitAsync().ConfigureAwait(false);
			try
			{
				if (this._writePos > 0)
				{
					await this.FlushWriteAsync(cancellationToken).ConfigureAwait(false);
				}
				else if (this._readPos < this._readLen)
				{
					if (this._stream.CanSeek)
					{
						this.FlushRead();
					}
					if (this._stream.CanWrite)
					{
						await this._stream.FlushAsync(cancellationToken).ConfigureAwait(false);
					}
				}
				else if (this._stream.CanWrite)
				{
					await this._stream.FlushAsync(cancellationToken).ConfigureAwait(false);
				}
			}
			finally
			{
				sem.Release();
			}
		}

		// Token: 0x060056EF RID: 22255 RVA: 0x001255DF File Offset: 0x001237DF
		private void FlushRead()
		{
			if (this._readPos - this._readLen != 0)
			{
				this._stream.Seek((long)(this._readPos - this._readLen), SeekOrigin.Current);
			}
			this._readPos = 0;
			this._readLen = 0;
		}

		// Token: 0x060056F0 RID: 22256 RVA: 0x0012561C File Offset: 0x0012381C
		private void ClearReadBufferBeforeWrite()
		{
			if (this._readPos == this._readLen)
			{
				this._readPos = (this._readLen = 0);
				return;
			}
			if (!this._stream.CanSeek)
			{
				throw new NotSupportedException("Cannot write to a BufferedStream while the read buffer is not empty if the underlying stream is not seekable. Ensure that the stream underlying this BufferedStream can seek or avoid interleaving read and write operations on this BufferedStream.");
			}
			this.FlushRead();
		}

		// Token: 0x060056F1 RID: 22257 RVA: 0x00125666 File Offset: 0x00123866
		private void FlushWrite()
		{
			this._stream.Write(this._buffer, 0, this._writePos);
			this._writePos = 0;
			this._stream.Flush();
		}

		// Token: 0x060056F2 RID: 22258 RVA: 0x00125694 File Offset: 0x00123894
		private async Task FlushWriteAsync(CancellationToken cancellationToken)
		{
			await this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._buffer, 0, this._writePos), cancellationToken).ConfigureAwait(false);
			this._writePos = 0;
			await this._stream.FlushAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060056F3 RID: 22259 RVA: 0x001256E0 File Offset: 0x001238E0
		private int ReadFromBuffer(byte[] array, int offset, int count)
		{
			int num = this._readLen - this._readPos;
			if (num == 0)
			{
				return 0;
			}
			if (num > count)
			{
				num = count;
			}
			Buffer.BlockCopy(this._buffer, this._readPos, array, offset, num);
			this._readPos += num;
			return num;
		}

		// Token: 0x060056F4 RID: 22260 RVA: 0x0012572C File Offset: 0x0012392C
		private int ReadFromBuffer(Span<byte> destination)
		{
			int num = Math.Min(this._readLen - this._readPos, destination.Length);
			if (num > 0)
			{
				new ReadOnlySpan<byte>(this._buffer, this._readPos, num).CopyTo(destination);
				this._readPos += num;
			}
			return num;
		}

		// Token: 0x060056F5 RID: 22261 RVA: 0x00125784 File Offset: 0x00123984
		private int ReadFromBuffer(byte[] array, int offset, int count, out Exception error)
		{
			int num;
			try
			{
				error = null;
				num = this.ReadFromBuffer(array, offset, count);
			}
			catch (Exception ex)
			{
				error = ex;
				num = 0;
			}
			return num;
		}

		// Token: 0x060056F6 RID: 22262 RVA: 0x001257BC File Offset: 0x001239BC
		public override int Read(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = this.ReadFromBuffer(array, offset, count);
			if (num == count)
			{
				return num;
			}
			int num2 = num;
			if (num > 0)
			{
				count -= num;
				offset += num;
			}
			this._readPos = (this._readLen = 0);
			if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			if (count >= this._bufferSize)
			{
				return this._stream.Read(array, offset, count) + num2;
			}
			this.EnsureBufferAllocated();
			this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
			num = this.ReadFromBuffer(array, offset, count);
			return num + num2;
		}

		// Token: 0x060056F7 RID: 22263 RVA: 0x001258B0 File Offset: 0x00123AB0
		public override int Read(Span<byte> destination)
		{
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = this.ReadFromBuffer(destination);
			if (num == destination.Length)
			{
				return num;
			}
			if (num > 0)
			{
				destination = destination.Slice(num);
			}
			this._readPos = (this._readLen = 0);
			if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			if (destination.Length >= this._bufferSize)
			{
				return this._stream.Read(destination) + num;
			}
			this.EnsureBufferAllocated();
			this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
			return this.ReadFromBuffer(destination) + num;
		}

		// Token: 0x060056F8 RID: 22264 RVA: 0x00125958 File Offset: 0x00123B58
		private Task<int> LastSyncCompletedReadTask(int val)
		{
			Task<int> task = this._lastSyncCompletedReadTask;
			if (task != null && task.Result == val)
			{
				return task;
			}
			task = Task.FromResult<int>(val);
			this._lastSyncCompletedReadTask = task;
			return task;
		}

		// Token: 0x060056F9 RID: 22265 RVA: 0x0012598C File Offset: 0x00123B8C
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = 0;
			SemaphoreSlim semaphoreSlim = this.LazyEnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync();
			if (task.IsCompletedSuccessfully)
			{
				bool flag = true;
				try
				{
					Exception ex;
					num = this.ReadFromBuffer(buffer, offset, count, out ex);
					flag = num == count || ex != null;
					if (flag)
					{
						return (ex == null) ? this.LastSyncCompletedReadTask(num) : Task.FromException<int>(ex);
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.ReadFromUnderlyingStreamAsync(new Memory<byte>(buffer, offset + num, count - num), cancellationToken, num, task).AsTask();
		}

		// Token: 0x060056FA RID: 22266 RVA: 0x00125A94 File Offset: 0x00123C94
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
			}
			this.EnsureNotClosed();
			this.EnsureCanRead();
			int num = 0;
			SemaphoreSlim semaphoreSlim = this.LazyEnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync();
			if (task.IsCompletedSuccessfully)
			{
				bool flag = true;
				try
				{
					num = this.ReadFromBuffer(buffer.Span);
					flag = num == buffer.Length;
					if (flag)
					{
						return new ValueTask<int>(num);
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return this.ReadFromUnderlyingStreamAsync(buffer.Slice(num), cancellationToken, num, task);
		}

		// Token: 0x060056FB RID: 22267 RVA: 0x00125B34 File Offset: 0x00123D34
		private async ValueTask<int> ReadFromUnderlyingStreamAsync(Memory<byte> buffer, CancellationToken cancellationToken, int bytesAlreadySatisfied, Task semaphoreLockTask)
		{
			await semaphoreLockTask.ConfigureAwait(false);
			int num2;
			try
			{
				int num = this.ReadFromBuffer(buffer.Span);
				if (num == buffer.Length)
				{
					num2 = bytesAlreadySatisfied + num;
				}
				else
				{
					if (num > 0)
					{
						buffer = buffer.Slice(num);
						bytesAlreadySatisfied += num;
					}
					int num3 = 0;
					this._readLen = num3;
					this._readPos = num3;
					if (this._writePos > 0)
					{
						await this.FlushWriteAsync(cancellationToken).ConfigureAwait(false);
					}
					if (buffer.Length >= this._bufferSize)
					{
						int num4 = bytesAlreadySatisfied;
						num2 = num4 + await this._stream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);
					}
					else
					{
						this.EnsureBufferAllocated();
						this._readLen = await this._stream.ReadAsync(new Memory<byte>(this._buffer, 0, this._bufferSize), cancellationToken).ConfigureAwait(false);
						num2 = bytesAlreadySatisfied + this.ReadFromBuffer(buffer.Span);
					}
				}
			}
			finally
			{
				this.LazyEnsureAsyncActiveSemaphoreInitialized().Release();
			}
			return num2;
		}

		// Token: 0x060056FC RID: 22268 RVA: 0x000A449A File Offset: 0x000A269A
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), callback, state);
		}

		// Token: 0x060056FD RID: 22269 RVA: 0x000A44B3 File Offset: 0x000A26B3
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x060056FE RID: 22270 RVA: 0x00125B98 File Offset: 0x00123D98
		public override int ReadByte()
		{
			if (this._readPos == this._readLen)
			{
				return this.ReadByteSlow();
			}
			byte[] buffer = this._buffer;
			int readPos = this._readPos;
			this._readPos = readPos + 1;
			return buffer[readPos];
		}

		// Token: 0x060056FF RID: 22271 RVA: 0x00125BD4 File Offset: 0x00123DD4
		private int ReadByteSlow()
		{
			this.EnsureNotClosed();
			this.EnsureCanRead();
			if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			this.EnsureBufferAllocated();
			this._readLen = this._stream.Read(this._buffer, 0, this._bufferSize);
			this._readPos = 0;
			if (this._readLen == 0)
			{
				return -1;
			}
			byte[] buffer = this._buffer;
			int readPos = this._readPos;
			this._readPos = readPos + 1;
			return buffer[readPos];
		}

		// Token: 0x06005700 RID: 22272 RVA: 0x00125C4C File Offset: 0x00123E4C
		private void WriteToBuffer(byte[] array, ref int offset, ref int count)
		{
			int num = Math.Min(this._bufferSize - this._writePos, count);
			if (num <= 0)
			{
				return;
			}
			this.EnsureBufferAllocated();
			Buffer.BlockCopy(array, offset, this._buffer, this._writePos, num);
			this._writePos += num;
			count -= num;
			offset += num;
		}

		// Token: 0x06005701 RID: 22273 RVA: 0x00125CA8 File Offset: 0x00123EA8
		private int WriteToBuffer(ReadOnlySpan<byte> buffer)
		{
			int num = Math.Min(this._bufferSize - this._writePos, buffer.Length);
			if (num > 0)
			{
				this.EnsureBufferAllocated();
				buffer.Slice(0, num).CopyTo(new Span<byte>(this._buffer, this._writePos, num));
				this._writePos += num;
			}
			return num;
		}

		// Token: 0x06005702 RID: 22274 RVA: 0x00125D0C File Offset: 0x00123F0C
		private void WriteToBuffer(byte[] array, ref int offset, ref int count, out Exception error)
		{
			try
			{
				error = null;
				this.WriteToBuffer(array, ref offset, ref count);
			}
			catch (Exception ex)
			{
				error = ex;
			}
		}

		// Token: 0x06005703 RID: 22275 RVA: 0x00125D40 File Offset: 0x00123F40
		public override void Write(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			this.EnsureNotClosed();
			this.EnsureCanWrite();
			if (this._writePos == 0)
			{
				this.ClearReadBufferBeforeWrite();
			}
			int num = checked(this._writePos + count);
			if (checked(num + count >= this._bufferSize + this._bufferSize))
			{
				if (this._writePos > 0)
				{
					if (num <= this._bufferSize + this._bufferSize && num <= 81920)
					{
						this.EnsureShadowBufferAllocated();
						Buffer.BlockCopy(array, offset, this._buffer, this._writePos, count);
						this._stream.Write(this._buffer, 0, num);
						this._writePos = 0;
						return;
					}
					this._stream.Write(this._buffer, 0, this._writePos);
					this._writePos = 0;
				}
				this._stream.Write(array, offset, count);
				return;
			}
			this.WriteToBuffer(array, ref offset, ref count);
			if (this._writePos < this._bufferSize)
			{
				return;
			}
			this._stream.Write(this._buffer, 0, this._writePos);
			this._writePos = 0;
			this.WriteToBuffer(array, ref offset, ref count);
		}

		// Token: 0x06005704 RID: 22276 RVA: 0x00125E9C File Offset: 0x0012409C
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			this.EnsureNotClosed();
			this.EnsureCanWrite();
			if (this._writePos == 0)
			{
				this.ClearReadBufferBeforeWrite();
			}
			int num = checked(this._writePos + buffer.Length);
			if (checked(num + buffer.Length >= this._bufferSize + this._bufferSize))
			{
				if (this._writePos > 0)
				{
					if (num <= this._bufferSize + this._bufferSize && num <= 81920)
					{
						this.EnsureShadowBufferAllocated();
						buffer.CopyTo(new Span<byte>(this._buffer, this._writePos, buffer.Length));
						this._stream.Write(this._buffer, 0, num);
						this._writePos = 0;
						return;
					}
					this._stream.Write(this._buffer, 0, this._writePos);
					this._writePos = 0;
				}
				this._stream.Write(buffer);
				return;
			}
			int num2 = this.WriteToBuffer(buffer);
			if (this._writePos < this._bufferSize)
			{
				return;
			}
			buffer = buffer.Slice(num2);
			this._stream.Write(this._buffer, 0, this._writePos);
			this._writePos = 0;
			num2 = this.WriteToBuffer(buffer);
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x00125FC4 File Offset: 0x001241C4
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			return this.WriteAsync(new ReadOnlyMemory<byte>(buffer, offset, count), cancellationToken).AsTask();
		}

		// Token: 0x06005706 RID: 22278 RVA: 0x00126038 File Offset: 0x00124238
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask(Task.FromCanceled<int>(cancellationToken));
			}
			this.EnsureNotClosed();
			this.EnsureCanWrite();
			SemaphoreSlim semaphoreSlim = this.LazyEnsureAsyncActiveSemaphoreInitialized();
			Task task = semaphoreSlim.WaitAsync();
			if (task.IsCompletedSuccessfully)
			{
				bool flag = true;
				try
				{
					if (this._writePos == 0)
					{
						this.ClearReadBufferBeforeWrite();
					}
					flag = buffer.Length < this._bufferSize - this._writePos;
					if (flag)
					{
						this.WriteToBuffer(buffer.Span);
						return default(ValueTask);
					}
				}
				finally
				{
					if (flag)
					{
						semaphoreSlim.Release();
					}
				}
			}
			return new ValueTask(this.WriteToUnderlyingStreamAsync(buffer, cancellationToken, task));
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x001260F0 File Offset: 0x001242F0
		private async Task WriteToUnderlyingStreamAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken, Task semaphoreLockTask)
		{
			await semaphoreLockTask.ConfigureAwait(false);
			try
			{
				if (this._writePos == 0)
				{
					this.ClearReadBufferBeforeWrite();
				}
				int num = checked(this._writePos + buffer.Length);
				if (checked(num + buffer.Length < this._bufferSize + this._bufferSize))
				{
					buffer = buffer.Slice(this.WriteToBuffer(buffer.Span));
					if (this._writePos >= this._bufferSize)
					{
						await this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._buffer, 0, this._writePos), cancellationToken).ConfigureAwait(false);
						this._writePos = 0;
						this.WriteToBuffer(buffer.Span);
					}
				}
				else
				{
					if (this._writePos > 0)
					{
						if (num <= this._bufferSize + this._bufferSize && num <= 81920)
						{
							this.EnsureShadowBufferAllocated();
							buffer.Span.CopyTo(new Span<byte>(this._buffer, this._writePos, buffer.Length));
							await this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._buffer, 0, num), cancellationToken).ConfigureAwait(false);
							this._writePos = 0;
							return;
						}
						await this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._buffer, 0, this._writePos), cancellationToken).ConfigureAwait(false);
						this._writePos = 0;
					}
					await this._stream.WriteAsync(buffer, cancellationToken).ConfigureAwait(false);
				}
			}
			finally
			{
				this.LazyEnsureAsyncActiveSemaphoreInitialized().Release();
			}
		}

		// Token: 0x06005708 RID: 22280 RVA: 0x000A46C7 File Offset: 0x000A28C7
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), callback, state);
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x000A46E0 File Offset: 0x000A28E0
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x0012614C File Offset: 0x0012434C
		public override void WriteByte(byte value)
		{
			this.EnsureNotClosed();
			if (this._writePos == 0)
			{
				this.EnsureCanWrite();
				this.ClearReadBufferBeforeWrite();
				this.EnsureBufferAllocated();
			}
			if (this._writePos >= this._bufferSize - 1)
			{
				this.FlushWrite();
			}
			byte[] buffer = this._buffer;
			int writePos = this._writePos;
			this._writePos = writePos + 1;
			buffer[writePos] = value;
		}

		// Token: 0x0600570B RID: 22283 RVA: 0x001261A8 File Offset: 0x001243A8
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.EnsureNotClosed();
			this.EnsureCanSeek();
			if (this._writePos > 0)
			{
				this.FlushWrite();
				return this._stream.Seek(offset, origin);
			}
			if (this._readLen - this._readPos > 0 && origin == SeekOrigin.Current)
			{
				offset -= (long)(this._readLen - this._readPos);
			}
			long position = this.Position;
			long num = this._stream.Seek(offset, origin);
			this._readPos = (int)(num - (position - (long)this._readPos));
			if (0 <= this._readPos && this._readPos < this._readLen)
			{
				this._stream.Seek((long)(this._readLen - this._readPos), SeekOrigin.Current);
			}
			else
			{
				this._readPos = (this._readLen = 0);
			}
			return num;
		}

		// Token: 0x0600570C RID: 22284 RVA: 0x00126270 File Offset: 0x00124470
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
			}
			this.EnsureNotClosed();
			this.EnsureCanSeek();
			this.EnsureCanWrite();
			this.Flush();
			this._stream.SetLength(value);
		}

		// Token: 0x0600570D RID: 22285 RVA: 0x001262AC File Offset: 0x001244AC
		public override void CopyTo(Stream destination, int bufferSize)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			int num = this._readLen - this._readPos;
			if (num > 0)
			{
				destination.Write(this._buffer, this._readPos, num);
				this._readPos = (this._readLen = 0);
			}
			else if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			this._stream.CopyTo(destination, bufferSize);
		}

		// Token: 0x0600570E RID: 22286 RVA: 0x00126314 File Offset: 0x00124514
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.CopyToAsyncCore(destination, bufferSize, cancellationToken);
			}
			return Task.FromCanceled<int>(cancellationToken);
		}

		// Token: 0x0600570F RID: 22287 RVA: 0x00126338 File Offset: 0x00124538
		private async Task CopyToAsyncCore(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			await this.LazyEnsureAsyncActiveSemaphoreInitialized().WaitAsync().ConfigureAwait(false);
			try
			{
				int num = this._readLen - this._readPos;
				if (num > 0)
				{
					await destination.WriteAsync(new ReadOnlyMemory<byte>(this._buffer, this._readPos, num), cancellationToken).ConfigureAwait(false);
					int num2 = 0;
					this._readLen = num2;
					this._readPos = num2;
				}
				else if (this._writePos > 0)
				{
					await this.FlushWriteAsync(cancellationToken).ConfigureAwait(false);
				}
				await this._stream.CopyToAsync(destination, bufferSize, cancellationToken).ConfigureAwait(false);
			}
			finally
			{
				this._asyncActiveSemaphore.Release();
			}
		}

		// Token: 0x0400346A RID: 13418
		private const int MaxShadowBufferSize = 81920;

		// Token: 0x0400346B RID: 13419
		private const int DefaultBufferSize = 4096;

		// Token: 0x0400346C RID: 13420
		private Stream _stream;

		// Token: 0x0400346D RID: 13421
		private byte[] _buffer;

		// Token: 0x0400346E RID: 13422
		private readonly int _bufferSize;

		// Token: 0x0400346F RID: 13423
		private int _readPos;

		// Token: 0x04003470 RID: 13424
		private int _readLen;

		// Token: 0x04003471 RID: 13425
		private int _writePos;

		// Token: 0x04003472 RID: 13426
		private Task<int> _lastSyncCompletedReadTask;

		// Token: 0x04003473 RID: 13427
		private SemaphoreSlim _asyncActiveSemaphore;

		// Token: 0x02000962 RID: 2402
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06005710 RID: 22288 RVA: 0x00126393 File Offset: 0x00124593
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06005711 RID: 22289 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06005712 RID: 22290 RVA: 0x000A5ABE File Offset: 0x000A3CBE
			internal SemaphoreSlim <LazyEnsureAsyncActiveSemaphoreInitialized>b__10_0()
			{
				return new SemaphoreSlim(1, 1);
			}

			// Token: 0x04003474 RID: 13428
			public static readonly BufferedStream.<>c <>9 = new BufferedStream.<>c();

			// Token: 0x04003475 RID: 13429
			public static Func<SemaphoreSlim> <>9__10_0;
		}

		// Token: 0x02000963 RID: 2403
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <DisposeAsync>d__34 : IAsyncStateMachine
		{
			// Token: 0x06005713 RID: 22291 RVA: 0x001263A0 File Offset: 0x001245A0
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				BufferedStream bufferedStream = this;
				try
				{
					try
					{
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
						if (num != 0)
						{
							if (num == 1)
							{
								ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
								configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
								configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
								num = (num2 = -1);
								goto IL_0116;
							}
							if (bufferedStream._stream == null)
							{
								goto IL_0147;
							}
							obj = null;
							int num3 = 0;
						}
						try
						{
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
							if (num != 0)
							{
								configuredTaskAwaiter = bufferedStream.FlushAsync().ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter.IsCompleted)
								{
									num = (num2 = 0);
									ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<DisposeAsync>d__34>(ref configuredTaskAwaiter, ref this);
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
						catch (object obj2)
						{
							obj = obj2;
						}
						configuredValueTaskAwaiter = bufferedStream._stream.DisposeAsync().ConfigureAwait(false).GetAwaiter();
						if (!configuredValueTaskAwaiter.IsCompleted)
						{
							num = (num2 = 1);
							ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, BufferedStream.<DisposeAsync>d__34>(ref configuredValueTaskAwaiter, ref this);
							return;
						}
						IL_0116:
						configuredValueTaskAwaiter.GetResult();
						object obj2 = obj;
						if (obj2 != null)
						{
							Exception ex = obj2 as Exception;
							if (ex == null)
							{
								throw obj2;
							}
							ExceptionDispatchInfo.Capture(ex).Throw();
						}
						obj = null;
						IL_0147:;
					}
					finally
					{
						if (num < 0)
						{
							bufferedStream._stream = null;
							bufferedStream._buffer = null;
						}
					}
				}
				catch (Exception ex2)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex2);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06005714 RID: 22292 RVA: 0x00126584 File Offset: 0x00124784
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003476 RID: 13430
			public int <>1__state;

			// Token: 0x04003477 RID: 13431
			public AsyncValueTaskMethodBuilder <>t__builder;

			// Token: 0x04003478 RID: 13432
			public BufferedStream <>4__this;

			// Token: 0x04003479 RID: 13433
			private object <>7__wrap1;

			// Token: 0x0400347A RID: 13434
			private int <>7__wrap2;

			// Token: 0x0400347B RID: 13435
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400347C RID: 13436
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__2;
		}

		// Token: 0x02000964 RID: 2404
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FlushAsyncInternal>d__38 : IAsyncStateMachine
		{
			// Token: 0x06005715 RID: 22293 RVA: 0x00126594 File Offset: 0x00124794
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				BufferedStream bufferedStream = this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					if (num != 0)
					{
						if (num - 1 <= 2)
						{
							goto IL_008C;
						}
						sem = bufferedStream.LazyEnsureAsyncActiveSemaphoreInitialized();
						configuredTaskAwaiter = sem.WaitAsync().ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num = (num2 = 0);
							configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<FlushAsyncInternal>d__38>(ref configuredTaskAwaiter, ref this);
							return;
						}
					}
					else
					{
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (num2 = -1);
					}
					configuredTaskAwaiter.GetResult();
					IL_008C:
					try
					{
						switch (num)
						{
						case 1:
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (num2 = -1);
							break;
						case 2:
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (num2 = -1);
							goto IL_01B2;
						case 3:
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (num2 = -1);
							goto IL_0230;
						default:
							if (bufferedStream._writePos > 0)
							{
								configuredTaskAwaiter = bufferedStream.FlushWriteAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter.IsCompleted)
								{
									num = (num2 = 1);
									configuredTaskAwaiter2 = configuredTaskAwaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<FlushAsyncInternal>d__38>(ref configuredTaskAwaiter, ref this);
									return;
								}
							}
							else if (bufferedStream._readPos < bufferedStream._readLen)
							{
								if (bufferedStream._stream.CanSeek)
								{
									bufferedStream.FlushRead();
								}
								if (!bufferedStream._stream.CanWrite)
								{
									goto IL_01B9;
								}
								configuredTaskAwaiter = bufferedStream._stream.FlushAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter.IsCompleted)
								{
									num = (num2 = 2);
									configuredTaskAwaiter2 = configuredTaskAwaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<FlushAsyncInternal>d__38>(ref configuredTaskAwaiter, ref this);
									return;
								}
								goto IL_01B2;
							}
							else
							{
								if (!bufferedStream._stream.CanWrite)
								{
									goto IL_0237;
								}
								configuredTaskAwaiter = bufferedStream._stream.FlushAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter.IsCompleted)
								{
									num = (num2 = 3);
									configuredTaskAwaiter2 = configuredTaskAwaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<FlushAsyncInternal>d__38>(ref configuredTaskAwaiter, ref this);
									return;
								}
								goto IL_0230;
							}
							break;
						}
						configuredTaskAwaiter.GetResult();
						goto IL_026C;
						IL_01B2:
						configuredTaskAwaiter.GetResult();
						IL_01B9:
						goto IL_026C;
						IL_0230:
						configuredTaskAwaiter.GetResult();
						IL_0237:;
					}
					finally
					{
						if (num < 0)
						{
							sem.Release();
						}
					}
				}
				catch (Exception ex)
				{
					num2 = -2;
					sem = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_026C:
				num2 = -2;
				sem = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06005716 RID: 22294 RVA: 0x0012685C File Offset: 0x00124A5C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400347D RID: 13437
			public int <>1__state;

			// Token: 0x0400347E RID: 13438
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400347F RID: 13439
			public BufferedStream <>4__this;

			// Token: 0x04003480 RID: 13440
			public CancellationToken cancellationToken;

			// Token: 0x04003481 RID: 13441
			private SemaphoreSlim <sem>5__2;

			// Token: 0x04003482 RID: 13442
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000965 RID: 2405
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FlushWriteAsync>d__42 : IAsyncStateMachine
		{
			// Token: 0x06005717 RID: 22295 RVA: 0x0012686C File Offset: 0x00124A6C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				BufferedStream bufferedStream = this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num2 = -1;
							goto IL_010D;
						}
						configuredValueTaskAwaiter = bufferedStream._stream.WriteAsync(new ReadOnlyMemory<byte>(bufferedStream._buffer, 0, bufferedStream._writePos), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredValueTaskAwaiter.IsCompleted)
						{
							num2 = 0;
							ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, BufferedStream.<FlushWriteAsync>d__42>(ref configuredValueTaskAwaiter, ref this);
							return;
						}
					}
					else
					{
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
						configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
						num2 = -1;
					}
					configuredValueTaskAwaiter.GetResult();
					bufferedStream._writePos = 0;
					configuredTaskAwaiter = bufferedStream._stream.FlushAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						num2 = 1;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<FlushWriteAsync>d__42>(ref configuredTaskAwaiter, ref this);
						return;
					}
					IL_010D:
					configuredTaskAwaiter.GetResult();
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

			// Token: 0x06005718 RID: 22296 RVA: 0x001269D8 File Offset: 0x00124BD8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003483 RID: 13443
			public int <>1__state;

			// Token: 0x04003484 RID: 13444
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003485 RID: 13445
			public BufferedStream <>4__this;

			// Token: 0x04003486 RID: 13446
			public CancellationToken cancellationToken;

			// Token: 0x04003487 RID: 13447
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__1;

			// Token: 0x04003488 RID: 13448
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000966 RID: 2406
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadFromUnderlyingStreamAsync>d__51 : IAsyncStateMachine
		{
			// Token: 0x06005719 RID: 22297 RVA: 0x001269E8 File Offset: 0x00124BE8
			void IAsyncStateMachine.MoveNext()
			{
				int num6;
				int num5 = num6;
				BufferedStream bufferedStream = this;
				int num8;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					if (num5 != 0)
					{
						if (num5 - 1 <= 2)
						{
							goto IL_007C;
						}
						configuredTaskAwaiter = semaphoreLockTask.ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num5 = (num6 = 0);
							configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<ReadFromUnderlyingStreamAsync>d__51>(ref configuredTaskAwaiter, ref this);
							return;
						}
					}
					else
					{
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num5 = (num6 = -1);
					}
					configuredTaskAwaiter.GetResult();
					IL_007C:
					try
					{
						ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
						int num7;
						switch (num5)
						{
						case 1:
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num5 = (num6 = -1);
							break;
						case 2:
						{
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
							num5 = (num6 = -1);
							goto IL_0207;
						}
						case 3:
						{
							ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter);
							num5 = (num6 = -1);
							goto IL_02A7;
						}
						default:
							num7 = bufferedStream.ReadFromBuffer(buffer.Span);
							if (num7 == buffer.Length)
							{
								num8 = bytesAlreadySatisfied + num7;
								goto IL_0301;
							}
							if (num7 > 0)
							{
								buffer = buffer.Slice(num7);
								bytesAlreadySatisfied += num7;
							}
							bufferedStream._readPos = (bufferedStream._readLen = 0);
							if (bufferedStream._writePos <= 0)
							{
								goto IL_016F;
							}
							configuredTaskAwaiter = bufferedStream.FlushWriteAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter.IsCompleted)
							{
								num5 = (num6 = 1);
								configuredTaskAwaiter2 = configuredTaskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<ReadFromUnderlyingStreamAsync>d__51>(ref configuredTaskAwaiter, ref this);
								return;
							}
							break;
						}
						configuredTaskAwaiter.GetResult();
						IL_016F:
						if (buffer.Length >= bufferedStream._bufferSize)
						{
							num4 = bytesAlreadySatisfied;
							configuredValueTaskAwaiter = bufferedStream._stream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredValueTaskAwaiter.IsCompleted)
							{
								num5 = (num6 = 2);
								ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, BufferedStream.<ReadFromUnderlyingStreamAsync>d__51>(ref configuredValueTaskAwaiter, ref this);
								return;
							}
						}
						else
						{
							bufferedStream.EnsureBufferAllocated();
							configuredValueTaskAwaiter = bufferedStream._stream.ReadAsync(new Memory<byte>(bufferedStream._buffer, 0, bufferedStream._bufferSize), cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredValueTaskAwaiter.IsCompleted)
							{
								num5 = (num6 = 3);
								ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter, BufferedStream.<ReadFromUnderlyingStreamAsync>d__51>(ref configuredValueTaskAwaiter, ref this);
								return;
							}
							goto IL_02A7;
						}
						IL_0207:
						int num9 = configuredValueTaskAwaiter.GetResult();
						num8 = num4 + num9;
						goto IL_0301;
						IL_02A7:
						num9 = configuredValueTaskAwaiter.GetResult();
						bufferedStream._readLen = num9;
						num7 = bufferedStream.ReadFromBuffer(buffer.Span);
						num8 = bytesAlreadySatisfied + num7;
					}
					finally
					{
						if (num5 < 0)
						{
							bufferedStream.LazyEnsureAsyncActiveSemaphoreInitialized().Release();
						}
					}
				}
				catch (Exception ex)
				{
					num6 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_0301:
				num6 = -2;
				this.<>t__builder.SetResult(num8);
			}

			// Token: 0x0600571A RID: 22298 RVA: 0x00126D40 File Offset: 0x00124F40
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003489 RID: 13449
			public int <>1__state;

			// Token: 0x0400348A RID: 13450
			public AsyncValueTaskMethodBuilder<int> <>t__builder;

			// Token: 0x0400348B RID: 13451
			public Task semaphoreLockTask;

			// Token: 0x0400348C RID: 13452
			public BufferedStream <>4__this;

			// Token: 0x0400348D RID: 13453
			public Memory<byte> buffer;

			// Token: 0x0400348E RID: 13454
			public int bytesAlreadySatisfied;

			// Token: 0x0400348F RID: 13455
			public CancellationToken cancellationToken;

			// Token: 0x04003490 RID: 13456
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003491 RID: 13457
			private int <>7__wrap1;

			// Token: 0x04003492 RID: 13458
			private ConfiguredValueTaskAwaitable<int>.ConfiguredValueTaskAwaiter <>u__2;
		}

		// Token: 0x02000967 RID: 2407
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteToUnderlyingStreamAsync>d__63 : IAsyncStateMachine
		{
			// Token: 0x0600571B RID: 22299 RVA: 0x00126D50 File Offset: 0x00124F50
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				BufferedStream bufferedStream = this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					if (num != 0)
					{
						if (num - 1 <= 3)
						{
							goto IL_007B;
						}
						configuredTaskAwaiter = semaphoreLockTask.ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num = (num2 = 0);
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<WriteToUnderlyingStreamAsync>d__63>(ref configuredTaskAwaiter, ref this);
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
					IL_007B:
					try
					{
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
						switch (num)
						{
						case 1:
						{
							ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (num2 = -1);
							break;
						}
						case 2:
						{
							ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (num2 = -1);
							goto IL_0294;
						}
						case 3:
						{
							ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (num2 = -1);
							goto IL_0329;
						}
						case 4:
						{
							ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (num2 = -1);
							goto IL_03AA;
						}
						default:
						{
							if (bufferedStream._writePos == 0)
							{
								bufferedStream.ClearReadBufferBeforeWrite();
							}
							int num3 = checked(bufferedStream._writePos + buffer.Length);
							if (checked(num3 + buffer.Length < bufferedStream._bufferSize + bufferedStream._bufferSize))
							{
								buffer = buffer.Slice(bufferedStream.WriteToBuffer(buffer.Span));
								if (bufferedStream._writePos < bufferedStream._bufferSize)
								{
									goto IL_03DF;
								}
								configuredValueTaskAwaiter = bufferedStream._stream.WriteAsync(new ReadOnlyMemory<byte>(bufferedStream._buffer, 0, bufferedStream._writePos), cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredValueTaskAwaiter.IsCompleted)
								{
									num = (num2 = 1);
									ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, BufferedStream.<WriteToUnderlyingStreamAsync>d__63>(ref configuredValueTaskAwaiter, ref this);
									return;
								}
							}
							else
							{
								if (bufferedStream._writePos <= 0)
								{
									goto IL_0337;
								}
								if (num3 <= bufferedStream._bufferSize + bufferedStream._bufferSize && num3 <= 81920)
								{
									bufferedStream.EnsureShadowBufferAllocated();
									buffer.Span.CopyTo(new Span<byte>(bufferedStream._buffer, bufferedStream._writePos, buffer.Length));
									configuredValueTaskAwaiter = bufferedStream._stream.WriteAsync(new ReadOnlyMemory<byte>(bufferedStream._buffer, 0, num3), cancellationToken).ConfigureAwait(false).GetAwaiter();
									if (!configuredValueTaskAwaiter.IsCompleted)
									{
										num = (num2 = 2);
										ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, BufferedStream.<WriteToUnderlyingStreamAsync>d__63>(ref configuredValueTaskAwaiter, ref this);
										return;
									}
									goto IL_0294;
								}
								else
								{
									configuredValueTaskAwaiter = bufferedStream._stream.WriteAsync(new ReadOnlyMemory<byte>(bufferedStream._buffer, 0, bufferedStream._writePos), cancellationToken).ConfigureAwait(false).GetAwaiter();
									if (!configuredValueTaskAwaiter.IsCompleted)
									{
										num = (num2 = 3);
										ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, BufferedStream.<WriteToUnderlyingStreamAsync>d__63>(ref configuredValueTaskAwaiter, ref this);
										return;
									}
									goto IL_0329;
								}
							}
							break;
						}
						}
						configuredValueTaskAwaiter.GetResult();
						bufferedStream._writePos = 0;
						bufferedStream.WriteToBuffer(buffer.Span);
						goto IL_03B1;
						IL_0294:
						configuredValueTaskAwaiter.GetResult();
						bufferedStream._writePos = 0;
						goto IL_03DF;
						IL_0329:
						configuredValueTaskAwaiter.GetResult();
						bufferedStream._writePos = 0;
						IL_0337:
						configuredValueTaskAwaiter = bufferedStream._stream.WriteAsync(buffer, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredValueTaskAwaiter.IsCompleted)
						{
							num = (num2 = 4);
							ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, BufferedStream.<WriteToUnderlyingStreamAsync>d__63>(ref configuredValueTaskAwaiter, ref this);
							return;
						}
						IL_03AA:
						configuredValueTaskAwaiter.GetResult();
						IL_03B1:;
					}
					finally
					{
						if (num < 0)
						{
							bufferedStream.LazyEnsureAsyncActiveSemaphoreInitialized().Release();
						}
					}
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_03DF:
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600571C RID: 22300 RVA: 0x00127184 File Offset: 0x00125384
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003493 RID: 13459
			public int <>1__state;

			// Token: 0x04003494 RID: 13460
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003495 RID: 13461
			public Task semaphoreLockTask;

			// Token: 0x04003496 RID: 13462
			public BufferedStream <>4__this;

			// Token: 0x04003497 RID: 13463
			public ReadOnlyMemory<byte> buffer;

			// Token: 0x04003498 RID: 13464
			public CancellationToken cancellationToken;

			// Token: 0x04003499 RID: 13465
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400349A RID: 13466
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__2;
		}

		// Token: 0x02000968 RID: 2408
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CopyToAsyncCore>d__71 : IAsyncStateMachine
		{
			// Token: 0x0600571D RID: 22301 RVA: 0x00127194 File Offset: 0x00125394
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				BufferedStream bufferedStream = this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					if (num != 0)
					{
						if (num - 1 <= 2)
						{
							goto IL_0080;
						}
						configuredTaskAwaiter = bufferedStream.LazyEnsureAsyncActiveSemaphoreInitialized().WaitAsync().ConfigureAwait(false)
							.GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num = (num2 = 0);
							configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<CopyToAsyncCore>d__71>(ref configuredTaskAwaiter, ref this);
							return;
						}
					}
					else
					{
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (num2 = -1);
					}
					configuredTaskAwaiter.GetResult();
					IL_0080:
					try
					{
						ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter;
						switch (num)
						{
						case 1:
						{
							ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter = configuredValueTaskAwaiter2;
							configuredValueTaskAwaiter2 = default(ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter);
							num = (num2 = -1);
							break;
						}
						case 2:
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (num2 = -1);
							goto IL_01B6;
						case 3:
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (num2 = -1);
							goto IL_022E;
						default:
						{
							int num3 = bufferedStream._readLen - bufferedStream._readPos;
							if (num3 > 0)
							{
								configuredValueTaskAwaiter = destination.WriteAsync(new ReadOnlyMemory<byte>(bufferedStream._buffer, bufferedStream._readPos, num3), cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredValueTaskAwaiter.IsCompleted)
								{
									num = (num2 = 1);
									ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter configuredValueTaskAwaiter2 = configuredValueTaskAwaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter, BufferedStream.<CopyToAsyncCore>d__71>(ref configuredValueTaskAwaiter, ref this);
									return;
								}
							}
							else
							{
								if (bufferedStream._writePos <= 0)
								{
									goto IL_01BD;
								}
								configuredTaskAwaiter = bufferedStream.FlushWriteAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter.IsCompleted)
								{
									num = (num2 = 2);
									configuredTaskAwaiter2 = configuredTaskAwaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<CopyToAsyncCore>d__71>(ref configuredTaskAwaiter, ref this);
									return;
								}
								goto IL_01B6;
							}
							break;
						}
						}
						configuredValueTaskAwaiter.GetResult();
						bufferedStream._readPos = (bufferedStream._readLen = 0);
						goto IL_01BD;
						IL_01B6:
						configuredTaskAwaiter.GetResult();
						IL_01BD:
						configuredTaskAwaiter = bufferedStream._stream.CopyToAsync(destination, bufferSize, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num = (num2 = 3);
							configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, BufferedStream.<CopyToAsyncCore>d__71>(ref configuredTaskAwaiter, ref this);
							return;
						}
						IL_022E:
						configuredTaskAwaiter.GetResult();
					}
					finally
					{
						if (num < 0)
						{
							bufferedStream._asyncActiveSemaphore.Release();
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

			// Token: 0x0600571E RID: 22302 RVA: 0x0012744C File Offset: 0x0012564C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400349B RID: 13467
			public int <>1__state;

			// Token: 0x0400349C RID: 13468
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400349D RID: 13469
			public BufferedStream <>4__this;

			// Token: 0x0400349E RID: 13470
			public Stream destination;

			// Token: 0x0400349F RID: 13471
			public CancellationToken cancellationToken;

			// Token: 0x040034A0 RID: 13472
			public int bufferSize;

			// Token: 0x040034A1 RID: 13473
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040034A2 RID: 13474
			private ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__2;
		}
	}
}
