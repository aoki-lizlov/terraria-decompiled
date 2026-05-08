using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x02000949 RID: 2377
	internal sealed class UnmanagedMemoryStreamWrapper : MemoryStream
	{
		// Token: 0x06005562 RID: 21858 RVA: 0x00120BA4 File Offset: 0x0011EDA4
		internal UnmanagedMemoryStreamWrapper(UnmanagedMemoryStream stream)
		{
			this._unmanagedStream = stream;
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06005563 RID: 21859 RVA: 0x00120BB3 File Offset: 0x0011EDB3
		public override bool CanRead
		{
			get
			{
				return this._unmanagedStream.CanRead;
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06005564 RID: 21860 RVA: 0x00120BC0 File Offset: 0x0011EDC0
		public override bool CanSeek
		{
			get
			{
				return this._unmanagedStream.CanSeek;
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06005565 RID: 21861 RVA: 0x00120BCD File Offset: 0x0011EDCD
		public override bool CanWrite
		{
			get
			{
				return this._unmanagedStream.CanWrite;
			}
		}

		// Token: 0x06005566 RID: 21862 RVA: 0x00120BDC File Offset: 0x0011EDDC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._unmanagedStream.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06005567 RID: 21863 RVA: 0x00120C14 File Offset: 0x0011EE14
		public override void Flush()
		{
			this._unmanagedStream.Flush();
		}

		// Token: 0x06005568 RID: 21864 RVA: 0x00120C21 File Offset: 0x0011EE21
		public override byte[] GetBuffer()
		{
			throw new UnauthorizedAccessException("MemoryStream's internal buffer cannot be accessed.");
		}

		// Token: 0x06005569 RID: 21865 RVA: 0x00120C2D File Offset: 0x0011EE2D
		public override bool TryGetBuffer(out ArraySegment<byte> buffer)
		{
			buffer = default(ArraySegment<byte>);
			return false;
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x0600556A RID: 21866 RVA: 0x00120C37 File Offset: 0x0011EE37
		// (set) Token: 0x0600556B RID: 21867 RVA: 0x00120C45 File Offset: 0x0011EE45
		public override int Capacity
		{
			get
			{
				return (int)this._unmanagedStream.Capacity;
			}
			set
			{
				throw new IOException("Unable to expand length of this stream beyond its capacity.");
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x0600556C RID: 21868 RVA: 0x00120C51 File Offset: 0x0011EE51
		public override long Length
		{
			get
			{
				return this._unmanagedStream.Length;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x0600556D RID: 21869 RVA: 0x00120C5E File Offset: 0x0011EE5E
		// (set) Token: 0x0600556E RID: 21870 RVA: 0x00120C6B File Offset: 0x0011EE6B
		public override long Position
		{
			get
			{
				return this._unmanagedStream.Position;
			}
			set
			{
				this._unmanagedStream.Position = value;
			}
		}

		// Token: 0x0600556F RID: 21871 RVA: 0x00120C79 File Offset: 0x0011EE79
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._unmanagedStream.Read(buffer, offset, count);
		}

		// Token: 0x06005570 RID: 21872 RVA: 0x00120C89 File Offset: 0x0011EE89
		public override int Read(Span<byte> buffer)
		{
			return this._unmanagedStream.Read(buffer);
		}

		// Token: 0x06005571 RID: 21873 RVA: 0x00120C97 File Offset: 0x0011EE97
		public override int ReadByte()
		{
			return this._unmanagedStream.ReadByte();
		}

		// Token: 0x06005572 RID: 21874 RVA: 0x00120CA4 File Offset: 0x0011EEA4
		public override long Seek(long offset, SeekOrigin loc)
		{
			return this._unmanagedStream.Seek(offset, loc);
		}

		// Token: 0x06005573 RID: 21875 RVA: 0x00120CB4 File Offset: 0x0011EEB4
		public override byte[] ToArray()
		{
			byte[] array = new byte[this._unmanagedStream.Length];
			this._unmanagedStream.Read(array, 0, (int)this._unmanagedStream.Length);
			return array;
		}

		// Token: 0x06005574 RID: 21876 RVA: 0x00120CEE File Offset: 0x0011EEEE
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._unmanagedStream.Write(buffer, offset, count);
		}

		// Token: 0x06005575 RID: 21877 RVA: 0x00120CFE File Offset: 0x0011EEFE
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			this._unmanagedStream.Write(buffer);
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x00120D0C File Offset: 0x0011EF0C
		public override void WriteByte(byte value)
		{
			this._unmanagedStream.WriteByte(value);
		}

		// Token: 0x06005577 RID: 21879 RVA: 0x00120D1C File Offset: 0x0011EF1C
		public override void WriteTo(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", "Stream cannot be null.");
			}
			byte[] array = this.ToArray();
			stream.Write(array, 0, array.Length);
		}

		// Token: 0x06005578 RID: 21880 RVA: 0x00120D4E File Offset: 0x0011EF4E
		public override void SetLength(long value)
		{
			base.SetLength(value);
		}

		// Token: 0x06005579 RID: 21881 RVA: 0x00120D58 File Offset: 0x0011EF58
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", "Positive number required.");
			}
			if (!this.CanRead && !this.CanWrite)
			{
				throw new ObjectDisposedException(null, "Cannot access a closed Stream.");
			}
			if (!destination.CanRead && !destination.CanWrite)
			{
				throw new ObjectDisposedException("destination", "Cannot access a closed Stream.");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("Stream does not support reading.");
			}
			if (!destination.CanWrite)
			{
				throw new NotSupportedException("Stream does not support writing.");
			}
			return this._unmanagedStream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

		// Token: 0x0600557A RID: 21882 RVA: 0x00120DF7 File Offset: 0x0011EFF7
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._unmanagedStream.FlushAsync(cancellationToken);
		}

		// Token: 0x0600557B RID: 21883 RVA: 0x00120E05 File Offset: 0x0011F005
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._unmanagedStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600557C RID: 21884 RVA: 0x00120E17 File Offset: 0x0011F017
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._unmanagedStream.ReadAsync(buffer, cancellationToken);
		}

		// Token: 0x0600557D RID: 21885 RVA: 0x00120E26 File Offset: 0x0011F026
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._unmanagedStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x00120E38 File Offset: 0x0011F038
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._unmanagedStream.WriteAsync(buffer, cancellationToken);
		}

		// Token: 0x040033F8 RID: 13304
		private UnmanagedMemoryStream _unmanagedStream;
	}
}
