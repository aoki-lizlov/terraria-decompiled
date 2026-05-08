using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Golf;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace Terraria.DataStructures
{
	// Token: 0x0200059C RID: 1436
	public struct PlayerDrawSet
	{
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06003882 RID: 14466 RVA: 0x00632C7C File Offset: 0x00630E7C
		public Vector2 Center
		{
			get
			{
				return new Vector2(this.Position.X + (float)(this.drawPlayer.width / 2), this.Position.Y + (float)(this.drawPlayer.height / 2));
			}
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x00632CB8 File Offset: 0x00630EB8
		public void BoringSetup(Player player, List<DrawData> drawData, List<int> dust, List<int> gore, Vector2 drawPosition, float shadowOpacity, float rotation, Vector2 rotationOrigin, Projectile overrideHeldProjectile)
		{
			this.DrawDataCache = drawData;
			this.SelectedDrawnProjectile = null;
			if (player.heldProj != -1)
			{
				this.SelectedDrawnProjectile = Main.projectile[player.heldProj];
			}
			if (overrideHeldProjectile != null)
			{
				this.SelectedDrawnProjectile = overrideHeldProjectile;
			}
			this.DustCache = dust;
			this.GoreCache = gore;
			this.drawPlayer = player;
			this.shadow = shadowOpacity;
			this.rotation = rotation;
			this.rotationOrigin = rotationOrigin;
			this.heldItem = player.lastVisualizedSelectedItem;
			this.cHead = this.drawPlayer.cHead;
			this.cBody = this.drawPlayer.cBody;
			this.cLegs = this.drawPlayer.cLegs;
			if (this.drawPlayer.wearsRobe)
			{
				this.cLegs = this.cBody;
			}
			this.cHandOn = this.drawPlayer.cHandOn;
			this.cHandOff = this.drawPlayer.cHandOff;
			this.cBack = this.drawPlayer.cBack;
			this.cFront = this.drawPlayer.cFront;
			this.cShoe = this.drawPlayer.cShoe;
			this.cFlameWaker = this.drawPlayer.cFlameWaker;
			this.cWaist = this.drawPlayer.cWaist;
			this.cShield = this.drawPlayer.cShield;
			this.cNeck = this.drawPlayer.cNeck;
			this.cFace = this.drawPlayer.cFace;
			this.cBalloon = this.drawPlayer.cBalloon;
			this.cWings = this.drawPlayer.cWings;
			this.cCarpet = this.drawPlayer.cCarpet;
			this.cPortableStool = this.drawPlayer.cPortableStool;
			this.cFloatingTube = this.drawPlayer.cFloatingTube;
			this.cUnicornHorn = this.drawPlayer.cUnicornHorn;
			this.cAngelHalo = this.drawPlayer.cAngelHalo;
			this.cLeinShampoo = this.drawPlayer.cLeinShampoo;
			this.cBackpack = this.drawPlayer.cBackpack;
			this.cTail = this.drawPlayer.cTail;
			this.cFaceHead = this.drawPlayer.cFaceHead;
			this.cFaceFlower = this.drawPlayer.cFaceFlower;
			this.cFaceMask = this.drawPlayer.cFaceMask;
			this.cBalloonFront = this.drawPlayer.cBalloonFront;
			this.cBeard = this.drawPlayer.cBeard;
			this.cCoat = this.drawPlayer.cCoat;
			this.isSitting = this.drawPlayer.sitting.isSitting;
			this.seatYOffset = 0f;
			this.sittingIndex = 0;
			Vector2 zero = Vector2.Zero;
			this.drawPlayer.sitting.GetSittingOffsetInfo(this.drawPlayer, out zero, out this.seatYOffset);
			if (this.isSitting)
			{
				this.sittingIndex = this.drawPlayer.sitting.sittingIndex;
			}
			if (this.drawPlayer.mount.Active && this.drawPlayer.mount.Type == 17)
			{
				this.isSitting = true;
			}
			if (this.drawPlayer.mount.Active && this.drawPlayer.mount.Type == 23)
			{
				this.isSitting = true;
			}
			if (this.drawPlayer.mount.Active && this.drawPlayer.mount.Type == 45)
			{
				this.isSitting = true;
			}
			this.isSleeping = this.drawPlayer.sleeping.isSleeping;
			this.Position = drawPosition;
			this.Position += new Vector2(this.drawPlayer.MountXOffset * (float)this.drawPlayer.direction, 0f);
			if (this.isSitting)
			{
				this.torsoOffset = this.seatYOffset;
				this.Position += zero;
			}
			else
			{
				this.sittingIndex = -1;
			}
			if (this.isSleeping)
			{
				this.rotationOrigin = player.Size / 2f;
				Vector2 vector;
				this.drawPlayer.sleeping.GetSleepingOffsetInfo(this.drawPlayer, out vector);
				this.Position += vector;
			}
			this.weaponDrawOrder = WeaponDrawOrder.BehindFrontArm;
			if (this.heldItem.type == 4952)
			{
				this.weaponDrawOrder = WeaponDrawOrder.BehindBackArm;
			}
			if (GolfHelper.IsPlayerHoldingClub(player) && player.itemAnimation > player.itemAnimationMax)
			{
				this.weaponDrawOrder = WeaponDrawOrder.OverFrontArm;
			}
			this.projectileDrawPosition = -1;
			this.ItemLocation = this.Position + (this.drawPlayer.itemLocation - this.drawPlayer.position);
			this.armorAdjust = 0;
			this.missingHand = false;
			this.missingArm = false;
			this.skinVar = this.drawPlayer.skinVariant;
			if (this.drawPlayer.body == 77 || this.drawPlayer.body == 103 || this.drawPlayer.body == 41 || this.drawPlayer.body == 100 || this.drawPlayer.body == 10 || this.drawPlayer.body == 11 || this.drawPlayer.body == 12 || this.drawPlayer.body == 13 || this.drawPlayer.body == 14 || this.drawPlayer.body == 43 || this.drawPlayer.body == 15 || this.drawPlayer.body == 16 || this.drawPlayer.body == 20 || this.drawPlayer.body == 39 || this.drawPlayer.body == 50 || this.drawPlayer.body == 38 || this.drawPlayer.body == 40 || this.drawPlayer.body == 57 || this.drawPlayer.body == 44 || this.drawPlayer.body == 52 || this.drawPlayer.body == 53 || this.drawPlayer.body == 68 || this.drawPlayer.body == 81 || this.drawPlayer.body == 85 || this.drawPlayer.body == 88 || this.drawPlayer.body == 98 || this.drawPlayer.body == 86 || this.drawPlayer.body == 87 || this.drawPlayer.body == 99 || this.drawPlayer.body == 165 || this.drawPlayer.body == 166 || this.drawPlayer.body == 167 || this.drawPlayer.body == 171 || this.drawPlayer.body == 45 || this.drawPlayer.body == 168 || this.drawPlayer.body == 169 || this.drawPlayer.body == 42 || this.drawPlayer.body == 180 || this.drawPlayer.body == 181 || this.drawPlayer.body == 183 || this.drawPlayer.body == 186 || this.drawPlayer.body == 187 || this.drawPlayer.body == 188 || this.drawPlayer.body == 64 || this.drawPlayer.body == 189 || this.drawPlayer.body == 191 || this.drawPlayer.body == 192 || this.drawPlayer.body == 198 || this.drawPlayer.body == 199 || this.drawPlayer.body == 202 || this.drawPlayer.body == 203 || this.drawPlayer.body == 58 || this.drawPlayer.body == 59 || this.drawPlayer.body == 60 || this.drawPlayer.body == 61 || this.drawPlayer.body == 62 || this.drawPlayer.body == 63 || this.drawPlayer.body == 36 || this.drawPlayer.body == 104 || this.drawPlayer.body == 184 || this.drawPlayer.body == 74 || this.drawPlayer.body == 78 || this.drawPlayer.body == 185 || this.drawPlayer.body == 196 || this.drawPlayer.body == 197 || this.drawPlayer.body == 182 || this.drawPlayer.body == 87 || this.drawPlayer.body == 76 || this.drawPlayer.body == 209 || this.drawPlayer.body == 168 || this.drawPlayer.body == 210 || this.drawPlayer.body == 211 || this.drawPlayer.body == 213)
			{
				this.missingHand = true;
			}
			int num = this.drawPlayer.body;
			if (num == 83)
			{
				this.missingArm = false;
			}
			else
			{
				this.missingArm = true;
			}
			this.drawPlayer.GetHairSettings(out this.fullHair, out this.hatHair, out this.hideHair, out this.backHairDraw, out this.drawsBackHairWithoutHeadgear);
			this.hairDyePacked = PlayerDrawHelper.PackShader((int)this.drawPlayer.hairDye, PlayerDrawHelper.ShaderConfiguration.HairShader);
			if (this.drawPlayer.head == 0 && this.drawPlayer.hairDye == 0)
			{
				this.hairDyePacked = PlayerDrawHelper.PackShader(1, PlayerDrawHelper.ShaderConfiguration.HairShader);
			}
			this.skinDyePacked = player.skinDyePacked;
			if (this.drawPlayer.mount.Active)
			{
				if (this.drawPlayer.mount.Type == 52)
				{
					this.AdjustmentsForWolfMount();
				}
				if (this.drawPlayer.mount.Type == 54)
				{
					this.AdjustmentsForVelociraptorMount();
				}
				if (this.drawPlayer.mount.Type == 55)
				{
					this.AdjustmentsForRatMount();
				}
				if (this.drawPlayer.mount.Type == 56)
				{
					this.AdjustmentsForBatMount();
				}
				if (this.drawPlayer.mount.Type == 61)
				{
					this.AdjustmentsForPixieMount();
				}
			}
			if (this.drawPlayer.isDisplayDollOrInanimate)
			{
				Point point = this.Center.ToTileCoordinates();
				bool flag;
				if (Main.InSmartCursorHighlightArea(point.X, point.Y, out flag))
				{
					Color color = Lighting.GetColor(point.X, point.Y);
					int num2 = (int)((color.R + color.G + color.B) / 3);
					if (num2 > 10)
					{
						this.selectionGlowColor = Colors.GetSelectionGlowColor(flag, num2);
					}
				}
			}
			this.mountOffSet = this.drawPlayer.HeightOffsetVisual;
			this.Position.Y = this.Position.Y - this.mountOffSet;
			if (this.drawPlayer.mount.Active)
			{
				Mount.currentShader = (this.drawPlayer.mount.Cart ? this.drawPlayer.cMinecart : this.drawPlayer.cMount);
			}
			else
			{
				Mount.currentShader = 0;
			}
			this.playerEffect = SpriteEffects.None;
			this.itemEffect = SpriteEffects.FlipHorizontally;
			this.colorHair = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.GetHairColor(true), this.shadow);
			this.colorEyeWhites = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.25) / 16.0), Color.White), this.shadow);
			this.colorEyes = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.25) / 16.0), this.drawPlayer.eyeColor), this.shadow);
			this.colorHead = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.25) / 16.0), this.drawPlayer.skinColor), this.shadow);
			this.colorBodySkin = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.5) / 16.0), this.drawPlayer.skinColor), this.shadow);
			this.colorLegs = this.drawPlayer.GetImmuneAlpha(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.75) / 16.0), this.drawPlayer.skinColor), this.shadow);
			this.colorShirt = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.5) / 16.0), this.drawPlayer.shirtColor), this.shadow);
			this.colorUnderShirt = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.5) / 16.0), this.drawPlayer.underShirtColor), this.shadow);
			this.colorPants = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.75) / 16.0), this.drawPlayer.pantsColor), this.shadow);
			this.colorShoes = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)(((double)this.Position.Y + (double)this.drawPlayer.height * 0.75) / 16.0), this.drawPlayer.shoeColor), this.shadow);
			this.colorArmorHead = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)((double)this.Position.Y + (double)this.drawPlayer.height * 0.25) / 16, Color.White), this.shadow);
			this.colorArmorBody = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)((double)this.Position.Y + (double)this.drawPlayer.height * 0.5) / 16, Color.White), this.shadow);
			this.colorMount = this.colorArmorBody;
			this.colorArmorLegs = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)((double)this.Position.Y + (double)this.drawPlayer.height * 0.75) / 16, Color.White), this.shadow);
			this.floatingTubeColor = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)((double)this.Position.Y + (double)this.drawPlayer.height * 0.75) / 16, Color.White), this.shadow);
			this.colorElectricity = new Color(255, 255, 255, 100);
			this.colorDisplayDollSkin = this.colorBodySkin;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			this.headGlowMask = -1;
			this.bodyGlowMask = -1;
			this.armGlowMask = -1;
			this.legsGlowMask = -1;
			this.headGlowColor = Color.Transparent;
			this.bodyGlowColor = Color.Transparent;
			this.armGlowColor = Color.Transparent;
			this.legsGlowColor = Color.Transparent;
			num = this.drawPlayer.head;
			switch (num)
			{
			case 169:
				num3++;
				break;
			case 170:
				num4++;
				break;
			case 171:
				num5++;
				break;
			default:
				if (num == 189)
				{
					num6++;
				}
				break;
			}
			num = this.drawPlayer.body;
			switch (num)
			{
			case 175:
				num3++;
				break;
			case 176:
				num4++;
				break;
			case 177:
				num5++;
				break;
			default:
				if (num == 190)
				{
					num6++;
				}
				break;
			}
			num = this.drawPlayer.legs;
			switch (num)
			{
			case 110:
				num3++;
				break;
			case 111:
				num4++;
				break;
			case 112:
				num5++;
				break;
			default:
				if (num == 130)
				{
					num6++;
				}
				break;
			}
			num3 = 3;
			num4 = 3;
			num5 = 3;
			num6 = 3;
			this.ArkhalisColor = this.drawPlayer.underShirtColor;
			this.ArkhalisColor.A = 180;
			if (this.drawPlayer.head == 169)
			{
				this.headGlowMask = 15;
				byte b = (byte)(62.5f * (float)(1 + num3));
				this.headGlowColor = new Color((int)b, (int)b, (int)b, 0);
			}
			else if (this.drawPlayer.head == 216)
			{
				this.headGlowMask = 256;
				byte b2 = 127;
				this.headGlowColor = new Color((int)b2, (int)b2, (int)b2, 0);
			}
			else if (this.drawPlayer.head == 210)
			{
				this.headGlowMask = 242;
				byte b3 = 127;
				this.headGlowColor = new Color((int)b3, (int)b3, (int)b3, 0);
			}
			else if (this.drawPlayer.head == 214)
			{
				this.headGlowMask = 245;
				this.headGlowColor = this.ArkhalisColor;
			}
			else if (this.drawPlayer.head == 240)
			{
				this.headGlowMask = 273;
				this.headGlowColor = new Color(230, 230, 230, 60);
			}
			else if (this.drawPlayer.head == 267)
			{
				this.headGlowMask = 301;
				this.headGlowColor = new Color(230, 230, 230, 60);
			}
			else if (this.drawPlayer.head == 268)
			{
				this.headGlowMask = 302;
				float num7 = (float)Main.mouseTextColor / 255f;
				num7 *= num7;
				this.headGlowColor = new Color(255, 255, 255) * num7;
			}
			else if (this.drawPlayer.head == 269)
			{
				this.headGlowMask = 304;
				this.headGlowColor = new Color(200, 200, 200);
			}
			else if (this.drawPlayer.head == 270)
			{
				this.headGlowMask = 305;
				this.headGlowColor = new Color(200, 200, 200, 150);
			}
			else if (this.drawPlayer.head == 271)
			{
				this.headGlowMask = 309;
				this.headGlowColor = Color.White;
			}
			else if (this.drawPlayer.head == 170)
			{
				this.headGlowMask = 16;
				byte b4 = (byte)(62.5f * (float)(1 + num4));
				this.headGlowColor = new Color((int)b4, (int)b4, (int)b4, 0);
			}
			else if (this.drawPlayer.head == 189)
			{
				this.headGlowMask = 184;
				byte b5 = (byte)(62.5f * (float)(1 + num6));
				this.headGlowColor = new Color((int)b5, (int)b5, (int)b5, 0);
				this.colorArmorHead = this.drawPlayer.GetImmuneAlphaPure(new Color((int)b5, (int)b5, (int)b5, 255), this.shadow);
			}
			else if (this.drawPlayer.head == 171)
			{
				byte b6 = (byte)(62.5f * (float)(1 + num5));
				this.colorArmorHead = this.drawPlayer.GetImmuneAlphaPure(new Color((int)b6, (int)b6, (int)b6, 255), this.shadow);
			}
			else if (this.drawPlayer.head == 175)
			{
				this.headGlowMask = 41;
				this.headGlowColor = new Color(255, 255, 255, 0);
			}
			else if (this.drawPlayer.head == 193)
			{
				this.headGlowMask = 209;
				this.headGlowColor = new Color(255, 255, 255, 127);
			}
			else if (this.drawPlayer.head == 109)
			{
				this.headGlowMask = 208;
				this.headGlowColor = new Color(255, 255, 255, 0);
			}
			else if (this.drawPlayer.head == 178)
			{
				this.headGlowMask = 96;
				this.headGlowColor = new Color(255, 255, 255, 0);
			}
			else if (this.drawPlayer.head == 282)
			{
				this.headGlowMask = 357;
				float num8 = (float)Main.mouseTextColor / 255f;
				num8 *= num8;
				this.headGlowColor = new Color(255, 255, 255, 0) * num8;
			}
			else if (this.drawPlayer.head == 284)
			{
				this.headGlowMask = 365;
				this.headGlowColor = PlayerDrawLayers.GetChickenBonesGlowColor(ref this, false, false);
			}
			else if (this.drawPlayer.head == 285)
			{
				this.headGlowMask = 367;
				this.headGlowColor = new Color(255, 255, 255, 0);
			}
			else if (this.drawPlayer.head == 291)
			{
				this.headGlowMask = 375;
				this.headGlowColor = new Color(255, 255, 255, 255);
			}
			else if (this.drawPlayer.head == 292)
			{
				this.headGlowMask = 378;
				this.headGlowColor = PlayerDrawLayers.GetLunaGlowColor(ref this, false);
			}
			if (this.drawPlayer.body == 175)
			{
				if (this.drawPlayer.Male)
				{
					this.bodyGlowMask = 13;
				}
				else
				{
					this.bodyGlowMask = 18;
				}
				byte b7 = (byte)(62.5f * (float)(1 + num3));
				this.bodyGlowColor = new Color((int)b7, (int)b7, (int)b7, 0);
			}
			else if (this.drawPlayer.body == 208)
			{
				if (this.drawPlayer.Male)
				{
					this.bodyGlowMask = 246;
				}
				else
				{
					this.bodyGlowMask = 247;
				}
				this.armGlowMask = 248;
				this.bodyGlowColor = this.ArkhalisColor;
				this.armGlowColor = this.ArkhalisColor;
			}
			else if (this.drawPlayer.body == 227)
			{
				this.bodyGlowColor = new Color(230, 230, 230, 60);
				this.armGlowColor = new Color(230, 230, 230, 60);
			}
			else if (this.drawPlayer.body == 237)
			{
				float num9 = (float)Main.mouseTextColor / 255f;
				num9 *= num9;
				this.bodyGlowColor = new Color(255, 255, 255) * num9;
			}
			else if (this.drawPlayer.body == 238 || this.drawPlayer.body == 260)
			{
				this.bodyGlowColor = new Color(255, 255, 255);
				this.armGlowColor = new Color(255, 255, 255);
			}
			else if (this.drawPlayer.body == 239)
			{
				this.bodyGlowColor = new Color(200, 200, 200, 150);
				this.armGlowColor = new Color(200, 200, 200, 150);
			}
			else if (this.drawPlayer.body == 190)
			{
				if (this.drawPlayer.Male)
				{
					this.bodyGlowMask = 185;
				}
				else
				{
					this.bodyGlowMask = 186;
				}
				this.armGlowMask = 188;
				byte b8 = (byte)(62.5f * (float)(1 + num6));
				this.bodyGlowColor = new Color((int)b8, (int)b8, (int)b8, 0);
				this.armGlowColor = new Color((int)b8, (int)b8, (int)b8, 0);
				this.colorArmorBody = this.drawPlayer.GetImmuneAlphaPure(new Color((int)b8, (int)b8, (int)b8, 255), this.shadow);
			}
			else if (this.drawPlayer.body == 176)
			{
				if (this.drawPlayer.Male)
				{
					this.bodyGlowMask = 14;
				}
				else
				{
					this.bodyGlowMask = 19;
				}
				this.armGlowMask = 12;
				byte b9 = (byte)(62.5f * (float)(1 + num4));
				this.bodyGlowColor = new Color((int)b9, (int)b9, (int)b9, 0);
				this.armGlowColor = new Color((int)b9, (int)b9, (int)b9, 0);
			}
			else if (this.drawPlayer.body == 194)
			{
				this.bodyGlowMask = 210;
				this.armGlowMask = 211;
				this.bodyGlowColor = new Color(255, 255, 255, 127);
				this.armGlowColor = new Color(255, 255, 255, 127);
			}
			else if (this.drawPlayer.body == 177)
			{
				byte b10 = (byte)(62.5f * (float)(1 + num5));
				this.colorArmorBody = this.drawPlayer.GetImmuneAlphaPure(new Color((int)b10, (int)b10, (int)b10, 255), this.shadow);
			}
			else if (this.drawPlayer.body == 179)
			{
				if (this.drawPlayer.Male)
				{
					this.bodyGlowMask = 42;
				}
				else
				{
					this.bodyGlowMask = 43;
				}
				this.armGlowMask = 44;
				this.bodyGlowColor = new Color(255, 255, 255, 0);
				this.armGlowColor = new Color(255, 255, 255, 0);
			}
			if (this.drawPlayer.legs == 111)
			{
				this.legsGlowMask = 17;
				byte b11 = (byte)(62.5f * (float)(1 + num4));
				this.legsGlowColor = new Color((int)b11, (int)b11, (int)b11, 0);
			}
			else if (this.drawPlayer.legs == 157)
			{
				this.legsGlowMask = 249;
				this.legsGlowColor = this.ArkhalisColor;
			}
			else if (this.drawPlayer.legs == 158)
			{
				this.legsGlowMask = 250;
				this.legsGlowColor = this.ArkhalisColor;
			}
			else if (this.drawPlayer.legs == 210)
			{
				this.legsGlowMask = 274;
				this.legsGlowColor = new Color(230, 230, 230, 60);
			}
			else if (this.drawPlayer.legs == 222)
			{
				this.legsGlowMask = 303;
				float num10 = (float)Main.mouseTextColor / 255f;
				num10 *= num10;
				this.legsGlowColor = new Color(255, 255, 255) * num10;
			}
			else if (this.drawPlayer.legs == 225)
			{
				this.legsGlowMask = 306;
				this.legsGlowColor = new Color(200, 200, 200, 150);
			}
			else if (this.drawPlayer.legs == 226)
			{
				this.legsGlowMask = 307;
				this.legsGlowColor = new Color(200, 200, 200, 150);
			}
			else if (this.drawPlayer.legs == 110)
			{
				this.legsGlowMask = 199;
				byte b12 = (byte)(62.5f * (float)(1 + num3));
				this.legsGlowColor = new Color((int)b12, (int)b12, (int)b12, 0);
			}
			else if (this.drawPlayer.legs == 112)
			{
				byte b13 = (byte)(62.5f * (float)(1 + num5));
				this.colorArmorLegs = this.drawPlayer.GetImmuneAlphaPure(new Color((int)b13, (int)b13, (int)b13, 255), this.shadow);
			}
			else if (this.drawPlayer.legs == 134)
			{
				this.legsGlowMask = 212;
				this.legsGlowColor = new Color(255, 255, 255, 127);
			}
			else if (this.drawPlayer.legs == 130)
			{
				byte b14 = (byte)(127 * (1 + num6));
				this.legsGlowMask = 187;
				this.legsGlowColor = new Color((int)b14, (int)b14, (int)b14, 0);
				this.colorArmorLegs = this.drawPlayer.GetImmuneAlphaPure(new Color((int)b14, (int)b14, (int)b14, 255), this.shadow);
			}
			float num11 = this.shadow;
			this.headGlowColor = this.drawPlayer.GetImmuneAlphaPure(this.headGlowColor, num11);
			this.bodyGlowColor = this.drawPlayer.GetImmuneAlphaPure(this.bodyGlowColor, num11);
			this.armGlowColor = this.drawPlayer.GetImmuneAlphaPure(this.armGlowColor, num11);
			this.legsGlowColor = this.drawPlayer.GetImmuneAlphaPure(this.legsGlowColor, num11);
			if (this.drawPlayer.head > 0 && this.drawPlayer.head < ArmorIDs.Head.Count)
			{
				Main.instance.LoadArmorHead(this.drawPlayer.head);
				int num12 = ArmorIDs.Head.Sets.FrontToBackID[this.drawPlayer.head];
				if (num12 >= 0)
				{
					Main.instance.LoadArmorHead(num12);
				}
			}
			if (this.drawPlayer.body > 0 && this.drawPlayer.body < ArmorIDs.Body.Count)
			{
				Main.instance.LoadArmorBody(this.drawPlayer.body);
			}
			if (this.drawPlayer.legs > 0 && this.drawPlayer.legs < ArmorIDs.Legs.Count)
			{
				Main.instance.LoadArmorLegs(this.drawPlayer.legs);
			}
			if (this.drawPlayer.handon > 0 && (int)this.drawPlayer.handon < ArmorIDs.HandOn.Count)
			{
				Main.instance.LoadAccHandsOn((int)this.drawPlayer.handon);
			}
			if (this.drawPlayer.handoff > 0 && (int)this.drawPlayer.handoff < ArmorIDs.HandOff.Count)
			{
				Main.instance.LoadAccHandsOff((int)this.drawPlayer.handoff);
			}
			if (this.drawPlayer.back > 0 && (int)this.drawPlayer.back < ArmorIDs.Back.Count)
			{
				Main.instance.LoadAccBack((int)this.drawPlayer.back);
			}
			if (this.drawPlayer.front > 0 && (int)this.drawPlayer.front < ArmorIDs.Front.Count)
			{
				Main.instance.LoadAccFront((int)this.drawPlayer.front);
			}
			if (this.drawPlayer.shoe > 0 && (int)this.drawPlayer.shoe < ArmorIDs.Shoe.Count)
			{
				Main.instance.LoadAccShoes((int)this.drawPlayer.shoe);
			}
			if (this.drawPlayer.waist > 0 && (int)this.drawPlayer.waist < ArmorIDs.Waist.Count)
			{
				Main.instance.LoadAccWaist((int)this.drawPlayer.waist);
			}
			if (this.drawPlayer.shield > 0 && (int)this.drawPlayer.shield < ArmorIDs.Shield.Count)
			{
				Main.instance.LoadAccShield((int)this.drawPlayer.shield);
			}
			if (this.drawPlayer.neck > 0 && (int)this.drawPlayer.neck < ArmorIDs.Neck.Count)
			{
				Main.instance.LoadAccNeck((int)this.drawPlayer.neck);
			}
			if (this.drawPlayer.face > 0 && this.drawPlayer.face < ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace((int)this.drawPlayer.face);
			}
			if (this.drawPlayer.balloon > 0 && (int)this.drawPlayer.balloon < ArmorIDs.Balloon.Count)
			{
				Main.instance.LoadAccBalloon((int)this.drawPlayer.balloon);
			}
			if (this.drawPlayer.backpack > 0 && (int)this.drawPlayer.backpack < ArmorIDs.Back.Count)
			{
				Main.instance.LoadAccBack((int)this.drawPlayer.backpack);
			}
			if (this.drawPlayer.tail > 0 && (int)this.drawPlayer.tail < ArmorIDs.Back.Count)
			{
				Main.instance.LoadAccBack((int)this.drawPlayer.tail);
			}
			if (this.drawPlayer.faceHead > 0 && this.drawPlayer.faceHead < ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace((int)this.drawPlayer.faceHead);
			}
			if (this.drawPlayer.faceFlower > 0 && this.drawPlayer.faceFlower < ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace((int)this.drawPlayer.faceFlower);
			}
			if (this.drawPlayer.faceMask > 0 && this.drawPlayer.faceMask < ArmorIDs.Face.Count)
			{
				Main.instance.LoadAccFace((int)this.drawPlayer.faceMask);
			}
			if (this.drawPlayer.balloonFront > 0 && (int)this.drawPlayer.balloonFront < ArmorIDs.Balloon.Count)
			{
				Main.instance.LoadAccBalloon((int)this.drawPlayer.balloonFront);
			}
			if (this.drawPlayer.beard > 0 && this.drawPlayer.beard < ArmorIDs.Beard.Count)
			{
				Main.instance.LoadAccBeard((int)this.drawPlayer.beard);
			}
			if (this.drawPlayer.coat > 0 && this.drawPlayer.coat < ArmorIDs.Body.Count)
			{
				Main.instance.LoadArmorBody(this.drawPlayer.coat);
			}
			Main.instance.LoadHair(this.drawPlayer.hair);
			if (this.drawPlayer.eyebrellaCloud)
			{
				Main.instance.LoadProjectile(238);
			}
			if (this.drawPlayer.isHatRackDoll)
			{
				this.colorLegs = Color.Transparent;
				this.colorBodySkin = Color.Transparent;
				this.colorHead = Color.Transparent;
				this.colorHair = Color.Transparent;
				this.colorEyes = Color.Transparent;
				this.colorEyeWhites = Color.Transparent;
			}
			if (this.drawPlayer.isDisplayDollOrInanimate)
			{
				if (this.drawPlayer.isFullbright)
				{
					this.colorHead = Color.White;
					this.colorBodySkin = Color.White;
					this.colorLegs = Color.White;
					this.colorEyes = Color.White;
					this.colorEyeWhites = Color.White;
					this.colorArmorHead = Color.White;
					this.colorArmorBody = Color.White;
					this.colorArmorLegs = Color.White;
					this.colorDisplayDollSkin = PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR;
				}
				else
				{
					this.colorDisplayDollSkin = this.drawPlayer.GetImmuneAlphaPure(Lighting.GetColorClamped((int)((double)this.Position.X + (double)this.drawPlayer.width * 0.5) / 16, (int)((double)this.Position.Y + (double)this.drawPlayer.height * 0.5) / 16, PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR), this.shadow);
				}
			}
			if (!this.drawPlayer.isDisplayDollOrInanimate)
			{
				if ((this.drawPlayer.head == 80 || this.drawPlayer.head == 79 || this.drawPlayer.head == 78 || this.drawPlayer.head == 283) && this.drawPlayer.body == 51 && this.drawPlayer.legs == 47)
				{
					float num13 = (float)Main.mouseTextColor / 200f - 0.3f;
					if (this.shadow != 0f)
					{
						num13 = 0f;
					}
					this.colorArmorHead.R = (byte)((float)this.colorArmorHead.R * num13);
					this.colorArmorHead.G = (byte)((float)this.colorArmorHead.G * num13);
					this.colorArmorHead.B = (byte)((float)this.colorArmorHead.B * num13);
					this.colorArmorBody.R = (byte)((float)this.colorArmorBody.R * num13);
					this.colorArmorBody.G = (byte)((float)this.colorArmorBody.G * num13);
					this.colorArmorBody.B = (byte)((float)this.colorArmorBody.B * num13);
					this.colorArmorLegs.R = (byte)((float)this.colorArmorLegs.R * num13);
					this.colorArmorLegs.G = (byte)((float)this.colorArmorLegs.G * num13);
					this.colorArmorLegs.B = (byte)((float)this.colorArmorLegs.B * num13);
				}
				if (this.drawPlayer.head == 193 && this.drawPlayer.body == 194 && this.drawPlayer.legs == 134)
				{
					float num14 = 0.6f - this.drawPlayer.ghostFade * 0.3f;
					if (this.shadow != 0f)
					{
						num14 = 0f;
					}
					this.colorArmorHead.R = (byte)((float)this.colorArmorHead.R * num14);
					this.colorArmorHead.G = (byte)((float)this.colorArmorHead.G * num14);
					this.colorArmorHead.B = (byte)((float)this.colorArmorHead.B * num14);
					this.colorArmorBody.R = (byte)((float)this.colorArmorBody.R * num14);
					this.colorArmorBody.G = (byte)((float)this.colorArmorBody.G * num14);
					this.colorArmorBody.B = (byte)((float)this.colorArmorBody.B * num14);
					this.colorArmorLegs.R = (byte)((float)this.colorArmorLegs.R * num14);
					this.colorArmorLegs.G = (byte)((float)this.colorArmorLegs.G * num14);
					this.colorArmorLegs.B = (byte)((float)this.colorArmorLegs.B * num14);
				}
				if (this.shadow > 0f)
				{
					this.colorLegs = Color.Transparent;
					this.colorBodySkin = Color.Transparent;
					this.colorHead = Color.Transparent;
					this.colorHair = Color.Transparent;
					this.colorEyes = Color.Transparent;
					this.colorEyeWhites = Color.Transparent;
				}
			}
			float num15 = 1f;
			float num16 = 1f;
			float num17 = 1f;
			float num18 = 1f;
			if (this.drawPlayer.honey && Main.rand.Next(30) == 0 && this.shadow == 0f)
			{
				Dust dust2 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, 152, 0f, 0f, 150, default(Color), 1f);
				dust2.velocity.Y = 0.3f;
				Dust dust3 = dust2;
				dust3.velocity.X = dust3.velocity.X * 0.1f;
				dust2.scale += (float)Main.rand.Next(3, 4) * 0.1f;
				dust2.alpha = 100;
				dust2.noGravity = true;
				dust2.velocity += this.drawPlayer.velocity * 0.1f;
				this.DustCache.Add(dust2.dustIndex);
			}
			if (this.drawPlayer.dryadWard && this.drawPlayer.velocity.X != 0f && Main.rand.Next(4) == 0)
			{
				Dust dust4 = Dust.NewDustDirect(new Vector2(this.drawPlayer.position.X - 2f, this.drawPlayer.position.Y + (float)this.drawPlayer.height - 2f), this.drawPlayer.width + 4, 4, 163, 0f, 0f, 100, default(Color), 1.5f);
				dust4.noGravity = true;
				dust4.noLight = true;
				dust4.velocity *= 0f;
				this.DustCache.Add(dust4.dustIndex);
			}
			if (this.drawPlayer.poisoned)
			{
				if (Main.rand.Next(50) == 0 && this.shadow == 0f)
				{
					Dust dust5 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, 46, 0f, 0f, 150, default(Color), 0.2f);
					dust5.noGravity = true;
					dust5.fadeIn = 1.9f;
					this.DustCache.Add(dust5.dustIndex);
				}
				num15 *= 0.65f;
				num17 *= 0.75f;
			}
			if (this.drawPlayer.venom)
			{
				if (Main.rand.Next(10) == 0 && this.shadow == 0f)
				{
					Dust dust6 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, 171, 0f, 0f, 100, default(Color), 0.5f);
					dust6.noGravity = true;
					dust6.fadeIn = 1.5f;
					this.DustCache.Add(dust6.dustIndex);
				}
				num16 *= 0.45f;
				num15 *= 0.75f;
			}
			if (this.drawPlayer.onFire)
			{
				if (Main.vampireSeed)
				{
					if (this.shadow == 0f)
					{
						for (int i = 0; i < 5; i++)
						{
							Dust dust7 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 10, this.drawPlayer.height + 10, 6, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
							dust7.noGravity = true;
							dust7.velocity *= 2.3f;
							Dust dust8 = dust7;
							dust8.velocity.Y = dust8.velocity.Y - 0.8f;
							if (i == 0)
							{
								Dust dust9 = dust7;
								dust9.velocity.X = dust9.velocity.X * 0.5f;
								Dust dust10 = dust7;
								dust10.velocity.Y = dust10.velocity.Y - 1.5f;
								dust7.noGravity = false;
								dust7.scale *= 0.4f;
							}
							this.DustCache.Add(dust7.dustIndex);
						}
					}
					num17 *= 0.6f;
					num16 *= 0.7f;
				}
				else
				{
					if (Main.rand.Next(4) == 0 && this.shadow == 0f)
					{
						Dust dust11 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 6, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
						dust11.noGravity = true;
						dust11.velocity *= 1.8f;
						Dust dust12 = dust11;
						dust12.velocity.Y = dust12.velocity.Y - 0.5f;
						this.DustCache.Add(dust11.dustIndex);
					}
					num17 *= 0.6f;
					num16 *= 0.7f;
				}
			}
			if (this.drawPlayer.onFire3)
			{
				if (Main.rand.Next(4) == 0 && this.shadow == 0f)
				{
					Dust dust13 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 6, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
					dust13.noGravity = true;
					dust13.velocity *= 1.8f;
					Dust dust14 = dust13;
					dust14.velocity.Y = dust14.velocity.Y - 0.5f;
					this.DustCache.Add(dust13.dustIndex);
				}
				num17 *= 0.6f;
				num16 *= 0.7f;
			}
			if (this.drawPlayer.dripping && this.shadow == 0f && Main.rand.Next(4) != 0)
			{
				Vector2 position = this.Position;
				position.X -= 2f;
				position.Y -= 2f;
				if (Main.rand.Next(2) == 0)
				{
					Dust dust15 = Dust.NewDustDirect(position, this.drawPlayer.width + 4, this.drawPlayer.height + 2, 211, 0f, 0f, 50, default(Color), 0.8f);
					if (Main.rand.Next(2) == 0)
					{
						dust15.alpha += 25;
					}
					if (Main.rand.Next(2) == 0)
					{
						dust15.alpha += 25;
					}
					dust15.noLight = true;
					dust15.velocity *= 0.2f;
					Dust dust16 = dust15;
					dust16.velocity.Y = dust16.velocity.Y + 0.2f;
					dust15.velocity += this.drawPlayer.velocity;
					this.DustCache.Add(dust15.dustIndex);
				}
				else
				{
					Dust dust17 = Dust.NewDustDirect(position, this.drawPlayer.width + 8, this.drawPlayer.height + 8, 211, 0f, 0f, 50, default(Color), 1.1f);
					if (Main.rand.Next(2) == 0)
					{
						dust17.alpha += 25;
					}
					if (Main.rand.Next(2) == 0)
					{
						dust17.alpha += 25;
					}
					dust17.noLight = true;
					dust17.noGravity = true;
					dust17.velocity *= 0.2f;
					Dust dust18 = dust17;
					dust18.velocity.Y = dust18.velocity.Y + 1f;
					dust17.velocity += this.drawPlayer.velocity;
					this.DustCache.Add(dust17.dustIndex);
				}
			}
			if (this.drawPlayer.drippingSlime)
			{
				int num19 = 175;
				Color color2 = new Color(0, 80, 255, 100);
				if (Main.rand.Next(4) != 0 && this.shadow == 0f)
				{
					Vector2 position2 = this.Position;
					position2.X -= 2f;
					position2.Y -= 2f;
					if (Main.rand.Next(2) == 0)
					{
						Dust dust19 = Dust.NewDustDirect(position2, this.drawPlayer.width + 4, this.drawPlayer.height + 2, 4, 0f, 0f, num19, color2, 1.4f);
						if (Main.rand.Next(2) == 0)
						{
							dust19.alpha += 25;
						}
						if (Main.rand.Next(2) == 0)
						{
							dust19.alpha += 25;
						}
						dust19.noLight = true;
						dust19.velocity *= 0.2f;
						Dust dust20 = dust19;
						dust20.velocity.Y = dust20.velocity.Y + 0.2f;
						dust19.velocity += this.drawPlayer.velocity;
						this.DustCache.Add(dust19.dustIndex);
					}
				}
				num15 *= 0.8f;
				num16 *= 0.8f;
			}
			if (this.drawPlayer.drippingSparkleSlime)
			{
				int num20 = 100;
				if (Main.rand.Next(4) != 0 && this.shadow == 0f)
				{
					Vector2 position3 = this.Position;
					position3.X -= 2f;
					position3.Y -= 2f;
					if (Main.rand.Next(4) == 0)
					{
						Color color3 = Main.hslToRgb(0.7f + 0.2f * Main.rand.NextFloat(), 1f, 0.5f, byte.MaxValue);
						color3.A /= 2;
						Dust dust21 = Dust.NewDustDirect(position3, this.drawPlayer.width + 4, this.drawPlayer.height + 2, 4, 0f, 0f, num20, color3, 0.65f);
						if (Main.rand.Next(2) == 0)
						{
							dust21.alpha += 25;
						}
						if (Main.rand.Next(2) == 0)
						{
							dust21.alpha += 25;
						}
						dust21.noLight = true;
						dust21.velocity *= 0.2f;
						dust21.velocity += this.drawPlayer.velocity * 0.7f;
						dust21.fadeIn = 0.8f;
						this.DustCache.Add(dust21.dustIndex);
					}
					if (Main.rand.Next(30) == 0)
					{
						Color color4;
						Main.hslToRgb(0.7f + 0.2f * Main.rand.NextFloat(), 1f, 0.5f, byte.MaxValue).A = color4.A / 2;
						Dust dust22 = Dust.NewDustDirect(position3, this.drawPlayer.width + 4, this.drawPlayer.height + 2, 43, 0f, 0f, 254, new Color(127, 127, 127, 0), 0.45f);
						dust22.noLight = true;
						Dust dust23 = dust22;
						dust23.velocity.X = dust23.velocity.X * 0f;
						dust22.velocity *= 0.03f;
						dust22.fadeIn = 0.6f;
						this.DustCache.Add(dust22.dustIndex);
					}
				}
				num15 *= 0.94f;
				num16 *= 0.82f;
			}
			if (this.drawPlayer.ichor)
			{
				num17 = 0f;
			}
			if (this.drawPlayer.electrified && this.shadow == 0f && Main.rand.Next(3) == 0)
			{
				Dust dust24 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 226, 0f, 0f, 100, default(Color), 0.5f);
				dust24.velocity *= 1.6f;
				Dust dust25 = dust24;
				dust25.velocity.Y = dust25.velocity.Y - 1f;
				dust24.position = Vector2.Lerp(dust24.position, this.drawPlayer.Center, 0.5f);
				this.DustCache.Add(dust24.dustIndex);
			}
			if (this.drawPlayer.burned)
			{
				if (this.shadow == 0f)
				{
					Dust dust26 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 6, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, default(Color), 2f);
					dust26.noGravity = true;
					dust26.velocity *= 1.8f;
					Dust dust27 = dust26;
					dust27.velocity.Y = dust27.velocity.Y - 0.75f;
					this.DustCache.Add(dust26.dustIndex);
				}
				num15 = 1f;
				num17 *= 0.6f;
				num16 *= 0.7f;
			}
			if (this.drawPlayer.onFrostBurn)
			{
				if (Main.rand.Next(4) == 0 && this.shadow == 0f)
				{
					Dust dust28 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 135, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
					dust28.noGravity = true;
					dust28.velocity *= 1.8f;
					Dust dust29 = dust28;
					dust29.velocity.Y = dust29.velocity.Y - 0.5f;
					this.DustCache.Add(dust28.dustIndex);
				}
				num15 *= 0.5f;
				num16 *= 0.7f;
			}
			if (this.drawPlayer.onFrostBurn2)
			{
				if (Main.rand.Next(4) == 0 && this.shadow == 0f)
				{
					Dust dust30 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 135, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
					dust30.noGravity = true;
					dust30.velocity *= 1.8f;
					Dust dust31 = dust30;
					dust31.velocity.Y = dust31.velocity.Y - 0.5f;
					this.DustCache.Add(dust30.dustIndex);
				}
				num15 *= 0.5f;
				num16 *= 0.7f;
			}
			if (this.drawPlayer.onFire2)
			{
				if (Main.rand.Next(4) == 0 && this.shadow == 0f)
				{
					Dust dust32 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 75, this.drawPlayer.velocity.X * 0.4f, this.drawPlayer.velocity.Y * 0.4f, 100, default(Color), 3f);
					dust32.noGravity = true;
					dust32.velocity *= 1.8f;
					Dust dust33 = dust32;
					dust33.velocity.Y = dust33.velocity.Y - 0.5f;
					this.DustCache.Add(dust32.dustIndex);
				}
				num17 *= 0.6f;
				num16 *= 0.7f;
			}
			if (this.drawPlayer.noItems)
			{
				num16 *= 0.8f;
				num15 *= 0.65f;
			}
			if (this.drawPlayer.blind)
			{
				num16 *= 0.65f;
				num15 *= 0.7f;
			}
			if (this.drawPlayer.bleed)
			{
				num16 *= 0.9f;
				num17 *= 0.9f;
				if (!this.drawPlayer.dead && Main.rand.Next(20) == 0 && this.shadow == 0f)
				{
					Dust dust34 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, 5, 0f, 0f, 0, default(Color), 1f);
					Dust dust35 = dust34;
					dust35.velocity.Y = dust35.velocity.Y + 0.5f;
					dust34.velocity *= 0.25f;
					this.DustCache.Add(dust34.dustIndex);
				}
			}
			if (this.shadow == 0f && this.drawPlayer.palladiumRegen && this.drawPlayer.statLife < this.drawPlayer.statLifeMax2 && FocusHelper.AllowPlayerToEmitEffects && this.drawPlayer.miscCounter % 10 == 0 && this.shadow == 0f)
			{
				Vector2 vector2;
				vector2.X = this.Position.X + (float)Main.rand.Next(this.drawPlayer.width);
				vector2.Y = this.Position.Y + (float)Main.rand.Next(this.drawPlayer.height);
				vector2.X = this.Position.X + (float)(this.drawPlayer.width / 2) - 6f;
				vector2.Y = this.Position.Y + (float)(this.drawPlayer.height / 2) - 6f;
				vector2.X -= (float)Main.rand.Next(-10, 11);
				vector2.Y -= (float)Main.rand.Next(-20, 21);
				int num21 = Gore.NewGore(vector2, new Vector2((float)Main.rand.Next(-10, 11) * 0.1f, (float)Main.rand.Next(-20, -10) * 0.1f), 331, (float)Main.rand.Next(80, 120) * 0.01f);
				this.GoreCache.Add(num21);
			}
			if (this.shadow == 0f && this.drawPlayer.loveStruck && FocusHelper.AllowPlayerToEmitEffects && Main.rand.Next(5) == 0)
			{
				Vector2 vector3 = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
				vector3.Normalize();
				vector3.X *= 0.66f;
				int num22 = Gore.NewGore(this.Position + new Vector2((float)Main.rand.Next(this.drawPlayer.width + 1), (float)Main.rand.Next(this.drawPlayer.height + 1)), vector3 * (float)Main.rand.Next(3, 6) * 0.33f, 331, (float)Main.rand.Next(40, 121) * 0.01f);
				Main.gore[num22].sticky = false;
				Main.gore[num22].velocity *= 0.4f;
				Gore gore2 = Main.gore[num22];
				gore2.velocity.Y = gore2.velocity.Y - 0.6f;
				this.GoreCache.Add(num22);
			}
			if (this.drawPlayer.stinky && FocusHelper.AllowPlayerToEmitEffects)
			{
				num15 *= 0.7f;
				num17 *= 0.55f;
				if (Main.rand.Next(5) == 0 && this.shadow == 0f)
				{
					Vector2 vector4 = new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
					vector4.Normalize();
					vector4.X *= 0.66f;
					vector4.Y = Math.Abs(vector4.Y);
					Vector2 vector5 = vector4 * (float)Main.rand.Next(3, 5) * 0.25f;
					int num23 = Dust.NewDust(this.Position, this.drawPlayer.width, this.drawPlayer.height, 188, vector5.X, vector5.Y * 0.5f, 100, default(Color), 1.5f);
					Main.dust[num23].velocity *= 0.1f;
					Dust dust36 = Main.dust[num23];
					dust36.velocity.Y = dust36.velocity.Y - 0.5f;
					this.DustCache.Add(num23);
				}
			}
			if (this.drawPlayer.slowOgreSpit && FocusHelper.AllowPlayerToEmitEffects)
			{
				num15 *= 0.6f;
				num17 *= 0.45f;
				if (Main.rand.Next(5) == 0 && this.shadow == 0f)
				{
					int num24 = Utils.SelectRandom<int>(Main.rand, new int[] { 4, 256 });
					Dust dust37 = Main.dust[Dust.NewDust(this.Position, this.drawPlayer.width, this.drawPlayer.height, num24, 0f, 0f, 100, default(Color), 1f)];
					dust37.scale = 0.8f + Main.rand.NextFloat() * 0.6f;
					dust37.fadeIn = 0.5f;
					dust37.velocity *= 0.05f;
					dust37.noLight = true;
					if (dust37.type == 4)
					{
						dust37.color = new Color(80, 170, 40, 120);
					}
					this.DustCache.Add(dust37.dustIndex);
				}
				if (Main.rand.Next(5) == 0 && this.shadow == 0f)
				{
					int num25 = Gore.NewGore(this.Position + new Vector2(Main.rand.NextFloat(), Main.rand.NextFloat()) * this.drawPlayer.Size, Vector2.Zero, Utils.SelectRandom<int>(Main.rand, new int[] { 1024, 1025, 1026 }), 0.65f);
					Main.gore[num25].velocity *= 0.05f;
					this.GoreCache.Add(num25);
				}
			}
			if (FocusHelper.AllowPlayerToEmitEffects && this.shadow == 0f)
			{
				float num26 = (float)this.drawPlayer.miscCounter / 180f;
				float num27 = 0f;
				float num28 = 10f;
				int num29 = 90;
				int num30 = 0;
				int j = 0;
				while (j < 3)
				{
					switch (j)
					{
					case 0:
						if (this.drawPlayer.nebulaLevelLife >= 1)
						{
							num27 = 6.2831855f / (float)this.drawPlayer.nebulaLevelLife;
							num30 = this.drawPlayer.nebulaLevelLife;
							goto IL_4496;
						}
						break;
					case 1:
						if (this.drawPlayer.nebulaLevelMana >= 1)
						{
							num27 = -6.2831855f / (float)this.drawPlayer.nebulaLevelMana;
							num30 = this.drawPlayer.nebulaLevelMana;
							num26 = (float)(-(float)this.drawPlayer.miscCounter) / 180f;
							num28 = 20f;
							num29 = 88;
							goto IL_4496;
						}
						break;
					case 2:
						if (this.drawPlayer.nebulaLevelDamage >= 1)
						{
							num27 = 6.2831855f / (float)this.drawPlayer.nebulaLevelDamage;
							num30 = this.drawPlayer.nebulaLevelDamage;
							num26 = (float)this.drawPlayer.miscCounter / 180f;
							num28 = 30f;
							num29 = 86;
							goto IL_4496;
						}
						break;
					default:
						goto IL_4496;
					}
					IL_456C:
					j++;
					continue;
					IL_4496:
					for (int k = 0; k < num30; k++)
					{
						Dust dust38 = Dust.NewDustDirect(this.Position, this.drawPlayer.width, this.drawPlayer.height, num29, 0f, 0f, 100, default(Color), 1.5f);
						dust38.noGravity = true;
						dust38.velocity = Vector2.Zero;
						dust38.position = this.drawPlayer.Center + Vector2.UnitY * this.drawPlayer.gfxOffY + (num26 * 6.2831855f + num27 * (float)k).ToRotationVector2() * num28;
						dust38.customData = this.drawPlayer;
						this.DustCache.Add(dust38.dustIndex);
					}
					goto IL_456C;
				}
			}
			if (this.drawPlayer.witheredArmor && FocusHelper.AllowPlayerToEmitEffects)
			{
				num16 *= 0.5f;
				num15 *= 0.75f;
			}
			if (this.drawPlayer.witheredWeapon && this.drawPlayer.itemAnimation > 0 && this.heldItem.damage > 0 && FocusHelper.AllowPlayerToEmitEffects && Main.rand.Next(3) == 0)
			{
				Dust dust39 = Dust.NewDustDirect(new Vector2(this.Position.X - 2f, this.Position.Y - 2f), this.drawPlayer.width + 4, this.drawPlayer.height + 4, 272, 0f, 0f, 50, default(Color), 0.5f);
				dust39.velocity *= 1.6f;
				Dust dust40 = dust39;
				dust40.velocity.Y = dust40.velocity.Y - 1f;
				dust39.position = Vector2.Lerp(dust39.position, this.drawPlayer.Center, 0.5f);
				this.DustCache.Add(dust39.dustIndex);
			}
			bool shimmering = this.drawPlayer.shimmering;
			if (num15 != 1f || num16 != 1f || num17 != 1f || num18 != 1f)
			{
				if (this.drawPlayer.onFire || this.drawPlayer.onFire2 || this.drawPlayer.onFrostBurn || this.drawPlayer.onFire3 || this.drawPlayer.onFrostBurn2)
				{
					this.colorEyeWhites = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
					this.colorEyes = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.eyeColor, this.shadow);
					this.colorHair = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.GetHairColor(false), this.shadow);
					this.colorHead = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.skinColor, this.shadow);
					this.colorBodySkin = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.skinColor, this.shadow);
					this.colorShirt = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.shirtColor, this.shadow);
					this.colorUnderShirt = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.underShirtColor, this.shadow);
					this.colorPants = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.pantsColor, this.shadow);
					this.colorLegs = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.skinColor, this.shadow);
					this.colorShoes = this.drawPlayer.GetImmuneAlpha(this.drawPlayer.shoeColor, this.shadow);
					this.colorArmorHead = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
					this.colorArmorBody = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
					this.colorArmorLegs = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
					if (this.drawPlayer.isDisplayDollOrInanimate)
					{
						this.colorDisplayDollSkin = this.drawPlayer.GetImmuneAlpha(PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR, this.shadow);
					}
				}
				else
				{
					this.colorEyeWhites = Main.buffColor(this.colorEyeWhites, num15, num16, num17, num18);
					this.colorEyes = Main.buffColor(this.colorEyes, num15, num16, num17, num18);
					this.colorHair = Main.buffColor(this.colorHair, num15, num16, num17, num18);
					this.colorHead = Main.buffColor(this.colorHead, num15, num16, num17, num18);
					this.colorBodySkin = Main.buffColor(this.colorBodySkin, num15, num16, num17, num18);
					this.colorShirt = Main.buffColor(this.colorShirt, num15, num16, num17, num18);
					this.colorUnderShirt = Main.buffColor(this.colorUnderShirt, num15, num16, num17, num18);
					this.colorPants = Main.buffColor(this.colorPants, num15, num16, num17, num18);
					this.colorLegs = Main.buffColor(this.colorLegs, num15, num16, num17, num18);
					this.colorShoes = Main.buffColor(this.colorShoes, num15, num16, num17, num18);
					this.colorArmorHead = Main.buffColor(this.colorArmorHead, num15, num16, num17, num18);
					this.colorArmorBody = Main.buffColor(this.colorArmorBody, num15, num16, num17, num18);
					this.colorArmorLegs = Main.buffColor(this.colorArmorLegs, num15, num16, num17, num18);
					if (this.drawPlayer.isDisplayDollOrInanimate)
					{
						this.colorDisplayDollSkin = Main.buffColor(PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR, num15, num16, num17, num18);
					}
				}
			}
			if (this.drawPlayer.socialGhost)
			{
				this.colorEyeWhites = Color.Transparent;
				this.colorEyes = Color.Transparent;
				this.colorHair = Color.Transparent;
				this.colorHead = Color.Transparent;
				this.colorBodySkin = Color.Transparent;
				this.colorShirt = Color.Transparent;
				this.colorUnderShirt = Color.Transparent;
				this.colorPants = Color.Transparent;
				this.colorShoes = Color.Transparent;
				this.colorLegs = Color.Transparent;
				if (this.colorArmorHead.A > Main.gFade)
				{
					this.colorArmorHead.A = Main.gFade;
				}
				if (this.colorArmorBody.A > Main.gFade)
				{
					this.colorArmorBody.A = Main.gFade;
				}
				if (this.colorArmorLegs.A > Main.gFade)
				{
					this.colorArmorLegs.A = Main.gFade;
				}
				if (this.drawPlayer.isDisplayDollOrInanimate)
				{
					this.colorDisplayDollSkin = Color.Transparent;
				}
			}
			if (this.drawPlayer.socialIgnoreLight)
			{
				float num31 = 1f;
				this.colorEyeWhites = Color.White * num31;
				this.colorEyes = this.drawPlayer.eyeColor * num31;
				this.colorHair = GameShaders.Hair.GetColor((short)this.drawPlayer.hairDye, this.drawPlayer, Color.White);
				this.colorHead = this.drawPlayer.skinColor * num31;
				this.colorBodySkin = this.drawPlayer.skinColor * num31;
				this.colorShirt = this.drawPlayer.shirtColor * num31;
				this.colorUnderShirt = this.drawPlayer.underShirtColor * num31;
				this.colorPants = this.drawPlayer.pantsColor * num31;
				this.colorShoes = this.drawPlayer.shoeColor * num31;
				this.colorLegs = this.drawPlayer.skinColor * num31;
				this.colorArmorHead = Color.White;
				this.colorArmorBody = Color.White;
				this.colorArmorLegs = Color.White;
				if (this.drawPlayer.isDisplayDollOrInanimate)
				{
					this.colorDisplayDollSkin = PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR * num31;
				}
			}
			if (this.drawPlayer.opacityForAnimation != 1f)
			{
				this.shadow = 1f - this.drawPlayer.opacityForAnimation;
				float num32 = this.drawPlayer.opacityForAnimation;
				num32 *= num32;
				this.colorEyeWhites = Color.White * num32;
				this.colorEyes = this.drawPlayer.eyeColor * num32;
				this.colorHair = GameShaders.Hair.GetColor((short)this.drawPlayer.hairDye, this.drawPlayer, Color.White) * num32;
				this.colorHead = this.drawPlayer.skinColor * num32;
				this.colorBodySkin = this.drawPlayer.skinColor * num32;
				this.colorShirt = this.drawPlayer.shirtColor * num32;
				this.colorUnderShirt = this.drawPlayer.underShirtColor * num32;
				this.colorPants = this.drawPlayer.pantsColor * num32;
				this.colorShoes = this.drawPlayer.shoeColor * num32;
				this.colorLegs = this.drawPlayer.skinColor * num32;
				this.colorArmorHead = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
				this.colorArmorBody = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
				this.colorArmorLegs = this.drawPlayer.GetImmuneAlpha(Color.White, this.shadow);
				if (this.drawPlayer.isDisplayDollOrInanimate)
				{
					this.colorDisplayDollSkin = PlayerDrawHelper.DISPLAY_DOLL_DEFAULT_SKIN_COLOR * num32;
				}
			}
			this.stealth = 1f;
			if (this.heldItem.type == 3106)
			{
				float num33 = this.drawPlayer.stealth;
				if ((double)num33 < 0.03)
				{
					num33 = 0.03f;
				}
				float num34 = (1f + num33 * 10f) / 11f;
				if (num33 < 0f)
				{
					num33 = 0f;
				}
				if (num33 >= 1f - this.shadow && this.shadow > 0f)
				{
					num33 = this.shadow * 0.5f;
				}
				this.stealth = num34;
				this.colorArmorHead = new Color((int)((byte)((float)this.colorArmorHead.R * num33)), (int)((byte)((float)this.colorArmorHead.G * num33)), (int)((byte)((float)this.colorArmorHead.B * num34)), (int)((byte)((float)this.colorArmorHead.A * num33)));
				this.colorArmorBody = new Color((int)((byte)((float)this.colorArmorBody.R * num33)), (int)((byte)((float)this.colorArmorBody.G * num33)), (int)((byte)((float)this.colorArmorBody.B * num34)), (int)((byte)((float)this.colorArmorBody.A * num33)));
				this.colorArmorLegs = new Color((int)((byte)((float)this.colorArmorLegs.R * num33)), (int)((byte)((float)this.colorArmorLegs.G * num33)), (int)((byte)((float)this.colorArmorLegs.B * num34)), (int)((byte)((float)this.colorArmorLegs.A * num33)));
				num33 *= num33;
				this.colorEyeWhites = Color.Multiply(this.colorEyeWhites, num33);
				this.colorEyes = Color.Multiply(this.colorEyes, num33);
				this.colorHair = Color.Multiply(this.colorHair, num33);
				this.colorHead = Color.Multiply(this.colorHead, num33);
				this.colorBodySkin = Color.Multiply(this.colorBodySkin, num33);
				this.colorShirt = Color.Multiply(this.colorShirt, num33);
				this.colorUnderShirt = Color.Multiply(this.colorUnderShirt, num33);
				this.colorPants = Color.Multiply(this.colorPants, num33);
				this.colorShoes = Color.Multiply(this.colorShoes, num33);
				this.colorLegs = Color.Multiply(this.colorLegs, num33);
				this.colorMount = Color.Multiply(this.colorMount, num33);
				this.floatingTubeColor = Color.Multiply(this.floatingTubeColor, num33);
				this.headGlowColor = Color.Multiply(this.headGlowColor, num33);
				this.bodyGlowColor = Color.Multiply(this.bodyGlowColor, num33);
				this.armGlowColor = Color.Multiply(this.armGlowColor, num33);
				this.legsGlowColor = Color.Multiply(this.legsGlowColor, num33);
				if (this.drawPlayer.isDisplayDollOrInanimate)
				{
					this.colorDisplayDollSkin = Color.Multiply(this.colorDisplayDollSkin, num33);
				}
			}
			else if (this.drawPlayer.shroomiteStealth)
			{
				float num35 = this.drawPlayer.stealth;
				if ((double)num35 < 0.03)
				{
					num35 = 0.03f;
				}
				float num36 = (1f + num35 * 10f) / 11f;
				if (num35 < 0f)
				{
					num35 = 0f;
				}
				if (num35 >= 1f - this.shadow && this.shadow > 0f)
				{
					num35 = this.shadow * 0.5f;
				}
				this.stealth = num36;
				this.colorArmorHead = new Color((int)((byte)((float)this.colorArmorHead.R * num35)), (int)((byte)((float)this.colorArmorHead.G * num35)), (int)((byte)((float)this.colorArmorHead.B * num36)), (int)((byte)((float)this.colorArmorHead.A * num35)));
				this.colorArmorBody = new Color((int)((byte)((float)this.colorArmorBody.R * num35)), (int)((byte)((float)this.colorArmorBody.G * num35)), (int)((byte)((float)this.colorArmorBody.B * num36)), (int)((byte)((float)this.colorArmorBody.A * num35)));
				this.colorArmorLegs = new Color((int)((byte)((float)this.colorArmorLegs.R * num35)), (int)((byte)((float)this.colorArmorLegs.G * num35)), (int)((byte)((float)this.colorArmorLegs.B * num36)), (int)((byte)((float)this.colorArmorLegs.A * num35)));
				num35 *= num35;
				this.colorEyeWhites = Color.Multiply(this.colorEyeWhites, num35);
				this.colorEyes = Color.Multiply(this.colorEyes, num35);
				this.colorHair = Color.Multiply(this.colorHair, num35);
				this.colorHead = Color.Multiply(this.colorHead, num35);
				this.colorBodySkin = Color.Multiply(this.colorBodySkin, num35);
				this.colorShirt = Color.Multiply(this.colorShirt, num35);
				this.colorUnderShirt = Color.Multiply(this.colorUnderShirt, num35);
				this.colorPants = Color.Multiply(this.colorPants, num35);
				this.colorShoes = Color.Multiply(this.colorShoes, num35);
				this.colorLegs = Color.Multiply(this.colorLegs, num35);
				this.colorMount = Color.Multiply(this.colorMount, num35);
				this.floatingTubeColor = Color.Multiply(this.floatingTubeColor, num35);
				this.headGlowColor = Color.Multiply(this.headGlowColor, num35);
				this.bodyGlowColor = Color.Multiply(this.bodyGlowColor, num35);
				this.armGlowColor = Color.Multiply(this.armGlowColor, num35);
				this.legsGlowColor = Color.Multiply(this.legsGlowColor, num35);
				if (this.drawPlayer.isDisplayDollOrInanimate)
				{
					this.colorDisplayDollSkin = Color.Multiply(this.colorDisplayDollSkin, num35);
				}
			}
			else if (this.drawPlayer.setVortex)
			{
				float num37 = this.drawPlayer.stealth;
				if ((double)num37 < 0.03)
				{
					num37 = 0.03f;
				}
				if (num37 < 0f)
				{
					num37 = 0f;
				}
				if (num37 >= 1f - this.shadow && this.shadow > 0f)
				{
					num37 = this.shadow * 0.5f;
				}
				this.stealth = num37;
				Color color5 = new Color(Vector4.Lerp(Vector4.One, new Vector4(0f, 0.12f, 0.16f, 0f), 1f - num37));
				this.colorArmorHead = this.colorArmorHead.MultiplyRGBA(color5);
				this.colorArmorBody = this.colorArmorBody.MultiplyRGBA(color5);
				this.colorArmorLegs = this.colorArmorLegs.MultiplyRGBA(color5);
				num37 *= num37;
				this.colorEyeWhites = Color.Multiply(this.colorEyeWhites, num37);
				this.colorEyes = Color.Multiply(this.colorEyes, num37);
				this.colorHair = Color.Multiply(this.colorHair, num37);
				this.colorHead = Color.Multiply(this.colorHead, num37);
				this.colorBodySkin = Color.Multiply(this.colorBodySkin, num37);
				this.colorShirt = Color.Multiply(this.colorShirt, num37);
				this.colorUnderShirt = Color.Multiply(this.colorUnderShirt, num37);
				this.colorPants = Color.Multiply(this.colorPants, num37);
				this.colorShoes = Color.Multiply(this.colorShoes, num37);
				this.colorLegs = Color.Multiply(this.colorLegs, num37);
				this.colorMount = Color.Multiply(this.colorMount, num37);
				this.floatingTubeColor = Color.Multiply(this.floatingTubeColor, num37);
				this.headGlowColor = Color.Multiply(this.headGlowColor, num37);
				this.bodyGlowColor = Color.Multiply(this.bodyGlowColor, num37);
				this.armGlowColor = Color.Multiply(this.armGlowColor, num37);
				this.legsGlowColor = Color.Multiply(this.legsGlowColor, num37);
				if (this.drawPlayer.isDisplayDollOrInanimate)
				{
					this.colorDisplayDollSkin = Color.Multiply(this.colorDisplayDollSkin, num37);
				}
			}
			if (this.hideEntirePlayerExceptHelmetsAndFaceAccessories)
			{
				this.hideHair = true;
				Color transparent = Color.Transparent;
				this.colorArmorBody = transparent;
				this.colorArmorLegs = transparent;
				this.colorEyeWhites = transparent;
				this.colorEyes = transparent;
				this.colorBodySkin = transparent;
				this.colorShirt = transparent;
				this.colorUnderShirt = transparent;
				this.colorPants = transparent;
				this.colorShoes = transparent;
				this.colorLegs = transparent;
				this.bodyGlowColor = transparent;
				this.armGlowColor = transparent;
				this.legsGlowColor = transparent;
				this.colorDisplayDollSkin = transparent;
			}
			if (this.hideEntirePlayer)
			{
				this.stealth = 1f;
				Color transparent2 = Color.Transparent;
				this.colorArmorHead = transparent2;
				this.colorArmorBody = transparent2;
				this.colorArmorLegs = transparent2;
				this.colorEyeWhites = transparent2;
				this.colorEyes = transparent2;
				this.colorHair = transparent2;
				this.colorHead = transparent2;
				this.colorBodySkin = transparent2;
				this.colorShirt = transparent2;
				this.colorUnderShirt = transparent2;
				this.colorPants = transparent2;
				this.colorShoes = transparent2;
				this.colorLegs = transparent2;
				this.headGlowColor = transparent2;
				this.bodyGlowColor = transparent2;
				this.armGlowColor = transparent2;
				this.legsGlowColor = transparent2;
				this.colorDisplayDollSkin = transparent2;
			}
			if (this.drawPlayer.gravDir == 1f)
			{
				if (this.drawPlayer.direction == 1)
				{
					this.playerEffect = SpriteEffects.None;
					this.itemEffect = SpriteEffects.None;
				}
				else
				{
					this.playerEffect = SpriteEffects.FlipHorizontally;
					this.itemEffect = SpriteEffects.FlipHorizontally;
				}
				if (!this.drawPlayer.dead)
				{
					this.drawPlayer.legPosition.Y = 0f;
					this.drawPlayer.headPosition.Y = 0f;
					this.drawPlayer.bodyPosition.Y = 0f;
				}
			}
			else
			{
				if (this.drawPlayer.direction == 1)
				{
					this.playerEffect = SpriteEffects.FlipVertically;
					this.itemEffect = SpriteEffects.FlipVertically;
				}
				else
				{
					this.playerEffect = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
					this.itemEffect = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
				}
				if (!this.drawPlayer.dead)
				{
					this.drawPlayer.legPosition.Y = 6f;
					this.drawPlayer.headPosition.Y = 6f;
					this.drawPlayer.bodyPosition.Y = 6f;
				}
			}
			num = this.heldItem.type;
			if (num <= 3185)
			{
				if (num != 3182 && num - 3184 > 1)
				{
					goto IL_5906;
				}
			}
			else if (num != 3782)
			{
				if (num - 4343 <= 1)
				{
					this.itemEffect ^= SpriteEffects.FlipHorizontally;
					goto IL_5906;
				}
				if (num != 5118)
				{
					goto IL_5906;
				}
				if (player.gravDir < 0f)
				{
					this.itemEffect ^= SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
					goto IL_5906;
				}
				goto IL_5906;
			}
			this.itemEffect ^= SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
			IL_5906:
			this.legVect = new Vector2((float)this.drawPlayer.legFrame.Width * 0.5f, (float)this.drawPlayer.legFrame.Height * 0.75f);
			this.bodyVect = new Vector2((float)this.drawPlayer.legFrame.Width * 0.5f, (float)this.drawPlayer.legFrame.Height * 0.5f);
			this.headVect = new Vector2((float)this.drawPlayer.legFrame.Width * 0.5f, (float)this.drawPlayer.legFrame.Height * 0.4f);
			if ((this.drawPlayer.merman || this.drawPlayer.forceMerman) && !this.drawPlayer.hideMerman)
			{
				this.drawPlayer.headRotation = this.drawPlayer.velocity.Y * (float)this.drawPlayer.direction * 0.1f;
				if ((double)this.drawPlayer.headRotation < -0.3)
				{
					this.drawPlayer.headRotation = -0.3f;
				}
				if ((double)this.drawPlayer.headRotation > 0.3)
				{
					this.drawPlayer.headRotation = 0.3f;
				}
			}
			else if (!this.drawPlayer.dead)
			{
				this.drawPlayer.headRotation = 0f;
			}
			Rectangle rectangle = this.drawPlayer.bodyFrame;
			rectangle = this.drawPlayer.bodyFrame;
			rectangle.Y -= 336;
			if (rectangle.Y < 0)
			{
				rectangle.Y = 0;
			}
			this.hairFrontFrame = rectangle;
			this.hairBackFrame = rectangle;
			if (this.hideHair)
			{
				this.hairFrontFrame.Height = 0;
				this.hairBackFrame.Height = 0;
			}
			else if (this.backHairDraw)
			{
				int num38 = 26;
				this.hairFrontFrame.Height = num38;
			}
			this.hidesTopSkin = this.drawPlayer.body == 82 || this.drawPlayer.body == 83 || this.drawPlayer.body == 93 || this.drawPlayer.body == 21 || this.drawPlayer.body == 22;
			this.hidesBottomSkin = this.drawPlayer.body == 93 || this.drawPlayer.legs == 20 || this.drawPlayer.legs == 21 || this.drawPlayer.legs == 216 || this.drawPlayer.legs == 214 || this.drawPlayer.legs == 215;
			this.drawFloatingTube = this.drawPlayer.hasFloatingTube && !this.hideEntirePlayer && !this.hideEntirePlayerExceptHelmetsAndFaceAccessories;
			this.drawUnicornHorn = this.drawPlayer.hasUnicornHorn;
			this.drawAngelHalo = this.drawPlayer.hasAngelHalo;
			this.drawFrontAccInNeckAccLayer = false;
			if (this.drawPlayer.front > 0 && (int)this.drawPlayer.front < ArmorIDs.Front.Count)
			{
				if (ArmorIDs.Front.Sets.DrawsInNeckLayerRegardlessOfPlayerFrame[(int)this.drawPlayer.front])
				{
					this.drawFrontAccInNeckAccLayer = true;
				}
				else if (this.drawPlayer.bodyFrame.Y / this.drawPlayer.bodyFrame.Height == 5 && ArmorIDs.Front.Sets.DrawsInNeckLayer[(int)this.drawPlayer.front])
				{
					this.drawFrontAccInNeckAccLayer = true;
				}
			}
			this.mountHandlesHeadDraw = false;
			this.mountDrawsEyelid = false;
			if (this.drawPlayer.mount.Active && this.drawPlayer.mount.Type == 54)
			{
				this.mountHandlesHeadDraw = true;
				this.mountDrawsEyelid = true;
			}
			this.hairOffset = this.drawPlayer.GetHairDrawOffset(this.drawPlayer.hair, this.hatHair);
			this.helmetOffset = this.drawPlayer.GetHelmetDrawOffset(false);
			this.legsOffset = this.drawPlayer.GetLegsDrawOffset();
			this.CreateCompositeData();
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x006389DC File Offset: 0x00636BDC
		private void AdjustmentsForWolfMount()
		{
			this.hideEntirePlayer = true;
			this.weaponDrawOrder = WeaponDrawOrder.BehindBackArm;
			Vector2 vector = new Vector2((float)(10 + this.drawPlayer.direction * 14), 12f);
			Vector2 vector2 = this.Position + vector;
			this.Position.X = this.Position.X - (float)(this.drawPlayer.direction * 10);
			bool flag = this.heldItem.useStyle == 5 || this.SelectedDrawnProjectile != null;
			bool flag2 = this.heldItem.useStyle == 2;
			bool flag3 = this.heldItem.useStyle == 9;
			bool flag4 = this.drawPlayer.itemAnimation > 0;
			bool flag5 = this.heldItem.fishingPole != 0;
			bool flag6 = this.heldItem.useStyle == 14;
			bool flag7 = this.heldItem.useStyle == 8;
			bool flag8 = this.heldItem.holdStyle == 1;
			bool flag9 = this.heldItem.holdStyle == 2;
			bool flag10 = this.heldItem.holdStyle == 5;
			if (flag2)
			{
				this.ItemLocation += new Vector2((float)(this.drawPlayer.direction * 14), -4f);
				return;
			}
			if (!flag5)
			{
				if (flag3)
				{
					this.ItemLocation += (flag4 ? new Vector2((float)(this.drawPlayer.direction * 18), -4f) : new Vector2((float)(this.drawPlayer.direction * 14), -18f));
					return;
				}
				if (flag10)
				{
					this.ItemLocation += new Vector2((float)(this.drawPlayer.direction * 17), -8f);
					return;
				}
				if (flag8 && this.drawPlayer.itemAnimation == 0)
				{
					this.ItemLocation += new Vector2((float)(this.drawPlayer.direction * 14), -6f);
					return;
				}
				if (flag9 && this.drawPlayer.itemAnimation == 0)
				{
					this.ItemLocation += new Vector2((float)(this.drawPlayer.direction * 17), 4f);
					return;
				}
				if (flag7)
				{
					this.ItemLocation = vector2 + new Vector2((float)(this.drawPlayer.direction * 12), 2f);
					return;
				}
				if (flag6)
				{
					this.ItemLocation += new Vector2((float)(this.drawPlayer.direction * 5), -2f);
					return;
				}
				if (flag)
				{
					this.ItemLocation += new Vector2((float)(this.drawPlayer.direction * 4), -4f);
					return;
				}
				this.ItemLocation = vector2;
			}
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x00638CA0 File Offset: 0x00636EA0
		private void AdjustmentsForVelociraptorMount()
		{
			this.hideEntirePlayerExceptHelmetsAndFaceAccessories = true;
			this.weaponDrawOrder = WeaponDrawOrder.BehindFrontArm;
			this.Position.X = this.Position.X - (float)(this.drawPlayer.direction * 14);
			bool flag = this.drawPlayer.itemAnimation > 0;
			if (this.heldItem.useStyle == 8 && flag)
			{
				this.weaponDrawOrder = WeaponDrawOrder.OverFrontArm;
			}
			this.drawPlayer.ApplyItemPositionOffsetFromMount(ref this.ItemLocation);
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x00638D12 File Offset: 0x00636F12
		private void AdjustmentsForRatMount()
		{
			this.hideEntirePlayer = true;
			this.weaponDrawOrder = WeaponDrawOrder.BehindBackArm;
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x00638D12 File Offset: 0x00636F12
		private void AdjustmentsForBatMount()
		{
			this.hideEntirePlayer = true;
			this.weaponDrawOrder = WeaponDrawOrder.BehindBackArm;
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x00638D12 File Offset: 0x00636F12
		private void AdjustmentsForPixieMount()
		{
			this.hideEntirePlayer = true;
			this.weaponDrawOrder = WeaponDrawOrder.BehindBackArm;
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x00638D24 File Offset: 0x00636F24
		private void CreateCompositeData()
		{
			this.frontShoulderOffset = Vector2.Zero;
			this.backShoulderOffset = Vector2.Zero;
			this.usesCompositeTorso = this.drawPlayer.body > 0 && this.drawPlayer.body < ArmorIDs.Body.Count && ArmorIDs.Body.Sets.UsesNewFramingCode[this.drawPlayer.body];
			this.usesCompositeFrontHandAcc = this.drawPlayer.handon > 0 && (int)this.drawPlayer.handon < ArmorIDs.HandOn.Count && ArmorIDs.HandOn.Sets.UsesNewFramingCode[(int)this.drawPlayer.handon];
			this.usesCompositeBackHandAcc = this.drawPlayer.handoff > 0 && (int)this.drawPlayer.handoff < ArmorIDs.HandOff.Count && ArmorIDs.HandOff.Sets.UsesNewFramingCode[(int)this.drawPlayer.handoff];
			if (this.drawPlayer.body < 1)
			{
				this.usesCompositeTorso = true;
			}
			if (!this.usesCompositeTorso)
			{
				return;
			}
			Point point = new Point(1, 1);
			Point point2 = new Point(0, 1);
			Point point3 = default(Point);
			Point point4 = default(Point);
			Point point5 = default(Point);
			int num = this.drawPlayer.bodyFrame.Y / this.drawPlayer.bodyFrame.Height;
			this.compShoulderOverFrontArm = true;
			this.hideCompositeShoulders = false;
			bool flag = true;
			if (this.drawPlayer.body > 0)
			{
				flag = ArmorIDs.Body.Sets.showsShouldersWhileJumping[this.drawPlayer.body];
			}
			if (this.drawPlayer.coat > 0)
			{
				this.hideCompositeShoulders = true;
			}
			if (this.drawPlayer.front > 0 && ArmorIDs.Front.Sets.HidesCompositeShoulders[(int)this.drawPlayer.front])
			{
				this.hideCompositeShoulders = true;
			}
			bool flag2 = false;
			if (this.drawPlayer.handon > 0)
			{
				flag2 = ArmorIDs.HandOn.Sets.UsesOldFramingTexturesForWalking[(int)this.drawPlayer.handon];
			}
			bool flag3 = !flag2;
			switch (num)
			{
			case 0:
				point5.X = 2;
				flag3 = true;
				break;
			case 1:
				point5.X = 3;
				this.compShoulderOverFrontArm = false;
				flag3 = true;
				break;
			case 2:
				point5.X = 4;
				this.compShoulderOverFrontArm = false;
				flag3 = true;
				break;
			case 3:
				point5.X = 5;
				this.compShoulderOverFrontArm = true;
				flag3 = true;
				break;
			case 4:
				point5.X = 6;
				this.compShoulderOverFrontArm = true;
				flag3 = true;
				break;
			case 5:
				point5.X = 2;
				point5.Y = 1;
				point3.X = 1;
				this.compShoulderOverFrontArm = false;
				flag3 = true;
				if (!flag)
				{
					this.hideCompositeShoulders = true;
				}
				break;
			case 6:
				point5.X = 3;
				point5.Y = 1;
				break;
			case 7:
			case 8:
			case 9:
			case 10:
				point5.X = 4;
				point5.Y = 1;
				break;
			case 11:
			case 12:
			case 13:
				point5.X = 3;
				point5.Y = 1;
				break;
			case 14:
				point5.X = 5;
				point5.Y = 1;
				break;
			case 15:
			case 16:
				point5.X = 6;
				point5.Y = 1;
				break;
			case 17:
				point5.X = 5;
				point5.Y = 1;
				break;
			case 18:
			case 19:
				point5.X = 3;
				point5.Y = 1;
				break;
			}
			this.CreateCompositeData_DetermineShoulderOffsets(this.drawPlayer.body, num);
			this.backShoulderOffset *= new Vector2((float)this.drawPlayer.direction, this.drawPlayer.gravDir);
			this.frontShoulderOffset *= new Vector2((float)this.drawPlayer.direction, this.drawPlayer.gravDir);
			if (this.drawPlayer.body > 0 && ArmorIDs.Body.Sets.shouldersAreAlwaysInTheBack[this.drawPlayer.body])
			{
				this.compShoulderOverFrontArm = false;
			}
			this.usesCompositeFrontHandAcc = flag3;
			point4.X = point5.X;
			point4.Y = point5.Y + 2;
			this.UpdateCompositeArm(this.drawPlayer.compositeFrontArm, ref this.compositeFrontArmRotation, ref point5, 7);
			this.UpdateCompositeArm(this.drawPlayer.compositeBackArm, ref this.compositeBackArmRotation, ref point4, 8);
			if (!this.drawPlayer.Male)
			{
				point.Y += 2;
				point2.Y += 2;
				point3.Y += 2;
			}
			this.compBackShoulderFrame = this.CreateCompositeFrameRect(point);
			this.compFrontShoulderFrame = this.CreateCompositeFrameRect(point2);
			this.compBackArmFrame = this.CreateCompositeFrameRect(point4);
			this.compFrontArmFrame = this.CreateCompositeFrameRect(point5);
			this.compTorsoFrame = this.CreateCompositeFrameRect(point3);
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x006391D4 File Offset: 0x006373D4
		private void CreateCompositeData_DetermineShoulderOffsets(int armor, int targetFrameNumber)
		{
			int num = 0;
			if (armor <= 101)
			{
				if (armor != 55)
				{
					if (armor != 71)
					{
						if (armor == 101)
						{
							num = 6;
						}
					}
					else
					{
						num = 2;
					}
				}
				else
				{
					num = 1;
				}
			}
			else if (armor <= 201)
			{
				if (armor != 183)
				{
					if (armor == 201)
					{
						num = 5;
					}
				}
				else
				{
					num = 4;
				}
			}
			else if (armor != 204)
			{
				if (armor == 207)
				{
					num = 7;
				}
			}
			else
			{
				num = 3;
			}
			if (num == 0)
			{
				return;
			}
			switch (num)
			{
			case 1:
				switch (targetFrameNumber)
				{
				case 6:
					this.frontShoulderOffset.X = -2f;
					return;
				case 7:
				case 8:
				case 9:
				case 10:
					this.frontShoulderOffset.X = -4f;
					return;
				case 11:
				case 12:
				case 13:
				case 14:
					this.frontShoulderOffset.X = -2f;
					return;
				case 15:
				case 16:
				case 17:
					break;
				case 18:
				case 19:
					this.frontShoulderOffset.X = -2f;
					return;
				default:
					return;
				}
				break;
			case 2:
				switch (targetFrameNumber)
				{
				case 6:
					this.frontShoulderOffset.X = -2f;
					return;
				case 7:
				case 8:
				case 9:
				case 10:
					this.frontShoulderOffset.X = -4f;
					return;
				case 11:
				case 12:
				case 13:
				case 14:
					this.frontShoulderOffset.X = -2f;
					return;
				case 15:
				case 16:
				case 17:
					break;
				case 18:
				case 19:
					this.frontShoulderOffset.X = -2f;
					return;
				default:
					return;
				}
				break;
			case 3:
				if (targetFrameNumber - 7 <= 2)
				{
					this.frontShoulderOffset.X = -2f;
					return;
				}
				if (targetFrameNumber - 15 > 2)
				{
					return;
				}
				this.frontShoulderOffset.X = 2f;
				return;
			case 4:
				switch (targetFrameNumber)
				{
				case 6:
					this.frontShoulderOffset.X = -2f;
					return;
				case 7:
				case 8:
				case 9:
				case 10:
					this.frontShoulderOffset.X = -4f;
					return;
				case 11:
				case 12:
				case 13:
					this.frontShoulderOffset.X = -2f;
					return;
				case 14:
				case 17:
					break;
				case 15:
				case 16:
					this.frontShoulderOffset.X = 2f;
					return;
				case 18:
				case 19:
					this.frontShoulderOffset.X = -2f;
					return;
				default:
					return;
				}
				break;
			case 5:
				if (targetFrameNumber - 7 <= 3)
				{
					this.frontShoulderOffset.X = -2f;
					return;
				}
				if (targetFrameNumber - 15 > 1)
				{
					return;
				}
				this.frontShoulderOffset.X = 2f;
				return;
			case 6:
				if (targetFrameNumber - 7 <= 3)
				{
					this.frontShoulderOffset.X = -2f;
					return;
				}
				if (targetFrameNumber - 14 > 3)
				{
					return;
				}
				this.frontShoulderOffset.X = 2f;
				return;
			case 7:
				switch (targetFrameNumber)
				{
				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
					this.frontShoulderOffset.X = -2f;
					return;
				case 11:
				case 12:
				case 13:
				case 14:
					this.frontShoulderOffset.X = -2f;
					return;
				case 15:
				case 16:
				case 17:
					break;
				case 18:
				case 19:
					this.frontShoulderOffset.X = -2f;
					break;
				default:
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x0063950B File Offset: 0x0063770B
		private Rectangle CreateCompositeFrameRect(Point pt)
		{
			return new Rectangle(pt.X * 40, pt.Y * 56, 40, 56);
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x00639528 File Offset: 0x00637728
		private void UpdateCompositeArm(Player.CompositeArmData data, ref float rotation, ref Point frameIndex, int targetX)
		{
			if (!data.enabled)
			{
				rotation = 0f;
				return;
			}
			rotation = data.rotation;
			switch (data.stretch)
			{
			case Player.CompositeArmStretchAmount.Full:
				frameIndex.X = targetX;
				frameIndex.Y = 0;
				return;
			case Player.CompositeArmStretchAmount.None:
				frameIndex.X = targetX;
				frameIndex.Y = 3;
				return;
			case Player.CompositeArmStretchAmount.Quarter:
				frameIndex.X = targetX;
				frameIndex.Y = 2;
				return;
			case Player.CompositeArmStretchAmount.ThreeQuarters:
				frameIndex.X = targetX;
				frameIndex.Y = 1;
				return;
			default:
				return;
			}
		}

		// Token: 0x04005CA8 RID: 23720
		public List<DrawData> DrawDataCache;

		// Token: 0x04005CA9 RID: 23721
		public List<int> DustCache;

		// Token: 0x04005CAA RID: 23722
		public List<int> GoreCache;

		// Token: 0x04005CAB RID: 23723
		public Player drawPlayer;

		// Token: 0x04005CAC RID: 23724
		public float shadow;

		// Token: 0x04005CAD RID: 23725
		public Vector2 Position;

		// Token: 0x04005CAE RID: 23726
		public int projectileDrawPosition;

		// Token: 0x04005CAF RID: 23727
		public Vector2 ItemLocation;

		// Token: 0x04005CB0 RID: 23728
		public int armorAdjust;

		// Token: 0x04005CB1 RID: 23729
		public bool missingHand;

		// Token: 0x04005CB2 RID: 23730
		public bool missingArm;

		// Token: 0x04005CB3 RID: 23731
		public int skinVar;

		// Token: 0x04005CB4 RID: 23732
		public bool fullHair;

		// Token: 0x04005CB5 RID: 23733
		public bool drawsBackHairWithoutHeadgear;

		// Token: 0x04005CB6 RID: 23734
		public bool hatHair;

		// Token: 0x04005CB7 RID: 23735
		public bool hideHair;

		// Token: 0x04005CB8 RID: 23736
		public int hairDyePacked;

		// Token: 0x04005CB9 RID: 23737
		public int skinDyePacked;

		// Token: 0x04005CBA RID: 23738
		public float mountOffSet;

		// Token: 0x04005CBB RID: 23739
		public int cHead;

		// Token: 0x04005CBC RID: 23740
		public int cBody;

		// Token: 0x04005CBD RID: 23741
		public int cLegs;

		// Token: 0x04005CBE RID: 23742
		public int cHandOn;

		// Token: 0x04005CBF RID: 23743
		public int cHandOff;

		// Token: 0x04005CC0 RID: 23744
		public int cBack;

		// Token: 0x04005CC1 RID: 23745
		public int cFront;

		// Token: 0x04005CC2 RID: 23746
		public int cShoe;

		// Token: 0x04005CC3 RID: 23747
		public int cFlameWaker;

		// Token: 0x04005CC4 RID: 23748
		public int cWaist;

		// Token: 0x04005CC5 RID: 23749
		public int cShield;

		// Token: 0x04005CC6 RID: 23750
		public int cNeck;

		// Token: 0x04005CC7 RID: 23751
		public int cFace;

		// Token: 0x04005CC8 RID: 23752
		public int cBalloon;

		// Token: 0x04005CC9 RID: 23753
		public int cWings;

		// Token: 0x04005CCA RID: 23754
		public int cCarpet;

		// Token: 0x04005CCB RID: 23755
		public int cPortableStool;

		// Token: 0x04005CCC RID: 23756
		public int cFloatingTube;

		// Token: 0x04005CCD RID: 23757
		public int cUnicornHorn;

		// Token: 0x04005CCE RID: 23758
		public int cAngelHalo;

		// Token: 0x04005CCF RID: 23759
		public int cBeard;

		// Token: 0x04005CD0 RID: 23760
		public int cLeinShampoo;

		// Token: 0x04005CD1 RID: 23761
		public int cBackpack;

		// Token: 0x04005CD2 RID: 23762
		public int cTail;

		// Token: 0x04005CD3 RID: 23763
		public int cFaceHead;

		// Token: 0x04005CD4 RID: 23764
		public int cFaceFlower;

		// Token: 0x04005CD5 RID: 23765
		public int cFaceMask;

		// Token: 0x04005CD6 RID: 23766
		public int cBalloonFront;

		// Token: 0x04005CD7 RID: 23767
		public int cCoat;

		// Token: 0x04005CD8 RID: 23768
		public SpriteEffects playerEffect;

		// Token: 0x04005CD9 RID: 23769
		public SpriteEffects itemEffect;

		// Token: 0x04005CDA RID: 23770
		public Color colorHair;

		// Token: 0x04005CDB RID: 23771
		public Color colorEyeWhites;

		// Token: 0x04005CDC RID: 23772
		public Color colorEyes;

		// Token: 0x04005CDD RID: 23773
		public Color colorHead;

		// Token: 0x04005CDE RID: 23774
		public Color colorBodySkin;

		// Token: 0x04005CDF RID: 23775
		public Color colorLegs;

		// Token: 0x04005CE0 RID: 23776
		public Color colorShirt;

		// Token: 0x04005CE1 RID: 23777
		public Color colorUnderShirt;

		// Token: 0x04005CE2 RID: 23778
		public Color colorPants;

		// Token: 0x04005CE3 RID: 23779
		public Color colorShoes;

		// Token: 0x04005CE4 RID: 23780
		public Color colorArmorHead;

		// Token: 0x04005CE5 RID: 23781
		public Color colorArmorBody;

		// Token: 0x04005CE6 RID: 23782
		public Color colorMount;

		// Token: 0x04005CE7 RID: 23783
		public Color colorArmorLegs;

		// Token: 0x04005CE8 RID: 23784
		public Color colorElectricity;

		// Token: 0x04005CE9 RID: 23785
		public Color colorDisplayDollSkin;

		// Token: 0x04005CEA RID: 23786
		public int headGlowMask;

		// Token: 0x04005CEB RID: 23787
		public int bodyGlowMask;

		// Token: 0x04005CEC RID: 23788
		public int armGlowMask;

		// Token: 0x04005CED RID: 23789
		public int legsGlowMask;

		// Token: 0x04005CEE RID: 23790
		public Color headGlowColor;

		// Token: 0x04005CEF RID: 23791
		public Color bodyGlowColor;

		// Token: 0x04005CF0 RID: 23792
		public Color armGlowColor;

		// Token: 0x04005CF1 RID: 23793
		public Color legsGlowColor;

		// Token: 0x04005CF2 RID: 23794
		public Color ArkhalisColor;

		// Token: 0x04005CF3 RID: 23795
		public float stealth;

		// Token: 0x04005CF4 RID: 23796
		public Vector2 legVect;

		// Token: 0x04005CF5 RID: 23797
		public Vector2 bodyVect;

		// Token: 0x04005CF6 RID: 23798
		public Vector2 headVect;

		// Token: 0x04005CF7 RID: 23799
		public Color selectionGlowColor;

		// Token: 0x04005CF8 RID: 23800
		public float torsoOffset;

		// Token: 0x04005CF9 RID: 23801
		public bool hidesTopSkin;

		// Token: 0x04005CFA RID: 23802
		public bool hidesBottomSkin;

		// Token: 0x04005CFB RID: 23803
		public float rotation;

		// Token: 0x04005CFC RID: 23804
		public Vector2 rotationOrigin;

		// Token: 0x04005CFD RID: 23805
		public Rectangle hairFrontFrame;

		// Token: 0x04005CFE RID: 23806
		public Rectangle hairBackFrame;

		// Token: 0x04005CFF RID: 23807
		public bool backHairDraw;

		// Token: 0x04005D00 RID: 23808
		public Color itemColor;

		// Token: 0x04005D01 RID: 23809
		public bool usesCompositeTorso;

		// Token: 0x04005D02 RID: 23810
		public bool usesCompositeFrontHandAcc;

		// Token: 0x04005D03 RID: 23811
		public bool usesCompositeBackHandAcc;

		// Token: 0x04005D04 RID: 23812
		public bool compShoulderOverFrontArm;

		// Token: 0x04005D05 RID: 23813
		public Rectangle compBackShoulderFrame;

		// Token: 0x04005D06 RID: 23814
		public Rectangle compFrontShoulderFrame;

		// Token: 0x04005D07 RID: 23815
		public Rectangle compBackArmFrame;

		// Token: 0x04005D08 RID: 23816
		public Rectangle compFrontArmFrame;

		// Token: 0x04005D09 RID: 23817
		public Rectangle compTorsoFrame;

		// Token: 0x04005D0A RID: 23818
		public float compositeBackArmRotation;

		// Token: 0x04005D0B RID: 23819
		public float compositeFrontArmRotation;

		// Token: 0x04005D0C RID: 23820
		public bool hideCompositeShoulders;

		// Token: 0x04005D0D RID: 23821
		public Vector2 frontShoulderOffset;

		// Token: 0x04005D0E RID: 23822
		public Vector2 backShoulderOffset;

		// Token: 0x04005D0F RID: 23823
		public WeaponDrawOrder weaponDrawOrder;

		// Token: 0x04005D10 RID: 23824
		public bool weaponOverFrontArm;

		// Token: 0x04005D11 RID: 23825
		public bool isSitting;

		// Token: 0x04005D12 RID: 23826
		public bool isSleeping;

		// Token: 0x04005D13 RID: 23827
		public float seatYOffset;

		// Token: 0x04005D14 RID: 23828
		public int sittingIndex;

		// Token: 0x04005D15 RID: 23829
		public bool drawFrontAccInNeckAccLayer;

		// Token: 0x04005D16 RID: 23830
		public bool drawFrontAccInNeckAccLayerAlways;

		// Token: 0x04005D17 RID: 23831
		public bool mountHandlesHeadDraw;

		// Token: 0x04005D18 RID: 23832
		public bool mountDrawsEyelid;

		// Token: 0x04005D19 RID: 23833
		public Item heldItem;

		// Token: 0x04005D1A RID: 23834
		public bool drawFloatingTube;

		// Token: 0x04005D1B RID: 23835
		public bool drawUnicornHorn;

		// Token: 0x04005D1C RID: 23836
		public bool drawAngelHalo;

		// Token: 0x04005D1D RID: 23837
		public Color floatingTubeColor;

		// Token: 0x04005D1E RID: 23838
		public Vector2 hairOffset;

		// Token: 0x04005D1F RID: 23839
		public Vector2 helmetOffset;

		// Token: 0x04005D20 RID: 23840
		public Vector2 legsOffset;

		// Token: 0x04005D21 RID: 23841
		public bool hideEntirePlayer;

		// Token: 0x04005D22 RID: 23842
		public bool hideEntirePlayerExceptHelmetsAndFaceAccessories;

		// Token: 0x04005D23 RID: 23843
		public Projectile SelectedDrawnProjectile;
	}
}
