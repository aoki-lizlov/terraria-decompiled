using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.GameContent.Events;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.DataStructures
{
	// Token: 0x0200059E RID: 1438
	public static class PlayerDrawLayers
	{
		// Token: 0x0600388D RID: 14477 RVA: 0x006395AA File Offset: 0x006377AA
		public static void DrawPlayer_extra_TorsoPlus(ref PlayerDrawSet drawinfo)
		{
			drawinfo.Position.Y = drawinfo.Position.Y + drawinfo.torsoOffset;
			drawinfo.ItemLocation.Y = drawinfo.ItemLocation.Y + drawinfo.torsoOffset;
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x006395D6 File Offset: 0x006377D6
		public static void DrawPlayer_extra_TorsoMinus(ref PlayerDrawSet drawinfo)
		{
			drawinfo.Position.Y = drawinfo.Position.Y - drawinfo.torsoOffset;
			drawinfo.ItemLocation.Y = drawinfo.ItemLocation.Y - drawinfo.torsoOffset;
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x00639602 File Offset: 0x00637802
		public static void DrawPlayer_extra_MountPlus(ref PlayerDrawSet drawinfo)
		{
			drawinfo.Position.Y = drawinfo.Position.Y + (float)((int)drawinfo.mountOffSet / 2);
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x0063961D File Offset: 0x0063781D
		public static void DrawPlayer_extra_MountMinus(ref PlayerDrawSet drawinfo)
		{
			drawinfo.Position.Y = drawinfo.Position.Y - (float)((int)drawinfo.mountOffSet / 2);
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x00639638 File Offset: 0x00637838
		public static void DrawCompositeArmorPiece(ref PlayerDrawSet drawinfo, CompositePlayerDrawContext context, DrawData data, int bodyIndex)
		{
			drawinfo.DrawDataCache.Add(data);
			if (drawinfo.hideEntirePlayer || drawinfo.hideEntirePlayerExceptHelmetsAndFaceAccessories)
			{
				return;
			}
			switch (context)
			{
			case CompositePlayerDrawContext.BackShoulder:
			case CompositePlayerDrawContext.BackArm:
			case CompositePlayerDrawContext.FrontArm:
			case CompositePlayerDrawContext.FrontShoulder:
				if (drawinfo.armGlowColor.PackedValue > 0U)
				{
					DrawData drawData = data;
					drawData.color = drawinfo.armGlowColor;
					Rectangle value = drawData.sourceRect.Value;
					value.Y += 224;
					drawData.sourceRect = new Rectangle?(value);
					if (bodyIndex == 227)
					{
						Vector2 position = drawData.position;
						for (int i = 0; i < 2; i++)
						{
							Vector2 vector = new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
							drawData.position = position + vector;
							if (i == 0)
							{
								drawinfo.DrawDataCache.Add(drawData);
							}
						}
					}
					drawinfo.DrawDataCache.Add(drawData);
				}
				break;
			case CompositePlayerDrawContext.Torso:
				if (drawinfo.bodyGlowColor.PackedValue > 0U)
				{
					DrawData drawData2 = data;
					drawData2.color = drawinfo.bodyGlowColor;
					Rectangle value2 = drawData2.sourceRect.Value;
					value2.Y += 224;
					drawData2.sourceRect = new Rectangle?(value2);
					if (bodyIndex == 227)
					{
						Vector2 position2 = drawData2.position;
						for (int j = 0; j < 2; j++)
						{
							Vector2 vector2 = new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
							drawData2.position = position2 + vector2;
							if (j == 0)
							{
								drawinfo.DrawDataCache.Add(drawData2);
							}
						}
					}
					drawinfo.DrawDataCache.Add(drawData2);
				}
				break;
			}
			if (context == CompositePlayerDrawContext.FrontShoulder && drawinfo.drawPlayer.head == 269)
			{
				Vector2 vector3 = drawinfo.helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect;
				drawinfo.drawPlayer.ApplyHeadOffsetFromMount(ref vector3);
				DrawData drawData3 = new DrawData(TextureAssets.Extra[214].Value, vector3, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData3.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(drawData3);
				drawData3 = new DrawData(TextureAssets.GlowMask[308].Value, vector3, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData3.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(drawData3);
			}
			if (context == CompositePlayerDrawContext.FrontArm && bodyIndex == 205)
			{
				Color immuneAlphaPure = drawinfo.drawPlayer.GetImmuneAlphaPure(new Color(100, 100, 100, 0), drawinfo.shadow);
				ulong num = (ulong)((long)(drawinfo.drawPlayer.miscCounter / 4));
				int num2 = 4;
				for (int k = 0; k < num2; k++)
				{
					float num3 = (float)Utils.RandomInt(ref num, -10, 11) * 0.2f;
					float num4 = (float)Utils.RandomInt(ref num, -10, 1) * 0.15f;
					DrawData drawData4 = data;
					Rectangle value3 = drawData4.sourceRect.Value;
					value3.Y += 224;
					drawData4.sourceRect = new Rectangle?(value3);
					drawData4.position.X = drawData4.position.X + num3;
					drawData4.position.Y = drawData4.position.Y + num4;
					drawData4.color = immuneAlphaPure;
					drawinfo.DrawDataCache.Add(drawData4);
				}
			}
			if (bodyIndex == 251)
			{
				DrawData drawData5 = data;
				drawData5.texture = TextureAssets.GlowMask[364].Value;
				drawData5.color = PlayerDrawLayers.GetChickenBonesGlowColor(ref drawinfo, true, false);
				float num5 = drawinfo.stealth * drawinfo.stealth;
				num5 *= 1f - drawinfo.shadow;
				drawData5.color = Color.Multiply(drawData5.color, num5);
				drawinfo.DrawDataCache.Add(drawData5);
				return;
			}
			if (bodyIndex == 259)
			{
				DrawData drawData6 = data;
				drawData6.texture = TextureAssets.GlowMask[376].Value;
				drawData6.color = drawinfo.drawPlayer.GetImmuneAlphaPure(Color.White, drawinfo.shadow);
				float num6 = drawinfo.stealth * drawinfo.stealth;
				num6 *= 1f - drawinfo.shadow;
				drawData6.color = Color.Multiply(drawData6.color, num6);
				drawinfo.DrawDataCache.Add(drawData6);
			}
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x00639B98 File Offset: 0x00637D98
		public static Color GetChickenBonesGlowColor(ref PlayerDrawSet drawinfo, bool scaleByShadow, bool wings = false)
		{
			if (drawinfo.hideEntirePlayer || drawinfo.hideEntirePlayerExceptHelmetsAndFaceAccessories)
			{
				return Color.Transparent;
			}
			Color color = new Color(255, 255, 255, 0);
			if (!wings)
			{
				float num = Utils.Remap(Utils.WrappedLerp(0f, 1f, (float)(drawinfo.drawPlayer.miscCounter % 100) / 100f), 0f, 1f, 0.8f, 1f, true);
				color *= num;
			}
			if (!scaleByShadow)
			{
				return color;
			}
			return drawinfo.drawPlayer.GetImmuneAlphaPure(color, drawinfo.shadow);
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x00639C34 File Offset: 0x00637E34
		public static Color GetLunaGlowColor(ref PlayerDrawSet drawinfo, bool scaleByShadow)
		{
			if (drawinfo.hideEntirePlayer || drawinfo.hideEntirePlayerExceptHelmetsAndFaceAccessories)
			{
				return Color.Transparent;
			}
			Color color = new Color(255, 255, 255, 100);
			float num = Utils.Remap(Utils.WrappedLerp(0f, 1f, (float)(drawinfo.drawPlayer.miscCounter % 100) / 100f), 0f, 1f, 0.85f, 1f, true);
			color *= num;
			if (!scaleByShadow)
			{
				return color;
			}
			return drawinfo.drawPlayer.GetImmuneAlphaPure(color, drawinfo.shadow);
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x00639CD0 File Offset: 0x00637ED0
		public static void DrawPlayer_01_BackHair(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.hideHair && drawinfo.backHairDraw)
			{
				Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + drawinfo.hairOffset;
				if (drawinfo.drawPlayer.head == -1 || drawinfo.fullHair || drawinfo.drawsBackHairWithoutHeadgear)
				{
					DrawData drawData = new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, vector, new Rectangle?(drawinfo.hairBackFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.hairDyePacked;
					drawinfo.DrawDataCache.Add(drawData);
					return;
				}
				if (drawinfo.hatHair)
				{
					DrawData drawData = new DrawData(TextureAssets.PlayerHairAlt[drawinfo.drawPlayer.hair].Value, vector, new Rectangle?(drawinfo.hairBackFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.hairDyePacked;
					drawinfo.DrawDataCache.Add(drawData);
				}
			}
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x00639E90 File Offset: 0x00638090
		public static void DrawPlayer_02_MountBehindPlayer(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.mount.Active)
			{
				PlayerDrawLayers.DrawMeowcartTrail(ref drawinfo);
				PlayerDrawLayers.DrawTiedBalloons(ref drawinfo);
				drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 0, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
				drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 1, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
			}
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x00639F28 File Offset: 0x00638128
		public static void DrawPlayer_03_Carpet(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.carpetFrame >= 0)
			{
				Color colorArmorLegs = drawinfo.colorArmorLegs;
				float num = 0f;
				if (drawinfo.drawPlayer.gravDir == -1f)
				{
					num = 10f;
				}
				DrawData drawData = new DrawData(TextureAssets.FlyingCarpet.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2) + 28f * drawinfo.drawPlayer.gravDir + num))), new Rectangle?(new Rectangle(0, TextureAssets.FlyingCarpet.Height() / 6 * drawinfo.drawPlayer.carpetFrame, TextureAssets.FlyingCarpet.Width(), TextureAssets.FlyingCarpet.Height() / 6)), colorArmorLegs, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.FlyingCarpet.Width() / 2), (float)(TextureAssets.FlyingCarpet.Height() / 8)), 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cCarpet;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x0063A070 File Offset: 0x00638270
		public static void DrawPlayer_03_PortableStool(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.portableStoolInfo.IsInUse && drawinfo.shadow == 0f)
			{
				Texture2D value = TextureAssets.Extra[102].Value;
				Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height + 28f)));
				Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
				Vector2 vector2 = rectangle.Size() * new Vector2(0.5f, 1f);
				DrawData drawData = new DrawData(value, vector, new Rectangle?(rectangle), drawinfo.colorArmorLegs, drawinfo.drawPlayer.bodyRotation, vector2, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cPortableStool;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x0063A180 File Offset: 0x00638380
		public static void DrawPlayer_04_ElectrifiedDebuffBack(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.electrified && drawinfo.shadow == 0f)
			{
				Texture2D value = TextureAssets.GlowMask[25].Value;
				int num = drawinfo.drawPlayer.miscCounter / 5;
				for (int i = 0; i < 2; i++)
				{
					num %= 7;
					if (num <= 1 || num >= 5)
					{
						DrawData drawData = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(new Rectangle(0, num * value.Height / 7, value.Width, value.Height / 7)), drawinfo.colorElectricity, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(value.Width / 2), (float)(value.Height / 14)), 1f, drawinfo.playerEffect, 0f);
						drawinfo.DrawDataCache.Add(drawData);
					}
					num += 3;
				}
			}
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x0063A31C File Offset: 0x0063851C
		public static void DrawPlayer_05_ForbiddenSetRing(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.setForbidden && drawinfo.shadow == 0f)
			{
				Color color = Color.Lerp(drawinfo.colorArmorBody, Color.White, 0.7f);
				Texture2D value = TextureAssets.Extra[74].Value;
				Texture2D value2 = TextureAssets.GlowMask[217].Value;
				bool flag = !drawinfo.drawPlayer.setForbiddenCooldownLocked;
				int num = (int)(((float)drawinfo.drawPlayer.miscCounter / 300f * 6.2831855f).ToRotationVector2().Y * 6f);
				float num2 = ((float)drawinfo.drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 4f;
				Color color2 = new Color(80, 70, 40, 0) * (num2 / 8f + 0.5f) * 0.8f;
				if (!flag)
				{
					num = 0;
					num2 = 2f;
					color2 = new Color(80, 70, 40, 0) * 0.3f;
					color = color.MultiplyRGB(new Color(0.5f, 0.5f, 1f));
				}
				Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
				int num3 = 10;
				int num4 = 20;
				if (drawinfo.drawPlayer.head == 238)
				{
					num3 += 4;
					num4 += 4;
				}
				vector += new Vector2((float)(-(float)drawinfo.drawPlayer.direction * num3), (float)(-(float)num4) * drawinfo.drawPlayer.gravDir + (float)num * drawinfo.drawPlayer.gravDir);
				DrawData drawData = new DrawData(value, vector, null, color, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cBody;
				drawinfo.DrawDataCache.Add(drawData);
				for (float num5 = 0f; num5 < 4f; num5 += 1f)
				{
					drawData = new DrawData(value2, vector + (num5 * 1.5707964f).ToRotationVector2() * num2, null, color2, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(drawData);
				}
			}
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x0063A64C File Offset: 0x0063884C
		public static void DrawPlayer_01_3_BackHead(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.head >= 0 && drawinfo.drawPlayer.head < ArmorIDs.Head.Count)
			{
				int num = ArmorIDs.Head.Sets.FrontToBackID[drawinfo.drawPlayer.head];
				if (num >= 0)
				{
					Vector2 helmetOffset = drawinfo.helmetOffset;
					drawinfo.drawPlayer.ApplyHeadOffsetFromMount(ref helmetOffset);
					DrawData drawData = new DrawData(TextureAssets.ArmorHead[num].Value, helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cHead;
					drawinfo.DrawDataCache.Add(drawData);
				}
			}
			if (drawinfo.drawPlayer.face > 0 && drawinfo.drawPlayer.face == 23)
			{
				PlayerDrawLayers.DrawPlayer_ChippysHeadband(ref drawinfo);
			}
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x0063A7D0 File Offset: 0x006389D0
		public static void DrawPlayer_01_2_JimsCloak(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.legs == 60 && !drawinfo.isSitting && !drawinfo.drawPlayer.invis && (!PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo) || drawinfo.drawPlayer.wearsRobe))
			{
				DrawData drawData = new DrawData(TextureAssets.Extra[153].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cLegs;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x0063A924 File Offset: 0x00638B24
		public static void DrawPlayer_05_2_SafemanSun(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.head == 238 && drawinfo.shadow == 0f)
			{
				Color color = Color.Lerp(drawinfo.colorArmorBody, Color.White, 0.7f);
				Texture2D value = TextureAssets.Extra[152].Value;
				Texture2D value2 = TextureAssets.Extra[152].Value;
				int num = (int)(((float)drawinfo.drawPlayer.miscCounter / 300f * 6.2831855f).ToRotationVector2().Y * 6f);
				float num2 = ((float)drawinfo.drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 4f;
				Color color2 = new Color(80, 70, 40, 0) * (num2 / 8f + 0.5f) * 0.8f;
				Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
				int num3 = 8;
				int num4 = 20;
				num3 += 4;
				num4 += 4;
				if (drawinfo.drawPlayer.mount.Active)
				{
					int type = drawinfo.drawPlayer.mount.Type;
					if (type - 55 <= 1 || type == 61)
					{
						num4 -= 22;
					}
				}
				vector += new Vector2((float)(-(float)drawinfo.drawPlayer.direction * num3), (float)(-(float)num4) * drawinfo.drawPlayer.gravDir + (float)num * drawinfo.drawPlayer.gravDir);
				DrawData drawData = new DrawData(value, vector, null, color, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(drawData);
				for (float num5 = 0f; num5 < 4f; num5 += 1f)
				{
					drawData = new DrawData(value2, vector + (num5 * 1.5707964f).ToRotationVector2() * num2, null, color2, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cHead;
					drawinfo.DrawDataCache.Add(drawData);
				}
			}
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x0063AC48 File Offset: 0x00638E48
		public static void DrawPlayer_06_WebbedDebuffBack(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.webbed && drawinfo.shadow == 0f && drawinfo.drawPlayer.velocity.Y != 0f)
			{
				Color color = drawinfo.colorArmorBody * 0.75f;
				Texture2D value = TextureAssets.Extra[32].Value;
				DrawData drawData = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), null, color, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x0063ADB4 File Offset: 0x00638FB4
		public static void DrawPlayer_07_LeinforsHairShampoo(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.drawPlayer.leinforsHair || (!drawinfo.fullHair && !drawinfo.hatHair && !drawinfo.drawsBackHairWithoutHeadgear && drawinfo.drawPlayer.head != -1 && drawinfo.drawPlayer.head != 0) || drawinfo.drawPlayer.hair == 12 || drawinfo.shadow != 0f || Main.rgbToHsl(drawinfo.colorHead).Z <= 0.2f)
			{
				return;
			}
			if (Main.rand.Next(20) == 0 && !drawinfo.hatHair)
			{
				Rectangle rectangle = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2(0f, drawinfo.drawPlayer.gravDir * -20f), new Vector2(20f, 14f));
				int num = Dust.NewDust(rectangle.TopLeft(), rectangle.Width, rectangle.Height, 204, 0f, 0f, 150, default(Color), 0.3f);
				Main.dust[num].fadeIn = 1f;
				Main.dust[num].velocity *= 0.1f;
				Main.dust[num].noLight = true;
				Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cLeinShampoo, drawinfo.drawPlayer);
				drawinfo.DustCache.Add(num);
			}
			if (Main.rand.Next(40) == 0 && drawinfo.hatHair)
			{
				Rectangle rectangle2 = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2((float)(drawinfo.drawPlayer.direction * -10), drawinfo.drawPlayer.gravDir * -10f), new Vector2(5f, 5f));
				int num2 = Dust.NewDust(rectangle2.TopLeft(), rectangle2.Width, rectangle2.Height, 204, 0f, 0f, 150, default(Color), 0.3f);
				Main.dust[num2].fadeIn = 1f;
				Main.dust[num2].velocity *= 0.1f;
				Main.dust[num2].noLight = true;
				Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cLeinShampoo, drawinfo.drawPlayer);
				drawinfo.DustCache.Add(num2);
			}
			if (drawinfo.drawPlayer.velocity.X != 0f && drawinfo.backHairDraw && Main.rand.Next(15) == 0)
			{
				Rectangle rectangle3 = Utils.CenteredRectangle(drawinfo.Position + drawinfo.drawPlayer.Size / 2f + new Vector2((float)(drawinfo.drawPlayer.direction * -14), 0f), new Vector2(4f, 30f));
				int num3 = Dust.NewDust(rectangle3.TopLeft(), rectangle3.Width, rectangle3.Height, 204, 0f, 0f, 150, default(Color), 0.3f);
				Main.dust[num3].fadeIn = 1f;
				Main.dust[num3].velocity *= 0.1f;
				Main.dust[num3].noLight = true;
				Main.dust[num3].shader = GameShaders.Armor.GetSecondaryShader(drawinfo.drawPlayer.cLeinShampoo, drawinfo.drawPlayer);
				drawinfo.DustCache.Add(num3);
			}
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x0063B1AF File Offset: 0x006393AF
		public static bool DrawPlayer_08_PlayerVisuallyHasFullArmorSet(PlayerDrawSet drawinfo, int head, int body, int legs)
		{
			return drawinfo.drawPlayer.head == head && drawinfo.drawPlayer.body == body && drawinfo.drawPlayer.legs == legs;
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x0063B1E0 File Offset: 0x006393E0
		public static void DrawPlayer_08_Backpacks(ref PlayerDrawSet drawinfo)
		{
			if (PlayerDrawLayers.DrawPlayer_08_PlayerVisuallyHasFullArmorSet(drawinfo, 266, 235, 218))
			{
				Vector2 vector = new Vector2(-2f + -2f * drawinfo.drawPlayer.Directions.X, 0f) + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2));
				vector = vector.Floor();
				Texture2D value = TextureAssets.Extra[212].Value;
				Rectangle rectangle = value.Frame(1, 5, 0, drawinfo.drawPlayer.miscCounter % 25 / 5, 0, 0);
				Color color = drawinfo.drawPlayer.GetImmuneAlphaPure(new Color(250, 250, 250, 200), drawinfo.shadow);
				color *= drawinfo.drawPlayer.stealth;
				DrawData drawData = new DrawData(value, vector, new Rectangle?(rectangle), color, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cBody;
				drawinfo.DrawDataCache.Add(drawData);
			}
			if (PlayerDrawLayers.DrawPlayer_08_PlayerVisuallyHasFullArmorSet(drawinfo, 268, 237, 222))
			{
				Vector2 vector2 = new Vector2(-9f + 1f * drawinfo.drawPlayer.Directions.X, -4f * drawinfo.drawPlayer.Directions.Y) + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2));
				vector2 = vector2.Floor();
				Texture2D value2 = TextureAssets.Extra[213].Value;
				Rectangle rectangle2 = value2.Frame(1, 5, 0, drawinfo.drawPlayer.miscCounter % 25 / 5, 0, 0);
				Color color2 = drawinfo.drawPlayer.GetImmuneAlphaPure(new Color(250, 250, 250, 200), drawinfo.shadow);
				color2 *= drawinfo.drawPlayer.stealth;
				DrawData drawData = new DrawData(value2, vector2, new Rectangle?(rectangle2), color2, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cBody;
				drawinfo.DrawDataCache.Add(drawData);
			}
			if (drawinfo.heldItem.type == 4818 && drawinfo.drawPlayer.ownedProjectileCounts[902] == 0)
			{
				int num = 8;
				Vector2 vector3 = new Vector2(0f, 8f);
				Vector2 vector4 = drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + vector3;
				vector4 = vector4.Floor();
				DrawData drawData = new DrawData(TextureAssets.BackPack[num].Value, vector4, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
			}
			if (drawinfo.drawPlayer.backpack > 0 && (int)drawinfo.drawPlayer.backpack < ArmorIDs.Back.Count && (!drawinfo.drawPlayer.mount.Active || (drawinfo.drawPlayer.mount.Type >= 0 && MountID.Sets.DoesNotOverrideBackpackDraw[drawinfo.drawPlayer.mount.Type])))
			{
				Vector2 vector5 = new Vector2(0f, 8f);
				Vector2 vector6 = drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + vector5;
				vector6 = vector6.Floor();
				DrawData drawData = new DrawData(TextureAssets.AccBack[(int)drawinfo.drawPlayer.backpack].Value, vector6, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cBackpack;
				drawinfo.DrawDataCache.Add(drawData);
				return;
			}
			if (drawinfo.heldItem.type == 1178 || drawinfo.heldItem.type == 779 || drawinfo.heldItem.type == 5134 || drawinfo.heldItem.type == 1295 || drawinfo.heldItem.type == 1910 || drawinfo.drawPlayer.turtleArmor || drawinfo.drawPlayer.body == 106 || drawinfo.drawPlayer.body == 170)
			{
				int type = drawinfo.heldItem.type;
				int num2 = 1;
				float num3 = -4f;
				float num4 = -8f;
				int num5 = 0;
				if (drawinfo.drawPlayer.turtleArmor)
				{
					num2 = 4;
					num5 = drawinfo.cBody;
				}
				else if (drawinfo.drawPlayer.body == 106)
				{
					num2 = 6;
					num5 = drawinfo.cBody;
				}
				else if (drawinfo.drawPlayer.body == 170)
				{
					num2 = 7;
					num5 = drawinfo.cBody;
				}
				else if (type == 1178)
				{
					num2 = 1;
				}
				else if (type == 779)
				{
					num2 = 2;
				}
				else if (type == 5134)
				{
					num2 = 9;
				}
				else if (type == 1295)
				{
					num2 = 3;
				}
				else if (type == 1910)
				{
					num2 = 5;
				}
				Vector2 vector7 = new Vector2(0f, 8f);
				Vector2 vector8 = drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + vector7;
				vector8 = vector8.Floor();
				Vector2 vector9 = drawinfo.Position - Main.screenPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2((-9f + num3) * (float)drawinfo.drawPlayer.direction, (2f + num4) * drawinfo.drawPlayer.gravDir) + vector7;
				vector9 = vector9.Floor();
				DrawData drawData;
				if (num2 == 7)
				{
					drawData = new DrawData(TextureAssets.BackPack[num2].Value, vector8, new Rectangle?(new Rectangle(0, drawinfo.drawPlayer.bodyFrame.Y, TextureAssets.BackPack[num2].Width(), drawinfo.drawPlayer.bodyFrame.Height)), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)TextureAssets.BackPack[num2].Width() * 0.5f, drawinfo.bodyVect.Y), 1f, drawinfo.playerEffect, 0f);
					drawData.shader = num5;
					drawinfo.DrawDataCache.Add(drawData);
					return;
				}
				if (num2 == 4 || num2 == 6)
				{
					drawData = new DrawData(TextureAssets.BackPack[num2].Value, vector8, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = num5;
					drawinfo.DrawDataCache.Add(drawData);
					return;
				}
				drawData = new DrawData(TextureAssets.BackPack[num2].Value, vector9, new Rectangle?(new Rectangle(0, 0, TextureAssets.BackPack[num2].Width(), TextureAssets.BackPack[num2].Height())), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.BackPack[num2].Width() / 2), (float)(TextureAssets.BackPack[num2].Height() / 2)), 1f, drawinfo.playerEffect, 0f);
				drawData.shader = num5;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x0063BB60 File Offset: 0x00639D60
		public static void DrawPlayer_08_1_Tails(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.tail > 0 && (int)drawinfo.drawPlayer.tail < ArmorIDs.Back.Count && !drawinfo.drawPlayer.mount.Active)
			{
				Vector2 zero = Vector2.Zero;
				if (drawinfo.isSitting)
				{
					zero.Y += -2f;
				}
				if (!drawinfo.drawPlayer.Male)
				{
					zero.X += (float)(2 * drawinfo.drawPlayer.direction);
				}
				Vector2 vector = new Vector2(0f, 8f);
				Vector2 vector2 = zero + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + vector;
				vector2 = vector2.Floor();
				DrawData drawData = new DrawData(TextureAssets.AccBack[(int)drawinfo.drawPlayer.tail].Value, vector2, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cTail;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x0063BCEC File Offset: 0x00639EEC
		public static void DrawPlayer_10_BackAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.back > 0 && (int)drawinfo.drawPlayer.back < ArmorIDs.Back.Count)
			{
				if (drawinfo.drawPlayer.front >= 1 && drawinfo.drawPlayer.front <= 4)
				{
					int num = drawinfo.drawPlayer.bodyFrame.Y / 56;
					if (num < 1 || num > 5)
					{
						drawinfo.armorAdjust = 10;
					}
					else
					{
						if (drawinfo.drawPlayer.front == 1)
						{
							drawinfo.armorAdjust = 0;
						}
						if (drawinfo.drawPlayer.front == 2)
						{
							drawinfo.armorAdjust = 8;
						}
						if (drawinfo.drawPlayer.front == 3)
						{
							drawinfo.armorAdjust = 0;
						}
						if (drawinfo.drawPlayer.front == 4)
						{
							drawinfo.armorAdjust = 8;
						}
					}
				}
				Vector2 zero = Vector2.Zero;
				Vector2 vector = new Vector2(0f, 8f);
				Vector2 vector2 = zero + drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + new Vector2(0f, -4f) + vector;
				vector2 = vector2.Floor();
				DrawData drawData = new DrawData(TextureAssets.AccBack[(int)drawinfo.drawPlayer.back].Value, vector2, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cBack;
				drawinfo.DrawDataCache.Add(drawData);
				if (drawinfo.drawPlayer.back == 36)
				{
					Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
					Rectangle rectangle = bodyFrame;
					rectangle.Width = 2;
					int num2 = 0;
					int num3 = bodyFrame.Width / 2;
					int num4 = 2;
					if ((drawinfo.playerEffect & SpriteEffects.FlipHorizontally) != SpriteEffects.None)
					{
						num2 = bodyFrame.Width - 2;
						num4 = -2;
					}
					for (int i = 0; i < num3; i++)
					{
						rectangle.X = bodyFrame.X + 2 * i;
						Color color = drawinfo.drawPlayer.GetImmuneAlpha(LiquidRenderer.GetShimmerGlitterColor(true, (float)i / 16f, 0f), drawinfo.shadow);
						color *= (float)drawinfo.colorArmorBody.A / 255f;
						drawData = new DrawData(TextureAssets.GlowMask[332].Value, vector2 + new Vector2((float)(num2 + i * num4), 0f), new Rectangle?(rectangle), color, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cBack;
						drawinfo.DrawDataCache.Add(drawData);
					}
				}
			}
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x0063BFF0 File Offset: 0x0063A1F0
		public static void DrawPlayer_09_Wings(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.dead || drawinfo.hideEntirePlayer || drawinfo.hideEntirePlayerExceptHelmetsAndFaceAccessories)
			{
				return;
			}
			Vector2 directions = drawinfo.drawPlayer.Directions;
			Vector2 vector = drawinfo.Position - Main.screenPosition + drawinfo.drawPlayer.Size / 2f;
			Vector2 vector2 = new Vector2(0f, 7f);
			vector = drawinfo.Position - Main.screenPosition + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) + vector2;
			if (drawinfo.drawPlayer.wings > 0)
			{
				Main.instance.LoadWings(drawinfo.drawPlayer.wings);
				if (drawinfo.drawPlayer.wings == 22)
				{
					if (drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated(false))
					{
						Main.instance.LoadItemFlames(1866);
						Color colorArmorBody = drawinfo.colorArmorBody;
						int num = 26;
						int num2 = -9;
						Vector2 vector3 = vector + new Vector2((float)num2, (float)num) * directions;
						DrawData drawData;
						if (drawinfo.shadow == 0f && drawinfo.drawPlayer.grappling[0] == -1)
						{
							for (int i = 0; i < 7; i++)
							{
								Color color = new Color(250 - i * 10, 250 - i * 10, 250 - i * 10, 150 - i * 10);
								Vector2 vector4 = new Vector2((float)Main.rand.Next(-10, 11) * 0.2f, (float)Main.rand.Next(-10, 11) * 0.2f);
								drawinfo.stealth *= drawinfo.stealth;
								drawinfo.stealth *= 1f - drawinfo.shadow;
								color = new Color((int)((float)color.R * drawinfo.stealth), (int)((float)color.G * drawinfo.stealth), (int)((float)color.B * drawinfo.stealth), (int)((float)color.A * drawinfo.stealth));
								vector4.X = drawinfo.drawPlayer.itemFlamePos[i].X;
								vector4.Y = -drawinfo.drawPlayer.itemFlamePos[i].Y;
								vector4 *= 0.5f;
								Vector2 vector5 = (vector3 + vector4).Floor();
								drawData = new DrawData(TextureAssets.ItemFlame[1866].Value, vector5, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 - 2)), color, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 14)), 1f, drawinfo.playerEffect, 0f);
								drawData.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(drawData);
							}
						}
						drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector3.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7)), colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 14)), 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(drawData);
						return;
					}
				}
				else if (drawinfo.drawPlayer.wings == 28)
				{
					if (drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated(false))
					{
						Color colorArmorBody2 = drawinfo.colorArmorBody;
						Vector2 vector6 = new Vector2(0f, 19f);
						Vector2 vector7 = vector + vector6 * directions;
						Texture2D texture2D = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
						Rectangle rectangle = texture2D.Frame(1, 4, 0, drawinfo.drawPlayer.miscCounter / 5 % 4, 0, 0);
						rectangle.Width -= 2;
						rectangle.Height -= 2;
						DrawData drawData = new DrawData(texture2D, vector7.Floor(), new Rectangle?(rectangle), Color.Lerp(colorArmorBody2, Color.White, 1f), drawinfo.drawPlayer.bodyRotation, rectangle.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(drawData);
						texture2D = TextureAssets.Extra[38].Value;
						drawData = new DrawData(texture2D, vector7.Floor(), new Rectangle?(rectangle), Color.Lerp(colorArmorBody2, Color.White, 0.5f), drawinfo.drawPlayer.bodyRotation, rectangle.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(drawData);
						return;
					}
				}
				else if (drawinfo.drawPlayer.wings == 45)
				{
					if (drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated(false))
					{
						PlayerDrawLayers.DrawStarboardRainbowTrail(ref drawinfo, vector, directions);
						Color color2 = new Color(255, 255, 255, 255);
						int num3 = 22;
						int num4 = 0;
						Vector2 vector8 = vector + new Vector2((float)num4, (float)num3) * directions;
						Color color3 = color2 * (1f - drawinfo.shadow);
						DrawData drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector8.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 6 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 6)), color3, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 12)), 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(drawData);
						if (drawinfo.shadow == 0f)
						{
							float num5 = ((float)drawinfo.drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 4f;
							Color color4 = new Color(70, 70, 70, 0) * (num5 / 8f + 0.5f) * 0.4f;
							for (float num6 = 0f; num6 < 6.2831855f; num6 += 1.5707964f)
							{
								drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector8.Floor() + num6.ToRotationVector2() * num5, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 6 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 6)), color4, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 12)), 1f, drawinfo.playerEffect, 0f);
								drawData.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(drawData);
							}
							return;
						}
					}
				}
				else if (drawinfo.drawPlayer.wings == 34)
				{
					if (drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated(false))
					{
						drawinfo.stealth *= drawinfo.stealth;
						drawinfo.stealth *= 1f - drawinfo.shadow;
						Color color5 = new Color((int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(100f * drawinfo.stealth));
						Vector2 vector9 = new Vector2(0f, 0f);
						Texture2D value = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
						Vector2 vector10 = drawinfo.Position + drawinfo.drawPlayer.Size / 2f - Main.screenPosition + vector9 * drawinfo.drawPlayer.Directions - Vector2.UnitX * (float)drawinfo.drawPlayer.direction * 4f;
						Rectangle rectangle2 = value.Frame(1, 6, 0, drawinfo.drawPlayer.wingFrame, 0, 0);
						rectangle2.Width -= 2;
						rectangle2.Height -= 2;
						DrawData drawData = new DrawData(value, vector10.Floor(), new Rectangle?(rectangle2), color5, drawinfo.drawPlayer.bodyRotation, rectangle2.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(drawData);
						return;
					}
				}
				else
				{
					if (drawinfo.drawPlayer.wings == 51)
					{
						drawinfo.stealth *= drawinfo.stealth;
						drawinfo.stealth *= 1f - drawinfo.shadow;
						Color color6 = PlayerDrawLayers.GetLunaGlowColor(ref drawinfo, true) * drawinfo.stealth;
						Vector2 vector11 = new Vector2(0f, (float)((drawinfo.drawPlayer.Directions.Y < 0f) ? 8 : 6));
						Texture2D value2 = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
						Vector2 vector12 = drawinfo.Position + new Vector2((float)(drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height / 2)) - Main.screenPosition + vector11 - Vector2.UnitX * (float)drawinfo.drawPlayer.direction * 4f;
						Rectangle rectangle3 = value2.Frame(1, 8, 0, drawinfo.drawPlayer.wingFrame, 0, 0);
						rectangle3.Width -= 2;
						rectangle3.Height -= 2;
						DrawData drawData = new DrawData(value2, vector12.Floor(), new Rectangle?(rectangle3), color6, drawinfo.drawPlayer.bodyRotation, rectangle3.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(drawData);
						return;
					}
					if (drawinfo.drawPlayer.wings == 47)
					{
						Color colorArmorBody3 = drawinfo.colorArmorBody;
						Color color7 = PlayerDrawLayers.GetChickenBonesGlowColor(ref drawinfo, true, true);
						drawinfo.stealth *= drawinfo.stealth;
						drawinfo.stealth *= 1f - drawinfo.shadow;
						if (drawinfo.stealth == 1f)
						{
							color7.A = 180;
						}
						color7 = Color.Multiply(color7, drawinfo.stealth);
						Vector2 vector13 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
						vector13.Y -= 2f;
						Vector2 vector14 = new Vector2(1f, 1f) + vector13;
						Texture2D value3 = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
						Vector2 vector15 = vector + vector14 * drawinfo.drawPlayer.Directions - Vector2.UnitX * (float)drawinfo.drawPlayer.direction * 4f;
						Rectangle rectangle4 = value3.Frame(1, 11, 0, drawinfo.drawPlayer.wingFrame, 0, 0);
						rectangle4.Width -= 2;
						rectangle4.Height -= 2;
						DrawData drawData = new DrawData(value3, vector15.Floor(), new Rectangle?(rectangle4), colorArmorBody3, drawinfo.drawPlayer.bodyRotation, rectangle4.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(drawData);
						drawData = new DrawData(TextureAssets.GlowMask[366].Value, vector15.Floor(), new Rectangle?(rectangle4), color7, drawinfo.drawPlayer.bodyRotation, rectangle4.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(drawData);
						return;
					}
					if (drawinfo.drawPlayer.wings == 49)
					{
						Color colorArmorBody4 = drawinfo.colorArmorBody;
						Vector2 vector16 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
						vector16.Y -= 2f;
						Vector2 vector17 = new Vector2(1f, 1f) + vector16;
						Texture2D value4 = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
						Vector2 vector18 = vector + vector17 * drawinfo.drawPlayer.Directions - Vector2.UnitX * (float)drawinfo.drawPlayer.direction * 4f;
						Rectangle rectangle5 = value4.Frame(1, 11, 0, drawinfo.drawPlayer.wingFrame, 0, 0);
						rectangle5.Width -= 2;
						rectangle5.Height -= 2;
						DrawData drawData = new DrawData(value4, vector18.Floor(), new Rectangle?(rectangle5), colorArmorBody4, drawinfo.drawPlayer.bodyRotation, rectangle5.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(drawData);
						return;
					}
					if (drawinfo.drawPlayer.wings == 48)
					{
						if (drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated(false))
						{
							Color colorArmorBody5 = drawinfo.colorArmorBody;
							Vector2 vector19 = new Vector2(4f, 0f);
							Texture2D value5 = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
							Vector2 vector20 = drawinfo.Position + drawinfo.drawPlayer.Size / 2f - Main.screenPosition + vector19 * drawinfo.drawPlayer.Directions - Vector2.UnitX * (float)drawinfo.drawPlayer.direction * 4f;
							Rectangle rectangle6 = value5.Frame(1, 8, 0, drawinfo.drawPlayer.wingFrame, 0, 0);
							rectangle6.Width -= 2;
							rectangle6.Height -= 2;
							DrawData drawData = new DrawData(value5, vector20.Floor(), new Rectangle?(rectangle6), colorArmorBody5, drawinfo.drawPlayer.bodyRotation, rectangle6.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
							drawData.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(drawData);
							return;
						}
					}
					else
					{
						if (drawinfo.drawPlayer.wings == 40)
						{
							drawinfo.stealth *= drawinfo.stealth;
							drawinfo.stealth *= 1f - drawinfo.shadow;
							Color color8 = new Color((int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(100f * drawinfo.stealth));
							Vector2 vector21 = new Vector2(-4f, 0f);
							Texture2D value6 = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
							Vector2 vector22 = vector + vector21 * directions;
							for (int j = 0; j < 1; j++)
							{
								SpriteEffects spriteEffects = drawinfo.playerEffect;
								Vector2 vector23 = new Vector2(1f);
								Vector2 zero = Vector2.Zero;
								zero.X = (float)(drawinfo.drawPlayer.direction * 3);
								if (j == 1)
								{
									spriteEffects ^= SpriteEffects.FlipHorizontally;
									vector23 = new Vector2(0.7f, 1f);
									zero.X += (float)(-(float)drawinfo.drawPlayer.direction) * 6f;
								}
								Vector2 vector24 = drawinfo.drawPlayer.velocity * -1.5f;
								int num7 = 0;
								int num8 = 8;
								float num9 = 4f;
								if (drawinfo.drawPlayer.velocity.Y == 0f)
								{
									num7 = 8;
									num8 = 14;
									num9 = 3f;
								}
								for (int k = num7; k < num8; k++)
								{
									Vector2 vector25 = vector22;
									Rectangle rectangle7 = value6.Frame(1, 14, 0, k, 0, 0);
									rectangle7.Width -= 2;
									rectangle7.Height -= 2;
									int num10 = (k - num7) % (int)num9;
									Vector2 vector26 = new Vector2(0f, 0.5f).RotatedBy((double)((drawinfo.drawPlayer.miscCounterNormalized * (2f + (float)num10) + (float)num10 * 0.5f + (float)j * 1.3f) * 6.2831855f), default(Vector2)) * (float)(num10 + 1);
									vector25 += vector26;
									vector25 += vector24 * ((float)num10 / num9);
									vector25 += zero;
									DrawData drawData = new DrawData(value6, vector25.Floor(), new Rectangle?(rectangle7), color8, drawinfo.drawPlayer.bodyRotation, rectangle7.Size() / 2f, vector23, spriteEffects, 0f);
									drawData.shader = drawinfo.cWings;
									drawinfo.DrawDataCache.Add(drawData);
								}
							}
							return;
						}
						if (drawinfo.drawPlayer.wings == 39)
						{
							if (drawinfo.drawPlayer.ShouldDrawWingsThatAreAlwaysAnimated(false))
							{
								drawinfo.stealth *= drawinfo.stealth;
								drawinfo.stealth *= 1f - drawinfo.shadow;
								Color colorArmorBody6 = drawinfo.colorArmorBody;
								Vector2 vector27 = new Vector2(-6f, -7f);
								Texture2D value7 = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
								Vector2 vector28 = vector + vector27 * directions;
								Rectangle rectangle8 = value7.Frame(1, 6, 0, drawinfo.drawPlayer.wingFrame, 0, 0);
								rectangle8.Width -= 2;
								rectangle8.Height -= 2;
								DrawData drawData = new DrawData(value7, vector28.Floor(), new Rectangle?(rectangle8), colorArmorBody6, drawinfo.drawPlayer.bodyRotation, rectangle8.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
								drawData.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(drawData);
								return;
							}
						}
						else
						{
							DrawData drawData;
							if (drawinfo.drawPlayer.wings == 50)
							{
								Texture2D value8 = TextureAssets.Wings[drawinfo.drawPlayer.wings].Value;
								Vector2 zero2 = Vector2.Zero;
								Vector2 vector29 = vector + zero2 * drawinfo.drawPlayer.Directions - Vector2.UnitX * (float)drawinfo.drawPlayer.direction * 4f;
								int num11 = 11;
								Rectangle rectangle9 = value8.Frame(1, num11, 0, drawinfo.drawPlayer.wingFrame, 0, 0);
								drawData = new DrawData(value8, vector29.Floor(), new Rectangle?(rectangle9), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num11 / 2)), 1f, drawinfo.playerEffect, 0f);
								drawData.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(drawData);
								Color color9 = drawinfo.drawPlayer.GetImmuneAlphaPure(Color.White, drawinfo.shadow) * (drawinfo.stealth * drawinfo.stealth) * (1f - drawinfo.shadow);
								drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector29.Floor(), new Rectangle?(rectangle9), color9, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num11 / 2)), 1f, drawinfo.playerEffect, 0f);
								drawData.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(drawData);
								return;
							}
							int num12 = 0;
							int num13 = 0;
							int num14 = 4;
							if (drawinfo.drawPlayer.wings == 43)
							{
								num13 = -5;
								num12 = -7;
								num14 = 7;
							}
							else if (drawinfo.drawPlayer.wings == 44)
							{
								num14 = 7;
							}
							else if (drawinfo.drawPlayer.wings == 5)
							{
								num13 = 4;
								num12 -= 4;
							}
							else if (drawinfo.drawPlayer.wings == 27)
							{
								num13 = 3;
							}
							else if (drawinfo.drawPlayer.wings == 41)
							{
								num13 = -1;
							}
							else if (drawinfo.drawPlayer.wings == 12)
							{
								num13 = -1;
								num12 = -1;
							}
							Color color10 = drawinfo.colorArmorBody;
							if (drawinfo.drawPlayer.wings == 9 || drawinfo.drawPlayer.wings == 29)
							{
								drawinfo.stealth *= drawinfo.stealth;
								drawinfo.stealth *= 1f - drawinfo.shadow;
								color10 = new Color((int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(100f * drawinfo.stealth));
							}
							if (drawinfo.drawPlayer.wings == 10)
							{
								drawinfo.stealth *= drawinfo.stealth;
								drawinfo.stealth *= 1f - drawinfo.shadow;
								color10 = new Color((int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(250f * drawinfo.stealth), (int)(175f * drawinfo.stealth));
							}
							if (drawinfo.drawPlayer.wings == 11 && color10.A > Main.gFade)
							{
								color10.A = Main.gFade;
							}
							if (drawinfo.drawPlayer.wings == 31)
							{
								color10.A = (byte)(220f * drawinfo.stealth);
							}
							if (drawinfo.drawPlayer.wings == 32)
							{
								color10.A = (byte)(127f * drawinfo.stealth);
							}
							if (drawinfo.drawPlayer.wings == 6)
							{
								color10.A = (byte)(160f * drawinfo.stealth);
								color10 *= 0.9f;
							}
							Vector2 vector30 = vector + new Vector2((float)(num13 - 9), (float)(num12 + 2)) * directions;
							drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector30.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num14 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num14)), color10, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num14 / 2)), 1f, drawinfo.playerEffect, 0f);
							drawData.shader = drawinfo.cWings;
							drawinfo.DrawDataCache.Add(drawData);
							if (drawinfo.drawPlayer.wings == 43 && drawinfo.shadow == 0f)
							{
								float num15 = drawinfo.stealth * drawinfo.stealth;
								Vector2 vector31 = vector30;
								Vector2 vector32 = new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num14 / 2));
								Rectangle rectangle10 = new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num14 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / num14);
								for (int l = 0; l < 2; l++)
								{
									Vector2 vector33 = new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
									drawData = new DrawData(TextureAssets.GlowMask[272].Value, vector31 + vector33, new Rectangle?(rectangle10), Color.Multiply(new Color(230, 230, 230, 60), num15), drawinfo.drawPlayer.bodyRotation, vector32, 1f, drawinfo.playerEffect, 0f);
									drawData.shader = drawinfo.cWings;
									drawinfo.DrawDataCache.Add(drawData);
								}
							}
							if (drawinfo.drawPlayer.wings == 23)
							{
								drawinfo.stealth *= drawinfo.stealth;
								drawinfo.stealth *= 1f - drawinfo.shadow;
								color10 = new Color((int)(200f * drawinfo.stealth), (int)(200f * drawinfo.stealth), (int)(200f * drawinfo.stealth), (int)(200f * drawinfo.stealth));
								drawData = new DrawData(TextureAssets.Flames[8].Value, vector30.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color10, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
								drawData.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(drawData);
								return;
							}
							if (drawinfo.drawPlayer.wings == 27)
							{
								drawData = new DrawData(TextureAssets.GlowMask[92].Value, vector30.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 127) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
								drawData.shader = drawinfo.cWings;
								drawinfo.DrawDataCache.Add(drawData);
								return;
							}
							if (drawinfo.drawPlayer.wings == 44)
							{
								PlayerRainbowWingsTextureContent playerRainbowWings = TextureAssets.RenderTargets.PlayerRainbowWings;
								playerRainbowWings.Request();
								if (playerRainbowWings.IsReady)
								{
									RenderTarget2D target = playerRainbowWings.GetTarget();
									drawData = new DrawData(target, vector30.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 7)), new Color(255, 255, 255, 255) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 14)), 1f, drawinfo.playerEffect, 0f);
									drawData.shader = drawinfo.cWings;
									drawinfo.DrawDataCache.Add(drawData);
									return;
								}
							}
							else
							{
								if (drawinfo.drawPlayer.wings == 30)
								{
									drawData = new DrawData(TextureAssets.GlowMask[181].Value, vector30.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 127) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
									drawData.shader = drawinfo.cWings;
									drawinfo.DrawDataCache.Add(drawData);
									return;
								}
								if (drawinfo.drawPlayer.wings == 38)
								{
									Color color11 = drawinfo.ArkhalisColor * drawinfo.stealth * (1f - drawinfo.shadow);
									drawData = new DrawData(TextureAssets.GlowMask[251].Value, vector30.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color11, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
									drawData.shader = drawinfo.cWings;
									drawinfo.DrawDataCache.Add(drawData);
									for (int m = drawinfo.drawPlayer.shadowPos.Length - 2; m >= 0; m--)
									{
										Color color12 = color11;
										color12.A = 0;
										color12 *= MathHelper.Lerp(1f, 0f, (float)m / 3f);
										color12 *= 0.1f;
										Vector2 vector34 = drawinfo.drawPlayer.shadowPos[m] - drawinfo.drawPlayer.position;
										for (float num16 = 0f; num16 < 1f; num16 += 0.01f)
										{
											Vector2 vector35 = new Vector2(2f, 0f).RotatedBy((double)(num16 / 0.04f * 6.2831855f), default(Vector2));
											drawData = new DrawData(TextureAssets.GlowMask[251].Value, vector35 + vector34 * num16 + vector30, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color12 * (1f - num16), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
											drawData.shader = drawinfo.cWings;
											drawinfo.DrawDataCache.Add(drawData);
										}
									}
									return;
								}
								if (drawinfo.drawPlayer.wings == 29)
								{
									drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector30.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 0) * drawinfo.stealth * (1f - drawinfo.shadow) * 0.5f, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1.06f, drawinfo.playerEffect, 0f);
									drawData.shader = drawinfo.cWings;
									drawinfo.DrawDataCache.Add(drawData);
									return;
								}
								if (drawinfo.drawPlayer.wings == 36)
								{
									drawData = new DrawData(TextureAssets.GlowMask[213].Value, vector30.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 0) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1.06f, drawinfo.playerEffect, 0f);
									drawData.shader = drawinfo.cWings;
									drawinfo.DrawDataCache.Add(drawData);
									Vector2 vector36 = new Vector2(0f, 2f - drawinfo.shadow * 2f);
									for (int n = 0; n < 4; n++)
									{
										drawData = new DrawData(TextureAssets.GlowMask[213].Value, vector36.RotatedBy((double)(1.5707964f * (float)n), default(Vector2)) + vector30.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(127, 127, 127, 127) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
										drawData.shader = drawinfo.cWings;
										drawinfo.DrawDataCache.Add(drawData);
									}
									return;
								}
								if (drawinfo.drawPlayer.wings == 31)
								{
									Color color13 = new Color(255, 255, 255, 0);
									color13 = Color.Lerp(Color.HotPink, Color.Crimson, (float)Math.Cos((double)(6.2831855f * ((float)drawinfo.drawPlayer.miscCounter / 100f))) * 0.4f + 0.5f);
									color13.A = 0;
									for (int num17 = 0; num17 < 4; num17++)
									{
										Vector2 vector37 = new Vector2((float)Math.Cos((double)(6.2831855f * ((float)drawinfo.drawPlayer.miscCounter / 60f))) * 0.5f + 0.5f, 0f).RotatedBy((double)((float)num17 * 1.5707964f), default(Vector2)) * 1f;
										drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector30.Floor() + vector37, new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color13 * drawinfo.stealth * (1f - drawinfo.shadow) * 0.5f, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
										drawData.shader = drawinfo.cWings;
										drawinfo.DrawDataCache.Add(drawData);
									}
									drawData = new DrawData(TextureAssets.Wings[drawinfo.drawPlayer.wings].Value, vector30.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), color13 * drawinfo.stealth * (1f - drawinfo.shadow) * 1f, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1f, drawinfo.playerEffect, 0f);
									drawData.shader = drawinfo.cWings;
									drawinfo.DrawDataCache.Add(drawData);
									return;
								}
								if (drawinfo.drawPlayer.wings == 32)
								{
									drawData = new DrawData(TextureAssets.GlowMask[183].Value, vector30.Floor(), new Rectangle?(new Rectangle(0, TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4 * drawinfo.drawPlayer.wingFrame, TextureAssets.Wings[drawinfo.drawPlayer.wings].Width(), TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 4)), new Color(255, 255, 255, 0) * drawinfo.stealth * (1f - drawinfo.shadow), drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Width() / 2), (float)(TextureAssets.Wings[drawinfo.drawPlayer.wings].Height() / 8)), 1.06f, drawinfo.playerEffect, 0f);
									drawData.shader = drawinfo.cWings;
									drawinfo.DrawDataCache.Add(drawData);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x0063EB2C File Offset: 0x0063CD2C
		public static void DrawPlayer_12_1_BalloonFronts(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.balloonFront > 0 && (int)drawinfo.drawPlayer.balloonFront < ArmorIDs.Balloon.Count)
			{
				DrawData drawData;
				if (ArmorIDs.Balloon.Sets.UsesTorsoFraming[(int)drawinfo.drawPlayer.balloonFront])
				{
					drawData = new DrawData(TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloonFront].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + drawinfo.bodyVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cBalloonFront;
					drawinfo.DrawDataCache.Add(drawData);
					return;
				}
				int num = (FocusHelper.PausePlayerBalloonAnimations ? 0 : (DateTime.Now.Millisecond % 800 / 200));
				Vector2 vector = Main.OffsetsPlayerOffhand[drawinfo.drawPlayer.bodyFrame.Y / 56];
				if (drawinfo.drawPlayer.direction != 1)
				{
					vector.X = (float)drawinfo.drawPlayer.width - vector.X;
				}
				if (drawinfo.drawPlayer.gravDir != 1f)
				{
					vector.Y -= (float)drawinfo.drawPlayer.height;
				}
				Vector2 vector2 = new Vector2(0f, 8f) + new Vector2(0f, 6f);
				Vector2 vector3 = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + vector.X)), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + vector.Y * drawinfo.drawPlayer.gravDir)));
				vector3 = drawinfo.Position - Main.screenPosition + vector * new Vector2(1f, drawinfo.drawPlayer.gravDir) + new Vector2(0f, (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height)) + vector2;
				vector3 = vector3.Floor();
				drawData = new DrawData(TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloonFront].Value, vector3, new Rectangle?(new Rectangle(0, TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloonFront].Height() / 4 * num, TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloonFront].Width(), TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloonFront].Height() / 4)), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(26 + drawinfo.drawPlayer.direction * 4), 28f + drawinfo.drawPlayer.gravDir * 6f), 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cBalloonFront;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x0063EECC File Offset: 0x0063D0CC
		public static void DrawPlayer_11_Balloons(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.balloon > 0 && (int)drawinfo.drawPlayer.balloon < ArmorIDs.Balloon.Count)
			{
				DrawData drawData;
				if (ArmorIDs.Balloon.Sets.UsesTorsoFraming[(int)drawinfo.drawPlayer.balloon])
				{
					drawData = new DrawData(TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloon].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + drawinfo.bodyVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cBalloon;
					drawinfo.DrawDataCache.Add(drawData);
					return;
				}
				int num = (FocusHelper.PausePlayerBalloonAnimations ? 0 : (DateTime.Now.Millisecond % 800 / 200));
				Vector2 vector = Main.OffsetsPlayerOffhand[drawinfo.drawPlayer.bodyFrame.Y / 56];
				if (drawinfo.drawPlayer.direction != 1)
				{
					vector.X = (float)drawinfo.drawPlayer.width - vector.X;
				}
				if (drawinfo.drawPlayer.gravDir != 1f)
				{
					vector.Y -= (float)drawinfo.drawPlayer.height;
				}
				Vector2 vector2 = new Vector2(0f, 8f) + new Vector2(0f, 6f);
				Vector2 vector3 = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + vector.X)), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + vector.Y * drawinfo.drawPlayer.gravDir)));
				vector3 = drawinfo.Position - Main.screenPosition + vector * new Vector2(1f, drawinfo.drawPlayer.gravDir) + new Vector2(0f, (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height)) + vector2;
				vector3 = vector3.Floor();
				drawData = new DrawData(TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloon].Value, vector3, new Rectangle?(new Rectangle(0, TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloon].Height() / 4 * num, TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloon].Width(), TextureAssets.AccBalloon[(int)drawinfo.drawPlayer.balloon].Height() / 4)), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(26 + drawinfo.drawPlayer.direction * 4), 28f + drawinfo.drawPlayer.gravDir * 6f), 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cBalloon;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x0063F26C File Offset: 0x0063D46C
		public static void DrawPlayer_12_Skin(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.usesCompositeTorso)
			{
				PlayerDrawLayers.DrawPlayer_12_Skin_Composite(ref drawinfo);
				return;
			}
			if (drawinfo.isSitting)
			{
				drawinfo.hidesBottomSkin = true;
			}
			if (!drawinfo.hidesTopSkin)
			{
				drawinfo.Position.Y = drawinfo.Position.Y + drawinfo.torsoOffset;
				DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 3].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				drawinfo.DrawDataCache.Add(drawData);
				drawinfo.Position.Y = drawinfo.Position.Y - drawinfo.torsoOffset;
			}
			if (!drawinfo.hidesBottomSkin && !PlayerDrawLayers.IsBottomOverridden(ref drawinfo))
			{
				if (drawinfo.isSitting)
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 10].Value, drawinfo.colorLegs, 0, drawinfo.drawPlayer.legs, default(Vector2), true);
					return;
				}
				DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 10].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorLegs, drawinfo.drawPlayer.legRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x0063F56E File Offset: 0x0063D76E
		public static bool IsBottomOverridden(ref PlayerDrawSet drawinfo)
		{
			return PlayerDrawLayers.ShouldOverrideLegs_CheckPants(ref drawinfo) || PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo);
		}

		// Token: 0x060038A8 RID: 14504 RVA: 0x0063F588 File Offset: 0x0063D788
		public static bool ShouldOverrideLegs_CheckPants(ref PlayerDrawSet drawinfo)
		{
			if (PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo))
			{
				return false;
			}
			int legs = drawinfo.drawPlayer.legs;
			if (legs <= 138)
			{
				if (legs <= 63)
				{
					if (legs != 55 && legs != 63)
					{
						return false;
					}
				}
				else if (legs != 67 && legs != 106 && legs != 138)
				{
					return false;
				}
			}
			else if (legs <= 217)
			{
				if (legs != 140 && legs != 143 && legs != 217)
				{
					return false;
				}
			}
			else if (legs != 222 && legs != 226 && legs != 228)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x0063F618 File Offset: 0x0063D818
		public static bool ShouldOverrideLegs_CheckShoes(ref PlayerDrawSet drawinfo)
		{
			sbyte shoe = drawinfo.drawPlayer.shoe;
			return shoe == 15;
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x0063F63C File Offset: 0x0063D83C
		public static void DrawPlayer_12_Skin_Composite(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.hidesTopSkin && !drawinfo.drawPlayer.invis)
			{
				Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
				vector.Y += drawinfo.torsoOffset;
				Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
				vector2.Y -= 2f;
				vector += vector2 * (float)(-(float)((drawinfo.playerEffect & SpriteEffects.FlipVertically) > SpriteEffects.None).ToDirectionInt());
				float bodyRotation = drawinfo.drawPlayer.bodyRotation;
				Vector2 vector3 = vector;
				Vector2 vector4 = vector;
				Vector2 bodyVect = drawinfo.bodyVect;
				Vector2 vector5 = drawinfo.bodyVect;
				Vector2 compositeOffset_BackArm = PlayerDrawLayers.GetCompositeOffset_BackArm(ref drawinfo);
				vector3 += compositeOffset_BackArm;
				bodyVect + compositeOffset_BackArm;
				Vector2 compositeOffset_FrontArm = PlayerDrawLayers.GetCompositeOffset_FrontArm(ref drawinfo);
				vector5 += compositeOffset_FrontArm;
				vector4 + compositeOffset_FrontArm;
				DrawData drawData;
				if (drawinfo.drawFloatingTube)
				{
					List<DrawData> drawDataCache = drawinfo.DrawDataCache;
					drawData = new DrawData(TextureAssets.Extra[105].Value, vector, new Rectangle?(new Rectangle(0, 0, 40, 56)), drawinfo.floatingTubeColor, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.cFloatingTube
					};
					drawDataCache.Add(drawData);
				}
				List<DrawData> drawDataCache2 = drawinfo.DrawDataCache;
				drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 3].Value, vector, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorBodySkin, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				drawDataCache2.Add(drawData);
			}
			if (!drawinfo.hidesBottomSkin && !drawinfo.drawPlayer.invis && !PlayerDrawLayers.IsBottomOverridden(ref drawinfo))
			{
				if (drawinfo.isSitting)
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 10].Value, drawinfo.colorLegs, drawinfo.skinDyePacked, drawinfo.drawPlayer.legs, default(Vector2), true);
				}
				else
				{
					DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 10].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorLegs, drawinfo.drawPlayer.legRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.skinDyePacked
					};
					DrawData drawData2 = drawData;
					drawinfo.DrawDataCache.Add(drawData2);
				}
			}
			PlayerDrawLayers.DrawPlayer_12_SkinComposite_BackArmShirt(ref drawinfo);
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x0063FA4C File Offset: 0x0063DC4C
		public static void DrawPlayer_12_SkinComposite_BackArmShirt(ref PlayerDrawSet drawinfo)
		{
			Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
			Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
			vector2.Y -= 2f;
			vector += vector2 * (float)(-(float)((drawinfo.playerEffect & SpriteEffects.FlipVertically) > SpriteEffects.None).ToDirectionInt());
			vector.Y += drawinfo.torsoOffset;
			float bodyRotation = drawinfo.drawPlayer.bodyRotation;
			Vector2 vector3 = vector;
			Vector2 vector4 = vector;
			Vector2 vector5 = drawinfo.bodyVect;
			Vector2 compositeOffset_BackArm = PlayerDrawLayers.GetCompositeOffset_BackArm(ref drawinfo);
			vector3 += compositeOffset_BackArm;
			vector4 += drawinfo.backShoulderOffset;
			vector5 += compositeOffset_BackArm;
			float num = bodyRotation + drawinfo.compositeBackArmRotation;
			bool flag = !drawinfo.drawPlayer.invis;
			bool flag2 = !drawinfo.drawPlayer.invis;
			bool flag3 = drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count;
			bool flag4 = drawinfo.drawPlayer.coat > 0 && drawinfo.drawPlayer.coat < ArmorIDs.Body.Count;
			bool flag5 = !drawinfo.hidesTopSkin;
			bool flag6 = false;
			if (flag3)
			{
				flag &= drawinfo.missingHand;
				if (flag2 && drawinfo.missingArm)
				{
					if (flag5)
					{
						List<DrawData> drawDataCache = drawinfo.DrawDataCache;
						DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, num, vector5, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.skinDyePacked
						};
						drawDataCache.Add(drawData);
					}
					if (!flag6 && flag5)
					{
						List<DrawData> drawDataCache2 = drawinfo.DrawDataCache;
						DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, num, vector5, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.skinDyePacked
						};
						drawDataCache2.Add(drawData);
						flag6 = true;
					}
					flag2 = false;
				}
				if (!drawinfo.drawPlayer.invis || PlayerDrawLayers.IsArmorDrawnWhenInvisible(drawinfo.drawPlayer.body))
				{
					Texture2D value = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.body].Value;
					DrawData drawData;
					if (!drawinfo.hideCompositeShoulders)
					{
						CompositePlayerDrawContext compositePlayerDrawContext = CompositePlayerDrawContext.BackShoulder;
						drawData = new DrawData(value, vector4, new Rectangle?(drawinfo.compBackShoulderFrame), drawinfo.colorArmorBody, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.cBody
						};
						PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext, drawData, drawinfo.drawPlayer.body);
						if (drawinfo.drawPlayer.body == 71)
						{
							Texture2D value2 = TextureAssets.Extra[277].Value;
							CompositePlayerDrawContext compositePlayerDrawContext2 = CompositePlayerDrawContext.BackShoulder;
							drawData = new DrawData(value2, vector4, new Rectangle?(drawinfo.compBackShoulderFrame), drawinfo.colorArmorBody, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
							{
								shader = 0
							};
							PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext2, drawData, drawinfo.drawPlayer.body);
						}
					}
					PlayerDrawLayers.DrawPlayer_12_1_BalloonFronts(ref drawinfo);
					CompositePlayerDrawContext compositePlayerDrawContext3 = CompositePlayerDrawContext.BackArm;
					drawData = new DrawData(value, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorArmorBody, num, vector5, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.cBody
					};
					PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext3, drawData, drawinfo.drawPlayer.body);
					if (drawinfo.drawPlayer.body == 71)
					{
						Texture2D value3 = TextureAssets.Extra[277].Value;
						CompositePlayerDrawContext compositePlayerDrawContext4 = CompositePlayerDrawContext.BackArm;
						drawData = new DrawData(value3, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorArmorBody, num, vector5, 1f, drawinfo.playerEffect, 0f)
						{
							shader = 0
						};
						PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext4, drawData, drawinfo.drawPlayer.body);
					}
				}
			}
			if (flag)
			{
				if (flag5)
				{
					if (flag2)
					{
						List<DrawData> drawDataCache3 = drawinfo.DrawDataCache;
						DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, num, vector5, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.skinDyePacked
						};
						drawDataCache3.Add(drawData);
					}
					if (!flag6 && flag5)
					{
						List<DrawData> drawDataCache4 = drawinfo.DrawDataCache;
						DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorBodySkin, num, vector5, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.skinDyePacked
						};
						drawDataCache4.Add(drawData);
					}
				}
				if (!flag3)
				{
					drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorUnderShirt, num, vector5, 1f, drawinfo.playerEffect, 0f));
					PlayerDrawLayers.DrawPlayer_12_1_BalloonFronts(ref drawinfo);
					drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorShirt, num, vector5, 1f, drawinfo.playerEffect, 0f));
				}
			}
			if (flag4 && (!drawinfo.drawPlayer.invis || PlayerDrawLayers.IsArmorDrawnWhenInvisible(drawinfo.drawPlayer.coat)))
			{
				Texture2D value4 = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.coat].Value;
				DrawData drawData;
				if (!drawinfo.hideCompositeShoulders)
				{
					CompositePlayerDrawContext compositePlayerDrawContext5 = CompositePlayerDrawContext.BackShoulder;
					drawData = new DrawData(value4, vector4, new Rectangle?(drawinfo.compBackShoulderFrame), drawinfo.colorArmorBody, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.cCoat
					};
					PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext5, drawData, drawinfo.drawPlayer.coat);
				}
				CompositePlayerDrawContext compositePlayerDrawContext6 = CompositePlayerDrawContext.BackArm;
				drawData = new DrawData(value4, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorArmorBody, num, vector5, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.cCoat
				};
				PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext6, drawData, drawinfo.drawPlayer.coat);
			}
			if (drawinfo.drawPlayer.handoff > 0 && (int)drawinfo.drawPlayer.handoff < ArmorIDs.HandOff.Count)
			{
				Texture2D value5 = TextureAssets.AccHandsOffComposite[(int)drawinfo.drawPlayer.handoff].Value;
				CompositePlayerDrawContext compositePlayerDrawContext7 = CompositePlayerDrawContext.BackArmAccessory;
				DrawData drawData = new DrawData(value5, vector3, new Rectangle?(drawinfo.compBackArmFrame), drawinfo.colorArmorBody, num, vector5, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.cHandOff
				};
				PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext7, drawData, -1);
			}
			if (drawinfo.drawPlayer.drawingFootball)
			{
				Main.instance.LoadProjectile(861);
				Texture2D value6 = TextureAssets.Projectile[861].Value;
				Rectangle rectangle = value6.Frame(1, 4, 0, 0, 0, 0);
				Vector2 vector6 = rectangle.Size() / 2f;
				Vector2 vector7 = vector3 + new Vector2((float)(drawinfo.drawPlayer.direction * -2), drawinfo.drawPlayer.gravDir * 4f);
				drawinfo.DrawDataCache.Add(new DrawData(value6, vector7, new Rectangle?(rectangle), drawinfo.colorArmorBody, bodyRotation + 0.7853982f * (float)drawinfo.drawPlayer.direction, vector6, 0.8f, drawinfo.playerEffect, 0f));
			}
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x006402B8 File Offset: 0x0063E4B8
		public static void DrawPlayer_13_ArmorBackCoat(ref PlayerDrawSet drawinfo)
		{
			int matchingBodyExtensionBack = PlayerDrawLayers.GetMatchingBodyExtensionBack(ref drawinfo, drawinfo.drawPlayer.coat);
			if (matchingBodyExtensionBack != -1)
			{
				Main.instance.LoadArmorLegs(matchingBodyExtensionBack);
				if (drawinfo.isSitting && !ArmorIDs.Legs.Sets.DoesNotSupportSittingDraw[matchingBodyExtensionBack])
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.ArmorLeg[matchingBodyExtensionBack].Value, drawinfo.colorArmorBody, drawinfo.cCoat, matchingBodyExtensionBack, new Vector2(0f, drawinfo.seatYOffset), false);
					return;
				}
				DrawData drawData = new DrawData(TextureAssets.ArmorLeg[matchingBodyExtensionBack].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cCoat;
				PlayerDrawLayers.DrawLongCoat(ref drawinfo, ref drawData, matchingBodyExtensionBack);
			}
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x00640424 File Offset: 0x0063E624
		public static void DrawPlayer_13_Leggings(ref PlayerDrawSet drawinfo)
		{
			Vector2 legsOffset = drawinfo.legsOffset;
			if (drawinfo.drawPlayer.legs == 169)
			{
				return;
			}
			if (drawinfo.isSitting && drawinfo.drawPlayer.legs != 140)
			{
				if (drawinfo.drawPlayer.legs > 0 && drawinfo.drawPlayer.legs < ArmorIDs.Legs.Count && (!PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo) || drawinfo.drawPlayer.wearsRobe))
				{
					if (!drawinfo.drawPlayer.invis)
					{
						PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.ArmorLeg[drawinfo.drawPlayer.legs].Value, drawinfo.colorArmorLegs, drawinfo.cLegs, drawinfo.drawPlayer.legs, default(Vector2), false);
						if (drawinfo.drawPlayer.legs == 60)
						{
							Texture2D value = TextureAssets.Extra[278].Value;
							PlayerDrawLayers.DrawSittingLegs(ref drawinfo, value, drawinfo.colorArmorLegs, 0, drawinfo.drawPlayer.legs, default(Vector2), false);
						}
						if (drawinfo.legsGlowMask != -1)
						{
							if (drawinfo.legsGlowMask == 274)
							{
								Vector2 legsOffset2 = drawinfo.legsOffset;
								for (int i = 0; i < 2; i++)
								{
									Vector2 vector = new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
									drawinfo.legsOffset += vector;
									PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.GlowMask[drawinfo.legsGlowMask].Value, drawinfo.legsGlowColor, drawinfo.cLegs, drawinfo.drawPlayer.legs, default(Vector2), false);
									drawinfo.legsOffset = legsOffset2;
								}
								return;
							}
							PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.GlowMask[drawinfo.legsGlowMask].Value, drawinfo.legsGlowColor, drawinfo.cLegs, drawinfo.drawPlayer.legs, default(Vector2), false);
							return;
						}
					}
				}
				else if (!drawinfo.drawPlayer.invis && !PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo))
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 11].Value, drawinfo.colorPants, 0, drawinfo.drawPlayer.legs, default(Vector2), true);
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 12].Value, drawinfo.colorShoes, 0, drawinfo.drawPlayer.legs, default(Vector2), true);
				}
				return;
			}
			if (drawinfo.drawPlayer.legs == 140)
			{
				if (!drawinfo.drawPlayer.invis && !drawinfo.drawPlayer.mount.Active)
				{
					Texture2D value2 = TextureAssets.Extra[73].Value;
					bool flag = drawinfo.drawPlayer.legFrame.Y != drawinfo.drawPlayer.legFrame.Height || Main.gameMenu;
					int num = drawinfo.drawPlayer.miscCounter / 3 % 8;
					if (flag)
					{
						num = drawinfo.drawPlayer.miscCounter / 4 % 8;
					}
					Rectangle rectangle = new Rectangle(18 * flag.ToInt(), num * 26, 16, 24);
					float num2 = 12f;
					if (drawinfo.drawPlayer.bodyFrame.Height != 0)
					{
						num2 = 12f - Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height].Y;
					}
					if (drawinfo.drawPlayer.Directions.Y == -1f)
					{
						num2 -= 6f;
					}
					Vector2 vector2 = new Vector2(1f, 1f);
					Vector2 vector3 = drawinfo.Position + drawinfo.drawPlayer.Size * new Vector2(0.5f, 0.5f + 0.5f * drawinfo.drawPlayer.gravDir);
					int direction = drawinfo.drawPlayer.direction;
					Vector2 vector4 = vector3 + new Vector2((float)0, -num2 * drawinfo.drawPlayer.gravDir) - Main.screenPosition + drawinfo.drawPlayer.legPosition;
					if (drawinfo.isSitting)
					{
						vector4.Y += drawinfo.seatYOffset;
					}
					vector4 += legsOffset;
					vector4 = vector4.Floor();
					DrawData drawData = new DrawData(value2, vector4, new Rectangle?(rectangle), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, rectangle.Size() * new Vector2(0.5f, 0.5f - drawinfo.drawPlayer.gravDir * 0.5f), vector2, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cLegs;
					drawinfo.DrawDataCache.Add(drawData);
					return;
				}
			}
			else if (drawinfo.drawPlayer.legs > 0 && drawinfo.drawPlayer.legs < ArmorIDs.Legs.Count && (!PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo) || drawinfo.drawPlayer.wearsRobe))
			{
				if (!drawinfo.drawPlayer.invis)
				{
					DrawData drawData = new DrawData(TextureAssets.ArmorLeg[drawinfo.drawPlayer.legs].Value, legsOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cLegs;
					drawinfo.DrawDataCache.Add(drawData);
					if (drawinfo.drawPlayer.legs == 60)
					{
						Texture2D value3 = TextureAssets.Extra[278].Value;
						drawData = new DrawData(value3, legsOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = 0;
						drawinfo.DrawDataCache.Add(drawData);
					}
					if (drawinfo.legsGlowMask != -1)
					{
						if (drawinfo.legsGlowMask == 274)
						{
							for (int j = 0; j < 2; j++)
							{
								Vector2 vector5 = new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
								drawData = new DrawData(TextureAssets.GlowMask[drawinfo.legsGlowMask].Value, legsOffset + vector5 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.legsGlowColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
								drawData.shader = drawinfo.cLegs;
								drawinfo.DrawDataCache.Add(drawData);
							}
							return;
						}
						drawData = new DrawData(TextureAssets.GlowMask[drawinfo.legsGlowMask].Value, legsOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.legsGlowColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cLegs;
						drawinfo.DrawDataCache.Add(drawData);
						return;
					}
				}
			}
			else if (!drawinfo.drawPlayer.invis && !PlayerDrawLayers.ShouldOverrideLegs_CheckShoes(ref drawinfo))
			{
				DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 11].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorPants, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
				drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 12].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorShoes, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x0064100C File Offset: 0x0063F20C
		private static void DrawSittingLegs(ref PlayerDrawSet drawinfo, Texture2D textureToDraw, Color matchingColor, int shaderIndex = 0, int legIndex = -1, Vector2 offset = default(Vector2), bool skin = false)
		{
			Vector2 legsOffset = drawinfo.legsOffset;
			Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect;
			Rectangle rectangle = drawinfo.drawPlayer.legFrame;
			vector.Y -= 2f;
			vector.Y += drawinfo.seatYOffset;
			vector += legsOffset;
			vector += offset;
			int num = 2;
			int num2 = 42;
			int num3 = 2;
			int num4 = 2;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			int num9 = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = legIndex == 169 || !skin;
			if (flag3)
			{
				if (legIndex > 172)
				{
					if (legIndex <= 194)
					{
						if (legIndex - 177 > 1 && legIndex - 181 > 1)
						{
							if (legIndex - 193 > 1)
							{
								goto IL_041C;
							}
							if (drawinfo.drawPlayer.body == 218)
							{
								num = -2;
								num7 = 2;
								vector.Y += 2f;
								goto IL_041C;
							}
							goto IL_041C;
						}
					}
					else if (legIndex != 206)
					{
						switch (legIndex)
						{
						case 214:
						case 215:
						case 216:
							num = -6;
							num4 = 2;
							num5 = 2;
							num3 = 4;
							num2 = 6;
							rectangle = drawinfo.drawPlayer.legFrame;
							vector.Y += 6f;
							goto IL_041C;
						case 217:
							num = 0;
							num4 = 0;
							num5 = 0;
							num3 = 1;
							num2 = 0;
							rectangle = drawinfo.drawPlayer.legFrame;
							flag2 = true;
							goto IL_041C;
						case 218:
						case 219:
						case 220:
						case 221:
						case 224:
						case 225:
							goto IL_041C;
						case 222:
							vector.X -= 2f * drawinfo.drawPlayer.Directions.X;
							goto IL_041C;
						case 223:
							vector.X -= 2f * drawinfo.drawPlayer.Directions.X;
							vector.Y -= drawinfo.seatYOffset;
							goto IL_041C;
						case 226:
							goto IL_0340;
						default:
							if (legIndex - 238 > 1)
							{
								goto IL_041C;
							}
							num = 2;
							num4 = 2;
							num5 = -2;
							num2 = 42;
							vector.Y -= drawinfo.seatYOffset;
							flag = true;
							goto IL_041C;
						}
					}
					num = 0;
					num4 = 0;
					num5 = 0;
					num3 = 1;
					num2 = 0;
					rectangle = drawinfo.drawPlayer.legFrame;
					num8 = 4;
					num9 = 6;
					goto IL_041C;
				}
				if (legIndex > 143)
				{
					if (legIndex != 149)
					{
						if (legIndex != 169)
						{
							if (legIndex - 171 > 1)
							{
								goto IL_041C;
							}
						}
						else
						{
							if (skin)
							{
								num = -6;
								num4 = 2;
								num5 = 2;
								num3 = 4;
								num2 = 6;
								rectangle = drawinfo.drawPlayer.legFrame;
								vector.Y += 6f;
								goto IL_041C;
							}
							num = 0;
							num4 = 0;
							num5 = 0;
							num3 = 1;
							num2 = 0;
							rectangle = drawinfo.drawPlayer.legFrame;
							vector.Y -= drawinfo.seatYOffset;
							flag = true;
							goto IL_041C;
						}
					}
					num = -6;
					num4 = 2;
					num5 = 2;
					num3 = 4;
					num2 = 6;
					rectangle = drawinfo.drawPlayer.legFrame;
					vector.Y += 6f;
					vector.Y -= drawinfo.seatYOffset;
					goto IL_041C;
				}
				if (legIndex != 106)
				{
					if (legIndex == 132)
					{
						num = -2;
						num7 = 2;
						goto IL_041C;
					}
					if (legIndex != 143)
					{
						goto IL_041C;
					}
				}
				IL_0340:
				num = 0;
				num4 = 0;
				num2 = 6;
				vector.Y += 4f;
				rectangle.Y = rectangle.Height * 5;
			}
			IL_041C:
			for (int i = num3; i >= 0; i--)
			{
				Vector2 vector2 = vector + new Vector2((float)num, 2f) * new Vector2((float)drawinfo.drawPlayer.direction, 1f);
				Rectangle rectangle2 = rectangle;
				if (!flag2)
				{
					rectangle2.Y += i * 2;
					rectangle2.Y += num2;
					rectangle2.Height -= num2;
					rectangle2.Height -= i * 2;
					if (i != num3)
					{
						rectangle2.Height = 2;
					}
				}
				vector2.X += (float)(drawinfo.drawPlayer.direction * num4 * i + num6 * drawinfo.drawPlayer.direction);
				if (i != 0)
				{
					vector2.X += (float)(num7 * drawinfo.drawPlayer.direction);
				}
				vector2.Y += (float)num2;
				vector2.Y += (float)num5;
				vector2.X += (float)(num8 * drawinfo.drawPlayer.direction);
				vector2.Y += (float)num9;
				DrawData drawData = new DrawData(textureToDraw, vector2, new Rectangle?(rectangle2), matchingColor, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = shaderIndex;
				if (flag)
				{
					PlayerDrawLayers.DrawLongCoat(ref drawinfo, ref drawData, legIndex);
				}
				else
				{
					drawinfo.DrawDataCache.Add(drawData);
				}
			}
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x006415B0 File Offset: 0x0063F7B0
		public static void DrawPlayer_14_Shoes(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.shoe > 0 && (int)drawinfo.drawPlayer.shoe < ArmorIDs.Shoe.Count && !PlayerDrawLayers.ShouldOverrideLegs_CheckPants(ref drawinfo))
			{
				Vector2 shoeDrawOffset = drawinfo.drawPlayer.GetShoeDrawOffset();
				int num = drawinfo.cShoe;
				if (drawinfo.drawPlayer.shoe == 22 || drawinfo.drawPlayer.shoe == 23)
				{
					num = drawinfo.cFlameWaker;
				}
				if (drawinfo.isSitting)
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.AccShoes[(int)drawinfo.drawPlayer.shoe].Value, drawinfo.colorArmorLegs, num, drawinfo.drawPlayer.legs, shoeDrawOffset, false);
					return;
				}
				DrawData drawData = new DrawData(TextureAssets.AccShoes[(int)drawinfo.drawPlayer.shoe].Value, shoeDrawOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = num;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x00641768 File Offset: 0x0063F968
		public static void DrawPlayer_15_SkinLongCoat(ref PlayerDrawSet drawinfo)
		{
			if ((drawinfo.skinVar == 3 || drawinfo.skinVar == 8 || drawinfo.skinVar == 7) && (drawinfo.drawPlayer.body <= 0 || drawinfo.drawPlayer.body >= ArmorIDs.Body.Count) && !drawinfo.drawPlayer.invis)
			{
				if (drawinfo.isSitting)
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.Players[drawinfo.skinVar, 14].Value, drawinfo.colorShirt, 0, drawinfo.drawPlayer.legs, default(Vector2), true);
					return;
				}
				DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 14].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorShirt, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x00641908 File Offset: 0x0063FB08
		public static void DrawPlayer_16_ArmorLongCoat(ref PlayerDrawSet drawinfo)
		{
			int matchingBodyExtension = PlayerDrawLayers.GetMatchingBodyExtension(ref drawinfo, drawinfo.drawPlayer.body);
			if (matchingBodyExtension != -1)
			{
				Main.instance.LoadArmorLegs(matchingBodyExtension);
				if (drawinfo.isSitting && !ArmorIDs.Legs.Sets.DoesNotSupportSittingDraw[matchingBodyExtension])
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.ArmorLeg[matchingBodyExtension].Value, drawinfo.colorArmorBody, drawinfo.cBody, matchingBodyExtension, default(Vector2), false);
				}
				else
				{
					DrawData drawData = new DrawData(TextureAssets.ArmorLeg[matchingBodyExtension].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cBody;
					PlayerDrawLayers.DrawLongCoat(ref drawinfo, ref drawData, matchingBodyExtension);
				}
			}
			int matchingBodyExtension2 = PlayerDrawLayers.GetMatchingBodyExtension(ref drawinfo, drawinfo.drawPlayer.coat);
			if (matchingBodyExtension2 != -1)
			{
				Main.instance.LoadArmorLegs(matchingBodyExtension2);
				if (drawinfo.isSitting && !ArmorIDs.Legs.Sets.DoesNotSupportSittingDraw[matchingBodyExtension2])
				{
					PlayerDrawLayers.DrawSittingLegs(ref drawinfo, TextureAssets.ArmorLeg[matchingBodyExtension2].Value, drawinfo.colorArmorBody, drawinfo.cCoat, matchingBodyExtension2, default(Vector2), false);
					return;
				}
				DrawData drawData = new DrawData(TextureAssets.ArmorLeg[matchingBodyExtension2].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(drawinfo.drawPlayer.legFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cCoat;
				PlayerDrawLayers.DrawLongCoat(ref drawinfo, ref drawData, matchingBodyExtension2);
			}
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x00641BC8 File Offset: 0x0063FDC8
		private static void DrawLongCoat(ref PlayerDrawSet drawinfo, ref DrawData cdd, int specialLegCoat)
		{
			drawinfo.DrawDataCache.Add(cdd);
			if (specialLegCoat == 238)
			{
				DrawData drawData = cdd;
				drawData.texture = TextureAssets.GlowMask[363].Value;
				drawData.color = PlayerDrawLayers.GetChickenBonesGlowColor(ref drawinfo, true, false);
				float num = drawinfo.stealth * drawinfo.stealth;
				num *= 1f - drawinfo.shadow;
				drawData.color = Color.Multiply(drawData.color, num);
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x00641C58 File Offset: 0x0063FE58
		public static int GetMatchingBodyExtensionBack(ref PlayerDrawSet drawinfo, int bodyValue)
		{
			int num = -1;
			if (bodyValue == 251)
			{
				num = 239;
			}
			return num;
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x00641C78 File Offset: 0x0063FE78
		public static int GetMatchingBodyExtension(ref PlayerDrawSet drawinfo, int bodyValue)
		{
			int num = -1;
			if (bodyValue <= 182)
			{
				if (bodyValue <= 73)
				{
					if (bodyValue != 52)
					{
						if (bodyValue != 53)
						{
							if (bodyValue == 73)
							{
								num = 170;
							}
						}
						else if (drawinfo.drawPlayer.Male)
						{
							num = 175;
						}
						else
						{
							num = 176;
						}
					}
					else if (drawinfo.drawPlayer.Male)
					{
						num = 171;
					}
					else
					{
						num = 172;
					}
				}
				else if (bodyValue <= 89)
				{
					if (bodyValue != 81)
					{
						if (bodyValue == 89)
						{
							num = 186;
						}
					}
					else
					{
						num = 169;
					}
				}
				else if (bodyValue != 168)
				{
					if (bodyValue == 182)
					{
						num = 163;
					}
				}
				else
				{
					num = 164;
				}
			}
			else if (bodyValue <= 222)
			{
				if (bodyValue <= 211)
				{
					if (bodyValue != 187)
					{
						switch (bodyValue)
						{
						case 198:
							num = 162;
							break;
						case 200:
							num = 149;
							break;
						case 201:
							num = 150;
							break;
						case 202:
							num = 151;
							break;
						case 205:
							num = 174;
							break;
						case 207:
							num = 161;
							break;
						case 209:
							num = 160;
							break;
						case 210:
							if (drawinfo.drawPlayer.Male)
							{
								num = 178;
							}
							else
							{
								num = 177;
							}
							break;
						case 211:
							if (drawinfo.drawPlayer.Male)
							{
								num = 182;
							}
							else
							{
								num = 181;
							}
							break;
						}
					}
					else
					{
						num = 173;
					}
				}
				else if (bodyValue != 218)
				{
					if (bodyValue == 222)
					{
						if (drawinfo.drawPlayer.Male)
						{
							num = 201;
						}
						else
						{
							num = 200;
						}
					}
				}
				else
				{
					num = 195;
				}
			}
			else if (bodyValue <= 236)
			{
				if (bodyValue != 225)
				{
					if (bodyValue == 236)
					{
						num = 221;
					}
				}
				else
				{
					num = 206;
				}
			}
			else if (bodyValue != 237)
			{
				if (bodyValue == 251)
				{
					num = 238;
				}
			}
			else
			{
				num = 223;
			}
			return num;
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x00641EF0 File Offset: 0x006400F0
		public static void DrawPlayer_17_Torso(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.usesCompositeTorso)
			{
				PlayerDrawLayers.DrawPlayer_17_TorsoComposite(ref drawinfo);
				return;
			}
			if (drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count)
			{
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				int num = drawinfo.armorAdjust;
				bodyFrame.X += num;
				bodyFrame.Width -= num;
				if (drawinfo.drawPlayer.direction == -1)
				{
					num = 0;
				}
				if (!drawinfo.drawPlayer.invis || (drawinfo.drawPlayer.body != 21 && drawinfo.drawPlayer.body != 22))
				{
					Texture2D texture2D;
					if (!drawinfo.drawPlayer.Male)
					{
						texture2D = TextureAssets.FemaleBody[drawinfo.drawPlayer.body].Value;
					}
					else
					{
						texture2D = TextureAssets.ArmorBody[drawinfo.drawPlayer.body].Value;
					}
					DrawData drawData = new DrawData(texture2D, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)) + num), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cBody;
					drawinfo.DrawDataCache.Add(drawData);
					if (drawinfo.bodyGlowMask != -1)
					{
						drawData = new DrawData(TextureAssets.GlowMask[drawinfo.bodyGlowMask].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)) + num), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.bodyGlowColor, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cBody;
						drawinfo.DrawDataCache.Add(drawData);
					}
				}
				if (drawinfo.missingHand && !drawinfo.drawPlayer.invis)
				{
					DrawData drawData2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.skinDyePacked
					};
					DrawData drawData = drawData2;
					drawinfo.DrawDataCache.Add(drawData);
					return;
				}
			}
			else if (!drawinfo.drawPlayer.invis)
			{
				DrawData drawData;
				if (!drawinfo.drawPlayer.Male)
				{
					drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 4].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorUnderShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(drawData);
					drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(drawData);
				}
				else
				{
					drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 4].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorUnderShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(drawData);
					drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(drawData);
				}
				DrawData drawData2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 5].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				drawData = drawData2;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x00642920 File Offset: 0x00640B20
		public static void DrawPlayer_17_TorsoComposite(ref PlayerDrawSet drawinfo)
		{
			Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
			Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
			vector2.Y -= 2f;
			vector += vector2 * (float)(-(float)((drawinfo.playerEffect & SpriteEffects.FlipVertically) > SpriteEffects.None).ToDirectionInt());
			float bodyRotation = drawinfo.drawPlayer.bodyRotation;
			Vector2 vector3 = vector;
			Vector2 vector4 = drawinfo.bodyVect;
			Vector2 compositeOffset_BackArm = PlayerDrawLayers.GetCompositeOffset_BackArm(ref drawinfo);
			vector3 + compositeOffset_BackArm;
			vector4 += compositeOffset_BackArm;
			bool flag = false;
			if (drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count)
			{
				flag = true;
				if (!drawinfo.drawPlayer.invis || PlayerDrawLayers.IsArmorDrawnWhenInvisible(drawinfo.drawPlayer.body))
				{
					Texture2D value = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.body].Value;
					CompositePlayerDrawContext compositePlayerDrawContext = CompositePlayerDrawContext.Torso;
					DrawData drawData = new DrawData(value, vector, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorArmorBody, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.cBody
					};
					PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext, drawData, drawinfo.drawPlayer.body);
					if (drawinfo.drawPlayer.body == 71)
					{
						Texture2D value2 = TextureAssets.Extra[277].Value;
						CompositePlayerDrawContext compositePlayerDrawContext2 = CompositePlayerDrawContext.Torso;
						drawData = new DrawData(value2, vector, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorArmorBody, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
						{
							shader = 0
						};
						PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext2, drawData, drawinfo.drawPlayer.body);
					}
				}
			}
			if (!flag && !drawinfo.drawPlayer.invis)
			{
				drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 4].Value, vector, new Rectangle?(drawinfo.compBackShoulderFrame), drawinfo.colorUnderShirt, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f));
				drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, vector, new Rectangle?(drawinfo.compBackShoulderFrame), drawinfo.colorShirt, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f));
				drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 4].Value, vector, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorUnderShirt, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f));
				drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, vector, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorShirt, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f));
			}
			if (drawinfo.drawPlayer.coat > 0 && drawinfo.drawPlayer.coat < ArmorIDs.Body.Count && (!drawinfo.drawPlayer.invis || PlayerDrawLayers.IsArmorDrawnWhenInvisible(drawinfo.drawPlayer.coat)))
			{
				Texture2D value3 = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.coat].Value;
				CompositePlayerDrawContext compositePlayerDrawContext3 = CompositePlayerDrawContext.Torso;
				DrawData drawData = new DrawData(value3, vector, new Rectangle?(drawinfo.compTorsoFrame), drawinfo.colorArmorBody, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.cCoat
				};
				PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext3, drawData, drawinfo.drawPlayer.coat);
			}
			if (drawinfo.drawFloatingTube)
			{
				List<DrawData> drawDataCache = drawinfo.DrawDataCache;
				DrawData drawData = new DrawData(TextureAssets.Extra[105].Value, vector, new Rectangle?(new Rectangle(0, 56, 40, 56)), drawinfo.floatingTubeColor, bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.cFloatingTube
				};
				drawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x00642E04 File Offset: 0x00641004
		public static void DrawPlayer_18_OffhandAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.usesCompositeBackHandAcc)
			{
				return;
			}
			if (drawinfo.drawPlayer.handoff > 0 && (int)drawinfo.drawPlayer.handoff < ArmorIDs.HandOff.Count)
			{
				DrawData drawData = new DrawData(TextureAssets.AccHandsOff[(int)drawinfo.drawPlayer.handoff].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cHandOff;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x00642F6C File Offset: 0x0064116C
		public static void DrawPlayer_JimsDroneRadio(ref PlayerDrawSet drawinfo)
		{
			if ((drawinfo.drawPlayer.HeldItem.type == 5451 || drawinfo.drawPlayer.HeldItem.type == 5738) && drawinfo.drawPlayer.itemAnimation == 0)
			{
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				Texture2D value = TextureAssets.Extra[261].Value;
				DrawData drawData = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)) + drawinfo.drawPlayer.direction * 2), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f + 14f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cWaist;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x006430F8 File Offset: 0x006412F8
		public static void DrawPlayer_19_WaistAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.waist > 0 && (int)drawinfo.drawPlayer.waist < ArmorIDs.Waist.Count)
			{
				Rectangle rectangle = drawinfo.drawPlayer.legFrame;
				if (ArmorIDs.Waist.Sets.UsesTorsoFraming[(int)drawinfo.drawPlayer.waist])
				{
					rectangle = drawinfo.drawPlayer.bodyFrame;
				}
				DrawData drawData = new DrawData(TextureAssets.AccWaist[(int)drawinfo.drawPlayer.waist].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.legFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.legFrame.Height + 4f))) + drawinfo.drawPlayer.legPosition + drawinfo.legVect, new Rectangle?(rectangle), drawinfo.colorArmorLegs, drawinfo.drawPlayer.legRotation, drawinfo.legVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cWaist;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x00643254 File Offset: 0x00641454
		public static void DrawPlayer_20_NeckAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.neck > 0 && (int)drawinfo.drawPlayer.neck < ArmorIDs.Neck.Count)
			{
				DrawData drawData = new DrawData(TextureAssets.AccNeck[(int)drawinfo.drawPlayer.neck].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cNeck;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x006433B4 File Offset: 0x006415B4
		public static void DrawPlayer_21_Head(ref PlayerDrawSet drawinfo)
		{
			Vector2 zero = Vector2.Zero;
			drawinfo.drawPlayer.ApplyHeadOffsetFromMount(ref zero);
			Vector2 vector = drawinfo.helmetOffset + zero;
			PlayerDrawLayers.DrawPlayer_21_Head_TheFace(ref drawinfo);
			bool flag = drawinfo.drawPlayer.head == 14 || drawinfo.drawPlayer.head == 56 || drawinfo.drawPlayer.head == 114 || drawinfo.drawPlayer.head == 158 || drawinfo.drawPlayer.head == 69 || drawinfo.drawPlayer.head == 180;
			bool flag2 = drawinfo.drawPlayer.head == 28;
			bool flag3 = drawinfo.drawPlayer.head == 39 || drawinfo.drawPlayer.head == 38;
			bool flag4 = true;
			if (drawinfo.drawPlayer.mount.Active)
			{
				int type = drawinfo.drawPlayer.mount.Type;
				if (type == 54)
				{
					if (drawinfo.drawPlayer.head >= 0 && !ArmorIDs.Head.Sets.CanDrawOnVelociraptorMount[drawinfo.drawPlayer.head])
					{
						flag4 = false;
					}
				}
				else if (type >= 0 && MountID.Sets.PlayerIsHidden[type])
				{
					flag4 = false;
				}
			}
			Vector2 vector2 = new Vector2((float)(-(float)drawinfo.drawPlayer.bodyFrame.Width / 2 + drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height + 4));
			Vector2 vector3 = (drawinfo.Position - Main.screenPosition + vector2).Floor() + drawinfo.drawPlayer.headPosition + drawinfo.headVect + zero;
			if ((drawinfo.playerEffect & SpriteEffects.FlipVertically) != SpriteEffects.None)
			{
				int num = drawinfo.drawPlayer.bodyFrame.Height - drawinfo.hairFrontFrame.Height;
				vector3.Y += (float)num;
			}
			vector3 += drawinfo.hairOffset;
			bool flag5 = drawinfo.drawPlayer.faceMask > 0 && drawinfo.drawPlayer.faceMask < ArmorIDs.Face.Count;
			if (flag5 && drawinfo.drawPlayer.head > 0 && drawinfo.drawPlayer.head < ArmorIDs.Head.Count && !ArmorIDs.Head.Sets.DrawFaceMaskUnderHeadLayer[drawinfo.drawPlayer.head])
			{
				flag5 = false;
			}
			else if (flag5 && drawinfo.drawPlayer.mount.Active && drawinfo.drawPlayer.mount.Type == 54 && !ArmorIDs.Face.Sets.CanDrawOnVelociraptorMount[(int)drawinfo.drawPlayer.faceMask])
			{
				flag5 = false;
			}
			if (flag5)
			{
				Vector2 faceDrawOffset = drawinfo.drawPlayer.GetFaceDrawOffset((int)drawinfo.drawPlayer.faceMask);
				DrawData drawData = new DrawData(TextureAssets.AccFace[(int)drawinfo.drawPlayer.faceMask].Value, faceDrawOffset + zero + drawinfo.helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cFaceMask;
				drawinfo.DrawDataCache.Add(drawData);
			}
			if (flag4 && drawinfo.fullHair)
			{
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
				DrawData drawData = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = num2;
				drawinfo.DrawDataCache.Add(drawData);
				if (!drawinfo.drawPlayer.invis)
				{
					drawData = new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, vector3, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.hairDyePacked;
					drawinfo.DrawDataCache.Add(drawData);
				}
			}
			if (flag4 && drawinfo.hatHair && !drawinfo.drawPlayer.invis)
			{
				DrawData drawData = new DrawData(TextureAssets.PlayerHairAlt[drawinfo.drawPlayer.hair].Value, vector3, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.hairDyePacked;
				drawinfo.DrawDataCache.Add(drawData);
			}
			if (flag4 && drawinfo.drawPlayer.head == 270)
			{
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				bodyFrame.Width += 2;
				DrawData drawData = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(drawData);
				drawData = new DrawData(TextureAssets.GlowMask[drawinfo.headGlowMask].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(drawData);
			}
			else if (flag4 && drawinfo.drawPlayer.head == 282)
			{
				Rectangle bodyFrame2 = drawinfo.drawPlayer.bodyFrame;
				Rectangle bodyFrame3 = drawinfo.drawPlayer.bodyFrame;
				bodyFrame3.X = (bodyFrame3.Y = 0);
				int num3 = 9;
				int num4 = 4;
				int num5 = drawinfo.drawPlayer.miscCounter % (num3 * num4) / num4;
				bodyFrame2.Y = bodyFrame2.Height * num5;
				int num6 = 0;
				num6 += drawinfo.drawPlayer.bodyFrame.Y / 56;
				if (num6 >= Main.OffsetsPlayerHeadgear.Length)
				{
					num6 = 0;
				}
				Vector2 vector4 = Main.OffsetsPlayerHeadgear[num6];
				vector4.Y -= 2f;
				vector4 *= (float)(-(float)((drawinfo.playerEffect & SpriteEffects.FlipVertically) > SpriteEffects.None).ToDirectionInt());
				DrawData drawData = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, vector4 + vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame2), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(drawData);
				drawData = new DrawData(TextureAssets.GlowMask[drawinfo.headGlowMask].Value, vector4 + vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame3), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cHead;
				drawinfo.DrawDataCache.Add(drawData);
			}
			else if (flag4 && flag)
			{
				Rectangle bodyFrame4 = drawinfo.drawPlayer.bodyFrame;
				Vector2 headVect = drawinfo.headVect;
				if (drawinfo.drawPlayer.gravDir == 1f)
				{
					if (bodyFrame4.Y != 0)
					{
						bodyFrame4.Y -= 2;
						headVect.Y += 2f;
					}
					bodyFrame4.Height -= 8;
				}
				else if (bodyFrame4.Y != 0)
				{
					bodyFrame4.Y -= 2;
					headVect.Y -= 10f;
					bodyFrame4.Height -= 8;
				}
				Color color2 = drawinfo.colorArmorHead;
				int num7 = drawinfo.cHead;
				if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
				{
					if (drawinfo.drawPlayer.isDisplayDollOrInanimate)
					{
						color2 = drawinfo.colorDisplayDollSkin;
					}
					else
					{
						color2 = drawinfo.colorHead;
					}
					num7 = drawinfo.skinDyePacked;
				}
				DrawData drawData = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame4), color2, drawinfo.drawPlayer.headRotation, headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = num7;
				drawinfo.DrawDataCache.Add(drawData);
			}
			else if (flag4 && drawinfo.drawPlayer.head == 259)
			{
				int num8 = 27;
				Texture2D value = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
				Rectangle rectangle = value.Frame(1, num8, 0, drawinfo.drawPlayer.rabbitOrderFrame.DisplayFrame, 0, 0);
				Vector2 vector5 = rectangle.Size() / 2f;
				int num9 = drawinfo.drawPlayer.babyBird.ToInt();
				Vector2 vector6 = PlayerDrawLayers.DrawPlayer_21_Head_GetSpecialHatDrawPosition(ref drawinfo, ref vector, new Vector2((float)(1 + num9 * 2), (float)(-26 + drawinfo.drawPlayer.babyBird.ToInt() * -6)));
				int hatStacks = PlayerDrawLayers.GetHatStacks(ref drawinfo, 4955);
				float num10 = 0.05235988f;
				float num11 = num10 * drawinfo.drawPlayer.position.X % 6.2831855f;
				for (int i = hatStacks - 1; i >= 0; i--)
				{
					float num12 = Vector2.UnitY.RotatedBy((double)(num11 + num10 * (float)i), default(Vector2)).X * ((float)i / 30f) * 2f - (float)(i * 2 * drawinfo.drawPlayer.direction);
					DrawData drawData = new DrawData(value, vector6 + new Vector2(num12, (float)(i * -14) * drawinfo.drawPlayer.gravDir), new Rectangle?(rectangle), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, vector5, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cHead;
					drawinfo.DrawDataCache.Add(drawData);
				}
				if (!drawinfo.drawPlayer.invis)
				{
					DrawData drawData = new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, vector3, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.hairDyePacked;
					drawinfo.DrawDataCache.Add(drawData);
				}
			}
			else if (flag4 && drawinfo.drawPlayer.head > 0 && drawinfo.drawPlayer.head < ArmorIDs.Head.Count && !flag2)
			{
				if (!drawinfo.drawPlayer.invis || !flag3)
				{
					if (drawinfo.drawPlayer.head == 13)
					{
						int hatStacks2 = PlayerDrawLayers.GetHatStacks(ref drawinfo, 205);
						float num13 = 0.05235988f;
						float num14 = num13 * drawinfo.drawPlayer.position.X % 6.2831855f;
						for (int j = 0; j < hatStacks2; j++)
						{
							float num15 = Vector2.UnitY.RotatedBy((double)(num14 + num13 * (float)j), default(Vector2)).X * ((float)j / 30f) * 2f;
							DrawData drawData = new DrawData(TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))) + num15, (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f - (float)(4 * j)))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
							drawData.shader = drawinfo.cHead;
							drawinfo.DrawDataCache.Add(drawData);
						}
					}
					else if (drawinfo.drawPlayer.head == 265)
					{
						int num16 = 6;
						Texture2D value2 = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
						Rectangle rectangle2 = value2.Frame(1, num16, 0, drawinfo.drawPlayer.rabbitOrderFrame.DisplayFrame, 0, 0);
						Vector2 vector7 = rectangle2.Size() / 2f;
						Vector2 vector8 = PlayerDrawLayers.DrawPlayer_21_Head_GetSpecialHatDrawPosition(ref drawinfo, ref vector, new Vector2(0f, -9f));
						int hatStacks3 = PlayerDrawLayers.GetHatStacks(ref drawinfo, 5004);
						float num17 = 0.05235988f;
						float num18 = num17 * drawinfo.drawPlayer.position.X % 6.2831855f;
						int num19 = hatStacks3 * 4 + 2;
						int num20 = 0;
						bool flag6 = (Main.GlobalTimeWrappedHourly + 180f) % 600f < 60f;
						for (int k = num19 - 1; k >= 0; k--)
						{
							int num21 = 0;
							if (k == num19 - 1)
							{
								rectangle2.Y = 0;
								num21 = 2;
							}
							else if (k == 0)
							{
								rectangle2.Y = rectangle2.Height * 5;
							}
							else
							{
								rectangle2.Y = rectangle2.Height * (num20++ % 4 + 1);
							}
							if (rectangle2.Y != rectangle2.Height * 3 || !flag6)
							{
								float num22 = Vector2.UnitY.RotatedBy((double)(num18 + num17 * (float)k), default(Vector2)).X * ((float)k / 10f) * 4f - (float)k * 0.1f * (float)drawinfo.drawPlayer.direction;
								DrawData drawData = new DrawData(value2, vector8 + new Vector2(num22, (float)(k * -4 + num21) * drawinfo.drawPlayer.gravDir), new Rectangle?(rectangle2), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, vector7, 1f, drawinfo.playerEffect, 0f);
								drawData.shader = drawinfo.cHead;
								drawinfo.DrawDataCache.Add(drawData);
							}
						}
					}
					else
					{
						Rectangle bodyFrame5 = drawinfo.drawPlayer.bodyFrame;
						Vector2 headVect2 = drawinfo.headVect;
						if (drawinfo.drawPlayer.gravDir == 1f)
						{
							bodyFrame5.Height -= 4;
						}
						else
						{
							headVect2.Y -= 4f;
							bodyFrame5.Height -= 4;
						}
						Color color3 = drawinfo.colorArmorHead;
						int num23 = drawinfo.cHead;
						Texture2D texture2D = TextureAssets.ArmorHead[drawinfo.drawPlayer.head].Value;
						Color color4 = (drawinfo.drawPlayer.isDisplayDollOrInanimate ? drawinfo.colorDisplayDollSkin : drawinfo.colorHead);
						int skinDyePacked = drawinfo.skinDyePacked;
						if (ArmorIDs.Head.Sets.UseSkinColor[drawinfo.drawPlayer.head])
						{
							color3 = color4;
							num23 = skinDyePacked;
						}
						if (drawinfo.drawPlayer.mount.Active && drawinfo.mountHandlesHeadDraw && drawinfo.drawPlayer.head == 288)
						{
							texture2D = TextureAssets.Extra[284].Value;
						}
						DrawData drawData = new DrawData(texture2D, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame5), color3, drawinfo.drawPlayer.headRotation, headVect2, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = num23;
						drawinfo.DrawDataCache.Add(drawData);
						if (drawinfo.drawPlayer.head == 292)
						{
							drawData = new DrawData(TextureAssets.Extra[300].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame5), color4, drawinfo.drawPlayer.headRotation, headVect2, 1f, drawinfo.playerEffect, 0f);
							drawData.shader = skinDyePacked;
							drawinfo.DrawDataCache.Add(drawData);
						}
						if (drawinfo.drawPlayer.head == 109)
						{
							Texture2D value3 = TextureAssets.Extra[276].Value;
							drawData = new DrawData(value3, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame5), color3, drawinfo.drawPlayer.headRotation, headVect2, 1f, drawinfo.playerEffect, 0f);
							drawData.shader = 0;
							drawinfo.DrawDataCache.Add(drawData);
						}
						if (drawinfo.headGlowMask != -1)
						{
							if (drawinfo.headGlowMask == 309)
							{
								int num24 = PlayerDrawLayers.DrawPlayer_Head_GetTVScreen(drawinfo.drawPlayer);
								if (num24 != 0)
								{
									int num25 = 0;
									num25 += drawinfo.drawPlayer.bodyFrame.Y / 56;
									if (num25 >= Main.OffsetsPlayerHeadgear.Length)
									{
										num25 = 0;
									}
									Vector2 vector9 = Main.OffsetsPlayerHeadgear[num25];
									vector9.Y -= 2f;
									vector9 *= (float)(-(float)((drawinfo.playerEffect & SpriteEffects.FlipVertically) > SpriteEffects.None).ToDirectionInt());
									Texture2D value4 = TextureAssets.GlowMask[drawinfo.headGlowMask].Value;
									int num26 = drawinfo.drawPlayer.miscCounter % 20 / 5;
									if (num24 == 5)
									{
										num26 = 0;
										if (drawinfo.drawPlayer.eyeHelper.EyeFrameToShow > 0)
										{
											num26 = 2;
										}
									}
									Rectangle rectangle3 = value4.Frame(6, 4, num24, num26, -2, 0);
									drawData = new DrawData(value4, vector9 + vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(rectangle3), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
									drawData.shader = drawinfo.cHead;
									drawinfo.DrawDataCache.Add(drawData);
								}
							}
							else if (drawinfo.headGlowMask == 273)
							{
								for (int l = 0; l < 2; l++)
								{
									Vector2 vector10 = new Vector2((float)Main.rand.Next(-10, 10) * 0.125f, (float)Main.rand.Next(-10, 10) * 0.125f);
									drawData = new DrawData(TextureAssets.GlowMask[drawinfo.headGlowMask].Value, vector10 + vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame5), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, headVect2, 1f, drawinfo.playerEffect, 0f);
									drawData.shader = drawinfo.cHead;
									drawinfo.DrawDataCache.Add(drawData);
								}
							}
							else
							{
								drawData = new DrawData(TextureAssets.GlowMask[drawinfo.headGlowMask].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(bodyFrame5), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, headVect2, 1f, drawinfo.playerEffect, 0f);
								drawData.shader = drawinfo.cHead;
								drawinfo.DrawDataCache.Add(drawData);
							}
						}
						if (drawinfo.drawPlayer.head == 211)
						{
							Color color5 = new Color(100, 100, 100, 0);
							ulong num27 = (ulong)((long)(drawinfo.drawPlayer.miscCounter / 4 + 100));
							int num28 = 4;
							for (int m = 0; m < num28; m++)
							{
								float num29 = (float)Utils.RandomInt(ref num27, -10, 11) * 0.2f;
								float num30 = (float)Utils.RandomInt(ref num27, -14, 1) * 0.15f;
								drawData = new DrawData(TextureAssets.GlowMask[241].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + new Vector2(num29, num30), new Rectangle?(drawinfo.drawPlayer.bodyFrame), color5, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
								drawData.shader = drawinfo.cHead;
								drawinfo.DrawDataCache.Add(drawData);
							}
						}
					}
				}
			}
			else if (flag4 && !drawinfo.drawPlayer.invis && (drawinfo.drawPlayer.face < 0 || !ArmorIDs.Face.Sets.PreventHairDraw[(int)drawinfo.drawPlayer.face]))
			{
				DrawData drawData = new DrawData(TextureAssets.PlayerHair[drawinfo.drawPlayer.hair].Value, vector3, new Rectangle?(drawinfo.hairFrontFrame), drawinfo.colorHair, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.hairDyePacked;
				drawinfo.DrawDataCache.Add(drawData);
			}
			bool flag7 = drawinfo.drawPlayer.head < 0 || !ArmorIDs.Head.Sets.PreventBeardDraw[drawinfo.drawPlayer.head];
			if (drawinfo.drawPlayer.mount.Active && drawinfo.drawPlayer.mount.Type == 54)
			{
				flag7 = true;
			}
			if (drawinfo.drawPlayer.beard > 0 && flag7)
			{
				Vector2 vector11 = drawinfo.drawPlayer.GetBeardDrawOffset(false) + zero;
				Color color6 = drawinfo.colorArmorHead;
				if (ArmorIDs.Beard.Sets.UseHairColor[(int)drawinfo.drawPlayer.beard])
				{
					color6 = drawinfo.colorHair;
				}
				DrawData drawData = new DrawData(TextureAssets.AccBeard[(int)drawinfo.drawPlayer.beard].Value, vector11 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color6, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cBeard;
				drawinfo.DrawDataCache.Add(drawData);
			}
			if (flag4 && drawinfo.drawPlayer.head == 205)
			{
				DrawData drawData = new DrawData(TextureAssets.Extra[77].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				drawinfo.DrawDataCache.Add(drawData);
			}
			if (flag4 && drawinfo.drawPlayer.head == 214 && !drawinfo.drawPlayer.invis)
			{
				Rectangle bodyFrame6 = drawinfo.drawPlayer.bodyFrame;
				bodyFrame6.Y = 0;
				float num31 = (float)drawinfo.drawPlayer.miscCounter / 300f;
				Color color7 = new Color(0, 0, 0, 0);
				float num32 = 0.8f;
				float num33 = 0.9f;
				if (num31 >= num32)
				{
					color7 = Color.Lerp(Color.Transparent, new Color(200, 200, 200, 0), Utils.GetLerpValue(num32, num33, num31, true));
				}
				if (num31 >= num33)
				{
					color7 = Color.Lerp(Color.Transparent, new Color(200, 200, 200, 0), Utils.GetLerpValue(1f, num33, num31, true));
				}
				color7 *= drawinfo.stealth * (1f - drawinfo.shadow);
				DrawData drawData = new DrawData(TextureAssets.Extra[90].Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect - Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height], new Rectangle?(bodyFrame6), color7, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
			}
			if (flag4 && drawinfo.drawPlayer.head == 137)
			{
				DrawData drawData = new DrawData(TextureAssets.JackHat.Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), new Color(255, 255, 255, 255), drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
				for (int n = 0; n < 7; n++)
				{
					Color color8 = new Color(110 - n * 10, 110 - n * 10, 110 - n * 10, 110 - n * 10);
					Vector2 vector12 = new Vector2((float)Main.rand.Next(-10, 11) * 0.2f, (float)Main.rand.Next(-10, 11) * 0.2f);
					vector12.X = drawinfo.drawPlayer.itemFlamePos[n].X;
					vector12.Y = drawinfo.drawPlayer.itemFlamePos[n].Y;
					vector12 *= 0.5f;
					drawData = new DrawData(TextureAssets.JackHat.Value, vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + vector12, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color8, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(drawData);
				}
			}
			if (drawinfo.drawPlayer.babyBird)
			{
				Rectangle bodyFrame7 = drawinfo.drawPlayer.bodyFrame;
				bodyFrame7.Y = 0;
				Vector2 vector13 = Vector2.Zero;
				Color color9 = drawinfo.colorArmorHead;
				if (drawinfo.drawPlayer.mount.Active)
				{
					int type2 = drawinfo.drawPlayer.mount.Type;
					if (type2 == 52)
					{
						Vector2 mountedCenter = drawinfo.drawPlayer.MountedCenter;
						color9 = drawinfo.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)mountedCenter.X / 16, (int)mountedCenter.Y / 16, Color.White), drawinfo.shadow);
						vector13 = new Vector2(0f, 6f) * drawinfo.drawPlayer.Directions;
					}
					if (type2 == 54)
					{
						Vector2 mountedCenter2 = drawinfo.drawPlayer.MountedCenter;
						color9 = drawinfo.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)mountedCenter2.X / 16, (int)mountedCenter2.Y / 16, Color.White), drawinfo.shadow);
						drawinfo.drawPlayer.ApplyHeadOffsetFromMount(ref vector13);
						vector13 += new Vector2(-2f, 2f) * drawinfo.drawPlayer.Directions;
					}
				}
				DrawData drawData = new DrawData(TextureAssets.Extra[100].Value, vector13 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height] * drawinfo.drawPlayer.gravDir, new Rectangle?(bodyFrame7), color9, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x00645B3C File Offset: 0x00643D3C
		public static int DrawPlayer_Head_GetTVScreen(Player plr)
		{
			if (NPC.AnyDanger(false, false))
			{
				return 4;
			}
			if (plr.statLife <= plr.statLifeMax2 / 4)
			{
				return 1;
			}
			if (plr.ZoneCorrupt || plr.ZoneCrimson || plr.ZoneGraveyard)
			{
				return 0;
			}
			if (plr.wet)
			{
				return 2;
			}
			if (plr.townNPCs >= 3 || BirthdayParty.PartyIsUp || LanternNight.LanternsUp)
			{
				return 5;
			}
			return 3;
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x00645BA4 File Offset: 0x00643DA4
		private static int GetHatStacks(ref PlayerDrawSet drawinfo, int hatItemId)
		{
			int num = 0;
			int num2 = 0;
			if (drawinfo.drawPlayer.armor[num2] != null && drawinfo.drawPlayer.armor[num2].type == hatItemId && drawinfo.drawPlayer.armor[num2].stack > 0)
			{
				num += drawinfo.drawPlayer.armor[num2].stack;
			}
			num2 = 10;
			if (drawinfo.drawPlayer.armor[num2] != null && drawinfo.drawPlayer.armor[num2].type == hatItemId && drawinfo.drawPlayer.armor[num2].stack > 0)
			{
				num += drawinfo.drawPlayer.armor[num2].stack;
			}
			if (num > 2)
			{
				num = 2;
			}
			return num;
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x00645C5C File Offset: 0x00643E5C
		private static Vector2 DrawPlayer_21_Head_GetSpecialHatDrawPosition(ref PlayerDrawSet drawinfo, ref Vector2 helmetOffset, Vector2 hatOffset)
		{
			Vector2 vector = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height] * drawinfo.drawPlayer.Directions;
			Vector2 vector2 = drawinfo.Position - Main.screenPosition + helmetOffset + new Vector2((float)(-(float)drawinfo.drawPlayer.bodyFrame.Width / 2 + drawinfo.drawPlayer.width / 2), (float)(drawinfo.drawPlayer.height - drawinfo.drawPlayer.bodyFrame.Height + 4)) + hatOffset * drawinfo.drawPlayer.Directions + vector;
			vector2 = vector2.Floor();
			vector2 += drawinfo.drawPlayer.headPosition + drawinfo.headVect;
			if (drawinfo.drawPlayer.gravDir == -1f)
			{
				vector2.Y += 12f;
			}
			vector2 = vector2.Floor();
			return vector2;
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x00645D78 File Offset: 0x00643F78
		private static void DrawPlayer_21_Head_TheFace(ref PlayerDrawSet drawinfo)
		{
			Vector2 zero = Vector2.Zero;
			drawinfo.drawPlayer.ApplyHeadOffsetFromMount(ref zero);
			bool flag = drawinfo.drawPlayer.head >= 0 && ArmorIDs.Head.Sets.HidesHead[drawinfo.drawPlayer.head];
			if (drawinfo.mountHandlesHeadDraw)
			{
				if (drawinfo.mountDrawsEyelid)
				{
					PlayerDrawLayers.DrawPlayer_21_Head_TheFace_Eyelid(ref drawinfo);
				}
				if (drawinfo.drawPlayer.face > 0 && ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int)drawinfo.drawPlayer.face] && (!drawinfo.drawPlayer.mount.Active || drawinfo.drawPlayer.mount.Type != 54 || ArmorIDs.Face.Sets.CanDrawOnVelociraptorMount[(int)drawinfo.drawPlayer.face]))
				{
					DrawData drawData = new DrawData(TextureAssets.AccFace[(int)drawinfo.drawPlayer.face].Value, zero + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cFace;
					drawinfo.DrawDataCache.Add(drawData);
					return;
				}
			}
			else if (!flag && drawinfo.drawPlayer.faceHead > 0 && drawinfo.drawPlayer.faceHead < ArmorIDs.Face.Count)
			{
				Vector2 vector = drawinfo.drawPlayer.GetFaceHeadOffsetFromHelmet() + zero;
				DrawData drawData = new DrawData(TextureAssets.AccFace[(int)drawinfo.drawPlayer.faceHead].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + vector, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cFaceHead;
				drawinfo.DrawDataCache.Add(drawData);
				if (drawinfo.drawPlayer.face > 0 && ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int)drawinfo.drawPlayer.face] && (!drawinfo.drawPlayer.mount.Active || drawinfo.drawPlayer.mount.Type != 54 || ArmorIDs.Face.Sets.CanDrawOnVelociraptorMount[(int)drawinfo.drawPlayer.face]))
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
					drawData = new DrawData(TextureAssets.AccFace[(int)drawinfo.drawPlayer.face].Value, zero + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))) + num, (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cFace;
					drawinfo.DrawDataCache.Add(drawData);
					return;
				}
			}
			else if (!drawinfo.drawPlayer.invis && !flag)
			{
				DrawData drawData2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 0].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				DrawData drawData = drawData2;
				drawinfo.DrawDataCache.Add(drawData);
				drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 1].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorEyeWhites, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
				drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 2].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorEyes, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
				PlayerDrawLayers.DrawPlayer_21_Head_TheFace_Eyelid(ref drawinfo);
				if (drawinfo.drawPlayer.yoraiz0rDarkness)
				{
					drawData2 = new DrawData(TextureAssets.Extra[67].Value, zero + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f)
					{
						shader = drawinfo.skinDyePacked
					};
					drawData = drawData2;
					drawinfo.DrawDataCache.Add(drawData);
				}
				if (drawinfo.drawPlayer.face > 0 && ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int)drawinfo.drawPlayer.face] && (!drawinfo.drawPlayer.mount.Active || drawinfo.drawPlayer.mount.Type != 54 || ArmorIDs.Face.Sets.CanDrawOnVelociraptorMount[(int)drawinfo.drawPlayer.face]))
				{
					drawData = new DrawData(TextureAssets.AccFace[(int)drawinfo.drawPlayer.face].Value, zero + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cFace;
					drawinfo.DrawDataCache.Add(drawData);
				}
			}
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x006467D0 File Offset: 0x006449D0
		private static void DrawPlayer_21_Head_TheFace_Eyelid(ref PlayerDrawSet drawinfo)
		{
			Asset<Texture2D> asset = TextureAssets.Players[drawinfo.skinVar, 15];
			if (asset.IsLoaded)
			{
				Vector2 zero = Vector2.Zero;
				drawinfo.drawPlayer.ApplyHeadOffsetFromMount(ref zero);
				Vector2 vector = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
				vector.Y -= 2f;
				vector *= (float)(-(float)((drawinfo.playerEffect & SpriteEffects.FlipVertically) > SpriteEffects.None).ToDirectionInt());
				Color color = drawinfo.colorHead;
				int num = drawinfo.skinDyePacked;
				int num2 = drawinfo.drawPlayer.eyeHelper.EyeFrameToShow;
				if (drawinfo.drawPlayer.mount.Active && drawinfo.drawPlayer.mount.Type == 54)
				{
					color = drawinfo.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int)drawinfo.drawPlayer.MountedCenter.X / 16, (int)drawinfo.drawPlayer.MountedCenter.Y / 16, new Color(158, 92, 67)), drawinfo.shadow);
					num = drawinfo.drawPlayer.cMount;
				}
				if (drawinfo.drawPlayer.mount.Active && drawinfo.mountHandlesHeadDraw && drawinfo.mountDrawsEyelid && drawinfo.drawPlayer.head == 288)
				{
					color = Color.Black;
					num2 = 2;
				}
				Rectangle rectangle = asset.Frame(1, 3, 0, num2, 0, 0);
				DrawData drawData = new DrawData(asset.Value, zero + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect + vector, new Rectangle?(rectangle), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = num
				};
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x00646A48 File Offset: 0x00644C48
		public static void DrawPlayer_21_1_Magiluminescence(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.shadow == 0f && drawinfo.drawPlayer.neck == 11 && !drawinfo.hideEntirePlayer && !drawinfo.hideEntirePlayerExceptHelmetsAndFaceAccessories)
			{
				Color colorArmorBody = drawinfo.colorArmorBody;
				Color color = new Color(140, 140, 35, 12);
				float num = (float)(colorArmorBody.R + colorArmorBody.G + colorArmorBody.B) / 3f / 255f;
				color = Color.Lerp(color, Color.Transparent, num);
				if (color == Color.Transparent)
				{
					return;
				}
				DrawData drawData = new DrawData(TextureAssets.GlowMask[310].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), color, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cNeck;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x00646C08 File Offset: 0x00644E08
		public static void DrawPlayer_ChippysHeadband(ref PlayerDrawSet drawinfo)
		{
			Texture2D value = TextureAssets.Extra[279].Value;
			float num = 0.4f;
			float num2 = 0.8f;
			float num3 = 160f;
			Vector2 zero = Vector2.Zero;
			float num4 = 0f;
			bool flag = false;
			bool flag2 = false;
			Vector2 vector = PlayerDrawLayers.DrawPlayer_GetMountOffsetForFaceAcc(ref drawinfo, ref flag, ref flag2);
			Vector2 vector2 = drawinfo.drawPlayer.GetFaceDrawOffset((int)drawinfo.drawPlayer.face);
			if (flag && flag2)
			{
				vector2 += drawinfo.drawPlayer.GetHelmetOffsetAddonFromMount();
			}
			vector2 += vector;
			int num5 = Math.Min(drawinfo.drawPlayer.availableAdvancedShadowsCount - 1, 30);
			float num6 = 0f;
			for (int i = num5; i > 0; i--)
			{
				EntityShadowInfo advancedShadow = drawinfo.drawPlayer.GetAdvancedShadow(i);
				EntityShadowInfo advancedShadow2 = drawinfo.drawPlayer.GetAdvancedShadow(i - 1);
				if (i == 1)
				{
					num4 = drawinfo.drawPlayer.position.AngleFrom(advancedShadow.Position);
				}
				num6 += Vector2.Distance(advancedShadow.Position, advancedShadow2.Position);
			}
			float num7 = MathHelper.Clamp(num6 / num3, 0f, 1f);
			float num8 = -Main.WindForVisuals * 0.45f * drawinfo.drawPlayer.Directions.Y;
			float num9 = Utils.MultiLerp((float)drawinfo.drawPlayer.miscCounter % 100f / 100f, new float[] { 0.3f, 0.5f, 0.8f, 1f, 0.8f, 1f, 0.7f, 0.6f, 0.8f, 0.3f });
			num8 *= num9;
			float num10 = num7;
			int num11;
			if (num10 >= num2)
			{
				num11 = 2;
			}
			else if (num10 >= num)
			{
				num11 = 1;
			}
			else
			{
				num11 = 0;
			}
			int num12;
			switch (num11)
			{
			default:
				num12 = 0;
				break;
			case 1:
			{
				float num13 = (float)drawinfo.drawPlayer.miscCounter % 24f / 24f;
				num12 = (int)Utils.Lerp(1.0, 6.0, (double)num13);
				break;
			}
			case 2:
			{
				float num13 = (float)drawinfo.drawPlayer.miscCounter % 18f / 18f;
				num12 = (int)Utils.Lerp(7.0, 12.0, (double)num13);
				break;
			}
			}
			float num14 = num4;
			if (num14 != 0f && drawinfo.drawPlayer.Directions.X == -1f)
			{
				num14 += 3.1415927f;
			}
			num14 += num8;
			num14 %= 6.2831855f;
			Rectangle rectangle = value.Frame(1, 13, 0, num12, 0, 0);
			Vector2 vector3 = new Vector2(26f, 22f);
			if (drawinfo.drawPlayer.Directions.X == -1f)
			{
				vector3.X = 46f;
			}
			if (drawinfo.drawPlayer.Directions.Y == -1f)
			{
				vector3.Y = 56f - vector3.Y;
			}
			Vector2 vector4 = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(rectangle.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)rectangle.Height + 4f))) + drawinfo.drawPlayer.headPosition + vector3;
			vector2 += new Vector2(1f, -2f) * drawinfo.drawPlayer.Directions;
			Vector2 vector5 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
			vector5.Y -= 2f;
			vector5 *= drawinfo.drawPlayer.Directions.Y;
			DrawData drawData = new DrawData(value, vector2 + zero + vector4 + vector5, new Rectangle?(rectangle), drawinfo.colorArmorHead, num14, vector3, 1f, drawinfo.playerEffect, 0f);
			drawData.shader = drawinfo.cFace;
			drawinfo.DrawDataCache.Add(drawData);
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x00647054 File Offset: 0x00645254
		public static Vector2 DrawPlayer_GetMountOffsetForFaceAcc(ref PlayerDrawSet drawinfo, ref bool isVelociraptor, ref bool hasRockGolemHead)
		{
			Vector2 vector = Vector2.Zero;
			isVelociraptor = false;
			hasRockGolemHead = drawinfo.drawPlayer.head == 241;
			if (drawinfo.drawPlayer.mount.Active)
			{
				int type = drawinfo.drawPlayer.mount.Type;
				switch (type)
				{
				case 52:
					return new Vector2(28f, -2f) * drawinfo.drawPlayer.Directions;
				case 53:
					return vector;
				case 54:
					drawinfo.drawPlayer.ApplyHeadOffsetFromMount(ref vector);
					vector -= drawinfo.drawPlayer.GetHelmetOffsetAddonFromMount();
					isVelociraptor = true;
					return vector;
				case 55:
				case 56:
					break;
				default:
					if (type != 61)
					{
						return vector;
					}
					break;
				}
				drawinfo.drawPlayer.ApplyHeadOffsetFromMount(ref vector);
				vector -= drawinfo.drawPlayer.GetHelmetOffsetAddonFromMount();
			}
			return vector;
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x00647130 File Offset: 0x00645330
		public static void DrawPlayer_22_FaceAcc(ref PlayerDrawSet drawinfo)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = drawinfo.drawPlayer.head == 287;
			Vector2 vector = PlayerDrawLayers.DrawPlayer_GetMountOffsetForFaceAcc(ref drawinfo, ref flag, ref flag2);
			if (drawinfo.drawPlayer.face > 0 && drawinfo.drawPlayer.face < ArmorIDs.Face.Count && !ArmorIDs.Face.Sets.DrawInFaceUnderHairLayer[(int)drawinfo.drawPlayer.face] && (!flag || ArmorIDs.Face.Sets.CanDrawOnVelociraptorMount[(int)drawinfo.drawPlayer.face]) && (drawinfo.drawPlayer.face != 23 || !flag3))
			{
				Vector2 vector2 = drawinfo.drawPlayer.GetFaceDrawOffset((int)drawinfo.drawPlayer.face);
				if (flag && flag2)
				{
					vector2 += drawinfo.drawPlayer.GetHelmetOffsetAddonFromMount();
				}
				DrawData drawData = new DrawData(TextureAssets.AccFace[(int)drawinfo.drawPlayer.face].Value, vector2 + vector + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cFace;
				drawinfo.DrawDataCache.Add(drawData);
			}
			bool flag4 = drawinfo.drawPlayer.faceMask > 0 && drawinfo.drawPlayer.faceMask < ArmorIDs.Face.Count;
			if (flag4 && drawinfo.drawPlayer.head > 0 && drawinfo.drawPlayer.head < ArmorIDs.Head.Count && (ArmorIDs.Head.Sets.PreventFaceMaskDraw[drawinfo.drawPlayer.head] || ArmorIDs.Head.Sets.DrawFaceMaskUnderHeadLayer[drawinfo.drawPlayer.head]))
			{
				flag4 = false;
			}
			else if (flag4 && drawinfo.drawPlayer.mount.Active && drawinfo.drawPlayer.mount.Type == 54 && !ArmorIDs.Face.Sets.CanDrawOnVelociraptorMount[(int)drawinfo.drawPlayer.faceMask])
			{
				flag4 = false;
			}
			if (flag4)
			{
				Vector2 vector3 = drawinfo.drawPlayer.GetFaceDrawOffset((int)drawinfo.drawPlayer.faceMask);
				if (flag && flag2)
				{
					vector3 += drawinfo.drawPlayer.GetHelmetOffsetAddonFromMount();
				}
				DrawData drawData = new DrawData(TextureAssets.AccFace[(int)drawinfo.drawPlayer.faceMask].Value, vector3 + vector + drawinfo.helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cFaceMask;
				drawinfo.DrawDataCache.Add(drawData);
			}
			bool flag5 = drawinfo.drawPlayer.faceFlower > 0 && drawinfo.drawPlayer.faceFlower < ArmorIDs.Face.Count;
			if (flag5 && drawinfo.drawPlayer.head > 0 && drawinfo.drawPlayer.head < ArmorIDs.Head.Count && ArmorIDs.Head.Sets.PreventFaceFlowerDraw[drawinfo.drawPlayer.head])
			{
				flag5 = false;
			}
			else if (flag5 && drawinfo.drawPlayer.mount.Active && drawinfo.drawPlayer.mount.Type == 54 && !ArmorIDs.Face.Sets.CanDrawOnVelociraptorMount[(int)drawinfo.drawPlayer.faceFlower])
			{
				flag5 = false;
			}
			if (flag5)
			{
				Vector2 vector4 = drawinfo.drawPlayer.GetFaceDrawOffset((int)drawinfo.drawPlayer.faceFlower);
				if (flag && flag2)
				{
					vector4 += drawinfo.drawPlayer.GetHelmetOffsetAddonFromMount();
				}
				DrawData drawData = new DrawData(TextureAssets.AccFace[(int)drawinfo.drawPlayer.faceFlower].Value, vector4 + vector + drawinfo.helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cFaceFlower;
				drawinfo.DrawDataCache.Add(drawData);
			}
			if (drawinfo.drawUnicornHorn)
			{
				Vector2 vector5 = Vector2.Zero;
				if (flag && flag2)
				{
					vector5 += drawinfo.drawPlayer.GetHelmetOffsetAddonFromMount();
				}
				DrawData drawData = new DrawData(TextureAssets.Extra[143].Value, vector + vector5 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cUnicornHorn;
				drawinfo.DrawDataCache.Add(drawData);
			}
			if (drawinfo.drawAngelHalo)
			{
				Vector2 vector6 = Vector2.Zero;
				if (flag && flag2)
				{
					vector6 += drawinfo.drawPlayer.GetHelmetOffsetAddonFromMount() + new Vector2(-4f, -14f) * drawinfo.drawPlayer.Directions;
				}
				Color color = drawinfo.drawPlayer.GetImmuneAlphaPure(new Color(200, 200, 200, 150), drawinfo.shadow);
				color *= drawinfo.drawPlayer.stealth;
				Main.instance.LoadAccFace(7);
				DrawData drawData = new DrawData(TextureAssets.AccFace[7].Value, vector + vector6 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect, new Rectangle?(drawinfo.drawPlayer.bodyFrame), color, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cAngelHalo;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x006479E8 File Offset: 0x00645BE8
		public static void DrawTiedBalloons(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.mount.Type != 34)
			{
				return;
			}
			Texture2D value = TextureAssets.Extra[141].Value;
			Vector2 vector = new Vector2(0f, 4f);
			Color colorMount = drawinfo.colorMount;
			int num = (int)(Main.GlobalTimeWrappedHourly * 3f + drawinfo.drawPlayer.position.X / 50f) % 3;
			Rectangle rectangle = value.Frame(1, 3, 0, num, 0, 0);
			Vector2 vector2 = new Vector2((float)(rectangle.Width / 2), (float)rectangle.Height);
			float num2 = -drawinfo.drawPlayer.velocity.X * 0.1f - drawinfo.drawPlayer.fullRotation;
			DrawData drawData = new DrawData(value, drawinfo.drawPlayer.MountedCenter + vector - Main.screenPosition, new Rectangle?(rectangle), colorMount, num2, vector2, 1f, drawinfo.playerEffect, 0f);
			drawinfo.DrawDataCache.Add(drawData);
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x00647AF4 File Offset: 0x00645CF4
		public static void DrawStarboardRainbowTrail(ref PlayerDrawSet drawinfo, Vector2 commonWingPosPreFloor, Vector2 dirsVec)
		{
			if (drawinfo.shadow != 0f)
			{
				return;
			}
			int num = Math.Min(drawinfo.drawPlayer.availableAdvancedShadowsCount - 1, 30);
			float num2 = 0f;
			for (int i = num; i > 0; i--)
			{
				EntityShadowInfo advancedShadow = drawinfo.drawPlayer.GetAdvancedShadow(i);
				EntityShadowInfo advancedShadow2 = drawinfo.drawPlayer.GetAdvancedShadow(i - 1);
				num2 += Vector2.Distance(advancedShadow.Position, advancedShadow2.Position);
			}
			float num3 = MathHelper.Clamp(num2 / 160f, 0f, 1f);
			Main.instance.LoadProjectile(250);
			Texture2D value = TextureAssets.Projectile[250].Value;
			float num4 = 1.7f;
			Vector2 vector = new Vector2((float)(value.Width / 2), (float)(value.Height / 2));
			new Vector2((float)drawinfo.drawPlayer.width, (float)drawinfo.drawPlayer.height) / 2f;
			Color white = Color.White;
			white.A = 64;
			Vector2 vector2 = drawinfo.drawPlayer.DefaultSize * new Vector2(0.5f, 1f) + new Vector2(0f, -4f);
			if (dirsVec.Y < 0f)
			{
				vector2 = drawinfo.drawPlayer.DefaultSize * new Vector2(0.5f, 0f) + new Vector2(0f, 4f);
			}
			for (int j = num; j > 0; j--)
			{
				EntityShadowInfo advancedShadow3 = drawinfo.drawPlayer.GetAdvancedShadow(j);
				EntityShadowInfo advancedShadow4 = drawinfo.drawPlayer.GetAdvancedShadow(j - 1);
				Vector2 vector3 = advancedShadow3.Position + vector2 + advancedShadow3.HeadgearOffset;
				Vector2 vector4 = advancedShadow4.Position + vector2 + advancedShadow4.HeadgearOffset;
				vector3 = drawinfo.drawPlayer.RotatedRelativePoint(vector3, true, false);
				vector4 = drawinfo.drawPlayer.RotatedRelativePoint(vector4, true, false);
				float num5 = (vector4 - vector3).ToRotation() - 1.5707964f;
				num5 = 1.5707964f * (float)drawinfo.drawPlayer.direction;
				float num6 = Math.Abs(vector4.X - vector3.X);
				Vector2 vector5 = new Vector2(num4, num6 / (float)value.Height);
				float num7 = 1f - (float)j / (float)num;
				num7 *= num7;
				num7 *= Utils.GetLerpValue(0f, 4f, num6, true);
				num7 *= 0.5f;
				num7 *= num7;
				Color color = white * num7 * num3;
				if (!(color == Color.Transparent))
				{
					DrawData drawData = new DrawData(value, vector3 - Main.screenPosition, null, color, num5, vector, vector5, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cWings;
					drawinfo.DrawDataCache.Add(drawData);
					for (float num8 = 0.25f; num8 < 1f; num8 += 0.25f)
					{
						drawData = new DrawData(value, Vector2.Lerp(vector3, vector4, num8) - Main.screenPosition, null, color, num5, vector, vector5, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cWings;
						drawinfo.DrawDataCache.Add(drawData);
					}
				}
			}
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x00647E74 File Offset: 0x00646074
		public static void DrawMeowcartTrail(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.mount.Type != 33)
			{
				return;
			}
			if (drawinfo.shadow > 0f)
			{
				return;
			}
			int num = Math.Min(drawinfo.drawPlayer.availableAdvancedShadowsCount - 1, 20);
			float num2 = 0f;
			for (int i = num; i > 0; i--)
			{
				EntityShadowInfo advancedShadow = drawinfo.drawPlayer.GetAdvancedShadow(i);
				EntityShadowInfo advancedShadow2 = drawinfo.drawPlayer.GetAdvancedShadow(i - 1);
				num2 += Vector2.Distance(advancedShadow.Position, advancedShadow2.Position);
			}
			float num3 = MathHelper.Clamp(num2 / 160f, 0f, 1f);
			Main.instance.LoadProjectile(250);
			Texture2D value = TextureAssets.Projectile[250].Value;
			float num4 = 1.5f;
			Vector2 vector = new Vector2((float)(value.Width / 2), 0f);
			Vector2 vector2 = new Vector2((float)drawinfo.drawPlayer.width, (float)drawinfo.drawPlayer.height) / 2f;
			Vector2 vector3 = new Vector2((float)(-(float)drawinfo.drawPlayer.direction * 10), 15f);
			Color white = Color.White;
			white.A = 127;
			Vector2 vector4 = vector2 + vector3;
			vector4 = Vector2.Zero;
			Vector2 vector5 = drawinfo.drawPlayer.RotatedRelativePoint(drawinfo.drawPlayer.Center + vector4 + vector3, false, true) - drawinfo.drawPlayer.position;
			for (int j = num; j > 0; j--)
			{
				EntityShadowInfo advancedShadow3 = drawinfo.drawPlayer.GetAdvancedShadow(j);
				ref EntityShadowInfo advancedShadow4 = drawinfo.drawPlayer.GetAdvancedShadow(j - 1);
				Vector2 vector6 = advancedShadow3.Position + vector4;
				Vector2 vector7 = advancedShadow4.Position + vector4;
				vector6 += vector5;
				vector7 += vector5;
				vector6 = drawinfo.drawPlayer.RotatedRelativePoint(vector6, true, false);
				vector7 = drawinfo.drawPlayer.RotatedRelativePoint(vector7, true, false);
				float num5 = (vector7 - vector6).ToRotation() - 1.5707964f;
				float num6 = Vector2.Distance(vector6, vector7);
				Vector2 vector8 = new Vector2(num4, num6 / (float)value.Height);
				float num7 = 1f - (float)j / (float)num;
				num7 *= num7;
				Color color = white * num7 * num3;
				DrawData drawData = new DrawData(value, vector6 - Main.screenPosition, null, color, num5, vector, vector8, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x00648118 File Offset: 0x00646318
		public static void DrawPlayer_23_MountFront(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.drawPlayer.mount.Active)
			{
				return;
			}
			drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 2, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
			if (drawinfo.mountHandlesHeadDraw)
			{
				PlayerDrawLayers.DrawPlayer_21_Head(ref drawinfo);
				PlayerDrawLayers.DrawPlayer_22_FaceAcc(ref drawinfo);
				if (drawinfo.drawFrontAccInNeckAccLayer)
				{
					PlayerDrawLayers.DrawPlayer_extra_TorsoMinus(ref drawinfo);
					PlayerDrawLayers.DrawPlayer_32_FrontAcc_FrontPart(ref drawinfo);
					PlayerDrawLayers.DrawPlayer_extra_TorsoPlus(ref drawinfo);
				}
			}
			if (drawinfo.drawPlayer.mount.Type != 54)
			{
				drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 3, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
			}
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x006481E4 File Offset: 0x006463E4
		public static void DrawPlayer_24_Pulley(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.pulley && drawinfo.drawPlayer.itemAnimation == 0)
			{
				DrawData drawData;
				if (drawinfo.drawPlayer.pulleyDir == 2)
				{
					int num = -25;
					int num2 = 0;
					float num3 = 0f;
					drawData = new DrawData(TextureAssets.Pulley.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2) - (float)(9 * drawinfo.drawPlayer.direction)) + num2 * drawinfo.drawPlayer.direction), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2) + 2f * drawinfo.drawPlayer.gravDir + (float)num * drawinfo.drawPlayer.gravDir))), new Rectangle?(new Rectangle(0, TextureAssets.Pulley.Height() / 2 * drawinfo.drawPlayer.pulleyFrame, TextureAssets.Pulley.Width(), TextureAssets.Pulley.Height() / 2)), drawinfo.colorArmorHead, num3, new Vector2((float)(TextureAssets.Pulley.Width() / 2), (float)(TextureAssets.Pulley.Height() / 4)), 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(drawData);
					return;
				}
				int num4 = -26;
				int num5 = 10;
				float num6 = 0.35f * (float)(-(float)drawinfo.drawPlayer.direction);
				drawData = new DrawData(TextureAssets.Pulley.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2) - (float)(9 * drawinfo.drawPlayer.direction)) + num5 * drawinfo.drawPlayer.direction), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2) + 2f * drawinfo.drawPlayer.gravDir + (float)num4 * drawinfo.drawPlayer.gravDir))), new Rectangle?(new Rectangle(0, TextureAssets.Pulley.Height() / 2 * drawinfo.drawPlayer.pulleyFrame, TextureAssets.Pulley.Width(), TextureAssets.Pulley.Height() / 2)), drawinfo.colorArmorHead, num6, new Vector2((float)(TextureAssets.Pulley.Width() / 2), (float)(TextureAssets.Pulley.Height() / 4)), 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x0064848C File Offset: 0x0064668C
		public static void DrawPlayer_25_Shield(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.shield > 0 && (int)drawinfo.drawPlayer.shield < ArmorIDs.Shield.Count)
			{
				Vector2 zero = Vector2.Zero;
				if (drawinfo.drawPlayer.shieldRaised)
				{
					zero.Y -= 4f * drawinfo.drawPlayer.gravDir;
				}
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				Vector2 zero2 = Vector2.Zero;
				Vector2 bodyVect = drawinfo.bodyVect;
				if (bodyFrame.Width != TextureAssets.AccShield[(int)drawinfo.drawPlayer.shield].Value.Width)
				{
					bodyFrame.Width = TextureAssets.AccShield[(int)drawinfo.drawPlayer.shield].Value.Width;
					bodyVect.X += (float)(bodyFrame.Width - TextureAssets.AccShield[(int)drawinfo.drawPlayer.shield].Value.Width);
					if ((drawinfo.playerEffect & SpriteEffects.FlipHorizontally) != SpriteEffects.None)
					{
						bodyVect.X = (float)bodyFrame.Width - bodyVect.X;
					}
				}
				DrawData drawData;
				if (drawinfo.drawPlayer.shieldRaised)
				{
					float num = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f));
					float num2 = 2.5f + 1.5f * num;
					Color color = drawinfo.colorArmorBody;
					color.A = 0;
					color *= 0.45f - num * 0.15f;
					for (float num3 = 0f; num3 < 4f; num3 += 1f)
					{
						drawData = new DrawData(TextureAssets.AccShield[(int)drawinfo.drawPlayer.shield].Value, zero2 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero + new Vector2(num2, 0f).RotatedBy((double)(num3 / 4f * 6.2831855f), default(Vector2)), new Rectangle?(bodyFrame), color, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cShield;
						drawinfo.DrawDataCache.Add(drawData);
					}
				}
				drawData = new DrawData(TextureAssets.AccShield[(int)drawinfo.drawPlayer.shield].Value, zero2 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero, new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cShield;
				drawinfo.DrawDataCache.Add(drawData);
				if (drawinfo.drawPlayer.shieldRaised)
				{
					Color color2 = drawinfo.colorArmorBody;
					float num4 = (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 3.1415927f));
					color2.A = (byte)((float)color2.A * (0.5f + 0.5f * num4));
					color2 *= 0.5f + 0.5f * num4;
					drawData = new DrawData(TextureAssets.AccShield[(int)drawinfo.drawPlayer.shield].Value, zero2 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero, new Rectangle?(bodyFrame), color2, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cShield;
				}
				if (drawinfo.drawPlayer.shieldRaised && drawinfo.drawPlayer.shieldParryTimeLeft > 0)
				{
					float num5 = (float)drawinfo.drawPlayer.shieldParryTimeLeft / 20f;
					float num6 = 1.5f * num5;
					Vector2 vector = zero2 + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)) + zero;
					Color color3 = drawinfo.colorArmorBody;
					float num7 = 1f;
					Vector2 vector2 = drawinfo.Position + drawinfo.drawPlayer.Size / 2f - Main.screenPosition;
					Vector2 vector3 = vector - vector2;
					vector += vector3 * num6;
					num7 += num6;
					color3.A = (byte)((float)color3.A * (1f - num5));
					color3 *= 1f - num5;
					drawData = new DrawData(TextureAssets.AccShield[(int)drawinfo.drawPlayer.shield].Value, vector, new Rectangle?(bodyFrame), color3, drawinfo.drawPlayer.bodyRotation, bodyVect, num7, drawinfo.playerEffect, 0f);
					drawData.shader = drawinfo.cShield;
					drawinfo.DrawDataCache.Add(drawData);
				}
				if (drawinfo.drawPlayer.mount.Cart)
				{
					drawinfo.DrawDataCache.Reverse(drawinfo.DrawDataCache.Count - 2, 2);
				}
			}
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x00648BD8 File Offset: 0x00646DD8
		public static void DrawPlayer_26_SolarShield(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.solarShields > 0 && drawinfo.shadow == 0f && !drawinfo.drawPlayer.dead)
			{
				Texture2D value = TextureAssets.Extra[61 + drawinfo.drawPlayer.solarShields - 1].Value;
				Color color = new Color(255, 255, 255, 127);
				float num = (drawinfo.drawPlayer.solarShieldPos[0] * new Vector2(1f, 0.5f)).ToRotation();
				if (drawinfo.drawPlayer.direction == -1)
				{
					num += 3.1415927f;
				}
				num += 0.06283186f * (float)drawinfo.drawPlayer.direction;
				DrawData drawData = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2)))) + drawinfo.drawPlayer.solarShieldPos[0], null, color, num, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cBody;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x00648D58 File Offset: 0x00646F58
		public static void DrawPlayer_27_HeldItem(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.JustDroppedAnItem || !drawinfo.drawPlayer.IsAllowedToHoldItems)
			{
				return;
			}
			if (drawinfo.SelectedDrawnProjectile != null && drawinfo.shadow == 0f && drawinfo.SelectedDrawnProjectile.drawLayer == 7)
			{
				drawinfo.projectileDrawPosition = drawinfo.DrawDataCache.Count;
			}
			Item heldItem = drawinfo.heldItem;
			int num = heldItem.type;
			if (drawinfo.drawPlayer.UsingBiomeTorches)
			{
				if (num != 8)
				{
					if (num == 966)
					{
						num = drawinfo.drawPlayer.BiomeCampfireHoldStyle(num);
					}
				}
				else
				{
					num = drawinfo.drawPlayer.BiomeTorchHoldStyle(num);
				}
			}
			float adjustedItemScale = drawinfo.drawPlayer.GetAdjustedItemScale(heldItem);
			Main.instance.LoadItem(num);
			Texture2D texture2D = TextureAssets.Item[num].Value;
			Vector2 vector = new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y)));
			Rectangle rectangle = drawinfo.drawPlayer.GetItemDrawFrame(num);
			if (num == 5629)
			{
				texture2D = TextureAssets.Extra[285].Value;
				rectangle = texture2D.Frame(1, 1, 0, 0, 0, 0);
			}
			bool flag = drawinfo.drawPlayer.itemAnimation > 0 && heldItem.useStyle != 0;
			bool flag2 = heldItem.holdStyle != 0 && !drawinfo.drawPlayer.pulley;
			if (!drawinfo.drawPlayer.CanVisuallyHoldItem(heldItem))
			{
				flag2 = false;
			}
			drawinfo.itemColor = Lighting.GetColor((int)((double)drawinfo.Position.X + (double)drawinfo.drawPlayer.width * 0.5) / 16, (int)(((double)drawinfo.Position.Y + (double)drawinfo.drawPlayer.height * 0.5) / 16.0));
			if (num == 678)
			{
				drawinfo.itemColor = Color.White;
			}
			PlayerDrawLayers.DrawPlayer_27_HeldItem_ApplyStealthToColor(ref drawinfo, heldItem, flag, flag2, ref drawinfo.itemColor);
			if (drawinfo.shadow == 0f && !drawinfo.drawPlayer.frozen && (flag || flag2) && num > 0 && !drawinfo.drawPlayer.dead && !heldItem.noUseGraphic && (!drawinfo.drawPlayer.wet || !heldItem.noWet) && (!drawinfo.drawPlayer.happyFunTorchTime || drawinfo.drawPlayer.inventory[drawinfo.drawPlayer.selectedItem].createTile != 4 || drawinfo.drawPlayer.itemAnimation != 0))
			{
				string name = drawinfo.drawPlayer.name;
				Color color = Color.White;
				Vector2 vector2 = Vector2.Zero;
				if (num <= 1506)
				{
					if (num <= 204)
					{
						if (num == 46)
						{
							float num2 = Utils.Remap(drawinfo.itemColor.ToVector3().Length() / 1.731f, 0.3f, 0.5f, 1f, 0f, true);
							color = Color.Lerp(Color.Transparent, new Color(255, 255, 255, 127) * 0.7f, num2);
							goto IL_0563;
						}
						if (num != 104)
						{
							if (num != 204)
							{
								goto IL_0563;
							}
							vector2 = new Vector2(4f, -6f) * drawinfo.drawPlayer.Directions;
							goto IL_0563;
						}
					}
					else
					{
						if (num != 426 && num != 797 && num != 1506)
						{
							goto IL_0563;
						}
						goto IL_03E2;
					}
				}
				else if (num <= 5097)
				{
					if (num == 3349)
					{
						vector2 = new Vector2(2f, -2f) * drawinfo.drawPlayer.Directions;
						goto IL_0563;
					}
					if (num - 5094 > 1)
					{
						if (num - 5096 > 1)
						{
							goto IL_0563;
						}
						goto IL_03E2;
					}
				}
				else if (num != 5462)
				{
					if (num == 5669)
					{
						float num3 = Utils.WrappedLerp(0.5f, 1f, (float)(Main.LocalPlayer.miscCounter % 100) / 100f);
						color = Color.Lerp(color, new Color(180, 85, 30), num3);
						color.A = (byte)heldItem.alpha;
						goto IL_0563;
					}
					if (num - 5670 > 1)
					{
						goto IL_0563;
					}
					color = Item.GetPhaseColor(heldItem.shoot, false);
					goto IL_0563;
				}
				else
				{
					vector2 = new Vector2(12f, -14f) * drawinfo.drawPlayer.Directions;
					color = new Color(255, 140, 0, 5);
					color = Color.Transparent;
					if (drawinfo.SelectedDrawnProjectile == null)
					{
						goto IL_0563;
					}
					Projectile selectedDrawnProjectile = drawinfo.SelectedDrawnProjectile;
					if (selectedDrawnProjectile.active && selectedDrawnProjectile.type == 1040)
					{
						Color color2 = new Color(255, 140, 0, 5);
						color = Color.Lerp(Color.Transparent, color2, Utils.Remap(selectedDrawnProjectile.ai[1], 0f, 30f, 0f, 1f, true));
						goto IL_0563;
					}
					goto IL_0563;
				}
				vector2 = new Vector2(4f, -4f) * drawinfo.drawPlayer.Directions;
				goto IL_0563;
				IL_03E2:
				vector2 = new Vector2(6f, -6f) * drawinfo.drawPlayer.Directions;
				IL_0563:
				if (num == 3823)
				{
					vector2 = new Vector2((float)(7 * drawinfo.drawPlayer.direction), -7f * drawinfo.drawPlayer.gravDir);
				}
				if (num == 3827)
				{
					vector2 = new Vector2((float)(13 * drawinfo.drawPlayer.direction), -13f * drawinfo.drawPlayer.gravDir);
					color = heldItem.GetAlpha(drawinfo.itemColor);
					color = Color.Lerp(color, Color.White, 0.6f);
					color.A = 66;
				}
				PlayerDrawLayers.DrawPlayer_27_HeldItem_ApplyStealthToColor(ref drawinfo, heldItem, flag, flag2, ref color);
				Vector2 vector3 = new Vector2((float)rectangle.Width * 0.5f - (float)rectangle.Width * 0.5f * (float)drawinfo.drawPlayer.direction, (float)rectangle.Height);
				if (heldItem.useStyle == 9 && drawinfo.drawPlayer.itemAnimation > 0)
				{
					Vector2 vector4 = new Vector2(0.5f, 0.4f);
					if (heldItem.type == 5009 || heldItem.type == 5042 || heldItem.type == 5645)
					{
						vector4 = new Vector2(0.26f, 0.5f);
						if (drawinfo.drawPlayer.direction == -1)
						{
							vector4.X = 1f - vector4.X;
						}
					}
					vector3 = rectangle.Size() * vector4;
				}
				if (drawinfo.drawPlayer.gravDir == -1f)
				{
					vector3.Y = (float)rectangle.Height - vector3.Y;
				}
				vector3 += vector2;
				float num4 = drawinfo.drawPlayer.itemRotation;
				if (heldItem.useStyle == 8)
				{
					float x = vector.X;
					int direction = drawinfo.drawPlayer.direction;
					vector.X = x - (float)0;
					num4 -= 1.5707964f * (float)drawinfo.drawPlayer.direction;
					vector3.Y = 2f;
					vector3.X += (float)(2 * drawinfo.drawPlayer.direction);
				}
				if (num == 425 || num == 507)
				{
					if (drawinfo.drawPlayer.gravDir == 1f)
					{
						if (drawinfo.drawPlayer.direction == 1)
						{
							drawinfo.itemEffect = SpriteEffects.FlipVertically;
						}
						else
						{
							drawinfo.itemEffect = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
						}
					}
					else if (drawinfo.drawPlayer.direction == 1)
					{
						drawinfo.itemEffect = SpriteEffects.None;
					}
					else
					{
						drawinfo.itemEffect = SpriteEffects.FlipHorizontally;
					}
				}
				if ((num == 946 || num == 4707) && num4 != 0f)
				{
					vector.Y -= 22f * drawinfo.drawPlayer.gravDir;
					num4 = -1.57f * (float)(-(float)drawinfo.drawPlayer.direction) * drawinfo.drawPlayer.gravDir;
				}
				ItemSlot.GetItemLight(ref drawinfo.itemColor, heldItem, false, drawinfo.drawPlayer.stealth);
				if (num == 3476)
				{
					Texture2D texture2D2 = TextureAssets.Extra[64].Value;
					Rectangle rectangle2 = texture2D2.Frame(1, 9, 0, drawinfo.drawPlayer.miscCounter % 54 / 6, 0, 0);
					Vector2 vector5 = new Vector2((float)(rectangle2.Width / 2 * drawinfo.drawPlayer.direction), 0f);
					Vector2 vector6 = rectangle2.Size() / 2f;
					DrawData drawData = new DrawData(texture2D2, (drawinfo.ItemLocation - Main.screenPosition + vector5).Floor(), new Rectangle?(rectangle2), heldItem.GetAlpha(drawinfo.itemColor).MultiplyRGBA(new Color(new Vector4(0.5f, 0.5f, 0.5f, 0.8f))), drawinfo.drawPlayer.itemRotation, vector6, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(drawData);
					texture2D2 = TextureAssets.GlowMask[195].Value;
					drawData = new DrawData(texture2D2, (drawinfo.ItemLocation - Main.screenPosition + vector5).Floor(), new Rectangle?(rectangle2), new Color(250, 250, 250, heldItem.alpha) * 0.5f, drawinfo.drawPlayer.itemRotation, vector6, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(drawData);
					return;
				}
				if (num == 4049)
				{
					Texture2D value = TextureAssets.Extra[92].Value;
					Rectangle rectangle3 = value.Frame(1, 4, 0, drawinfo.drawPlayer.miscCounter % 20 / 5, 0, 0);
					Vector2 vector7 = new Vector2((float)(rectangle3.Width / 2 * drawinfo.drawPlayer.direction), 0f);
					vector7 += new Vector2((float)(-10 * drawinfo.drawPlayer.direction), 8f * drawinfo.drawPlayer.gravDir);
					Vector2 vector8 = rectangle3.Size() / 2f;
					DrawData drawData = new DrawData(value, (drawinfo.ItemLocation - Main.screenPosition + vector7).Floor(), new Rectangle?(rectangle3), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, vector8, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(drawData);
					return;
				}
				if (num == 3779)
				{
					Texture2D texture2D3 = texture2D;
					Rectangle rectangle4 = texture2D3.Frame(1, 1, 0, 0, 0, 0);
					Vector2 vector9 = new Vector2((float)(rectangle4.Width / 2 * drawinfo.drawPlayer.direction), 0f);
					Vector2 vector10 = rectangle4.Size() / 2f;
					float num5 = ((float)drawinfo.drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 1f + 0f;
					Color color3 = new Color(120, 40, 222, 0) * (num5 / 2f * 0.3f + 0.85f) * 0.5f;
					num5 = 2f;
					DrawData drawData;
					for (float num6 = 0f; num6 < 4f; num6 += 1f)
					{
						drawData = new DrawData(TextureAssets.GlowMask[218].Value, (drawinfo.ItemLocation - Main.screenPosition + vector9).Floor() + (num6 * 1.5707964f).ToRotationVector2() * num5, new Rectangle?(rectangle4), color3, drawinfo.drawPlayer.itemRotation, vector10, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(drawData);
					}
					drawData = new DrawData(texture2D3, (drawinfo.ItemLocation - Main.screenPosition + vector9).Floor(), new Rectangle?(rectangle4), heldItem.GetAlpha(drawinfo.itemColor).MultiplyRGBA(new Color(new Vector4(0.5f, 0.5f, 0.5f, 0.8f))), drawinfo.drawPlayer.itemRotation, vector10, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(drawData);
					return;
				}
				if (heldItem.useStyle == 5)
				{
					if (Item.staff[num])
					{
						float num7 = drawinfo.drawPlayer.itemRotation + 0.785f * (float)drawinfo.drawPlayer.direction;
						float num8 = 0f;
						float num9 = 0f;
						Vector2 zero = new Vector2(0f, (float)rectangle.Height);
						if (num == 3210)
						{
							num8 = (float)(8 * -(float)drawinfo.drawPlayer.direction);
							num9 = (float)(2 * (int)drawinfo.drawPlayer.gravDir);
						}
						if (num == 3870)
						{
							Vector2 vector11 = (drawinfo.drawPlayer.itemRotation + 0.7853982f * (float)drawinfo.drawPlayer.direction).ToRotationVector2() * new Vector2((float)(-(float)drawinfo.drawPlayer.direction) * 1.5f, drawinfo.drawPlayer.gravDir) * 3f;
							num8 = (float)((int)vector11.X);
							num9 = (float)((int)vector11.Y);
						}
						if (num == 3787)
						{
							num9 = (float)((int)((float)(8 * (int)drawinfo.drawPlayer.gravDir) * (float)Math.Cos((double)num7)));
						}
						if (num == 3209)
						{
							Vector2 vector12 = (new Vector2(-8f, 0f) * drawinfo.drawPlayer.Directions).RotatedBy((double)drawinfo.drawPlayer.itemRotation, default(Vector2));
							num8 = vector12.X;
							num9 = vector12.Y;
						}
						if (drawinfo.drawPlayer.gravDir == -1f)
						{
							if (drawinfo.drawPlayer.direction == -1)
							{
								num7 += 1.57f;
								zero = new Vector2((float)rectangle.Width, 0f);
								num8 -= (float)rectangle.Width;
							}
							else
							{
								num7 -= 1.57f;
								zero = Vector2.Zero;
							}
						}
						else if (drawinfo.drawPlayer.direction == -1)
						{
							zero = new Vector2((float)rectangle.Width, (float)rectangle.Height);
							num8 -= (float)rectangle.Width;
						}
						DrawData drawData = new DrawData(texture2D, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + zero.X + num8)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + num9))), new Rectangle?(rectangle), heldItem.GetAlpha(drawinfo.itemColor), num7, zero, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(drawData);
						if (num == 3870)
						{
							drawData = new DrawData(TextureAssets.GlowMask[238].Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + zero.X + num8)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + num9))), new Rectangle?(rectangle), new Color(255, 255, 255, 127), num7, zero, adjustedItemScale, drawinfo.itemEffect, 0f);
							drawinfo.DrawDataCache.Add(drawData);
							return;
						}
					}
					else
					{
						DrawData drawData;
						if (num == 5118)
						{
							float num10 = drawinfo.drawPlayer.itemRotation + 1.57f * (float)drawinfo.drawPlayer.direction;
							Vector2 vector13 = new Vector2((float)rectangle.Width * 0.5f, (float)rectangle.Height);
							Vector2 vector14 = new Vector2(10f, 4f) * drawinfo.drawPlayer.Directions;
							vector14 = vector14.RotatedBy((double)drawinfo.drawPlayer.itemRotation, default(Vector2));
							vector14.Y += (float)rectangle.Height * 0.5f;
							drawData = new DrawData(texture2D, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector14.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector14.Y))), new Rectangle?(rectangle), heldItem.GetAlpha(drawinfo.itemColor), num10, vector13, adjustedItemScale, drawinfo.itemEffect, 0f);
							drawinfo.DrawDataCache.Add(drawData);
							return;
						}
						Vector2 vector15 = new Vector2(0f, (float)(rectangle.Height / 2));
						Vector2 vector16 = Main.DrawPlayerItemPos(drawinfo.drawPlayer.gravDir, num);
						int num11 = (int)vector16.X;
						vector15.Y = vector16.Y;
						Vector2 vector17 = new Vector2((float)(-(float)num11), (float)(rectangle.Height / 2));
						if (drawinfo.drawPlayer.direction == -1)
						{
							vector17 = new Vector2((float)(rectangle.Width + num11), (float)(rectangle.Height / 2));
						}
						drawData = new DrawData(texture2D, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector15.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector15.Y))), new Rectangle?(rectangle), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, vector17, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(drawData);
						if (heldItem.color != default(Color))
						{
							drawData = new DrawData(texture2D, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector15.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector15.Y))), new Rectangle?(rectangle), heldItem.GetColor(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, vector17, adjustedItemScale, drawinfo.itemEffect, 0f);
							drawinfo.DrawDataCache.Add(drawData);
						}
						if (heldItem.glowMask != -1)
						{
							Color white = Color.White;
							PlayerDrawLayers.DrawPlayer_27_HeldItem_ApplyStealthToColor(ref drawinfo, heldItem, flag, flag2, ref white);
							drawData = new DrawData(TextureAssets.GlowMask[(int)heldItem.glowMask].Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector15.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector15.Y))), new Rectangle?(rectangle), white, drawinfo.drawPlayer.itemRotation, vector17, adjustedItemScale, drawinfo.itemEffect, 0f);
							drawinfo.DrawDataCache.Add(drawData);
						}
						if (num == 3788)
						{
							float num12 = ((float)drawinfo.drawPlayer.miscCounter / 75f * 6.2831855f).ToRotationVector2().X * 1f + 0f;
							Color color4 = new Color(80, 40, 252, 0) * (num12 / 2f * 0.3f + 0.85f) * 0.5f;
							PlayerDrawLayers.DrawPlayer_27_HeldItem_ApplyStealthToColor(ref drawinfo, heldItem, flag, flag2, ref color4);
							for (float num13 = 0f; num13 < 4f; num13 += 1f)
							{
								drawData = new DrawData(TextureAssets.GlowMask[220].Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X + vector15.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y + vector15.Y))) + (num13 * 1.5707964f + drawinfo.drawPlayer.itemRotation).ToRotationVector2() * num12, null, color4, drawinfo.drawPlayer.itemRotation, vector17, adjustedItemScale, drawinfo.itemEffect, 0f);
								drawinfo.DrawDataCache.Add(drawData);
							}
							return;
						}
					}
				}
				else
				{
					DrawData drawData = new DrawData(texture2D, vector, new Rectangle?(rectangle), heldItem.GetAlpha(drawinfo.itemColor), num4, vector3, adjustedItemScale, drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(drawData);
					if (heldItem.color != default(Color))
					{
						drawData = new DrawData(texture2D, vector, new Rectangle?(rectangle), heldItem.GetColor(drawinfo.itemColor), num4, vector3, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(drawData);
					}
					if (heldItem.glowMask != -1)
					{
						if (num == 5670 || num == 5671)
						{
							drawData = new DrawData(TextureAssets.GlowMask[(int)heldItem.glowMask].Value, vector, new Rectangle?(rectangle), color, num4, vector3, adjustedItemScale, drawinfo.itemEffect, 0f);
							drawinfo.DrawDataCache.Add(drawData);
							color = Item.GetPhaseColor(heldItem.shoot, true);
							PlayerDrawLayers.DrawPlayer_27_HeldItem_ApplyStealthToColor(ref drawinfo, heldItem, flag, flag2, ref color);
						}
						drawData = new DrawData(TextureAssets.GlowMask[(int)heldItem.glowMask].Value, vector, new Rectangle?(rectangle), color, num4, vector3, adjustedItemScale, drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(drawData);
					}
					if (heldItem.type == 5462 && drawinfo.SelectedDrawnProjectile != null)
					{
						Projectile selectedDrawnProjectile2 = drawinfo.SelectedDrawnProjectile;
						if (selectedDrawnProjectile2.active && selectedDrawnProjectile2.type == 1040)
						{
							float num14 = selectedDrawnProjectile2.ai[1];
							Color color5 = new Color(255, 180, 60, 0);
							color = Color.Lerp(Color.Transparent, color5, Utils.Remap(selectedDrawnProjectile2.ai[1], 0f, 30f, 0f, 1f, true));
							float num15 = Utils.Remap(num14, 20f, 26f, 0f, 1f, true) * Utils.Remap(num14, 26f, 32f, 1f, 0f, true);
							float num16 = Utils.Remap(num14, 23f, 29f, 0f, 1f, true);
							num16 = 1f - (1f - num16) * (1f - num16);
							float num17 = num16;
							float num18 = adjustedItemScale * (1f + num17 * 0.3f);
							Vector2 vector18 = vector - new Vector2((float)drawinfo.drawPlayer.direction, -drawinfo.drawPlayer.gravDir).RotatedBy((double)drawinfo.drawPlayer.itemRotation, default(Vector2)) * (num18 * 4f + 3f);
							for (float num19 = 0f; num19 < 6.2831855f; num19 += 1.5707964f)
							{
								drawData = new DrawData(TextureAssets.GlowMask[(int)heldItem.glowMask].Value, vector18, new Rectangle?(rectangle), color * num15, num4, vector3, num18, drawinfo.itemEffect, 0f);
								drawinfo.DrawDataCache.Add(drawData);
							}
							int num20 = 37;
							Vector2 vector19 = vector + new Vector2((float)(num20 * drawinfo.drawPlayer.direction), (float)(-(float)num20) * drawinfo.drawPlayer.gravDir).RotatedBy((double)drawinfo.drawPlayer.itemRotation, default(Vector2)) * adjustedItemScale;
							Texture2D value2 = TextureAssets.Extra[174].Value;
							float num21 = 1f - num17;
							num21 *= 0.85f;
							drawData = new DrawData(value2, vector19, null, color * num15, 0f, value2.Frame(1, 1, 0, 0, 0, 0).Size() / 2f, num21, drawinfo.itemEffect, 0f);
							drawinfo.DrawDataCache.Add(drawData);
							drawData = new DrawData(value2, vector19, null, Color.White * num15, 0f, value2.Frame(1, 1, 0, 0, 0, 0).Size() / 2f, num21 * 0.92f, drawinfo.itemEffect, 0f);
							drawinfo.DrawDataCache.Add(drawData);
						}
					}
					if (heldItem.flame && drawinfo.shadow == 0f)
					{
						try
						{
							Main.instance.LoadItemFlames(num);
							if (TextureAssets.ItemFlame[num].IsLoaded)
							{
								Color color6 = new Color(100, 100, 100, 0);
								int num22 = 7;
								float num23 = 1f;
								float num24 = 0f;
								if (num <= 4952)
								{
									if (num != 3045)
									{
										if (num == 4952)
										{
											num22 = 3;
											num23 = 0.6f;
											color6 = new Color(50, 50, 50, 0);
										}
									}
									else
									{
										color6 = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
									}
								}
								else if (num != 5293)
								{
									if (num != 5322)
									{
										if (num == 5353)
										{
											color6 = new Color(255, 255, 255, 200);
										}
									}
									else
									{
										color6 = new Color(100, 100, 100, 150);
										num24 = (float)(-2 * drawinfo.drawPlayer.direction);
									}
								}
								else
								{
									color6 = new Color(50, 50, 100, 20);
								}
								PlayerDrawLayers.DrawPlayer_27_HeldItem_ApplyStealthToColor(ref drawinfo, heldItem, flag, flag2, ref color6);
								for (int i = 0; i < num22; i++)
								{
									float num25 = drawinfo.drawPlayer.itemFlamePos[i].X * adjustedItemScale * num23;
									float num26 = drawinfo.drawPlayer.itemFlamePos[i].Y * adjustedItemScale * num23;
									drawData = new DrawData(TextureAssets.ItemFlame[num].Value, new Vector2((float)((int)(vector.X + num25 + num24)), (float)((int)(vector.Y + num26))), new Rectangle?(rectangle), color6, num4, vector3, adjustedItemScale, drawinfo.itemEffect, 0f);
									drawinfo.DrawDataCache.Add(drawData);
								}
							}
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x0064A7EC File Offset: 0x006489EC
		private static void DrawPlayer_27_HeldItem_ApplyStealthToColor(ref PlayerDrawSet drawinfo, Item playerItem, bool drawUseStyle, bool drawHoldStyle, ref Color color)
		{
			bool flag = drawUseStyle && playerItem.ranged;
			bool flag2 = !drawUseStyle && drawHoldStyle;
			if (drawinfo.drawPlayer.shroomiteStealth && (flag || flag2))
			{
				float num = drawinfo.drawPlayer.stealth;
				if (num < 0.03f)
				{
					num = 0.03f;
				}
				float num2 = (1f + num * 10f) / 11f;
				color = new Color((int)((byte)((float)color.R * num)), (int)((byte)((float)color.G * num)), (int)((byte)((float)color.B * num2)), (int)((byte)((float)color.A * num)));
			}
			if (drawinfo.drawPlayer.setVortex && (flag || flag2))
			{
				float num3 = drawinfo.drawPlayer.stealth;
				if (num3 < 0.03f)
				{
					num3 = 0.03f;
				}
				float num4 = (1f + num3 * 10f) / 11f;
				color = color.MultiplyRGBA(new Color(Vector4.Lerp(Vector4.One, new Vector4(0f, 0.12f, 0.16f, 0f), 1f - num3)));
			}
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x0064A90C File Offset: 0x00648B0C
		public static void DrawPlayer_28_ArmOverItem(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.mount.Active && drawinfo.drawPlayer.mount.Type == 54)
			{
				drawinfo.drawPlayer.mount.Draw(drawinfo.DrawDataCache, 3, drawinfo.drawPlayer, drawinfo.Position, drawinfo.colorMount, drawinfo.playerEffect, drawinfo.shadow);
				return;
			}
			if (drawinfo.usesCompositeTorso)
			{
				PlayerDrawLayers.DrawPlayer_28_ArmOverItemComposite(ref drawinfo);
				return;
			}
			if (drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count)
			{
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				int num = drawinfo.armorAdjust;
				bodyFrame.X += num;
				bodyFrame.Width -= num;
				if (drawinfo.drawPlayer.direction == -1)
				{
					num = 0;
				}
				if (!drawinfo.drawPlayer.invis || (drawinfo.drawPlayer.body != 21 && drawinfo.drawPlayer.body != 22))
				{
					DrawData drawData2;
					if (drawinfo.missingHand && !drawinfo.drawPlayer.invis)
					{
						int body = drawinfo.drawPlayer.body;
						DrawData drawData;
						if (drawinfo.missingArm)
						{
							drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
							{
								shader = drawinfo.skinDyePacked
							};
							drawData2 = drawData;
							drawinfo.DrawDataCache.Add(drawData2);
						}
						drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 9].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.skinDyePacked
						};
						drawData2 = drawData;
						drawinfo.DrawDataCache.Add(drawData2);
					}
					drawData2 = new DrawData(TextureAssets.ArmorArm[drawinfo.drawPlayer.body].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)) + num), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
					drawData2.shader = drawinfo.cBody;
					drawinfo.DrawDataCache.Add(drawData2);
					if (drawinfo.armGlowMask != -1)
					{
						drawData2 = new DrawData(TextureAssets.GlowMask[drawinfo.armGlowMask].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)) + num), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(bodyFrame), drawinfo.armGlowColor, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
						drawData2.shader = drawinfo.cBody;
						drawinfo.DrawDataCache.Add(drawData2);
					}
					if (drawinfo.drawPlayer.body == 205)
					{
						Color color = new Color(100, 100, 100, 0);
						ulong num2 = (ulong)((long)(drawinfo.drawPlayer.miscCounter / 4));
						int num3 = 4;
						for (int i = 0; i < num3; i++)
						{
							float num4 = (float)Utils.RandomInt(ref num2, -10, 11) * 0.2f;
							float num5 = (float)Utils.RandomInt(ref num2, -10, 1) * 0.15f;
							drawData2 = new DrawData(TextureAssets.GlowMask[240].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2)) + num), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + num4, (float)(drawinfo.drawPlayer.bodyFrame.Height / 2) + num5), new Rectangle?(bodyFrame), color, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
							drawData2.shader = drawinfo.cBody;
							drawinfo.DrawDataCache.Add(drawData2);
						}
						return;
					}
				}
			}
			else if (!drawinfo.drawPlayer.invis)
			{
				DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorBodySkin, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.skinDyePacked
				};
				DrawData drawData2 = drawData;
				drawinfo.DrawDataCache.Add(drawData2);
				drawData2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorUnderShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData2);
				drawData2 = new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorShirt, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData2);
			}
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x0064B3F8 File Offset: 0x006495F8
		public static void DrawPlayer_28_ArmOverItemComposite(ref PlayerDrawSet drawinfo)
		{
			Vector2 vector = new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
			Vector2 vector2 = Main.OffsetsPlayerHeadgear[drawinfo.drawPlayer.bodyFrame.Y / drawinfo.drawPlayer.bodyFrame.Height];
			vector2.Y -= 2f;
			vector += vector2 * (float)(-(float)((drawinfo.playerEffect & SpriteEffects.FlipVertically) > SpriteEffects.None).ToDirectionInt());
			float bodyRotation = drawinfo.drawPlayer.bodyRotation;
			float num = drawinfo.drawPlayer.bodyRotation + drawinfo.compositeFrontArmRotation;
			Vector2 vector3 = drawinfo.bodyVect;
			Vector2 compositeOffset_FrontArm = PlayerDrawLayers.GetCompositeOffset_FrontArm(ref drawinfo);
			vector3 += compositeOffset_FrontArm;
			vector += compositeOffset_FrontArm;
			Vector2 vector4 = vector + drawinfo.frontShoulderOffset;
			if (drawinfo.compFrontArmFrame.X / drawinfo.compFrontArmFrame.Width >= 7)
			{
				vector += new Vector2((float)(((drawinfo.playerEffect & SpriteEffects.FlipHorizontally) != SpriteEffects.None) ? (-1) : 1), (float)(((drawinfo.playerEffect & SpriteEffects.FlipVertically) != SpriteEffects.None) ? (-1) : 1));
			}
			bool invis = drawinfo.drawPlayer.invis;
			bool flag = drawinfo.drawPlayer.body > 0 && drawinfo.drawPlayer.body < ArmorIDs.Body.Count;
			bool flag2 = drawinfo.drawPlayer.coat > 0 && drawinfo.drawPlayer.coat < ArmorIDs.Body.Count;
			int num2 = (drawinfo.compShoulderOverFrontArm ? 1 : 0);
			int num3 = (drawinfo.compShoulderOverFrontArm ? 0 : 1);
			int num4 = (drawinfo.compShoulderOverFrontArm ? 0 : 1);
			bool flag3 = !drawinfo.hidesTopSkin;
			if (flag)
			{
				if (!drawinfo.drawPlayer.invis || PlayerDrawLayers.IsArmorDrawnWhenInvisible(drawinfo.drawPlayer.body))
				{
					Texture2D value = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.body].Value;
					for (int i = 0; i < 2; i++)
					{
						if (!drawinfo.drawPlayer.invis && i == num4 && flag3)
						{
							if (drawinfo.missingArm)
							{
								List<DrawData> drawDataCache = drawinfo.DrawDataCache;
								DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorBodySkin, num, vector3, 1f, drawinfo.playerEffect, 0f)
								{
									shader = drawinfo.skinDyePacked
								};
								drawDataCache.Add(drawData);
							}
							if (drawinfo.missingHand)
							{
								List<DrawData> drawDataCache2 = drawinfo.DrawDataCache;
								DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 9].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorBodySkin, num, vector3, 1f, drawinfo.playerEffect, 0f)
								{
									shader = drawinfo.skinDyePacked
								};
								drawDataCache2.Add(drawData);
							}
						}
						if (i == num2 && !drawinfo.hideCompositeShoulders)
						{
							CompositePlayerDrawContext compositePlayerDrawContext = CompositePlayerDrawContext.FrontShoulder;
							DrawData drawData = new DrawData(value, vector4, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorArmorBody, bodyRotation, vector3, 1f, drawinfo.playerEffect, 0f)
							{
								shader = drawinfo.cBody
							};
							PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext, drawData, drawinfo.drawPlayer.body);
							if (drawinfo.drawPlayer.body == 71)
							{
								Texture2D value2 = TextureAssets.Extra[277].Value;
								CompositePlayerDrawContext compositePlayerDrawContext2 = CompositePlayerDrawContext.FrontShoulder;
								drawData = new DrawData(value2, vector4, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorArmorBody, bodyRotation, vector3, 1f, drawinfo.playerEffect, 0f)
								{
									shader = 0
								};
								PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext2, drawData, drawinfo.drawPlayer.body);
								if (drawinfo.drawPlayer.legs == 60)
								{
									Texture2D value3 = TextureAssets.Extra[275].Value;
									CompositePlayerDrawContext compositePlayerDrawContext3 = CompositePlayerDrawContext.FrontShoulder;
									drawData = new DrawData(value3, vector4, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorArmorBody, bodyRotation, vector3, 1f, drawinfo.playerEffect, 0f)
									{
										shader = drawinfo.cLegs
									};
									PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext3, drawData, drawinfo.drawPlayer.body);
								}
							}
						}
						if (i == num3)
						{
							CompositePlayerDrawContext compositePlayerDrawContext4 = CompositePlayerDrawContext.FrontArm;
							DrawData drawData = new DrawData(value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorArmorBody, num, vector3, 1f, drawinfo.playerEffect, 0f)
							{
								shader = drawinfo.cBody
							};
							PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext4, drawData, drawinfo.drawPlayer.body);
							if (drawinfo.drawPlayer.body == 71)
							{
								Texture2D value4 = TextureAssets.Extra[277].Value;
								CompositePlayerDrawContext compositePlayerDrawContext5 = CompositePlayerDrawContext.FrontArm;
								drawData = new DrawData(value4, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorArmorBody, num, vector3, 1f, drawinfo.playerEffect, 0f)
								{
									shader = 0
								};
								PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext5, drawData, drawinfo.drawPlayer.body);
							}
						}
					}
				}
			}
			else if (!drawinfo.drawPlayer.invis)
			{
				for (int j = 0; j < 2; j++)
				{
					if (j == num2)
					{
						if (flag3)
						{
							List<DrawData> drawDataCache3 = drawinfo.DrawDataCache;
							DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, vector4, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorBodySkin, bodyRotation, vector3, 1f, drawinfo.playerEffect, 0f)
							{
								shader = drawinfo.skinDyePacked
							};
							drawDataCache3.Add(drawData);
						}
						if (!drawinfo.hideCompositeShoulders)
						{
							drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, vector4, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorUnderShirt, bodyRotation, vector3, 1f, drawinfo.playerEffect, 0f));
							drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, vector4, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorShirt, bodyRotation, vector3, 1f, drawinfo.playerEffect, 0f));
							drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, vector4, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorShirt, bodyRotation, vector3, 1f, drawinfo.playerEffect, 0f));
							if (drawinfo.drawPlayer.head == 269)
							{
								Vector2 vector5 = drawinfo.helmetOffset + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.headPosition + drawinfo.headVect;
								drawinfo.drawPlayer.ApplyHeadOffsetFromMount(ref vector5);
								DrawData drawData2 = new DrawData(TextureAssets.Extra[214].Value, vector5, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorHead, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
								drawData2.shader = drawinfo.cHead;
								drawinfo.DrawDataCache.Add(drawData2);
								drawData2 = new DrawData(TextureAssets.GlowMask[308].Value, vector5, new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.headGlowColor, drawinfo.drawPlayer.headRotation, drawinfo.headVect, 1f, drawinfo.playerEffect, 0f);
								drawData2.shader = drawinfo.cHead;
								drawinfo.DrawDataCache.Add(drawData2);
							}
						}
					}
					if (j == num3)
					{
						if (flag3)
						{
							List<DrawData> drawDataCache4 = drawinfo.DrawDataCache;
							DrawData drawData = new DrawData(TextureAssets.Players[drawinfo.skinVar, 7].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorBodySkin, num, vector3, 1f, drawinfo.playerEffect, 0f)
							{
								shader = drawinfo.skinDyePacked
							};
							drawDataCache4.Add(drawData);
						}
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 8].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorUnderShirt, num, vector3, 1f, drawinfo.playerEffect, 0f));
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 13].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorShirt, num, vector3, 1f, drawinfo.playerEffect, 0f));
						drawinfo.DrawDataCache.Add(new DrawData(TextureAssets.Players[drawinfo.skinVar, 6].Value, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorShirt, num, vector3, 1f, drawinfo.playerEffect, 0f));
					}
				}
			}
			if (flag2 && (!drawinfo.drawPlayer.invis || PlayerDrawLayers.IsArmorDrawnWhenInvisible(drawinfo.drawPlayer.coat)))
			{
				Texture2D value5 = TextureAssets.ArmorBodyComposite[drawinfo.drawPlayer.coat].Value;
				for (int k = 0; k < 2; k++)
				{
					if (k == num2 && !drawinfo.hideCompositeShoulders)
					{
						CompositePlayerDrawContext compositePlayerDrawContext6 = CompositePlayerDrawContext.FrontShoulder;
						DrawData drawData = new DrawData(value5, vector4, new Rectangle?(drawinfo.compFrontShoulderFrame), drawinfo.colorArmorBody, bodyRotation, vector3, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.cCoat
						};
						PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext6, drawData, drawinfo.drawPlayer.coat);
					}
					if (k == num3)
					{
						CompositePlayerDrawContext compositePlayerDrawContext7 = CompositePlayerDrawContext.FrontArm;
						DrawData drawData = new DrawData(value5, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorArmorBody, num, vector3, 1f, drawinfo.playerEffect, 0f)
						{
							shader = drawinfo.cCoat
						};
						PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext7, drawData, drawinfo.drawPlayer.coat);
					}
				}
			}
			if (drawinfo.drawPlayer.handon > 0 && (int)drawinfo.drawPlayer.handon < ArmorIDs.HandOn.Count)
			{
				Texture2D value6 = TextureAssets.AccHandsOnComposite[(int)drawinfo.drawPlayer.handon].Value;
				CompositePlayerDrawContext compositePlayerDrawContext8 = CompositePlayerDrawContext.FrontArmAccessory;
				DrawData drawData = new DrawData(value6, vector, new Rectangle?(drawinfo.compFrontArmFrame), drawinfo.colorArmorBody, num, vector3, 1f, drawinfo.playerEffect, 0f)
				{
					shader = drawinfo.cHandOn
				};
				PlayerDrawLayers.DrawCompositeArmorPiece(ref drawinfo, compositePlayerDrawContext8, drawData, -1);
			}
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x0064BF74 File Offset: 0x0064A174
		public static void DrawPlayer_29_OnhandAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.usesCompositeFrontHandAcc)
			{
				return;
			}
			if (drawinfo.drawPlayer.handon > 0 && (int)drawinfo.drawPlayer.handon < ArmorIDs.HandOn.Count)
			{
				DrawData drawData = new DrawData(TextureAssets.AccHandsOn[(int)drawinfo.drawPlayer.handon].Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cHandOn;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x0064C0DC File Offset: 0x0064A2DC
		public static void DrawPlayer_30_BladedGlove(ref PlayerDrawSet drawinfo)
		{
			Item heldItem = drawinfo.heldItem;
			if (heldItem.type > -1 && Item.claw[heldItem.type] && drawinfo.shadow == 0f)
			{
				Main.instance.LoadItem(heldItem.type);
				Asset<Texture2D> asset = TextureAssets.Item[heldItem.type];
				if (!drawinfo.drawPlayer.frozen && (drawinfo.drawPlayer.itemAnimation > 0 || (heldItem.holdStyle != 0 && !drawinfo.drawPlayer.pulley)) && heldItem.type > 0 && !drawinfo.drawPlayer.dead && !heldItem.noUseGraphic && (!drawinfo.drawPlayer.wet || !heldItem.noWet))
				{
					DrawData drawData;
					if (drawinfo.drawPlayer.gravDir == -1f)
					{
						drawData = new DrawData(asset.Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, asset.Width(), asset.Height())), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, new Vector2((float)asset.Width() * 0.5f - (float)asset.Width() * 0.5f * (float)drawinfo.drawPlayer.direction, 0f), drawinfo.drawPlayer.GetAdjustedItemScale(heldItem), drawinfo.itemEffect, 0f);
						drawinfo.DrawDataCache.Add(drawData);
						return;
					}
					drawData = new DrawData(asset.Value, new Vector2((float)((int)(drawinfo.ItemLocation.X - Main.screenPosition.X)), (float)((int)(drawinfo.ItemLocation.Y - Main.screenPosition.Y))), new Rectangle?(new Rectangle(0, 0, asset.Width(), asset.Height())), heldItem.GetAlpha(drawinfo.itemColor), drawinfo.drawPlayer.itemRotation, new Vector2((float)asset.Width() * 0.5f - (float)asset.Width() * 0.5f * (float)drawinfo.drawPlayer.direction, (float)asset.Height()), drawinfo.drawPlayer.GetAdjustedItemScale(heldItem), drawinfo.itemEffect, 0f);
					drawinfo.DrawDataCache.Add(drawData);
				}
			}
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x0064C356 File Offset: 0x0064A556
		public static void DrawPlayer_31_ProjectileOverArm(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.SelectedDrawnProjectile != null && drawinfo.shadow == 0f && drawinfo.SelectedDrawnProjectile.drawLayer == 8)
			{
				drawinfo.projectileDrawPosition = drawinfo.DrawDataCache.Count;
			}
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x0064C38C File Offset: 0x0064A58C
		public static void DrawPlayer_32_FrontAcc(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.front > 0 && (int)drawinfo.drawPlayer.front < ArmorIDs.Front.Count && !drawinfo.drawPlayer.mount.Active)
			{
				Vector2 zero = Vector2.Zero;
				DrawData drawData = new DrawData(TextureAssets.AccFront[(int)drawinfo.drawPlayer.front].Value, zero + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(drawinfo.drawPlayer.bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, drawinfo.bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cFront;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x0064C50C File Offset: 0x0064A70C
		public static void DrawPlayer_32_FrontAcc_FrontPart(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.front > 0 && (int)drawinfo.drawPlayer.front < ArmorIDs.Front.Count)
			{
				if (ArmorIDs.Front.Sets.DontDrawIfWearingAScarfOrCape[(int)drawinfo.drawPlayer.front] && ((drawinfo.drawPlayer.neck > 0 && ArmorIDs.Neck.Sets.IsAScarf[(int)drawinfo.drawPlayer.neck]) || (drawinfo.drawPlayer.back > 0 && ArmorIDs.Back.Sets.IsACape[(int)drawinfo.drawPlayer.back])))
				{
					return;
				}
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				int num = bodyFrame.Width / 2;
				bodyFrame.Width -= num;
				Vector2 bodyVect = drawinfo.bodyVect;
				if ((drawinfo.playerEffect & SpriteEffects.FlipHorizontally) != SpriteEffects.None)
				{
					bodyVect.X -= (float)num;
				}
				Vector2 vector = drawinfo.drawPlayer.GetFrontDrawOffset() + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
				DrawData drawData = new DrawData(TextureAssets.AccFront[(int)drawinfo.drawPlayer.front].Value, vector, new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cFront;
				drawinfo.DrawDataCache.Add(drawData);
				if (drawinfo.drawPlayer.front == 12)
				{
					Rectangle rectangle = bodyFrame;
					Rectangle rectangle2 = rectangle;
					rectangle2.Width = 2;
					int num2 = 0;
					int num3 = rectangle.Width / 2;
					int num4 = 2;
					if ((drawinfo.playerEffect & SpriteEffects.FlipHorizontally) != SpriteEffects.None)
					{
						num2 = rectangle.Width - 2;
						num4 = -2;
					}
					for (int i = 0; i < num3; i++)
					{
						rectangle2.X = rectangle.X + 2 * i;
						Color color = drawinfo.drawPlayer.GetImmuneAlpha(LiquidRenderer.GetShimmerGlitterColor(true, (float)i / 16f, 0f), drawinfo.shadow);
						color *= (float)drawinfo.colorArmorBody.A / 255f;
						drawData = new DrawData(TextureAssets.GlowMask[331].Value, vector + new Vector2((float)(num2 + i * num4), 0f), new Rectangle?(rectangle2), color, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cFront;
						drawinfo.DrawDataCache.Add(drawData);
					}
				}
			}
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x0064C82C File Offset: 0x0064AA2C
		public static void DrawPlayer_32_FrontAcc_BackPart(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.front > 0 && (int)drawinfo.drawPlayer.front < ArmorIDs.Front.Count)
			{
				if (ArmorIDs.Front.Sets.DontDrawIfWearingAScarfOrCape[(int)drawinfo.drawPlayer.front] && ((drawinfo.drawPlayer.neck > 0 && ArmorIDs.Neck.Sets.IsAScarf[(int)drawinfo.drawPlayer.neck]) || (drawinfo.drawPlayer.back > 0 && ArmorIDs.Back.Sets.IsACape[(int)drawinfo.drawPlayer.back])))
				{
					return;
				}
				Rectangle bodyFrame = drawinfo.drawPlayer.bodyFrame;
				int num = bodyFrame.Width / 2;
				bodyFrame.Width -= num;
				bodyFrame.X += num;
				Vector2 bodyVect = drawinfo.bodyVect;
				if ((drawinfo.playerEffect & SpriteEffects.FlipHorizontally) == SpriteEffects.None)
				{
					bodyVect.X -= (float)num;
				}
				Vector2 vector = drawinfo.drawPlayer.GetFrontDrawOffset() + new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2));
				DrawData drawData = new DrawData(TextureAssets.AccFront[(int)drawinfo.drawPlayer.front].Value, vector, new Rectangle?(bodyFrame), drawinfo.colorArmorBody, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
				drawData.shader = drawinfo.cFront;
				drawinfo.DrawDataCache.Add(drawData);
				if (drawinfo.drawPlayer.front == 12)
				{
					Rectangle rectangle = bodyFrame;
					Rectangle rectangle2 = rectangle;
					rectangle2.Width = 2;
					int num2 = 0;
					int num3 = rectangle.Width / 2;
					int num4 = 2;
					if ((drawinfo.playerEffect & SpriteEffects.FlipHorizontally) != SpriteEffects.None)
					{
						num2 = rectangle.Width - 2;
						num4 = -2;
					}
					for (int i = 0; i < num3; i++)
					{
						rectangle2.X = rectangle.X + 2 * i;
						Color color = drawinfo.drawPlayer.GetImmuneAlpha(LiquidRenderer.GetShimmerGlitterColor(true, (float)i / 16f, 0f), drawinfo.shadow);
						color *= (float)drawinfo.colorArmorBody.A / 255f;
						drawData = new DrawData(TextureAssets.GlowMask[331].Value, vector + new Vector2((float)(num2 + i * num4), 0f), new Rectangle?(rectangle2), color, drawinfo.drawPlayer.bodyRotation, bodyVect, 1f, drawinfo.playerEffect, 0f);
						drawData.shader = drawinfo.cFront;
						drawinfo.DrawDataCache.Add(drawData);
					}
				}
			}
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x0064CB58 File Offset: 0x0064AD58
		public static void DrawPlayer_33_FrozenOrWebbedDebuff(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.shimmering)
			{
				return;
			}
			if (drawinfo.drawPlayer.frozen && drawinfo.shadow == 0f)
			{
				Color colorArmorBody = drawinfo.colorArmorBody;
				colorArmorBody.R = (byte)((double)colorArmorBody.R * 0.55);
				colorArmorBody.G = (byte)((double)colorArmorBody.G * 0.55);
				colorArmorBody.B = (byte)((double)colorArmorBody.B * 0.55);
				colorArmorBody.A = (byte)((double)colorArmorBody.A * 0.55);
				DrawData drawData = new DrawData(TextureAssets.Frozen.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(new Rectangle(0, 0, TextureAssets.Frozen.Width(), TextureAssets.Frozen.Height())), colorArmorBody, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(TextureAssets.Frozen.Width() / 2), (float)(TextureAssets.Frozen.Height() / 2)), 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
				return;
			}
			if (drawinfo.drawPlayer.webbed && drawinfo.shadow == 0f && drawinfo.drawPlayer.velocity.Y == 0f)
			{
				Color color = drawinfo.colorArmorBody * 0.75f;
				Texture2D value = TextureAssets.Extra[31].Value;
				int num = drawinfo.drawPlayer.height / 2;
				DrawData drawData = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f + (float)num))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), null, color, drawinfo.drawPlayer.bodyRotation, value.Size() / 2f, 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x0064CEAC File Offset: 0x0064B0AC
		public static void DrawPlayer_34_ElectrifiedDebuffFront(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.electrified && drawinfo.shadow == 0f)
			{
				Texture2D value = TextureAssets.GlowMask[25].Value;
				int num = drawinfo.drawPlayer.miscCounter / 5;
				for (int i = 0; i < 2; i++)
				{
					num %= 7;
					if (num > 1 && num < 5)
					{
						DrawData drawData = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(new Rectangle(0, num * value.Height / 7, value.Width, value.Height / 7)), drawinfo.colorElectricity, drawinfo.drawPlayer.bodyRotation, new Vector2((float)(value.Width / 2), (float)(value.Height / 14)), 1f, drawinfo.playerEffect, 0f);
						drawinfo.DrawDataCache.Add(drawData);
					}
					num += 3;
				}
			}
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x0064D04C File Offset: 0x0064B24C
		public static void DrawPlayer_35_IceBarrier(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.iceBarrier && drawinfo.shadow == 0f)
			{
				int num = TextureAssets.IceBarrier.Height() / 12;
				Color white = Color.White;
				DrawData drawData = new DrawData(TextureAssets.IceBarrier.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X - (float)(drawinfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)drawinfo.drawPlayer.height - (float)drawinfo.drawPlayer.bodyFrame.Height + 4f))) + drawinfo.drawPlayer.bodyPosition + new Vector2((float)(drawinfo.drawPlayer.bodyFrame.Width / 2), (float)(drawinfo.drawPlayer.bodyFrame.Height / 2)), new Rectangle?(new Rectangle(0, num * (int)drawinfo.drawPlayer.iceBarrierFrame, TextureAssets.IceBarrier.Width(), num)), white, 0f, new Vector2((float)(TextureAssets.Frozen.Width() / 2), (float)(TextureAssets.Frozen.Height() / 2)), 1f, drawinfo.playerEffect, 0f);
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x0064D1C0 File Offset: 0x0064B3C0
		public static void DrawPlayer_36_CTG(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.shadow == 0f && drawinfo.drawPlayer.ownedLargeGems > 0)
			{
				bool flag = false;
				BitsByte ownedLargeGems = drawinfo.drawPlayer.ownedLargeGems;
				float num = 0f;
				for (int i = 0; i < 7; i++)
				{
					if (ownedLargeGems[i])
					{
						num += 1f;
					}
				}
				float num2 = 1f - num * 0.06f;
				float num3 = (num - 1f) * 4f;
				switch ((int)num)
				{
				case 2:
					num3 += 10f;
					break;
				case 3:
					num3 += 8f;
					break;
				case 4:
					num3 += 6f;
					break;
				case 5:
					num3 += 6f;
					break;
				case 6:
					num3 += 2f;
					break;
				case 7:
					num3 += 0f;
					break;
				}
				float num4 = (float)drawinfo.drawPlayer.miscCounter / 300f * 6.2831855f;
				if (num > 0f)
				{
					float num5 = 6.2831855f / num;
					float num6 = 0f;
					Vector2 one = new Vector2(1.3f, 0.65f);
					if (!flag)
					{
						one = Vector2.One;
					}
					List<DrawData> list = new List<DrawData>();
					for (int j = 0; j < 7; j++)
					{
						if (!ownedLargeGems[j])
						{
							num6 += 1f;
						}
						else
						{
							Vector2 vector = (num4 + num5 * ((float)j - num6)).ToRotationVector2();
							float num7 = num2;
							if (flag)
							{
								num7 = MathHelper.Lerp(num2 * 0.7f, 1f, vector.Y / 2f + 0.5f);
							}
							Texture2D value = TextureAssets.Gem[j].Value;
							int num8 = 38;
							DrawData drawData = new DrawData(value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y - (float)num8))) + vector * one * num3, null, new Color(250, 250, 250, (int)(Main.mouseTextColor / 2)), 0f, value.Size() / 2f, ((float)Main.mouseTextColor / 1000f + 0.8f) * num7, SpriteEffects.None, 0f);
							list.Add(drawData);
						}
					}
					if (flag)
					{
						list.Sort(new Comparison<DrawData>(DelegateMethods.CompareDrawSorterByYScale));
					}
					drawinfo.DrawDataCache.AddRange(list);
				}
			}
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x0064D478 File Offset: 0x0064B678
		public static void DrawPlayer_37_BeetleBuff(ref PlayerDrawSet drawinfo)
		{
			if ((drawinfo.drawPlayer.beetleOffense || drawinfo.drawPlayer.beetleDefense) && drawinfo.shadow == 0f)
			{
				for (int i = 0; i < drawinfo.drawPlayer.beetleOrbs; i++)
				{
					DrawData drawData;
					for (int j = 0; j < 5; j++)
					{
						Color colorArmorBody = drawinfo.colorArmorBody;
						float num = (float)j * 0.1f;
						num = 0.5f - num;
						colorArmorBody.R = (byte)((float)colorArmorBody.R * num);
						colorArmorBody.G = (byte)((float)colorArmorBody.G * num);
						colorArmorBody.B = (byte)((float)colorArmorBody.B * num);
						colorArmorBody.A = (byte)((float)colorArmorBody.A * num);
						Vector2 vector = -drawinfo.drawPlayer.beetleVel[i] * (float)j;
						drawData = new DrawData(TextureAssets.Beetle.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2)))) + drawinfo.drawPlayer.beetlePos[i] + vector, new Rectangle?(new Rectangle(0, TextureAssets.Beetle.Height() / 3 * drawinfo.drawPlayer.beetleFrame + 1, TextureAssets.Beetle.Width(), TextureAssets.Beetle.Height() / 3 - 2)), colorArmorBody, 0f, new Vector2((float)(TextureAssets.Beetle.Width() / 2), (float)(TextureAssets.Beetle.Height() / 6)), 1f, drawinfo.playerEffect, 0f);
						drawinfo.DrawDataCache.Add(drawData);
					}
					drawData = new DrawData(TextureAssets.Beetle.Value, new Vector2((float)((int)(drawinfo.Position.X - Main.screenPosition.X + (float)(drawinfo.drawPlayer.width / 2))), (float)((int)(drawinfo.Position.Y - Main.screenPosition.Y + (float)(drawinfo.drawPlayer.height / 2)))) + drawinfo.drawPlayer.beetlePos[i], new Rectangle?(new Rectangle(0, TextureAssets.Beetle.Height() / 3 * drawinfo.drawPlayer.beetleFrame + 1, TextureAssets.Beetle.Width(), TextureAssets.Beetle.Height() / 3 - 2)), drawinfo.colorArmorBody, 0f, new Vector2((float)(TextureAssets.Beetle.Width() / 2), (float)(TextureAssets.Beetle.Height() / 6)), 1f, drawinfo.playerEffect, 0f);
					drawinfo.DrawDataCache.Add(drawData);
				}
			}
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x0064D764 File Offset: 0x0064B964
		public static void DrawPlayer_38_EyebrellaCloud(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.drawPlayer.eyebrellaCloud && drawinfo.shadow == 0f)
			{
				Texture2D value = TextureAssets.Projectile[238].Value;
				int num = drawinfo.drawPlayer.miscCounter % 18 / 6;
				Rectangle rectangle = value.Frame(1, 6, 0, num, 0, 0);
				Vector2 vector = new Vector2((float)(rectangle.Width / 2), (float)(rectangle.Height / 2));
				Vector2 vector2 = new Vector2(0f, -70f);
				drawinfo.drawPlayer.ApplyHeadOffsetFromMount(ref vector2);
				if (drawinfo.drawPlayer.mount.Active && drawinfo.drawPlayer.mount.Type == 54)
				{
					vector2 += new Vector2(-14f, 0f) * drawinfo.drawPlayer.Directions;
				}
				Vector2 vector3 = drawinfo.drawPlayer.MountedCenter - new Vector2(0f, (float)drawinfo.drawPlayer.height * 0.5f) + vector2 - Main.screenPosition;
				Color color = Lighting.GetColor((drawinfo.drawPlayer.Top + new Vector2(0f, -30f)).ToTileCoordinates());
				int num2 = 170;
				int num5;
				int num4;
				int num3 = (num4 = (num5 = num2));
				if ((int)color.R < num2)
				{
					num4 = (int)color.R;
				}
				if ((int)color.G < num2)
				{
					num3 = (int)color.G;
				}
				if ((int)color.B < num2)
				{
					num5 = (int)color.B;
				}
				Color color2 = new Color(num4, num3, num5, 100);
				float num6 = (float)(drawinfo.drawPlayer.miscCounter % 50) / 50f;
				float num7 = 3f;
				DrawData drawData;
				for (int i = 0; i < 2; i++)
				{
					Vector2 vector4 = new Vector2((i == 0) ? (-num7) : num7, 0f).RotatedBy((double)(num6 * 6.2831855f * ((i == 0) ? 1f : (-1f))), default(Vector2));
					drawData = new DrawData(value, vector3 + vector4, new Rectangle?(rectangle), color2 * 0.65f, 0f, vector, 1f, (drawinfo.drawPlayer.gravDir == -1f) ? SpriteEffects.FlipVertically : SpriteEffects.None, 0f);
					drawData.shader = drawinfo.cHead;
					drawData.ignorePlayerRotation = true;
					drawinfo.DrawDataCache.Add(drawData);
				}
				drawData = new DrawData(value, vector3, new Rectangle?(rectangle), color2, 0f, vector, 1f, (drawinfo.drawPlayer.gravDir == -1f) ? SpriteEffects.FlipVertically : SpriteEffects.None, 0f);
				drawData.shader = drawinfo.cHead;
				drawData.ignorePlayerRotation = true;
				drawinfo.DrawDataCache.Add(drawData);
			}
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x0064DA44 File Offset: 0x0064BC44
		private static Vector2 GetCompositeOffset_BackArm(ref PlayerDrawSet drawinfo)
		{
			return new Vector2((float)(6 * (((drawinfo.playerEffect & SpriteEffects.FlipHorizontally) != SpriteEffects.None) ? (-1) : 1)), (float)(2 * (((drawinfo.playerEffect & SpriteEffects.FlipVertically) != SpriteEffects.None) ? (-1) : 1)));
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x0064DA6D File Offset: 0x0064BC6D
		private static Vector2 GetCompositeOffset_FrontArm(ref PlayerDrawSet drawinfo)
		{
			return new Vector2((float)(-5 * (((drawinfo.playerEffect & SpriteEffects.FlipHorizontally) != SpriteEffects.None) ? (-1) : 1)), 0f);
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x0064DA8C File Offset: 0x0064BC8C
		public static void DrawPlayer_TransformDrawData(ref PlayerDrawSet drawinfo)
		{
			float rotation = drawinfo.rotation;
			Vector2 vector = drawinfo.Position - Main.screenPosition + drawinfo.rotationOrigin;
			Vector2 vector2 = drawinfo.drawPlayer.position + drawinfo.rotationOrigin;
			Matrix matrix = Matrix.CreateRotationZ(drawinfo.rotation);
			for (int i = 0; i < drawinfo.DustCache.Count; i++)
			{
				Vector2 vector3 = Main.dust[drawinfo.DustCache[i]].position - vector2;
				vector3 = Vector2.Transform(vector3, matrix);
				Main.dust[drawinfo.DustCache[i]].position = vector3 + vector2;
			}
			for (int j = 0; j < drawinfo.GoreCache.Count; j++)
			{
				Vector2 vector4 = Main.gore[drawinfo.GoreCache[j]].position - vector2;
				vector4 = Vector2.Transform(vector4, matrix);
				Main.gore[drawinfo.GoreCache[j]].position = vector4 + vector2;
			}
			for (int k = 0; k < drawinfo.DrawDataCache.Count; k++)
			{
				DrawData drawData = drawinfo.DrawDataCache[k];
				if (!drawData.ignorePlayerRotation)
				{
					Vector2 vector5 = drawData.position - vector;
					vector5 = Vector2.Transform(vector5, matrix);
					drawData.position = vector5 + vector;
					drawData.rotation += drawinfo.rotation;
					drawinfo.DrawDataCache[k] = drawData;
				}
			}
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x0064DC24 File Offset: 0x0064BE24
		public static void DrawPlayer_ScaleDrawData(ref PlayerDrawSet drawinfo, float scale)
		{
			if (scale == 1f)
			{
				return;
			}
			Vector2 vector = drawinfo.Position + drawinfo.drawPlayer.Size * new Vector2(0.5f, 1f) - Main.screenPosition;
			for (int i = 0; i < drawinfo.DrawDataCache.Count; i++)
			{
				DrawData drawData = drawinfo.DrawDataCache[i];
				Vector2 vector2 = drawData.position - vector;
				drawData.position = vector + vector2 * scale;
				drawData.scale *= scale;
				drawinfo.DrawDataCache[i] = drawData;
			}
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x0064DCD8 File Offset: 0x0064BED8
		public static void DrawPlayer_AddSelectionGlow(ref PlayerDrawSet drawinfo)
		{
			if (drawinfo.selectionGlowColor == Color.Transparent)
			{
				return;
			}
			Color selectionGlowColor = drawinfo.selectionGlowColor;
			List<DrawData> list = new List<DrawData>();
			list.AddRange(PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(0f, -2f), selectionGlowColor));
			list.AddRange(PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(0f, 2f), selectionGlowColor));
			list.AddRange(PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(2f, 0f), selectionGlowColor));
			list.AddRange(PlayerDrawLayers.GetFlatColoredCloneData(ref drawinfo, new Vector2(-2f, 0f), selectionGlowColor));
			list.AddRange(drawinfo.DrawDataCache);
			drawinfo.DrawDataCache = list;
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x0064DD88 File Offset: 0x0064BF88
		public static void DrawPlayer_MakeIntoFirstFractalAfterImage(ref PlayerDrawSet drawinfo)
		{
			if (!drawinfo.drawPlayer.isFirstFractalAfterImage)
			{
				if (drawinfo.drawPlayer.HeldItem.type == 4722)
				{
					bool flag = drawinfo.drawPlayer.itemAnimation > 0;
				}
				return;
			}
			for (int i = 0; i < drawinfo.DrawDataCache.Count; i++)
			{
				DrawData drawData = drawinfo.DrawDataCache[i];
				drawData.color *= drawinfo.drawPlayer.firstFractalAfterImageOpacity;
				drawData.color.A = (byte)((float)drawData.color.A * 0.8f);
				drawinfo.DrawDataCache[i] = drawData;
			}
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x0064DE40 File Offset: 0x0064C040
		public static void DrawPlayer_RenderAllLayers(ref PlayerDrawSet drawinfo)
		{
			SpriteDrawBuffer spriteBuffer = Main.spriteBuffer;
			List<DrawData> drawDataCache = drawinfo.DrawDataCache;
			foreach (DrawData drawData in drawDataCache)
			{
				if (drawData.texture != null)
				{
					drawData.Draw(spriteBuffer);
				}
			}
			DrawData drawData2 = default(DrawData);
			int num = 0;
			for (int i = 0; i <= drawDataCache.Count; i++)
			{
				if (drawinfo.projectileDrawPosition == i)
				{
					if (drawData2.shader != 0)
					{
						Main.pixelShader.CurrentTechnique.Passes[0].Apply();
					}
					spriteBuffer.Unbind();
					PlayerDrawLayers.DrawHeldProj(drawinfo, drawinfo.SelectedDrawnProjectile);
				}
				if (i != drawDataCache.Count)
				{
					drawData2 = drawDataCache[i];
					if (drawData2.sourceRect == null)
					{
						drawData2.sourceRect = new Rectangle?(drawData2.texture.Frame(1, 1, 0, 0, 0, 0));
					}
					PlayerDrawHelper.SetShaderForData(drawinfo.drawPlayer, drawinfo.cHead, ref drawData2);
					if (drawData2.texture != null)
					{
						spriteBuffer.DrawSingle(num++);
					}
				}
			}
			spriteBuffer.Unbind();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x0064DF94 File Offset: 0x0064C194
		private static void DrawHeldProj(PlayerDrawSet drawinfo, Projectile proj)
		{
			SamplerState samplerState = Main.graphics.GraphicsDevice.SamplerStates[0];
			Player player = Main.player[proj.owner];
			player.position += player.netOffset;
			proj.position += player.netOffset;
			try
			{
				Main.instance.DrawProjDirect(proj, drawinfo.drawPlayer);
				if (proj.type == 595 || proj.type == 735 || proj.type == 927)
				{
					Main.instance.DrawProjDirect(proj, drawinfo.drawPlayer);
				}
			}
			catch
			{
				proj.active = false;
			}
			player.position -= player.netOffset;
			proj.position -= player.netOffset;
			Main.graphics.GraphicsDevice.SamplerStates[0] = samplerState;
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x0064E09C File Offset: 0x0064C29C
		public static void DrawPlayer_RenderAllLayersSlow(ref PlayerDrawSet drawinfo)
		{
			int num = -1;
			List<DrawData> drawDataCache = drawinfo.DrawDataCache;
			Effect pixelShader = Main.pixelShader;
			Projectile[] projectile = Main.projectile;
			SpriteBatch spriteBatch = Main.spriteBatch;
			for (int i = 0; i <= drawDataCache.Count; i++)
			{
				if (drawinfo.projectileDrawPosition == i)
				{
					if (num != 0)
					{
						pixelShader.CurrentTechnique.Passes[0].Apply();
						num = 0;
					}
					try
					{
						Main.instance.DrawProjDirect(drawinfo.SelectedDrawnProjectile, drawinfo.drawPlayer);
					}
					catch
					{
						drawinfo.SelectedDrawnProjectile.active = false;
					}
				}
				if (i != drawDataCache.Count)
				{
					DrawData drawData = drawDataCache[i];
					if (drawData.sourceRect == null)
					{
						drawData.sourceRect = new Rectangle?(drawData.texture.Frame(1, 1, 0, 0, 0, 0));
					}
					PlayerDrawHelper.SetShaderForData(drawinfo.drawPlayer, drawinfo.cHead, ref drawData);
					num = drawData.shader;
					if (drawData.texture != null)
					{
						drawData.Draw(spriteBatch);
					}
				}
			}
			pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x0064E1BC File Offset: 0x0064C3BC
		public static void DrawPlayer_DrawSelectionRect(ref PlayerDrawSet drawinfo)
		{
			Vector2 vector;
			Vector2 vector2;
			SpriteRenderTargetHelper.GetDrawBoundary(drawinfo.DrawDataCache, out vector, out vector2);
			Utils.DrawRect(Main.spriteBatch, vector + Main.screenPosition, vector2 + Main.screenPosition, Color.White);
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x0064E1FD File Offset: 0x0064C3FD
		private static bool IsArmorDrawnWhenInvisible(int torsoID)
		{
			return torsoID - 21 > 1;
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x0064E20C File Offset: 0x0064C40C
		private static DrawData[] GetFlatColoredCloneData(ref PlayerDrawSet drawinfo, Vector2 offset, Color color)
		{
			int colorOnlyShaderIndex = ContentSamples.DyeShaderIDs.ColorOnlyShaderIndex;
			DrawData[] array = new DrawData[drawinfo.DrawDataCache.Count];
			for (int i = 0; i < drawinfo.DrawDataCache.Count; i++)
			{
				DrawData drawData = drawinfo.DrawDataCache[i];
				drawData.position += offset;
				drawData.shader = colorOnlyShaderIndex;
				drawData.color = color;
				array[i] = drawData;
			}
			return array;
		}
	}
}
