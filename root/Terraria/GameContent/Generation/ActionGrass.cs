using System;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Generation
{
	// Token: 0x02000483 RID: 1155
	public class ActionGrass : GenAction
	{
		// Token: 0x06003354 RID: 13140 RVA: 0x005F5994 File Offset: 0x005F3B94
		public override bool Apply(Point origin, int x, int y, params object[] args)
		{
			if (GenBase._tiles[x, y].active() || GenBase._tiles[x, y - 1].active())
			{
				return false;
			}
			WorldGen.PlaceTile(x, y, (int)Utils.SelectRandom<ushort>(GenBase._random, new ushort[] { 3, 73 }), true, false, -1, 0);
			return base.UnitApply(origin, x, y, args);
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x005F59FB File Offset: 0x005F3BFB
		public ActionGrass()
		{
		}
	}
}
