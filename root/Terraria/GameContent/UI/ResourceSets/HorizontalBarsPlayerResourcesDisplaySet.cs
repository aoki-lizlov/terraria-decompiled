using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.DataStructures;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003BB RID: 955
	public class HorizontalBarsPlayerResourcesDisplaySet : IPlayerResourcesDisplaySet, IConfigKeyHolder
	{
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06002CFB RID: 11515 RVA: 0x005A1620 File Offset: 0x0059F820
		// (set) Token: 0x06002CFC RID: 11516 RVA: 0x005A1628 File Offset: 0x0059F828
		public string NameKey
		{
			[CompilerGenerated]
			get
			{
				return this.<NameKey>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<NameKey>k__BackingField = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x005A1631 File Offset: 0x0059F831
		// (set) Token: 0x06002CFE RID: 11518 RVA: 0x005A1639 File Offset: 0x0059F839
		public string ConfigKey
		{
			[CompilerGenerated]
			get
			{
				return this.<ConfigKey>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ConfigKey>k__BackingField = value;
			}
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x005A1644 File Offset: 0x0059F844
		public HorizontalBarsPlayerResourcesDisplaySet(string nameKey, string configKey, string resourceFolderName, AssetRequestMode mode)
		{
			this.NameKey = nameKey;
			this.ConfigKey = configKey;
			if (configKey == "HorizontalBarsWithFullText")
			{
				this._drawTextStyle = 2;
			}
			else if (configKey == "HorizontalBarsWithText")
			{
				this._drawTextStyle = 1;
			}
			else
			{
				this._drawTextStyle = 0;
			}
			string text = "Images\\UI\\PlayerResourceSets\\" + resourceFolderName;
			this._hpFill = Main.Assets.Request<Texture2D>(text + "\\HP_Fill", mode);
			this._hpFillHoney = Main.Assets.Request<Texture2D>(text + "\\HP_Fill_Honey", mode);
			this._mpFill = Main.Assets.Request<Texture2D>(text + "\\MP_Fill", mode);
			this._panelLeft = Main.Assets.Request<Texture2D>(text + "\\Panel_Left", mode);
			this._panelMiddleHP = Main.Assets.Request<Texture2D>(text + "\\HP_Panel_Middle", mode);
			this._panelRightHP = Main.Assets.Request<Texture2D>(text + "\\HP_Panel_Right", mode);
			this._panelMiddleMP = Main.Assets.Request<Texture2D>(text + "\\MP_Panel_Middle", mode);
			this._panelRightMP = Main.Assets.Request<Texture2D>(text + "\\MP_Panel_Right", mode);
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x005A178C File Offset: 0x0059F98C
		public void Draw()
		{
			this.PrepareFields(Main.LocalPlayer);
			SpriteBatch spriteBatch = Main.spriteBatch;
			int num = 16;
			int num2 = 18;
			int num3 = Main.screenWidth - 300 - 22 + num;
			if (this._drawTextStyle == 2)
			{
				num2 += 2;
				HorizontalBarsPlayerResourcesDisplaySet.DrawLifeBarText(spriteBatch, new Vector2((float)num3, (float)num2));
				HorizontalBarsPlayerResourcesDisplaySet.DrawManaText(spriteBatch);
			}
			else if (this._drawTextStyle == 1)
			{
				num2 += 4;
				HorizontalBarsPlayerResourcesDisplaySet.DrawLifeBarText(spriteBatch, new Vector2((float)num3, (float)num2));
			}
			Vector2 vector = new Vector2((float)num3, (float)num2);
			vector.X += (float)((this._maxSegmentCount - this._hpSegmentsCount) * this._panelMiddleHP.Width());
			bool flag = false;
			ResourceDrawSettings resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._hpSegmentsCount + 2;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector;
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.LifePanelDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.Draw(spriteBatch, ref flag);
			resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._hpSegmentsCount;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector + new Vector2(6f, 6f);
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.LifeFillingDrawer);
			resourceDrawSettings.OffsetPerDraw = new Vector2((float)this._hpFill.Width(), 0f);
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.Draw(spriteBatch, ref flag);
			this._hpHovered = flag;
			flag = false;
			Vector2 vector2 = new Vector2((float)(num3 - 10), (float)(num2 + 24));
			vector2.X += (float)((this._maxSegmentCount - this._mpSegmentsCount) * this._panelMiddleMP.Width());
			resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._mpSegmentsCount + 2;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector2;
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.ManaPanelDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.Draw(spriteBatch, ref flag);
			resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._mpSegmentsCount;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector2 + new Vector2(6f, 6f);
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.ManaFillingDrawer);
			resourceDrawSettings.OffsetPerDraw = new Vector2((float)this._mpFill.Width(), 0f);
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.Draw(spriteBatch, ref flag);
			this._mpHovered = flag;
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x005A1A98 File Offset: 0x0059FC98
		private static void DrawManaText(SpriteBatch spriteBatch)
		{
			Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			int num = 180;
			Player localPlayer = Main.LocalPlayer;
			string text = Lang.inter[2].Value + ":";
			string text2 = localPlayer.statMana + "/" + localPlayer.statManaMax2;
			Vector2 vector = new Vector2((float)(Main.screenWidth - num), 65f);
			string text3 = text + " " + text2;
			Vector2 vector2 = FontAssets.MouseText.Value.MeasureString(text3);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text, vector + new Vector2(-vector2.X * 0.5f, 0f), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f, null, null);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, text2, vector + new Vector2(vector2.X * 0.5f, 0f), color, 0f, new Vector2(FontAssets.MouseText.Value.MeasureString(text2).X, 0f), 1f, SpriteEffects.None, 0f, null, null);
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x005A1BEC File Offset: 0x0059FDEC
		private static void DrawLifeBarText(SpriteBatch spriteBatch, Vector2 topLeftAnchor)
		{
			Vector2 vector = topLeftAnchor + new Vector2(130f, -20f);
			Player localPlayer = Main.LocalPlayer;
			Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			string text = string.Concat(new object[]
			{
				Lang.inter[0].Value,
				" ",
				localPlayer.statLifeMax2,
				"/",
				localPlayer.statLifeMax2
			});
			Vector2 vector2 = FontAssets.MouseText.Value.MeasureString(text);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, Lang.inter[0].Value, vector + new Vector2(-vector2.X * 0.5f, 0f), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f, null, null);
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, localPlayer.statLife + "/" + localPlayer.statLifeMax2, vector + new Vector2(vector2.X * 0.5f, 0f), color, 0f, new Vector2(FontAssets.MouseText.Value.MeasureString(localPlayer.statLife + "/" + localPlayer.statLifeMax2).X, 0f), 1f, SpriteEffects.None, 0f, null, null);
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x005A1D80 File Offset: 0x0059FF80
		private void PrepareFields(Player player)
		{
			PlayerStatsSnapshot playerStatsSnapshot = new PlayerStatsSnapshot(player);
			this._hpSegmentsCount = (int)((float)playerStatsSnapshot.LifeMax / playerStatsSnapshot.LifePerSegment);
			this._mpSegmentsCount = (int)((float)playerStatsSnapshot.ManaMax / playerStatsSnapshot.ManaPerSegment);
			this._maxSegmentCount = 20;
			this._hpFruitCount = playerStatsSnapshot.LifeFruitCount;
			this._hpPercent = (float)playerStatsSnapshot.Life / (float)playerStatsSnapshot.LifeMax;
			this._mpPercent = (float)playerStatsSnapshot.Mana / (float)playerStatsSnapshot.ManaMax;
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x005A1E00 File Offset: 0x005A0000
		private void LifePanelDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			sprite = this._panelLeft;
			drawScale = 1f;
			if (elementIndex == lastElementIndex)
			{
				sprite = this._panelRightHP;
				offset = new Vector2(-16f, -10f);
				return;
			}
			if (elementIndex != firstElementIndex)
			{
				sprite = this._panelMiddleHP;
			}
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x005A1E64 File Offset: 0x005A0064
		private void ManaPanelDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			sprite = this._panelLeft;
			drawScale = 1f;
			if (elementIndex == lastElementIndex)
			{
				sprite = this._panelRightMP;
				offset = new Vector2(-16f, -6f);
				return;
			}
			if (elementIndex != firstElementIndex)
			{
				sprite = this._panelMiddleMP;
			}
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x005A1EC7 File Offset: 0x005A00C7
		private void LifeFillingDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sprite = this._hpFill;
			if (elementIndex >= this._hpSegmentsCount - this._hpFruitCount)
			{
				sprite = this._hpFillHoney;
			}
			HorizontalBarsPlayerResourcesDisplaySet.FillBarByValues(elementIndex, sprite, this._hpSegmentsCount, this._hpPercent, out offset, out drawScale, out sourceRect);
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x005A1F08 File Offset: 0x005A0108
		private static void FillBarByValues(int elementIndex, Asset<Texture2D> sprite, int segmentsCount, float fillPercent, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			float num = 1f / (float)segmentsCount;
			float num2 = 1f - fillPercent;
			float lerpValue = Utils.GetLerpValue(num * (float)elementIndex, num * (float)(elementIndex + 1), num2, true);
			float num3 = 1f - lerpValue;
			drawScale = 1f;
			Rectangle rectangle = sprite.Frame(1, 1, 0, 0, 0, 0);
			int num4 = (int)((float)rectangle.Width * (1f - num3));
			offset.X += (float)num4;
			rectangle.X += num4;
			rectangle.Width -= num4;
			sourceRect = new Rectangle?(rectangle);
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x005A1FBD File Offset: 0x005A01BD
		private void ManaFillingDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sprite = this._mpFill;
			HorizontalBarsPlayerResourcesDisplaySet.FillBarByValues(elementIndex, sprite, this._mpSegmentsCount, this._mpPercent, out offset, out drawScale, out sourceRect);
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x005A1FE3 File Offset: 0x005A01E3
		public void TryToHover()
		{
			if (this._hpHovered)
			{
				CommonResourceBarMethods.DrawLifeMouseOver();
			}
			if (this._mpHovered)
			{
				CommonResourceBarMethods.DrawManaMouseOver();
			}
		}

		// Token: 0x04005473 RID: 21619
		private int _maxSegmentCount;

		// Token: 0x04005474 RID: 21620
		private int _hpSegmentsCount;

		// Token: 0x04005475 RID: 21621
		private int _mpSegmentsCount;

		// Token: 0x04005476 RID: 21622
		private int _hpFruitCount;

		// Token: 0x04005477 RID: 21623
		private float _hpPercent;

		// Token: 0x04005478 RID: 21624
		private float _mpPercent;

		// Token: 0x04005479 RID: 21625
		private byte _drawTextStyle;

		// Token: 0x0400547A RID: 21626
		private bool _hpHovered;

		// Token: 0x0400547B RID: 21627
		private bool _mpHovered;

		// Token: 0x0400547C RID: 21628
		private Asset<Texture2D> _hpFill;

		// Token: 0x0400547D RID: 21629
		private Asset<Texture2D> _hpFillHoney;

		// Token: 0x0400547E RID: 21630
		private Asset<Texture2D> _mpFill;

		// Token: 0x0400547F RID: 21631
		private Asset<Texture2D> _panelLeft;

		// Token: 0x04005480 RID: 21632
		private Asset<Texture2D> _panelMiddleHP;

		// Token: 0x04005481 RID: 21633
		private Asset<Texture2D> _panelRightHP;

		// Token: 0x04005482 RID: 21634
		private Asset<Texture2D> _panelMiddleMP;

		// Token: 0x04005483 RID: 21635
		private Asset<Texture2D> _panelRightMP;

		// Token: 0x04005484 RID: 21636
		[CompilerGenerated]
		private string <NameKey>k__BackingField;

		// Token: 0x04005485 RID: 21637
		[CompilerGenerated]
		private string <ConfigKey>k__BackingField;
	}
}
