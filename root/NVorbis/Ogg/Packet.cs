using System;
using System.Collections.Generic;
using NVorbis.Contracts.Ogg;

namespace NVorbis.Ogg
{
	// Token: 0x0200001E RID: 30
	internal class Packet : DataPacket
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00006E02 File Offset: 0x00005002
		internal Packet(IList<int> dataParts, IPacketReader packetReader, Memory<byte> initialData)
		{
			this._dataParts = dataParts;
			this._packetReader = packetReader;
			this._data = initialData;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006E1F File Offset: 0x0000501F
		protected override int TotalBits
		{
			get
			{
				return (this._dataCount + this._data.Length) * 8;
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006E38 File Offset: 0x00005038
		protected unsafe override int ReadNextByte()
		{
			if (this._dataIndex == this._dataParts.Count)
			{
				return -1;
			}
			byte b = *this._data.Span[this._dataOfs];
			int num = this._dataOfs + 1;
			this._dataOfs = num;
			if (num == this._data.Length)
			{
				this._dataOfs = 0;
				this._dataCount += this._data.Length;
				num = this._dataIndex + 1;
				this._dataIndex = num;
				if (num < this._dataParts.Count)
				{
					this._data = this._packetReader.GetPacketData(this._dataParts[this._dataIndex]);
					return (int)b;
				}
				this._data = Memory<byte>.Empty;
			}
			return (int)b;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006F00 File Offset: 0x00005100
		public override void Reset()
		{
			this._dataIndex = 0;
			this._dataOfs = 0;
			if (this._dataParts.Count > 0)
			{
				this._data = this._packetReader.GetPacketData(this._dataParts[0]);
			}
			base.Reset();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006F4C File Offset: 0x0000514C
		public override void Done()
		{
			IPacketReader packetReader = this._packetReader;
			if (packetReader != null)
			{
				packetReader.InvalidatePacketCache(this);
			}
			base.Done();
		}

		// Token: 0x0400009E RID: 158
		private IList<int> _dataParts;

		// Token: 0x0400009F RID: 159
		private IPacketReader _packetReader;

		// Token: 0x040000A0 RID: 160
		private int _dataCount;

		// Token: 0x040000A1 RID: 161
		private Memory<byte> _data;

		// Token: 0x040000A2 RID: 162
		private int _dataIndex;

		// Token: 0x040000A3 RID: 163
		private int _dataOfs;
	}
}
