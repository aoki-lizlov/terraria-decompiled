using System;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x02000486 RID: 1158
	public class ActionVines : GenAction
	{
		// Token: 0x0600335A RID: 13146 RVA: 0x005F5A91 File Offset: 0x005F3C91
		public ActionVines(int minLength = 6, int maxLength = 10, int vineId = 52)
		{
			this._minLength = minLength;
			this._maxLength = maxLength;
			this._vineId = vineId;
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x005F5AB0 File Offset: 0x005F3CB0
		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			int num = GenBase._random.Next(this._minLength, this._maxLength + 1);
			int num2 = 0;
			while (num2 < num && !GenBase._tiles[x, y + num2].active())
			{
				GenBase._tiles[x, y + num2].type = (ushort)this._vineId;
				GenBase._tiles[x, y + num2].active(true);
				num2++;
			}
			return num2 > 0 && base.UnitApply(origin, x, y, args);
		}

		// Token: 0x040058D0 RID: 22736
		private int _minLength;

		// Token: 0x040058D1 RID: 22737
		private int _maxLength;

		// Token: 0x040058D2 RID: 22738
		private int _vineId;
	}
}
