using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.GameContent.Generation.Dungeon.Entrances;
using Terraria.GameContent.Generation.Dungeon.Features;
using Terraria.GameContent.Generation.Dungeon.Halls;
using Terraria.GameContent.Generation.Dungeon.Rooms;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation.Dungeon
{
	// Token: 0x0200049A RID: 1178
	public class DungeonData
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060033C3 RID: 13251 RVA: 0x005FA0FF File Offset: 0x005F82FF
		public DungeonGenVars genVars
		{
			get
			{
				return GenVars.dungeonGenVars[this.Iteration];
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060033C4 RID: 13252 RVA: 0x005FA111 File Offset: 0x005F8311
		public double HallSizeScalar
		{
			get
			{
				return (this.hallStrengthScalar + this.hallStepScalar) / 2.0;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x005FA12A File Offset: 0x005F832A
		public double RoomSizeScalar
		{
			get
			{
				return (this.roomStrengthScalar + this.roomStepScalar) / 2.0;
			}
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x005FA144 File Offset: 0x005F8344
		public bool CanGenerateFeatureInArea(IDungeonFeature feature, int x, int y, int fluff)
		{
			DungeonBounds dungeonBounds = new DungeonBounds();
			dungeonBounds.SetBounds(x - fluff, y - fluff, x + fluff, y + fluff);
			dungeonBounds.CalculateHitbox();
			return this.CanGenerateFeatureInArea(feature, dungeonBounds);
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x005FA17C File Offset: 0x005F837C
		public bool CanGenerateFeatureInArea(IDungeonFeature feature, DungeonBounds bounds)
		{
			return bounds.HasHitbox() && this.CanGenerateFeatureInArea(feature, bounds.Hitbox);
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x005FA198 File Offset: 0x005F8398
		public bool CanGenerateFeatureInArea(IDungeonFeature feature, Rectangle hitbox)
		{
			for (int i = hitbox.Left; i <= hitbox.Right; i++)
			{
				for (int j = hitbox.Top; j <= hitbox.Bottom; j++)
				{
					if (!this.CanGenerateFeatureAt(feature, i, j))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x005FA1E4 File Offset: 0x005F83E4
		public bool CanGenerateFeatureAt(IDungeonFeature feature, int x, int y)
		{
			if (!WorldGen.InWorld(x, y, 5))
			{
				return false;
			}
			if (Main.tile[x, y].wall == 350)
			{
				return false;
			}
			if (this.dungeonEntrance.Bounds.Contains(x, y) && !this.dungeonEntrance.CanGenerateFeatureAt(this, feature, x, y))
			{
				return false;
			}
			for (int i = 0; i < this.protectedDungeonBounds.Count; i++)
			{
				if (this.protectedDungeonBounds[i].Contains(x, y))
				{
					return false;
				}
			}
			for (int j = 0; j < this.dungeonFeatures.Count; j++)
			{
				IDungeonFeature dungeonFeature = this.dungeonFeatures[j];
				if (dungeonFeature is DungeonFeature)
				{
					DungeonFeature dungeonFeature2 = (DungeonFeature)dungeonFeature;
					if (dungeonFeature2.generated && dungeonFeature2.Bounds.Contains(x, y) && !dungeonFeature2.CanGenerateFeatureAt(this, feature, x, y))
					{
						return false;
					}
				}
			}
			for (int k = 0; k < this.dungeonRooms.Count; k++)
			{
				DungeonRoom dungeonRoom = this.dungeonRooms[k];
				if (dungeonRoom.generated && dungeonRoom.OuterBounds.Contains(x, y) && !dungeonRoom.CanGenerateFeatureAt(this, feature, x, y))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x005FA314 File Offset: 0x005F8514
		public bool IsAnyRoomInSpot(out DungeonRoom roomFound, int x, int y, DungeonRoomSearchSettings settings)
		{
			roomFound = null;
			for (int i = 0; i < this.dungeonRooms.Count; i++)
			{
				DungeonRoom dungeonRoom = this.dungeonRooms[i];
				if (DungeonUtils.RoomCanBeChosen(dungeonRoom, settings) && dungeonRoom.InnerBounds.ContainsWithFluff(x, y, settings.Fluff))
				{
					roomFound = dungeonRoom;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x005FA370 File Offset: 0x005F8570
		public DungeonData()
		{
		}

		// Token: 0x0400593F RID: 22847
		public DungeonType Type;

		// Token: 0x04005940 RID: 22848
		public int Iteration;

		// Token: 0x04005941 RID: 22849
		public DungeonEntrance dungeonEntrance;

		// Token: 0x04005942 RID: 22850
		public List<DungeonRoom> dungeonRooms = new List<DungeonRoom>();

		// Token: 0x04005943 RID: 22851
		public List<DungeonHall> dungeonHalls = new List<DungeonHall>();

		// Token: 0x04005944 RID: 22852
		public List<IDungeonFeature> dungeonFeatures = new List<IDungeonFeature>();

		// Token: 0x04005945 RID: 22853
		public List<DungeonDoorData> dungeonDoorData = new List<DungeonDoorData>();

		// Token: 0x04005946 RID: 22854
		public List<DungeonPlatformData> dungeonPlatformData = new List<DungeonPlatformData>();

		// Token: 0x04005947 RID: 22855
		public List<DungeonBounds> protectedDungeonBounds = new List<DungeonBounds>();

		// Token: 0x04005948 RID: 22856
		public bool makeNextPitTrapFlooded;

		// Token: 0x04005949 RID: 22857
		public bool useSkewedDungeonEntranceHalls;

		// Token: 0x0400594A RID: 22858
		public bool createdDungeonEntranceOnSurface;

		// Token: 0x0400594B RID: 22859
		public double dungeonEntranceStrengthX;

		// Token: 0x0400594C RID: 22860
		public double dungeonEntranceStrengthY;

		// Token: 0x0400594D RID: 22861
		public double dungeonEntranceStrengthX2;

		// Token: 0x0400594E RID: 22862
		public double dungeonEntranceStrengthY2;

		// Token: 0x0400594F RID: 22863
		public Vector2D lastDungeonHall = Vector2D.Zero;

		// Token: 0x04005950 RID: 22864
		public DungeonBounds dungeonBounds = new DungeonBounds();

		// Token: 0x04005951 RID: 22865
		public DungeonBounds[] outerProgressionBounds = new DungeonBounds[0];

		// Token: 0x04005952 RID: 22866
		public int[] wallVariants = new int[3];

		// Token: 0x04005953 RID: 22867
		public int chandelierItemType;

		// Token: 0x04005954 RID: 22868
		public int platformItemType;

		// Token: 0x04005955 RID: 22869
		public int doorItemType;

		// Token: 0x04005956 RID: 22870
		public int[] lanternStyles = new int[3];

		// Token: 0x04005957 RID: 22871
		public int[] shelfStyles = new int[3];

		// Token: 0x04005958 RID: 22872
		public int[] bannerStyles = new int[6];

		// Token: 0x04005959 RID: 22873
		public double globalFeatureScalar = 1.0;

		// Token: 0x0400595A RID: 22874
		public double dungeonStepScalar = 1.0;

		// Token: 0x0400595B RID: 22875
		public double hallStrengthScalar = 1.0;

		// Token: 0x0400595C RID: 22876
		public double hallStepScalar = 1.0;

		// Token: 0x0400595D RID: 22877
		public double hallInteriorToExteriorRatio = 0.5;

		// Token: 0x0400595E RID: 22878
		public double hallSlantVariantScalar = 1.0;

		// Token: 0x0400595F RID: 22879
		public double roomStrengthScalar = 1.0;

		// Token: 0x04005960 RID: 22880
		public double roomStepScalar = 1.0;

		// Token: 0x04005961 RID: 22881
		public double roomInteriorToExteriorRatio = 0.5;

		// Token: 0x04005962 RID: 22882
		public double roomSlantVariantScalar = 1.0;
	}
}
