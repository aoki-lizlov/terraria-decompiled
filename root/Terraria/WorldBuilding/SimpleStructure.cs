using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000BB RID: 187
	public class SimpleStructure : GenStructure
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x004DE77C File Offset: 0x004DC97C
		public int Width
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x004DE784 File Offset: 0x004DC984
		public int Height
		{
			get
			{
				return this._height;
			}
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x004DE78C File Offset: 0x004DC98C
		public SimpleStructure(params string[] data)
		{
			this.ReadData(data);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x004DE79B File Offset: 0x004DC99B
		public SimpleStructure(string data)
		{
			this.ReadData(data.Split(new char[] { '\n' }));
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x004DE7BC File Offset: 0x004DC9BC
		private void ReadData(string[] lines)
		{
			this._height = lines.Length;
			this._width = lines[0].Length;
			this._data = new int[this._width, this._height];
			for (int i = 0; i < this._height; i++)
			{
				for (int j = 0; j < this._width; j++)
				{
					int num = (int)lines[i][j];
					if (num >= 48 && num <= 57)
					{
						this._data[j, i] = num - 48;
					}
					else
					{
						this._data[j, i] = -1;
					}
				}
			}
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x004DE84E File Offset: 0x004DCA4E
		public SimpleStructure SetActions(params GenAction[] actions)
		{
			this._actions = actions;
			return this;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x004DE858 File Offset: 0x004DCA58
		public SimpleStructure Mirror(bool horizontalMirror, bool verticalMirror)
		{
			this._xMirror = horizontalMirror;
			this._yMirror = verticalMirror;
			return this;
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x004DE86C File Offset: 0x004DCA6C
		public override bool Place(Point origin, StructureMap structures, GenerationProgress progress)
		{
			if (!structures.CanPlace(new Rectangle(origin.X, origin.Y, this._width, this._height), 0))
			{
				return false;
			}
			for (int i = 0; i < this._width; i++)
			{
				for (int j = 0; j < this._height; j++)
				{
					int num = (this._xMirror ? (-i) : i);
					int num2 = (this._yMirror ? (-j) : j);
					if (this._data[i, j] != -1 && !this._actions[this._data[i, j]].Apply(origin, num + origin.X, num2 + origin.Y, new object[0]))
					{
						return false;
					}
				}
			}
			structures.AddProtectedStructure(new Rectangle(origin.X, origin.Y, this._width, this._height), 0);
			return true;
		}

		// Token: 0x04001283 RID: 4739
		private int[,] _data;

		// Token: 0x04001284 RID: 4740
		private int _width;

		// Token: 0x04001285 RID: 4741
		private int _height;

		// Token: 0x04001286 RID: 4742
		private GenAction[] _actions;

		// Token: 0x04001287 RID: 4743
		private bool _xMirror;

		// Token: 0x04001288 RID: 4744
		private bool _yMirror;
	}
}
