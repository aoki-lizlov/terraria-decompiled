using System;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.GameContent
{
	// Token: 0x02000262 RID: 610
	public struct PlayerSleepingHelper
	{
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x0053F839 File Offset: 0x0053DA39
		public bool FullyFallenAsleep
		{
			get
			{
				return this.isSleeping && this.timeSleeping >= 120;
			}
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x0053F854 File Offset: 0x0053DA54
		public void GetSleepingOffsetInfo(Player player, out Vector2 posOffset)
		{
			if (this.isSleeping)
			{
				posOffset = this.visualOffsetOfBedBase * player.Directions + new Vector2(0f, (float)this.sleepingIndex * player.gravDir * -4f);
				return;
			}
			posOffset = Vector2.Zero;
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x0053F8AF File Offset: 0x0053DAAF
		private bool DoesPlayerHaveReasonToActUpInBed(Player player)
		{
			return NPC.AnyDanger(true, false) || (Main.bloodMoon && !Main.dayTime) || (Main.eclipse && Main.dayTime) || player.itemAnimation > 0;
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x0053F8E8 File Offset: 0x0053DAE8
		public void SetIsSleepingAndAdjustPlayerRotation(Player player, bool state)
		{
			if (this.isSleeping == state)
			{
				return;
			}
			this.isSleeping = state;
			if (state)
			{
				player.fullRotation = 1.5707964f * (float)(-(float)player.direction);
				player.fullRotationOrigin = player.Size / 2f;
				return;
			}
			player.fullRotation = 0f;
			player.fullRotationOrigin = Vector2.Zero;
			this.visualOffsetOfBedBase = default(Vector2);
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x0053F958 File Offset: 0x0053DB58
		public void UpdateState(Player player)
		{
			if (!this.isSleeping)
			{
				this.timeSleeping = 0;
				return;
			}
			this.timeSleeping++;
			if (this.DoesPlayerHaveReasonToActUpInBed(player))
			{
				this.timeSleeping = 0;
			}
			Point point = (player.Bottom + new Vector2(0f, -2f)).ToTileCoordinates();
			int num;
			Vector2 vector;
			Vector2 vector2;
			if (!PlayerSleepingHelper.GetSleepingTargetInfo(point.X, point.Y, out num, out vector, out vector2))
			{
				this.StopSleeping(player, true);
				return;
			}
			if (player.controlLeft || player.controlRight || player.controlUp || player.controlDown || player.controlJump || player.pulley || player.mount.Active || num != player.direction)
			{
				this.StopSleeping(player, true);
			}
			bool flag = false;
			if (player.itemAnimation > 0)
			{
				Item heldItem = player.HeldItem;
				if (heldItem.damage > 0 && !heldItem.noMelee)
				{
					flag = true;
				}
				if (heldItem.fishingPole > 0)
				{
					flag = true;
				}
				bool? flag2 = ItemID.Sets.ForcesBreaksSleeping[heldItem.type];
				if (flag2 != null)
				{
					flag = flag2.Value;
				}
			}
			if (flag)
			{
				this.StopSleeping(player, true);
			}
			if (Main.sleepingManager.GetNextPlayerStackIndexInCoords(point) >= 2)
			{
				this.StopSleeping(player, true);
			}
			if (!this.isSleeping)
			{
				return;
			}
			this.visualOffsetOfBedBase = vector2;
			Main.sleepingManager.AddPlayerAndGetItsStackedIndexInCoords(player.whoAmI, point, out this.sleepingIndex);
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x0053FAD0 File Offset: 0x0053DCD0
		public void StopSleeping(Player player, bool multiplayerBroadcast = true)
		{
			if (!this.isSleeping)
			{
				return;
			}
			this.SetIsSleepingAndAdjustPlayerRotation(player, false);
			this.timeSleeping = 0;
			this.sleepingIndex = -1;
			this.visualOffsetOfBedBase = default(Vector2);
			if (multiplayerBroadcast && Main.myPlayer == player.whoAmI)
			{
				NetMessage.SendData(13, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x0053FB3C File Offset: 0x0053DD3C
		public void StartSleeping(Player player, int x, int y)
		{
			int num;
			Vector2 vector;
			Vector2 vector2;
			PlayerSleepingHelper.GetSleepingTargetInfo(x, y, out num, out vector, out vector2);
			Vector2 vector3 = vector - player.Bottom;
			bool flag = player.CanSnapToPosition(vector3);
			if (flag)
			{
				flag &= Main.sleepingManager.GetNextPlayerStackIndexInCoords((vector + new Vector2(0f, -2f)).ToTileCoordinates()) < 2;
			}
			if (!flag)
			{
				return;
			}
			if (this.isSleeping && player.Bottom == vector)
			{
				this.StopSleeping(player, true);
				return;
			}
			player.StopVanityActions(true);
			player.RemoveAllGrapplingHooks();
			player.RemoveAllFishingBobbers();
			if (player.mount.Active)
			{
				player.mount.TryDismount(player);
			}
			player.Bottom = vector;
			player.ChangeDir(num);
			Main.sleepingManager.AddPlayerAndGetItsStackedIndexInCoords(player.whoAmI, new Point(x, y), out this.sleepingIndex);
			player.velocity = Vector2.Zero;
			player.gravDir = 1f;
			this.SetIsSleepingAndAdjustPlayerRotation(player, true);
			this.visualOffsetOfBedBase = vector2;
			if (Main.myPlayer == player.whoAmI)
			{
				NetMessage.SendData(13, -1, -1, null, player.whoAmI, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x0053FC70 File Offset: 0x0053DE70
		public static bool GetSleepingTargetInfo(int x, int y, out int targetDirection, out Vector2 anchorPosition, out Vector2 visualoffset)
		{
			Tile tileSafely = Framing.GetTileSafely(x, y);
			if (!TileID.Sets.CanBeSleptIn[(int)tileSafely.type] || !tileSafely.active())
			{
				targetDirection = 1;
				anchorPosition = default(Vector2);
				visualoffset = default(Vector2);
				return false;
			}
			int num = y;
			int num2 = x - (int)(tileSafely.frameX % 72 / 18);
			if (tileSafely.frameY % 36 != 0)
			{
				num--;
			}
			targetDirection = 1;
			int num3 = (int)(tileSafely.frameX / 72);
			int num4 = num2;
			if (num3 != 0)
			{
				if (num3 == 1)
				{
					num4 += 2;
				}
			}
			else
			{
				targetDirection = -1;
				num4++;
			}
			anchorPosition = new Point(num4, num + 1).ToWorldCoordinates(8f, 16f);
			visualoffset = PlayerSleepingHelper.SetOffsetbyBed((int)(tileSafely.frameY / 36));
			return true;
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x0053FD28 File Offset: 0x0053DF28
		private static Vector2 SetOffsetbyBed(int bedStyle)
		{
			switch (bedStyle)
			{
			case 8:
				return new Vector2(-11f, 1f);
			default:
				return new Vector2(-9f, 1f);
			case 10:
				return new Vector2(-9f, -1f);
			case 11:
				return new Vector2(-11f, 1f);
			case 13:
				return new Vector2(-11f, -3f);
			case 15:
			case 16:
			case 17:
				return new Vector2(-7f, -3f);
			case 18:
				return new Vector2(-9f, -3f);
			case 19:
				return new Vector2(-3f, -1f);
			case 20:
				return new Vector2(-9f, -5f);
			case 21:
				return new Vector2(-9f, 5f);
			case 22:
				return new Vector2(-7f, 1f);
			case 23:
				return new Vector2(-5f, -1f);
			case 24:
			case 25:
				return new Vector2(-7f, 1f);
			case 27:
				return new Vector2(-9f, 3f);
			case 28:
				return new Vector2(-9f, 5f);
			case 29:
				return new Vector2(-11f, -1f);
			case 30:
				return new Vector2(-9f, 3f);
			case 31:
				return new Vector2(-7f, 5f);
			case 32:
				return new Vector2(-7f, -1f);
			case 34:
			case 35:
			case 36:
			case 37:
				return new Vector2(-13f, 1f);
			case 38:
				return new Vector2(-11f, -3f);
			}
		}

		// Token: 0x04004D93 RID: 19859
		public const int BedSleepingMaxDistance = 96;

		// Token: 0x04004D94 RID: 19860
		public const int TimeToFullyFallAsleep = 120;

		// Token: 0x04004D95 RID: 19861
		public bool isSleeping;

		// Token: 0x04004D96 RID: 19862
		public int sleepingIndex;

		// Token: 0x04004D97 RID: 19863
		public int timeSleeping;

		// Token: 0x04004D98 RID: 19864
		public Vector2 visualOffsetOfBedBase;
	}
}
