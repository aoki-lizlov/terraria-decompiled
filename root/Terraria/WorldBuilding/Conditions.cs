using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000A8 RID: 168
	public static class Conditions
	{
		// Token: 0x020006B2 RID: 1714
		public class IsTile : GenCondition
		{
			// Token: 0x06003EE5 RID: 16101 RVA: 0x00698B8B File Offset: 0x00696D8B
			public IsTile(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x06003EE6 RID: 16102 RVA: 0x00698B9C File Offset: 0x00696D9C
			protected override bool CheckValidity(int x, int y)
			{
				if (!WorldGen.InWorld(x, y, 0))
				{
					return false;
				}
				if (GenBase._tiles[x, y].active())
				{
					for (int i = 0; i < this._types.Length; i++)
					{
						if (GenBase._tiles[x, y].type == this._types[i])
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x04006795 RID: 26517
			private ushort[] _types;
		}

		// Token: 0x020006B3 RID: 1715
		public class Continue : GenCondition
		{
			// Token: 0x06003EE7 RID: 16103 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			protected override bool CheckValidity(int x, int y)
			{
				return false;
			}

			// Token: 0x06003EE8 RID: 16104 RVA: 0x00698BF9 File Offset: 0x00696DF9
			public Continue()
			{
			}
		}

		// Token: 0x020006B4 RID: 1716
		public class BoolCheck : GenCondition
		{
			// Token: 0x06003EE9 RID: 16105 RVA: 0x00698C01 File Offset: 0x00696E01
			public BoolCheck(bool theBool)
			{
				this._theBool = theBool;
			}

			// Token: 0x06003EEA RID: 16106 RVA: 0x00698C10 File Offset: 0x00696E10
			protected override bool CheckValidity(int x, int y)
			{
				return this._theBool;
			}

			// Token: 0x04006796 RID: 26518
			private bool _theBool;
		}

		// Token: 0x020006B5 RID: 1717
		public class MysticSnake : GenCondition
		{
			// Token: 0x06003EEB RID: 16107 RVA: 0x00698C18 File Offset: 0x00696E18
			protected override bool CheckValidity(int x, int y)
			{
				return GenBase._tiles[x, y].active() && !Main.tileCut[(int)GenBase._tiles[x, y].type] && GenBase._tiles[x, y].type != 504;
			}

			// Token: 0x06003EEC RID: 16108 RVA: 0x00698BF9 File Offset: 0x00696DF9
			public MysticSnake()
			{
			}
		}

		// Token: 0x020006B6 RID: 1718
		public class InWorld : GenCondition
		{
			// Token: 0x06003EED RID: 16109 RVA: 0x00698C6E File Offset: 0x00696E6E
			public InWorld(int fluff)
			{
				this._fluff = fluff;
			}

			// Token: 0x06003EEE RID: 16110 RVA: 0x00698C7D File Offset: 0x00696E7D
			protected override bool CheckValidity(int x, int y)
			{
				return WorldGen.InWorld(x, y, this._fluff);
			}

			// Token: 0x04006797 RID: 26519
			private int _fluff;
		}

		// Token: 0x020006B7 RID: 1719
		public class IsSolid : GenCondition
		{
			// Token: 0x06003EEF RID: 16111 RVA: 0x00698C8C File Offset: 0x00696E8C
			protected override bool CheckValidity(int x, int y)
			{
				return WorldGen.InWorld(x, y, 10) && GenBase._tiles[x, y].active() && Main.tileSolid[(int)GenBase._tiles[x, y].type];
			}

			// Token: 0x06003EF0 RID: 16112 RVA: 0x00698BF9 File Offset: 0x00696DF9
			public IsSolid()
			{
			}
		}

		// Token: 0x020006B8 RID: 1720
		public class HasLava : GenCondition
		{
			// Token: 0x06003EF1 RID: 16113 RVA: 0x00698CC7 File Offset: 0x00696EC7
			protected override bool CheckValidity(int x, int y)
			{
				return GenBase._tiles[x, y].liquid > 0 && GenBase._tiles[x, y].liquidType() == 1;
			}

			// Token: 0x06003EF2 RID: 16114 RVA: 0x00698BF9 File Offset: 0x00696DF9
			public HasLava()
			{
			}
		}

		// Token: 0x020006B9 RID: 1721
		public class NotNull : GenCondition
		{
			// Token: 0x06003EF3 RID: 16115 RVA: 0x00698CF3 File Offset: 0x00696EF3
			protected override bool CheckValidity(int x, int y)
			{
				return GenBase._tiles[x, y] != null;
			}

			// Token: 0x06003EF4 RID: 16116 RVA: 0x00698BF9 File Offset: 0x00696DF9
			public NotNull()
			{
			}
		}
	}
}
