using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000AF RID: 175
	public abstract class GenSearch : GenBase
	{
		// Token: 0x0600175E RID: 5982 RVA: 0x004DE075 File Offset: 0x004DC275
		public GenSearch Conditions(params GenCondition[] conditions)
		{
			this._conditions = conditions;
			return this;
		}

		// Token: 0x0600175F RID: 5983
		public abstract Point Find(Point origin);

		// Token: 0x06001760 RID: 5984 RVA: 0x004DE080 File Offset: 0x004DC280
		protected bool Check(int x, int y)
		{
			for (int i = 0; i < this._conditions.Length; i++)
			{
				if (this._requireAll ^ this._conditions[i].IsValid(x, y))
				{
					return !this._requireAll;
				}
			}
			return this._requireAll;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x004DE0C8 File Offset: 0x004DC2C8
		public GenSearch RequireAll(bool mode)
		{
			this._requireAll = mode;
			return this;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x004DE0D2 File Offset: 0x004DC2D2
		protected GenSearch()
		{
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x004DE0E1 File Offset: 0x004DC2E1
		// Note: this type is marked as 'beforefieldinit'.
		static GenSearch()
		{
		}

		// Token: 0x040011E7 RID: 4583
		public static Point NOT_FOUND = new Point(int.MaxValue, int.MaxValue);

		// Token: 0x040011E8 RID: 4584
		private bool _requireAll = true;

		// Token: 0x040011E9 RID: 4585
		private GenCondition[] _conditions;
	}
}
