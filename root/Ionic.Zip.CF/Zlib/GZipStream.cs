using System;
using System.IO;
using System.Text;

namespace Ionic.Zlib
{
	// Token: 0x02000040 RID: 64
	public class GZipStream : Stream
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00014ACC File Offset: 0x00012CCC
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00014AD4 File Offset: 0x00012CD4
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._Comment = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00014AF0 File Offset: 0x00012CF0
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00014AF8 File Offset: 0x00012CF8
		public string FileName
		{
			get
			{
				return this._FileName;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._FileName = value;
				if (this._FileName == null)
				{
					return;
				}
				if (this._FileName.IndexOf("/") != -1)
				{
					this._FileName = this._FileName.Replace("/", "\\");
				}
				if (this._FileName.EndsWith("\\"))
				{
					throw new Exception("Illegal filename");
				}
				if (this._FileName.IndexOf("\\") != -1)
				{
					this._FileName = Path.GetFileName(this._FileName);
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00014B97 File Offset: 0x00012D97
		public int Crc32
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00014B9F File Offset: 0x00012D9F
		public GZipStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00014BAB File Offset: 0x00012DAB
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00014BB7 File Offset: 0x00012DB7
		public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00014BC3 File Offset: 0x00012DC3
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.GZIP, leaveOpen);
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00014BE0 File Offset: 0x00012DE0
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00014BED File Offset: 0x00012DED
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
					throw new ObjectDisposedException("GZipStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00014C0E File Offset: 0x00012E0E
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00014C1C File Offset: 0x00012E1C
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
					throw new ObjectDisposedException("GZipStream");
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

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00014C88 File Offset: 0x00012E88
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00014C9A File Offset: 0x00012E9A
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00014CAC File Offset: 0x00012EAC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
						this._Crc32 = this._baseStream.Crc32;
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00014D0C File Offset: 0x00012F0C
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00014D31 File Offset: 0x00012F31
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00014D34 File Offset: 0x00012F34
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00014D59 File Offset: 0x00012F59
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00014D79 File Offset: 0x00012F79
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00014D80 File Offset: 0x00012F80
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00014DE1 File Offset: 0x00012FE1
		public override long Position
		{
			get
			{
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return this._baseStream._z.TotalBytesOut + (long)this._headerByteCount;
				}
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return this._baseStream._z.TotalBytesIn + (long)this._baseStream._gzipHeaderByteCount;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00014DE8 File Offset: 0x00012FE8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			int num = this._baseStream.Read(buffer, offset, count);
			if (!this._firstReadDone)
			{
				this._firstReadDone = true;
				this.FileName = this._baseStream._GzipFileName;
				this.Comment = this._baseStream._GzipComment;
			}
			return num;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00014E49 File Offset: 0x00013049
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00014E50 File Offset: 0x00013050
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00014E58 File Offset: 0x00013058
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				if (!this._baseStream._wantCompress)
				{
					throw new InvalidOperationException();
				}
				this._headerByteCount = this.EmitHeader();
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00014EB8 File Offset: 0x000130B8
		private int EmitHeader()
		{
			byte[] array = ((this.Comment == null) ? null : GZipStream.iso8859dash1.GetBytes(this.Comment));
			byte[] array2 = ((this.FileName == null) ? null : GZipStream.iso8859dash1.GetBytes(this.FileName));
			int num = ((this.Comment == null) ? 0 : (array.Length + 1));
			int num2 = ((this.FileName == null) ? 0 : (array2.Length + 1));
			int num3 = 10 + num + num2;
			byte[] array3 = new byte[num3];
			int num4 = 0;
			array3[num4++] = 31;
			array3[num4++] = 139;
			array3[num4++] = 8;
			byte b = 0;
			if (this.Comment != null)
			{
				b ^= 16;
			}
			if (this.FileName != null)
			{
				b ^= 8;
			}
			array3[num4++] = b;
			if (this.LastModified == null)
			{
				this.LastModified = new DateTime?(DateTime.Now);
			}
			int num5 = (int)(this.LastModified.Value - GZipStream._unixEpoch).TotalSeconds;
			Array.Copy(BitConverter.GetBytes(num5), 0, array3, num4, 4);
			num4 += 4;
			array3[num4++] = 0;
			array3[num4++] = byte.MaxValue;
			if (num2 != 0)
			{
				Array.Copy(array2, 0, array3, num4, num2 - 1);
				num4 += num2 - 1;
				array3[num4++] = 0;
			}
			if (num != 0)
			{
				Array.Copy(array, 0, array3, num4, num - 1);
				num4 += num - 1;
				array3[num4++] = 0;
			}
			this._baseStream._stream.Write(array3, 0, array3.Length);
			return array3.Length;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001505C File Offset: 0x0001325C
		public static byte[] CompressString(string s)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000150A4 File Offset: 0x000132A4
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000150EC File Offset: 0x000132EC
		public static string UncompressString(byte[] compressed)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Decompress);
				text = ZlibBaseStream.UncompressString(compressed, stream);
			}
			return text;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00015130 File Offset: 0x00013330
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Decompress);
				array = ZlibBaseStream.UncompressBuffer(compressed, stream);
			}
			return array;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00015174 File Offset: 0x00013374
		// Note: this type is marked as 'beforefieldinit'.
		static GZipStream()
		{
		}

		// Token: 0x04000221 RID: 545
		public DateTime? LastModified;

		// Token: 0x04000222 RID: 546
		private int _headerByteCount;

		// Token: 0x04000223 RID: 547
		internal ZlibBaseStream _baseStream;

		// Token: 0x04000224 RID: 548
		private bool _disposed;

		// Token: 0x04000225 RID: 549
		private bool _firstReadDone;

		// Token: 0x04000226 RID: 550
		private string _FileName;

		// Token: 0x04000227 RID: 551
		private string _Comment;

		// Token: 0x04000228 RID: 552
		private int _Crc32;

		// Token: 0x04000229 RID: 553
		internal static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 1);

		// Token: 0x0400022A RID: 554
		internal static readonly Encoding iso8859dash1 = Encoding.GetEncoding("iso-8859-1");
	}
}
