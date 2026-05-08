using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x0200092B RID: 2347
	[Serializable]
	public class MemoryStream : Stream
	{
		// Token: 0x06005394 RID: 21396 RVA: 0x00118C13 File Offset: 0x00116E13
		public MemoryStream()
			: this(0)
		{
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x00118C1C File Offset: 0x00116E1C
		public MemoryStream(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", "Capacity must be positive.");
			}
			this._buffer = ((capacity != 0) ? new byte[capacity] : Array.Empty<byte>());
			this._capacity = capacity;
			this._expandable = true;
			this._writable = true;
			this._exposable = true;
			this._origin = 0;
			this._isOpen = true;
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x00118C83 File Offset: 0x00116E83
		public MemoryStream(byte[] buffer)
			: this(buffer, true)
		{
		}

		// Token: 0x06005397 RID: 21399 RVA: 0x00118C90 File Offset: 0x00116E90
		public MemoryStream(byte[] buffer, bool writable)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			this._buffer = buffer;
			this._length = (this._capacity = buffer.Length);
			this._writable = writable;
			this._exposable = false;
			this._origin = 0;
			this._isOpen = true;
		}

		// Token: 0x06005398 RID: 21400 RVA: 0x00118CEB File Offset: 0x00116EEB
		public MemoryStream(byte[] buffer, int index, int count)
			: this(buffer, index, count, true, false)
		{
		}

		// Token: 0x06005399 RID: 21401 RVA: 0x00118CF8 File Offset: 0x00116EF8
		public MemoryStream(byte[] buffer, int index, int count, bool writable)
			: this(buffer, index, count, writable, false)
		{
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x00118D08 File Offset: 0x00116F08
		public MemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "Buffer cannot be null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Non-negative number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Non-negative number required.");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			this._buffer = buffer;
			this._position = index;
			this._origin = index;
			this._length = (this._capacity = index + count);
			this._writable = writable;
			this._exposable = publiclyVisible;
			this._expandable = false;
			this._isOpen = true;
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x0600539B RID: 21403 RVA: 0x00118DB0 File Offset: 0x00116FB0
		public override bool CanRead
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x0600539C RID: 21404 RVA: 0x00118DB0 File Offset: 0x00116FB0
		public override bool CanSeek
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x0600539D RID: 21405 RVA: 0x00118DB8 File Offset: 0x00116FB8
		public override bool CanWrite
		{
			get
			{
				return this._writable;
			}
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x00118DC0 File Offset: 0x00116FC0
		private void EnsureNotClosed()
		{
			if (!this._isOpen)
			{
				throw Error.GetStreamIsClosed();
			}
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x00118DD0 File Offset: 0x00116FD0
		private void EnsureWriteable()
		{
			if (!this.CanWrite)
			{
				throw Error.GetWriteNotSupported();
			}
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x00118DE0 File Offset: 0x00116FE0
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._isOpen = false;
					this._writable = false;
					this._expandable = false;
					this._lastReadTask = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x00118E28 File Offset: 0x00117028
		private bool EnsureCapacity(int value)
		{
			if (value < 0)
			{
				throw new IOException("Stream was too long.");
			}
			if (value > this._capacity)
			{
				int num = value;
				if (num < 256)
				{
					num = 256;
				}
				if (num < this._capacity * 2)
				{
					num = this._capacity * 2;
				}
				if (this._capacity * 2 > 2147483591)
				{
					num = ((value > 2147483591) ? value : 2147483591);
				}
				this.Capacity = num;
				return true;
			}
			return false;
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x00004088 File Offset: 0x00002288
		public override void Flush()
		{
		}

		// Token: 0x060053A3 RID: 21411 RVA: 0x00118E9C File Offset: 0x0011709C
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

		// Token: 0x060053A4 RID: 21412 RVA: 0x00118EE4 File Offset: 0x001170E4
		public virtual byte[] GetBuffer()
		{
			if (!this._exposable)
			{
				throw new UnauthorizedAccessException("MemoryStream's internal buffer cannot be accessed.");
			}
			return this._buffer;
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x00118EFF File Offset: 0x001170FF
		public virtual bool TryGetBuffer(out ArraySegment<byte> buffer)
		{
			if (!this._exposable)
			{
				buffer = default(ArraySegment<byte>);
				return false;
			}
			buffer = new ArraySegment<byte>(this._buffer, this._origin, this._length - this._origin);
			return true;
		}

		// Token: 0x060053A6 RID: 21414 RVA: 0x00118F37 File Offset: 0x00117137
		internal byte[] InternalGetBuffer()
		{
			return this._buffer;
		}

		// Token: 0x060053A7 RID: 21415 RVA: 0x00118F3F File Offset: 0x0011713F
		internal void InternalGetOriginAndLength(out int origin, out int length)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			origin = this._origin;
			length = this._length;
		}

		// Token: 0x060053A8 RID: 21416 RVA: 0x00118F5E File Offset: 0x0011715E
		internal int InternalGetPosition()
		{
			return this._position;
		}

		// Token: 0x060053A9 RID: 21417 RVA: 0x00118F68 File Offset: 0x00117168
		internal int InternalReadInt32()
		{
			this.EnsureNotClosed();
			int num = (this._position += 4);
			if (num > this._length)
			{
				this._position = this._length;
				throw Error.GetEndOfFile();
			}
			return (int)this._buffer[num - 4] | ((int)this._buffer[num - 3] << 8) | ((int)this._buffer[num - 2] << 16) | ((int)this._buffer[num - 1] << 24);
		}

		// Token: 0x060053AA RID: 21418 RVA: 0x00118FDC File Offset: 0x001171DC
		internal int InternalEmulateRead(int count)
		{
			this.EnsureNotClosed();
			int num = this._length - this._position;
			if (num > count)
			{
				num = count;
			}
			if (num < 0)
			{
				num = 0;
			}
			this._position += num;
			return num;
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x060053AB RID: 21419 RVA: 0x00119018 File Offset: 0x00117218
		// (set) Token: 0x060053AC RID: 21420 RVA: 0x00119030 File Offset: 0x00117230
		public virtual int Capacity
		{
			get
			{
				this.EnsureNotClosed();
				return this._capacity - this._origin;
			}
			set
			{
				if ((long)value < this.Length)
				{
					throw new ArgumentOutOfRangeException("value", "capacity was less than the current size.");
				}
				this.EnsureNotClosed();
				if (!this._expandable && value != this.Capacity)
				{
					throw new NotSupportedException("Memory stream is not expandable.");
				}
				if (this._expandable && value != this._capacity)
				{
					if (value > 0)
					{
						byte[] array = new byte[value];
						if (this._length > 0)
						{
							Buffer.BlockCopy(this._buffer, 0, array, 0, this._length);
						}
						this._buffer = array;
					}
					else
					{
						this._buffer = null;
					}
					this._capacity = value;
				}
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x060053AD RID: 21421 RVA: 0x001190C9 File Offset: 0x001172C9
		public override long Length
		{
			get
			{
				this.EnsureNotClosed();
				return (long)(this._length - this._origin);
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x060053AE RID: 21422 RVA: 0x001190DF File Offset: 0x001172DF
		// (set) Token: 0x060053AF RID: 21423 RVA: 0x001190F8 File Offset: 0x001172F8
		public override long Position
		{
			get
			{
				this.EnsureNotClosed();
				return (long)(this._position - this._origin);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Non-negative number required.");
				}
				this.EnsureNotClosed();
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", "Stream length must be non-negative and less than 2^31 - 1 - origin.");
				}
				this._position = this._origin + (int)value;
			}
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x00119148 File Offset: 0x00117348
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
			this.EnsureNotClosed();
			int num = this._length - this._position;
			if (num > count)
			{
				num = count;
			}
			if (num <= 0)
			{
				return 0;
			}
			if (num <= 8)
			{
				int num2 = num;
				while (--num2 >= 0)
				{
					buffer[offset + num2] = this._buffer[this._position + num2];
				}
			}
			else
			{
				Buffer.BlockCopy(this._buffer, this._position, buffer, offset, num);
			}
			this._position += num;
			return num;
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x0011920C File Offset: 0x0011740C
		public override int Read(Span<byte> buffer)
		{
			if (base.GetType() != typeof(MemoryStream))
			{
				return base.Read(buffer);
			}
			this.EnsureNotClosed();
			int num = Math.Min(this._length - this._position, buffer.Length);
			if (num <= 0)
			{
				return 0;
			}
			new Span<byte>(this._buffer, this._position, num).CopyTo(buffer);
			this._position += num;
			return num;
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x00119288 File Offset: 0x00117488
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
			catch (OperationCanceledException ex)
			{
				task = Task.FromCancellation<int>(ex);
			}
			catch (Exception ex2)
			{
				task = Task.FromException<int>(ex2);
			}
			return task;
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x00119354 File Offset: 0x00117554
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
			catch (OperationCanceledException ex)
			{
				valueTask = new ValueTask<int>(Task.FromCancellation<int>(ex));
			}
			catch (Exception ex2)
			{
				valueTask = new ValueTask<int>(Task.FromException<int>(ex2));
			}
			return valueTask;
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x001193F0 File Offset: 0x001175F0
		public override int ReadByte()
		{
			this.EnsureNotClosed();
			if (this._position >= this._length)
			{
				return -1;
			}
			byte[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			return buffer[position];
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x0011942C File Offset: 0x0011762C
		public override void CopyTo(Stream destination, int bufferSize)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (base.GetType() != typeof(MemoryStream))
			{
				base.CopyTo(destination, bufferSize);
				return;
			}
			int position = this._position;
			int num = this.InternalEmulateRead(this._length - position);
			if (num > 0)
			{
				destination.Write(this._buffer, position, num);
			}
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x0011948C File Offset: 0x0011768C
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			StreamHelpers.ValidateCopyToArgs(this, destination, bufferSize);
			if (base.GetType() != typeof(MemoryStream))
			{
				return base.CopyToAsync(destination, bufferSize, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			int position = this._position;
			int num = this.InternalEmulateRead(this._length - this._position);
			if (num == 0)
			{
				return Task.CompletedTask;
			}
			MemoryStream memoryStream = destination as MemoryStream;
			if (memoryStream == null)
			{
				return destination.WriteAsync(this._buffer, position, num, cancellationToken);
			}
			Task task;
			try
			{
				memoryStream.Write(this._buffer, position, num);
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x00119540 File Offset: 0x00117740
		public override long Seek(long offset, SeekOrigin loc)
		{
			this.EnsureNotClosed();
			if (offset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("offset", "Stream length must be non-negative and less than 2^31 - 1 - origin.");
			}
			switch (loc)
			{
			case SeekOrigin.Begin:
			{
				int num = this._origin + (int)offset;
				if (offset < 0L || num < this._origin)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				this._position = num;
				break;
			}
			case SeekOrigin.Current:
			{
				int num2 = this._position + (int)offset;
				if ((long)this._position + offset < (long)this._origin || num2 < this._origin)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				this._position = num2;
				break;
			}
			case SeekOrigin.End:
			{
				int num3 = this._length + (int)offset;
				if ((long)this._length + offset < (long)this._origin || num3 < this._origin)
				{
					throw new IOException("An attempt was made to move the position before the beginning of the stream.");
				}
				this._position = num3;
				break;
			}
			default:
				throw new ArgumentException("Invalid seek origin.");
			}
			return (long)this._position;
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x00119634 File Offset: 0x00117834
		public override void SetLength(long value)
		{
			if (value < 0L || value > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("value", "Stream length must be non-negative and less than 2^31 - 1 - origin.");
			}
			this.EnsureWriteable();
			if (value > (long)(2147483647 - this._origin))
			{
				throw new ArgumentOutOfRangeException("value", "Stream length must be non-negative and less than 2^31 - 1 - origin.");
			}
			int num = this._origin + (int)value;
			if (!this.EnsureCapacity(num) && num > this._length)
			{
				Array.Clear(this._buffer, this._length, num - this._length);
			}
			this._length = num;
			if (this._position > num)
			{
				this._position = num;
			}
		}

		// Token: 0x060053B9 RID: 21433 RVA: 0x001196D4 File Offset: 0x001178D4
		public virtual byte[] ToArray()
		{
			int num = this._length - this._origin;
			if (num == 0)
			{
				return Array.Empty<byte>();
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._buffer, this._origin, array, 0, num);
			return array;
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x00119714 File Offset: 0x00117914
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
			this.EnsureNotClosed();
			this.EnsureWriteable();
			int num = this._position + count;
			if (num < 0)
			{
				throw new IOException("Stream was too long.");
			}
			if (num > this._length)
			{
				bool flag = this._position > this._length;
				if (num > this._capacity && this.EnsureCapacity(num))
				{
					flag = false;
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, num - this._length);
				}
				this._length = num;
			}
			if (count <= 8 && buffer != this._buffer)
			{
				int num2 = count;
				while (--num2 >= 0)
				{
					this._buffer[this._position + num2] = buffer[offset + num2];
				}
			}
			else
			{
				Buffer.BlockCopy(buffer, offset, this._buffer, this._position, count);
			}
			this._position = num;
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x0011982C File Offset: 0x00117A2C
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			if (base.GetType() != typeof(MemoryStream))
			{
				base.Write(buffer);
				return;
			}
			this.EnsureNotClosed();
			this.EnsureWriteable();
			int num = this._position + buffer.Length;
			if (num < 0)
			{
				throw new IOException("Stream was too long.");
			}
			if (num > this._length)
			{
				bool flag = this._position > this._length;
				if (num > this._capacity && this.EnsureCapacity(num))
				{
					flag = false;
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, num - this._length);
				}
				this._length = num;
			}
			buffer.CopyTo(new Span<byte>(this._buffer, this._position, buffer.Length));
			this._position = num;
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x001198F8 File Offset: 0x00117AF8
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
			catch (OperationCanceledException ex)
			{
				task = Task.FromCancellation<VoidTaskResult>(ex);
			}
			catch (Exception ex2)
			{
				task = Task.FromException(ex2);
			}
			return task;
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x001199A4 File Offset: 0x00117BA4
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
			catch (OperationCanceledException ex)
			{
				valueTask = new ValueTask(Task.FromCancellation<VoidTaskResult>(ex));
			}
			catch (Exception ex2)
			{
				valueTask = new ValueTask(Task.FromException(ex2));
			}
			return valueTask;
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x00119A40 File Offset: 0x00117C40
		public override void WriteByte(byte value)
		{
			this.EnsureNotClosed();
			this.EnsureWriteable();
			if (this._position >= this._length)
			{
				int num = this._position + 1;
				bool flag = this._position > this._length;
				if (num >= this._capacity && this.EnsureCapacity(num))
				{
					flag = false;
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, this._position - this._length);
				}
				this._length = num;
			}
			byte[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			buffer[position] = value;
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x00119AD4 File Offset: 0x00117CD4
		public virtual void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", "Stream cannot be null.");
			}
			this.EnsureNotClosed();
			stream.Write(this._buffer, this._origin, this._length - this._origin);
		}

		// Token: 0x0400332F RID: 13103
		private byte[] _buffer;

		// Token: 0x04003330 RID: 13104
		private int _origin;

		// Token: 0x04003331 RID: 13105
		private int _position;

		// Token: 0x04003332 RID: 13106
		private int _length;

		// Token: 0x04003333 RID: 13107
		private int _capacity;

		// Token: 0x04003334 RID: 13108
		private bool _expandable;

		// Token: 0x04003335 RID: 13109
		private bool _writable;

		// Token: 0x04003336 RID: 13110
		private bool _exposable;

		// Token: 0x04003337 RID: 13111
		private bool _isOpen;

		// Token: 0x04003338 RID: 13112
		[NonSerialized]
		private Task<int> _lastReadTask;

		// Token: 0x04003339 RID: 13113
		private const int MemStreamMaxLength = 2147483647;
	}
}
