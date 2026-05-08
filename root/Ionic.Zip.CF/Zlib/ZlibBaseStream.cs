using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Crc;

namespace Ionic.Zlib
{
	// Token: 0x02000053 RID: 83
	internal class ZlibBaseStream : Stream
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0001B60E File Offset: 0x0001980E
		internal int Crc32
		{
			get
			{
				if (this.crc == null)
				{
					return 0;
				}
				return this.crc.Crc32Result;
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001B628 File Offset: 0x00019828
		public ZlibBaseStream(Stream stream, CompressionMode compressionMode, CompressionLevel level, ZlibStreamFlavor flavor, bool leaveOpen)
		{
			this._flushMode = FlushType.None;
			this._stream = stream;
			this._leaveOpen = leaveOpen;
			this._compressionMode = compressionMode;
			this._flavor = flavor;
			this._level = level;
			if (flavor == ZlibStreamFlavor.GZIP)
			{
				this.crc = new CRC32();
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0001B699 File Offset: 0x00019899
		protected internal bool _wantCompress
		{
			get
			{
				return this._compressionMode == CompressionMode.Compress;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0001B6A4 File Offset: 0x000198A4
		private ZlibCodec z
		{
			get
			{
				if (this._z == null)
				{
					bool flag = this._flavor == ZlibStreamFlavor.ZLIB;
					this._z = new ZlibCodec();
					if (this._compressionMode == CompressionMode.Decompress)
					{
						this._z.InitializeInflate(flag);
					}
					else
					{
						this._z.Strategy = this.Strategy;
						this._z.InitializeDeflate(this._level, flag);
					}
				}
				return this._z;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001B714 File Offset: 0x00019914
		private byte[] workingBuffer
		{
			get
			{
				if (this._workingBuffer == null)
				{
					this._workingBuffer = new byte[this._bufferSize];
				}
				return this._workingBuffer;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001B738 File Offset: 0x00019938
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.crc != null)
			{
				this.crc.SlurpBlock(buffer, offset, count);
			}
			if (this._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				this._streamMode = ZlibBaseStream.StreamMode.Writer;
			}
			else if (this._streamMode != ZlibBaseStream.StreamMode.Writer)
			{
				throw new ZlibException("Cannot Write after Reading.");
			}
			if (count == 0)
			{
				return;
			}
			this.z.InputBuffer = buffer;
			this._z.NextIn = offset;
			this._z.AvailableBytesIn = count;
			for (;;)
			{
				this._z.OutputBuffer = this.workingBuffer;
				this._z.NextOut = 0;
				this._z.AvailableBytesOut = this._workingBuffer.Length;
				int num = (this._wantCompress ? this._z.Deflate(this._flushMode) : this._z.Inflate(this._flushMode));
				if (num != 0 && num != 1)
				{
					break;
				}
				this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
				bool flag = this._z.AvailableBytesIn == 0 && this._z.AvailableBytesOut != 0;
				if (this._flavor == ZlibStreamFlavor.GZIP && !this._wantCompress)
				{
					flag = this._z.AvailableBytesIn == 8 && this._z.AvailableBytesOut != 0;
				}
				if (flag)
				{
					return;
				}
			}
			throw new ZlibException((this._wantCompress ? "de" : "in") + "flating: " + this._z.Message);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001B8C4 File Offset: 0x00019AC4
		private void finish()
		{
			if (this._z == null)
			{
				return;
			}
			if (this._streamMode == ZlibBaseStream.StreamMode.Writer)
			{
				int num;
				for (;;)
				{
					this._z.OutputBuffer = this.workingBuffer;
					this._z.NextOut = 0;
					this._z.AvailableBytesOut = this._workingBuffer.Length;
					num = (this._wantCompress ? this._z.Deflate(FlushType.Finish) : this._z.Inflate(FlushType.Finish));
					if (num != 1 && num != 0)
					{
						break;
					}
					if (this._workingBuffer.Length - this._z.AvailableBytesOut > 0)
					{
						this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
					}
					bool flag = this._z.AvailableBytesIn == 0 && this._z.AvailableBytesOut != 0;
					if (this._flavor == ZlibStreamFlavor.GZIP && !this._wantCompress)
					{
						flag = this._z.AvailableBytesIn == 8 && this._z.AvailableBytesOut != 0;
					}
					if (flag)
					{
						goto Block_12;
					}
				}
				string text = (this._wantCompress ? "de" : "in") + "flating";
				if (this._z.Message == null)
				{
					throw new ZlibException(string.Format("{0}: (rc = {1})", text, num));
				}
				throw new ZlibException(text + ": " + this._z.Message);
				Block_12:
				this.Flush();
				if (this._flavor == ZlibStreamFlavor.GZIP)
				{
					if (this._wantCompress)
					{
						int crc32Result = this.crc.Crc32Result;
						this._stream.Write(BitConverter.GetBytes(crc32Result), 0, 4);
						int num2 = (int)(this.crc.TotalBytesRead & (long)((ulong)(-1)));
						this._stream.Write(BitConverter.GetBytes(num2), 0, 4);
						return;
					}
					throw new ZlibException("Writing with decompression is not supported.");
				}
			}
			else if (this._streamMode == ZlibBaseStream.StreamMode.Reader && this._flavor == ZlibStreamFlavor.GZIP)
			{
				if (this._wantCompress)
				{
					throw new ZlibException("Reading with compression is not supported.");
				}
				if (this._z.TotalBytesOut == 0L)
				{
					return;
				}
				byte[] array = new byte[8];
				if (this._z.AvailableBytesIn < 8)
				{
					Array.Copy(this._z.InputBuffer, this._z.NextIn, array, 0, this._z.AvailableBytesIn);
					int num3 = 8 - this._z.AvailableBytesIn;
					int num4 = this._stream.Read(array, this._z.AvailableBytesIn, num3);
					if (num3 != num4)
					{
						throw new ZlibException(string.Format("Missing or incomplete GZIP trailer. Expected 8 bytes, got {0}.", this._z.AvailableBytesIn + num4));
					}
				}
				else
				{
					Array.Copy(this._z.InputBuffer, this._z.NextIn, array, 0, array.Length);
				}
				int num5 = BitConverter.ToInt32(array, 0);
				int crc32Result2 = this.crc.Crc32Result;
				int num6 = BitConverter.ToInt32(array, 4);
				int num7 = (int)(this._z.TotalBytesOut & (long)((ulong)(-1)));
				if (crc32Result2 != num5)
				{
					throw new ZlibException(string.Format("Bad CRC32 in GZIP trailer. (actual({0:X8})!=expected({1:X8}))", crc32Result2, num5));
				}
				if (num7 != num6)
				{
					throw new ZlibException(string.Format("Bad size in GZIP trailer. (actual({0})!=expected({1}))", num7, num6));
				}
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001BC1C File Offset: 0x00019E1C
		private void end()
		{
			if (this.z == null)
			{
				return;
			}
			if (this._wantCompress)
			{
				this._z.EndDeflate();
			}
			else
			{
				this._z.EndInflate();
			}
			this._z = null;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001BC50 File Offset: 0x00019E50
		public override void Close()
		{
			if (this._stream == null)
			{
				return;
			}
			try
			{
				this.finish();
			}
			finally
			{
				this.end();
				if (!this._leaveOpen)
				{
					this._stream.Close();
				}
				this._stream = null;
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001BCA0 File Offset: 0x00019EA0
		public override void Flush()
		{
			this._stream.Flush();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001BCAD File Offset: 0x00019EAD
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001BCB4 File Offset: 0x00019EB4
		public override void SetLength(long value)
		{
			this._stream.SetLength(value);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001BCC4 File Offset: 0x00019EC4
		private string ReadZeroTerminatedString()
		{
			List<byte> list = new List<byte>();
			bool flag = false;
			for (;;)
			{
				int num = this._stream.Read(this._buf1, 0, 1);
				if (num != 1)
				{
					break;
				}
				if (this._buf1[0] == 0)
				{
					flag = true;
				}
				else
				{
					list.Add(this._buf1[0]);
				}
				if (flag)
				{
					goto Block_3;
				}
			}
			throw new ZlibException("Unexpected EOF reading GZIP header.");
			Block_3:
			byte[] array = list.ToArray();
			return GZipStream.iso8859dash1.GetString(array, 0, array.Length);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001BD34 File Offset: 0x00019F34
		private int _ReadAndValidateGzipHeader()
		{
			int num = 0;
			byte[] array = new byte[10];
			int num2 = this._stream.Read(array, 0, array.Length);
			if (num2 == 0)
			{
				return 0;
			}
			if (num2 != 10)
			{
				throw new ZlibException("Not a valid GZIP stream.");
			}
			if (array[0] != 31 || array[1] != 139 || array[2] != 8)
			{
				throw new ZlibException("Bad GZIP header.");
			}
			int num3 = BitConverter.ToInt32(array, 4);
			this._GzipMtime = GZipStream._unixEpoch.AddSeconds((double)num3);
			num += num2;
			if ((array[3] & 4) == 4)
			{
				num2 = this._stream.Read(array, 0, 2);
				num += num2;
				short num4 = (short)((int)array[0] + (int)array[1] * 256);
				byte[] array2 = new byte[(int)num4];
				num2 = this._stream.Read(array2, 0, array2.Length);
				if (num2 != (int)num4)
				{
					throw new ZlibException("Unexpected end-of-file reading GZIP header.");
				}
				num += num2;
			}
			if ((array[3] & 8) == 8)
			{
				this._GzipFileName = this.ReadZeroTerminatedString();
			}
			if ((array[3] & 16) == 16)
			{
				this._GzipComment = this.ReadZeroTerminatedString();
			}
			if ((array[3] & 2) == 2)
			{
				this.Read(this._buf1, 0, 1);
			}
			return num;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001BE54 File Offset: 0x0001A054
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				if (!this._stream.CanRead)
				{
					throw new ZlibException("The stream is not readable.");
				}
				this._streamMode = ZlibBaseStream.StreamMode.Reader;
				this.z.AvailableBytesIn = 0;
				if (this._flavor == ZlibStreamFlavor.GZIP)
				{
					this._gzipHeaderByteCount = this._ReadAndValidateGzipHeader();
					if (this._gzipHeaderByteCount == 0)
					{
						return 0;
					}
				}
			}
			if (this._streamMode != ZlibBaseStream.StreamMode.Reader)
			{
				throw new ZlibException("Cannot Read after Writing.");
			}
			if (count == 0)
			{
				return 0;
			}
			if (this.nomoreinput && this._wantCompress)
			{
				return 0;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (offset < buffer.GetLowerBound(0))
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.GetLength(0))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._z.OutputBuffer = buffer;
			this._z.NextOut = offset;
			this._z.AvailableBytesOut = count;
			this._z.InputBuffer = this.workingBuffer;
			int num;
			for (;;)
			{
				if (this._z.AvailableBytesIn == 0 && !this.nomoreinput)
				{
					this._z.NextIn = 0;
					this._z.AvailableBytesIn = this._stream.Read(this._workingBuffer, 0, this._workingBuffer.Length);
					if (this._z.AvailableBytesIn == 0)
					{
						this.nomoreinput = true;
					}
				}
				num = (this._wantCompress ? this._z.Deflate(this._flushMode) : this._z.Inflate(this._flushMode));
				if (this.nomoreinput && num == -5)
				{
					break;
				}
				if (num != 0 && num != 1)
				{
					goto Block_20;
				}
				if (((this.nomoreinput || num == 1) && this._z.AvailableBytesOut == count) || this._z.AvailableBytesOut <= 0 || this.nomoreinput || num != 0)
				{
					goto IL_020A;
				}
			}
			return 0;
			Block_20:
			throw new ZlibException(string.Format("{0}flating:  rc={1}  msg={2}", this._wantCompress ? "de" : "in", num, this._z.Message));
			IL_020A:
			if (this._z.AvailableBytesOut > 0)
			{
				if (num == 0)
				{
					int availableBytesIn = this._z.AvailableBytesIn;
				}
				if (this.nomoreinput && this._wantCompress)
				{
					num = this._z.Deflate(FlushType.Finish);
					if (num != 0 && num != 1)
					{
						throw new ZlibException(string.Format("Deflating:  rc={0}  msg={1}", num, this._z.Message));
					}
				}
			}
			num = count - this._z.AvailableBytesOut;
			if (this.crc != null)
			{
				this.crc.SlurpBlock(buffer, offset, num);
			}
			return num;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0001C0F2 File Offset: 0x0001A2F2
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0001C0FF File Offset: 0x0001A2FF
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0001C10C File Offset: 0x0001A30C
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0001C119 File Offset: 0x0001A319
		public override long Length
		{
			get
			{
				return this._stream.Length;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0001C126 File Offset: 0x0001A326
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0001C12D File Offset: 0x0001A32D
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001C134 File Offset: 0x0001A334
		public static void CompressString(string s, Stream compressor)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			try
			{
				compressor.Write(bytes, 0, bytes.Length);
			}
			finally
			{
				if (compressor != null)
				{
					compressor.Dispose();
				}
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001C178 File Offset: 0x0001A378
		public static void CompressBuffer(byte[] b, Stream compressor)
		{
			try
			{
				compressor.Write(b, 0, b.Length);
			}
			finally
			{
				if (compressor != null)
				{
					compressor.Dispose();
				}
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
		public static string UncompressString(byte[] compressed, Stream decompressor)
		{
			byte[] array = new byte[1024];
			Encoding utf = Encoding.UTF8;
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				try
				{
					int num;
					while ((num = decompressor.Read(array, 0, array.Length)) != 0)
					{
						memoryStream.Write(array, 0, num);
					}
				}
				finally
				{
					if (decompressor != null)
					{
						decompressor.Dispose();
					}
				}
				memoryStream.Seek(0L, 0);
				StreamReader streamReader = new StreamReader(memoryStream, utf);
				text = streamReader.ReadToEnd();
			}
			return text;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001C248 File Offset: 0x0001A448
		public static byte[] UncompressBuffer(byte[] compressed, Stream decompressor)
		{
			byte[] array = new byte[1024];
			byte[] array2;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				try
				{
					int num;
					while ((num = decompressor.Read(array, 0, array.Length)) != 0)
					{
						memoryStream.Write(array, 0, num);
					}
				}
				finally
				{
					if (decompressor != null)
					{
						decompressor.Dispose();
					}
				}
				array2 = memoryStream.ToArray();
			}
			return array2;
		}

		// Token: 0x040002DC RID: 732
		protected internal ZlibCodec _z;

		// Token: 0x040002DD RID: 733
		protected internal ZlibBaseStream.StreamMode _streamMode = ZlibBaseStream.StreamMode.Undefined;

		// Token: 0x040002DE RID: 734
		protected internal FlushType _flushMode;

		// Token: 0x040002DF RID: 735
		protected internal ZlibStreamFlavor _flavor;

		// Token: 0x040002E0 RID: 736
		protected internal CompressionMode _compressionMode;

		// Token: 0x040002E1 RID: 737
		protected internal CompressionLevel _level;

		// Token: 0x040002E2 RID: 738
		protected internal bool _leaveOpen;

		// Token: 0x040002E3 RID: 739
		protected internal byte[] _workingBuffer;

		// Token: 0x040002E4 RID: 740
		protected internal int _bufferSize = 8192;

		// Token: 0x040002E5 RID: 741
		protected internal byte[] _buf1 = new byte[1];

		// Token: 0x040002E6 RID: 742
		protected internal Stream _stream;

		// Token: 0x040002E7 RID: 743
		protected internal CompressionStrategy Strategy;

		// Token: 0x040002E8 RID: 744
		private CRC32 crc;

		// Token: 0x040002E9 RID: 745
		protected internal string _GzipFileName;

		// Token: 0x040002EA RID: 746
		protected internal string _GzipComment;

		// Token: 0x040002EB RID: 747
		protected internal DateTime _GzipMtime;

		// Token: 0x040002EC RID: 748
		protected internal int _gzipHeaderByteCount;

		// Token: 0x040002ED RID: 749
		private bool nomoreinput;

		// Token: 0x02000054 RID: 84
		internal enum StreamMode
		{
			// Token: 0x040002EF RID: 751
			Writer,
			// Token: 0x040002F0 RID: 752
			Reader,
			// Token: 0x040002F1 RID: 753
			Undefined
		}
	}
}
