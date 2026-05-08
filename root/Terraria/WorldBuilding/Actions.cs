using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Generation.Dungeon;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000A5 RID: 165
	public static class Actions
	{
		// Token: 0x0600173C RID: 5948 RVA: 0x004DDD94 File Offset: 0x004DBF94
		public static GenAction Chain(params GenAction[] actions)
		{
			for (int i = 0; i < actions.Length - 1; i++)
			{
				actions[i].NextAction = actions[i + 1];
			}
			return actions[0];
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x004DDDC1 File Offset: 0x004DBFC1
		public static GenAction Continue(GenAction action)
		{
			return new Actions.ContinueWrapper(action);
		}

		// Token: 0x02000690 RID: 1680
		public class ContinueWrapper : GenAction
		{
			// Token: 0x06003E9E RID: 16030 RVA: 0x0069823F File Offset: 0x0069643F
			public ContinueWrapper(GenAction action)
			{
				this._action = action;
			}

			// Token: 0x06003E9F RID: 16031 RVA: 0x0069824E File Offset: 0x0069644E
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				this._action.Apply(origin, x, y, args);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x0400676C RID: 26476
			private GenAction _action;
		}

		// Token: 0x02000691 RID: 1681
		public class Count : GenAction
		{
			// Token: 0x06003EA0 RID: 16032 RVA: 0x0069826C File Offset: 0x0069646C
			public Count(Ref<int> count)
			{
				this._count = count;
			}

			// Token: 0x06003EA1 RID: 16033 RVA: 0x0069827B File Offset: 0x0069647B
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				this._count.Value++;
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x0400676D RID: 26477
			private Ref<int> _count;
		}

		// Token: 0x02000692 RID: 1682
		public class Scanner : GenAction
		{
			// Token: 0x06003EA2 RID: 16034 RVA: 0x0069829B File Offset: 0x0069649B
			public Scanner(Ref<int> count)
			{
				this._count = count;
			}

			// Token: 0x06003EA3 RID: 16035 RVA: 0x006982AA File Offset: 0x006964AA
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				this._count.Value++;
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x0400676E RID: 26478
			private Ref<int> _count;
		}

		// Token: 0x02000693 RID: 1683
		public class TileScanner : GenAction
		{
			// Token: 0x06003EA4 RID: 16036 RVA: 0x006982CC File Offset: 0x006964CC
			public TileScanner(params ushort[] tiles)
			{
				this._tileIds = tiles;
				this._tileCounts = new Dictionary<ushort, int>();
				for (int i = 0; i < tiles.Length; i++)
				{
					this._tileCounts[this._tileIds[i]] = 0;
				}
			}

			// Token: 0x06003EA5 RID: 16037 RVA: 0x00698314 File Offset: 0x00696514
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Tile tile = GenBase._tiles[x, y];
				if (tile.active() && this._tileCounts.ContainsKey(tile.type))
				{
					Dictionary<ushort, int> tileCounts = this._tileCounts;
					ushort type = tile.type;
					int num = tileCounts[type];
					tileCounts[type] = num + 1;
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x06003EA6 RID: 16038 RVA: 0x00698374 File Offset: 0x00696574
			public Actions.TileScanner Output(Dictionary<ushort, int> resultsOutput)
			{
				this._tileCounts = resultsOutput;
				for (int i = 0; i < this._tileIds.Length; i++)
				{
					if (!this._tileCounts.ContainsKey(this._tileIds[i]))
					{
						this._tileCounts[this._tileIds[i]] = 0;
					}
				}
				return this;
			}

			// Token: 0x06003EA7 RID: 16039 RVA: 0x006983C5 File Offset: 0x006965C5
			public Dictionary<ushort, int> GetResults()
			{
				return this._tileCounts;
			}

			// Token: 0x06003EA8 RID: 16040 RVA: 0x006983CD File Offset: 0x006965CD
			public int GetCount(ushort tileId)
			{
				if (!this._tileCounts.ContainsKey(tileId))
				{
					return -1;
				}
				return this._tileCounts[tileId];
			}

			// Token: 0x0400676F RID: 26479
			private ushort[] _tileIds;

			// Token: 0x04006770 RID: 26480
			private Dictionary<ushort, int> _tileCounts;
		}

		// Token: 0x02000694 RID: 1684
		public class Blank : GenAction
		{
			// Token: 0x06003EA9 RID: 16041 RVA: 0x006983EB File Offset: 0x006965EB
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x06003EAA RID: 16042 RVA: 0x005F59FB File Offset: 0x005F3BFB
			public Blank()
			{
			}
		}

		// Token: 0x02000695 RID: 1685
		public class Custom : GenAction
		{
			// Token: 0x06003EAB RID: 16043 RVA: 0x006983F8 File Offset: 0x006965F8
			public Custom(GenBase.CustomPerUnitAction perUnit)
			{
				this._perUnit = perUnit;
			}

			// Token: 0x06003EAC RID: 16044 RVA: 0x00698407 File Offset: 0x00696607
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				return this._perUnit(x, y, args) | base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006771 RID: 26481
			private GenBase.CustomPerUnitAction _perUnit;
		}

		// Token: 0x02000696 RID: 1686
		public class ClearMetadata : GenAction
		{
			// Token: 0x06003EAD RID: 16045 RVA: 0x00698424 File Offset: 0x00696624
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].ClearMetadata();
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x06003EAE RID: 16046 RVA: 0x005F59FB File Offset: 0x005F3BFB
			public ClearMetadata()
			{
			}
		}

		// Token: 0x02000697 RID: 1687
		public class Clear : GenAction
		{
			// Token: 0x06003EAF RID: 16047 RVA: 0x00698442 File Offset: 0x00696642
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].ClearEverything();
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x06003EB0 RID: 16048 RVA: 0x005F59FB File Offset: 0x005F3BFB
			public Clear()
			{
			}
		}

		// Token: 0x02000698 RID: 1688
		public class ClearTile : GenAction
		{
			// Token: 0x06003EB1 RID: 16049 RVA: 0x00698460 File Offset: 0x00696660
			public ClearTile(bool frameNeighbors = false)
			{
				this._frameNeighbors = frameNeighbors;
			}

			// Token: 0x06003EB2 RID: 16050 RVA: 0x0069846F File Offset: 0x0069666F
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldUtils.ClearTile(x, y, this._frameNeighbors);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006772 RID: 26482
			private bool _frameNeighbors;
		}

		// Token: 0x02000699 RID: 1689
		public class ClearWall : GenAction
		{
			// Token: 0x06003EB3 RID: 16051 RVA: 0x00698489 File Offset: 0x00696689
			public ClearWall(bool frameNeighbors = false)
			{
				this._frameNeighbors = frameNeighbors;
			}

			// Token: 0x06003EB4 RID: 16052 RVA: 0x00698498 File Offset: 0x00696698
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldUtils.ClearWall(x, y, this._frameNeighbors);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006773 RID: 26483
			private bool _frameNeighbors;
		}

		// Token: 0x0200069A RID: 1690
		public class HalfBlock : GenAction
		{
			// Token: 0x06003EB5 RID: 16053 RVA: 0x006984B2 File Offset: 0x006966B2
			public HalfBlock(bool value = true)
			{
				this._value = value;
			}

			// Token: 0x06003EB6 RID: 16054 RVA: 0x006984C1 File Offset: 0x006966C1
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].halfBrick(this._value);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006774 RID: 26484
			private bool _value;
		}

		// Token: 0x0200069B RID: 1691
		public class SetTile : GenAction
		{
			// Token: 0x06003EB7 RID: 16055 RVA: 0x006984E5 File Offset: 0x006966E5
			public SetTile(ushort type, bool setSelfFrames = false, bool setNeighborFrames = true, bool clearTile = true)
			{
				this._type = type;
				this._doFraming = setSelfFrames;
				this._doNeighborFraming = setNeighborFrames;
				this._clearTile = clearTile;
			}

			// Token: 0x06003EB8 RID: 16056 RVA: 0x0069850C File Offset: 0x0069670C
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Tile tile = GenBase._tiles[x, y];
				if (this._clearTile)
				{
					tile.Clear(~(TileDataType.Wiring | TileDataType.Actuator));
				}
				tile.type = this._type;
				tile.active(true);
				if (this._doFraming)
				{
					WorldUtils.TileFrame(x, y, this._doNeighborFraming);
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006775 RID: 26485
			private ushort _type;

			// Token: 0x04006776 RID: 26486
			private bool _doFraming;

			// Token: 0x04006777 RID: 26487
			private bool _doNeighborFraming;

			// Token: 0x04006778 RID: 26488
			private bool _clearTile;
		}

		// Token: 0x0200069C RID: 1692
		public class SetWall : GenAction
		{
			// Token: 0x06003EB9 RID: 16057 RVA: 0x00698569 File Offset: 0x00696769
			public SetWall(ushort type, bool setSelfFrames = false, bool setNeighborFrames = true, bool clearTile = true)
			{
				this._type = type;
				this._doFraming = setSelfFrames;
				this._doNeighborFraming = setNeighborFrames;
				this._clearTile = clearTile;
			}

			// Token: 0x06003EBA RID: 16058 RVA: 0x00698590 File Offset: 0x00696790
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Tile tile = GenBase._tiles[x, y];
				if (this._clearTile)
				{
					tile.Clear(~(TileDataType.Wiring | TileDataType.Actuator));
				}
				tile.wall = this._type;
				if (this._doFraming)
				{
					WorldUtils.WallFrame(x, y, this._doNeighborFraming);
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006779 RID: 26489
			private ushort _type;

			// Token: 0x0400677A RID: 26490
			private bool _doFraming;

			// Token: 0x0400677B RID: 26491
			private bool _doNeighborFraming;

			// Token: 0x0400677C RID: 26492
			private bool _clearTile;
		}

		// Token: 0x0200069D RID: 1693
		public class SetTileKeepWall : GenAction
		{
			// Token: 0x06003EBB RID: 16059 RVA: 0x006985E6 File Offset: 0x006967E6
			public SetTileKeepWall(ushort type, bool setSelfFrames = false, bool setNeighborFrames = true)
			{
				this._type = type;
				this._doFraming = setSelfFrames;
				this._doNeighborFraming = setNeighborFrames;
			}

			// Token: 0x06003EBC RID: 16060 RVA: 0x00698604 File Offset: 0x00696804
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				ushort wall = GenBase._tiles[x, y].wall;
				int num = GenBase._tiles[x, y].wallFrameX();
				int num2 = GenBase._tiles[x, y].wallFrameY();
				GenBase._tiles[x, y].Clear(~(TileDataType.Wiring | TileDataType.Actuator));
				GenBase._tiles[x, y].type = this._type;
				GenBase._tiles[x, y].active(true);
				if (wall > 0)
				{
					GenBase._tiles[x, y].wall = wall;
					GenBase._tiles[x, y].wallFrameX(num);
					GenBase._tiles[x, y].wallFrameY(num2);
				}
				if (this._doFraming)
				{
					WorldUtils.TileFrame(x, y, this._doNeighborFraming);
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x0400677D RID: 26493
			private ushort _type;

			// Token: 0x0400677E RID: 26494
			private bool _doFraming;

			// Token: 0x0400677F RID: 26495
			private bool _doNeighborFraming;
		}

		// Token: 0x0200069E RID: 1694
		public class UpdateBounds : GenAction
		{
			// Token: 0x06003EBD RID: 16061 RVA: 0x006986DD File Offset: 0x006968DD
			public UpdateBounds(DungeonBounds bounds)
			{
				this._bounds = bounds;
			}

			// Token: 0x06003EBE RID: 16062 RVA: 0x006986EC File Offset: 0x006968EC
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				this._bounds.UpdateBounds(x, y);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006780 RID: 26496
			private DungeonBounds _bounds;
		}

		// Token: 0x0200069F RID: 1695
		public class DebugDraw : GenAction
		{
			// Token: 0x06003EBF RID: 16063 RVA: 0x00698706 File Offset: 0x00696906
			public DebugDraw(SpriteBatch spriteBatch, Color color = default(Color))
			{
				this._spriteBatch = spriteBatch;
				this._color = color;
			}

			// Token: 0x06003EC0 RID: 16064 RVA: 0x0069871C File Offset: 0x0069691C
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				this._spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((x << 4) - (int)Main.screenPosition.X, (y << 4) - (int)Main.screenPosition.Y, 16, 16), this._color);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006781 RID: 26497
			private Color _color;

			// Token: 0x04006782 RID: 26498
			private SpriteBatch _spriteBatch;
		}

		// Token: 0x020006A0 RID: 1696
		public class SetSlope : GenAction
		{
			// Token: 0x06003EC1 RID: 16065 RVA: 0x00698776 File Offset: 0x00696976
			public SetSlope(int slope)
			{
				this._slope = slope;
			}

			// Token: 0x06003EC2 RID: 16066 RVA: 0x00698785 File Offset: 0x00696985
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldGen.SlopeTile(x, y, this._slope, false, true);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006783 RID: 26499
			private int _slope;
		}

		// Token: 0x020006A1 RID: 1697
		public class SetHalfTile : GenAction
		{
			// Token: 0x06003EC3 RID: 16067 RVA: 0x006987A2 File Offset: 0x006969A2
			public SetHalfTile(bool halfTile)
			{
				this._halfTile = halfTile;
			}

			// Token: 0x06003EC4 RID: 16068 RVA: 0x006987B1 File Offset: 0x006969B1
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].halfBrick(this._halfTile);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006784 RID: 26500
			private bool _halfTile;
		}

		// Token: 0x020006A2 RID: 1698
		public class SetTilePaint : GenAction
		{
			// Token: 0x06003EC5 RID: 16069 RVA: 0x006987D5 File Offset: 0x006969D5
			public SetTilePaint(byte paintID)
			{
				this.paintID = paintID;
			}

			// Token: 0x06003EC6 RID: 16070 RVA: 0x006987E4 File Offset: 0x006969E4
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (this.paintID == 0)
				{
					return base.Fail();
				}
				GenBase._tiles[x, y].color(this.paintID);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006785 RID: 26501
			private byte paintID;
		}

		// Token: 0x020006A3 RID: 1699
		public class ClearTilePaint : GenAction
		{
			// Token: 0x06003EC7 RID: 16071 RVA: 0x005F59FB File Offset: 0x005F3BFB
			public ClearTilePaint()
			{
			}

			// Token: 0x06003EC8 RID: 16072 RVA: 0x00698817 File Offset: 0x00696A17
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].color(0);
				return base.UnitApply(origin, x, y, args);
			}
		}

		// Token: 0x020006A4 RID: 1700
		public class SetWallPaint : GenAction
		{
			// Token: 0x06003EC9 RID: 16073 RVA: 0x00698836 File Offset: 0x00696A36
			public SetWallPaint(byte paintID)
			{
				this.paintID = paintID;
			}

			// Token: 0x06003ECA RID: 16074 RVA: 0x00698845 File Offset: 0x00696A45
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (this.paintID == 0)
				{
					return base.Fail();
				}
				GenBase._tiles[x, y].wallColor(this.paintID);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006786 RID: 26502
			private byte paintID;
		}

		// Token: 0x020006A5 RID: 1701
		public class ClearWallPaint : GenAction
		{
			// Token: 0x06003ECB RID: 16075 RVA: 0x005F59FB File Offset: 0x005F3BFB
			public ClearWallPaint()
			{
			}

			// Token: 0x06003ECC RID: 16076 RVA: 0x00698878 File Offset: 0x00696A78
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].wallColor(0);
				return base.UnitApply(origin, x, y, args);
			}
		}

		// Token: 0x020006A6 RID: 1702
		public class SetTileAndWallPaint : GenAction
		{
			// Token: 0x06003ECD RID: 16077 RVA: 0x00698897 File Offset: 0x00696A97
			public SetTileAndWallPaint(byte paintID)
			{
				this.paintID = paintID;
			}

			// Token: 0x06003ECE RID: 16078 RVA: 0x006988A8 File Offset: 0x00696AA8
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (this.paintID == 0)
				{
					return base.Fail();
				}
				if (GenBase._tiles[x, y].active())
				{
					GenBase._tiles[x, y].color(this.paintID);
				}
				if (GenBase._tiles[x, y].wall != 0)
				{
					GenBase._tiles[x, y].wallColor(this.paintID);
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006787 RID: 26503
			private byte paintID;
		}

		// Token: 0x020006A7 RID: 1703
		public class ClearTileAndWallPaint : GenAction
		{
			// Token: 0x06003ECF RID: 16079 RVA: 0x005F59FB File Offset: 0x005F3BFB
			public ClearTileAndWallPaint()
			{
			}

			// Token: 0x06003ED0 RID: 16080 RVA: 0x00698923 File Offset: 0x00696B23
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].color(0);
				GenBase._tiles[x, y].wallColor(0);
				return base.UnitApply(origin, x, y, args);
			}
		}

		// Token: 0x020006A8 RID: 1704
		public class SetTileAndWallRainbowPaint : GenAction
		{
			// Token: 0x06003ED1 RID: 16081 RVA: 0x005F59FB File Offset: 0x005F3BFB
			public SetTileAndWallRainbowPaint()
			{
			}

			// Token: 0x06003ED2 RID: 16082 RVA: 0x00698954 File Offset: 0x00696B54
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				byte rainbowPaintIDForPosition = WorldGen.GetRainbowPaintIDForPosition(x, y, false);
				if (GenBase._tiles[x, y].active())
				{
					GenBase._tiles[x, y].color(rainbowPaintIDForPosition);
				}
				if (GenBase._tiles[x, y].wall != 0)
				{
					GenBase._tiles[x, y].wallColor(rainbowPaintIDForPosition);
				}
				return base.UnitApply(origin, x, y, args);
			}
		}

		// Token: 0x020006A9 RID: 1705
		public class PlaceTile : GenAction
		{
			// Token: 0x06003ED3 RID: 16083 RVA: 0x006989BF File Offset: 0x00696BBF
			public PlaceTile(ushort type, int style = 0)
			{
				this._type = type;
				this._style = style;
			}

			// Token: 0x06003ED4 RID: 16084 RVA: 0x006989D5 File Offset: 0x00696BD5
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldGen.PlaceTile(x, y, (int)this._type, true, false, -1, this._style);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006788 RID: 26504
			private ushort _type;

			// Token: 0x04006789 RID: 26505
			private int _style;
		}

		// Token: 0x020006AA RID: 1706
		public class RemoveWall : GenAction
		{
			// Token: 0x06003ED5 RID: 16085 RVA: 0x006989F9 File Offset: 0x00696BF9
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].wall = 0;
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x06003ED6 RID: 16086 RVA: 0x005F59FB File Offset: 0x005F3BFB
			public RemoveWall()
			{
			}
		}

		// Token: 0x020006AB RID: 1707
		public class PlaceWall : GenAction
		{
			// Token: 0x06003ED7 RID: 16087 RVA: 0x00698A18 File Offset: 0x00696C18
			public PlaceWall(ushort type, bool neighbors = true)
			{
				this._type = type;
				this._neighbors = neighbors;
			}

			// Token: 0x06003ED8 RID: 16088 RVA: 0x00698A30 File Offset: 0x00696C30
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].wall = this._type;
				WorldGen.SquareWallFrame(x, y, true);
				if (this._neighbors)
				{
					WorldGen.SquareWallFrame(x + 1, y, true);
					WorldGen.SquareWallFrame(x - 1, y, true);
					WorldGen.SquareWallFrame(x, y - 1, true);
					WorldGen.SquareWallFrame(x, y + 1, true);
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x0400678A RID: 26506
			private ushort _type;

			// Token: 0x0400678B RID: 26507
			private bool _neighbors;
		}

		// Token: 0x020006AC RID: 1708
		public class SetLiquid : GenAction
		{
			// Token: 0x06003ED9 RID: 16089 RVA: 0x00698A97 File Offset: 0x00696C97
			public SetLiquid(int type = 0, byte value = 255)
			{
				this._value = value;
				this._type = type;
			}

			// Token: 0x06003EDA RID: 16090 RVA: 0x00698AAD File Offset: 0x00696CAD
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._tiles[x, y].liquidType(this._type);
				GenBase._tiles[x, y].liquid = this._value;
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x0400678C RID: 26508
			private int _type;

			// Token: 0x0400678D RID: 26509
			private byte _value;
		}

		// Token: 0x020006AD RID: 1709
		public class SwapSolidTile : GenAction
		{
			// Token: 0x06003EDB RID: 16091 RVA: 0x00698AE8 File Offset: 0x00696CE8
			public SwapSolidTile(ushort type)
			{
				this._type = type;
			}

			// Token: 0x06003EDC RID: 16092 RVA: 0x00698AF8 File Offset: 0x00696CF8
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Tile tile = GenBase._tiles[x, y];
				if (WorldGen.SolidTile(tile))
				{
					tile.ResetToType(this._type);
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x0400678E RID: 26510
			private ushort _type;
		}

		// Token: 0x020006AE RID: 1710
		public class SetFrames : GenAction
		{
			// Token: 0x06003EDD RID: 16093 RVA: 0x00698B38 File Offset: 0x00696D38
			public SetFrames(bool frameNeighbors = false)
			{
				this._frameNeighbors = frameNeighbors;
			}

			// Token: 0x06003EDE RID: 16094 RVA: 0x00698B47 File Offset: 0x00696D47
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				WorldUtils.TileFrame(x, y, this._frameNeighbors);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x0400678F RID: 26511
			private bool _frameNeighbors;
		}

		// Token: 0x020006AF RID: 1711
		public class Smooth : GenAction
		{
			// Token: 0x06003EDF RID: 16095 RVA: 0x00698B61 File Offset: 0x00696D61
			public Smooth(bool applyToNeighbors = false)
			{
				this._applyToNeighbors = applyToNeighbors;
			}

			// Token: 0x06003EE0 RID: 16096 RVA: 0x00698B70 File Offset: 0x00696D70
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Tile.SmoothSlope(x, y, this._applyToNeighbors, false);
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x04006790 RID: 26512
			private bool _applyToNeighbors;
		}
	}
}
