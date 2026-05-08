using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x02000505 RID: 1285
	public class DeadMansChestBiome : MicroBiome
	{
		// Token: 0x06003603 RID: 13827 RVA: 0x0061F6F4 File Offset: 0x0061D8F4
		public override bool Place(Point origin, StructureMap structures, GenerationProgress progress)
		{
			if (!DeadMansChestBiome.IsAGoodSpot(origin))
			{
				return false;
			}
			this.ClearCaches();
			Point point = new Point(origin.X, origin.Y + 1);
			this.FindBoulderTrapSpots(point);
			this.FindDartTrapSpots(point);
			this.FindExplosiveTrapSpots(point);
			if (!this.AreThereEnoughTraps())
			{
				return false;
			}
			this.TurnGoldChestIntoDeadMansChest(origin);
			foreach (DeadMansChestBiome.DartTrapPlacementAttempt dartTrapPlacementAttempt in this._dartTrapPlacementSpots)
			{
				this.ActuallyPlaceDartTrap(dartTrapPlacementAttempt.position, dartTrapPlacementAttempt.directionX, dartTrapPlacementAttempt.x, dartTrapPlacementAttempt.y, dartTrapPlacementAttempt.xPush, dartTrapPlacementAttempt.t);
			}
			foreach (DeadMansChestBiome.WirePlacementAttempt wirePlacementAttempt in this._wirePlacementSpots)
			{
				this.PlaceWireLine(wirePlacementAttempt.position, wirePlacementAttempt.dirX, wirePlacementAttempt.dirY, wirePlacementAttempt.steps);
			}
			foreach (DeadMansChestBiome.BoulderPlacementAttempt boulderPlacementAttempt in this._boulderPlacementSpots)
			{
				this.ActuallyPlaceBoulderTrap(boulderPlacementAttempt.position, boulderPlacementAttempt.yPush, boulderPlacementAttempt.requiredHeight, boulderPlacementAttempt.bestType);
			}
			foreach (DeadMansChestBiome.ExplosivePlacementAttempt explosivePlacementAttempt in this._explosivePlacementAttempt)
			{
				this.ActuallyPlaceExplosive(explosivePlacementAttempt.position);
			}
			this.PlaceWiresForExplosives(origin);
			return true;
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x0061F8C4 File Offset: 0x0061DAC4
		private void PlaceWiresForExplosives(Point origin)
		{
			if (this._explosivePlacementAttempt.Count > 0)
			{
				this.PlaceWireLine(origin, 0, 1, this._explosivePlacementAttempt[0].position.Y - origin.Y);
				int num = this._explosivePlacementAttempt[0].position.X;
				int num2 = this._explosivePlacementAttempt[0].position.X;
				int y = this._explosivePlacementAttempt[0].position.Y;
				for (int i = 1; i < this._explosivePlacementAttempt.Count; i++)
				{
					int x = this._explosivePlacementAttempt[i].position.X;
					if (num > x)
					{
						num = x;
					}
					if (num2 < x)
					{
						num2 = x;
					}
				}
				this.PlaceWireLine(new Point(num, y), 1, 0, num2 - num);
			}
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x0061F99D File Offset: 0x0061DB9D
		private bool AreThereEnoughTraps()
		{
			return (this._boulderPlacementSpots.Count >= 1 || this._explosivePlacementAttempt.Count >= 1) && this._dartTrapPlacementSpots.Count >= 1;
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x0061F9CE File Offset: 0x0061DBCE
		private void ClearCaches()
		{
			this._dartTrapPlacementSpots.Clear();
			this._wirePlacementSpots.Clear();
			this._boulderPlacementSpots.Clear();
			this._explosivePlacementAttempt.Clear();
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x0061F9FC File Offset: 0x0061DBFC
		private void FindBoulderTrapSpots(Point position)
		{
			int num = position.X;
			int num2 = GenBase._random.Next(this._numberOfBoulderTraps);
			int num3 = GenBase._random.Next(this._numberOfStepsBetweenBoulderTraps);
			num -= num2 / 2 * num3;
			int num4 = position.Y - 6;
			for (int i = 0; i <= num2; i++)
			{
				this.FindBoulderTrapSpot(new Point(num, num4));
				num += num3;
			}
			if (this._boulderPlacementSpots.Count > 0)
			{
				int num5 = this._boulderPlacementSpots[0].position.X;
				int num6 = this._boulderPlacementSpots[0].position.X;
				for (int j = 1; j < this._boulderPlacementSpots.Count; j++)
				{
					int x = this._boulderPlacementSpots[j].position.X;
					if (num5 > x)
					{
						num5 = x;
					}
					if (num6 < x)
					{
						num6 = x;
					}
				}
				if (num5 > position.X)
				{
					num5 = position.X;
				}
				if (num6 < position.X)
				{
					num6 = position.X;
				}
				this._wirePlacementSpots.Add(new DeadMansChestBiome.WirePlacementAttempt(new Point(num5, num4 - 1), 1, 0, num6 - num5));
				this._wirePlacementSpots.Add(new DeadMansChestBiome.WirePlacementAttempt(position, 0, -1, 7));
			}
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x0061FB48 File Offset: 0x0061DD48
		private void FindBoulderTrapSpot(Point position)
		{
			int x = position.X;
			int y = position.Y;
			for (int i = 0; i < 50; i++)
			{
				if (Main.tile[x, y - i].active())
				{
					this.PlaceBoulderTrapSpot(new Point(x, y - i), i);
					return;
				}
			}
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x0061FB98 File Offset: 0x0061DD98
		private void PlaceBoulderTrapSpot(Point position, int yPush)
		{
			int[] array = new int[(int)TileID.Count];
			for (int i = position.X; i < position.X + 2; i++)
			{
				for (int j = position.Y - 4; j <= position.Y; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile.active() && !Main.tileFrameImportant[(int)tile.type] && Main.tileSolid[(int)tile.type])
					{
						array[(int)tile.type]++;
					}
					if (tile.active() && !WorldGen.CanBeClearedDuringGeneration((int)tile.type, i, j, false))
					{
						return;
					}
					if (tile.active() && TileID.Sets.IsAContainer[(int)tile.type])
					{
						return;
					}
				}
			}
			for (int k = position.X - 1; k < position.X + 2 + 1; k++)
			{
				for (int l = position.Y - 4 - 1; l <= position.Y - 4 + 2; l++)
				{
					Tile tile2 = Main.tile[k, l];
					if (!tile2.active())
					{
						return;
					}
					if (TileID.Sets.IsAContainer[(int)tile2.type])
					{
						return;
					}
				}
			}
			int num = 2;
			int num2 = position.X - num;
			int num3 = position.Y - 4 - num;
			int num4 = position.X + num + 1;
			int num5 = position.Y - 4 + num + 1;
			for (int m = num2; m <= num4; m++)
			{
				for (int n = num3; n <= num5; n++)
				{
					Tile tile3 = Main.tile[m, n];
					if (tile3.active() && (TileID.Sets.IsAContainer[(int)tile3.type] || tile3.type == 12 || tile3.type == 665 || tile3.type == 639))
					{
						return;
					}
				}
			}
			int num6 = -1;
			for (int num7 = 0; num7 < array.Length; num7++)
			{
				if (num6 == -1 || array[num6] < array[num7])
				{
					num6 = num7;
				}
			}
			this._boulderPlacementSpots.Add(new DeadMansChestBiome.BoulderPlacementAttempt(position, yPush - 1, 4, num6));
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x0061FDBC File Offset: 0x0061DFBC
		private void FindDartTrapSpots(Point position)
		{
			int num = GenBase._random.Next(this._numberOfDartTraps);
			int num2 = ((GenBase._random.Next(2) == 0) ? (-1) : 1);
			int num3 = -1;
			for (int i = 0; i < num; i++)
			{
				bool flag = this.FindDartTrapSpotSingle(position, num2);
				num2 *= -1;
				position.Y--;
				if (flag)
				{
					num3 = i;
				}
			}
			this._wirePlacementSpots.Add(new DeadMansChestBiome.WirePlacementAttempt(new Point(position.X, position.Y + num), 0, -1, num3));
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x0061FE3C File Offset: 0x0061E03C
		private bool FindDartTrapSpotSingle(Point position, int directionX)
		{
			int x = position.X;
			int y = position.Y;
			int i = 0;
			while (i < 20)
			{
				Tile tile = Main.tile[x + i * directionX, y];
				if ((!tile.active() || tile.type < 0 || tile.type >= TileID.Count || !TileID.Sets.IsAContainer[(int)tile.type]) && tile.active() && Main.tileSolid[(int)tile.type])
				{
					if (i >= 5 && !tile.actuator() && !Main.tileFrameImportant[(int)tile.type] && WorldGen.CanBeClearedDuringGeneration((int)tile.type, x + i * directionX, y, false))
					{
						this._dartTrapPlacementSpots.Add(new DeadMansChestBiome.DartTrapPlacementAttempt(position, directionX, x, y, i, tile));
						return true;
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x0061FF08 File Offset: 0x0061E108
		private void FindExplosiveTrapSpots(Point position)
		{
			int num = position.X;
			int num2 = position.Y + 3;
			List<int> list = new List<int>();
			if (this.IsGoodSpotsForExplosive(num, num2))
			{
				list.Add(num);
			}
			num++;
			if (this.IsGoodSpotsForExplosive(num, num2))
			{
				list.Add(num);
			}
			int num3 = -1;
			if (list.Count > 0)
			{
				num3 = list[GenBase._random.Next(list.Count)];
			}
			list.Clear();
			num += GenBase._random.Next(2, 6);
			int num4 = 4;
			for (int i = num; i < num + num4; i++)
			{
				if (this.IsGoodSpotsForExplosive(i, num2))
				{
					list.Add(i);
				}
			}
			int num5 = -1;
			if (list.Count > 0)
			{
				num5 = list[GenBase._random.Next(list.Count)];
			}
			num = position.X - num4 - GenBase._random.Next(2, 6);
			for (int j = num; j < num + num4; j++)
			{
				if (this.IsGoodSpotsForExplosive(j, num2))
				{
					list.Add(j);
				}
			}
			int num6 = -1;
			if (list.Count > 0)
			{
				num6 = list[GenBase._random.Next(list.Count)];
			}
			if (num6 != -1)
			{
				this._explosivePlacementAttempt.Add(new DeadMansChestBiome.ExplosivePlacementAttempt(new Point(num6, num2)));
			}
			if (num3 != -1)
			{
				this._explosivePlacementAttempt.Add(new DeadMansChestBiome.ExplosivePlacementAttempt(new Point(num3, num2)));
			}
			if (num5 != -1)
			{
				this._explosivePlacementAttempt.Add(new DeadMansChestBiome.ExplosivePlacementAttempt(new Point(num5, num2)));
			}
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x0062008C File Offset: 0x0061E28C
		private bool IsGoodSpotsForExplosive(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return (!tile.active() || tile.type < 0 || tile.type >= TileID.Count || !TileID.Sets.IsAContainer[(int)tile.type]) && (tile.active() && Main.tileSolid[(int)tile.type] && !Main.tileFrameImportant[(int)tile.type] && !Main.tileSolidTop[(int)tile.type]);
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x0062010C File Offset: 0x0061E30C
		public List<int> GetPossibleChestsToTrapify(StructureMap structures)
		{
			List<int> list = new List<int>();
			bool[] array = new bool[TileID.Sets.GeneralPlacementTiles.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = TileID.Sets.GeneralPlacementTiles[i];
			}
			array[21] = true;
			array[467] = true;
			array[138] = true;
			array[664] = true;
			array[712] = true;
			array[713] = true;
			array[714] = true;
			array[715] = true;
			for (int j = 0; j < 8000; j++)
			{
				Chest chest = Main.chest[j];
				if (chest != null)
				{
					Point point = new Point(chest.x, chest.y);
					if (DeadMansChestBiome.IsAGoodSpot(point))
					{
						this.ClearCaches();
						Point point2 = new Point(point.X, point.Y + 1);
						this.FindBoulderTrapSpots(point2);
						this.FindDartTrapSpots(point2);
						if (this.AreThereEnoughTraps() && (structures == null || structures.CanPlace(new Rectangle(point.X, point.Y, 1, 1), array, 10)))
						{
							list.Add(j);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x00620224 File Offset: 0x0061E424
		private static bool IsAGoodSpot(Point position)
		{
			if (!WorldGen.InWorld(position.X, position.Y, 50))
			{
				return false;
			}
			if (WorldGen.oceanDepths(position.X, position.Y))
			{
				return false;
			}
			Tile tile = Main.tile[position.X, position.Y];
			if (tile.type != 21)
			{
				return false;
			}
			if (tile.frameX / 36 != 1)
			{
				return false;
			}
			tile = Main.tile[position.X, position.Y + 2];
			return WorldGen.CanBeClearedDuringGeneration((int)tile.type, position.X, position.Y + 2, false) && WorldGen.countWires(position.X, position.Y, 20) <= 0 && WorldGen.countTiles(position.X, position.Y, false, true) >= 40;
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x006202F8 File Offset: 0x0061E4F8
		private void TurnGoldChestIntoDeadMansChest(Point position)
		{
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					int num = position.X + i;
					int num2 = position.Y + j;
					Tile tile = Main.tile[num, num2];
					tile.type = 467;
					tile.frameX = (short)(144 + i * 18);
					tile.frameY = (short)(j * 18);
				}
			}
			if (GenBase._random.Next(3) == 0)
			{
				int num3 = Chest.FindChest(position.X, position.Y);
				if (num3 > -1)
				{
					Item[] item = Main.chest[num3].item;
					for (int k = item.Length - 2; k > 0; k--)
					{
						Item item2 = item[k];
						if (item2.stack != 0)
						{
							item[k + 1] = item2.DeepClone();
						}
					}
					item[1] = new Item();
					item[1].SetDefaults(5007, null);
					Main.chest[num3].item = item;
				}
			}
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x006203F4 File Offset: 0x0061E5F4
		private void ActuallyPlaceDartTrap(Point position, int directionX, int x, int y, int xPush, Tile t)
		{
			t.type = 137;
			t.frameY = 0;
			if (directionX == -1)
			{
				t.frameX = 18;
			}
			else
			{
				t.frameX = 0;
			}
			t.slope(0);
			t.halfBrick(false);
			WorldGen.TileFrame(x, y, true, false);
			this.PlaceWireLine(position, directionX, 0, xPush);
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x00620454 File Offset: 0x0061E654
		private void PlaceWireLine(Point start, int offsetX, int offsetY, int steps)
		{
			for (int i = 0; i <= steps; i++)
			{
				Main.tile[start.X + offsetX * i, start.Y + offsetY * i].wire(true);
			}
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x00620494 File Offset: 0x0061E694
		private void ActuallyPlaceBoulderTrap(Point position, int yPush, int requiredHeight, int bestType)
		{
			for (int i = position.X; i < position.X + 2; i++)
			{
				for (int j = position.Y - requiredHeight; j <= position.Y + 2; j++)
				{
					Tile tile = Main.tile[i, j];
					if (j < position.Y - requiredHeight + 2)
					{
						tile.ClearTile();
					}
					else if (j <= position.Y)
					{
						bool flag = false;
						do
						{
							if (!tile.active())
							{
								tile.active(true);
								tile.type = (ushort)bestType;
							}
							tile.slope(0);
							tile.halfBrick(false);
							WorldGen.TileFrame(i, j, true, false);
							if (flag)
							{
								break;
							}
							flag = true;
						}
						while (!tile.active());
						tile.wire(true);
						if (Main.tileSolid[(int)tile.type])
						{
							tile.actuator(true);
						}
					}
					else
					{
						tile.ClearTile();
					}
				}
			}
			int num = position.X + 1;
			int num2 = position.Y - requiredHeight + 1;
			int num3 = 3;
			int num4 = num - num3;
			int num5 = num2 - num3;
			int num6 = num + num3 - 1;
			int num7 = num2 + num3 - 1;
			for (int k = num4; k <= num6; k++)
			{
				for (int l = num5; l <= num7; l++)
				{
					Tile tile2 = Main.tile[k, l];
					if (tile2.type >= 0 && !TileID.Sets.Boulders[(int)tile2.type])
					{
						tile2.type = 1;
						if (tile2.wire())
						{
							tile2.actuator(true);
						}
					}
				}
			}
			WorldGen.PlaceTile(num, num2, 138, false, false, -1, 0);
			this.PlaceWireLine(position, 0, 1, yPush);
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x00620638 File Offset: 0x0061E838
		private void ActuallyPlaceExplosive(Point position)
		{
			Tile tile = Main.tile[position.X, position.Y];
			tile.type = 141;
			tile.frameX = (tile.frameY = 0);
			tile.slope(0);
			tile.halfBrick(false);
			WorldGen.TileFrame(position.X, position.Y, true, false);
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x00620698 File Offset: 0x0061E898
		public DeadMansChestBiome()
		{
		}

		// Token: 0x04005B0C RID: 23308
		private List<DeadMansChestBiome.DartTrapPlacementAttempt> _dartTrapPlacementSpots = new List<DeadMansChestBiome.DartTrapPlacementAttempt>();

		// Token: 0x04005B0D RID: 23309
		private List<DeadMansChestBiome.WirePlacementAttempt> _wirePlacementSpots = new List<DeadMansChestBiome.WirePlacementAttempt>();

		// Token: 0x04005B0E RID: 23310
		private List<DeadMansChestBiome.BoulderPlacementAttempt> _boulderPlacementSpots = new List<DeadMansChestBiome.BoulderPlacementAttempt>();

		// Token: 0x04005B0F RID: 23311
		private List<DeadMansChestBiome.ExplosivePlacementAttempt> _explosivePlacementAttempt = new List<DeadMansChestBiome.ExplosivePlacementAttempt>();

		// Token: 0x04005B10 RID: 23312
		[JsonProperty("NumberOfDartTraps")]
		private IntRange _numberOfDartTraps = new IntRange(3, 6);

		// Token: 0x04005B11 RID: 23313
		[JsonProperty("NumberOfBoulderTraps")]
		private IntRange _numberOfBoulderTraps = new IntRange(2, 4);

		// Token: 0x04005B12 RID: 23314
		[JsonProperty("NumberOfStepsBetweenBoulderTraps")]
		private IntRange _numberOfStepsBetweenBoulderTraps = new IntRange(2, 4);

		// Token: 0x02000990 RID: 2448
		private class DartTrapPlacementAttempt
		{
			// Token: 0x0600497F RID: 18815 RVA: 0x006D222A File Offset: 0x006D042A
			public DartTrapPlacementAttempt(Point position, int directionX, int x, int y, int xPush, Tile t)
			{
				this.position = position;
				this.directionX = directionX;
				this.x = x;
				this.y = y;
				this.xPush = xPush;
				this.t = t;
			}

			// Token: 0x04007655 RID: 30293
			public int directionX;

			// Token: 0x04007656 RID: 30294
			public int xPush;

			// Token: 0x04007657 RID: 30295
			public int x;

			// Token: 0x04007658 RID: 30296
			public int y;

			// Token: 0x04007659 RID: 30297
			public Point position;

			// Token: 0x0400765A RID: 30298
			public Tile t;
		}

		// Token: 0x02000991 RID: 2449
		private class BoulderPlacementAttempt
		{
			// Token: 0x06004980 RID: 18816 RVA: 0x006D225F File Offset: 0x006D045F
			public BoulderPlacementAttempt(Point position, int yPush, int requiredHeight, int bestType)
			{
				this.position = position;
				this.yPush = yPush;
				this.requiredHeight = requiredHeight;
				this.bestType = bestType;
			}

			// Token: 0x0400765B RID: 30299
			public Point position;

			// Token: 0x0400765C RID: 30300
			public int yPush;

			// Token: 0x0400765D RID: 30301
			public int requiredHeight;

			// Token: 0x0400765E RID: 30302
			public int bestType;
		}

		// Token: 0x02000992 RID: 2450
		private class WirePlacementAttempt
		{
			// Token: 0x06004981 RID: 18817 RVA: 0x006D2284 File Offset: 0x006D0484
			public WirePlacementAttempt(Point position, int dirX, int dirY, int steps)
			{
				this.position = position;
				this.dirX = dirX;
				this.dirY = dirY;
				this.steps = steps;
			}

			// Token: 0x0400765F RID: 30303
			public Point position;

			// Token: 0x04007660 RID: 30304
			public int dirX;

			// Token: 0x04007661 RID: 30305
			public int dirY;

			// Token: 0x04007662 RID: 30306
			public int steps;
		}

		// Token: 0x02000993 RID: 2451
		private class ExplosivePlacementAttempt
		{
			// Token: 0x06004982 RID: 18818 RVA: 0x006D22A9 File Offset: 0x006D04A9
			public ExplosivePlacementAttempt(Point position)
			{
				this.position = position;
			}

			// Token: 0x04007663 RID: 30307
			public Point position;
		}
	}
}
