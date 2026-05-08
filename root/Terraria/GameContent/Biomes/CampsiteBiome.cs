using System;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x0200050C RID: 1292
	public class CampsiteBiome : MicroBiome
	{
		// Token: 0x06003644 RID: 13892 RVA: 0x006245F8 File Offset: 0x006227F8
		public override bool Place(Point origin, StructureMap structures, GenerationProgress progress)
		{
			Ref<int> @ref = new Ref<int>(0);
			Ref<int> ref2 = new Ref<int>(0);
			WorldUtils.Gen(origin, new Shapes.Circle(10), Actions.Chain(new GenAction[]
			{
				new Actions.Scanner(ref2),
				new Modifiers.IsSolid(),
				new Actions.Scanner(@ref)
			}));
			if (@ref.Value < ref2.Value - 5)
			{
				return false;
			}
			int num = GenBase._random.Next(6, 10);
			int num2 = GenBase._random.Next(1, 5);
			if (!structures.CanPlace(new Rectangle(origin.X - num, origin.Y - num, num * 2, num * 2), 0))
			{
				return false;
			}
			int num3 = num + 3;
			for (int i = origin.X - num3; i <= origin.X + num3; i++)
			{
				for (int j = origin.Y - num3; j <= origin.Y + num3; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile.active() && (Main.tileDungeon[(int)tile.type] || TileID.Sets.IsAContainer[(int)tile.type] || tile.type == 226 || tile.type == 237))
					{
						return false;
					}
				}
			}
			ushort num4 = (ushort)((byte)(196 + WorldGen.genRand.Next(4)));
			for (int k = origin.X - num; k <= origin.X + num; k++)
			{
				for (int l = origin.Y - num; l <= origin.Y + num; l++)
				{
					if (Main.tile[k, l].active())
					{
						int type = (int)Main.tile[k, l].type;
						if (type == 53 || type == 396 || type == 397 || type == 404)
						{
							num4 = 171;
						}
						if (type == 161 || type == 147)
						{
							num4 = 40;
						}
						if (type == 60)
						{
							num4 = (ushort)((byte)(204 + WorldGen.genRand.Next(4)));
						}
						if (type == 367)
						{
							num4 = 178;
						}
						if (type == 368)
						{
							num4 = 180;
						}
					}
				}
			}
			ShapeData shapeData = new ShapeData();
			WorldUtils.Gen(origin, new Shapes.Slime(num), Actions.Chain(new GenAction[]
			{
				new Modifiers.Blotches(num2, num2, num2, 1, 1.0).Output(shapeData),
				new Modifiers.Offset(0, -2),
				new Modifiers.OnlyTiles(new ushort[] { 53 }),
				new Actions.SetTile(397, true, true, true),
				new Modifiers.OnlyWalls(new ushort[1]),
				new Actions.PlaceWall(num4, true)
			}));
			WorldUtils.Gen(origin, new ModShapes.All(shapeData), Actions.Chain(new GenAction[]
			{
				new Actions.ClearTile(false),
				new Actions.SetLiquid(0, 0),
				new Actions.SetFrames(true),
				new Modifiers.OnlyWalls(new ushort[1]),
				new Actions.PlaceWall(num4, true)
			}));
			Point point;
			if (!WorldUtils.Find(origin, Searches.Chain(new Searches.Down(10), new GenCondition[]
			{
				new Conditions.IsSolid()
			}), out point))
			{
				return false;
			}
			int num5 = point.Y - 1;
			bool flag = GenBase._random.Next() % 2 == 0;
			if (GenBase._random.Next() % 10 != 0)
			{
				int num6 = GenBase._random.Next(1, 4);
				int num7 = (flag ? 4 : (-(num >> 1)));
				for (int m = 0; m < num6; m++)
				{
					int num8 = GenBase._random.Next(1, 3);
					for (int n = 0; n < num8; n++)
					{
						WorldGen.PlaceTile(origin.X + num7 - m, num5 - n, 332, true, false, -1, 0);
					}
				}
			}
			int num9 = (num - 3) * (flag ? (-1) : 1);
			if (GenBase._random.Next() % 10 != 0)
			{
				WorldGen.PlaceTile(origin.X + num9, num5, 186, false, false, -1, 0);
			}
			if (GenBase._random.Next() % 10 != 0)
			{
				if (WorldGen.SecretSeed.rainbowStuff.Enabled)
				{
					WorldGen.PlaceTile(origin.X, num5, 215, true, false, -1, 5);
				}
				else
				{
					WorldGen.PlaceTile(origin.X, num5, 215, true, false, -1, 0);
				}
				if (GenBase._tiles[origin.X, num5].active() && GenBase._tiles[origin.X, num5].type == 215)
				{
					Tile tile2 = GenBase._tiles[origin.X, num5];
					tile2.frameY += 36;
					Tile tile3 = GenBase._tiles[origin.X - 1, num5];
					tile3.frameY += 36;
					Tile tile4 = GenBase._tiles[origin.X + 1, num5];
					tile4.frameY += 36;
					Tile tile5 = GenBase._tiles[origin.X, num5 - 1];
					tile5.frameY += 36;
					Tile tile6 = GenBase._tiles[origin.X - 1, num5 - 1];
					tile6.frameY += 36;
					Tile tile7 = GenBase._tiles[origin.X + 1, num5 - 1];
					tile7.frameY += 36;
				}
			}
			structures.AddProtectedStructure(new Rectangle(origin.X - num, origin.Y - num, num * 2, num * 2), 4);
			return true;
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x0061F6EC File Offset: 0x0061D8EC
		public CampsiteBiome()
		{
		}
	}
}
