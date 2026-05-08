using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameInput;

namespace Terraria.GameContent.ObjectInteractions
{
	// Token: 0x020002D0 RID: 720
	public abstract class AHoverInteractionChecker
	{
		// Token: 0x060025FC RID: 9724 RVA: 0x0055C708 File Offset: 0x0055A908
		internal AHoverInteractionChecker.HoverStatus AttemptInteraction(Player player, Rectangle Hitbox)
		{
			Point point = Hitbox.ClosestPointInRect(player.Center).ToTileCoordinates();
			if (!player.IsInTileInteractionRange(point.X, point.Y, TileReachCheckSettings.Simple, 0))
			{
				return AHoverInteractionChecker.HoverStatus.NotSelectable;
			}
			Vector2 vector = Main.ReverseGravitySupport(Main.MouseScreen, 0f) + Main.screenPosition;
			bool flag = Hitbox.Contains(vector.ToPoint());
			bool flag2 = flag;
			bool? flag3 = this.AttemptOverridingHoverStatus(player, Hitbox);
			if (flag3 != null)
			{
				flag2 = flag3.Value;
			}
			flag2 &= !player.lastMouseInterface;
			bool flag4 = !Main.SmartCursorIsUsed && !PlayerInput.UsingGamepad;
			if (!flag2)
			{
				if (!flag4)
				{
					return AHoverInteractionChecker.HoverStatus.SelectableButNotSelected;
				}
				return AHoverInteractionChecker.HoverStatus.NotSelectable;
			}
			else
			{
				Main.HasInteractableObjectThatIsNotATile = true;
				if (flag)
				{
					this.DoHoverEffect(player, Hitbox);
				}
				if (PlayerInput.UsingGamepad)
				{
					player.GamepadEnableGrappleCooldown();
				}
				bool flag5 = this.ShouldBlockInteraction(player, Hitbox);
				if (Main.mouseRight && Main.mouseRightRelease && !flag5)
				{
					Main.mouseRightRelease = false;
					player.tileInteractAttempted = true;
					player.tileInteractionHappened = true;
					player.releaseUseTile = false;
					this.PerformInteraction(player, Hitbox);
				}
				if (!Main.SmartCursorIsUsed && !PlayerInput.UsingGamepad)
				{
					return AHoverInteractionChecker.HoverStatus.NotSelectable;
				}
				if (!flag4)
				{
					return AHoverInteractionChecker.HoverStatus.Selected;
				}
				return AHoverInteractionChecker.HoverStatus.NotSelectable;
			}
		}

		// Token: 0x060025FD RID: 9725
		internal abstract bool? AttemptOverridingHoverStatus(Player player, Rectangle rectangle);

		// Token: 0x060025FE RID: 9726
		internal abstract void DoHoverEffect(Player player, Rectangle hitbox);

		// Token: 0x060025FF RID: 9727
		internal abstract bool ShouldBlockInteraction(Player player, Rectangle hitbox);

		// Token: 0x06002600 RID: 9728
		internal abstract void PerformInteraction(Player player, Rectangle hitbox);

		// Token: 0x06002601 RID: 9729 RVA: 0x0000357B File Offset: 0x0000177B
		protected AHoverInteractionChecker()
		{
		}

		// Token: 0x02000820 RID: 2080
		internal enum HoverStatus
		{
			// Token: 0x04007246 RID: 29254
			NotSelectable,
			// Token: 0x04007247 RID: 29255
			SelectableButNotSelected,
			// Token: 0x04007248 RID: 29256
			Selected
		}
	}
}
