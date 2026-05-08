using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NVorbis.Contracts;
using NVorbis.Contracts.Ogg;

namespace NVorbis.Ogg
{
	// Token: 0x0200001C RID: 28
	internal class ForwardOnlyPacketProvider : DataPacket, IForwardOnlyPacketProvider, IPacketProvider
	{
		// Token: 0x06000127 RID: 295 RVA: 0x0000680D File Offset: 0x00004A0D
		public ForwardOnlyPacketProvider(IPageReader reader, int streamSerial)
		{
			this._reader = reader;
			this.StreamSerial = streamSerial;
			this._packetIndex = int.MaxValue;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006839 File Offset: 0x00004A39
		public bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000683C File Offset: 0x00004A3C
		public int StreamSerial
		{
			[CompilerGenerated]
			get
			{
				return this.<StreamSerial>k__BackingField;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006844 File Offset: 0x00004A44
		public bool AddPage(byte[] buf, bool isResync)
		{
			if ((buf[5] & 2) != 0)
			{
				if (this._isEndOfStream)
				{
					return false;
				}
				isResync = true;
				this._lastSeqNo = BitConverter.ToInt32(buf, 18);
			}
			else
			{
				int num = BitConverter.ToInt32(buf, 18);
				isResync |= num != this._lastSeqNo + 1;
				this._lastSeqNo = num;
			}
			int num2 = 0;
			for (int i = 0; i < (int)buf[26]; i++)
			{
				num2 += (int)buf[27 + i];
			}
			if (num2 == 0)
			{
				return false;
			}
			this._pageQueue.Enqueue(new ValueTuple<byte[], bool>(buf, isResync));
			return true;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000068C9 File Offset: 0x00004AC9
		public void SetEndOfStream()
		{
			this._isEndOfStream = true;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000068D2 File Offset: 0x00004AD2
		public IPacket GetNextPacket()
		{
			if (this._packetBuf.Length > 0)
			{
				if (!this._lastWasPeek)
				{
					throw new InvalidOperationException("Must call Done() on previous packet first.");
				}
				this._lastWasPeek = false;
				return this;
			}
			else
			{
				this._lastWasPeek = false;
				if (this.GetPacket())
				{
					return this;
				}
				return null;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006910 File Offset: 0x00004B10
		public IPacket PeekNextPacket()
		{
			if (this._packetBuf.Length > 0)
			{
				if (!this._lastWasPeek)
				{
					throw new InvalidOperationException("Must call Done() on previous packet first.");
				}
				return this;
			}
			else
			{
				this._lastWasPeek = true;
				if (this.GetPacket())
				{
					return this;
				}
				return null;
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006948 File Offset: 0x00004B48
		private bool GetPacket()
		{
			byte[] pageBuf;
			bool flag;
			int num;
			int packetIndex;
			bool flag2;
			bool flag3;
			if (this._pageBuf != null && this._packetIndex < (int)(27 + this._pageBuf[26]))
			{
				pageBuf = this._pageBuf;
				flag = false;
				num = this._dataStart;
				packetIndex = this._packetIndex;
				flag2 = false;
				flag3 = pageBuf[(int)(26 + pageBuf[26])] == byte.MaxValue;
			}
			else if (!this.ReadNextPage(out pageBuf, out flag, out num, out packetIndex, out flag2, out flag3))
			{
				return false;
			}
			int num2 = num;
			bool flag4 = packetIndex == 27;
			if (flag2 && flag4)
			{
				flag = true;
				num2 += this.GetPacketLength(pageBuf, ref packetIndex);
				if (packetIndex == (int)(27 + pageBuf[26]))
				{
					return this.GetPacket();
				}
			}
			if (!flag4)
			{
				num2 = 0;
			}
			int packetLength = this.GetPacketLength(pageBuf, ref packetIndex);
			Memory<byte> memory = new Memory<byte>(pageBuf, num, packetLength);
			num += packetLength;
			bool flag5 = packetIndex == (int)(27 + pageBuf[26]);
			if (flag3)
			{
				if (flag5)
				{
					flag5 = false;
				}
				else
				{
					int num3 = packetIndex;
					this.GetPacketLength(pageBuf, ref num3);
					flag5 = num3 == (int)(27 + pageBuf[26]);
				}
			}
			bool flag6 = false;
			long? num4 = default(long?);
			if (flag5)
			{
				num4 = new long?(BitConverter.ToInt64(pageBuf, 6));
				if ((pageBuf[5] & 4) != 0 || (this._isEndOfStream && this._pageQueue.Count == 0))
				{
					flag6 = true;
				}
			}
			else
			{
				while (flag3 && packetIndex == (int)(27 + pageBuf[26]) && (this.ReadNextPage(out pageBuf, out flag, out num, out packetIndex, out flag2, out flag3) && !flag && flag2))
				{
					num2 += (int)(27 + pageBuf[26]);
					Memory<byte> memory2 = memory;
					int packetLength2 = this.GetPacketLength(pageBuf, ref packetIndex);
					memory = new Memory<byte>(new byte[memory2.Length + packetLength2]);
					memory2.CopyTo(memory);
					new Memory<byte>(pageBuf, num, packetLength2).CopyTo(memory.Slice(memory2.Length));
					num += packetLength2;
				}
			}
			base.IsResync = flag;
			base.GranulePosition = num4;
			base.IsEndOfStream = flag6;
			base.ContainerOverheadBits = num2 * 8;
			this._pageBuf = pageBuf;
			this._dataStart = num;
			this._packetIndex = packetIndex;
			this._packetBuf = memory;
			this._isEndOfStream = this._isEndOfStream || flag6;
			this.Reset();
			return true;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006B68 File Offset: 0x00004D68
		private bool ReadNextPage(out byte[] pageBuf, out bool isResync, out int dataStart, out int packetIndex, out bool isContinuation, out bool isContinued)
		{
			while (this._pageQueue.Count == 0)
			{
				if (this._isEndOfStream || !this._reader.ReadNextPage())
				{
					pageBuf = null;
					isResync = false;
					dataStart = 0;
					packetIndex = 0;
					isContinuation = false;
					isContinued = false;
					return false;
				}
			}
			ValueTuple<byte[], bool> valueTuple = this._pageQueue.Dequeue();
			pageBuf = valueTuple.Item1;
			isResync = valueTuple.Item2;
			dataStart = (int)(pageBuf[26] + 27);
			packetIndex = 27;
			isContinuation = (pageBuf[5] & 1) > 0;
			isContinued = pageBuf[(int)(26 + pageBuf[26])] == byte.MaxValue;
			return true;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006C00 File Offset: 0x00004E00
		private int GetPacketLength(byte[] pageBuf, ref int packetIndex)
		{
			int num = 0;
			while (pageBuf[packetIndex] == 255 && packetIndex < (int)(pageBuf[26] + 27))
			{
				num += (int)pageBuf[packetIndex];
				packetIndex++;
			}
			if (packetIndex < (int)(pageBuf[26] + 27))
			{
				num += (int)pageBuf[packetIndex];
				packetIndex++;
			}
			return num;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006C4D File Offset: 0x00004E4D
		protected override int TotalBits
		{
			get
			{
				return this._packetBuf.Length * 8;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006C5C File Offset: 0x00004E5C
		protected unsafe override int ReadNextByte()
		{
			if (this._dataIndex < this._packetBuf.Length)
			{
				Span<byte> span = this._packetBuf.Span;
				int dataIndex = this._dataIndex;
				this._dataIndex = dataIndex + 1;
				return (int)(*span[dataIndex]);
			}
			return -1;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006CA3 File Offset: 0x00004EA3
		public override void Reset()
		{
			this._dataIndex = 0;
			base.Reset();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006CB2 File Offset: 0x00004EB2
		public override void Done()
		{
			this._packetBuf = Memory<byte>.Empty;
			base.Done();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006CC5 File Offset: 0x00004EC5
		long IPacketProvider.GetGranuleCount()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006CCC File Offset: 0x00004ECC
		long IPacketProvider.SeekTo(long granulePos, int preRoll, GetPacketGranuleCount getPacketGranuleCount)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000090 RID: 144
		private int _lastSeqNo;

		// Token: 0x04000091 RID: 145
		[TupleElementNames(new string[] { "buf", "isResync" })]
		private readonly Queue<ValueTuple<byte[], bool>> _pageQueue = new Queue<ValueTuple<byte[], bool>>();

		// Token: 0x04000092 RID: 146
		private readonly IPageReader _reader;

		// Token: 0x04000093 RID: 147
		private byte[] _pageBuf;

		// Token: 0x04000094 RID: 148
		private int _packetIndex;

		// Token: 0x04000095 RID: 149
		private bool _isEndOfStream;

		// Token: 0x04000096 RID: 150
		private int _dataStart;

		// Token: 0x04000097 RID: 151
		private bool _lastWasPeek;

		// Token: 0x04000098 RID: 152
		private Memory<byte> _packetBuf;

		// Token: 0x04000099 RID: 153
		private int _dataIndex;

		// Token: 0x0400009A RID: 154
		[CompilerGenerated]
		private readonly int <StreamSerial>k__BackingField;
	}
}
