using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.CaveHouse
{
	// Token: 0x02000520 RID: 1312
	public class HouseBuilder
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060036A2 RID: 13986 RVA: 0x006297B0 File Offset: 0x006279B0
		// (set) Token: 0x060036A3 RID: 13987 RVA: 0x006297B8 File Offset: 0x006279B8
		public double ChestChance
		{
			[CompilerGenerated]
			get
			{
				return this.<ChestChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ChestChance>k__BackingField = value;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060036A4 RID: 13988 RVA: 0x006297C1 File Offset: 0x006279C1
		// (set) Token: 0x060036A5 RID: 13989 RVA: 0x006297C9 File Offset: 0x006279C9
		public ushort TileType
		{
			[CompilerGenerated]
			get
			{
				return this.<TileType>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<TileType>k__BackingField = value;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060036A6 RID: 13990 RVA: 0x006297D2 File Offset: 0x006279D2
		// (set) Token: 0x060036A7 RID: 13991 RVA: 0x006297DA File Offset: 0x006279DA
		public ushort WallType
		{
			[CompilerGenerated]
			get
			{
				return this.<WallType>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<WallType>k__BackingField = value;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060036A8 RID: 13992 RVA: 0x006297E3 File Offset: 0x006279E3
		// (set) Token: 0x060036A9 RID: 13993 RVA: 0x006297EB File Offset: 0x006279EB
		public ushort BeamType
		{
			[CompilerGenerated]
			get
			{
				return this.<BeamType>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<BeamType>k__BackingField = value;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060036AA RID: 13994 RVA: 0x006297F4 File Offset: 0x006279F4
		// (set) Token: 0x060036AB RID: 13995 RVA: 0x006297FC File Offset: 0x006279FC
		public byte BeamPaint
		{
			[CompilerGenerated]
			get
			{
				return this.<BeamPaint>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<BeamPaint>k__BackingField = value;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060036AC RID: 13996 RVA: 0x00629805 File Offset: 0x00627A05
		// (set) Token: 0x060036AD RID: 13997 RVA: 0x0062980D File Offset: 0x00627A0D
		public int PlatformStyle
		{
			[CompilerGenerated]
			get
			{
				return this.<PlatformStyle>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<PlatformStyle>k__BackingField = value;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060036AE RID: 13998 RVA: 0x00629816 File Offset: 0x00627A16
		// (set) Token: 0x060036AF RID: 13999 RVA: 0x0062981E File Offset: 0x00627A1E
		public int DoorStyle
		{
			[CompilerGenerated]
			get
			{
				return this.<DoorStyle>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<DoorStyle>k__BackingField = value;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060036B0 RID: 14000 RVA: 0x00629827 File Offset: 0x00627A27
		// (set) Token: 0x060036B1 RID: 14001 RVA: 0x0062982F File Offset: 0x00627A2F
		public int TableStyle
		{
			[CompilerGenerated]
			get
			{
				return this.<TableStyle>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<TableStyle>k__BackingField = value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060036B2 RID: 14002 RVA: 0x00629838 File Offset: 0x00627A38
		// (set) Token: 0x060036B3 RID: 14003 RVA: 0x00629840 File Offset: 0x00627A40
		public bool UsesTables2
		{
			[CompilerGenerated]
			get
			{
				return this.<UsesTables2>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<UsesTables2>k__BackingField = value;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x060036B4 RID: 14004 RVA: 0x00629849 File Offset: 0x00627A49
		// (set) Token: 0x060036B5 RID: 14005 RVA: 0x00629851 File Offset: 0x00627A51
		public int WorkbenchStyle
		{
			[CompilerGenerated]
			get
			{
				return this.<WorkbenchStyle>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<WorkbenchStyle>k__BackingField = value;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060036B6 RID: 14006 RVA: 0x0062985A File Offset: 0x00627A5A
		// (set) Token: 0x060036B7 RID: 14007 RVA: 0x00629862 File Offset: 0x00627A62
		public int PianoStyle
		{
			[CompilerGenerated]
			get
			{
				return this.<PianoStyle>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<PianoStyle>k__BackingField = value;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060036B8 RID: 14008 RVA: 0x0062986B File Offset: 0x00627A6B
		// (set) Token: 0x060036B9 RID: 14009 RVA: 0x00629873 File Offset: 0x00627A73
		public int BookcaseStyle
		{
			[CompilerGenerated]
			get
			{
				return this.<BookcaseStyle>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<BookcaseStyle>k__BackingField = value;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060036BA RID: 14010 RVA: 0x0062987C File Offset: 0x00627A7C
		// (set) Token: 0x060036BB RID: 14011 RVA: 0x00629884 File Offset: 0x00627A84
		public int ChairStyle
		{
			[CompilerGenerated]
			get
			{
				return this.<ChairStyle>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ChairStyle>k__BackingField = value;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060036BC RID: 14012 RVA: 0x0062988D File Offset: 0x00627A8D
		// (set) Token: 0x060036BD RID: 14013 RVA: 0x00629895 File Offset: 0x00627A95
		public int ChestStyle
		{
			[CompilerGenerated]
			get
			{
				return this.<ChestStyle>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ChestStyle>k__BackingField = value;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060036BE RID: 14014 RVA: 0x0062989E File Offset: 0x00627A9E
		// (set) Token: 0x060036BF RID: 14015 RVA: 0x006298A6 File Offset: 0x00627AA6
		public bool UsesContainers2
		{
			[CompilerGenerated]
			get
			{
				return this.<UsesContainers2>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<UsesContainers2>k__BackingField = value;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060036C0 RID: 14016 RVA: 0x006298AF File Offset: 0x00627AAF
		// (set) Token: 0x060036C1 RID: 14017 RVA: 0x006298B7 File Offset: 0x00627AB7
		public ReadOnlyCollection<Rectangle> Rooms
		{
			[CompilerGenerated]
			get
			{
				return this.<Rooms>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Rooms>k__BackingField = value;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060036C2 RID: 14018 RVA: 0x006298C0 File Offset: 0x00627AC0
		public Rectangle TopRoom
		{
			get
			{
				return this.Rooms.First<Rectangle>();
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060036C3 RID: 14019 RVA: 0x006298CD File Offset: 0x00627ACD
		public Rectangle BottomRoom
		{
			get
			{
				return this.Rooms.Last<Rectangle>();
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060036C4 RID: 14020 RVA: 0x004DDDC9 File Offset: 0x004DBFC9
		private UnifiedRandom _random
		{
			get
			{
				return WorldGen.genRand;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060036C5 RID: 14021 RVA: 0x004DDDD0 File Offset: 0x004DBFD0
		private Tile[,] _tiles
		{
			get
			{
				return Main.tile;
			}
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x006298DA File Offset: 0x00627ADA
		private HouseBuilder()
		{
			this.IsValid = false;
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x00629900 File Offset: 0x00627B00
		protected HouseBuilder(HouseType type, IEnumerable<Rectangle> rooms)
		{
			this.Type = type;
			this.IsValid = true;
			List<Rectangle> list = rooms.ToList<Rectangle>();
			list.Sort((Rectangle lhs, Rectangle rhs) => lhs.Top.CompareTo(rhs.Top));
			this.Rooms = list.AsReadOnly();
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x00009E46 File Offset: 0x00008046
		protected virtual void AgeRoom(Rectangle room)
		{
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x00629970 File Offset: 0x00627B70
		public void PotentiallyConvertToRainbowMossBlock()
		{
			if (WorldGen.SecretSeed.rainbowStuff.Enabled && WorldGen.genRand.Next(2) == 0)
			{
				this.TileType = 692;
				this.WallType = 346;
				this.PlatformStyle = 43;
				this.DoorStyle = 44;
			}
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x006299BC File Offset: 0x00627BBC
		public void PotentiallyConvertToRainbowBrick()
		{
			if (Main.tenthAnniversaryWorld)
			{
				if (Main.getGoodWorld)
				{
					if (WorldGen.genRand.Next(7) == 0)
					{
						this.TileType = 160;
						this.WallType = 44;
						return;
					}
				}
				else if (WorldGen.genRand.Next(2) == 0)
				{
					this.TileType = 160;
					this.WallType = 44;
				}
			}
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x00629A18 File Offset: 0x00627C18
		public void RainbowifyOnTenthAnniversaryWorlds()
		{
			if (!Main.tenthAnniversaryWorld || (this.TileType == 160 && WorldGen.genRand.Next(2) == 0))
			{
				return;
			}
			foreach (Rectangle rectangle in this.Rooms)
			{
				WorldUtils.Gen(new Point(rectangle.X, rectangle.Y), new Shapes.Rectangle(rectangle.Width, rectangle.Height), new Actions.SetTileAndWallRainbowPaint());
			}
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x00629AB0 File Offset: 0x00627CB0
		public void PotentiallyConvertToSeedHouse()
		{
			if (WorldGen.SecretSeed.errorWorld.Enabled)
			{
				this.PlatformStyle = WorldGen.genRand.Next(49);
				this.DoorStyle = WorldGen.genRand.Next(49);
				this.TableStyle = WorldGen.genRand.Next(35);
				this.WorkbenchStyle = WorldGen.genRand.Next(44);
				this.PianoStyle = WorldGen.genRand.Next(39);
				this.BookcaseStyle = WorldGen.genRand.Next(40);
				this.ChairStyle = WorldGen.genRand.Next(44);
				switch (WorldGen.genRand.Next(20))
				{
				default:
					this.TileType = 159;
					this.WallType = 43;
					return;
				case 1:
					this.TileType = 422;
					this.WallType = 225;
					return;
				case 2:
					this.TileType = 194;
					this.WallType = 75;
					return;
				case 3:
					this.TileType = 541;
					this.WallType = 318;
					this.PlatformStyle = 48;
					return;
				case 4:
					this.TileType = 137;
					this.WallType = 147;
					return;
				case 5:
					this.TileType = 48;
					this.WallType = 245;
					return;
				case 6:
					this.TileType = 370;
					this.WallType = 182;
					return;
				case 7:
					this.TileType = 140;
					this.WallType = 33;
					return;
				case 8:
					this.TileType = 347;
					this.WallType = 174;
					return;
				case 9:
					this.TileType = 508;
					this.WallType = 243;
					return;
				case 10:
					this.TileType = 507;
					this.WallType = 242;
					return;
				case 11:
					this.TileType = 546;
					this.WallType = 167;
					return;
				case 12:
					this.TileType = 329;
					this.WallType = 169;
					return;
				case 13:
					this.TileType = 326;
					this.WallType = 136;
					return;
				case 14:
					this.TileType = 327;
					this.WallType = 137;
					return;
				case 15:
					this.TileType = 345;
					this.WallType = 172;
					return;
				case 16:
					this.TileType = 708;
					this.WallType = 347;
					return;
				case 17:
					this.TileType = 501;
					this.WallType = 238;
					return;
				case 18:
					this.TileType = 272;
					this.WallType = 225;
					return;
				case 19:
					this.TileType = 421;
					this.WallType = 225;
					return;
				}
			}
			else
			{
				if (WorldGen.genRand.NextFloat() > 0.4f)
				{
					return;
				}
				bool flag = this.Type == HouseType.Wood;
				bool flag2 = this.Type == HouseType.Desert;
				bool flag3 = this.Type == HouseType.Jungle;
				bool flag4 = this.Type == HouseType.Ice;
				List<ushort> list = new List<ushort>();
				if (flag3 && Main.notTheBeesWorld && Main.tenthAnniversaryWorld)
				{
					list.Add(562);
					list.Add(563);
					list.Add(229);
				}
				if ((flag || flag4) && Main.drunkWorld && Main.tenthAnniversaryWorld)
				{
					if (flag4)
					{
						list.Add(197);
					}
					else
					{
						list.Add(193);
					}
				}
				if (flag4 && WorldGen.SecretSeed.worldIsFrozen.Enabled && WorldGen.genRand.Next(3) == 0)
				{
					list.Add(145);
					list.Add(146);
				}
				if (flag2 && Main.remixWorld && Main.getGoodWorld)
				{
					list.Add(188);
				}
				if (list.Count <= 0)
				{
					return;
				}
				ushort num = list[WorldGen.genRand.Next(list.Count)];
				if (num <= 193)
				{
					if (num <= 146)
					{
						if (num == 145)
						{
							this.TileType = num;
							this.WallType = 29;
							this.BeamType = 574;
							this.BeamPaint = 26;
							return;
						}
						if (num != 146)
						{
							return;
						}
						this.TileType = num;
						this.WallType = 30;
						this.BeamType = 574;
						this.BeamPaint = 26;
						return;
					}
					else
					{
						if (num == 188)
						{
							this.TileType = num;
							this.WallType = 72;
							this.BeamType = 124;
							this.BeamPaint = 17;
							this.PlatformStyle = 25;
							this.DoorStyle = 4;
							this.TableStyle = 30;
							this.UsesTables2 = false;
							this.WorkbenchStyle = 5;
							this.PianoStyle = 17;
							this.BookcaseStyle = 6;
							this.ChairStyle = 6;
							this.ChestStyle = 42;
							this.UsesContainers2 = false;
							return;
						}
						if (num != 193)
						{
							return;
						}
						this.TileType = num;
						this.WallType = 76;
						this.BeamType = 124;
						this.BeamPaint = 19;
						this.PlatformStyle = 20;
						this.DoorStyle = 31;
						this.TableStyle = 29;
						this.UsesTables2 = false;
						this.WorkbenchStyle = 8;
						this.PianoStyle = 24;
						this.BookcaseStyle = 26;
						this.ChairStyle = 31;
						this.ChestStyle = 34;
						this.UsesContainers2 = false;
						return;
					}
				}
				else if (num <= 229)
				{
					if (num == 197)
					{
						this.TileType = num;
						this.WallType = 76;
						this.BeamType = 574;
						this.BeamPaint = 26;
						this.PlatformStyle = 20;
						this.DoorStyle = 31;
						this.TableStyle = 29;
						this.UsesTables2 = false;
						this.WorkbenchStyle = 8;
						this.PianoStyle = 24;
						this.BookcaseStyle = 26;
						this.ChairStyle = 31;
						this.ChestStyle = 34;
						this.UsesContainers2 = false;
						return;
					}
					if (num != 229)
					{
						return;
					}
					this.TileType = num;
					this.WallType = 86;
					this.BeamType = 575;
					this.BeamPaint = 15;
					this.PlatformStyle = 24;
					this.DoorStyle = 22;
					this.TableStyle = 19;
					this.UsesTables2 = false;
					this.WorkbenchStyle = 19;
					this.PianoStyle = 9;
					this.BookcaseStyle = 9;
					this.ChairStyle = 22;
					this.ChestStyle = 29;
					this.UsesContainers2 = false;
					return;
				}
				else
				{
					if (num == 562)
					{
						this.TileType = num;
						this.WallType = 312;
						this.BeamType = 575;
						this.BeamPaint = 16;
						this.PlatformStyle = 44;
						this.DoorStyle = 45;
						this.TableStyle = 8;
						this.UsesTables2 = true;
						this.WorkbenchStyle = 40;
						this.PianoStyle = 39;
						this.BookcaseStyle = 40;
						this.ChairStyle = 44;
						this.ChestStyle = 11;
						this.UsesContainers2 = true;
						return;
					}
					if (num != 563)
					{
						return;
					}
					this.TileType = num;
					this.WallType = 313;
					this.BeamType = 575;
					this.BeamPaint = 16;
					this.PlatformStyle = 44;
					this.DoorStyle = 45;
					this.TableStyle = 8;
					this.UsesTables2 = true;
					this.WorkbenchStyle = 40;
					this.PianoStyle = 39;
					this.BookcaseStyle = 40;
					this.ChairStyle = 44;
					this.ChestStyle = 11;
					this.UsesContainers2 = true;
					return;
				}
			}
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x0062A1D4 File Offset: 0x006283D4
		public void PaintSeedHouses()
		{
			if (this.TileType == 197 && Main.drunkWorld && Main.tenthAnniversaryWorld)
			{
				foreach (Rectangle rectangle in this.Rooms)
				{
					WorldUtils.Gen(new Point(rectangle.X, rectangle.Y), new Shapes.Rectangle(rectangle.Width, rectangle.Height), Actions.Chain(new GenAction[]
					{
						new Modifiers.OnlyTiles(new ushort[] { 19, 10, 11, 14, 18, 87, 101, 15, 21 }),
						new Actions.SetTilePaint(7)
					}));
					WorldUtils.Gen(new Point(rectangle.X, rectangle.Y), new Shapes.Rectangle(rectangle.Width, rectangle.Height), Actions.Chain(new GenAction[]
					{
						new Modifiers.OnlyWalls(new ushort[] { this.WallType }),
						new Actions.SetWallPaint(7)
					}));
				}
			}
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x0062A2F0 File Offset: 0x006284F0
		public virtual void Place(HouseBuilderContext context, StructureMap structures)
		{
			this.PlaceEmptyRooms();
			foreach (Rectangle rectangle in this.Rooms)
			{
				structures.AddProtectedStructure(rectangle, 8);
			}
			this.PlaceStairs();
			this.PlaceDoors();
			this.PlacePlatforms();
			this.PlaceSupportBeams();
			this.PlaceBiomeSpecificPriorityTool(context);
			this.FillRooms();
			foreach (Rectangle rectangle2 in this.Rooms)
			{
				this.AgeRoom(rectangle2);
			}
			this.PlaceChests();
			this.PlaceBiomeSpecificTool(context);
			this.PaintSeedHouses();
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x0062A3B8 File Offset: 0x006285B8
		private void PlaceEmptyRooms()
		{
			foreach (Rectangle rectangle in this.Rooms)
			{
				WorldUtils.Gen(new Point(rectangle.X, rectangle.Y), new Shapes.Rectangle(rectangle.Width, rectangle.Height), Actions.Chain(new GenAction[]
				{
					new Actions.SetTileKeepWall(this.TileType, false, true),
					new Actions.SetFrames(true)
				}));
				WorldUtils.Gen(new Point(rectangle.X + 1, rectangle.Y + 1), new Shapes.Rectangle(rectangle.Width - 2, rectangle.Height - 2), Actions.Chain(new GenAction[]
				{
					new Actions.ClearTile(true),
					new Actions.PlaceWall(this.WallType, true)
				}));
			}
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x0062A4A4 File Offset: 0x006286A4
		private void FillRooms()
		{
			int num = 14;
			if (this.UsesTables2)
			{
				num = 469;
			}
			Point[] array = new Point[]
			{
				new Point(num, this.TableStyle),
				new Point(16, 0),
				new Point(18, this.WorkbenchStyle),
				new Point(86, 0),
				new Point(87, this.PianoStyle),
				new Point(94, 0),
				new Point(101, this.BookcaseStyle)
			};
			foreach (Rectangle rectangle in this.Rooms)
			{
				int num2 = rectangle.Width / 8;
				int num3 = rectangle.Width / (num2 + 1);
				int num4 = this._random.Next(2);
				for (int i = 0; i < num2; i++)
				{
					int num5 = (i + 1) * num3 + rectangle.X;
					int num6 = i + num4 % 2;
					if (num6 != 0)
					{
						if (num6 == 1)
						{
							int num7 = rectangle.Y + 1;
							WorldGen.PlaceTile(num5, num7, 34, true, false, -1, this._random.Next(6));
							for (int j = -1; j < 2; j++)
							{
								for (int k = 0; k < 3; k++)
								{
									Tile tile = this._tiles[j + num5, k + num7];
									tile.frameX += 54;
								}
							}
						}
					}
					else
					{
						int num7 = rectangle.Y + Math.Min(rectangle.Height / 2, rectangle.Height - 5);
						PaintingEntry paintingEntry = ((this.Type == HouseType.Desert) ? WorldGen.RandHousePictureDesert() : WorldGen.RandHousePicture());
						WorldGen.PlaceTile(num5, num7, paintingEntry.tileType, true, false, -1, paintingEntry.style);
					}
				}
				int l = rectangle.Width / 8 + 3;
				WorldGen.SetupStatueList();
				while (l > 0)
				{
					int num8 = this._random.Next(rectangle.Width - 3) + 1 + rectangle.X;
					int num9 = rectangle.Y + rectangle.Height - 2;
					switch (this._random.Next(4))
					{
					case 0:
						WorldGen.PlaceSmallPile(num8, num9, this._random.Next(31, 34), 1, 185);
						break;
					case 1:
						WorldGen.PlaceTile(num8, num9, 186, true, false, -1, this._random.Next(22, 26));
						break;
					case 2:
					{
						int num10 = this._random.Next(2, GenVars.statueList.Length);
						WorldGen.PlaceTile(num8, num9, (int)GenVars.statueList[num10].X, true, false, -1, (int)GenVars.statueList[num10].Y);
						if (GenVars.StatuesWithTraps.Contains(num10))
						{
							WorldGen.PlaceStatueTrap(num8, num9);
						}
						break;
					}
					case 3:
					{
						Point point = Utils.SelectRandom<Point>(this._random, array);
						WorldGen.PlaceTile(num8, num9, point.X, true, false, -1, point.Y);
						break;
					}
					}
					l--;
				}
			}
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x0062A7FC File Offset: 0x006289FC
		private void PlaceStairs()
		{
			foreach (Tuple<Point, Point> tuple in this.CreateStairsList())
			{
				Point item = tuple.Item1;
				Point item2 = tuple.Item2;
				int num = ((item2.X > item.X) ? 1 : (-1));
				ShapeData shapeData = new ShapeData();
				for (int i = 0; i < item2.Y - item.Y; i++)
				{
					shapeData.Add(num * (i + 1), i);
				}
				WorldUtils.Gen(item, new ModShapes.All(shapeData), Actions.Chain(new GenAction[]
				{
					new Actions.PlaceTile(19, this.PlatformStyle),
					new Actions.SetSlope((num == 1) ? 1 : 2),
					new Actions.SetFrames(true)
				}));
				WorldUtils.Gen(new Point(item.X + ((num == 1) ? 1 : (-4)), item.Y - 1), new Shapes.Rectangle(4, 1), Actions.Chain(new GenAction[]
				{
					new Actions.Clear(),
					new Actions.PlaceWall(this.WallType, true),
					new Actions.PlaceTile(19, this.PlatformStyle),
					new Actions.SetFrames(true)
				}));
			}
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x0062A954 File Offset: 0x00628B54
		private List<Tuple<Point, Point>> CreateStairsList()
		{
			List<Tuple<Point, Point>> list = new List<Tuple<Point, Point>>();
			for (int i = 1; i < this.Rooms.Count; i++)
			{
				Rectangle rectangle = this.Rooms[i];
				Rectangle rectangle2 = this.Rooms[i - 1];
				int num = rectangle2.X - rectangle.X;
				int num2 = rectangle.X + rectangle.Width - (rectangle2.X + rectangle2.Width);
				if (num > num2)
				{
					list.Add(new Tuple<Point, Point>(new Point(rectangle.X + rectangle.Width - 1, rectangle.Y + 1), new Point(rectangle.X + rectangle.Width - rectangle.Height + 1, rectangle.Y + rectangle.Height - 1)));
				}
				else
				{
					list.Add(new Tuple<Point, Point>(new Point(rectangle.X, rectangle.Y + 1), new Point(rectangle.X + rectangle.Height - 1, rectangle.Y + rectangle.Height - 1)));
				}
			}
			return list;
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x0062AA64 File Offset: 0x00628C64
		private void PlaceDoors()
		{
			foreach (Point point in this.CreateDoorList())
			{
				WorldUtils.Gen(point, new Shapes.Rectangle(1, 3), new Actions.ClearTile(true));
				WorldGen.PlaceTile(point.X, point.Y, 10, true, true, -1, this.DoorStyle);
			}
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x0062AAE4 File Offset: 0x00628CE4
		private List<Point> CreateDoorList()
		{
			List<Point> list = new List<Point>();
			foreach (Rectangle rectangle in this.Rooms)
			{
				int num;
				if (HouseBuilder.FindSideExit(new Rectangle(rectangle.X + rectangle.Width, rectangle.Y + 1, 1, rectangle.Height - 2), false, out num))
				{
					list.Add(new Point(rectangle.X + rectangle.Width - 1, num));
				}
				if (HouseBuilder.FindSideExit(new Rectangle(rectangle.X, rectangle.Y + 1, 1, rectangle.Height - 2), true, out num))
				{
					list.Add(new Point(rectangle.X, num));
				}
			}
			return list;
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x0062ABB8 File Offset: 0x00628DB8
		private void PlacePlatforms()
		{
			foreach (Point point in this.CreatePlatformsList())
			{
				WorldUtils.Gen(point, new Shapes.Rectangle(3, 1), Actions.Chain(new GenAction[]
				{
					new Actions.ClearMetadata(),
					new Actions.PlaceTile(19, this.PlatformStyle),
					new Actions.SetFrames(true)
				}));
			}
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x0062AC3C File Offset: 0x00628E3C
		private List<Point> CreatePlatformsList()
		{
			List<Point> list = new List<Point>();
			Rectangle topRoom = this.TopRoom;
			Rectangle bottomRoom = this.BottomRoom;
			int num;
			if (HouseBuilder.FindVerticalExit(new Rectangle(topRoom.X + 2, topRoom.Y, topRoom.Width - 4, 1), true, out num))
			{
				list.Add(new Point(num, topRoom.Y));
			}
			if (HouseBuilder.FindVerticalExit(new Rectangle(bottomRoom.X + 2, bottomRoom.Y + bottomRoom.Height - 1, bottomRoom.Width - 4, 1), false, out num))
			{
				list.Add(new Point(num, bottomRoom.Y + bottomRoom.Height - 1));
			}
			return list;
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x0062ACE0 File Offset: 0x00628EE0
		private void PlaceSupportBeams()
		{
			foreach (Rectangle rectangle in this.CreateSupportBeamList())
			{
				if (rectangle.Height > 1 && this._tiles[rectangle.X, rectangle.Y - 1].type != 19)
				{
					WorldUtils.Gen(new Point(rectangle.X, rectangle.Y), new Shapes.Rectangle(rectangle.Width, rectangle.Height), Actions.Chain(new GenAction[]
					{
						new Actions.SetTileKeepWall(this.BeamType, false, true),
						new Actions.SetFrames(true),
						new Actions.SetTilePaint(this.BeamPaint)
					}));
					Tile tile = this._tiles[rectangle.X, rectangle.Y + rectangle.Height];
					tile.slope(0);
					tile.halfBrick(false);
				}
			}
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x0062ADE8 File Offset: 0x00628FE8
		private List<Rectangle> CreateSupportBeamList()
		{
			List<Rectangle> list = new List<Rectangle>();
			int num = this.Rooms.Min((Rectangle room) => room.Left);
			int num2 = this.Rooms.Max((Rectangle room) => room.Right) - 1;
			int num3 = 6;
			while (num3 > 4 && (num2 - num) % num3 != 0)
			{
				num3--;
			}
			for (int i = num; i <= num2; i += num3)
			{
				for (int j = 0; j < this.Rooms.Count; j++)
				{
					Rectangle rectangle = this.Rooms[j];
					if (i >= rectangle.X && i < rectangle.X + rectangle.Width)
					{
						int num4 = rectangle.Y + rectangle.Height;
						int num5 = 50;
						for (int k = j + 1; k < this.Rooms.Count; k++)
						{
							if (i >= this.Rooms[k].X && i < this.Rooms[k].X + this.Rooms[k].Width)
							{
								num5 = Math.Min(num5, this.Rooms[k].Y - num4);
							}
						}
						if (num5 > 0)
						{
							Point point;
							bool flag = WorldUtils.Find(new Point(i, num4), Searches.Chain(new Searches.Down(num5), new GenCondition[]
							{
								new Conditions.IsSolid()
							}), out point);
							if (num5 < 50 && !WorldGen.SecretSeed.GenerateBiggerAbandonedHouses)
							{
								flag = true;
								point = new Point(i, num4 + num5);
							}
							if (flag)
							{
								list.Add(new Rectangle(i, num4, 1, point.Y - num4));
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x0062AFCC File Offset: 0x006291CC
		private static bool FindVerticalExit(Rectangle wall, bool isUp, out int exitX)
		{
			Point point;
			bool flag = WorldUtils.Find(new Point(wall.X + wall.Width - 3, wall.Y + (isUp ? (-5) : 0)), Searches.Chain(new Searches.Left(wall.Width - 3), new GenCondition[] { new Conditions.IsSolid().Not().AreaOr(3, 5) }), out point);
			exitX = point.X;
			return flag;
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x0062B038 File Offset: 0x00629238
		private static bool FindSideExit(Rectangle wall, bool isLeft, out int exitY)
		{
			Point point;
			bool flag = WorldUtils.Find(new Point(wall.X + (isLeft ? (-4) : 0), wall.Y + wall.Height - 3), Searches.Chain(new Searches.Up(wall.Height - 3), new GenCondition[] { new Conditions.IsSolid().Not().AreaOr(4, 3) }), out point);
			exitY = point.Y;
			return flag;
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x0062B0A4 File Offset: 0x006292A4
		private void PlaceChests()
		{
			if (this._random.NextDouble() > this.ChestChance)
			{
				return;
			}
			bool flag = false;
			foreach (Rectangle rectangle in this.Rooms)
			{
				int num = rectangle.Height - 1 + rectangle.Y;
				bool flag2 = num > (int)Main.worldSurface;
				ushort num2 = ((flag2 && this.UsesContainers2) ? 467 : 21);
				int num3 = (flag2 ? this.ChestStyle : 0);
				int num4 = 0;
				while (num4 < 10 && !(flag = WorldGen.AddBuriedChest(this._random.Next(2, rectangle.Width - 2) + rectangle.X, num, 0, false, num3, false, num2)))
				{
					num4++;
				}
				if (flag)
				{
					break;
				}
				int num5 = rectangle.X + 2;
				while (num5 <= rectangle.X + rectangle.Width - 2 && !(flag = WorldGen.AddBuriedChest(num5, num, 0, false, num3, false, num2)))
				{
					num5++;
				}
				if (flag)
				{
					break;
				}
			}
			if (!flag)
			{
				foreach (Rectangle rectangle2 in this.Rooms)
				{
					int num6 = rectangle2.Y - 1;
					bool flag3 = num6 > (int)Main.worldSurface;
					ushort num7 = ((flag3 && this.UsesContainers2) ? 467 : 21);
					int num8 = (flag3 ? this.ChestStyle : 0);
					int num9 = 0;
					while (num9 < 10 && !(flag = WorldGen.AddBuriedChest(this._random.Next(2, rectangle2.Width - 2) + rectangle2.X, num6, 0, false, num8, false, num7)))
					{
						num9++;
					}
					if (flag)
					{
						break;
					}
					int num10 = rectangle2.X + 2;
					while (num10 <= rectangle2.X + rectangle2.Width - 2 && !(flag = WorldGen.AddBuriedChest(num10, num6, 0, false, num8, false, num7)))
					{
						num10++;
					}
					if (flag)
					{
						break;
					}
				}
			}
			if (!flag)
			{
				for (int i = 0; i < 1000; i++)
				{
					int num11 = this._random.Next(this.Rooms[0].X - 30, this.Rooms[0].X + 30);
					int num12 = this._random.Next(this.Rooms[0].Y - 30, this.Rooms[0].Y + 30);
					bool flag4 = num12 > (int)Main.worldSurface;
					ushort num13 = ((flag4 && this.UsesContainers2) ? 467 : 21);
					int num14 = (flag4 ? this.ChestStyle : 0);
					if (flag = WorldGen.AddBuriedChest(num11, num12, 0, false, num14, false, num13))
					{
						break;
					}
				}
			}
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x0062B388 File Offset: 0x00629588
		private void PlaceBiomeSpecificPriorityTool(HouseBuilderContext context)
		{
			if (this.Type == HouseType.Desert && GenVars.extraBastStatueCount < GenVars.extraBastStatueCountMax)
			{
				bool flag = false;
				foreach (Rectangle rectangle in this.Rooms)
				{
					int num = rectangle.Height - 2 + rectangle.Y;
					if (WorldGen.remixWorldGen && (double)num > Main.rockLayer)
					{
						return;
					}
					for (int i = 0; i < 10; i++)
					{
						int num2 = this._random.Next(2, rectangle.Width - 2) + rectangle.X;
						WorldGen.PlaceTile(num2, num, 506, true, true, -1, 0);
						if (flag = this._tiles[num2, num].active() && this._tiles[num2, num].type == 506)
						{
							break;
						}
					}
					if (flag)
					{
						break;
					}
					int num3 = rectangle.X + 2;
					while (num3 <= rectangle.X + rectangle.Width - 2 && !(flag = WorldGen.PlaceTile(num3, num, 506, true, true, -1, 0)))
					{
						num3++;
					}
					if (flag)
					{
						break;
					}
				}
				if (!flag)
				{
					foreach (Rectangle rectangle2 in this.Rooms)
					{
						int num4 = rectangle2.Y - 1;
						for (int j = 0; j < 10; j++)
						{
							int num5 = this._random.Next(2, rectangle2.Width - 2) + rectangle2.X;
							WorldGen.PlaceTile(num5, num4, 506, true, true, -1, 0);
							if (flag = this._tiles[num5, num4].active() && this._tiles[num5, num4].type == 506)
							{
								break;
							}
						}
						if (flag)
						{
							break;
						}
						int num6 = rectangle2.X + 2;
						while (num6 <= rectangle2.X + rectangle2.Width - 2 && !(flag = WorldGen.PlaceTile(num6, num4, 506, true, true, -1, 0)))
						{
							num6++;
						}
						if (flag)
						{
							break;
						}
					}
				}
				if (flag)
				{
					GenVars.extraBastStatueCount++;
				}
			}
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x0062B5F0 File Offset: 0x006297F0
		private void PlaceBiomeSpecificTool(HouseBuilderContext context)
		{
			if (this.Type == HouseType.Jungle && context.SharpenerCount < this._random.Next(2, 5))
			{
				bool flag = false;
				foreach (Rectangle rectangle in this.Rooms)
				{
					int num = rectangle.Height - 2 + rectangle.Y;
					for (int i = 0; i < 10; i++)
					{
						int num2 = this._random.Next(2, rectangle.Width - 2) + rectangle.X;
						WorldGen.PlaceTile(num2, num, 377, true, true, -1, 0);
						if (flag = this._tiles[num2, num].active() && this._tiles[num2, num].type == 377)
						{
							break;
						}
					}
					if (flag)
					{
						break;
					}
					int num3 = rectangle.X + 2;
					while (num3 <= rectangle.X + rectangle.Width - 2 && !(flag = WorldGen.PlaceTile(num3, num, 377, true, true, -1, 0)))
					{
						num3++;
					}
					if (flag)
					{
						break;
					}
				}
				if (flag)
				{
					context.SharpenerCount++;
				}
			}
			if (this.Type == HouseType.Desert && context.ExtractinatorCount < this._random.Next(2, 5))
			{
				ushort num4 = 219;
				if (WorldGen.SecretSeed.errorWorld.Enabled)
				{
					num4 = 642;
				}
				bool flag2 = false;
				foreach (Rectangle rectangle2 in this.Rooms)
				{
					int num5 = rectangle2.Height - 2 + rectangle2.Y;
					for (int j = 0; j < 10; j++)
					{
						int num6 = this._random.Next(2, rectangle2.Width - 2) + rectangle2.X;
						WorldGen.PlaceTile(num6, num5, (int)num4, true, true, -1, 0);
						if (flag2 = this._tiles[num6, num5].active() && this._tiles[num6, num5].type == num4)
						{
							break;
						}
					}
					if (flag2)
					{
						break;
					}
					int num7 = rectangle2.X + 2;
					while (num7 <= rectangle2.X + rectangle2.Width - 2 && !(flag2 = WorldGen.PlaceTile(num7, num5, (int)num4, true, true, -1, 0)))
					{
						num7++;
					}
					if (flag2)
					{
						break;
					}
				}
				if (flag2)
				{
					context.ExtractinatorCount++;
				}
			}
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x0062B8A0 File Offset: 0x00629AA0
		// Note: this type is marked as 'beforefieldinit'.
		static HouseBuilder()
		{
		}

		// Token: 0x04005B3A RID: 23354
		private const int VERTICAL_EXIT_WIDTH = 3;

		// Token: 0x04005B3B RID: 23355
		public static readonly HouseBuilder Invalid = new HouseBuilder();

		// Token: 0x04005B3C RID: 23356
		public readonly HouseType Type;

		// Token: 0x04005B3D RID: 23357
		public readonly bool IsValid;

		// Token: 0x04005B3E RID: 23358
		[CompilerGenerated]
		private double <ChestChance>k__BackingField;

		// Token: 0x04005B3F RID: 23359
		[CompilerGenerated]
		private ushort <TileType>k__BackingField;

		// Token: 0x04005B40 RID: 23360
		[CompilerGenerated]
		private ushort <WallType>k__BackingField;

		// Token: 0x04005B41 RID: 23361
		[CompilerGenerated]
		private ushort <BeamType>k__BackingField;

		// Token: 0x04005B42 RID: 23362
		[CompilerGenerated]
		private byte <BeamPaint>k__BackingField;

		// Token: 0x04005B43 RID: 23363
		[CompilerGenerated]
		private int <PlatformStyle>k__BackingField;

		// Token: 0x04005B44 RID: 23364
		[CompilerGenerated]
		private int <DoorStyle>k__BackingField;

		// Token: 0x04005B45 RID: 23365
		[CompilerGenerated]
		private int <TableStyle>k__BackingField;

		// Token: 0x04005B46 RID: 23366
		[CompilerGenerated]
		private bool <UsesTables2>k__BackingField;

		// Token: 0x04005B47 RID: 23367
		[CompilerGenerated]
		private int <WorkbenchStyle>k__BackingField;

		// Token: 0x04005B48 RID: 23368
		[CompilerGenerated]
		private int <PianoStyle>k__BackingField;

		// Token: 0x04005B49 RID: 23369
		[CompilerGenerated]
		private int <BookcaseStyle>k__BackingField;

		// Token: 0x04005B4A RID: 23370
		[CompilerGenerated]
		private int <ChairStyle>k__BackingField;

		// Token: 0x04005B4B RID: 23371
		[CompilerGenerated]
		private int <ChestStyle>k__BackingField;

		// Token: 0x04005B4C RID: 23372
		[CompilerGenerated]
		private bool <UsesContainers2>k__BackingField;

		// Token: 0x04005B4D RID: 23373
		[CompilerGenerated]
		private ReadOnlyCollection<Rectangle> <Rooms>k__BackingField;

		// Token: 0x04005B4E RID: 23374
		protected ushort[] SkipTilesDuringWallAging = new ushort[] { 245, 246, 240, 241, 242 };

		// Token: 0x020009A1 RID: 2465
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060049AD RID: 18861 RVA: 0x006D2B0C File Offset: 0x006D0D0C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060049AE RID: 18862 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060049AF RID: 18863 RVA: 0x006D2B18 File Offset: 0x006D0D18
			internal int <.ctor>b__78_0(Rectangle lhs, Rectangle rhs)
			{
				return lhs.Top.CompareTo(rhs.Top);
			}

			// Token: 0x060049B0 RID: 18864 RVA: 0x0069A39F File Offset: 0x0069859F
			internal int <CreateSupportBeamList>b__95_0(Rectangle room)
			{
				return room.Left;
			}

			// Token: 0x060049B1 RID: 18865 RVA: 0x0069A3B1 File Offset: 0x006985B1
			internal int <CreateSupportBeamList>b__95_1(Rectangle room)
			{
				return room.Right;
			}

			// Token: 0x04007680 RID: 30336
			public static readonly HouseBuilder.<>c <>9 = new HouseBuilder.<>c();

			// Token: 0x04007681 RID: 30337
			public static Comparison<Rectangle> <>9__78_0;

			// Token: 0x04007682 RID: 30338
			public static Func<Rectangle, int> <>9__95_0;

			// Token: 0x04007683 RID: 30339
			public static Func<Rectangle, int> <>9__95_1;
		}
	}
}
