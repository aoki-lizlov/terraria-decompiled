using System;
using System.IO;

namespace Ionic.Crc
{
	// Token: 0x02000059 RID: 89
	public class CrcCalculatorStream : Stream, IDisposable
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x0001CF9E File Offset: 0x0001B19E
		public CrcCalculatorStream(Stream stream)
			: this(true, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001CFAE File Offset: 0x0001B1AE
		public CrcCalculatorStream(Stream stream, bool leaveOpen)
			: this(leaveOpen, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001CFBE File Offset: 0x0001B1BE
		public CrcCalculatorStream(Stream stream, long length)
			: this(true, length, stream, null)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001CFDA File Offset: 0x0001B1DA
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen)
			: this(leaveOpen, length, stream, null)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001CFF6 File Offset: 0x0001B1F6
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen, CRC32 crc32)
			: this(leaveOpen, length, stream, crc32)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001D013 File Offset: 0x0001B213
		private CrcCalculatorStream(bool leaveOpen, long length, Stream stream, CRC32 crc32)
		{
			this._innerStream = stream;
			this._Crc32 = crc32 ?? new CRC32();
			this._lengthLimit = length;
			this._leaveOpen = leaveOpen;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0001D04A File Offset: 0x0001B24A
		public long TotalBytesSlurped
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0001D057 File Offset: 0x0001B257
		public int Crc
		{
			get
			{
				return this._Crc32.Crc32Result;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0001D064 File Offset: 0x0001B264
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0001D06C File Offset: 0x0001B26C
		public bool LeaveOpen
		{
			get
			{
				return this._leaveOpen;
			}
			set
			{
				this._leaveOpen = value;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001D078 File Offset: 0x0001B278
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = count;
			if (this._lengthLimit != CrcCalculatorStream.UnsetLengthLimit)
			{
				if (this._Crc32.TotalBytesRead >= this._lengthLimit)
				{
					return 0;
				}
				long num2 = this._lengthLimit - this._Crc32.TotalBytesRead;
				if (num2 < (long)count)
				{
					num = (int)num2;
				}
			}
			int num3 = this._innerStream.Read(buffer, offset, num);
			if (num3 > 0)
			{
				this._Crc32.SlurpBlock(buffer, offset, num3);
			}
			return num3;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001D0E6 File Offset: 0x0001B2E6
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count > 0)
			{
				this._Crc32.SlurpBlock(buffer, offset, count);
			}
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0001D108 File Offset: 0x0001B308
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0001D115 File Offset: 0x0001B315
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0001D118 File Offset: 0x0001B318
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001D125 File Offset: 0x0001B325
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0001D132 File Offset: 0x0001B332
		public override long Length
		{
			get
			{
				if (this._lengthLimit == CrcCalculatorStream.UnsetLengthLimit)
				{
					return this._innerStream.Length;
				}
				return this._lengthLimit;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0001D153 File Offset: 0x0001B353
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x0001D160 File Offset: 0x0001B360
		public override long Position
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001D167 File Offset: 0x0001B367
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001D16E File Offset: 0x0001B36E
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001D175 File Offset: 0x0001B375
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001D17D File Offset: 0x0001B37D
		public override void Close()
		{
			base.Close();
			if (!this._leaveOpen)
			{
				this._innerStream.Close();
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001D198 File Offset: 0x0001B398
		// Note: this type is marked as 'beforefieldinit'.
		static CrcCalculatorStream()
		{
		}

		// Token: 0x04000313 RID: 787
		private static readonly long UnsetLengthLimit = -99L;

		// Token: 0x04000314 RID: 788
		internal Stream _innerStream;

		// Token: 0x04000315 RID: 789
		private CRC32 _Crc32;

		// Token: 0x04000316 RID: 790
		private long _lengthLimit = -99L;

		// Token: 0x04000317 RID: 791
		private bool _leaveOpen;
	}
}
