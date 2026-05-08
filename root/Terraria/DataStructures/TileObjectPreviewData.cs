using System;

namespace Terraria.DataStructures
{
	// Token: 0x020005A9 RID: 1449
	public class TileObjectPreviewData
	{
		// Token: 0x06003948 RID: 14664 RVA: 0x00651B78 File Offset: 0x0064FD78
		public void Reset()
		{
			this._active = false;
			this._size = Point16.Zero;
			this._coordinates = Point16.Zero;
			this._objectStart = Point16.Zero;
			this._percentValid = 0f;
			this._type = 0;
			this._style = 0;
			this._alternate = -1;
			this._random = -1;
			if (this._data != null)
			{
				Array.Clear(this._data, 0, (int)(this._dataSize.X * this._dataSize.Y));
			}
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x00651C00 File Offset: 0x0064FE00
		public void CopyFrom(TileObjectPreviewData copy)
		{
			this._type = copy._type;
			this._style = copy._style;
			this._alternate = copy._alternate;
			this._random = copy._random;
			this._active = copy._active;
			this._size = copy._size;
			this._coordinates = copy._coordinates;
			this._objectStart = copy._objectStart;
			this._percentValid = copy._percentValid;
			if (this._data == null)
			{
				this._data = new int[(int)copy._dataSize.X, (int)copy._dataSize.Y];
				this._dataSize = copy._dataSize;
			}
			else
			{
				Array.Clear(this._data, 0, this._data.Length);
			}
			if (this._dataSize.X < copy._dataSize.X || this._dataSize.Y < copy._dataSize.Y)
			{
				int num = (int)((copy._dataSize.X > this._dataSize.X) ? copy._dataSize.X : this._dataSize.X);
				int num2 = (int)((copy._dataSize.Y > this._dataSize.Y) ? copy._dataSize.Y : this._dataSize.Y);
				this._data = new int[num, num2];
				this._dataSize = new Point16(num, num2);
			}
			for (int i = 0; i < (int)copy._dataSize.X; i++)
			{
				for (int j = 0; j < (int)copy._dataSize.Y; j++)
				{
					this._data[i, j] = copy._data[i, j];
				}
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600394A RID: 14666 RVA: 0x00651DB9 File Offset: 0x0064FFB9
		// (set) Token: 0x0600394B RID: 14667 RVA: 0x00651DC1 File Offset: 0x0064FFC1
		public bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600394C RID: 14668 RVA: 0x00651DCA File Offset: 0x0064FFCA
		// (set) Token: 0x0600394D RID: 14669 RVA: 0x00651DD2 File Offset: 0x0064FFD2
		public ushort Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600394E RID: 14670 RVA: 0x00651DDB File Offset: 0x0064FFDB
		// (set) Token: 0x0600394F RID: 14671 RVA: 0x00651DE3 File Offset: 0x0064FFE3
		public short Style
		{
			get
			{
				return this._style;
			}
			set
			{
				this._style = value;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06003950 RID: 14672 RVA: 0x00651DEC File Offset: 0x0064FFEC
		// (set) Token: 0x06003951 RID: 14673 RVA: 0x00651DF4 File Offset: 0x0064FFF4
		public int Alternate
		{
			get
			{
				return this._alternate;
			}
			set
			{
				this._alternate = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06003952 RID: 14674 RVA: 0x00651DFD File Offset: 0x0064FFFD
		// (set) Token: 0x06003953 RID: 14675 RVA: 0x00651E05 File Offset: 0x00650005
		public int Random
		{
			get
			{
				return this._random;
			}
			set
			{
				this._random = value;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06003954 RID: 14676 RVA: 0x00651E0E File Offset: 0x0065000E
		// (set) Token: 0x06003955 RID: 14677 RVA: 0x00651E18 File Offset: 0x00650018
		public Point16 Size
		{
			get
			{
				return this._size;
			}
			set
			{
				if (value.X <= 0 || value.Y <= 0)
				{
					throw new FormatException("PlacementData.Size was set to a negative value.");
				}
				if (value.X > this._dataSize.X || value.Y > this._dataSize.Y)
				{
					int num = (int)((value.X > this._dataSize.X) ? value.X : this._dataSize.X);
					int num2 = (int)((value.Y > this._dataSize.Y) ? value.Y : this._dataSize.Y);
					int[,] array = new int[num, num2];
					if (this._data != null)
					{
						for (int i = 0; i < (int)this._dataSize.X; i++)
						{
							for (int j = 0; j < (int)this._dataSize.Y; j++)
							{
								array[i, j] = this._data[i, j];
							}
						}
					}
					this._data = array;
					this._dataSize = new Point16(num, num2);
				}
				this._size = value;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06003956 RID: 14678 RVA: 0x00651F2B File Offset: 0x0065012B
		// (set) Token: 0x06003957 RID: 14679 RVA: 0x00651F33 File Offset: 0x00650133
		public Point16 Coordinates
		{
			get
			{
				return this._coordinates;
			}
			set
			{
				this._coordinates = value;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06003958 RID: 14680 RVA: 0x00651F3C File Offset: 0x0065013C
		// (set) Token: 0x06003959 RID: 14681 RVA: 0x00651F44 File Offset: 0x00650144
		public Point16 ObjectStart
		{
			get
			{
				return this._objectStart;
			}
			set
			{
				this._objectStart = value;
			}
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x00651F50 File Offset: 0x00650150
		public void AllInvalid()
		{
			for (int i = 0; i < (int)this._size.X; i++)
			{
				for (int j = 0; j < (int)this._size.Y; j++)
				{
					if (this._data[i, j] != 0)
					{
						this._data[i, j] = 2;
					}
				}
			}
		}

		// Token: 0x17000499 RID: 1177
		public int this[int x, int y]
		{
			get
			{
				if (x < 0 || y < 0 || x >= (int)this._size.X || y >= (int)this._size.Y)
				{
					throw new IndexOutOfRangeException();
				}
				return this._data[x, y];
			}
			set
			{
				if (x < 0 || y < 0 || x >= (int)this._size.X || y >= (int)this._size.Y)
				{
					throw new IndexOutOfRangeException();
				}
				this._data[x, y] = value;
			}
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x0000357B File Offset: 0x0000177B
		public TileObjectPreviewData()
		{
		}

		// Token: 0x04005D86 RID: 23942
		private ushort _type;

		// Token: 0x04005D87 RID: 23943
		private short _style;

		// Token: 0x04005D88 RID: 23944
		private int _alternate;

		// Token: 0x04005D89 RID: 23945
		private int _random;

		// Token: 0x04005D8A RID: 23946
		private bool _active;

		// Token: 0x04005D8B RID: 23947
		private Point16 _size;

		// Token: 0x04005D8C RID: 23948
		private Point16 _coordinates;

		// Token: 0x04005D8D RID: 23949
		private Point16 _objectStart;

		// Token: 0x04005D8E RID: 23950
		private int[,] _data;

		// Token: 0x04005D8F RID: 23951
		private Point16 _dataSize;

		// Token: 0x04005D90 RID: 23952
		private float _percentValid;

		// Token: 0x04005D91 RID: 23953
		public static TileObjectPreviewData placementCache;

		// Token: 0x04005D92 RID: 23954
		public static TileObjectPreviewData randomCache;

		// Token: 0x04005D93 RID: 23955
		public const int None = 0;

		// Token: 0x04005D94 RID: 23956
		public const int ValidSpot = 1;

		// Token: 0x04005D95 RID: 23957
		public const int InvalidSpot = 2;
	}
}
