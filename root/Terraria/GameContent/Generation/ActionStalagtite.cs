using System;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x02000485 RID: 1157
	public class ActionStalagtite : GenAction
	{
		// Token: 0x06003358 RID: 13144 RVA: 0x005F5A7C File Offset: 0x005F3C7C
		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			WorldGen.PlaceTight(x, y, false);
			return base.UnitApply(origin, x, y, args);
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x005F59FB File Offset: 0x005F3BFB
		public ActionStalagtite()
		{
		}
	}
}
