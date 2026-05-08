using System;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000AA RID: 170
	public static class Modifiers
	{
		// Token: 0x020006BA RID: 1722
		public class ShapeScale : GenAction
		{
			// Token: 0x06003EF5 RID: 16117 RVA: 0x00698D04 File Offset: 0x00696F04
			public ShapeScale(int scale)
			{
				this._scale = scale;
			}

			// Token: 0x06003EF6 RID: 16118 RVA: 0x00698D14 File Offset: 0x00696F14
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				bool flag = false;
				for (int i = 0; i < this._scale; i++)
				{
					for (int j = 0; j < this._scale; j++)
					{
						flag |= !base.UnitApply(origin, (x - origin.X << 1) + i + origin.X, (y - origin.Y << 1) + j + origin.Y, new object[0]);
					}
				}
				return !flag;
			}

			// Token: 0x04006798 RID: 26520
			private int _scale;
		}

		// Token: 0x020006BB RID: 1723
		public class Expand : GenAction
		{
			// Token: 0x06003EF7 RID: 16119 RVA: 0x00698D82 File Offset: 0x00696F82
			public Expand(int expansion)
			{
				this._xExpansion = expansion;
				this._yExpansion = expansion;
			}

			// Token: 0x06003EF8 RID: 16120 RVA: 0x00698D98 File Offset: 0x00696F98
			public Expand(int xExpansion, int yExpansion)
			{
				this._xExpansion = xExpansion;
				this._yExpansion = yExpansion;
			}

			// Token: 0x06003EF9 RID: 16121 RVA: 0x00698DB0 File Offset: 0x00696FB0
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				bool flag = false;
				for (int i = -this._xExpansion; i <= this._xExpansion; i++)
				{
					for (int j = -this._yExpansion; j <= this._yExpansion; j++)
					{
						flag |= !base.UnitApply(origin, x + i, y + j, args);
					}
				}
				return !flag;
			}

			// Token: 0x04006799 RID: 26521
			private int _xExpansion;

			// Token: 0x0400679A RID: 26522
			private int _yExpansion;
		}

		// Token: 0x020006BC RID: 1724
		public class RadialDither : GenAction
		{
			// Token: 0x06003EFA RID: 16122 RVA: 0x00698E06 File Offset: 0x00697006
			public RadialDither(double innerRadius, double outerRadius)
			{
				this._innerRadius = innerRadius;
				this._outerRadius = outerRadius;
			}

			// Token: 0x06003EFB RID: 16123 RVA: 0x00698E1C File Offset: 0x0069701C
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Vector2D vector2D;
				vector2D..ctor((double)origin.X, (double)origin.Y);
				double num = Vector2D.Distance(new Vector2D((double)x, (double)y), vector2D);
				double num2 = Math.Max(0.0, Math.Min(1.0, (num - this._innerRadius) / (this._outerRadius - this._innerRadius)));
				if (GenBase._random.NextDouble() > num2)
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x0400679B RID: 26523
			private double _innerRadius;

			// Token: 0x0400679C RID: 26524
			private double _outerRadius;
		}

		// Token: 0x020006BD RID: 1725
		public class Blotches : GenAction
		{
			// Token: 0x06003EFC RID: 16124 RVA: 0x00698EA0 File Offset: 0x006970A0
			public Blotches(int scale = 2, double chance = 0.3)
			{
				this._minX = scale;
				this._minY = scale;
				this._maxX = scale;
				this._maxY = scale;
				this._chance = chance;
			}

			// Token: 0x06003EFD RID: 16125 RVA: 0x00698ECB File Offset: 0x006970CB
			public Blotches(int xScale, int yScale, double chance = 0.3)
			{
				this._minX = xScale;
				this._maxX = xScale;
				this._minY = yScale;
				this._maxY = yScale;
				this._chance = chance;
			}

			// Token: 0x06003EFE RID: 16126 RVA: 0x00698EF6 File Offset: 0x006970F6
			public Blotches(int leftScale, int upScale, int rightScale, int downScale, double chance = 0.3)
			{
				this._minX = leftScale;
				this._maxX = rightScale;
				this._minY = upScale;
				this._maxY = downScale;
				this._chance = chance;
			}

			// Token: 0x06003EFF RID: 16127 RVA: 0x00698F24 File Offset: 0x00697124
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				GenBase._random.NextDouble();
				if (GenBase._random.NextDouble() < this._chance)
				{
					bool flag = false;
					int num = GenBase._random.Next(1 - this._minX, 1);
					int num2 = GenBase._random.Next(0, this._maxX);
					int num3 = GenBase._random.Next(1 - this._minY, 1);
					int num4 = GenBase._random.Next(0, this._maxY);
					for (int i = num; i <= num2; i++)
					{
						for (int j = num3; j <= num4; j++)
						{
							flag |= !base.UnitApply(origin, x + i, y + j, args);
						}
					}
					return !flag;
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x0400679D RID: 26525
			private int _minX;

			// Token: 0x0400679E RID: 26526
			private int _minY;

			// Token: 0x0400679F RID: 26527
			private int _maxX;

			// Token: 0x040067A0 RID: 26528
			private int _maxY;

			// Token: 0x040067A1 RID: 26529
			private double _chance;
		}

		// Token: 0x020006BE RID: 1726
		public class InShape : GenAction
		{
			// Token: 0x06003F00 RID: 16128 RVA: 0x00698FE4 File Offset: 0x006971E4
			public InShape(ShapeData shapeData)
			{
				this._shapeData = shapeData;
			}

			// Token: 0x06003F01 RID: 16129 RVA: 0x00698FF3 File Offset: 0x006971F3
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!this._shapeData.Contains(x - origin.X, y - origin.Y))
				{
					return base.Fail();
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040067A2 RID: 26530
			private readonly ShapeData _shapeData;
		}

		// Token: 0x020006BF RID: 1727
		public class NotInShape : GenAction
		{
			// Token: 0x06003F02 RID: 16130 RVA: 0x00699024 File Offset: 0x00697224
			public NotInShape(ShapeData shapeData)
			{
				this._shapeData = shapeData;
			}

			// Token: 0x06003F03 RID: 16131 RVA: 0x00699033 File Offset: 0x00697233
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (this._shapeData.Contains(x - origin.X, y - origin.Y))
				{
					return base.Fail();
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040067A3 RID: 26531
			private readonly ShapeData _shapeData;
		}

		// Token: 0x020006C0 RID: 1728
		public class Conditions : GenAction
		{
			// Token: 0x06003F04 RID: 16132 RVA: 0x00699064 File Offset: 0x00697264
			public Conditions(params GenCondition[] conditions)
			{
				this._conditions = conditions;
			}

			// Token: 0x06003F05 RID: 16133 RVA: 0x00699074 File Offset: 0x00697274
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				bool flag = true;
				for (int i = 0; i < this._conditions.Length; i++)
				{
					flag &= this._conditions[i].IsValid(x, y);
				}
				if (flag)
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040067A4 RID: 26532
			private readonly GenCondition[] _conditions;
		}

		// Token: 0x020006C1 RID: 1729
		public class OnlyWalls : GenAction
		{
			// Token: 0x06003F06 RID: 16134 RVA: 0x006990BD File Offset: 0x006972BD
			public OnlyWalls(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x06003F07 RID: 16135 RVA: 0x006990CC File Offset: 0x006972CC
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				for (int i = 0; i < this._types.Length; i++)
				{
					if (GenBase._tiles[x, y].wall == this._types[i])
					{
						return base.UnitApply(origin, x, y, args);
					}
				}
				return base.Fail();
			}

			// Token: 0x040067A5 RID: 26533
			private ushort[] _types;
		}

		// Token: 0x020006C2 RID: 1730
		public class OnlyTiles : GenAction
		{
			// Token: 0x06003F08 RID: 16136 RVA: 0x00699119 File Offset: 0x00697319
			public OnlyTiles(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x06003F09 RID: 16137 RVA: 0x00699128 File Offset: 0x00697328
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!GenBase._tiles[x, y].active())
				{
					return base.Fail();
				}
				for (int i = 0; i < this._types.Length; i++)
				{
					if (GenBase._tiles[x, y].type == this._types[i])
					{
						return base.UnitApply(origin, x, y, args);
					}
				}
				return base.Fail();
			}

			// Token: 0x040067A6 RID: 26534
			private ushort[] _types;
		}

		// Token: 0x020006C3 RID: 1731
		public class Checkerboard : GenAction
		{
			// Token: 0x06003F0A RID: 16138 RVA: 0x0069918F File Offset: 0x0069738F
			public Checkerboard(int percentile)
			{
				this._percentile = percentile;
			}

			// Token: 0x06003F0B RID: 16139 RVA: 0x0069919E File Offset: 0x0069739E
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (x % this._percentile == 0 && y % this._percentile == 0)
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040067A7 RID: 26535
			private int _percentile;
		}

		// Token: 0x020006C4 RID: 1732
		public class IsTouching : GenAction
		{
			// Token: 0x06003F0C RID: 16140 RVA: 0x006991C6 File Offset: 0x006973C6
			public IsTouching(bool useDiagonals, params ushort[] tileIds)
			{
				this._useDiagonals = useDiagonals;
				this._tileIds = tileIds;
			}

			// Token: 0x06003F0D RID: 16141 RVA: 0x006991DC File Offset: 0x006973DC
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				int num = (this._useDiagonals ? 16 : 8);
				for (int i = 0; i < num; i += 2)
				{
					Tile tile = GenBase._tiles[x + Modifiers.IsTouching.DIRECTIONS[i], y + Modifiers.IsTouching.DIRECTIONS[i + 1]];
					if (tile.active())
					{
						for (int j = 0; j < this._tileIds.Length; j++)
						{
							if (tile.type == this._tileIds[j])
							{
								return base.UnitApply(origin, x, y, args);
							}
						}
					}
				}
				return base.Fail();
			}

			// Token: 0x06003F0E RID: 16142 RVA: 0x0069925F File Offset: 0x0069745F
			// Note: this type is marked as 'beforefieldinit'.
			static IsTouching()
			{
			}

			// Token: 0x040067A8 RID: 26536
			private static readonly int[] DIRECTIONS = new int[]
			{
				0, -1, 1, 0, -1, 0, 0, 1, -1, -1,
				1, -1, -1, 1, 1, 1
			};

			// Token: 0x040067A9 RID: 26537
			private bool _useDiagonals;

			// Token: 0x040067AA RID: 26538
			private ushort[] _tileIds;
		}

		// Token: 0x020006C5 RID: 1733
		public class NotTouching : GenAction
		{
			// Token: 0x06003F0F RID: 16143 RVA: 0x00699278 File Offset: 0x00697478
			public NotTouching(bool useDiagonals, params ushort[] tileIds)
			{
				this._useDiagonals = useDiagonals;
				this._tileIds = tileIds;
			}

			// Token: 0x06003F10 RID: 16144 RVA: 0x00699290 File Offset: 0x00697490
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				int num = (this._useDiagonals ? 16 : 8);
				for (int i = 0; i < num; i += 2)
				{
					Tile tile = GenBase._tiles[x + Modifiers.NotTouching.DIRECTIONS[i], y + Modifiers.NotTouching.DIRECTIONS[i + 1]];
					if (tile.active())
					{
						for (int j = 0; j < this._tileIds.Length; j++)
						{
							if (tile.type == this._tileIds[j])
							{
								return base.Fail();
							}
						}
					}
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x06003F11 RID: 16145 RVA: 0x00699313 File Offset: 0x00697513
			// Note: this type is marked as 'beforefieldinit'.
			static NotTouching()
			{
			}

			// Token: 0x040067AB RID: 26539
			private static readonly int[] DIRECTIONS = new int[]
			{
				0, -1, 1, 0, -1, 0, 0, 1, -1, -1,
				1, -1, -1, 1, 1, 1
			};

			// Token: 0x040067AC RID: 26540
			private bool _useDiagonals;

			// Token: 0x040067AD RID: 26541
			private ushort[] _tileIds;
		}

		// Token: 0x020006C6 RID: 1734
		public class IsTouchingAir : GenAction
		{
			// Token: 0x06003F12 RID: 16146 RVA: 0x0069932C File Offset: 0x0069752C
			public IsTouchingAir(bool useDiagonals = false)
			{
				this._useDiagonals = useDiagonals;
			}

			// Token: 0x06003F13 RID: 16147 RVA: 0x0069933C File Offset: 0x0069753C
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				int num = (this._useDiagonals ? 16 : 8);
				for (int i = 0; i < num; i += 2)
				{
					if (!GenBase._tiles[x + Modifiers.IsTouchingAir.DIRECTIONS[i], y + Modifiers.IsTouchingAir.DIRECTIONS[i + 1]].active())
					{
						return base.UnitApply(origin, x, y, args);
					}
				}
				return base.Fail();
			}

			// Token: 0x06003F14 RID: 16148 RVA: 0x0069939A File Offset: 0x0069759A
			// Note: this type is marked as 'beforefieldinit'.
			static IsTouchingAir()
			{
			}

			// Token: 0x040067AE RID: 26542
			private static readonly int[] DIRECTIONS = new int[]
			{
				0, -1, 1, 0, -1, 0, 0, 1, -1, -1,
				1, -1, -1, 1, 1, 1
			};

			// Token: 0x040067AF RID: 26543
			private bool _useDiagonals;
		}

		// Token: 0x020006C7 RID: 1735
		public class SkipTiles : GenAction
		{
			// Token: 0x06003F15 RID: 16149 RVA: 0x006993B3 File Offset: 0x006975B3
			public SkipTiles(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x06003F16 RID: 16150 RVA: 0x006993C4 File Offset: 0x006975C4
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!GenBase._tiles[x, y].active())
				{
					return base.UnitApply(origin, x, y, args);
				}
				for (int i = 0; i < this._types.Length; i++)
				{
					if (GenBase._tiles[x, y].type == this._types[i])
					{
						return base.Fail();
					}
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040067B0 RID: 26544
			private ushort[] _types;
		}

		// Token: 0x020006C8 RID: 1736
		public class HasLiquid : GenAction
		{
			// Token: 0x06003F17 RID: 16151 RVA: 0x00699430 File Offset: 0x00697630
			public HasLiquid(int liquidLevel = -1, int liquidType = -1)
			{
				this._liquidType = liquidType;
				this._liquidLevel = liquidLevel;
			}

			// Token: 0x06003F18 RID: 16152 RVA: 0x00699448 File Offset: 0x00697648
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Tile tile = GenBase._tiles[x, y];
				if ((this._liquidType == -1 || this._liquidType == (int)tile.liquidType()) && ((this._liquidLevel == -1 && tile.liquid != 0) || this._liquidLevel == (int)tile.liquid))
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040067B1 RID: 26545
			private int _liquidType;

			// Token: 0x040067B2 RID: 26546
			private int _liquidLevel;
		}

		// Token: 0x020006C9 RID: 1737
		public class NoLiquid : GenAction
		{
			// Token: 0x06003F19 RID: 16153 RVA: 0x006994AA File Offset: 0x006976AA
			public NoLiquid(int liquidType = -1)
			{
				this._liquidType = liquidType;
			}

			// Token: 0x06003F1A RID: 16154 RVA: 0x006994BC File Offset: 0x006976BC
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				Tile tile = GenBase._tiles[x, y];
				if (tile.liquid > 0 && (this._liquidType == -1 || this._liquidType == (int)tile.liquidType()))
				{
					return base.Fail();
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040067B3 RID: 26547
			private int _liquidType;
		}

		// Token: 0x020006CA RID: 1738
		public class SkipWalls : GenAction
		{
			// Token: 0x06003F1B RID: 16155 RVA: 0x00699508 File Offset: 0x00697708
			public SkipWalls(params ushort[] types)
			{
				this._types = types;
			}

			// Token: 0x06003F1C RID: 16156 RVA: 0x00699518 File Offset: 0x00697718
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				for (int i = 0; i < this._types.Length; i++)
				{
					if (GenBase._tiles[x, y].wall == this._types[i])
					{
						return base.Fail();
					}
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040067B4 RID: 26548
			private ushort[] _types;
		}

		// Token: 0x020006CB RID: 1739
		public class SkipUnbreakableWalledTiles : GenAction
		{
			// Token: 0x06003F1D RID: 16157 RVA: 0x00699565 File Offset: 0x00697765
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (GenBase._tiles[x, y].active() && GenBase._tiles[x, y].wall == 350)
				{
					return base.Fail();
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x06003F1E RID: 16158 RVA: 0x005F59FB File Offset: 0x005F3BFB
			public SkipUnbreakableWalledTiles()
			{
			}
		}

		// Token: 0x020006CC RID: 1740
		public class IsAboveHeight : GenAction
		{
			// Token: 0x06003F1F RID: 16159 RVA: 0x006995A4 File Offset: 0x006977A4
			public IsAboveHeight(int y, bool inclusive = false)
			{
				this._y = y;
				this._inclusive = inclusive;
			}

			// Token: 0x06003F20 RID: 16160 RVA: 0x006995BA File Offset: 0x006977BA
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (this._inclusive ? (y <= this._y) : (y < this._y))
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040067B5 RID: 26549
			private int _y;

			// Token: 0x040067B6 RID: 26550
			private bool _inclusive;
		}

		// Token: 0x020006CD RID: 1741
		public class IsBelowHeight : GenAction
		{
			// Token: 0x06003F21 RID: 16161 RVA: 0x006995EF File Offset: 0x006977EF
			public IsBelowHeight(int y, bool inclusive = false)
			{
				this._y = y;
				this._inclusive = inclusive;
			}

			// Token: 0x06003F22 RID: 16162 RVA: 0x00699605 File Offset: 0x00697805
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (this._inclusive ? (y >= this._y) : (y > this._y))
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040067B7 RID: 26551
			private int _y;

			// Token: 0x040067B8 RID: 26552
			private bool _inclusive;
		}

		// Token: 0x020006CE RID: 1742
		public class IsEmpty : GenAction
		{
			// Token: 0x06003F23 RID: 16163 RVA: 0x0069963A File Offset: 0x0069783A
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!GenBase._tiles[x, y].active())
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x06003F24 RID: 16164 RVA: 0x005F59FB File Offset: 0x005F3BFB
			public IsEmpty()
			{
			}
		}

		// Token: 0x020006CF RID: 1743
		public class IsSolid : GenAction
		{
			// Token: 0x06003F25 RID: 16165 RVA: 0x00699661 File Offset: 0x00697861
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (GenBase._tiles[x, y].active() && WorldGen.SolidOrSlopedTile(x, y))
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x06003F26 RID: 16166 RVA: 0x005F59FB File Offset: 0x005F3BFB
			public IsSolid()
			{
			}
		}

		// Token: 0x020006D0 RID: 1744
		public class IsNotSolid : GenAction
		{
			// Token: 0x06003F27 RID: 16167 RVA: 0x00699691 File Offset: 0x00697891
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (!GenBase._tiles[x, y].active() || !WorldGen.SolidOrSlopedTile(x, y))
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x06003F28 RID: 16168 RVA: 0x005F59FB File Offset: 0x005F3BFB
			public IsNotSolid()
			{
			}
		}

		// Token: 0x020006D1 RID: 1745
		public class RectangleMask : GenAction
		{
			// Token: 0x06003F29 RID: 16169 RVA: 0x006996C1 File Offset: 0x006978C1
			public RectangleMask(int xMin, int xMax, int yMin, int yMax)
			{
				this._xMin = xMin;
				this._yMin = yMin;
				this._xMax = xMax;
				this._yMax = yMax;
			}

			// Token: 0x06003F2A RID: 16170 RVA: 0x006996E8 File Offset: 0x006978E8
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (x >= this._xMin + origin.X && x <= this._xMax + origin.X && y >= this._yMin + origin.Y && y <= this._yMax + origin.Y)
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040067B9 RID: 26553
			private int _xMin;

			// Token: 0x040067BA RID: 26554
			private int _yMin;

			// Token: 0x040067BB RID: 26555
			private int _xMax;

			// Token: 0x040067BC RID: 26556
			private int _yMax;
		}

		// Token: 0x020006D2 RID: 1746
		public class Offset : GenAction
		{
			// Token: 0x06003F2B RID: 16171 RVA: 0x00699747 File Offset: 0x00697947
			public Offset(int x, int y)
			{
				this._xOffset = x;
				this._yOffset = y;
			}

			// Token: 0x06003F2C RID: 16172 RVA: 0x0069975D File Offset: 0x0069795D
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				return base.UnitApply(origin, x + this._xOffset, y + this._yOffset, args);
			}

			// Token: 0x040067BD RID: 26557
			private int _xOffset;

			// Token: 0x040067BE RID: 26558
			private int _yOffset;
		}

		// Token: 0x020006D3 RID: 1747
		public class Dither : GenAction
		{
			// Token: 0x06003F2D RID: 16173 RVA: 0x00699778 File Offset: 0x00697978
			public Dither(double failureChance = 0.5)
			{
				this._failureChance = failureChance;
			}

			// Token: 0x06003F2E RID: 16174 RVA: 0x00699787 File Offset: 0x00697987
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (GenBase._random.NextDouble() >= this._failureChance)
				{
					return base.UnitApply(origin, x, y, args);
				}
				return base.Fail();
			}

			// Token: 0x040067BF RID: 26559
			private double _failureChance;
		}

		// Token: 0x020006D4 RID: 1748
		public class Flip : GenAction
		{
			// Token: 0x06003F2F RID: 16175 RVA: 0x006997AD File Offset: 0x006979AD
			public Flip(bool flipX, bool flipY)
			{
				this._flipX = flipX;
				this._flipY = flipY;
			}

			// Token: 0x06003F30 RID: 16176 RVA: 0x006997C3 File Offset: 0x006979C3
			public override bool Apply(Point origin, int x, int y, params object[] args)
			{
				if (this._flipX)
				{
					x = origin.X * 2 - x;
				}
				if (this._flipY)
				{
					y = origin.Y * 2 - y;
				}
				return base.UnitApply(origin, x, y, args);
			}

			// Token: 0x040067C0 RID: 26560
			private bool _flipX;

			// Token: 0x040067C1 RID: 26561
			private bool _flipY;
		}
	}
}
