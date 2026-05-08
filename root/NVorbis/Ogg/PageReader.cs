using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using NVorbis.Contracts;
using NVorbis.Contracts.Ogg;

namespace NVorbis.Ogg
{
	// Token: 0x02000020 RID: 32
	internal class PageReader : PageReaderBase, IPageData, IPageReader, IDisposable
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000074F5 File Offset: 0x000056F5
		// (set) Token: 0x06000151 RID: 337 RVA: 0x000074FC File Offset: 0x000056FC
		internal static Func<IPageData, int, IStreamPageReader> CreateStreamPageReader
		{
			[CompilerGenerated]
			get
			{
				return PageReader.<CreateStreamPageReader>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				PageReader.<CreateStreamPageReader>k__BackingField = value;
			}
		} = (IPageData pr, int ss) => new StreamPageReader(pr, ss);

		// Token: 0x06000152 RID: 338 RVA: 0x00007504 File Offset: 0x00005704
		public PageReader(Stream stream, bool closeOnDispose, Func<IPacketProvider, bool> newStreamCallback)
			: base(stream, closeOnDispose)
		{
			this._newStreamCallback = newStreamCallback;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000752C File Offset: 0x0000572C
		private ushort ParsePageHeader(byte[] pageBuf, int? streamSerial, bool? isResync)
		{
			byte b = pageBuf[26];
			int num = 0;
			int num2 = 0;
			bool flag = false;
			int num3 = 0;
			int i = 0;
			int num4 = 27;
			while (i < (int)b)
			{
				byte b2 = pageBuf[num4];
				num3 += (int)b2;
				num += (int)b2;
				if (b2 < 255)
				{
					if (num3 > 0)
					{
						num2++;
					}
					num3 = 0;
				}
				i++;
				num4++;
			}
			if (num3 > 0)
			{
				flag = pageBuf[(int)(b + 26)] == byte.MaxValue;
				num2++;
			}
			this.StreamSerial = streamSerial ?? BitConverter.ToInt32(pageBuf, 14);
			this.SequenceNumber = BitConverter.ToInt32(pageBuf, 18);
			this.PageFlags = (PageFlags)pageBuf[5];
			this.GranulePosition = BitConverter.ToInt64(pageBuf, 6);
			this.PacketCount = (short)num2;
			this.IsResync = isResync;
			this.IsContinued = flag;
			this.PageOverhead = (int)(27 + b);
			return (ushort)(this.PageOverhead + num);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007610 File Offset: 0x00005810
		private unsafe static Memory<byte>[] ReadPackets(int packetCount, Span<byte> segments, Memory<byte> dataBuffer)
		{
			Memory<byte>[] array = new Memory<byte>[packetCount];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < segments.Length; i++)
			{
				byte b = *segments[i];
				num3 += (int)b;
				if (b < 255)
				{
					if (num3 > 0)
					{
						array[num++] = dataBuffer.Slice(num2, num3);
						num2 += num3;
					}
					num3 = 0;
				}
			}
			if (num3 > 0)
			{
				array[num] = dataBuffer.Slice(num2, num3);
			}
			return array;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007683 File Offset: 0x00005883
		public override void Lock()
		{
			Monitor.Enter(this._readLock);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007690 File Offset: 0x00005890
		protected override bool CheckLock()
		{
			return true;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00007694 File Offset: 0x00005894
		public override bool Release()
		{
			bool flag;
			try
			{
				Monitor.Exit(this._readLock);
				flag = true;
			}
			catch (SynchronizationLockException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000076C8 File Offset: 0x000058C8
		protected override void SaveNextPageSearch()
		{
			this._nextPageOffset = base.StreamPosition;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000076D6 File Offset: 0x000058D6
		protected override void PrepareStreamForNextPage()
		{
			base.SeekStream(this._nextPageOffset);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000076E8 File Offset: 0x000058E8
		protected override bool AddPage(int streamSerial, byte[] pageBuf, bool isResync)
		{
			this.PageOffset = base.StreamPosition - (long)pageBuf.Length;
			this.ParsePageHeader(pageBuf, new int?(streamSerial), new bool?(isResync));
			if (this.PacketCount == 0)
			{
				return false;
			}
			this._packets = PageReader.ReadPackets((int)this.PacketCount, new Span<byte>(pageBuf, 27, (int)pageBuf[26]), new Memory<byte>(pageBuf, (int)(27 + pageBuf[26]), pageBuf.Length - 27 - (int)pageBuf[26]));
			IStreamPageReader streamPageReader;
			if (this._streamReaders.TryGetValue(streamSerial, ref streamPageReader))
			{
				streamPageReader.AddPage();
				if ((this.PageFlags & PageFlags.EndOfStream) == PageFlags.EndOfStream)
				{
					this._streamReaders.Remove(this.StreamSerial);
				}
			}
			else
			{
				IStreamPageReader streamPageReader2 = PageReader.CreateStreamPageReader.Invoke(this, this.StreamSerial);
				streamPageReader2.AddPage();
				this._streamReaders.Add(this.StreamSerial, streamPageReader2);
				if (!this._newStreamCallback.Invoke(streamPageReader2.PacketProvider))
				{
					this._streamReaders.Remove(this.StreamSerial);
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000077E0 File Offset: 0x000059E0
		public override bool ReadPageAt(long offset)
		{
			if (!this.CheckLock())
			{
				throw new InvalidOperationException("Must be locked prior to reading!");
			}
			if (offset == this.PageOffset)
			{
				return true;
			}
			byte[] array = new byte[282];
			base.SeekStream(offset);
			int num = base.EnsureRead(array, 0, 27, 10);
			this.PageOffset = offset;
			if (base.VerifyHeader(array, 0, ref num))
			{
				this._packets = null;
				this._pageSize = this.ParsePageHeader(array, default(int?), default(bool?));
				return true;
			}
			return false;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00007868 File Offset: 0x00005A68
		protected override void SetEndOfStreams()
		{
			foreach (KeyValuePair<int, IStreamPageReader> keyValuePair in this._streamReaders)
			{
				keyValuePair.Value.SetEndOfStream();
			}
			this._streamReaders.Clear();
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000078CC File Offset: 0x00005ACC
		// (set) Token: 0x0600015E RID: 350 RVA: 0x000078D4 File Offset: 0x00005AD4
		public long PageOffset
		{
			[CompilerGenerated]
			get
			{
				return this.<PageOffset>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PageOffset>k__BackingField = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000078DD File Offset: 0x00005ADD
		// (set) Token: 0x06000160 RID: 352 RVA: 0x000078E5 File Offset: 0x00005AE5
		public int StreamSerial
		{
			[CompilerGenerated]
			get
			{
				return this.<StreamSerial>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<StreamSerial>k__BackingField = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000078EE File Offset: 0x00005AEE
		// (set) Token: 0x06000162 RID: 354 RVA: 0x000078F6 File Offset: 0x00005AF6
		public int SequenceNumber
		{
			[CompilerGenerated]
			get
			{
				return this.<SequenceNumber>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SequenceNumber>k__BackingField = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000078FF File Offset: 0x00005AFF
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00007907 File Offset: 0x00005B07
		public PageFlags PageFlags
		{
			[CompilerGenerated]
			get
			{
				return this.<PageFlags>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PageFlags>k__BackingField = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00007910 File Offset: 0x00005B10
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00007918 File Offset: 0x00005B18
		public long GranulePosition
		{
			[CompilerGenerated]
			get
			{
				return this.<GranulePosition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GranulePosition>k__BackingField = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00007921 File Offset: 0x00005B21
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00007929 File Offset: 0x00005B29
		public short PacketCount
		{
			[CompilerGenerated]
			get
			{
				return this.<PacketCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PacketCount>k__BackingField = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00007932 File Offset: 0x00005B32
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000793A File Offset: 0x00005B3A
		public bool? IsResync
		{
			[CompilerGenerated]
			get
			{
				return this.<IsResync>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsResync>k__BackingField = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00007943 File Offset: 0x00005B43
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000794B File Offset: 0x00005B4B
		public bool IsContinued
		{
			[CompilerGenerated]
			get
			{
				return this.<IsContinued>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsContinued>k__BackingField = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00007954 File Offset: 0x00005B54
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000795C File Offset: 0x00005B5C
		public int PageOverhead
		{
			[CompilerGenerated]
			get
			{
				return this.<PageOverhead>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PageOverhead>k__BackingField = value;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007968 File Offset: 0x00005B68
		public Memory<byte>[] GetPackets()
		{
			if (!this.CheckLock())
			{
				throw new InvalidOperationException("Must be locked!");
			}
			if (this._packets == null)
			{
				byte[] array = new byte[(int)this._pageSize];
				base.SeekStream(this.PageOffset);
				base.EnsureRead(array, 0, (int)this._pageSize, 10);
				this._packets = PageReader.ReadPackets((int)this.PacketCount, new Span<byte>(array, 27, (int)array[26]), new Memory<byte>(array, (int)(27 + array[26]), array.Length - 27 - (int)array[26]));
			}
			return this._packets;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000079F5 File Offset: 0x00005BF5
		// Note: this type is marked as 'beforefieldinit'.
		static PageReader()
		{
		}

		// Token: 0x040000AD RID: 173
		[CompilerGenerated]
		private static Func<IPageData, int, IStreamPageReader> <CreateStreamPageReader>k__BackingField;

		// Token: 0x040000AE RID: 174
		private readonly Dictionary<int, IStreamPageReader> _streamReaders = new Dictionary<int, IStreamPageReader>();

		// Token: 0x040000AF RID: 175
		private readonly Func<IPacketProvider, bool> _newStreamCallback;

		// Token: 0x040000B0 RID: 176
		private readonly object _readLock = new object();

		// Token: 0x040000B1 RID: 177
		private long _nextPageOffset;

		// Token: 0x040000B2 RID: 178
		private ushort _pageSize;

		// Token: 0x040000B3 RID: 179
		private Memory<byte>[] _packets;

		// Token: 0x040000B4 RID: 180
		[CompilerGenerated]
		private long <PageOffset>k__BackingField;

		// Token: 0x040000B5 RID: 181
		[CompilerGenerated]
		private int <StreamSerial>k__BackingField;

		// Token: 0x040000B6 RID: 182
		[CompilerGenerated]
		private int <SequenceNumber>k__BackingField;

		// Token: 0x040000B7 RID: 183
		[CompilerGenerated]
		private PageFlags <PageFlags>k__BackingField;

		// Token: 0x040000B8 RID: 184
		[CompilerGenerated]
		private long <GranulePosition>k__BackingField;

		// Token: 0x040000B9 RID: 185
		[CompilerGenerated]
		private short <PacketCount>k__BackingField;

		// Token: 0x040000BA RID: 186
		[CompilerGenerated]
		private bool? <IsResync>k__BackingField;

		// Token: 0x040000BB RID: 187
		[CompilerGenerated]
		private bool <IsContinued>k__BackingField;

		// Token: 0x040000BC RID: 188
		[CompilerGenerated]
		private int <PageOverhead>k__BackingField;

		// Token: 0x02000048 RID: 72
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600028E RID: 654 RVA: 0x00009C56 File Offset: 0x00007E56
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600028F RID: 655 RVA: 0x00009C62 File Offset: 0x00007E62
			public <>c()
			{
			}

			// Token: 0x06000290 RID: 656 RVA: 0x00009C6A File Offset: 0x00007E6A
			internal IStreamPageReader <.cctor>b__58_0(IPageData pr, int ss)
			{
				return new StreamPageReader(pr, ss);
			}

			// Token: 0x0400010D RID: 269
			public static readonly PageReader.<>c <>9 = new PageReader.<>c();
		}
	}
}
