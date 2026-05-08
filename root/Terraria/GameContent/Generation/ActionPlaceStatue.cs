using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x02000484 RID: 1156
	public class ActionPlaceStatue : GenAction
	{
		// Token: 0x06003356 RID: 13142 RVA: 0x005F5A03 File Offset: 0x005F3C03
		public ActionPlaceStatue(int index = -1)
		{
			this._statueIndex = index;
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x005F5A14 File Offset: 0x005F3C14
		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			Point16 point;
			if (this._statueIndex == -1)
			{
				point = GenVars.statueList[GenBase._random.Next(2, GenVars.statueList.Length)];
			}
			else
			{
				point = GenVars.statueList[this._statueIndex];
			}
			WorldGen.PlaceTile(x, y, (int)point.X, true, false, -1, (int)point.Y);
			return base.UnitApply(origin, x, y, args);
		}

		// Token: 0x040058CF RID: 22735
		private int _statueIndex;
	}
}
