using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020002DB RID: 731
	public class PotionOfReturnHelper
	{
		// Token: 0x06002619 RID: 9753 RVA: 0x0055DB20 File Offset: 0x0055BD20
		public static bool TryGetGateHitbox(Player player, out Rectangle homeHitbox)
		{
			homeHitbox = Rectangle.Empty;
			if (player.PotionOfReturnHomePosition == null)
			{
				return false;
			}
			Vector2 vector = new Vector2(0f, -21f);
			Vector2 vector2 = player.PotionOfReturnHomePosition.Value + vector;
			homeHitbox = Utils.CenteredRectangle(vector2, new Vector2(24f, 40f));
			return true;
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x0000357B File Offset: 0x0000177B
		public PotionOfReturnHelper()
		{
		}
	}
}
