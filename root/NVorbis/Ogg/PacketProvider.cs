using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using NVorbis.Contracts;
using NVorbis.Contracts.Ogg;

namespace NVorbis.Ogg
{
	// Token: 0x0200001F RID: 31
	internal class PacketProvider : IPacketProvider, IPacketReader
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006F66 File Offset: 0x00005166
		public bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006F69 File Offset: 0x00005169
		public int StreamSerial
		{
			[CompilerGenerated]
			get
			{
				return this.<StreamSerial>k__BackingField;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006F71 File Offset: 0x00005171
		internal PacketProvider(IStreamPageReader reader, int streamSerial)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this._reader = reader;
			this.StreamSerial = streamSerial;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006F98 File Offset: 0x00005198
		public long GetGranuleCount()
		{
			if (this._reader == null)
			{
				throw new ObjectDisposedException("PacketProvider");
			}
			if (!this._reader.HasAllPages)
			{
				long num;
				bool flag;
				bool flag2;
				bool flag3;
				int num2;
				int num3;
				this._reader.GetPage(int.MaxValue, out num, out flag, out flag2, out flag3, out num2, out num3);
			}
			return this._reader.MaxGranulePosition.Value;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006FF6 File Offset: 0x000051F6
		public IPacket GetNextPacket()
		{
			return this.GetNextPacket(ref this._pageIndex, ref this._packetIndex);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000700C File Offset: 0x0000520C
		public IPacket PeekNextPacket()
		{
			int pageIndex = this._pageIndex;
			int packetIndex = this._packetIndex;
			return this.GetNextPacket(ref pageIndex, ref packetIndex);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007034 File Offset: 0x00005234
		public long SeekTo(long granulePos, int preRoll, GetPacketGranuleCount getPacketGranuleCount)
		{
			if (this._reader == null)
			{
				throw new ObjectDisposedException("PacketProvider");
			}
			int num;
			int num2;
			if (granulePos == 0L)
			{
				num = this._reader.FirstDataPageIndex;
				num2 = 0;
			}
			else
			{
				num = this._reader.FindPage(granulePos);
				if (this._reader.HasAllPages)
				{
					long? maxGranulePosition = this._reader.MaxGranulePosition;
					long num3 = granulePos;
					if ((maxGranulePosition.GetValueOrDefault() == num3) & (maxGranulePosition != null))
					{
						this._lastPacket = null;
						this._pageIndex = num;
						this._packetIndex = 0;
						return granulePos;
					}
				}
				num2 = this.FindPacket(num, ref granulePos, getPacketGranuleCount);
				num2 -= preRoll;
			}
			if (!this.NormalizePacketIndex(ref num, ref num2))
			{
				throw new ArgumentOutOfRangeException("granulePos");
			}
			if (num < this._reader.FirstDataPageIndex)
			{
				num = this._reader.FirstDataPageIndex;
				num2 = 0;
			}
			this._lastPacket = null;
			this._pageIndex = num;
			this._packetIndex = num2;
			return granulePos;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007114 File Offset: 0x00005314
		private int FindPacket(int pageIndex, ref long granulePos, GetPacketGranuleCount getPacketGranuleCount)
		{
			int num = 0;
			long num2;
			bool flag;
			bool flag2;
			bool flag3;
			int num3;
			int num4;
			if (!this._reader.GetPage(pageIndex - 1, out num2, out flag, out flag2, out flag3, out num3, out num4))
			{
				throw new InvalidDataException("Could not get page?!");
			}
			if (flag3)
			{
				num = 1;
			}
			long num5;
			bool flag4;
			bool flag5;
			int num6;
			if (!this._reader.GetPage(pageIndex, out num5, out flag4, out flag5, out flag3, out num6, out num4))
			{
				throw new InvalidDataException("Could not get found page?!");
			}
			if (flag3)
			{
				num6--;
			}
			int num7 = num6;
			bool flag6 = !flag3;
			long num8 = num5;
			while (num8 > granulePos && --num7 >= num)
			{
				Packet packet = this.CreatePacket(ref pageIndex, ref num7, false, num5, num7 == 0 && flag4, flag3, num6, 0);
				if (packet == null)
				{
					throw new InvalidDataException("Could not find end of continuation!");
				}
				num8 -= (long)getPacketGranuleCount(packet, flag6);
				flag6 = false;
			}
			if (num7 >= num)
			{
				granulePos = num8;
				return num7;
			}
			int num9 = pageIndex;
			int num10 = -1;
			if (!this.NormalizePacketIndex(ref num9, ref num10))
			{
				throw new InvalidDataException("Failed to normalize packet index?");
			}
			Packet packet2 = this.CreatePacket(ref num9, ref num10, false, num8, false, flag5, num10 + 1, 0);
			if (packet2 == null)
			{
				throw new InvalidDataException("Could not load previous packet!");
			}
			granulePos = num8 - (long)getPacketGranuleCount(packet2, false);
			return -1;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007238 File Offset: 0x00005438
		private bool NormalizePacketIndex(ref int pageIndex, ref int packetIndex)
		{
			long num;
			bool flag;
			bool flag2;
			bool flag3;
			int num2;
			int num3;
			if (!this._reader.GetPage(pageIndex, out num, out flag, out flag2, out flag3, out num2, out num3))
			{
				return false;
			}
			int num4 = pageIndex;
			int i;
			bool flag4;
			int num5;
			for (i = packetIndex; i < (flag2 ? 1 : 0); i += num5 - (flag4 ? 1 : 0))
			{
				if (flag2 && flag)
				{
					return false;
				}
				flag4 = flag2;
				bool flag5;
				if (!this._reader.GetPage(--num4, out num, out flag, out flag2, out flag5, out num5, out num3))
				{
					return false;
				}
				if (flag4 && !flag5)
				{
					return false;
				}
			}
			pageIndex = num4;
			packetIndex = i;
			return true;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000072C0 File Offset: 0x000054C0
		private Packet GetNextPacket(ref int pageIndex, ref int packetIndex)
		{
			if (this._reader == null)
			{
				throw new ObjectDisposedException("PacketProvider");
			}
			if (this._lastPacketPacketIndex != packetIndex || this._lastPacketPageIndex != pageIndex || this._lastPacket == null)
			{
				this._lastPacket = null;
				long num;
				bool flag;
				bool flag2;
				bool flag3;
				int num2;
				int num3;
				if (this._reader.GetPage(pageIndex, out num, out flag, out flag2, out flag3, out num2, out num3))
				{
					this._lastPacketPageIndex = pageIndex;
					this._lastPacketPacketIndex = packetIndex;
					this._lastPacket = this.CreatePacket(ref pageIndex, ref packetIndex, true, num, flag, flag3, num2, num3);
					this._nextPacketPageIndex = pageIndex;
					this._nextPacketPacketIndex = packetIndex;
				}
			}
			else
			{
				pageIndex = this._nextPacketPageIndex;
				packetIndex = this._nextPacketPacketIndex;
			}
			return this._lastPacket;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000736C File Offset: 0x0000556C
		private Packet CreatePacket(ref int pageIndex, ref int packetIndex, bool advance, long granulePos, bool isResync, bool isContinued, int packetCount, int pageOverhead)
		{
			Memory<byte> memory = this._reader.GetPagePackets(pageIndex)[packetIndex];
			List<int> list = new List<int>(2);
			list.Add((pageIndex << 8) | packetIndex);
			List<int> list2 = list;
			int num = pageIndex;
			bool flag;
			bool flag3;
			if (isContinued && packetIndex == packetCount - 1)
			{
				flag = true;
				if (packetIndex > 0)
				{
					pageOverhead = 0;
				}
				int num2 = pageIndex;
				while (isContinued)
				{
					bool flag2;
					int num3;
					if (!this._reader.GetPage(++num2, out granulePos, out isResync, out flag2, out isContinued, out packetCount, out num3))
					{
						return null;
					}
					pageOverhead += num3;
					if (!flag2 || isResync)
					{
						break;
					}
					if (isContinued && packetCount > 1)
					{
						isContinued = false;
					}
					list2.Add(num2 << 8);
				}
				flag3 = packetCount == 1;
				num = num2;
			}
			else
			{
				flag = packetIndex == 0;
				flag3 = packetIndex == packetCount - 1;
			}
			Packet packet = new Packet(list2, this, memory)
			{
				IsResync = isResync
			};
			if (flag)
			{
				packet.ContainerOverheadBits = pageOverhead * 8;
			}
			if (flag3)
			{
				packet.GranulePosition = new long?(granulePos);
				if (this._reader.HasAllPages && num == this._reader.PageCount - 1)
				{
					packet.IsEndOfStream = true;
				}
			}
			if (advance)
			{
				if (num != pageIndex)
				{
					pageIndex = num;
					packetIndex = 0;
				}
				if (packetIndex == packetCount - 1)
				{
					pageIndex++;
					packetIndex = 0;
				}
				else
				{
					packetIndex++;
				}
			}
			return packet;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000074A8 File Offset: 0x000056A8
		Memory<byte> IPacketReader.GetPacketData(int pagePacketIndex)
		{
			int num = (pagePacketIndex >> 8) & 16777215;
			int num2 = pagePacketIndex & 255;
			Memory<byte>[] pagePackets = this._reader.GetPagePackets(num);
			if (num2 < pagePackets.Length)
			{
				return pagePackets[num2];
			}
			return Memory<byte>.Empty;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000074E3 File Offset: 0x000056E3
		void IPacketReader.InvalidatePacketCache(IPacket packet)
		{
			if (this._lastPacket == packet)
			{
				this._lastPacket = null;
			}
		}

		// Token: 0x040000A4 RID: 164
		private IStreamPageReader _reader;

		// Token: 0x040000A5 RID: 165
		private int _pageIndex;

		// Token: 0x040000A6 RID: 166
		private int _packetIndex;

		// Token: 0x040000A7 RID: 167
		private int _lastPacketPageIndex;

		// Token: 0x040000A8 RID: 168
		private int _lastPacketPacketIndex;

		// Token: 0x040000A9 RID: 169
		private Packet _lastPacket;

		// Token: 0x040000AA RID: 170
		private int _nextPacketPageIndex;

		// Token: 0x040000AB RID: 171
		private int _nextPacketPacketIndex;

		// Token: 0x040000AC RID: 172
		[CompilerGenerated]
		private readonly int <StreamSerial>k__BackingField;
	}
}
