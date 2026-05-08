using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.GameInput
{
	// Token: 0x0200008D RID: 141
	public class LockOnHelper
	{
		// Token: 0x060015DA RID: 5594 RVA: 0x004D3AD0 File Offset: 0x004D1CD0
		public static void CycleUseModes()
		{
			switch (LockOnHelper.UseMode)
			{
			case LockOnHelper.LockOnMode.FocusTarget:
				LockOnHelper.UseMode = LockOnHelper.LockOnMode.TargetClosest;
				return;
			case LockOnHelper.LockOnMode.TargetClosest:
				LockOnHelper.UseMode = LockOnHelper.LockOnMode.ThreeDS;
				return;
			case LockOnHelper.LockOnMode.ThreeDS:
				LockOnHelper.UseMode = LockOnHelper.LockOnMode.TargetClosest;
				return;
			default:
				return;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x004D3B0A File Offset: 0x004D1D0A
		public static NPC AimedTarget
		{
			get
			{
				if (LockOnHelper._pickedTarget == -1 || LockOnHelper._targets.Count < 1)
				{
					return null;
				}
				return Main.npc[LockOnHelper._targets[LockOnHelper._pickedTarget]];
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x004D3B38 File Offset: 0x004D1D38
		public static Vector2 PredictedPosition
		{
			get
			{
				NPC aimedTarget = LockOnHelper.AimedTarget;
				if (aimedTarget == null)
				{
					return Vector2.Zero;
				}
				Vector2 vector = aimedTarget.Center;
				int num;
				Vector2 vector2;
				if (NPC.GetNPCLocation(LockOnHelper._targets[LockOnHelper._pickedTarget], true, false, out num, out vector2))
				{
					vector = vector2;
					vector += Main.npc[num].Distance(Main.player[Main.myPlayer].Center) / 2000f * Main.npc[num].velocity * 45f;
				}
				Player player = Main.player[Main.myPlayer];
				int num2 = ItemID.Sets.LockOnAimAbove[player.inventory[player.selectedItem].type];
				while (num2 > 0 && vector.Y > 100f)
				{
					Point point = vector.ToTileCoordinates();
					point.Y -= 4;
					if (!WorldGen.InWorld(point.X, point.Y, 10) || WorldGen.SolidTile(point.X, point.Y, false))
					{
						break;
					}
					vector.Y -= 16f;
					num2--;
				}
				float? num3 = ItemID.Sets.LockOnAimCompensation[player.inventory[player.selectedItem].type];
				if (num3 != null)
				{
					vector.Y -= (float)(aimedTarget.height / 2);
					Vector2 vector3 = vector - player.Center;
					Vector2 vector4 = vector3.SafeNormalize(Vector2.Zero);
					vector4.Y -= 1f;
					float num4 = vector3.Length();
					num4 = (float)Math.Pow((double)(num4 / 700f), 2.0) * 700f;
					vector.Y += vector4.Y * num4 * num3.Value * 1f;
					vector.X += -vector4.X * num4 * num3.Value * 1f;
				}
				return vector;
			}
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x004D3D30 File Offset: 0x004D1F30
		public static void Update()
		{
			LockOnHelper._canLockOn = false;
			if (!LockOnHelper.CanUseLockonSystem())
			{
				LockOnHelper.SetActive(false);
				return;
			}
			if (--LockOnHelper._lifeTimeArrowDisplay < 0)
			{
				LockOnHelper._lifeTimeArrowDisplay = 0;
			}
			LockOnHelper.FindMostViableTarget(LockOnHelper.LockOnMode.ThreeDS, ref LockOnHelper._threeDSTarget);
			LockOnHelper.FindMostViableTarget(LockOnHelper.LockOnMode.TargetClosest, ref LockOnHelper._targetClosestTarget);
			if (PlayerInput.Triggers.JustPressed.LockOn && !PlayerInput.WritingText)
			{
				LockOnHelper._lifeTimeCounter = 40;
				LockOnHelper._lifeTimeArrowDisplay = 30;
				LockOnHelper.HandlePressing();
			}
			if (!LockOnHelper._enabled)
			{
				return;
			}
			if (LockOnHelper.UseMode == LockOnHelper.LockOnMode.FocusTarget && PlayerInput.Triggers.Current.LockOn)
			{
				if (LockOnHelper._lifeTimeCounter <= 0)
				{
					LockOnHelper.SetActive(false);
					return;
				}
				LockOnHelper._lifeTimeCounter--;
			}
			NPC aimedTarget = LockOnHelper.AimedTarget;
			if (!LockOnHelper.ValidTarget(aimedTarget))
			{
				LockOnHelper.SetActive(false);
			}
			if (LockOnHelper.UseMode == LockOnHelper.LockOnMode.TargetClosest)
			{
				LockOnHelper.SetActive(false);
				LockOnHelper.SetActive(LockOnHelper.CanEnable());
			}
			if (!LockOnHelper._enabled)
			{
				return;
			}
			Player player = Main.player[Main.myPlayer];
			Vector2 predictedPosition = LockOnHelper.PredictedPosition;
			bool flag = false;
			if (LockOnHelper.ShouldLockOn(player) && (ItemID.Sets.LockOnIgnoresCollision[player.inventory[player.selectedItem].type] || Collision.CanHit(player.Center, 0, 0, predictedPosition, 0, 0) || Collision.CanHitLine(player.Center, 0, 0, predictedPosition, 0, 0) || Collision.CanHit(player.Center, 0, 0, aimedTarget.Center, 0, 0) || Collision.CanHitLine(player.Center, 0, 0, aimedTarget.Center, 0, 0)))
			{
				flag = true;
			}
			if (flag)
			{
				LockOnHelper._canLockOn = true;
			}
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x004D3EAA File Offset: 0x004D20AA
		public static bool CanUseLockonSystem()
		{
			return LockOnHelper.ForceUsability || PlayerInput.UsingGamepad;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x004D3EBA File Offset: 0x004D20BA
		public static void SetUP()
		{
			if (!LockOnHelper._canLockOn)
			{
				return;
			}
			NPC aimedTarget = LockOnHelper.AimedTarget;
			LockOnHelper.SetLockPosition(Main.ReverseGravitySupport(LockOnHelper.PredictedPosition - Main.screenPosition, 0f));
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x004D3EE8 File Offset: 0x004D20E8
		public static void SetDOWN()
		{
			if (!LockOnHelper._canLockOn)
			{
				return;
			}
			LockOnHelper.ResetLockPosition();
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x004D3EF7 File Offset: 0x004D20F7
		private static bool ShouldLockOn(Player p)
		{
			return !ItemID.Sets.NeverLocksOn[p.inventory[p.selectedItem].type];
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x004D3F16 File Offset: 0x004D2116
		public static void Toggle(bool forceOff = false)
		{
			LockOnHelper._lifeTimeCounter = 40;
			LockOnHelper._lifeTimeArrowDisplay = 30;
			LockOnHelper.HandlePressing();
			if (forceOff)
			{
				LockOnHelper._enabled = false;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x004D3F34 File Offset: 0x004D2134
		public static bool Enabled
		{
			get
			{
				return LockOnHelper._enabled;
			}
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x004D3F3C File Offset: 0x004D213C
		private static void FindMostViableTarget(LockOnHelper.LockOnMode context, ref int targetVar)
		{
			targetVar = -1;
			if (LockOnHelper.UseMode != context)
			{
				return;
			}
			if (!LockOnHelper.CanUseLockonSystem())
			{
				return;
			}
			List<int> list = new List<int>();
			int num = -1;
			Utils.Swap<List<int>>(ref list, ref LockOnHelper._targets);
			Utils.Swap<int>(ref num, ref LockOnHelper._pickedTarget);
			LockOnHelper.RefreshTargets(Main.MouseWorld, 2000f);
			LockOnHelper.GetClosestTarget(Main.MouseWorld);
			Utils.Swap<List<int>>(ref list, ref LockOnHelper._targets);
			Utils.Swap<int>(ref num, ref LockOnHelper._pickedTarget);
			if (num >= 0)
			{
				targetVar = list[num];
			}
			list.Clear();
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x004D3FC4 File Offset: 0x004D21C4
		private static void HandlePressing()
		{
			if (LockOnHelper.UseMode == LockOnHelper.LockOnMode.TargetClosest)
			{
				LockOnHelper.SetActive(!LockOnHelper._enabled);
				return;
			}
			if (LockOnHelper.UseMode == LockOnHelper.LockOnMode.ThreeDS)
			{
				if (!LockOnHelper._enabled)
				{
					LockOnHelper.SetActive(true);
					return;
				}
				LockOnHelper.CycleTargetThreeDS();
				return;
			}
			else
			{
				if (!LockOnHelper._enabled)
				{
					LockOnHelper.SetActive(true);
					return;
				}
				LockOnHelper.CycleTargetFocus();
				return;
			}
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x004D4018 File Offset: 0x004D2218
		private static void CycleTargetFocus()
		{
			int num = LockOnHelper._targets[LockOnHelper._pickedTarget];
			LockOnHelper.RefreshTargets(Main.MouseWorld, 2000f);
			if (LockOnHelper._targets.Count < 1 || (LockOnHelper._targets.Count == 1 && num == LockOnHelper._targets[0]))
			{
				LockOnHelper.SetActive(false);
				return;
			}
			LockOnHelper._pickedTarget = 0;
			for (int i = 0; i < LockOnHelper._targets.Count; i++)
			{
				if (LockOnHelper._targets[i] > num)
				{
					LockOnHelper._pickedTarget = i;
					return;
				}
			}
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x004D40A4 File Offset: 0x004D22A4
		private static void CycleTargetThreeDS()
		{
			int num = LockOnHelper._targets[LockOnHelper._pickedTarget];
			LockOnHelper.RefreshTargets(Main.MouseWorld, 2000f);
			LockOnHelper.GetClosestTarget(Main.MouseWorld);
			if (LockOnHelper._targets.Count < 1 || (LockOnHelper._targets.Count == 1 && num == LockOnHelper._targets[0]) || num == LockOnHelper._targets[LockOnHelper._pickedTarget])
			{
				LockOnHelper.SetActive(false);
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x004D411A File Offset: 0x004D231A
		private static bool CanEnable()
		{
			return !Main.player[Main.myPlayer].dead;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x004D4134 File Offset: 0x004D2334
		private static void SetActive(bool on)
		{
			if (on)
			{
				if (!LockOnHelper.CanEnable())
				{
					return;
				}
				LockOnHelper.RefreshTargets(Main.MouseWorld, 2000f);
				LockOnHelper.GetClosestTarget(Main.MouseWorld);
				if (LockOnHelper._pickedTarget >= 0)
				{
					LockOnHelper._enabled = true;
					return;
				}
			}
			else
			{
				LockOnHelper._enabled = false;
				LockOnHelper._targets.Clear();
				LockOnHelper._lifeTimeCounter = 0;
				LockOnHelper._threeDSTarget = -1;
				LockOnHelper._targetClosestTarget = -1;
			}
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x004D4198 File Offset: 0x004D2398
		private static void RefreshTargets(Vector2 position, float radius)
		{
			LockOnHelper._targets.Clear();
			Rectangle rectangle = Utils.CenteredRectangle(Main.player[Main.myPlayer].Center, Main.MaxWorldViewSize.ToVector2());
			Vector2 center = Main.player[Main.myPlayer].Center;
			Main.player[Main.myPlayer].DirectionTo(Main.MouseWorld);
			for (int i = 0; i < Main.npc.Length; i++)
			{
				NPC npc = Main.npc[i];
				if (LockOnHelper.ValidTarget(npc) && npc.Distance(position) <= radius && rectangle.Intersects(npc.Hitbox) && Lighting.GetSubLight(npc.Center).Length() / 3f >= 0.03f)
				{
					LockOnHelper._targets.Add(i);
				}
			}
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x004D425C File Offset: 0x004D245C
		private static void GetClosestTarget(Vector2 position)
		{
			LockOnHelper._pickedTarget = -1;
			float num = -1f;
			if (LockOnHelper.UseMode == LockOnHelper.LockOnMode.ThreeDS)
			{
				Vector2 center = Main.player[Main.myPlayer].Center;
				Vector2 vector = Main.player[Main.myPlayer].DirectionTo(Main.MouseWorld);
				for (int i = 0; i < LockOnHelper._targets.Count; i++)
				{
					int num2 = LockOnHelper._targets[i];
					NPC npc = Main.npc[num2];
					float num3 = Vector2.Dot(npc.DirectionFrom(center), vector);
					if (LockOnHelper.ValidTarget(npc) && (LockOnHelper._pickedTarget == -1 || num3 > num))
					{
						LockOnHelper._pickedTarget = i;
						num = num3;
					}
				}
				return;
			}
			for (int j = 0; j < LockOnHelper._targets.Count; j++)
			{
				int num4 = LockOnHelper._targets[j];
				NPC npc2 = Main.npc[num4];
				if (LockOnHelper.ValidTarget(npc2) && (LockOnHelper._pickedTarget == -1 || npc2.Distance(position) < num))
				{
					LockOnHelper._pickedTarget = j;
					num = npc2.Distance(position);
				}
			}
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x004D4358 File Offset: 0x004D2558
		private static bool ValidTarget(NPC n)
		{
			return n != null && n.active && !n.dontTakeDamage && !n.friendly && !n.isLikeATownNPC && n.life >= 1 && !n.immortal && (n.aiStyle != 25 || n.ai[0] != 0f);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x004D43B7 File Offset: 0x004D25B7
		private static void SetLockPosition(Vector2 position)
		{
			PlayerInput.LockOnCachePosition();
			Main.mouseX = (PlayerInput.MouseX = (int)position.X);
			Main.mouseY = (PlayerInput.MouseY = (int)position.Y);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x004D43E2 File Offset: 0x004D25E2
		private static void ResetLockPosition()
		{
			PlayerInput.LockOnUnCachePosition();
			Main.mouseX = PlayerInput.MouseX;
			Main.mouseY = PlayerInput.MouseY;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x004D4400 File Offset: 0x004D2600
		public static void Draw(SpriteBatch spriteBatch)
		{
			if (Main.gameMenu)
			{
				return;
			}
			Texture2D value = TextureAssets.LockOnCursor.Value;
			Rectangle rectangle = new Rectangle(0, 0, value.Width, 12);
			Rectangle rectangle2 = new Rectangle(0, 16, value.Width, 12);
			Color color = Main.OurFavoriteColor.MultiplyRGBA(new Color(0.75f, 0.75f, 0.75f, 1f));
			color.A = 220;
			Color color2 = Main.OurFavoriteColor;
			color2.A = 220;
			float num = 0.94f + (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f)) * 0.06f;
			color2 *= num;
			color *= num;
			Utils.Swap<Color>(ref color, ref color2);
			Color color3 = color.MultiplyRGBA(new Color(0.8f, 0.8f, 0.8f, 0.8f));
			Color color4 = color.MultiplyRGBA(new Color(0.8f, 0.8f, 0.8f, 0.8f));
			float gravDir = Main.player[Main.myPlayer].gravDir;
			float num2 = 1f;
			float num3 = 0.1f;
			float num4 = 0.8f;
			float num5 = 1f;
			float num6 = 10f;
			float num7 = 10f;
			bool flag = false;
			for (int i = 0; i < LockOnHelper._drawProgress.GetLength(0); i++)
			{
				int num8 = 0;
				if (LockOnHelper._pickedTarget != -1 && LockOnHelper._targets.Count > 0 && i == LockOnHelper._targets[LockOnHelper._pickedTarget])
				{
					num8 = 2;
				}
				else if ((flag && LockOnHelper._targets.Contains(i)) || (LockOnHelper.UseMode == LockOnHelper.LockOnMode.ThreeDS && LockOnHelper._threeDSTarget == i) || (LockOnHelper.UseMode == LockOnHelper.LockOnMode.TargetClosest && LockOnHelper._targetClosestTarget == i))
				{
					num8 = 1;
				}
				LockOnHelper._drawProgress[i, 0] = MathHelper.Clamp(LockOnHelper._drawProgress[i, 0] + ((num8 == 1) ? num3 : (-num3)), 0f, 1f);
				LockOnHelper._drawProgress[i, 1] = MathHelper.Clamp(LockOnHelper._drawProgress[i, 1] + ((num8 == 2) ? num3 : (-num3)), 0f, 1f);
				float num9 = LockOnHelper._drawProgress[i, 0];
				if (num9 > 0f)
				{
					float num10 = 1f - num9 * num9;
					Vector2 vector = Main.npc[i].Top + new Vector2(0f, -num7 - num10 * num6) * gravDir - Main.screenPosition;
					vector = Main.ReverseGravitySupport(vector, (float)Main.npc[i].height);
					spriteBatch.Draw(value, vector, new Rectangle?(rectangle), color3 * num9, 0f, rectangle.Size() / 2f, new Vector2(0.58f, 1f) * num2 * num4 * (1f + num9) / 2f, SpriteEffects.None, 0f);
					spriteBatch.Draw(value, vector, new Rectangle?(rectangle2), color4 * num9 * num9, 0f, rectangle2.Size() / 2f, new Vector2(0.58f, 1f) * num2 * num4 * (1f + num9) / 2f, SpriteEffects.None, 0f);
				}
				float num11 = LockOnHelper._drawProgress[i, 1];
				if (num11 > 0f)
				{
					int num12 = Main.npc[i].width;
					if (Main.npc[i].height > num12)
					{
						num12 = Main.npc[i].height;
					}
					num12 += 20;
					if ((float)num12 < 70f)
					{
						num5 *= (float)num12 / 70f;
					}
					float num13 = 3f;
					Vector2 vector2 = Main.npc[i].Center;
					int num14;
					Vector2 vector3;
					if (LockOnHelper._targets.Count >= 0 && LockOnHelper._pickedTarget >= 0 && LockOnHelper._pickedTarget < LockOnHelper._targets.Count && i == LockOnHelper._targets[LockOnHelper._pickedTarget] && NPC.GetNPCLocation(i, true, false, out num14, out vector3))
					{
						vector2 = vector3;
					}
					int num15 = 0;
					while ((float)num15 < num13)
					{
						float num16 = 6.2831855f / num13 * (float)num15 + Main.GlobalTimeWrappedHourly * 6.2831855f * 0.25f;
						Vector2 vector4 = new Vector2(0f, (float)(num12 / 2)).RotatedBy((double)num16, default(Vector2));
						Vector2 vector5 = vector2 + vector4 - Main.screenPosition;
						vector5 = Main.ReverseGravitySupport(vector5, 0f);
						float num17 = num16 * (float)((gravDir == 1f) ? 1 : (-1)) + 3.1415927f * (float)((gravDir == 1f) ? 1 : 0);
						spriteBatch.Draw(value, vector5, new Rectangle?(rectangle), color * num11, num17, rectangle.Size() / 2f, new Vector2(0.58f, 1f) * num2 * num5 * (1f + num11) / 2f, SpriteEffects.None, 0f);
						spriteBatch.Draw(value, vector5, new Rectangle?(rectangle2), color2 * num11 * num11, num17, rectangle2.Size() / 2f, new Vector2(0.58f, 1f) * num2 * num5 * (1f + num11) / 2f, SpriteEffects.None, 0f);
						num15++;
					}
				}
			}
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x0000357B File Offset: 0x0000177B
		public LockOnHelper()
		{
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x004D49C2 File Offset: 0x004D2BC2
		// Note: this type is marked as 'beforefieldinit'.
		static LockOnHelper()
		{
		}

		// Token: 0x0400111A RID: 4378
		private const float LOCKON_RANGE = 2000f;

		// Token: 0x0400111B RID: 4379
		private const int LOCKON_HOLD_LIFETIME = 40;

		// Token: 0x0400111C RID: 4380
		public static LockOnHelper.LockOnMode UseMode = LockOnHelper.LockOnMode.ThreeDS;

		// Token: 0x0400111D RID: 4381
		private static bool _enabled;

		// Token: 0x0400111E RID: 4382
		private static bool _canLockOn;

		// Token: 0x0400111F RID: 4383
		private static List<int> _targets = new List<int>();

		// Token: 0x04001120 RID: 4384
		private static int _pickedTarget;

		// Token: 0x04001121 RID: 4385
		private static int _lifeTimeCounter;

		// Token: 0x04001122 RID: 4386
		private static int _lifeTimeArrowDisplay;

		// Token: 0x04001123 RID: 4387
		private static int _threeDSTarget = -1;

		// Token: 0x04001124 RID: 4388
		private static int _targetClosestTarget = -1;

		// Token: 0x04001125 RID: 4389
		public static bool ForceUsability = false;

		// Token: 0x04001126 RID: 4390
		private static float[,] _drawProgress = new float[Main.maxNPCs, 2];

		// Token: 0x02000688 RID: 1672
		public enum LockOnMode
		{
			// Token: 0x04006758 RID: 26456
			FocusTarget,
			// Token: 0x04006759 RID: 26457
			TargetClosest,
			// Token: 0x0400675A RID: 26458
			ThreeDS
		}
	}
}
