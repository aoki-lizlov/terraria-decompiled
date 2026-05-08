using System;
using System.IO;

namespace Ionic.BZip2
{
	// Token: 0x02000030 RID: 48
	internal class BitWriter
	{
		// Token: 0x06000292 RID: 658 RVA: 0x0000DBBC File Offset: 0x0000BDBC
		public BitWriter(Stream s)
		{
			this.output = s;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000DBCB File Offset: 0x0000BDCB
		public byte RemainingBits
		{
			get
			{
				return (byte)((this.accumulator >> 32 - this.nAccumulatedBits) & 255U);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000DBE7 File Offset: 0x0000BDE7
		public int NumRemainingBits
		{
			get
			{
				return this.nAccumulatedBits;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000DBEF File Offset: 0x0000BDEF
		public int TotalBytesWrittenOut
		{
			get
			{
				return this.totalBytesWrittenOut;
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000DBF7 File Offset: 0x0000BDF7
		public void Reset()
		{
			this.accumulator = 0U;
			this.nAccumulatedBits = 0;
			this.totalBytesWrittenOut = 0;
			this.output.Seek(0L, 0);
			this.output.SetLength(0L);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000DC2C File Offset: 0x0000BE2C
		public void WriteBits(int nbits, uint value)
		{
			int i = this.nAccumulatedBits;
			uint num = this.accumulator;
			while (i >= 8)
			{
				this.output.WriteByte((byte)((num >> 24) & 255U));
				this.totalBytesWrittenOut++;
				num <<= 8;
				i -= 8;
			}
			this.accumulator = num | (value << 32 - i - nbits);
			this.nAccumulatedBits = i + nbits;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000DC95 File Offset: 0x0000BE95
		public void WriteByte(byte b)
		{
			this.WriteBits(8, (uint)b);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000DCA0 File Offset: 0x0000BEA0
		public void WriteInt(uint u)
		{
			this.WriteBits(8, (u >> 24) & 255U);
			this.WriteBits(8, (u >> 16) & 255U);
			this.WriteBits(8, (u >> 8) & 255U);
			this.WriteBits(8, u & 255U);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000DCED File Offset: 0x0000BEED
		public void Flush()
		{
			this.WriteBits(0, 0U);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		public void FinishAndPad()
		{
			this.Flush();
			if (this.NumRemainingBits > 0)
			{
				byte b = (byte)((this.accumulator >> 24) & 255U);
				this.output.WriteByte(b);
				this.totalBytesWrittenOut++;
			}
		}

		// Token: 0x0400013D RID: 317
		private uint accumulator;

		// Token: 0x0400013E RID: 318
		private int nAccumulatedBits;

		// Token: 0x0400013F RID: 319
		private Stream output;

		// Token: 0x04000140 RID: 320
		private int totalBytesWrittenOut;
	}
}
