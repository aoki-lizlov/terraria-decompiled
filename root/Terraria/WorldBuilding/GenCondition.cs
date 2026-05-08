using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000A7 RID: 167
	public abstract class GenCondition : GenBase
	{
		// Token: 0x06001743 RID: 5955 RVA: 0x004DDDE8 File Offset: 0x004DBFE8
		public bool IsValid(int x, int y)
		{
			switch (this._areaType)
			{
			case GenCondition.AreaType.And:
			{
				for (int i = x; i < x + this._width; i++)
				{
					for (int j = y; j < y + this._height; j++)
					{
						if (!this.CheckValidity(i, j))
						{
							return this.InvertResults;
						}
					}
				}
				return !this.InvertResults;
			}
			case GenCondition.AreaType.Or:
			{
				for (int k = x; k < x + this._width; k++)
				{
					for (int l = y; l < y + this._height; l++)
					{
						if (this.CheckValidity(k, l))
						{
							return !this.InvertResults;
						}
					}
				}
				return this.InvertResults;
			}
			case GenCondition.AreaType.None:
				return this.CheckValidity(x, y) ^ this.InvertResults;
			default:
				return true;
			}
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x004DDEAB File Offset: 0x004DC0AB
		public GenCondition Not()
		{
			this.InvertResults = !this.InvertResults;
			return this;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x004DDEBD File Offset: 0x004DC0BD
		public GenCondition AreaOr(int width, int height)
		{
			this._areaType = GenCondition.AreaType.Or;
			this._width = width;
			this._height = height;
			return this;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x004DDED5 File Offset: 0x004DC0D5
		public GenCondition AreaAnd(int width, int height)
		{
			this._areaType = GenCondition.AreaType.And;
			this._width = width;
			this._height = height;
			return this;
		}

		// Token: 0x06001747 RID: 5959
		protected abstract bool CheckValidity(int x, int y);

		// Token: 0x06001748 RID: 5960 RVA: 0x004DDEED File Offset: 0x004DC0ED
		protected GenCondition()
		{
		}

		// Token: 0x040011DA RID: 4570
		private bool InvertResults;

		// Token: 0x040011DB RID: 4571
		private int _width;

		// Token: 0x040011DC RID: 4572
		private int _height;

		// Token: 0x040011DD RID: 4573
		private GenCondition.AreaType _areaType = GenCondition.AreaType.None;

		// Token: 0x020006B1 RID: 1713
		private enum AreaType
		{
			// Token: 0x04006792 RID: 26514
			And,
			// Token: 0x04006793 RID: 26515
			Or,
			// Token: 0x04006794 RID: 26516
			None
		}
	}
}
