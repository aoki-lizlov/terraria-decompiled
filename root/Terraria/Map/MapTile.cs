using System;

namespace Terraria.Map
{
	// Token: 0x02000182 RID: 386
	public struct MapTile
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x0050FAF3 File Offset: 0x0050DCF3
		// (set) Token: 0x06001E62 RID: 7778 RVA: 0x0050FB04 File Offset: 0x0050DD04
		public bool IsChanged
		{
			get
			{
				return (this._extraData & 128) > 0;
			}
			set
			{
				if (value)
				{
					this._extraData |= 128;
					return;
				}
				this._extraData &= 127;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x0050FB2D File Offset: 0x0050DD2D
		// (set) Token: 0x06001E64 RID: 7780 RVA: 0x0050FB3B File Offset: 0x0050DD3B
		public bool UpdateQueued
		{
			get
			{
				return (this._extraData & 64) > 0;
			}
			set
			{
				if (value)
				{
					this._extraData |= 64;
					return;
				}
				this._extraData &= 191;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x0050FB64 File Offset: 0x0050DD64
		// (set) Token: 0x06001E66 RID: 7782 RVA: 0x0050FB70 File Offset: 0x0050DD70
		public byte Color
		{
			get
			{
				return this._extraData & 31;
			}
			set
			{
				this._extraData = (byte)(((int)this._extraData & -32) | (int)(value & 31));
			}
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0050FB87 File Offset: 0x0050DD87
		private MapTile(ushort type, byte light, byte extraData)
		{
			this.Type = type;
			this.Light = light;
			this._extraData = extraData;
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x0050FB9E File Offset: 0x0050DD9E
		public bool Equals(MapTile other)
		{
			return this.Light == other.Light && this.Type == other.Type && this.Color == other.Color;
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0050FBCD File Offset: 0x0050DDCD
		public bool EqualsWithoutLight(MapTile other)
		{
			return this.Type == other.Type && this.Color == other.Color;
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0050FBEE File Offset: 0x0050DDEE
		public void Clear()
		{
			this.Type = 0;
			this.Light = 0;
			this._extraData = 0;
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0050FC05 File Offset: 0x0050DE05
		public MapTile WithLight(byte light)
		{
			return new MapTile(this.Type, light, this._extraData | 128);
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0050FC20 File Offset: 0x0050DE20
		public static MapTile Create(ushort type, byte light, byte color)
		{
			return new MapTile(type, light, color | 128);
		}

		// Token: 0x040016D8 RID: 5848
		public ushort Type;

		// Token: 0x040016D9 RID: 5849
		public byte Light;

		// Token: 0x040016DA RID: 5850
		private byte _extraData;
	}
}
