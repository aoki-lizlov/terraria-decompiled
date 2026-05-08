using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.ID;

namespace Terraria.DataStructures
{
	// Token: 0x020005A0 RID: 1440
	public static class PlayerDrawHeadLayers
	{
		// Token: 0x060038EA RID: 14570 RVA: 0x00009E46 File Offset: 0x00008046
		public static void DrawPlayer_0_(ref PlayerDrawHeadSet drawinfo)
		{
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x0064E868 File Offset: 0x0064CA68
		public static void DrawPlayer_00_BackHelmet(ref PlayerDrawHeadSet drawinfo)
		{
			if (drawinfo.drawPlayer.head < 0 || drawinfo.drawPlayer.head >= ArmorIDs.Head.Count)
			{
				return;
			}
			int num = ArmorIDs.Head.Sets.FrontToBackID[drawinfo.drawPlayer.head];
			if (num < 0)
			{
				return;
			}
			Rectangle hairFrame = drawinfo.HairFrame;
			PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, TextureAssets.ArmorHead[num].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x0064E994 File Offset: 0x0064CB94
		public static void DrawPlayer_01_FaceSkin(ref PlayerDrawHeadSet drawinfo)
		{
			bool flag = drawinfo.drawPlayer.head >= 0 && ArmorIDs.Head.Sets.HidesHead[drawinfo.drawPlayer.head];
			if (!flag && drawinfo.drawPlayer.faceHead > 0 && drawinfo.drawPlayer.faceHead < ArmorIDs.Face.Count)
			{
				Vector2 faceHeadOffsetFromHelmet = drawinfo.drawPlayer.GetFaceHeadOffsetFromHelmet();
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFaceHead, TextureAssets.AccFace[(int)drawinfo.drawPlayer.faceHead].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + faceHeadOffsetFromHelmet, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				if (drawinfo.drawPlayer.face > 0 && drawinfo.drawPlayer.face < ArmorIDs.Face.Count && ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int)drawinfo.drawPlayer.face])
				{
					float num = 0f;
					if (drawinfo.drawPlayer.face == 5)
					{
						sbyte faceHead = drawinfo.drawPlayer.faceHead;
						if (faceHead - 10 <= 3)
						{
							num = (float)(2 * drawinfo.drawPlayer.direction);
						}
					}
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFace, TextureAssets.AccFace[(int)drawinfo.drawPlayer.face].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))) + num, drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
					return;
				}
			}
			else if (!flag && !drawinfo.drawPlayer.isHatRackDoll)
			{
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.skinDyePacked, TextureAssets.Players[drawinfo.skinVar, 0].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, TextureAssets.Players[drawinfo.skinVar, 1].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorEyeWhites, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, TextureAssets.Players[drawinfo.skinVar, 2].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorEyes, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				if (drawinfo.drawPlayer.yoraiz0rDarkness)
				{
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.skinDyePacked, TextureAssets.Extra[67].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
				if (drawinfo.drawPlayer.face > 0 && drawinfo.drawPlayer.face < ArmorIDs.Face.Count && ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int)drawinfo.drawPlayer.face])
				{
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFace, TextureAssets.AccFace[(int)drawinfo.drawPlayer.face].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
			}
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x0064F104 File Offset: 0x0064D304
		public static void DrawPlayer_02_DrawArmorWithFullHair(ref PlayerDrawHeadSet drawinfo)
		{
			bool flag = drawinfo.drawPlayer.faceMask > 0 && drawinfo.drawPlayer.faceMask < ArmorIDs.Face.Count;
			if (flag && drawinfo.drawPlayer.head > 0 && drawinfo.drawPlayer.head < ArmorIDs.Head.Count && !ArmorIDs.Head.Sets.DrawFaceMaskUnderHeadLayer[drawinfo.drawPlayer.head])
			{
				flag = false;
			}
			if (flag)
			{
				Vector2 vector = drawinfo.drawPlayer.GetFaceDrawOffset((int)drawinfo.drawPlayer.faceMask) * drawinfo.drawPlayer.Directions;
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFaceMask, TextureAssets.AccFace[(int)drawinfo.drawPlayer.faceMask].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
			if (drawinfo.fullHair)
			{
				Color color = drawinfo.colorArmorHead;
				int num = drawinfo.cHead;
				if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
				{
					if (drawinfo.drawPlayer.isDisplayDollOrInanimate)
					{
						color = drawinfo.colorDisplayDollSkin;
					}
					else
					{
						color = drawinfo.colorHead;
					}
					num = drawinfo.skinDyePacked;
				}
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, num, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.HairFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				if (!drawinfo.hideHair)
				{
					Rectangle hairFrame = drawinfo.HairFrame;
					hairFrame.Y -= 336;
					if (hairFrame.Y < 0)
					{
						hairFrame.Y = 0;
					}
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
			}
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x0064F4DC File Offset: 0x0064D6DC
		public static void DrawPlayer_03_HelmetHair(ref PlayerDrawHeadSet drawinfo)
		{
			if (drawinfo.hideHair)
			{
				return;
			}
			if (drawinfo.hatHair)
			{
				Rectangle hairFrame = drawinfo.HairFrame;
				hairFrame.Y -= 336;
				if (hairFrame.Y < 0)
				{
					hairFrame.Y = 0;
				}
				if (!drawinfo.drawPlayer.invis)
				{
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHairAlt[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
			}
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x0064F614 File Offset: 0x0064D814
		public static void DrawPlayer_04_CapricornMask(ref PlayerDrawHeadSet drawinfo)
		{
			Rectangle hairFrame = drawinfo.HairFrame;
			hairFrame.Width += 2;
			PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x0064F720 File Offset: 0x0064D920
		public static void DrawPlayer_04_RabbitOrder(ref PlayerDrawHeadSet drawinfo)
		{
			int num = 27;
			Texture2D value = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
			Rectangle rectangle = value.Frame(1, num, 0, drawinfo.drawPlayer.rabbitOrderFrame.DisplayFrame, 0, 0);
			Vector2 vector = rectangle.Size() / 2f;
			int num2 = 1;
			Vector2 vector2 = PlayerDrawHeadLayers.DrawPlayer_04_GetHatDrawPosition(ref drawinfo, new Vector2(1f, -26f), num2);
			int hatStacks = PlayerDrawHeadLayers.GetHatStacks(ref drawinfo, 4955);
			float num3 = 0.05235988f;
			float num4 = num3 * drawinfo.drawPlayer.position.X % 6.2831855f;
			for (int i = hatStacks - 1; i >= 0; i--)
			{
				float num5 = Vector2.UnitY.RotatedBy((double)(num4 + num3 * (float)i), default(Vector2)).X * ((float)i / 30f) * 2f - (float)(i * 2 * drawinfo.drawPlayer.direction);
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, value, vector2 + new Vector2(num5, (float)(i * -14) * drawinfo.scale), new Rectangle?(rectangle), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, vector, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
			if (!drawinfo.hideHair)
			{
				Rectangle hairFrame = drawinfo.HairFrame;
				hairFrame.Y -= 336;
				if (hairFrame.Y < 0)
				{
					hairFrame.Y = 0;
				}
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x0064F994 File Offset: 0x0064DB94
		public static void DrawPlayer_04_DeadCellsBeheadedHead(ref PlayerDrawHeadSet drawinfo)
		{
			Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
			int num = 9;
			int num2 = 4;
			int num3 = drawinfo.drawPlayer.miscCounter % (num * num2) / num2;
			bodyFrame.Y = bodyFrame.Height * num3;
			Vector2 vector = drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect;
			PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, vector, new Rectangle?(bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x0064FAC4 File Offset: 0x0064DCC4
		public static void DrawPlayer_04_BadgersHat(ref PlayerDrawHeadSet drawinfo)
		{
			int num = 6;
			Texture2D value = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
			Rectangle rectangle = value.Frame(1, num, 0, drawinfo.drawPlayer.rabbitOrderFrame.DisplayFrame, 0, 0);
			Vector2 vector = rectangle.Size() / 2f;
			int num2 = 1;
			Vector2 vector2 = PlayerDrawHeadLayers.DrawPlayer_04_GetHatDrawPosition(ref drawinfo, new Vector2(0f, -9f), num2);
			int hatStacks = PlayerDrawHeadLayers.GetHatStacks(ref drawinfo, 5004);
			float num3 = 0.05235988f;
			float num4 = num3 * drawinfo.drawPlayer.position.X % 6.2831855f;
			int num5 = hatStacks * 4 + 2;
			int num6 = 0;
			bool flag = (Main.GlobalTimeWrappedHourly + 180f) % 600f < 60f;
			for (int i = num5 - 1; i >= 0; i--)
			{
				int num7 = 0;
				if (i == num5 - 1)
				{
					rectangle.Y = 0;
					num7 = 2;
				}
				else if (i == 0)
				{
					rectangle.Y = rectangle.Height * 5;
				}
				else
				{
					rectangle.Y = rectangle.Height * (num6++ % 4 + 1);
				}
				if (rectangle.Y != rectangle.Height * 3 || !flag)
				{
					float num8 = Vector2.UnitY.RotatedBy((double)(num4 + num3 * (float)i), default(Vector2)).X * ((float)i / 10f) * 4f - (float)i * 0.1f * (float)drawinfo.drawPlayer.direction;
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, value, vector2 + new Vector2(num8, (float)((i * -4 + num7) * num2)) * drawinfo.scale, new Rectangle?(rectangle), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, vector, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
			}
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x0064FCA8 File Offset: 0x0064DEA8
		private static Vector2 DrawPlayer_04_GetHatDrawPosition(ref PlayerDrawHeadSet drawinfo, Vector2 hatOffset, int usedGravDir)
		{
			Vector2 vector = new Vector2((float)drawinfo.drawPlayer.direction, (float)usedGravDir);
			return drawinfo.Position - Main.screenPosition + new Vector2((float)(-(float)drawinfo.bodyFrameMemory.Width / 2 + drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.bodyFrameMemory.Height + 4)) + hatOffset * vector * drawinfo.scale + (drawinfo.drawPlayer.headPosition + drawinfo.headVect);
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x0064FD50 File Offset: 0x0064DF50
		private static int GetHatStacks(ref PlayerDrawHeadSet drawinfo, int itemId)
		{
			int num = 0;
			int num2 = 0;
			if (drawinfo.drawPlayer.armor[num2] != null && drawinfo.drawPlayer.armor[num2].type == itemId && drawinfo.drawPlayer.armor[num2].stack > 0)
			{
				num += drawinfo.drawPlayer.armor[num2].stack;
			}
			num2 = 10;
			if (drawinfo.drawPlayer.armor[num2] != null && drawinfo.drawPlayer.armor[num2].type == itemId && drawinfo.drawPlayer.armor[num2].stack > 0)
			{
				num += drawinfo.drawPlayer.armor[num2].stack;
			}
			if (num > 2)
			{
				num = 2;
			}
			return num;
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x0064FE08 File Offset: 0x0064E008
		public static void DrawPlayer_04_HatsWithFullHair(ref PlayerDrawHeadSet drawinfo)
		{
			if (drawinfo.drawPlayer.head == 259)
			{
				PlayerDrawHeadLayers.DrawPlayer_04_RabbitOrder(ref drawinfo);
				return;
			}
			if (drawinfo.helmetIsOverFullHair)
			{
				if (!drawinfo.hideHair)
				{
					Rectangle hairFrame = drawinfo.HairFrame;
					hairFrame.Y -= 336;
					if (hairFrame.Y < 0)
					{
						hairFrame.Y = 0;
					}
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
				if (drawinfo.drawPlayer.head != 0)
				{
					Color color = drawinfo.colorArmorHead;
					int num = drawinfo.cHead;
					if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
					{
						if (drawinfo.drawPlayer.isDisplayDollOrInanimate)
						{
							color = drawinfo.colorDisplayDollSkin;
						}
						else
						{
							color = drawinfo.colorHead;
						}
						num = drawinfo.skinDyePacked;
					}
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, num, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
			}
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x00650090 File Offset: 0x0064E290
		public static void DrawPlayer_05_TallHats(ref PlayerDrawHeadSet drawinfo)
		{
			if (drawinfo.helmetIsTall)
			{
				Rectangle hairFrame = drawinfo.HairFrame;
				if (drawinfo.drawPlayer.head == 158)
				{
					hairFrame.Height -= 2;
				}
				int num = 0;
				if (hairFrame.Y == hairFrame.Height * 6)
				{
					hairFrame.Height -= 2;
				}
				else if (hairFrame.Y == hairFrame.Height * 7)
				{
					num = -2;
				}
				else if (hairFrame.Y == hairFrame.Height * 8)
				{
					num = -2;
				}
				else if (hairFrame.Y == hairFrame.Height * 9)
				{
					num = -2;
				}
				else if (hairFrame.Y == hairFrame.Height * 10)
				{
					num = -2;
				}
				else if (hairFrame.Y == hairFrame.Height * 13)
				{
					hairFrame.Height -= 2;
				}
				else if (hairFrame.Y == hairFrame.Height * 14)
				{
					num = -2;
				}
				else if (hairFrame.Y == hairFrame.Height * 15)
				{
					num = -2;
				}
				else if (hairFrame.Y == hairFrame.Height * 16)
				{
					num = -2;
				}
				hairFrame.Y += num;
				Color color = drawinfo.colorArmorHead;
				int num2 = drawinfo.cHead;
				if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
				{
					if (drawinfo.drawPlayer.isDisplayDollOrInanimate)
					{
						color = drawinfo.colorDisplayDollSkin;
					}
					else
					{
						color = drawinfo.colorHead;
					}
					num2 = drawinfo.skinDyePacked;
				}
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, num2, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f + (float)num) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x006502E0 File Offset: 0x0064E4E0
		public static void DrawPlayer_06_NormalHats(ref PlayerDrawHeadSet drawinfo)
		{
			if (drawinfo.drawPlayer.head == 270)
			{
				PlayerDrawHeadLayers.DrawPlayer_04_CapricornMask(ref drawinfo);
				return;
			}
			if (drawinfo.drawPlayer.head == 265)
			{
				PlayerDrawHeadLayers.DrawPlayer_04_BadgersHat(ref drawinfo);
				return;
			}
			if (drawinfo.drawPlayer.head == 282)
			{
				PlayerDrawHeadLayers.DrawPlayer_04_DeadCellsBeheadedHead(ref drawinfo);
				return;
			}
			if (drawinfo.helmetIsNormal)
			{
				Rectangle hairFrame = drawinfo.HairFrame;
				Color color = drawinfo.colorArmorHead;
				int num = drawinfo.cHead;
				if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
				{
					if (drawinfo.drawPlayer.isDisplayDollOrInanimate)
					{
						color = drawinfo.colorDisplayDollSkin;
					}
					else
					{
						color = drawinfo.colorHead;
					}
					num = drawinfo.skinDyePacked;
				}
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, num, TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				if (drawinfo.drawPlayer.head == 109)
				{
					Texture2D value = TextureAssets.Extra[276].Value;
					PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, 0, value, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(hairFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
				}
				if (drawinfo.drawPlayer.head == 271)
				{
					int num2 = PlayerDrawLayers.DrawPlayer_Head_GetTVScreen(drawinfo.drawPlayer);
					if (num2 != 0)
					{
						Texture2D value2 = TextureAssets.GlowMask[309].Value;
						int num3 = drawinfo.drawPlayer.miscCounter % 20 / 5;
						if (num2 == 5)
						{
							num3 = 0;
							if (drawinfo.drawPlayer.eyeHelper.EyeFrameToShow > 0)
							{
								num3 = 2;
							}
						}
						Rectangle rectangle = value2.Frame(6, 4, num2, num3, -2, 0);
						PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cHead, value2, drawinfo.helmetOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(rectangle), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
					}
				}
			}
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x006506B4 File Offset: 0x0064E8B4
		public static void DrawPlayer_07_JustHair(ref PlayerDrawHeadSet drawinfo)
		{
			if (!drawinfo.helmetIsNormal && !drawinfo.helmetIsOverFullHair && !drawinfo.helmetIsTall && !drawinfo.hideHair)
			{
				Rectangle hairFrame = drawinfo.HairFrame;
				hairFrame.Y -= 336;
				if (hairFrame.Y < 0)
				{
					hairFrame.Y = 0;
				}
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.hairShaderPacked, TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset, new Rectangle?(hairFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x00650800 File Offset: 0x0064EA00
		public static void DrawPlayer_08_FaceAcc(ref PlayerDrawHeadSet drawinfo)
		{
			bool flag = drawinfo.drawPlayer.head < 0 || !ArmorIDs.Head.Sets.PreventBeardDraw[drawinfo.drawPlayer.head];
			if (drawinfo.drawPlayer.beard > 0 && flag)
			{
				Vector2 beardDrawOffset = drawinfo.drawPlayer.GetBeardDrawOffset(true);
				Color color = drawinfo.colorArmorHead;
				if (ArmorIDs.Beard.Sets.UseHairColor[(int)drawinfo.drawPlayer.beard])
				{
					color = drawinfo.colorHair;
				}
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cBeard, TextureAssets.AccBeard[(int)drawinfo.drawPlayer.beard].Value, beardDrawOffset + new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
			bool flag2 = drawinfo.drawPlayer.head == 287;
			if (drawinfo.drawPlayer.face > 0 && drawinfo.drawPlayer.face < ArmorIDs.Face.Count && !ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int)drawinfo.drawPlayer.face] && (drawinfo.drawPlayer.face != 23 || !flag2))
			{
				Vector2 vector = drawinfo.drawPlayer.GetFaceDrawOffset((int)drawinfo.drawPlayer.face) * drawinfo.drawPlayer.Directions;
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFace, TextureAssets.AccFace[(int)drawinfo.drawPlayer.face].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
			bool flag3 = drawinfo.drawPlayer.faceMask > 0 && drawinfo.drawPlayer.faceMask < ArmorIDs.Face.Count;
			if (flag3 && drawinfo.drawPlayer.head > 0 && drawinfo.drawPlayer.head < ArmorIDs.Head.Count && (ArmorIDs.Head.Sets.PreventFaceMaskDraw[drawinfo.drawPlayer.head] || ArmorIDs.Head.Sets.DrawFaceMaskUnderHeadLayer[drawinfo.drawPlayer.head]))
			{
				flag3 = false;
			}
			if (flag3)
			{
				Vector2 vector2 = drawinfo.drawPlayer.GetFaceDrawOffset((int)drawinfo.drawPlayer.faceMask) * drawinfo.drawPlayer.Directions;
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFaceMask, TextureAssets.AccFace[(int)drawinfo.drawPlayer.faceMask].Value, vector2 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
			bool flag4 = drawinfo.drawPlayer.faceFlower > 0 && drawinfo.drawPlayer.faceFlower < ArmorIDs.Face.Count;
			if (flag4 && drawinfo.drawPlayer.head > 0 && drawinfo.drawPlayer.head < ArmorIDs.Head.Count && ArmorIDs.Head.Sets.PreventFaceFlowerDraw[drawinfo.drawPlayer.head])
			{
				flag4 = false;
			}
			if (flag4)
			{
				Vector2 vector3 = drawinfo.drawPlayer.GetFaceDrawOffset((int)drawinfo.drawPlayer.faceFlower) * drawinfo.drawPlayer.Directions;
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cFaceFlower, TextureAssets.AccFace[(int)drawinfo.drawPlayer.faceFlower].Value, vector3 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
			if (drawinfo.drawUnicornHorn)
			{
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cUnicornHorn, TextureAssets.Extra[143].Value, new Vector2(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
			if (drawinfo.drawAngelHalo)
			{
				Main.instance.LoadAccFace(7);
				PlayerDrawHeadLayers.QuickCDD(drawinfo.DrawData, drawinfo.cAngelHalo, TextureAssets.AccFace[7].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.bodyFrameMemory.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.bodyFrameMemory.Height + 4f) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.bodyFrameMemory), new Color(200, 200, 200, 200), drawinfo.drawPlayer.headRotation, drawinfo.headVect, drawinfo.scale, drawinfo.playerEffect, 0f);
			}
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x00650FC8 File Offset: 0x0064F1C8
		public static void DrawPlayer_RenderAllLayers(ref PlayerDrawHeadSet drawinfo)
		{
			List<DrawData> drawData = drawinfo.DrawData;
			Effect pixelShader = Main.pixelShader;
			Projectile[] projectile = Main.projectile;
			SpriteBatch spriteBatch = Main.spriteBatch;
			for (int i = 0; i < drawData.Count; i++)
			{
				DrawData drawData2 = drawData[i];
				if (drawData2.sourceRect == null)
				{
					drawData2.sourceRect = new Rectangle?(drawData2.texture.Frame(1, 1, 0, 0, 0, 0));
				}
				PlayerDrawHelper.SetShaderForData(drawinfo.drawPlayer, drawinfo.cHead, ref drawData2);
				if (drawData2.texture != null)
				{
					drawData2.Draw(spriteBatch);
				}
			}
			pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x00651070 File Offset: 0x0064F270
		public static void DrawPlayer_DrawSelectionRect(ref PlayerDrawHeadSet drawinfo)
		{
			Vector2 vector;
			Vector2 vector2;
			SpriteRenderTargetHelper.GetDrawBoundary(drawinfo.DrawData, out vector, out vector2);
			Utils.DrawRect(Main.spriteBatch, vector + Main.screenPosition, vector2 + Main.screenPosition, Color.White);
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x006510B4 File Offset: 0x0064F2B4
		public static void QuickCDD(List<DrawData> drawData, Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
		{
			drawData.Add(new DrawData(texture, position, sourceRectangle, color, rotation, origin, scale, effects, 0f));
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x006510E0 File Offset: 0x0064F2E0
		public static void QuickCDD(List<DrawData> drawData, int shaderTechnique, Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
		{
			drawData.Add(new DrawData(texture, position, sourceRectangle, color, rotation, origin, scale, effects, 0f)
			{
				shader = shaderTechnique
			});
		}
	}
}
