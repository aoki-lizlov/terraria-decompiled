using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent;

namespace Terraria.DataStructures
{
	// Token: 0x02000536 RID: 1334
	public class PlayerIntentionGuesser
	{
		// Token: 0x06003745 RID: 14149 RVA: 0x0062E708 File Offset: 0x0062C908
		public void Track(Player player, int x, int y, GuessedPlayerIntention intention)
		{
			if (this.PlayerActiveActionTimeLeft == 0)
			{
				return;
			}
			this.LastX = x;
			this.LastY = y;
			this.Intention = intention;
			this.LastPosition = player.position;
			this.LastCenter = player.Center;
			this.LastDirection = player.direction;
			this.LastWidth = player.width;
			this.LastMouse = Main.MouseWorld;
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x0062E76F File Offset: 0x0062C96F
		public void AllowTracking(int time = 60)
		{
			this.PlayerActiveActionTimeLeft = time;
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x0062E778 File Offset: 0x0062C978
		public void Update(Player player)
		{
			if (player.whoAmI != Main.myPlayer)
			{
				return;
			}
			this.TimeWithIntention++;
			if (this.PlayerActiveActionTimeLeft > 0)
			{
				this.PlayerActiveActionTimeLeft--;
			}
			if (this.Intention == GuessedPlayerIntention.None)
			{
				return;
			}
			float num = player.Center.Distance(this.LastCenter);
			bool flag = false;
			if (num > 80f)
			{
				flag = true;
			}
			if (player.controlJump)
			{
				flag = true;
			}
			bool usingOrReusingItem = player.UsingOrReusingItem;
			if (usingOrReusingItem && this.Intention == GuessedPlayerIntention.HarvestTreasure && player.HeldItem.pick <= 0)
			{
				flag = true;
			}
			if (usingOrReusingItem && this.Intention == GuessedPlayerIntention.HarvestTrees && player.HeldItem.axe <= 0)
			{
				flag = true;
			}
			if (this.TimeWithIntention >= 480)
			{
				flag = true;
			}
			if (player.dead)
			{
				flag = true;
			}
			if (flag)
			{
				this.Intention = GuessedPlayerIntention.None;
				this.TimeWithIntention = 0;
				return;
			}
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x0062E850 File Offset: 0x0062CA50
		public void PrepareUsageProxy(Player player, int itemType, int areaInflateWidth, int areaInflateHeight)
		{
			this.UsageProxy.player = player;
			if (this.UsageProxy.item == null)
			{
				this.UsageProxy.item = new Item();
			}
			this.UsageProxy.item.SetDefaults(itemType, null);
			this.UsageProxy.position = this.LastPosition;
			this.UsageProxy.Center = this.LastCenter;
			this.UsageProxy.mouse = this.LastMouse;
			this.UsageProxy.screenTargetX = this.LastX;
			this.UsageProxy.screenTargetY = this.LastY;
			this.UsageProxy.screenTargetX = Utils.Clamp<int>(this.UsageProxy.screenTargetX, 10, Main.maxTilesX - 10);
			this.UsageProxy.screenTargetY = Utils.Clamp<int>(this.UsageProxy.screenTargetY, 10, Main.maxTilesY - 10);
			Rectangle rectangle = new Rectangle(this.LastX, this.LastY, 1, 1);
			rectangle.Inflate(areaInflateWidth, areaInflateHeight);
			Rectangle rectangle2 = new Rectangle(0, 0, Main.maxTilesX, Main.maxTilesY);
			rectangle2.Inflate(-10, -10);
			Rectangle rectangle3 = default(Rectangle);
			rectangle3 = Rectangle.Intersect(rectangle, rectangle2);
			this.UsageProxy.reachableStartX = rectangle3.Left;
			this.UsageProxy.reachableStartY = rectangle3.Top;
			this.UsageProxy.reachableEndX = rectangle3.Right;
			this.UsageProxy.reachableEndY = rectangle3.Bottom;
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x0062E9CC File Offset: 0x0062CBCC
		public PlayerIntentionGuesser()
		{
		}

		// Token: 0x04005B78 RID: 23416
		public int LastX;

		// Token: 0x04005B79 RID: 23417
		public int LastY;

		// Token: 0x04005B7A RID: 23418
		public Vector2 LastPosition;

		// Token: 0x04005B7B RID: 23419
		public Vector2 LastCenter;

		// Token: 0x04005B7C RID: 23420
		public Vector2 LastMouse;

		// Token: 0x04005B7D RID: 23421
		public int LastDirection;

		// Token: 0x04005B7E RID: 23422
		public int LastWidth;

		// Token: 0x04005B7F RID: 23423
		public GuessedPlayerIntention Intention;

		// Token: 0x04005B80 RID: 23424
		public SmartCursorHelper.SmartCursorUsageInfo UsageProxy = new SmartCursorHelper.SmartCursorUsageInfo();

		// Token: 0x04005B81 RID: 23425
		public int TimeWithIntention;

		// Token: 0x04005B82 RID: 23426
		public int PlayerActiveActionTimeLeft;
	}
}
