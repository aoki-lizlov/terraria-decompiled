using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000AC RID: 172
	public static class ModShapes
	{
		// Token: 0x020006D5 RID: 1749
		public class All : GenModShape
		{
			// Token: 0x06003F31 RID: 16177 RVA: 0x006997F8 File Offset: 0x006979F8
			public All(ShapeData data)
				: base(data)
			{
			}

			// Token: 0x06003F32 RID: 16178 RVA: 0x00699804 File Offset: 0x00697A04
			public override bool Perform(Point origin, GenAction action)
			{
				foreach (Point16 point in this._data.GetData())
				{
					if (!base.UnitApply(action, origin, (int)point.X + origin.X, (int)point.Y + origin.Y, new object[0]) && this._quitOnFail)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x020006D6 RID: 1750
		public class OuterOutline : GenModShape
		{
			// Token: 0x06003F33 RID: 16179 RVA: 0x00699890 File Offset: 0x00697A90
			public OuterOutline(ShapeData data, bool useDiagonals = true, bool useInterior = false)
				: base(data)
			{
				this._useDiagonals = useDiagonals;
				this._useInterior = useInterior;
			}

			// Token: 0x06003F34 RID: 16180 RVA: 0x006998A8 File Offset: 0x00697AA8
			public override bool Perform(Point origin, GenAction action)
			{
				int num = (this._useDiagonals ? 16 : 8);
				foreach (Point16 point in this._data.GetData())
				{
					if (this._useInterior && !base.UnitApply(action, origin, (int)point.X + origin.X, (int)point.Y + origin.Y, new object[0]) && this._quitOnFail)
					{
						return false;
					}
					for (int i = 0; i < num; i += 2)
					{
						if (!this._data.Contains((int)point.X + ModShapes.OuterOutline.POINT_OFFSETS[i], (int)point.Y + ModShapes.OuterOutline.POINT_OFFSETS[i + 1]) && !base.UnitApply(action, origin, origin.X + (int)point.X + ModShapes.OuterOutline.POINT_OFFSETS[i], origin.Y + (int)point.Y + ModShapes.OuterOutline.POINT_OFFSETS[i + 1], new object[0]) && this._quitOnFail)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x06003F35 RID: 16181 RVA: 0x006999DC File Offset: 0x00697BDC
			// Note: this type is marked as 'beforefieldinit'.
			static OuterOutline()
			{
			}

			// Token: 0x040067C2 RID: 26562
			private static readonly int[] POINT_OFFSETS = new int[]
			{
				1, 0, -1, 0, 0, 1, 0, -1, 1, 1,
				1, -1, -1, 1, -1, -1
			};

			// Token: 0x040067C3 RID: 26563
			private bool _useDiagonals;

			// Token: 0x040067C4 RID: 26564
			private bool _useInterior;
		}

		// Token: 0x020006D7 RID: 1751
		public class InnerOutline : GenModShape
		{
			// Token: 0x06003F36 RID: 16182 RVA: 0x006999F5 File Offset: 0x00697BF5
			public InnerOutline(ShapeData data, bool useDiagonals = true)
				: base(data)
			{
				this._useDiagonals = useDiagonals;
			}

			// Token: 0x06003F37 RID: 16183 RVA: 0x00699A08 File Offset: 0x00697C08
			public override bool Perform(Point origin, GenAction action)
			{
				int num = (this._useDiagonals ? 16 : 8);
				foreach (Point16 point in this._data.GetData())
				{
					bool flag = false;
					for (int i = 0; i < num; i += 2)
					{
						if (!this._data.Contains((int)point.X + ModShapes.InnerOutline.POINT_OFFSETS[i], (int)point.Y + ModShapes.InnerOutline.POINT_OFFSETS[i + 1]))
						{
							flag = true;
							break;
						}
					}
					if (flag && !base.UnitApply(action, origin, (int)point.X + origin.X, (int)point.Y + origin.Y, new object[0]) && this._quitOnFail)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06003F38 RID: 16184 RVA: 0x00699AF0 File Offset: 0x00697CF0
			// Note: this type is marked as 'beforefieldinit'.
			static InnerOutline()
			{
			}

			// Token: 0x040067C5 RID: 26565
			private static readonly int[] POINT_OFFSETS = new int[]
			{
				1, 0, -1, 0, 0, 1, 0, -1, 1, 1,
				1, -1, -1, 1, -1, -1
			};

			// Token: 0x040067C6 RID: 26566
			private bool _useDiagonals;
		}
	}
}
