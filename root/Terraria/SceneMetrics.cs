using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria
{
	// Token: 0x0200001F RID: 31
	public class SceneMetrics
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000ECEE File Offset: 0x0000CEEE
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x0000ECF6 File Offset: 0x0000CEF6
		public uint LastScanTime
		{
			[CompilerGenerated]
			get
			{
				return this.<LastScanTime>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<LastScanTime>k__BackingField = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000ECFF File Offset: 0x0000CEFF
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000ED07 File Offset: 0x0000CF07
		public Vector2 Center
		{
			[CompilerGenerated]
			get
			{
				return this.<Center>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Center>k__BackingField = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000ED10 File Offset: 0x0000CF10
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000ED18 File Offset: 0x0000CF18
		public Point TileCenter
		{
			[CompilerGenerated]
			get
			{
				return this.<TileCenter>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<TileCenter>k__BackingField = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000ED21 File Offset: 0x0000CF21
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000ED29 File Offset: 0x0000CF29
		public Point BestOrePosition
		{
			[CompilerGenerated]
			get
			{
				return this.<BestOrePosition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<BestOrePosition>k__BackingField = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000ED32 File Offset: 0x0000CF32
		public static int SnowTileThreshold
		{
			get
			{
				if (WorldGen.Skyblock.lowTiles)
				{
					return SceneMetrics.SnowTileSkyblockThreshold;
				}
				return SceneMetrics.SnowTileNormalThreshold;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000ED46 File Offset: 0x0000CF46
		public static int DesertTileThreshold
		{
			get
			{
				if (WorldGen.Skyblock.lowTiles)
				{
					return SceneMetrics.DesertTileSkyblockThreshold;
				}
				return SceneMetrics.DesertTileNormalThreshold;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000ED5A File Offset: 0x0000CF5A
		// (set) Token: 0x060000FE RID: 254 RVA: 0x0000ED62 File Offset: 0x0000CF62
		public int ShimmerTileCount
		{
			[CompilerGenerated]
			get
			{
				return this.<ShimmerTileCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ShimmerTileCount>k__BackingField = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000ED6B File Offset: 0x0000CF6B
		// (set) Token: 0x06000100 RID: 256 RVA: 0x0000ED73 File Offset: 0x0000CF73
		public int EvilTileCount
		{
			[CompilerGenerated]
			get
			{
				return this.<EvilTileCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EvilTileCount>k__BackingField = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000ED7C File Offset: 0x0000CF7C
		// (set) Token: 0x06000102 RID: 258 RVA: 0x0000ED84 File Offset: 0x0000CF84
		public int HolyTileCount
		{
			[CompilerGenerated]
			get
			{
				return this.<HolyTileCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HolyTileCount>k__BackingField = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000ED8D File Offset: 0x0000CF8D
		// (set) Token: 0x06000104 RID: 260 RVA: 0x0000ED95 File Offset: 0x0000CF95
		public int HoneyBlockCount
		{
			[CompilerGenerated]
			get
			{
				return this.<HoneyBlockCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HoneyBlockCount>k__BackingField = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000ED9E File Offset: 0x0000CF9E
		// (set) Token: 0x06000106 RID: 262 RVA: 0x0000EDA6 File Offset: 0x0000CFA6
		public int ActiveMusicBox
		{
			[CompilerGenerated]
			get
			{
				return this.<ActiveMusicBox>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ActiveMusicBox>k__BackingField = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000EDAF File Offset: 0x0000CFAF
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000EDB7 File Offset: 0x0000CFB7
		public bool MusicBoxSilence
		{
			[CompilerGenerated]
			get
			{
				return this.<MusicBoxSilence>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MusicBoxSilence>k__BackingField = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		// (set) Token: 0x0600010A RID: 266 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		public int SandTileCount
		{
			[CompilerGenerated]
			get
			{
				return this.<SandTileCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SandTileCount>k__BackingField = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000EDD1 File Offset: 0x0000CFD1
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000EDD9 File Offset: 0x0000CFD9
		public int MushroomTileCount
		{
			[CompilerGenerated]
			get
			{
				return this.<MushroomTileCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MushroomTileCount>k__BackingField = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000EDE2 File Offset: 0x0000CFE2
		// (set) Token: 0x0600010E RID: 270 RVA: 0x0000EDEA File Offset: 0x0000CFEA
		public int SnowTileCount
		{
			[CompilerGenerated]
			get
			{
				return this.<SnowTileCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SnowTileCount>k__BackingField = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000EDF3 File Offset: 0x0000CFF3
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000EDFB File Offset: 0x0000CFFB
		public int WaterCandleCount
		{
			[CompilerGenerated]
			get
			{
				return this.<WaterCandleCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<WaterCandleCount>k__BackingField = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000EE04 File Offset: 0x0000D004
		// (set) Token: 0x06000112 RID: 274 RVA: 0x0000EE0C File Offset: 0x0000D00C
		public int PeaceCandleCount
		{
			[CompilerGenerated]
			get
			{
				return this.<PeaceCandleCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PeaceCandleCount>k__BackingField = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000EE15 File Offset: 0x0000D015
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000EE1D File Offset: 0x0000D01D
		public int ShadowCandleCount
		{
			[CompilerGenerated]
			get
			{
				return this.<ShadowCandleCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ShadowCandleCount>k__BackingField = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000EE26 File Offset: 0x0000D026
		// (set) Token: 0x06000116 RID: 278 RVA: 0x0000EE2E File Offset: 0x0000D02E
		public int PartyMonolithCount
		{
			[CompilerGenerated]
			get
			{
				return this.<PartyMonolithCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PartyMonolithCount>k__BackingField = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000EE37 File Offset: 0x0000D037
		// (set) Token: 0x06000118 RID: 280 RVA: 0x0000EE3F File Offset: 0x0000D03F
		public int MeteorTileCount
		{
			[CompilerGenerated]
			get
			{
				return this.<MeteorTileCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MeteorTileCount>k__BackingField = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000EE48 File Offset: 0x0000D048
		// (set) Token: 0x0600011A RID: 282 RVA: 0x0000EE50 File Offset: 0x0000D050
		public int BloodTileCount
		{
			[CompilerGenerated]
			get
			{
				return this.<BloodTileCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<BloodTileCount>k__BackingField = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000EE59 File Offset: 0x0000D059
		// (set) Token: 0x0600011C RID: 284 RVA: 0x0000EE61 File Offset: 0x0000D061
		public int JungleTileCount
		{
			[CompilerGenerated]
			get
			{
				return this.<JungleTileCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<JungleTileCount>k__BackingField = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000EE6A File Offset: 0x0000D06A
		// (set) Token: 0x0600011E RID: 286 RVA: 0x0000EE72 File Offset: 0x0000D072
		public int DungeonTileCount
		{
			[CompilerGenerated]
			get
			{
				return this.<DungeonTileCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DungeonTileCount>k__BackingField = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000EE7B File Offset: 0x0000D07B
		// (set) Token: 0x06000120 RID: 288 RVA: 0x0000EE83 File Offset: 0x0000D083
		public bool HasSunflower
		{
			[CompilerGenerated]
			get
			{
				return this.<HasSunflower>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasSunflower>k__BackingField = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000EE8C File Offset: 0x0000D08C
		// (set) Token: 0x06000122 RID: 290 RVA: 0x0000EE94 File Offset: 0x0000D094
		public bool HasGardenGnome
		{
			[CompilerGenerated]
			get
			{
				return this.<HasGardenGnome>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasGardenGnome>k__BackingField = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000EE9D File Offset: 0x0000D09D
		// (set) Token: 0x06000124 RID: 292 RVA: 0x0000EEA5 File Offset: 0x0000D0A5
		public bool HasClock
		{
			[CompilerGenerated]
			get
			{
				return this.<HasClock>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasClock>k__BackingField = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000EEAE File Offset: 0x0000D0AE
		// (set) Token: 0x06000126 RID: 294 RVA: 0x0000EEB6 File Offset: 0x0000D0B6
		public bool HasCampfire
		{
			[CompilerGenerated]
			get
			{
				return this.<HasCampfire>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasCampfire>k__BackingField = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000EEBF File Offset: 0x0000D0BF
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000EEC7 File Offset: 0x0000D0C7
		public bool HasStarInBottle
		{
			[CompilerGenerated]
			get
			{
				return this.<HasStarInBottle>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasStarInBottle>k__BackingField = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		// (set) Token: 0x0600012A RID: 298 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		public bool HasHeartLantern
		{
			[CompilerGenerated]
			get
			{
				return this.<HasHeartLantern>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasHeartLantern>k__BackingField = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000EEE1 File Offset: 0x0000D0E1
		// (set) Token: 0x0600012C RID: 300 RVA: 0x0000EEE9 File Offset: 0x0000D0E9
		public int ActiveFountainColor
		{
			[CompilerGenerated]
			get
			{
				return this.<ActiveFountainColor>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ActiveFountainColor>k__BackingField = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000EEF2 File Offset: 0x0000D0F2
		// (set) Token: 0x0600012E RID: 302 RVA: 0x0000EEFA File Offset: 0x0000D0FA
		public int ActiveMonolithType
		{
			[CompilerGenerated]
			get
			{
				return this.<ActiveMonolithType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ActiveMonolithType>k__BackingField = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000EF03 File Offset: 0x0000D103
		// (set) Token: 0x06000130 RID: 304 RVA: 0x0000EF0B File Offset: 0x0000D10B
		public bool BloodMoonMonolith
		{
			[CompilerGenerated]
			get
			{
				return this.<BloodMoonMonolith>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<BloodMoonMonolith>k__BackingField = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000EF14 File Offset: 0x0000D114
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000EF1C File Offset: 0x0000D11C
		public bool MoonLordMonolith
		{
			[CompilerGenerated]
			get
			{
				return this.<MoonLordMonolith>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MoonLordMonolith>k__BackingField = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000EF25 File Offset: 0x0000D125
		// (set) Token: 0x06000134 RID: 308 RVA: 0x0000EF2D File Offset: 0x0000D12D
		public bool EchoMonolith
		{
			[CompilerGenerated]
			get
			{
				return this.<EchoMonolith>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<EchoMonolith>k__BackingField = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000EF36 File Offset: 0x0000D136
		// (set) Token: 0x06000136 RID: 310 RVA: 0x0000EF3E File Offset: 0x0000D13E
		public int ShimmerMonolithState
		{
			[CompilerGenerated]
			get
			{
				return this.<ShimmerMonolithState>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ShimmerMonolithState>k__BackingField = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000EF47 File Offset: 0x0000D147
		// (set) Token: 0x06000138 RID: 312 RVA: 0x0000EF4F File Offset: 0x0000D14F
		public bool CRTMonolith
		{
			[CompilerGenerated]
			get
			{
				return this.<CRTMonolith>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CRTMonolith>k__BackingField = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000EF58 File Offset: 0x0000D158
		// (set) Token: 0x0600013A RID: 314 RVA: 0x0000EF60 File Offset: 0x0000D160
		public bool RetroMonolith
		{
			[CompilerGenerated]
			get
			{
				return this.<RetroMonolith>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RetroMonolith>k__BackingField = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000EF69 File Offset: 0x0000D169
		// (set) Token: 0x0600013C RID: 316 RVA: 0x0000EF71 File Offset: 0x0000D171
		public bool NoirMonolith
		{
			[CompilerGenerated]
			get
			{
				return this.<NoirMonolith>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<NoirMonolith>k__BackingField = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000EF7A File Offset: 0x0000D17A
		// (set) Token: 0x0600013E RID: 318 RVA: 0x0000EF82 File Offset: 0x0000D182
		public bool RadioThingMonolith
		{
			[CompilerGenerated]
			get
			{
				return this.<RadioThingMonolith>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RadioThingMonolith>k__BackingField = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000EF8B File Offset: 0x0000D18B
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000EF93 File Offset: 0x0000D193
		public bool HasCatBast
		{
			[CompilerGenerated]
			get
			{
				return this.<HasCatBast>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasCatBast>k__BackingField = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000EF9C File Offset: 0x0000D19C
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		public int GraveyardTileCount
		{
			[CompilerGenerated]
			get
			{
				return this.<GraveyardTileCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GraveyardTileCount>k__BackingField = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000EFAD File Offset: 0x0000D1AD
		// (set) Token: 0x06000144 RID: 324 RVA: 0x0000EFB5 File Offset: 0x0000D1B5
		public int DesertSandTileCount
		{
			[CompilerGenerated]
			get
			{
				return this.<DesertSandTileCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DesertSandTileCount>k__BackingField = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000EFBE File Offset: 0x0000D1BE
		// (set) Token: 0x06000146 RID: 326 RVA: 0x0000EFC6 File Offset: 0x0000D1C6
		public int OceanSandTileCount
		{
			[CompilerGenerated]
			get
			{
				return this.<OceanSandTileCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<OceanSandTileCount>k__BackingField = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000EFCF File Offset: 0x0000D1CF
		public bool EnoughTilesForShimmer
		{
			get
			{
				return this.ShimmerTileCount >= SceneMetrics.ShimmerTileThreshold;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000EFE1 File Offset: 0x0000D1E1
		public bool EnoughTilesForJungle
		{
			get
			{
				return this.JungleTileCount >= SceneMetrics.JungleTileThreshold;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000EFF3 File Offset: 0x0000D1F3
		public bool EnoughTilesForHallow
		{
			get
			{
				return this.HolyTileCount >= SceneMetrics.HallowTileThreshold;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000F005 File Offset: 0x0000D205
		public bool EnoughTilesForSnow
		{
			get
			{
				return this.SnowTileCount >= SceneMetrics.SnowTileThreshold;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000F017 File Offset: 0x0000D217
		public bool EnoughTilesForGlowingMushroom
		{
			get
			{
				return this.MushroomTileCount >= SceneMetrics.MushroomTileThreshold;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000F029 File Offset: 0x0000D229
		public bool EnoughTilesForDesert
		{
			get
			{
				return this.DesertSandTileCount >= SceneMetrics.DesertTileThreshold;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000F03B File Offset: 0x0000D23B
		public bool EnoughTilesForCorruption
		{
			get
			{
				return this.EvilTileCount >= SceneMetrics.CorruptionTileThreshold;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000F04D File Offset: 0x0000D24D
		public bool EnoughTilesForCrimson
		{
			get
			{
				return this.BloodTileCount >= SceneMetrics.CrimsonTileThreshold;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000F05F File Offset: 0x0000D25F
		public bool EnoughTilesForMeteor
		{
			get
			{
				return this.MeteorTileCount >= SceneMetrics.MeteorTileThreshold;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000F071 File Offset: 0x0000D271
		public bool EnoughTilesForDungeon
		{
			get
			{
				return this.DungeonTileCount >= SceneMetrics.DungeonTileThreshold;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000F083 File Offset: 0x0000D283
		public bool EnoughTilesForGraveyard
		{
			get
			{
				return this.GraveyardTileCount >= SceneMetrics.GraveyardTileThreshold;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000F095 File Offset: 0x0000D295
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000F09D File Offset: 0x0000D29D
		public bool BehindBackwall
		{
			[CompilerGenerated]
			get
			{
				return this.<BehindBackwall>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<BehindBackwall>k__BackingField = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000F0A6 File Offset: 0x0000D2A6
		public bool CloseEnoughToSolarTower
		{
			get
			{
				return this.WithinRangeOfNPC(517, (double)SceneMetrics.NPCEventZoneRadius);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000F0B9 File Offset: 0x0000D2B9
		public bool CloseEnoughToVortexTower
		{
			get
			{
				return this.WithinRangeOfNPC(422, (double)SceneMetrics.NPCEventZoneRadius);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		public bool CloseEnoughToNebulaTower
		{
			get
			{
				return this.WithinRangeOfNPC(507, (double)SceneMetrics.NPCEventZoneRadius);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000F0DF File Offset: 0x0000D2DF
		public bool CloseEnoughToStardustTower
		{
			get
			{
				return this.WithinRangeOfNPC(493, (double)SceneMetrics.NPCEventZoneRadius);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000F0F2 File Offset: 0x0000D2F2
		public bool CloseEnoughToDD2LanePortal
		{
			get
			{
				return this.WithinRangeOfNPC(549, (double)SceneMetrics.NPCEventZoneRadius);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000F108 File Offset: 0x0000D308
		public float? DistanceToMoonLord
		{
			get
			{
				Vector2 vector = this.ClosestNPCPosition[398];
				if (vector == Vector2.Zero)
				{
					return null;
				}
				return new float?(Vector2.Distance(this.Center, vector));
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000F150 File Offset: 0x0000D350
		public float? MoonLordSkyIntensity
		{
			get
			{
				float? distanceToMoonLord = Main.SceneMetrics.DistanceToMoonLord;
				if (distanceToMoonLord != null)
				{
					float value = distanceToMoonLord.Value;
					return new float?(1f - Utils.SmoothStep(3000f, 6000f, value));
				}
				return null;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000F19E File Offset: 0x0000D39E
		public bool AnyNPCs(int type)
		{
			return this.ClosestNPCPosition[type] != Vector2.Zero;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000F1B6 File Offset: 0x0000D3B6
		// (set) Token: 0x0600015D RID: 349 RVA: 0x0000F1BE File Offset: 0x0000D3BE
		public int TownNPCCount
		{
			[CompilerGenerated]
			get
			{
				return this.<TownNPCCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<TownNPCCount>k__BackingField = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000F1C7 File Offset: 0x0000D3C7
		// (set) Token: 0x0600015F RID: 351 RVA: 0x0000F1CF File Offset: 0x0000D3CF
		public Player PerspectivePlayer
		{
			[CompilerGenerated]
			get
			{
				return this.<PerspectivePlayer>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PerspectivePlayer>k__BackingField = value;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000F1D8 File Offset: 0x0000D3D8
		public SceneMetrics()
		{
			this.Reset();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000F234 File Offset: 0x0000D434
		public void Scan(SceneMetricsScanSettings settings)
		{
			if (this.LastScanTime == Main.GameUpdateCount && this.Center == settings.BiomeScanCenterPositionInWorld)
			{
				return;
			}
			this.Reset();
			this.LastScanTime = Main.GameUpdateCount;
			this.Center = settings.BiomeScanCenterPositionInWorld;
			this.TileCenter = this.Center.ToTileCoordinates().ClampedInWorld(0);
			this.ScanTiles();
			if (settings.VisualScanArea != null)
			{
				this.ScanOnScreenTiles(settings.VisualScanArea.Value);
			}
			if (settings.ScanNPCPositions)
			{
				this.ScanNPCPositions();
			}
			this.AggregateTileCounts();
			this.CalculateZones();
			if (settings.PerspectivePlayer != null)
			{
				this.AddPlayerEffects(settings.PerspectivePlayer);
			}
			this.CanPlayCreditsRoll = this.ActiveMusicBox == 85;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000F2FC File Offset: 0x0000D4FC
		private void ScanTiles()
		{
			Rectangle rectangle = Utils.CenteredRectangle(this.TileCenter, SceneMetrics.ZoneScanSize);
			rectangle = WorldUtils.ClampToWorld(rectangle, 0);
			for (int i = rectangle.Left; i < rectangle.Right; i++)
			{
				for (int j = rectangle.Top; j < rectangle.Bottom; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null)
					{
						if (!tile.active())
						{
							if (tile.liquid > 0)
							{
								this._liquidCounts[(int)tile.liquidType()]++;
							}
						}
						else
						{
							this._tileCounts[(int)tile.type]++;
							if (TileID.Sets.isDesertBiomeSand[(int)tile.type] && WorldGen.oceanDepths(i, j))
							{
								int num = this.OceanSandTileCount;
								this.OceanSandTileCount = num + 1;
							}
							if (TileID.Sets.Campfires[(int)tile.type] && tile.frameY < 36)
							{
								this.HasCampfire = true;
							}
							if (tile.type == 49 && tile.frameX < 18)
							{
								int num = this.WaterCandleCount;
								this.WaterCandleCount = num + 1;
							}
							if (tile.type == 372 && tile.frameX < 18)
							{
								int num = this.PeaceCandleCount;
								this.PeaceCandleCount = num + 1;
							}
							if (tile.type == 646 && tile.frameX < 18)
							{
								int num = this.ShadowCandleCount;
								this.ShadowCandleCount = num + 1;
							}
							if (tile.type == 405 && tile.frameX < 54)
							{
								this.HasCampfire = true;
							}
							if (tile.type == 506 && tile.frameX < 72)
							{
								this.HasCatBast = true;
							}
							if (tile.type == 42 && tile.frameY >= 324 && tile.frameY <= 358)
							{
								this.HasHeartLantern = true;
							}
							if (tile.type == 42 && tile.frameY >= 252 && tile.frameY <= 286)
							{
								this.HasStarInBottle = true;
							}
							if (tile.type == 91)
							{
								int num2 = (int)(tile.frameX / 18);
								for (short num3 = tile.frameY; num3 >= 54; num3 -= 54)
								{
									num2 += 111;
								}
								bool flag = false;
								if ((tile.frameX < 396 && tile.frameY < 54) || num2 == 311 || num2 == 312)
								{
									flag = true;
								}
								if (!flag)
								{
									int num4 = (int)(tile.frameX / 18 - 21);
									for (int k = (int)tile.frameY; k >= 54; k -= 54)
									{
										num4 += 90;
										num4 += 21;
									}
									if (num2 >= 311)
									{
										num4--;
									}
									if (num2 >= 312)
									{
										num4--;
									}
									int num5 = BannerSystem.BannerToItem(num4);
									if (ItemID.Sets.BannerStrength.IndexInRange(num5) && ItemID.Sets.BannerStrength[num5].Enabled)
									{
										this.NPCBannerBuff[num4] = true;
										this.hasBanner = true;
									}
								}
							}
							this.UpdateOreFinder(new Point(i, j), tile);
						}
					}
				}
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000F610 File Offset: 0x0000D810
		private void ScanOnScreenTiles(Rectangle visualScanArea)
		{
			visualScanArea = WorldUtils.ClampToWorld(visualScanArea, 0);
			for (int i = visualScanArea.Left; i < visualScanArea.Right; i++)
			{
				for (int j = visualScanArea.Top; j < visualScanArea.Bottom; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && tile.active())
					{
						if (tile.type == 104)
						{
							this.HasClock = true;
						}
						ushort type = tile.type;
						if (type <= 509)
						{
							if (type <= 207)
							{
								if (type != 139)
								{
									if (type == 207)
									{
										if (tile.frameY >= 72)
										{
											switch (tile.frameX / 36)
											{
											case 0:
												this.ActiveFountainColor = 0;
												break;
											case 1:
												this.ActiveFountainColor = 12;
												break;
											case 2:
												this.ActiveFountainColor = 3;
												break;
											case 3:
												this.ActiveFountainColor = 5;
												break;
											case 4:
												this.ActiveFountainColor = 2;
												break;
											case 5:
												this.ActiveFountainColor = 10;
												break;
											case 6:
												this.ActiveFountainColor = 4;
												break;
											case 7:
												this.ActiveFountainColor = 9;
												break;
											case 8:
												this.ActiveFountainColor = 8;
												break;
											case 9:
												this.ActiveFountainColor = 6;
												break;
											default:
												this.ActiveFountainColor = -1;
												break;
											}
										}
									}
								}
								else if (tile.frameX >= 36)
								{
									int num = (int)(tile.frameY / 36);
									if (num == 100)
									{
										this.MusicBoxSilence = true;
									}
									else
									{
										this.ActiveMusicBox = num;
									}
								}
							}
							else if (type != 410)
							{
								if (type != 480)
								{
									if (type == 509)
									{
										if (tile.frameY >= 56)
										{
											this.ActiveMonolithType = 4;
										}
									}
								}
								else if (tile.frameY >= 54)
								{
									this.BloodMoonMonolith = true;
								}
							}
							else if (tile.frameY >= 56)
							{
								int num2 = (int)(tile.frameX / 36);
								this.ActiveMonolithType = num2;
							}
						}
						else if (type <= 720)
						{
							if (type != 657)
							{
								if (type != 658)
								{
									if (type == 720)
									{
										if (tile.frameY >= 54)
										{
											this.CRTMonolith = true;
										}
									}
								}
								else
								{
									int num3 = (int)(tile.frameY / 54);
									this.ShimmerMonolithState = num3;
								}
							}
							else if (tile.frameY >= 54)
							{
								this.EchoMonolith = true;
							}
						}
						else if (type != 721)
						{
							if (type != 725)
							{
								if (type == 733)
								{
									if (tile.frameY >= 54)
									{
										this.RadioThingMonolith = true;
									}
								}
							}
							else if (tile.frameY >= 54)
							{
								this.NoirMonolith = true;
							}
						}
						else if (tile.frameY >= 54)
						{
							this.RetroMonolith = true;
						}
					}
				}
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000F908 File Offset: 0x0000DB08
		private void AggregateTileCounts()
		{
			int num = -10;
			if (Main.infectedSeed)
			{
				num *= 3;
			}
			if (this._tileCounts[27] > 0)
			{
				this.HasSunflower = true;
			}
			if (this._tileCounts[567] > 0)
			{
				this.HasGardenGnome = true;
			}
			this.ShimmerTileCount = this._liquidCounts[3];
			this.HoneyBlockCount = this._tileCounts[229];
			this.HolyTileCount = this._tileCounts[109] + this._tileCounts[492] + this._tileCounts[110] + this._tileCounts[113] + this._tileCounts[117] + this._tileCounts[116] + this._tileCounts[164] + this._tileCounts[403] + this._tileCounts[402];
			this.SnowTileCount = this._tileCounts[147] + this._tileCounts[148] + this._tileCounts[161] + this._tileCounts[162] + this._tileCounts[164] + this._tileCounts[163] + this._tileCounts[200];
			if (Main.remixWorld)
			{
				this.JungleTileCount = this._tileCounts[60] + this._tileCounts[61] + this._tileCounts[62] + this._tileCounts[74] + this._tileCounts[225];
				this.EvilTileCount = this._tileCounts[23] + this._tileCounts[661] + this._tileCounts[24] + this._tileCounts[25] + this._tileCounts[32] + this._tileCounts[112] + this._tileCounts[163] + this._tileCounts[400] + this._tileCounts[398] + this._tileCounts[27] * num + this._tileCounts[474];
				this.BloodTileCount = this._tileCounts[199] + this._tileCounts[662] + this._tileCounts[201] + this._tileCounts[203] + this._tileCounts[200] + this._tileCounts[401] + this._tileCounts[399] + this._tileCounts[234] + this._tileCounts[352] + this._tileCounts[27] * num + this._tileCounts[195];
			}
			else
			{
				this.JungleTileCount = this._tileCounts[60] + this._tileCounts[61] + this._tileCounts[62] + this._tileCounts[74] + this._tileCounts[226] + this._tileCounts[225];
				this.EvilTileCount = this._tileCounts[23] + this._tileCounts[661] + this._tileCounts[24] + this._tileCounts[25] + this._tileCounts[32] + this._tileCounts[112] + this._tileCounts[163] + this._tileCounts[400] + this._tileCounts[398] + this._tileCounts[27] * num;
				this.BloodTileCount = this._tileCounts[199] + this._tileCounts[662] + this._tileCounts[201] + this._tileCounts[203] + this._tileCounts[200] + this._tileCounts[401] + this._tileCounts[399] + this._tileCounts[234] + this._tileCounts[352] + this._tileCounts[27] * num;
			}
			this.MushroomTileCount = this._tileCounts[70] + this._tileCounts[71] + this._tileCounts[72] + this._tileCounts[528];
			this.MeteorTileCount = this._tileCounts[37];
			this.DungeonTileCount = this._tileCounts[41] + this._tileCounts[43] + this._tileCounts[44] + this._tileCounts[481] + this._tileCounts[482] + this._tileCounts[483];
			this.SandTileCount = this._tileCounts[53] + this._tileCounts[112] + this._tileCounts[116] + this._tileCounts[234] + this._tileCounts[397] + this._tileCounts[398] + this._tileCounts[402] + this._tileCounts[399] + this._tileCounts[396] + this._tileCounts[400] + this._tileCounts[403] + this._tileCounts[401];
			this.PartyMonolithCount = this._tileCounts[455];
			this.GraveyardTileCount = this._tileCounts[85];
			this.GraveyardTileCount -= this._tileCounts[27] / 2;
			if (this._tileCounts[27] > 0)
			{
				this.HasSunflower = true;
			}
			if (this.GraveyardTileCount > SceneMetrics.GraveyardTileMin)
			{
				this.HasSunflower = false;
			}
			if (this.GraveyardTileCount < 0)
			{
				this.GraveyardTileCount = 0;
			}
			if (this.HolyTileCount < 0)
			{
				this.HolyTileCount = 0;
			}
			if (this.EvilTileCount < 0)
			{
				this.EvilTileCount = 0;
			}
			if (this.BloodTileCount < 0)
			{
				this.BloodTileCount = 0;
			}
			int holyTileCount = this.HolyTileCount;
			this.HolyTileCount -= this.EvilTileCount;
			this.HolyTileCount -= this.BloodTileCount;
			this.EvilTileCount -= holyTileCount;
			this.BloodTileCount -= holyTileCount;
			if (this.HolyTileCount < 0)
			{
				this.HolyTileCount = 0;
			}
			if (this.EvilTileCount < 0)
			{
				this.EvilTileCount = 0;
			}
			if (this.BloodTileCount < 0)
			{
				this.BloodTileCount = 0;
			}
			this.DesertSandTileCount = Math.Max(0, this.SandTileCount - this.OceanSandTileCount);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000FF30 File Offset: 0x0000E130
		private void CalculateZones()
		{
			Tile tileSafely = Framing.GetTileSafely(this.TileCenter);
			this.BehindBackwall = tileSafely.wall > 0;
			this.ZoneSkyHeight = (double)this.TileCenter.Y <= Main.worldSurface * 0.3499999940395355;
			this.ZoneOverworldHeight = (double)this.TileCenter.Y <= Main.worldSurface && (double)this.TileCenter.Y > Main.worldSurface * 0.3499999940395355;
			this.BelowSurface = (double)this.TileCenter.Y > Main.worldSurface;
			this.ZoneDirtLayerHeight = (double)this.TileCenter.Y <= Main.rockLayer && (double)this.TileCenter.Y > Main.worldSurface;
			this.ZoneRockLayerHeight = this.TileCenter.Y <= Main.UnderworldLayer && (double)this.TileCenter.Y > Main.rockLayer;
			this.ZoneUnderworldHeight = this.TileCenter.Y > Main.UnderworldLayer;
			this.ZoneCorrupt = this.EnoughTilesForCorruption;
			this.ZoneCrimson = this.EnoughTilesForCrimson;
			this.ZoneHallow = this.EnoughTilesForHallow;
			this.ZoneJungle = this.EnoughTilesForJungle && !this.ZoneUnderworldHeight;
			this.ZoneSnow = this.EnoughTilesForSnow;
			this.ZoneDesert = this.EnoughTilesForDesert;
			this.ZoneGlowshroom = this.EnoughTilesForGlowingMushroom;
			this.ZoneMeteor = this.EnoughTilesForMeteor;
			this.ZoneGraveyard = this.EnoughTilesForGraveyard;
			this.ZoneDungeon = this.EnoughTilesForDungeon && this.BelowSurface && Main.wallDungeon[(int)tileSafely.wall];
			this.ZoneLihzhardTemple = tileSafely.wall == 87;
			this.ZoneGranite = tileSafely.wall == 184 || tileSafely.wall == 180;
			this.ZoneMarble = tileSafely.wall == 183 || tileSafely.wall == 178;
			this.ZoneHive = tileSafely.wall == 108 || tileSafely.wall == 86;
			this.ZoneGemCave = tileSafely.wall >= 48 && tileSafely.wall <= 53;
			this.ZoneBeach = WorldGen.oceanDepths(this.TileCenter.X, this.TileCenter.Y);
			this.ZoneUndergroundDesert = this.ZoneDesert && this.BelowSurface && (WallID.Sets.Conversion.Sandstone[(int)tileSafely.wall] || WallID.Sets.Conversion.HardenedSand[(int)tileSafely.wall] || tileSafely.wall == 223) && !Main.wallHouse[(int)tileSafely.wall];
			this.SurfaceAtmospherics = WorldGen.IsSurfaceForAtmospherics(this.TileCenter);
			if (Main.remixWorld && this.ZoneDungeon)
			{
				this.SurfaceAtmospherics = false;
			}
			this.ZoneRain = Main.raining && this.SurfaceAtmospherics;
			this.ZoneSandstorm = this.ZoneDesert && this.SurfaceAtmospherics && Sandstorm.Happening;
			if (this.ZoneSandstorm)
			{
				this.ZoneRain = false;
			}
			this.UndergroundForShimmering = (double)this.TileCenter.Y > Main.worldSurface + 84.0 && this.TileCenter.Y < Main.maxTilesY - 396;
			this.ZoneShimmer = this.EnoughTilesForShimmer && this.UndergroundForShimmering && !this.ZoneDungeon;
			this.ZoneWaterCandle = this.WaterCandleCount > 0;
			this.ZonePeaceCandle = this.PeaceCandleCount > 0;
			this.ZoneShadowCandle = this.ShadowCandleCount > 0;
			if (Main.dualDungeonsSeed && this.BelowSurface && !this.ZoneUnderworldHeight)
			{
				NPCSpawningFlagsForDualDungeons npcspawningFlagsForDualDungeons = default(NPCSpawningFlagsForDualDungeons);
				Point point = new Point(this.TileCenter.X, this.TileCenter.Y);
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < 300; i++)
				{
					Tile tileSafely2 = Framing.GetTileSafely(point);
					if (npcspawningFlagsForDualDungeons.CanScan(tileSafely2) && npcspawningFlagsForDualDungeons.ScanZonesFor(true, point.X, point.Y, (int)tileSafely2.type, (int)tileSafely2.wall, true))
					{
						Tile tileSafely3 = Framing.GetTileSafely(new Point(point.X, point.Y - 1));
						num = (int)tileSafely2.type;
						num2 = (int)tileSafely3.wall;
						break;
					}
					point.Y++;
				}
				npcspawningFlagsForDualDungeons.ScanZonesFor(false, point.X, point.Y, num, num2, true);
				this.ZoneDungeon = npcspawningFlagsForDualDungeons.ZoneDungeon;
				this.ZoneSnow = npcspawningFlagsForDualDungeons.ZoneSnow;
				this.ZoneGlowshroom = npcspawningFlagsForDualDungeons.ZoneGlowshroom;
				this.ZoneCorrupt = npcspawningFlagsForDualDungeons.ZoneCorrupt;
				this.ZoneCrimson = npcspawningFlagsForDualDungeons.ZoneCrimson;
				this.ZoneJungle = npcspawningFlagsForDualDungeons.ZoneJungle;
				this.ZoneHallow = npcspawningFlagsForDualDungeons.ZoneHallow;
				this.ZoneLihzhardTemple = npcspawningFlagsForDualDungeons.ZoneLihzhardTemple;
				this.ZoneUndergroundDesert = npcspawningFlagsForDualDungeons.ZoneUndergroundDesert;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00010434 File Offset: 0x0000E634
		private void ScanNPCPositions()
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active)
				{
					Vector2 vector = this.ClosestNPCPosition[npc.type];
					if (vector == Vector2.Zero || Vector2.DistanceSquared(this.Center, npc.Center) < Vector2.DistanceSquared(this.Center, vector))
					{
						this.ClosestNPCPosition[npc.type] = npc.Center;
					}
					if (npc.townNPC && Utils.CenteredRectangle(this.Center, SceneMetrics.TownNPCRectSize).Contains(npc.Center.ToPoint()))
					{
						int townNPCCount = this.TownNPCCount;
						this.TownNPCCount = townNPCCount + 1;
					}
				}
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00010500 File Offset: 0x0000E700
		private void AddPlayerEffects(Player player)
		{
			this.PerspectivePlayer = player;
			if (player.inventory[player.selectedItem].type == 148)
			{
				this.ZoneWaterCandle = true;
			}
			if (player.inventory[player.selectedItem].type == 3117)
			{
				this.ZonePeaceCandle = true;
			}
			if (player.inventory[player.selectedItem].type == 5322)
			{
				this.ZoneShadowCandle = true;
			}
			if (player.musicBox >= 0)
			{
				this.ActiveMusicBox = player.musicBox;
			}
			if (player.musicBoxSilence)
			{
				this.MusicBoxSilence = true;
			}
			if (player.happyFunTorchTime)
			{
				this.InTorchGodMinigame = true;
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000105A7 File Offset: 0x0000E7A7
		public int GetTileCount(ushort tileId)
		{
			return this._tileCounts[(int)tileId];
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000105B4 File Offset: 0x0000E7B4
		public void Reset()
		{
			this.LastScanTime = uint.MaxValue;
			Array.Clear(this._tileCounts, 0, this._tileCounts.Length);
			Array.Clear(this._liquidCounts, 0, this._liquidCounts.Length);
			Array.Clear(this.ClosestNPCPosition, 0, this.ClosestNPCPosition.Length);
			this.SandTileCount = 0;
			this.EvilTileCount = 0;
			this.BloodTileCount = 0;
			this.GraveyardTileCount = 0;
			this.DesertSandTileCount = 0;
			this.MushroomTileCount = 0;
			this.SnowTileCount = 0;
			this.HolyTileCount = 0;
			this.HoneyBlockCount = 0;
			this.ShimmerTileCount = 0;
			this.MeteorTileCount = 0;
			this.JungleTileCount = 0;
			this.DungeonTileCount = 0;
			this.OceanSandTileCount = 0;
			this.HasCampfire = false;
			this.HasSunflower = false;
			this.HasGardenGnome = false;
			this.HasStarInBottle = false;
			this.HasHeartLantern = false;
			this.HasClock = false;
			this.HasCatBast = false;
			this.ActiveMusicBox = -1;
			this.MusicBoxSilence = false;
			this.WaterCandleCount = 0;
			this.PeaceCandleCount = 0;
			this.ShadowCandleCount = 0;
			this.ActiveFountainColor = -1;
			this.ActiveMonolithType = -1;
			this.PartyMonolithCount = 0;
			this.BloodMoonMonolith = false;
			this.MoonLordMonolith = false;
			this.EchoMonolith = false;
			this.ShimmerMonolithState = 0;
			this.CRTMonolith = false;
			this.RetroMonolith = false;
			this.NoirMonolith = false;
			this.RadioThingMonolith = false;
			this.BehindBackwall = false;
			this.BelowSurface = false;
			this.ZoneSkyHeight = false;
			this.ZoneOverworldHeight = false;
			this.ZoneDirtLayerHeight = false;
			this.ZoneRockLayerHeight = false;
			this.ZoneUnderworldHeight = false;
			this.ZoneCorrupt = false;
			this.ZoneCrimson = false;
			this.ZoneHallow = false;
			this.ZoneJungle = false;
			this.ZoneSnow = false;
			this.ZoneDesert = false;
			this.ZoneGlowshroom = false;
			this.ZoneMeteor = false;
			this.ZoneGraveyard = false;
			this.ZoneDungeon = false;
			this.ZoneLihzhardTemple = false;
			this.ZoneGranite = false;
			this.ZoneMarble = false;
			this.ZoneHive = false;
			this.ZoneGemCave = false;
			this.ZoneBeach = false;
			this.ZoneUndergroundDesert = false;
			this.SurfaceAtmospherics = false;
			this.ZoneRain = false;
			this.ZoneSandstorm = false;
			this.UndergroundForShimmering = false;
			this.ZoneShimmer = false;
			this.ZoneWaterCandle = false;
			this.ZonePeaceCandle = false;
			this.ZoneShadowCandle = false;
			this.InTorchGodMinigame = false;
			Array.Clear(this.NPCBannerBuff, 0, this.NPCBannerBuff.Length);
			this.hasBanner = false;
			this.CanPlayCreditsRoll = false;
			this.BestOreType = -1;
			this.BestOrePosition = default(Point);
			this._bestOreDistSq = int.MaxValue;
			this.TownNPCCount = 0;
			this.PerspectivePlayer = SceneMetrics._dummyPlayer;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00010844 File Offset: 0x0000EA44
		private void UpdateOreFinder(Point pos, Tile tile)
		{
			int num = (int)Main.tileOreFinderPriority[(int)tile.type];
			if (num <= 0)
			{
				return;
			}
			int num2 = (int)((this.BestOreType < 0) ? -1 : Main.tileOreFinderPriority[this.BestOreType]);
			if (num < num2)
			{
				return;
			}
			if (!SceneMetrics.IsValidForOreFinder(tile))
			{
				return;
			}
			Point point = new Point(pos.X - this.TileCenter.X, pos.Y - this.TileCenter.Y);
			int num3 = point.X * point.X + point.Y * point.Y;
			if (num == num2 && num3 >= this._bestOreDistSq)
			{
				return;
			}
			this.BestOreType = (int)tile.type;
			this.BestOrePosition = pos;
			this._bestOreDistSq = num3;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000108FC File Offset: 0x0000EAFC
		public static bool IsValidForOreFinder(Tile t)
		{
			if (t.type == 227)
			{
				return t.frameX >= 272 && t.frameX <= 374;
			}
			return t.type != 129 || t.frameX >= 324;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00010958 File Offset: 0x0000EB58
		public bool WithinRangeOfNPC(int type, double range)
		{
			Vector2 vector = this.ClosestNPCPosition[type];
			return vector != Vector2.Zero && (double)Vector2.DistanceSquared(this.Center, vector) <= range * range;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00010998 File Offset: 0x0000EB98
		// Note: this type is marked as 'beforefieldinit'.
		static SceneMetrics()
		{
		}

		// Token: 0x0400009D RID: 157
		private static readonly Point AssumedConstantScreenSize = new Point(1920, 1200);

		// Token: 0x0400009E RID: 158
		private static readonly int ZoneScanPadding = 25;

		// Token: 0x0400009F RID: 159
		public static readonly Point ZoneScanSize = new Point(SceneMetrics.AssumedConstantScreenSize.X / 16 + SceneMetrics.ZoneScanPadding * 2 - 1, SceneMetrics.AssumedConstantScreenSize.Y / 16 + SceneMetrics.ZoneScanPadding * 2 - 1);

		// Token: 0x040000A0 RID: 160
		public static readonly Vector2 TownNPCRectSize = SceneMetrics.AssumedConstantScreenSize.ToVector2() * 2f;

		// Token: 0x040000A1 RID: 161
		[CompilerGenerated]
		private uint <LastScanTime>k__BackingField;

		// Token: 0x040000A2 RID: 162
		[CompilerGenerated]
		private Vector2 <Center>k__BackingField;

		// Token: 0x040000A3 RID: 163
		[CompilerGenerated]
		private Point <TileCenter>k__BackingField;

		// Token: 0x040000A4 RID: 164
		private int _bestOreDistSq;

		// Token: 0x040000A5 RID: 165
		[CompilerGenerated]
		private Point <BestOrePosition>k__BackingField;

		// Token: 0x040000A6 RID: 166
		public int BestOreType;

		// Token: 0x040000A7 RID: 167
		public static int ShimmerTileThreshold = 300;

		// Token: 0x040000A8 RID: 168
		public static int CorruptionTileThreshold = 300;

		// Token: 0x040000A9 RID: 169
		public static int CorruptionTileMax = 1000;

		// Token: 0x040000AA RID: 170
		public static int CrimsonTileThreshold = 300;

		// Token: 0x040000AB RID: 171
		public static int CrimsonTileMax = 1000;

		// Token: 0x040000AC RID: 172
		public static int HallowTileThreshold = 125;

		// Token: 0x040000AD RID: 173
		public static int HallowTileMax = 600;

		// Token: 0x040000AE RID: 174
		public static int JungleTileThreshold = 140;

		// Token: 0x040000AF RID: 175
		public static int JungleTileMax = 700;

		// Token: 0x040000B0 RID: 176
		public static int SnowTileNormalThreshold = 1500;

		// Token: 0x040000B1 RID: 177
		public static int SnowTileSkyblockThreshold = 300;

		// Token: 0x040000B2 RID: 178
		public static int SnowTileMax = 6000;

		// Token: 0x040000B3 RID: 179
		public static int DesertTileNormalThreshold = 1500;

		// Token: 0x040000B4 RID: 180
		public static int DesertTileSkyblockThreshold = 300;

		// Token: 0x040000B5 RID: 181
		public static int MushroomTileThreshold = 100;

		// Token: 0x040000B6 RID: 182
		public static int MushroomTileMax = 160;

		// Token: 0x040000B7 RID: 183
		public static int MeteorTileThreshold = 75;

		// Token: 0x040000B8 RID: 184
		public static int DungeonTileThreshold = 250;

		// Token: 0x040000B9 RID: 185
		public static int GraveyardTileMax = 36;

		// Token: 0x040000BA RID: 186
		public static int GraveyardTileMin = 16;

		// Token: 0x040000BB RID: 187
		public static int GraveyardTileThreshold = 28;

		// Token: 0x040000BC RID: 188
		[CompilerGenerated]
		private int <ShimmerTileCount>k__BackingField;

		// Token: 0x040000BD RID: 189
		[CompilerGenerated]
		private int <EvilTileCount>k__BackingField;

		// Token: 0x040000BE RID: 190
		[CompilerGenerated]
		private int <HolyTileCount>k__BackingField;

		// Token: 0x040000BF RID: 191
		[CompilerGenerated]
		private int <HoneyBlockCount>k__BackingField;

		// Token: 0x040000C0 RID: 192
		[CompilerGenerated]
		private int <ActiveMusicBox>k__BackingField;

		// Token: 0x040000C1 RID: 193
		[CompilerGenerated]
		private bool <MusicBoxSilence>k__BackingField;

		// Token: 0x040000C2 RID: 194
		[CompilerGenerated]
		private int <SandTileCount>k__BackingField;

		// Token: 0x040000C3 RID: 195
		[CompilerGenerated]
		private int <MushroomTileCount>k__BackingField;

		// Token: 0x040000C4 RID: 196
		[CompilerGenerated]
		private int <SnowTileCount>k__BackingField;

		// Token: 0x040000C5 RID: 197
		[CompilerGenerated]
		private int <WaterCandleCount>k__BackingField;

		// Token: 0x040000C6 RID: 198
		[CompilerGenerated]
		private int <PeaceCandleCount>k__BackingField;

		// Token: 0x040000C7 RID: 199
		[CompilerGenerated]
		private int <ShadowCandleCount>k__BackingField;

		// Token: 0x040000C8 RID: 200
		[CompilerGenerated]
		private int <PartyMonolithCount>k__BackingField;

		// Token: 0x040000C9 RID: 201
		[CompilerGenerated]
		private int <MeteorTileCount>k__BackingField;

		// Token: 0x040000CA RID: 202
		[CompilerGenerated]
		private int <BloodTileCount>k__BackingField;

		// Token: 0x040000CB RID: 203
		[CompilerGenerated]
		private int <JungleTileCount>k__BackingField;

		// Token: 0x040000CC RID: 204
		[CompilerGenerated]
		private int <DungeonTileCount>k__BackingField;

		// Token: 0x040000CD RID: 205
		[CompilerGenerated]
		private bool <HasSunflower>k__BackingField;

		// Token: 0x040000CE RID: 206
		[CompilerGenerated]
		private bool <HasGardenGnome>k__BackingField;

		// Token: 0x040000CF RID: 207
		[CompilerGenerated]
		private bool <HasClock>k__BackingField;

		// Token: 0x040000D0 RID: 208
		[CompilerGenerated]
		private bool <HasCampfire>k__BackingField;

		// Token: 0x040000D1 RID: 209
		[CompilerGenerated]
		private bool <HasStarInBottle>k__BackingField;

		// Token: 0x040000D2 RID: 210
		[CompilerGenerated]
		private bool <HasHeartLantern>k__BackingField;

		// Token: 0x040000D3 RID: 211
		[CompilerGenerated]
		private int <ActiveFountainColor>k__BackingField;

		// Token: 0x040000D4 RID: 212
		[CompilerGenerated]
		private int <ActiveMonolithType>k__BackingField;

		// Token: 0x040000D5 RID: 213
		[CompilerGenerated]
		private bool <BloodMoonMonolith>k__BackingField;

		// Token: 0x040000D6 RID: 214
		[CompilerGenerated]
		private bool <MoonLordMonolith>k__BackingField;

		// Token: 0x040000D7 RID: 215
		[CompilerGenerated]
		private bool <EchoMonolith>k__BackingField;

		// Token: 0x040000D8 RID: 216
		[CompilerGenerated]
		private int <ShimmerMonolithState>k__BackingField;

		// Token: 0x040000D9 RID: 217
		[CompilerGenerated]
		private bool <CRTMonolith>k__BackingField;

		// Token: 0x040000DA RID: 218
		[CompilerGenerated]
		private bool <RetroMonolith>k__BackingField;

		// Token: 0x040000DB RID: 219
		[CompilerGenerated]
		private bool <NoirMonolith>k__BackingField;

		// Token: 0x040000DC RID: 220
		[CompilerGenerated]
		private bool <RadioThingMonolith>k__BackingField;

		// Token: 0x040000DD RID: 221
		[CompilerGenerated]
		private bool <HasCatBast>k__BackingField;

		// Token: 0x040000DE RID: 222
		[CompilerGenerated]
		private int <GraveyardTileCount>k__BackingField;

		// Token: 0x040000DF RID: 223
		[CompilerGenerated]
		private int <DesertSandTileCount>k__BackingField;

		// Token: 0x040000E0 RID: 224
		[CompilerGenerated]
		private int <OceanSandTileCount>k__BackingField;

		// Token: 0x040000E1 RID: 225
		[CompilerGenerated]
		private bool <BehindBackwall>k__BackingField;

		// Token: 0x040000E2 RID: 226
		public bool BelowSurface;

		// Token: 0x040000E3 RID: 227
		public bool ZoneSkyHeight;

		// Token: 0x040000E4 RID: 228
		public bool ZoneOverworldHeight;

		// Token: 0x040000E5 RID: 229
		public bool ZoneDirtLayerHeight;

		// Token: 0x040000E6 RID: 230
		public bool ZoneRockLayerHeight;

		// Token: 0x040000E7 RID: 231
		public bool ZoneUnderworldHeight;

		// Token: 0x040000E8 RID: 232
		public bool ZoneCorrupt;

		// Token: 0x040000E9 RID: 233
		public bool ZoneCrimson;

		// Token: 0x040000EA RID: 234
		public bool ZoneHallow;

		// Token: 0x040000EB RID: 235
		public bool ZoneJungle;

		// Token: 0x040000EC RID: 236
		public bool ZoneSnow;

		// Token: 0x040000ED RID: 237
		public bool ZoneDesert;

		// Token: 0x040000EE RID: 238
		public bool ZoneGlowshroom;

		// Token: 0x040000EF RID: 239
		public bool ZoneMeteor;

		// Token: 0x040000F0 RID: 240
		public bool ZoneGraveyard;

		// Token: 0x040000F1 RID: 241
		public bool ZoneDungeon;

		// Token: 0x040000F2 RID: 242
		public bool ZoneLihzhardTemple;

		// Token: 0x040000F3 RID: 243
		public bool ZoneGranite;

		// Token: 0x040000F4 RID: 244
		public bool ZoneMarble;

		// Token: 0x040000F5 RID: 245
		public bool ZoneHive;

		// Token: 0x040000F6 RID: 246
		public bool ZoneGemCave;

		// Token: 0x040000F7 RID: 247
		public bool ZoneBeach;

		// Token: 0x040000F8 RID: 248
		public bool ZoneUndergroundDesert;

		// Token: 0x040000F9 RID: 249
		public bool ZoneRain;

		// Token: 0x040000FA RID: 250
		public bool ZoneSandstorm;

		// Token: 0x040000FB RID: 251
		public bool SurfaceAtmospherics;

		// Token: 0x040000FC RID: 252
		public bool UndergroundForShimmering;

		// Token: 0x040000FD RID: 253
		public bool ZoneShimmer;

		// Token: 0x040000FE RID: 254
		public bool ZoneWaterCandle;

		// Token: 0x040000FF RID: 255
		public bool ZonePeaceCandle;

		// Token: 0x04000100 RID: 256
		public bool ZoneShadowCandle;

		// Token: 0x04000101 RID: 257
		public bool InTorchGodMinigame;

		// Token: 0x04000102 RID: 258
		public static int NPCEventZoneRadius = 4000;

		// Token: 0x04000103 RID: 259
		public bool CanPlayCreditsRoll;

		// Token: 0x04000104 RID: 260
		public bool[] NPCBannerBuff = new bool[BannerSystem.MaxBannerTypes];

		// Token: 0x04000105 RID: 261
		public bool hasBanner;

		// Token: 0x04000106 RID: 262
		public Vector2[] ClosestNPCPosition = new Vector2[(int)NPCID.Count];

		// Token: 0x04000107 RID: 263
		[CompilerGenerated]
		private int <TownNPCCount>k__BackingField;

		// Token: 0x04000108 RID: 264
		[CompilerGenerated]
		private Player <PerspectivePlayer>k__BackingField;

		// Token: 0x04000109 RID: 265
		private static Player _dummyPlayer = new Player();

		// Token: 0x0400010A RID: 266
		private readonly int[] _tileCounts = new int[(int)TileID.Count];

		// Token: 0x0400010B RID: 267
		private readonly int[] _liquidCounts = new int[(int)LiquidID.Count];
	}
}
