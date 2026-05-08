using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Ionic.BZip2
{
	// Token: 0x02000037 RID: 55
	public class BZip2OutputStream : Stream
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x00011513 File Offset: 0x0000F713
		public BZip2OutputStream(Stream output)
			: this(output, BZip2.MaxBlockSize, false)
		{
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00011522 File Offset: 0x0000F722
		public BZip2OutputStream(Stream output, int blockSize)
			: this(output, blockSize, false)
		{
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001152D File Offset: 0x0000F72D
		public BZip2OutputStream(Stream output, bool leaveOpen)
			: this(output, BZip2.MaxBlockSize, leaveOpen)
		{
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001153C File Offset: 0x0000F73C
		public BZip2OutputStream(Stream output, int blockSize, bool leaveOpen)
		{
			if (blockSize < BZip2.MinBlockSize || blockSize > BZip2.MaxBlockSize)
			{
				string text = string.Format("blockSize={0} is out of range; must be between {1} and {2}", blockSize, BZip2.MinBlockSize, BZip2.MaxBlockSize);
				throw new ArgumentException(text, "blockSize");
			}
			this.output = output;
			if (!this.output.CanWrite)
			{
				throw new ArgumentException("The stream is not writable.", "output");
			}
			this.bw = new BitWriter(this.output);
			this.blockSize100k = blockSize;
			this.compressor = new BZip2Compressor(this.bw, blockSize);
			this.leaveOpen = leaveOpen;
			this.combinedCRC = 0U;
			this.EmitHeader();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000115FC File Offset: 0x0000F7FC
		public override void Close()
		{
			if (this.output != null)
			{
				Stream stream = this.output;
				this.Finish();
				if (!this.leaveOpen)
				{
					stream.Close();
				}
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0001162C File Offset: 0x0000F82C
		public override void Flush()
		{
			if (this.output != null)
			{
				this.bw.Flush();
				this.output.Flush();
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00011654 File Offset: 0x0000F854
		private void EmitHeader()
		{
			byte[] array = new byte[] { 66, 90, 104, 0 };
			array[3] = (byte)(48 + this.blockSize100k);
			byte[] array2 = array;
			this.output.Write(array2, 0, array2.Length);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00011694 File Offset: 0x0000F894
		private void EmitTrailer()
		{
			this.bw.WriteByte(23);
			this.bw.WriteByte(114);
			this.bw.WriteByte(69);
			this.bw.WriteByte(56);
			this.bw.WriteByte(80);
			this.bw.WriteByte(144);
			this.bw.WriteInt(this.combinedCRC);
			this.bw.FinishAndPad();
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00011710 File Offset: 0x0000F910
		private void Finish()
		{
			try
			{
				int totalBytesWrittenOut = this.bw.TotalBytesWrittenOut;
				this.compressor.CompressAndWrite();
				this.combinedCRC = (this.combinedCRC << 1) | (this.combinedCRC >> 31);
				this.combinedCRC ^= this.compressor.Crc32;
				this.EmitTrailer();
			}
			finally
			{
				this.output = null;
				this.compressor = null;
				this.bw = null;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00011794 File Offset: 0x0000F994
		public int BlockSize
		{
			get
			{
				return this.blockSize100k;
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0001179C File Offset: 0x0000F99C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (offset < 0)
			{
				throw new IndexOutOfRangeException(string.Format("offset ({0}) must be > 0", offset));
			}
			if (count < 0)
			{
				throw new IndexOutOfRangeException(string.Format("count ({0}) must be > 0", count));
			}
			if (offset + count > buffer.Length)
			{
				throw new IndexOutOfRangeException(string.Format("offset({0}) count({1}) bLength({2})", offset, count, buffer.Length));
			}
			if (this.output == null)
			{
				throw new IOException("the stream is not open");
			}
			if (count == 0)
			{
				return;
			}
			int num = 0;
			int num2 = count;
			do
			{
				int num3 = this.compressor.Fill(buffer, offset, num2);
				if (num3 != num2)
				{
					int totalBytesWrittenOut = this.bw.TotalBytesWrittenOut;
					this.compressor.CompressAndWrite();
					this.combinedCRC = (this.combinedCRC << 1) | (this.combinedCRC >> 31);
					this.combinedCRC ^= this.compressor.Crc32;
					offset += num3;
				}
				num2 -= num3;
				num += num3;
			}
			while (num2 > 0);
			this.totalBytesWrittenIn += num;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0001189D File Offset: 0x0000FA9D
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000118A0 File Offset: 0x0000FAA0
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x000118A3 File Offset: 0x0000FAA3
		public override bool CanWrite
		{
			get
			{
				if (this.output == null)
				{
					throw new ObjectDisposedException("BZip2Stream");
				}
				return this.output.CanWrite;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x000118C3 File Offset: 0x0000FAC3
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x000118CA File Offset: 0x0000FACA
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x000118D3 File Offset: 0x0000FAD3
		public override long Position
		{
			get
			{
				return (long)this.totalBytesWrittenIn;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000118DA File Offset: 0x0000FADA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000118E1 File Offset: 0x0000FAE1
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000118E8 File Offset: 0x0000FAE8
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000118F0 File Offset: 0x0000FAF0
		[Conditional("Trace")]
		private void TraceOutput(BZip2OutputStream.TraceBits bits, string format, params object[] varParams)
		{
			if ((bits & this.desiredTrace) != BZip2OutputStream.TraceBits.None)
			{
				int hashCode = Thread.CurrentThread.GetHashCode();
				Console.Write("{0:000} PBOS ", hashCode);
				Console.WriteLine(format, varParams);
			}
		}

		// Token: 0x040001B7 RID: 439
		private int totalBytesWrittenIn;

		// Token: 0x040001B8 RID: 440
		private bool leaveOpen;

		// Token: 0x040001B9 RID: 441
		private BZip2Compressor compressor;

		// Token: 0x040001BA RID: 442
		private uint combinedCRC;

		// Token: 0x040001BB RID: 443
		private Stream output;

		// Token: 0x040001BC RID: 444
		private BitWriter bw;

		// Token: 0x040001BD RID: 445
		private int blockSize100k;

		// Token: 0x040001BE RID: 446
		private BZip2OutputStream.TraceBits desiredTrace = BZip2OutputStream.TraceBits.Crc | BZip2OutputStream.TraceBits.Write;

		// Token: 0x02000038 RID: 56
		[Flags]
		private enum TraceBits : uint
		{
			// Token: 0x040001C0 RID: 448
			None = 0U,
			// Token: 0x040001C1 RID: 449
			Crc = 1U,
			// Token: 0x040001C2 RID: 450
			Write = 2U,
			// Token: 0x040001C3 RID: 451
			All = 4294967295U
		}
	}
}
