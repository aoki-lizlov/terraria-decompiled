using System;

namespace Terraria.GameContent
{
	// Token: 0x0200023A RID: 570
	public static class Luck
	{
		// Token: 0x06002281 RID: 8833 RVA: 0x00538F94 File Offset: 0x00537194
		public static int RollLuck(float luck, int range)
		{
			if (luck > 0f && Main.rand.NextFloat() < luck)
			{
				return Main.rand.Next(Main.rand.Next(range / 2, range));
			}
			if (luck < 0f && Main.rand.NextFloat() < -luck)
			{
				return Main.rand.Next(Main.rand.Next(range, range * 2));
			}
			return Main.rand.Next(range);
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0053900C File Offset: 0x0053720C
		public static int RollBadLuck(float luck, int range)
		{
			if (luck > 0f && Main.rand.NextFloat() < luck)
			{
				return Main.rand.Next(Main.rand.Next(range, range * 2));
			}
			if (luck < 0f && Main.rand.NextFloat() < -luck)
			{
				return Main.rand.Next(Main.rand.Next(range / 2, range));
			}
			return Main.rand.Next(range);
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x00539081 File Offset: 0x00537281
		public static int RollOnlyBadLuck(float luck, int range)
		{
			if (luck < 0f && Main.rand.NextFloat() < -luck)
			{
				return Main.rand.Next(Main.rand.Next(range / 2, range));
			}
			return Main.rand.Next(range);
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x005390C0 File Offset: 0x005372C0
		public static int RollBadLuckExtreme(float luck, int range)
		{
			if (luck > 0f && Main.rand.NextFloat() < luck)
			{
				return Main.rand.Next(range * 10);
			}
			if (luck < 0f && Main.rand.NextFloat() < -luck)
			{
				return Main.rand.Next(range / 10);
			}
			return Main.rand.Next(range);
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x00539121 File Offset: 0x00537321
		public static int RollOnlyBadLuckExtreme(float luck, int range)
		{
			if (luck < 0f && Main.rand.NextFloat() < -luck)
			{
				return Main.rand.Next(range / 10);
			}
			return -1;
		}
	}
}
