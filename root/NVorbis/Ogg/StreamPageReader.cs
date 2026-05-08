using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using NVorbis.Contracts;
using NVorbis.Contracts.Ogg;

namespace NVorbis.Ogg
{
	// Token: 0x02000022 RID: 34
	internal class StreamPageReader : IStreamPageReader
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00007FCE File Offset: 0x000061CE
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00007FD5 File Offset: 0x000061D5
		internal static Func<IStreamPageReader, int, IPacketProvider> CreatePacketProvider
		{
			[CompilerGenerated]
			get
			{
				return StreamPageReader.<CreatePacketProvider>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				StreamPageReader.<CreatePacketProvider>k__BackingField = value;
			}
		} = (IStreamPageReader pr, int ss) => new PacketProvider(pr, ss);

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00007FDD File Offset: 0x000061DD
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00007FE5 File Offset: 0x000061E5
		public IPacketProvider PacketProvider
		{
			[CompilerGenerated]
			get
			{
				return this.<PacketProvider>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PacketProvider>k__BackingField = value;
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007FEE File Offset: 0x000061EE
		public StreamPageReader(IPageData pageReader, int streamSerial)
		{
			this._reader = pageReader;
			this.PacketProvider = StreamPageReader.CreatePacketProvider.Invoke(this, streamSerial);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008024 File Offset: 0x00006224
		public void AddPage()
		{
			if (!this.HasAllPages)
			{
				if (this._reader.GranulePosition != -1L)
				{
					if (this._firstDataPageIndex == null && this._reader.GranulePosition > 0L)
					{
						this._firstDataPageIndex = new int?(this._pageOffsets.Count);
					}
					else if (this._maxGranulePos > this._reader.GranulePosition)
					{
						throw new InvalidDataException("Granule Position regressed?!");
					}
					this._maxGranulePos = this._reader.GranulePosition;
				}
				else if (this._firstDataPageIndex != null && (!this._reader.IsContinued || this._reader.PacketCount != 1))
				{
					throw new InvalidDataException("Granule Position was -1 but page does not have exactly 1 continued packet.");
				}
				if ((this._reader.PageFlags & PageFlags.EndOfStream) != PageFlags.None)
				{
					this.HasAllPages = true;
				}
				if (this._reader.IsResync.Value || (this._lastSeqNbr != 0 && this._lastSeqNbr + 1 != this._reader.SequenceNumber))
				{
					this._pageOffsets.Add(-this._reader.PageOffset);
				}
				else
				{
					this._pageOffsets.Add(this._reader.PageOffset);
				}
				this._lastSeqNbr = this._reader.SequenceNumber;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000816C File Offset: 0x0000636C
		public Memory<byte>[] GetPagePackets(int pageIndex)
		{
			if (this._cachedPagePackets != null && this._lastPageIndex == pageIndex)
			{
				return this._cachedPagePackets;
			}
			long num = this._pageOffsets[pageIndex];
			if (num < 0L)
			{
				num = -num;
			}
			this._reader.Lock();
			Memory<byte>[] array;
			try
			{
				this._reader.ReadPageAt(num);
				Memory<byte>[] packets = this._reader.GetPackets();
				if (pageIndex == this._lastPageIndex)
				{
					this._cachedPagePackets = packets;
				}
				array = packets;
			}
			finally
			{
				this._reader.Release();
			}
			return array;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000081FC File Offset: 0x000063FC
		public int FindPage(long granulePos)
		{
			int num = -1;
			if (granulePos == 0L)
			{
				num = this.FindFirstDataPage();
			}
			else
			{
				int num2 = this._pageOffsets.Count - 1;
				long num3;
				if (this.GetPageRaw(num2, out num3))
				{
					if (granulePos < num3)
					{
						num = this.FindPageBisection(granulePos, this.FindFirstDataPage(), num2, num3);
					}
					else if (granulePos > num3)
					{
						num = this.FindPageForward(num2, num3, granulePos);
					}
					else
					{
						num = num2 + 1;
					}
				}
			}
			if (num == -1)
			{
				throw new ArgumentOutOfRangeException("granulePos");
			}
			return num;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000826C File Offset: 0x0000646C
		private int FindFirstDataPage()
		{
			while (this._firstDataPageIndex == null)
			{
				long num;
				if (!this.GetPageRaw(this._pageOffsets.Count, out num))
				{
					return 0;
				}
			}
			return this._firstDataPageIndex.Value;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000082AC File Offset: 0x000064AC
		private int FindPageForward(int pageIndex, long pageGranulePos, long granulePos)
		{
			while (pageGranulePos <= granulePos)
			{
				if (++pageIndex == this._pageOffsets.Count)
				{
					if (!this.GetNextPageGranulePos(out pageGranulePos))
					{
						long? maxGranulePosition = this.MaxGranulePosition;
						if ((maxGranulePosition.GetValueOrDefault() < granulePos) & (maxGranulePosition != null))
						{
							pageIndex = -1;
							break;
						}
						break;
					}
				}
				else if (!this.GetPageRaw(pageIndex, out pageGranulePos))
				{
					pageIndex = -1;
					break;
				}
			}
			return pageIndex;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008310 File Offset: 0x00006510
		private bool GetNextPageGranulePos(out long granulePos)
		{
			int count = this._pageOffsets.Count;
			while (count == this._pageOffsets.Count && !this.HasAllPages)
			{
				this._reader.Lock();
				try
				{
					if (!this._reader.ReadNextPage())
					{
						this.HasAllPages = true;
					}
					else if (count < this._pageOffsets.Count)
					{
						granulePos = this._reader.GranulePosition;
						return true;
					}
				}
				finally
				{
					this._reader.Release();
				}
			}
			granulePos = 0L;
			return false;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000083A8 File Offset: 0x000065A8
		private int FindPageBisection(long granulePos, int low, int high, long highGranulePos)
		{
			long num = 0L;
			int num2;
			while ((num2 = high - low) > 0)
			{
				int num3 = low + (int)((double)num2 * ((double)(granulePos - num) / (double)(highGranulePos - num)));
				long num4;
				if (!this.GetPageRaw(num3, out num4))
				{
					return -1;
				}
				if (num4 > granulePos)
				{
					high = num3;
					highGranulePos = num4;
				}
				else
				{
					if (num4 >= granulePos)
					{
						return num3 + 1;
					}
					low = num3 + 1;
					num = num4 + 1L;
				}
			}
			return low;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008404 File Offset: 0x00006604
		private bool GetPageRaw(int pageIndex, out long pageGranulePos)
		{
			pageGranulePos = 0L;
			if (pageIndex >= this._pageOffsets.Count)
			{
				return false;
			}
			long num = this._pageOffsets[pageIndex];
			if (num < 0L)
			{
				num = -num;
			}
			this._reader.Lock();
			bool flag;
			try
			{
				if (this._reader.ReadPageAt(num))
				{
					pageGranulePos = this._reader.GranulePosition;
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			finally
			{
				this._reader.Release();
			}
			return flag;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00008488 File Offset: 0x00006688
		public bool GetPage(int pageIndex, out long granulePos, out bool isResync, out bool isContinuation, out bool isContinued, out int packetCount, out int pageOverhead)
		{
			if (this._lastPageIndex == pageIndex)
			{
				granulePos = this._lastPageGranulePos;
				isResync = this._lastPageIsResync;
				isContinuation = this._lastPageIsContinuation;
				isContinued = this._lastPageIsContinued;
				packetCount = this._lastPagePacketCount;
				pageOverhead = this._lastPageOverhead;
				return true;
			}
			this._reader.Lock();
			try
			{
				while (pageIndex >= this._pageOffsets.Count && !this.HasAllPages && this._reader.ReadNextPage())
				{
					if (pageIndex < this._pageOffsets.Count)
					{
						isResync = this._reader.IsResync.Value;
						this.ReadPageData(pageIndex, out granulePos, out isContinuation, out isContinued, out packetCount, out pageOverhead);
						return true;
					}
				}
			}
			finally
			{
				this._reader.Release();
			}
			if (pageIndex < this._pageOffsets.Count)
			{
				long num = this._pageOffsets[pageIndex];
				if (num < 0L)
				{
					isResync = true;
					num = -num;
				}
				else
				{
					isResync = false;
				}
				this._reader.Lock();
				try
				{
					if (this._reader.ReadPageAt(num))
					{
						this._lastPageIsResync = isResync;
						this.ReadPageData(pageIndex, out granulePos, out isContinuation, out isContinued, out packetCount, out pageOverhead);
						return true;
					}
				}
				finally
				{
					this._reader.Release();
				}
			}
			granulePos = 0L;
			isResync = false;
			isContinuation = false;
			isContinued = false;
			packetCount = 0;
			pageOverhead = 0;
			return false;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000085F4 File Offset: 0x000067F4
		private void ReadPageData(int pageIndex, out long granulePos, out bool isContinuation, out bool isContinued, out int packetCount, out int pageOverhead)
		{
			this._cachedPagePackets = null;
			this._lastPageGranulePos = (granulePos = this._reader.GranulePosition);
			this._lastPageIsContinuation = (isContinuation = (this._reader.PageFlags & PageFlags.ContinuesPacket) > PageFlags.None);
			this._lastPageIsContinued = (isContinued = this._reader.IsContinued);
			this._lastPagePacketCount = (packetCount = (int)this._reader.PacketCount);
			this._lastPageOverhead = (pageOverhead = this._reader.PageOverhead);
			this._lastPageIndex = pageIndex;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008685 File Offset: 0x00006885
		public void SetEndOfStream()
		{
			this.HasAllPages = true;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000868E File Offset: 0x0000688E
		public int PageCount
		{
			get
			{
				return this._pageOffsets.Count;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000869B File Offset: 0x0000689B
		// (set) Token: 0x0600019F RID: 415 RVA: 0x000086A3 File Offset: 0x000068A3
		public bool HasAllPages
		{
			[CompilerGenerated]
			get
			{
				return this.<HasAllPages>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasAllPages>k__BackingField = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000086AC File Offset: 0x000068AC
		public long? MaxGranulePosition
		{
			get
			{
				if (!this.HasAllPages)
				{
					return default(long?);
				}
				return new long?(this._maxGranulePos);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000086D6 File Offset: 0x000068D6
		public int FirstDataPageIndex
		{
			get
			{
				return this.FindFirstDataPage();
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000086DE File Offset: 0x000068DE
		// Note: this type is marked as 'beforefieldinit'.
		static StreamPageReader()
		{
		}

		// Token: 0x040000C7 RID: 199
		[CompilerGenerated]
		private static Func<IStreamPageReader, int, IPacketProvider> <CreatePacketProvider>k__BackingField;

		// Token: 0x040000C8 RID: 200
		private readonly IPageData _reader;

		// Token: 0x040000C9 RID: 201
		private readonly List<long> _pageOffsets = new List<long>();

		// Token: 0x040000CA RID: 202
		private int _lastSeqNbr;

		// Token: 0x040000CB RID: 203
		private int? _firstDataPageIndex;

		// Token: 0x040000CC RID: 204
		private long _maxGranulePos;

		// Token: 0x040000CD RID: 205
		private int _lastPageIndex = -1;

		// Token: 0x040000CE RID: 206
		private long _lastPageGranulePos;

		// Token: 0x040000CF RID: 207
		private bool _lastPageIsResync;

		// Token: 0x040000D0 RID: 208
		private bool _lastPageIsContinuation;

		// Token: 0x040000D1 RID: 209
		private bool _lastPageIsContinued;

		// Token: 0x040000D2 RID: 210
		private int _lastPagePacketCount;

		// Token: 0x040000D3 RID: 211
		private int _lastPageOverhead;

		// Token: 0x040000D4 RID: 212
		private Memory<byte>[] _cachedPagePackets;

		// Token: 0x040000D5 RID: 213
		[CompilerGenerated]
		private IPacketProvider <PacketProvider>k__BackingField;

		// Token: 0x040000D6 RID: 214
		[CompilerGenerated]
		private bool <HasAllPages>k__BackingField;

		// Token: 0x0200004A RID: 74
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000294 RID: 660 RVA: 0x00009C8E File Offset: 0x00007E8E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000295 RID: 661 RVA: 0x00009C9A File Offset: 0x00007E9A
			public <>c()
			{
			}

			// Token: 0x06000296 RID: 662 RVA: 0x00009CA2 File Offset: 0x00007EA2
			internal IPacketProvider <.cctor>b__43_0(IStreamPageReader pr, int ss)
			{
				return new PacketProvider(pr, ss);
			}

			// Token: 0x0400010F RID: 271
			public static readonly StreamPageReader.<>c <>9 = new StreamPageReader.<>c();
		}
	}
}
