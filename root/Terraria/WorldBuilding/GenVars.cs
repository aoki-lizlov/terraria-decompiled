using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using ReLogic.Utilities;
using Terraria.DataStructures;
using Terraria.GameContent.Biomes;
using Terraria.GameContent.Generation.Dungeon;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000B5 RID: 181
	public static class GenVars
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x004DE178 File Offset: 0x004DC378
		// (set) Token: 0x0600176C RID: 5996 RVA: 0x004DE164 File Offset: 0x004DC364
		public static int CurrentDungeon
		{
			get
			{
				return GenVars._currentDungeon;
			}
			set
			{
				GenVars._currentDungeon = (int)MathHelper.Max(0f, (float)value);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x004DE17F File Offset: 0x004DC37F
		public static DungeonGenVars CurrentDungeonGenVars
		{
			get
			{
				return GenVars.dungeonGenVars[GenVars.CurrentDungeon];
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x004DE190 File Offset: 0x004DC390
		// (set) Token: 0x06001770 RID: 6000 RVA: 0x004DE197 File Offset: 0x004DC397
		public static double DualDungeon_NormalizedDistanceSafeFromDither
		{
			get
			{
				return DungeonControlLine.NormalizedDistanceSafeFromDither;
			}
			set
			{
				DungeonControlLine.NormalizedDistanceSafeFromDither = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x004DE19F File Offset: 0x004DC39F
		// (set) Token: 0x06001772 RID: 6002 RVA: 0x004DE1A6 File Offset: 0x004DC3A6
		public static bool hardMode
		{
			get
			{
				return Main.hardMode;
			}
			set
			{
				Main.hardMode = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x004DE1AE File Offset: 0x004DC3AE
		// (set) Token: 0x06001774 RID: 6004 RVA: 0x004DE1B5 File Offset: 0x004DC3B5
		public static int spawnTileX
		{
			get
			{
				return Main.spawnTileX;
			}
			set
			{
				Main.spawnTileX = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x004DE1BD File Offset: 0x004DC3BD
		// (set) Token: 0x06001776 RID: 6006 RVA: 0x004DE1C4 File Offset: 0x004DC3C4
		public static int spawnTileY
		{
			get
			{
				return Main.spawnTileY;
			}
			set
			{
				Main.spawnTileY = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x004DE1CC File Offset: 0x004DC3CC
		// (set) Token: 0x06001778 RID: 6008 RVA: 0x004DE1D3 File Offset: 0x004DC3D3
		public static bool[] townNPCCanSpawn
		{
			get
			{
				return Main.townNPCCanSpawn;
			}
			set
			{
				Main.townNPCCanSpawn = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x004DE1DB File Offset: 0x004DC3DB
		// (set) Token: 0x0600177A RID: 6010 RVA: 0x004DE1E2 File Offset: 0x004DC3E2
		public static bool[] tileSolid
		{
			get
			{
				return Main.tileSolid;
			}
			set
			{
				Main.tileSolid = value;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x004DE1EA File Offset: 0x004DC3EA
		// (set) Token: 0x0600177C RID: 6012 RVA: 0x004DE1F1 File Offset: 0x004DC3F1
		public static double mainWorldSurface
		{
			get
			{
				return Main.worldSurface;
			}
			set
			{
				Main.worldSurface = value;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x004DE1F9 File Offset: 0x004DC3F9
		// (set) Token: 0x0600177E RID: 6014 RVA: 0x004DE200 File Offset: 0x004DC400
		public static double mainRockLayer
		{
			get
			{
				return Main.rockLayer;
			}
			set
			{
				Main.rockLayer = value;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x004DE208 File Offset: 0x004DC408
		// (set) Token: 0x06001780 RID: 6016 RVA: 0x004DE20F File Offset: 0x004DC40F
		public static int mainDungeonX
		{
			get
			{
				return Main.dungeonX;
			}
			set
			{
				Main.dungeonX = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x004DE217 File Offset: 0x004DC417
		// (set) Token: 0x06001782 RID: 6018 RVA: 0x004DE21E File Offset: 0x004DC41E
		public static int mainDungeonY
		{
			get
			{
				return Main.dungeonY;
			}
			set
			{
				Main.dungeonY = value;
			}
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x004DE228 File Offset: 0x004DC428
		// Note: this type is marked as 'beforefieldinit'.
		static GenVars()
		{
		}

		// Token: 0x040011F5 RID: 4597
		[JsonIgnore]
		public static WorldGenConfiguration configuration;

		// Token: 0x040011F6 RID: 4598
		public static StructureMap structures;

		// Token: 0x040011F7 RID: 4599
		public static int copper;

		// Token: 0x040011F8 RID: 4600
		public static int iron;

		// Token: 0x040011F9 RID: 4601
		public static int silver;

		// Token: 0x040011FA RID: 4602
		public static int gold;

		// Token: 0x040011FB RID: 4603
		public static int copperBar = 20;

		// Token: 0x040011FC RID: 4604
		public static int ironBar = 22;

		// Token: 0x040011FD RID: 4605
		public static int silverBar = 21;

		// Token: 0x040011FE RID: 4606
		public static int goldBar = 19;

		// Token: 0x040011FF RID: 4607
		public static bool worldSpawnHasBeenRandomized = false;

		// Token: 0x04001200 RID: 4608
		public static List<LandmassData> landmassData = new List<LandmassData>();

		// Token: 0x04001201 RID: 4609
		public static int remixSurfaceLayerLow;

		// Token: 0x04001202 RID: 4610
		public static int remixSurfaceLayerHigh;

		// Token: 0x04001203 RID: 4611
		public static int remixMushroomLayerLow;

		// Token: 0x04001204 RID: 4612
		public static int remixMushroomLayerHigh;

		// Token: 0x04001205 RID: 4613
		public static int lowestCloud = -1;

		// Token: 0x04001206 RID: 4614
		public static int boulderPetsPlaced = 0;

		// Token: 0x04001207 RID: 4615
		public static ushort crimStoneWall = 83;

		// Token: 0x04001208 RID: 4616
		public static ushort crimStone = 203;

		// Token: 0x04001209 RID: 4617
		public static ushort ebonStoneWall = 3;

		// Token: 0x0400120A RID: 4618
		public static ushort ebonStone = 25;

		// Token: 0x0400120B RID: 4619
		public static ushort mossTile = 179;

		// Token: 0x0400120C RID: 4620
		public static ushort mossWall = 54;

		// Token: 0x0400120D RID: 4621
		public static int lavaLine;

		// Token: 0x0400120E RID: 4622
		public static int waterLine;

		// Token: 0x0400120F RID: 4623
		public static double worldSurfaceLow;

		// Token: 0x04001210 RID: 4624
		public static double worldSurface;

		// Token: 0x04001211 RID: 4625
		public static double worldSurfaceHigh;

		// Token: 0x04001212 RID: 4626
		public static double rockLayerLow;

		// Token: 0x04001213 RID: 4627
		public static double rockLayer;

		// Token: 0x04001214 RID: 4628
		public static double rockLayerHigh;

		// Token: 0x04001215 RID: 4629
		public static int snowTop;

		// Token: 0x04001216 RID: 4630
		public static int snowBottom;

		// Token: 0x04001217 RID: 4631
		public static int snowOriginLeft;

		// Token: 0x04001218 RID: 4632
		public static int snowOriginRight;

		// Token: 0x04001219 RID: 4633
		public static int[] snowMinX;

		// Token: 0x0400121A RID: 4634
		public static int[] snowMaxX;

		// Token: 0x0400121B RID: 4635
		public static int leftBeachEnd;

		// Token: 0x0400121C RID: 4636
		public static int rightBeachStart;

		// Token: 0x0400121D RID: 4637
		public static int beachBordersWidth;

		// Token: 0x0400121E RID: 4638
		public static int beachSandRandomCenter;

		// Token: 0x0400121F RID: 4639
		public static int beachSandRandomWidthRange;

		// Token: 0x04001220 RID: 4640
		public static int beachSandDungeonExtraWidth;

		// Token: 0x04001221 RID: 4641
		public static int beachSandJungleExtraWidth;

		// Token: 0x04001222 RID: 4642
		public static int shellStartXLeft;

		// Token: 0x04001223 RID: 4643
		public static int shellStartYLeft;

		// Token: 0x04001224 RID: 4644
		public static int shellStartXRight;

		// Token: 0x04001225 RID: 4645
		public static int shellStartYRight;

		// Token: 0x04001226 RID: 4646
		public static int oceanWaterStartRandomMin;

		// Token: 0x04001227 RID: 4647
		public static int oceanWaterStartRandomMax;

		// Token: 0x04001228 RID: 4648
		public static int oceanWaterForcedJungleLength;

		// Token: 0x04001229 RID: 4649
		public static int evilBiomeBeachAvoidance;

		// Token: 0x0400122A RID: 4650
		public static int evilBiomeAvoidanceMidFixer;

		// Token: 0x0400122B RID: 4651
		public static int lakesBeachAvoidance;

		// Token: 0x0400122C RID: 4652
		public static int smallHolesBeachAvoidance;

		// Token: 0x0400122D RID: 4653
		public static int surfaceCavesBeachAvoidance;

		// Token: 0x0400122E RID: 4654
		public static int surfaceCavesBeachAvoidance2;

		// Token: 0x0400122F RID: 4655
		public static readonly int maxOceanCaveTreasure = 2;

		// Token: 0x04001230 RID: 4656
		public static int numOceanCaveTreasure = 0;

		// Token: 0x04001231 RID: 4657
		public static Point[] oceanCaveTreasure = new Point[GenVars.maxOceanCaveTreasure];

		// Token: 0x04001232 RID: 4658
		public static bool skipDesertTileCheck = false;

		// Token: 0x04001233 RID: 4659
		public static Rectangle UndergroundDesertLocation = Rectangle.Empty;

		// Token: 0x04001234 RID: 4660
		public static Rectangle UndergroundDesertHiveLocation = Rectangle.Empty;

		// Token: 0x04001235 RID: 4661
		public static int desertHiveHigh;

		// Token: 0x04001236 RID: 4662
		public static int desertHiveLow;

		// Token: 0x04001237 RID: 4663
		public static int desertHiveLeft;

		// Token: 0x04001238 RID: 4664
		public static int desertHiveRight;

		// Token: 0x04001239 RID: 4665
		public static int numLarva;

		// Token: 0x0400123A RID: 4666
		public static int[] larvaY = new int[100];

		// Token: 0x0400123B RID: 4667
		public static int[] larvaX = new int[100];

		// Token: 0x0400123C RID: 4668
		public static int numPyr;

		// Token: 0x0400123D RID: 4669
		public static int[] PyrX;

		// Token: 0x0400123E RID: 4670
		public static int[] PyrY;

		// Token: 0x0400123F RID: 4671
		public static int extraBastStatueCount;

		// Token: 0x04001240 RID: 4672
		public static int extraBastStatueCountMax;

		// Token: 0x04001241 RID: 4673
		public static int jungleOriginX;

		// Token: 0x04001242 RID: 4674
		public static int jungleMinX;

		// Token: 0x04001243 RID: 4675
		public static int jungleMaxX;

		// Token: 0x04001244 RID: 4676
		public static int JungleX;

		// Token: 0x04001245 RID: 4677
		public static ushort jungleHut;

		// Token: 0x04001246 RID: 4678
		public static bool mudWall;

		// Token: 0x04001247 RID: 4679
		public static int JungleItemCount;

		// Token: 0x04001248 RID: 4680
		public static bool gennedLivingMahoganyWands;

		// Token: 0x04001249 RID: 4681
		public static int[] JChestX = new int[100];

		// Token: 0x0400124A RID: 4682
		public static int[] JChestY = new int[100];

		// Token: 0x0400124B RID: 4683
		public static int numJChests;

		// Token: 0x0400124C RID: 4684
		public static int tLeft;

		// Token: 0x0400124D RID: 4685
		public static int tRight;

		// Token: 0x0400124E RID: 4686
		public static int tTop;

		// Token: 0x0400124F RID: 4687
		public static int tBottom;

		// Token: 0x04001250 RID: 4688
		public static int tRooms;

		// Token: 0x04001251 RID: 4689
		public static int lAltarX;

		// Token: 0x04001252 RID: 4690
		public static int lAltarY;

		// Token: 0x04001253 RID: 4691
		public static List<DungeonGenVars> dungeonGenVars = new List<DungeonGenVars>();

		// Token: 0x04001254 RID: 4692
		private static int _currentDungeon;

		// Token: 0x04001255 RID: 4693
		public static readonly int dungeonBeachPadding = 50;

		// Token: 0x04001256 RID: 4694
		public static int skyLakes;

		// Token: 0x04001257 RID: 4695
		public static bool generatedShadowKey;

		// Token: 0x04001258 RID: 4696
		public static bool generatedRamRune;

		// Token: 0x04001259 RID: 4697
		public static int numIslandHouses;

		// Token: 0x0400125A RID: 4698
		public static int skyIslandHouseCount;

		// Token: 0x0400125B RID: 4699
		public static bool[] skyLake = new bool[300];

		// Token: 0x0400125C RID: 4700
		public static int[] floatingIslandHouseX = new int[300];

		// Token: 0x0400125D RID: 4701
		public static int[] floatingIslandHouseY = new int[300];

		// Token: 0x0400125E RID: 4702
		public static int[] floatingIslandStyle = new int[300];

		// Token: 0x0400125F RID: 4703
		public static int numMCaves;

		// Token: 0x04001260 RID: 4704
		public static int[] mCaveX = new int[30];

		// Token: 0x04001261 RID: 4705
		public static int[] mCaveY = new int[30];

		// Token: 0x04001262 RID: 4706
		public static readonly int maxTunnels = 50;

		// Token: 0x04001263 RID: 4707
		public static int numTunnels;

		// Token: 0x04001264 RID: 4708
		public static int[] tunnelX = new int[GenVars.maxTunnels];

		// Token: 0x04001265 RID: 4709
		public static readonly int maxOrePatch = 50;

		// Token: 0x04001266 RID: 4710
		public static int numOrePatch;

		// Token: 0x04001267 RID: 4711
		public static int[] orePatchX = new int[GenVars.maxOrePatch];

		// Token: 0x04001268 RID: 4712
		public static readonly int maxMushroomBiomes = 50;

		// Token: 0x04001269 RID: 4713
		public static int numMushroomBiomes = 0;

		// Token: 0x0400126A RID: 4714
		public static Point[] mushroomBiomesPosition = new Point[GenVars.maxMushroomBiomes];

		// Token: 0x0400126B RID: 4715
		public static int logX;

		// Token: 0x0400126C RID: 4716
		public static int logY;

		// Token: 0x0400126D RID: 4717
		public static readonly int maxLakes = 50;

		// Token: 0x0400126E RID: 4718
		public static int numLakes = 0;

		// Token: 0x0400126F RID: 4719
		public static int[] LakeX = new int[GenVars.maxLakes];

		// Token: 0x04001270 RID: 4720
		public static readonly int maxOasis = 20;

		// Token: 0x04001271 RID: 4721
		public static int numOasis = 0;

		// Token: 0x04001272 RID: 4722
		public static Point[] oasisPosition = new Point[GenVars.maxOasis];

		// Token: 0x04001273 RID: 4723
		public static int[] oasisWidth = new int[GenVars.maxOasis];

		// Token: 0x04001274 RID: 4724
		public static readonly int oasisHeight = 20;

		// Token: 0x04001275 RID: 4725
		public static int hellChest;

		// Token: 0x04001276 RID: 4726
		public static int[] hellChestItem;

		// Token: 0x04001277 RID: 4727
		public static Point16[] statueList;

		// Token: 0x04001278 RID: 4728
		public static List<int> StatuesWithTraps = new List<int>(new int[] { 4, 7, 10, 18 });

		// Token: 0x04001279 RID: 4729
		public static bool crimsonLeft = true;

		// Token: 0x0400127A RID: 4730
		public static Vector2D shimmerPosition;

		// Token: 0x0400127B RID: 4731
		public static bool notTheBeesAndForTheWorthyNoCelebration;

		// Token: 0x0400127C RID: 4732
		public static bool noTrapsAndForTheWorthyNoCelebration;

		// Token: 0x0400127D RID: 4733
		public static bool flipInfections;
	}
}
