using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.Desert
{
	// Token: 0x02000517 RID: 1303
	public class DesertDescription
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600366D RID: 13933 RVA: 0x00627249 File Offset: 0x00625449
		// (set) Token: 0x0600366E RID: 13934 RVA: 0x00627251 File Offset: 0x00625451
		public Rectangle CombinedArea
		{
			[CompilerGenerated]
			get
			{
				return this.<CombinedArea>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CombinedArea>k__BackingField = value;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600366F RID: 13935 RVA: 0x0062725A File Offset: 0x0062545A
		// (set) Token: 0x06003670 RID: 13936 RVA: 0x00627262 File Offset: 0x00625462
		public Rectangle Desert
		{
			[CompilerGenerated]
			get
			{
				return this.<Desert>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Desert>k__BackingField = value;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06003671 RID: 13937 RVA: 0x0062726B File Offset: 0x0062546B
		// (set) Token: 0x06003672 RID: 13938 RVA: 0x00627273 File Offset: 0x00625473
		public Rectangle Hive
		{
			[CompilerGenerated]
			get
			{
				return this.<Hive>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Hive>k__BackingField = value;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06003673 RID: 13939 RVA: 0x0062727C File Offset: 0x0062547C
		// (set) Token: 0x06003674 RID: 13940 RVA: 0x00627284 File Offset: 0x00625484
		public Vector2D BlockScale
		{
			[CompilerGenerated]
			get
			{
				return this.<BlockScale>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<BlockScale>k__BackingField = value;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x0062728D File Offset: 0x0062548D
		// (set) Token: 0x06003676 RID: 13942 RVA: 0x00627295 File Offset: 0x00625495
		public int BlockColumnCount
		{
			[CompilerGenerated]
			get
			{
				return this.<BlockColumnCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<BlockColumnCount>k__BackingField = value;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06003677 RID: 13943 RVA: 0x0062729E File Offset: 0x0062549E
		// (set) Token: 0x06003678 RID: 13944 RVA: 0x006272A6 File Offset: 0x006254A6
		public int BlockRowCount
		{
			[CompilerGenerated]
			get
			{
				return this.<BlockRowCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<BlockRowCount>k__BackingField = value;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x006272AF File Offset: 0x006254AF
		// (set) Token: 0x0600367A RID: 13946 RVA: 0x006272B7 File Offset: 0x006254B7
		public bool IsValid
		{
			[CompilerGenerated]
			get
			{
				return this.<IsValid>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsValid>k__BackingField = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600367B RID: 13947 RVA: 0x006272C0 File Offset: 0x006254C0
		// (set) Token: 0x0600367C RID: 13948 RVA: 0x006272C8 File Offset: 0x006254C8
		public SurfaceMap Surface
		{
			[CompilerGenerated]
			get
			{
				return this.<Surface>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Surface>k__BackingField = value;
			}
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x0000357B File Offset: 0x0000177B
		private DesertDescription()
		{
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x006272D4 File Offset: 0x006254D4
		public void UpdateSurfaceMap()
		{
			this.Surface = SurfaceMap.FromArea(this.CombinedArea.Left - 5, this.CombinedArea.Width + 10);
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x0062730C File Offset: 0x0062550C
		public static DesertDescription CreateFromPlacement(Point origin)
		{
			Vector2D defaultBlockScale = DesertDescription.DefaultBlockScale;
			double num = (double)Main.maxTilesX / 4200.0;
			int num2 = (int)(80.0 * num);
			int num3 = (int)((WorldGen.genRand.NextDouble() * 0.5 + 1.5) * 170.0 * num);
			if (WorldGen.remixWorldGen)
			{
				num3 = (int)(340.0 * num);
			}
			int num4 = (int)(defaultBlockScale.X * (double)num2);
			int num5 = (int)(defaultBlockScale.Y * (double)num3);
			origin.X -= num4 / 2;
			SurfaceMap surfaceMap = SurfaceMap.FromArea(origin.X - 5, num4 + 10);
			if (DesertDescription.RowHasInvalidTiles(origin.X, surfaceMap.Bottom, num4))
			{
				return DesertDescription.Invalid;
			}
			int num6 = (int)(surfaceMap.Average + (double)surfaceMap.Bottom) / 2;
			origin.Y = num6 + WorldGen.genRand.Next(40, 60);
			int num7 = 0;
			if (Main.tenthAnniversaryWorld)
			{
				num7 = (int)(20.0 * num);
			}
			return new DesertDescription
			{
				CombinedArea = new Rectangle(origin.X, num6, num4, origin.Y + num5 - num6),
				Hive = new Rectangle(origin.X, origin.Y + num7, num4, num5 - num7),
				Desert = new Rectangle(origin.X, num6, num4, origin.Y + num5 / 2 - num6 + num7),
				BlockScale = defaultBlockScale,
				BlockColumnCount = num2,
				BlockRowCount = num3,
				Surface = surfaceMap,
				IsValid = true
			};
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x006274AC File Offset: 0x006256AC
		private static bool RowHasInvalidTiles(int startX, int startY, int width)
		{
			if (GenVars.skipDesertTileCheck)
			{
				return false;
			}
			for (int i = startX; i < startX + width; i++)
			{
				ushort type = Main.tile[i, startY].type;
				if ((!WorldGen.notTheBees || WorldGen.remixWorldGen) && (type == 59 || type == 60))
				{
					return true;
				}
				if (type == 161 || type == 147)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x0062750F File Offset: 0x0062570F
		// Note: this type is marked as 'beforefieldinit'.
		static DesertDescription()
		{
		}

		// Token: 0x04005B28 RID: 23336
		public static readonly DesertDescription Invalid = new DesertDescription
		{
			IsValid = false
		};

		// Token: 0x04005B29 RID: 23337
		private static readonly Vector2D DefaultBlockScale = new Vector2D(4.0, 2.0);

		// Token: 0x04005B2A RID: 23338
		private const int SCAN_PADDING = 5;

		// Token: 0x04005B2B RID: 23339
		[CompilerGenerated]
		private Rectangle <CombinedArea>k__BackingField;

		// Token: 0x04005B2C RID: 23340
		[CompilerGenerated]
		private Rectangle <Desert>k__BackingField;

		// Token: 0x04005B2D RID: 23341
		[CompilerGenerated]
		private Rectangle <Hive>k__BackingField;

		// Token: 0x04005B2E RID: 23342
		[CompilerGenerated]
		private Vector2D <BlockScale>k__BackingField;

		// Token: 0x04005B2F RID: 23343
		[CompilerGenerated]
		private int <BlockColumnCount>k__BackingField;

		// Token: 0x04005B30 RID: 23344
		[CompilerGenerated]
		private int <BlockRowCount>k__BackingField;

		// Token: 0x04005B31 RID: 23345
		[CompilerGenerated]
		private bool <IsValid>k__BackingField;

		// Token: 0x04005B32 RID: 23346
		[CompilerGenerated]
		private SurfaceMap <Surface>k__BackingField;
	}
}
