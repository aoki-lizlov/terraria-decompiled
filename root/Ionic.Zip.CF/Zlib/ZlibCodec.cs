using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x02000055 RID: 85
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000D")]
	[ComVisible(true)]
	public sealed class ZlibCodec
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0001C2C4 File Offset: 0x0001A4C4
		public int Adler32
		{
			get
			{
				return (int)this._Adler32;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001C2CC File Offset: 0x0001A4CC
		public ZlibCodec()
		{
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001C2E4 File Offset: 0x0001A4E4
		public ZlibCodec(CompressionMode mode)
		{
			if (mode == CompressionMode.Compress)
			{
				int num = this.InitializeDeflate();
				if (num != 0)
				{
					throw new ZlibException("Cannot initialize for deflate.");
				}
			}
			else
			{
				if (mode != CompressionMode.Decompress)
				{
					throw new ZlibException("Invalid ZlibStreamFlavor.");
				}
				int num2 = this.InitializeInflate();
				if (num2 != 0)
				{
					throw new ZlibException("Cannot initialize for inflate.");
				}
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001C342 File Offset: 0x0001A542
		public int InitializeInflate()
		{
			return this.InitializeInflate(this.WindowBits);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001C350 File Offset: 0x0001A550
		public int InitializeInflate(bool expectRfc1950Header)
		{
			return this.InitializeInflate(this.WindowBits, expectRfc1950Header);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001C35F File Offset: 0x0001A55F
		public int InitializeInflate(int windowBits)
		{
			this.WindowBits = windowBits;
			return this.InitializeInflate(windowBits, true);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001C370 File Offset: 0x0001A570
		public int InitializeInflate(int windowBits, bool expectRfc1950Header)
		{
			this.WindowBits = windowBits;
			if (this.dstate != null)
			{
				throw new ZlibException("You may not call InitializeInflate() after calling InitializeDeflate().");
			}
			this.istate = new InflateManager(expectRfc1950Header);
			return this.istate.Initialize(this, windowBits);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001C3A5 File Offset: 0x0001A5A5
		public int Inflate(FlushType flush)
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Inflate(flush);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001C3C8 File Offset: 0x0001A5C8
		public int EndInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			int num = this.istate.End();
			this.istate = null;
			return num;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001C3FC File Offset: 0x0001A5FC
		public int SyncInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Sync();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001C41C File Offset: 0x0001A61C
		public int InitializeDeflate()
		{
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001C425 File Offset: 0x0001A625
		public int InitializeDeflate(CompressionLevel level)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001C435 File Offset: 0x0001A635
		public int InitializeDeflate(CompressionLevel level, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001C445 File Offset: 0x0001A645
		public int InitializeDeflate(CompressionLevel level, int bits)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001C45C File Offset: 0x0001A65C
		public int InitializeDeflate(CompressionLevel level, int bits, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001C474 File Offset: 0x0001A674
		private int _InternalInitializeDeflate(bool wantRfc1950Header)
		{
			if (this.istate != null)
			{
				throw new ZlibException("You may not call InitializeDeflate() after calling InitializeInflate().");
			}
			this.dstate = new DeflateManager();
			this.dstate.WantRfc1950HeaderBytes = wantRfc1950Header;
			return this.dstate.Initialize(this, this.CompressLevel, this.WindowBits, this.Strategy);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001C4C9 File Offset: 0x0001A6C9
		public int Deflate(FlushType flush)
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.Deflate(flush);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001C4EA File Offset: 0x0001A6EA
		public int EndDeflate()
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate = null;
			return 0;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001C507 File Offset: 0x0001A707
		public void ResetDeflate()
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate.Reset();
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001C527 File Offset: 0x0001A727
		public int SetDeflateParams(CompressionLevel level, CompressionStrategy strategy)
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.SetParams(level, strategy);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001C549 File Offset: 0x0001A749
		public int SetDictionary(byte[] dictionary)
		{
			if (this.istate != null)
			{
				return this.istate.SetDictionary(dictionary);
			}
			if (this.dstate != null)
			{
				return this.dstate.SetDictionary(dictionary);
			}
			throw new ZlibException("No Inflate or Deflate state!");
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001C580 File Offset: 0x0001A780
		internal void flush_pending()
		{
			int num = this.dstate.pendingCount;
			if (num > this.AvailableBytesOut)
			{
				num = this.AvailableBytesOut;
			}
			if (num == 0)
			{
				return;
			}
			if (this.dstate.pending.Length <= this.dstate.nextPending || this.OutputBuffer.Length <= this.NextOut || this.dstate.pending.Length < this.dstate.nextPending + num || this.OutputBuffer.Length < this.NextOut + num)
			{
				throw new ZlibException(string.Format("Invalid State. (pending.Length={0}, pendingCount={1})", this.dstate.pending.Length, this.dstate.pendingCount));
			}
			Array.Copy(this.dstate.pending, this.dstate.nextPending, this.OutputBuffer, this.NextOut, num);
			this.NextOut += num;
			this.dstate.nextPending += num;
			this.TotalBytesOut += (long)num;
			this.AvailableBytesOut -= num;
			this.dstate.pendingCount -= num;
			if (this.dstate.pendingCount == 0)
			{
				this.dstate.nextPending = 0;
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001C6CC File Offset: 0x0001A8CC
		internal int read_buf(byte[] buf, int start, int size)
		{
			int num = this.AvailableBytesIn;
			if (num > size)
			{
				num = size;
			}
			if (num == 0)
			{
				return 0;
			}
			this.AvailableBytesIn -= num;
			if (this.dstate.WantRfc1950HeaderBytes)
			{
				this._Adler32 = Adler.Adler32(this._Adler32, this.InputBuffer, this.NextIn, num);
			}
			Array.Copy(this.InputBuffer, this.NextIn, buf, start, num);
			this.NextIn += num;
			this.TotalBytesIn += (long)num;
			return num;
		}

		// Token: 0x040002F2 RID: 754
		public byte[] InputBuffer;

		// Token: 0x040002F3 RID: 755
		public int NextIn;

		// Token: 0x040002F4 RID: 756
		public int AvailableBytesIn;

		// Token: 0x040002F5 RID: 757
		public long TotalBytesIn;

		// Token: 0x040002F6 RID: 758
		public byte[] OutputBuffer;

		// Token: 0x040002F7 RID: 759
		public int NextOut;

		// Token: 0x040002F8 RID: 760
		public int AvailableBytesOut;

		// Token: 0x040002F9 RID: 761
		public long TotalBytesOut;

		// Token: 0x040002FA RID: 762
		public string Message;

		// Token: 0x040002FB RID: 763
		internal DeflateManager dstate;

		// Token: 0x040002FC RID: 764
		internal InflateManager istate;

		// Token: 0x040002FD RID: 765
		internal uint _Adler32;

		// Token: 0x040002FE RID: 766
		public CompressionLevel CompressLevel = CompressionLevel.Default;

		// Token: 0x040002FF RID: 767
		public int WindowBits = 15;

		// Token: 0x04000300 RID: 768
		public CompressionStrategy Strategy;
	}
}
