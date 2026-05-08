using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x02000948 RID: 2376
	public class UnmanagedMemoryStream : Stream
	{
		// Token: 0x0600553E RID: 21822 RVA: 0x0011FF12 File Offset: 0x0011E112
		protected UnmanagedMemoryStream()
		{
			this._mem = null;
			this._isOpen = false;
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x0011FF29 File Offset: 0x0011E129
		public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length)
		{
			this.Initialize(buffer, offset, length, FileAccess.Read);
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x0011FF3B File Offset: 0x0011E13B
		public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length, FileAccess access)
		{
			this.Initialize(buffer, offset, length, access);
		}

		// Token: 0x06005541 RID: 21825 RVA: 0x0011FF50 File Offset: 0x0011E150
		protected unsafe void Initialize(SafeBuffer buffer, long offset, long length, FileAccess access)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length", "Non-negative number required.");
			}
			if (buffer.ByteLength < (ulong)(offset + length))
			{
				throw new ArgumentException("Offset and length were greater than the size of the SafeBuffer.");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException("The method cannot be called twice on the same instance.");
			}
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				buffer.AcquirePointer(ref ptr);
				if (ptr + offset + length < ptr)
				{
					throw new ArgumentException("The UnmanagedMemoryStream capacity would wrap around the high end of the address space.");
				}
			}
			finally
			{
				if (ptr != null)
				{
					buffer.ReleasePointer();
				}
			}
			this._offset = offset;
			this._buffer = buffer;
			this._length = length;
			this._capacity = length;
			this._access = access;
			this._isOpen = true;
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x00120044 File Offset: 0x0011E244
		[CLSCompliant(false)]
		public unsafe UnmanagedMemoryStream(byte* pointer, long length)
		{
			this.Initialize(pointer, length, length, FileAccess.Read);
		}

		// Token: 0x06005543 RID: 21827 RVA: 0x00120056 File Offset: 0x0011E256
		[CLSCompliant(false)]
		public unsafe UnmanagedMemoryStream(byte* pointer, long length, long capacity, FileAccess access)
		{
			this.Initialize(pointer, length, capacity, access);
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x0012006C File Offset: 0x0011E26C
		[CLSCompliant(false)]
		protected unsafe void Initialize(byte* pointer, long length, long capacity, FileAccess access)
		{
			if (pointer == null)
			{
				throw new ArgumentNullException("pointer");
			}
			if (length < 0L || capacity < 0L)
			{
				throw new ArgumentOutOfRangeException((length < 0L) ? "length" : "capacity", "Non-negative number required.");
			}
			if (length > capacity)
			{
				throw new ArgumentOutOfRangeException("length", "The length cannot be greater than the capacity.");
			}
			if (pointer + capacity < pointer)
			{
				throw new ArgumentOutOfRangeException("capacity", "The UnmanagedMemoryStream capacity would wrap around the high end of the address space.");
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access", "Enum value was out of legal range.");
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException("The method cannot be called twice on the same instance.");
			}
			this._mem = pointer;
			this._offset = 0L;
			this._length = length;
			this._capacity = capacity;
			this._access = access;
			this._isOpen = true;
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06005545 RID: 21829 RVA: 0x00120134 File Offset: 0x0011E334
		public override bool CanRead
		{
			get
			{
				return this._isOpen && (this._access & FileAccess.Read) > (FileAccess)0;
			}
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06005546 RID: 21830 RVA: 0x0012014B File Offset: 0x0011E34B
		public override bool CanSeek
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06005547 RID: 21831 RVA: 0x00120153 File Offset: 0x0011E353
		public override bool CanWrite
		{
			get
			{
				return this._isOpen && (this._access & FileAccess.Write) > (FileAccess)0;
			}
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x0012016A File Offset: 0x0011E36A
		protected override void Dispose(bool disposing)
		{
			this._isOpen = false;
			this._mem = null;
			base.Dispose(disposing);
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x00120182 File Offset: 0x0011E382
		private void EnsureNotClosed()
		{
			if (!this._isOpen)
			{
				throw Error.GetStreamIsClosed();
			}
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x00120192 File Offset: 0x0011E392
		private void EnsureReadable()
		{
			if (!this.CanRead)
			{
				throw Error.GetReadNotSupported();
			}
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x00118DD0 File Offset: 0x00116FD0
		private void EnsureWriteable()
		{
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x001201A2 File Offset: 0x0011E3A2
		public override void Flush()
		{
			this.EnsureNotClosed();
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x001201AC File Offset: 0x0011E3AC
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task;
			try
			{
				this.Flush();
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x0600554E RID: 21838 RVA: 0x001201F4 File Offset: 0x0011E3F4
		public override long Length
		{
			get
			{
				this.EnsureNotClosed();
				return Interlocked.Read(ref this._length);
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x0600554F RID: 21839 RVA: 0x00120207 File Offset: 0x0011E407
		public long Capacity
		{
			get
			{
				this.EnsureNotClosed();
				return this._capacity;
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06005550 RID: 21840 RVA: 0x00120215 File Offset: 0x0011E415
		// (set) Token: 0x06005551 RID: 21841 RVA: 0x00120230 File Offset: 0x0011E430
		public override long Position
		{
			get
			{
				if (!this.CanSeek)
				{
					throw Error.GetStreamIsClosed();
				}
				return Interlocked.Read(ref this._position);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
				}
				if (!this.CanSeek)
				{
					throw Error.GetStreamIsClosed();
				}
				Interlocked.Exchange(ref this._position, value);
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06005552 RID: 21842 RVA: 0x00120264 File Offset: 0x0011E464
		// (set) Token: 0x06005553 RID: 21843 RVA: 0x001202B4 File Offset: 0x0011E4B4
		[CLSCompliant(false)]
		public unsafe byte* PositionPointer
		{
			get
			{
				if (this._buffer != null)
				{
					throw new NotSupportedException("This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer.");
				}
				this.EnsureNotClosed();
				long num = Interlocked.Read(ref this._position);
				if (num > this._capacity)
				{
					throw new IndexOutOfRangeException("Unmanaged memory stream position was beyond the capacity of the stream.");
				}
				return this._mem + num;
			}
			set
			{
				if (this._buffer != null)
				{
					throw new NotSupportedException("This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer.");
				}
				this.EnsureNotClosed();
				if (value < this._mem)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				long num = (long)(value - this._mem);
				if (num < 0L)
				{
					throw new ArgumentOutOfRangeException("offset", "UnmanagedMemoryStream length must be non-negative and less than 2^63 - 1 - baseAddress.");
				}
				Interlocked.Exchange(ref this._position, num);
			}
		}

		// Token: 0x06005554 RID: 21844 RVA: 0x0012031C File Offset: 0x0011E51C
		public override int Read(byte[] buffer, int offset, int count)
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
			return this.ReadCore(new Span<byte>(buffer, offset, count));
		}

		// Token: 0x06005555 RID: 21845 RVA: 0x00120385 File Offset: 0x0011E585
		public override int Read(Span<byte> buffer)
		{
			if (base.GetType() == typeof(UnmanagedMemoryStream))
			{
				return this.ReadCore(buffer);
			}
			return base.Read(buffer);
		}

		// Token: 0x06005556 RID: 21846 RVA: 0x001203B0 File Offset: 0x0011E5B0
		internal unsafe int ReadCore(Span<byte> buffer)
		{
			this.EnsureNotClosed();
			this.EnsureReadable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Math.Min(Interlocked.Read(ref this._length) - num, (long)buffer.Length);
			if (num2 <= 0L)
			{
				return 0;
			}
			int num3 = (int)num2;
			if (num3 < 0)
			{
				return 0;
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(buffer))
			{
				byte* ptr = reference;
				if (this._buffer != null)
				{
					byte* ptr2 = null;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this._buffer.AcquirePointer(ref ptr2);
						Buffer.Memcpy(ptr, ptr2 + num + this._offset, num3);
						goto IL_00A5;
					}
					finally
					{
						if (ptr2 != null)
						{
							this._buffer.ReleasePointer();
						}
					}
				}
				Buffer.Memcpy(ptr, this._mem + num, num3);
				IL_00A5:;
			}
			Interlocked.Exchange(ref this._position, num + num2);
			return num3;
		}

		// Token: 0x06005557 RID: 21847 RVA: 0x00120488 File Offset: 0x0011E688
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
			Task<int> task;
			try
			{
				int num = this.Read(buffer, offset, count);
				Task<int> lastReadTask = this._lastReadTask;
				Task<int> task2;
				if (lastReadTask == null || lastReadTask.Result != num)
				{
					task = (this._lastReadTask = Task.FromResult<int>(num));
					task2 = task;
				}
				else
				{
					task2 = lastReadTask;
				}
				task = task2;
			}
			catch (Exception ex)
			{
				task = Task.FromException<int>(ex);
			}
			return task;
		}

		// Token: 0x06005558 RID: 21848 RVA: 0x00120540 File Offset: 0x0011E740
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
			}
			ValueTask<int> valueTask;
			try
			{
				ArraySegment<byte> arraySegment;
				valueTask = new ValueTask<int>(MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment) ? this.Read(arraySegment.Array, arraySegment.Offset, arraySegment.Count) : this.Read(buffer.Span));
			}
			catch (Exception ex)
			{
				valueTask = new ValueTask<int>(Task.FromException<int>(ex));
			}
			return valueTask;
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x001205C4 File Offset: 0x0011E7C4
		public unsafe override int ReadByte()
		{
			this.EnsureNotClosed();
			this.EnsureReadable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			if (num >= num2)
			{
				return -1;
			}
			Interlocked.Exchange(ref this._position, num + 1L);
			if (this._buffer != null)
			{
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					return (int)(ptr + num)[this._offset];
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			return (int)this._mem[num];
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x00120668 File Offset: 0x0011E868
		public override long Seek(long offset, SeekOrigin loc)
		{
			this.EnsureNotClosed();
			switch (loc)
			{
			case SeekOrigin.Begin:
				if (offset < 0L)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				Interlocked.Exchange(ref this._position, offset);
				break;
			case SeekOrigin.Current:
			{
				long num = Interlocked.Read(ref this._position);
				if (offset + num < 0L)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				Interlocked.Exchange(ref this._position, offset + num);
				break;
			}
			case SeekOrigin.End:
			{
				long num2 = Interlocked.Read(ref this._length);
				if (num2 + offset < 0L)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				Interlocked.Exchange(ref this._position, num2 + offset);
				break;
			}
			default:
				throw new ArgumentException("Invalid seek origin.");
			}
			return Interlocked.Read(ref this._position);
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x00120724 File Offset: 0x0011E924
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
			}
			if (this._buffer != null)
			{
				throw new NotSupportedException("This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer.");
			}
			this.EnsureNotClosed();
			this.EnsureWriteable();
			if (value > this._capacity)
			{
				throw new IOException("Unable to expand length of this stream beyond its capacity.");
			}
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			if (value > num2)
			{
				Buffer.ZeroMemory(this._mem + num2, value - num2);
			}
			Interlocked.Exchange(ref this._length, value);
			if (num > value)
			{
				Interlocked.Exchange(ref this._position, value);
			}
		}

		// Token: 0x0600555C RID: 21852 RVA: 0x001207C4 File Offset: 0x0011E9C4
		public override void Write(byte[] buffer, int offset, int count)
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
			this.WriteCore(new Span<byte>(buffer, offset, count));
		}

		// Token: 0x0600555D RID: 21853 RVA: 0x00120832 File Offset: 0x0011EA32
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			if (base.GetType() == typeof(UnmanagedMemoryStream))
			{
				this.WriteCore(buffer);
				return;
			}
			base.Write(buffer);
		}

		// Token: 0x0600555E RID: 21854 RVA: 0x0012085C File Offset: 0x0011EA5C
		internal unsafe void WriteCore(ReadOnlySpan<byte> buffer)
		{
			this.EnsureNotClosed();
			this.EnsureWriteable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			long num3 = num + (long)buffer.Length;
			if (num3 < 0L)
			{
				throw new IOException("Stream was too long.");
			}
			if (num3 > this._capacity)
			{
				throw new NotSupportedException("Unable to expand length of this stream beyond its capacity.");
			}
			if (this._buffer == null)
			{
				if (num > num2)
				{
					Buffer.ZeroMemory(this._mem + num2, num - num2);
				}
				if (num3 > num2)
				{
					Interlocked.Exchange(ref this._length, num3);
				}
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(buffer))
			{
				byte* ptr = reference;
				if (this._buffer != null)
				{
					if (this._capacity - num < (long)buffer.Length)
					{
						throw new ArgumentException("Not enough space available in the buffer.");
					}
					byte* ptr2 = null;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this._buffer.AcquirePointer(ref ptr2);
						Buffer.Memcpy(ptr2 + num + this._offset, ptr, buffer.Length);
						goto IL_010C;
					}
					finally
					{
						if (ptr2 != null)
						{
							this._buffer.ReleasePointer();
						}
					}
				}
				Buffer.Memcpy(this._mem + num, ptr, buffer.Length);
				IL_010C:;
			}
			Interlocked.Exchange(ref this._position, num3);
		}

		// Token: 0x0600555F RID: 21855 RVA: 0x00120998 File Offset: 0x0011EB98
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
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			Task task;
			try
			{
				this.Write(buffer, offset, count);
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		// Token: 0x06005560 RID: 21856 RVA: 0x00120A30 File Offset: 0x0011EC30
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask(Task.FromCanceled(cancellationToken));
			}
			ValueTask valueTask;
			try
			{
				ArraySegment<byte> arraySegment;
				if (MemoryMarshal.TryGetArray<byte>(buffer, out arraySegment))
				{
					this.Write(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				}
				else
				{
					this.Write(buffer.Span);
				}
				valueTask = default(ValueTask);
				valueTask = valueTask;
			}
			catch (Exception ex)
			{
				valueTask = new ValueTask(Task.FromException(ex));
			}
			return valueTask;
		}

		// Token: 0x06005561 RID: 21857 RVA: 0x00120AB4 File Offset: 0x0011ECB4
		public unsafe override void WriteByte(byte value)
		{
			this.EnsureNotClosed();
			this.EnsureWriteable();
			long num = Interlocked.Read(ref this._position);
			long num2 = Interlocked.Read(ref this._length);
			long num3 = num + 1L;
			if (num >= num2)
			{
				if (num3 < 0L)
				{
					throw new IOException("Stream was too long.");
				}
				if (num3 > this._capacity)
				{
					throw new NotSupportedException("Unable to expand length of this stream beyond its capacity.");
				}
				if (this._buffer == null)
				{
					if (num > num2)
					{
						Buffer.ZeroMemory(this._mem + num2, num - num2);
					}
					Interlocked.Exchange(ref this._length, num3);
				}
			}
			if (this._buffer != null)
			{
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					(ptr + num)[this._offset] = value;
					goto IL_00C4;
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			this._mem[num] = value;
			IL_00C4:
			Interlocked.Exchange(ref this._position, num3);
		}

		// Token: 0x040033EF RID: 13295
		private SafeBuffer _buffer;

		// Token: 0x040033F0 RID: 13296
		private unsafe byte* _mem;

		// Token: 0x040033F1 RID: 13297
		private long _length;

		// Token: 0x040033F2 RID: 13298
		private long _capacity;

		// Token: 0x040033F3 RID: 13299
		private long _position;

		// Token: 0x040033F4 RID: 13300
		private long _offset;

		// Token: 0x040033F5 RID: 13301
		private FileAccess _access;

		// Token: 0x040033F6 RID: 13302
		internal bool _isOpen;

		// Token: 0x040033F7 RID: 13303
		private Task<int> _lastReadTask;
	}
}
