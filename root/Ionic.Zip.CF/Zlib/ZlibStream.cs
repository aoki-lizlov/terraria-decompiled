using System;
using System.IO;

namespace Ionic.Zlib
{
	// Token: 0x02000057 RID: 87
	public class ZlibStream : Stream
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0001C756 File Offset: 0x0001A956
		public ZlibStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001C762 File Offset: 0x0001A962
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001C76E File Offset: 0x0001A96E
		public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001C77A File Offset: 0x0001A97A
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.ZLIB, leaveOpen);
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0001C797 File Offset: 0x0001A997
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
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
					throw new ObjectDisposedException("ZlibStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0001C7C5 File Offset: 0x0001A9C5
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0001C7D4 File Offset: 0x0001A9D4
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
					throw new ObjectDisposedException("ZlibStream");
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

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0001C840 File Offset: 0x0001AA40
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0001C852 File Offset: 0x0001AA52
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001C864 File Offset: 0x0001AA64
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

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0001C8B0 File Offset: 0x0001AAB0
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0001C8D5 File Offset: 0x0001AAD5
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0001C8D8 File Offset: 0x0001AAD8
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001C8FD File Offset: 0x0001AAFD
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001C91D File Offset: 0x0001AB1D
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0001C924 File Offset: 0x0001AB24
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0001C970 File Offset: 0x0001AB70
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
				throw new NotSupportedException();
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001C977 File Offset: 0x0001AB77
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001C99A File Offset: 0x0001AB9A
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001C9A1 File Offset: 0x0001ABA1
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001C9A8 File Offset: 0x0001ABA8
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0001C9CC File Offset: 0x0001ABCC
		public static byte[] CompressString(string s)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001CA14 File Offset: 0x0001AC14
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001CA5C File Offset: 0x0001AC5C
		public static string UncompressString(byte[] compressed)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Decompress);
				text = ZlibBaseStream.UncompressString(compressed, stream);
			}
			return text;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001CAA0 File Offset: 0x0001ACA0
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Decompress);
				array = ZlibBaseStream.UncompressBuffer(compressed, stream);
			}
			return array;
		}

		// Token: 0x0400030B RID: 779
		internal ZlibBaseStream _baseStream;

		// Token: 0x0400030C RID: 780
		private bool _disposed;
	}
}
