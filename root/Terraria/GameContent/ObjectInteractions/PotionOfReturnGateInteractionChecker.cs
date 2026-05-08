using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020002DC RID: 732
	public class PotionOfReturnGateInteractionChecker : AHoverInteractionChecker
	{
		// Token: 0x0600261B RID: 9755 RVA: 0x0055DB88 File Offset: 0x0055BD88
		internal override bool? AttemptOverridingHoverStatus(Player player, Rectangle rectangle)
		{
			if (Main.SmartInteractPotionOfReturn)
			{
				return new bool?(true);
			}
			return null;
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x0055DBAC File Offset: 0x0055BDAC
		internal override void DoHoverEffect(Player player, Rectangle hitbox)
		{
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = 4870;
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x0055DBC7 File Offset: 0x0055BDC7
		internal override bool ShouldBlockInteraction(Player player, Rectangle hitbox)
		{
			return Player.BlockInteractionWithProjectiles != 0;
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x0055DBD1 File Offset: 0x0055BDD1
		internal override void PerformInteraction(Player player, Rectangle hitbox)
		{
			player.DoPotionOfReturnReturnToOriginalUsePosition();
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x0055DBD9 File Offset: 0x0055BDD9
		public PotionOfReturnGateInteractionChecker()
		{
		}
	}
}
