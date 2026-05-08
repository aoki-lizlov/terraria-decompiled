using System;
using System.IO;

namespace Ionic.Zlib
{
	// Token: 0x0200003F RID: 63
	public class DeflateStream : Stream
	{
		// Token: 0x06000330 RID: 816 RVA: 0x00014706 File Offset: 0x00012906
		public DeflateStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00014712 File Offset: 0x00012912
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0001471E File Offset: 0x0001291E
		public DeflateStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001472A File Offset: 0x0001292A
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._innerStream = stream;
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.DEFLATE, leaveOpen);
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0001474E File Offset: 0x0001294E
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0001475B File Offset: 0x0001295B
		public virtual FlushType FlushMode
		{
			get
			{
				return this._baseStream._flushMode;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0001477C File Offset: 0x0001297C
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0001478C File Offset: 0x0001298C
		public int BufferSize
		{
			get
			{
				return this._baseStream._bufferSize;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				if (this._baseStream._workingBuffer != null)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				if (value < 1024)
				{
					throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer, at least {1}.", value, 1024));
				}
				this._baseStream._bufferSize = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000338 RID: 824 RVA: 0x000147F8 File Offset: 0x000129F8
		// (set) Token: 0x06000339 RID: 825 RVA: 0x00014805 File Offset: 0x00012A05
		public CompressionStrategy Strategy
		{
			get
			{
				return this._baseStream.Strategy;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream.Strategy = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00014826 File Offset: 0x00012A26
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00014838 File Offset: 0x00012A38
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001484C File Offset: 0x00012A4C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00014898 File Offset: 0x00012A98
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600033E RID: 830 RVA: 0x000148BD File Offset: 0x00012ABD
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600033F RID: 831 RVA: 0x000148C0 File Offset: 0x00012AC0
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000148E5 File Offset: 0x00012AE5
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00014905 File Offset: 0x00012B05
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0001490C File Offset: 0x00012B0C
		// (set) Token: 0x06000343 RID: 835 RVA: 0x00014958 File Offset: 0x00012B58
		public override long Position
		{
			get
			{
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return this._baseStream._z.TotalBytesOut;
				}
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return this._baseStream._z.TotalBytesIn;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001495F File Offset: 0x00012B5F
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00014982 File Offset: 0x00012B82
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00014989 File Offset: 0x00012B89
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00014990 File Offset: 0x00012B90
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000149B4 File Offset: 0x00012BB4
		public static byte[] CompressString(string s)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000149FC File Offset: 0x00012BFC
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00014A44 File Offset: 0x00012C44
		public static string UncompressString(byte[] compressed)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Decompress);
				text = ZlibBaseStream.UncompressString(compressed, stream);
			}
			return text;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00014A88 File Offset: 0x00012C88
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Decompress);
				array = ZlibBaseStream.UncompressBuffer(compressed, stream);
			}
			return array;
		}

		// Token: 0x0400021E RID: 542
		internal ZlibBaseStream _baseStream;

		// Token: 0x0400021F RID: 543
		internal Stream _innerStream;

		// Token: 0x04000220 RID: 544
		private bool _disposed;
	}
}
