using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.DataStructures;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003BA RID: 954
	public class FancyClassicPlayerResourcesDisplaySet : IPlayerResourcesDisplaySet, IConfigKeyHolder
	{
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x005A0ADC File Offset: 0x0059ECDC
		// (set) Token: 0x06002CEC RID: 11500 RVA: 0x005A0AE4 File Offset: 0x0059ECE4
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

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x005A0AED File Offset: 0x0059ECED
		// (set) Token: 0x06002CEE RID: 11502 RVA: 0x005A0AF5 File Offset: 0x0059ECF5
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

		// Token: 0x06002CEF RID: 11503 RVA: 0x005A0B00 File Offset: 0x0059ED00
		public FancyClassicPlayerResourcesDisplaySet(string nameKey, string configKey, string resourceFolderName, AssetRequestMode mode)
		{
			this.NameKey = nameKey;
			this.ConfigKey = configKey;
			if (configKey == "NewWithText")
			{
				this._drawText = true;
			}
			else
			{
				this._drawText = false;
			}
			string text = "Images\\UI\\PlayerResourceSets\\" + resourceFolderName;
			this._heartLeft = Main.Assets.Request<Texture2D>(text + "\\Heart_Left", mode);
			this._heartMiddle = Main.Assets.Request<Texture2D>(text + "\\Heart_Middle", mode);
			this._heartRight = Main.Assets.Request<Texture2D>(text + "\\Heart_Right", mode);
			this._heartRightFancy = Main.Assets.Request<Texture2D>(text + "\\Heart_Right_Fancy", mode);
			this._heartFill = Main.Assets.Request<Texture2D>(text + "\\Heart_Fill", mode);
			this._heartFillHoney = Main.Assets.Request<Texture2D>(text + "\\Heart_Fill_B", mode);
			this._heartSingleFancy = Main.Assets.Request<Texture2D>(text + "\\Heart_Single_Fancy", mode);
			this._starTop = Main.Assets.Request<Texture2D>(text + "\\Star_A", mode);
			this._starMiddle = Main.Assets.Request<Texture2D>(text + "\\Star_B", mode);
			this._starBottom = Main.Assets.Request<Texture2D>(text + "\\Star_C", mode);
			this._starSingle = Main.Assets.Request<Texture2D>(text + "\\Star_Single", mode);
			this._starFill = Main.Assets.Request<Texture2D>(text + "\\Star_Fill", mode);
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x005A0CA8 File Offset: 0x0059EEA8
		public void Draw()
		{
			Player localPlayer = Main.LocalPlayer;
			SpriteBatch spriteBatch = Main.spriteBatch;
			this.PrepareFields(localPlayer);
			this.DrawLifeBar(spriteBatch);
			this.DrawManaBar(spriteBatch);
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x005A0CD8 File Offset: 0x0059EED8
		private void DrawLifeBar(SpriteBatch spriteBatch)
		{
			Vector2 vector = new Vector2((float)(Main.screenWidth - 300 + 4), 15f);
			if (this._drawText)
			{
				vector.Y += 6f;
				FancyClassicPlayerResourcesDisplaySet.DrawLifeBarText(spriteBatch, vector + new Vector2(-4f, 3f));
			}
			bool flag = false;
			ResourceDrawSettings resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._heartCountRow1;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector;
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.HeartPanelDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.Draw(spriteBatch, ref flag);
			resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._heartCountRow2;
			resourceDrawSettings.ElementIndexOffset = 10;
			resourceDrawSettings.TopLeftAnchor = vector + new Vector2(0f, 28f);
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.HeartPanelDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.Draw(spriteBatch, ref flag);
			resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._heartCountRow1;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector + new Vector2(15f, 15f);
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.HeartFillingDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.UnitX * 2f;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = new Vector2(0.5f, 0.5f);
			resourceDrawSettings.Draw(spriteBatch, ref flag);
			resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._heartCountRow2;
			resourceDrawSettings.ElementIndexOffset = 10;
			resourceDrawSettings.TopLeftAnchor = vector + new Vector2(15f, 15f) + new Vector2(0f, 28f);
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.HeartFillingDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.UnitX * 2f;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitX;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = new Vector2(0.5f, 0.5f);
			resourceDrawSettings.Draw(spriteBatch, ref flag);
			this._hoverLife = flag;
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x005A0F80 File Offset: 0x0059F180
		private static void DrawLifeBarText(SpriteBatch spriteBatch, Vector2 topLeftAnchor)
		{
			Vector2 vector = topLeftAnchor + new Vector2(130f, -24f);
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

		// Token: 0x06002CF3 RID: 11507 RVA: 0x005A1114 File Offset: 0x0059F314
		private void DrawManaBar(SpriteBatch spriteBatch)
		{
			Vector2 vector = new Vector2((float)(Main.screenWidth - 40), 22f);
			int starCount = this._starCount;
			bool flag = false;
			ResourceDrawSettings resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._starCount;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector;
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.StarPanelDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.Zero;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitY;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = Vector2.Zero;
			resourceDrawSettings.Draw(spriteBatch, ref flag);
			resourceDrawSettings = default(ResourceDrawSettings);
			resourceDrawSettings.ElementCount = this._starCount;
			resourceDrawSettings.ElementIndexOffset = 0;
			resourceDrawSettings.TopLeftAnchor = vector + new Vector2(15f, 16f);
			resourceDrawSettings.GetTextureMethod = new ResourceDrawSettings.TextureGetter(this.StarFillingDrawer);
			resourceDrawSettings.OffsetPerDraw = Vector2.UnitY * -2f;
			resourceDrawSettings.OffsetPerDrawByTexturePercentile = Vector2.UnitY;
			resourceDrawSettings.OffsetSpriteAnchor = Vector2.Zero;
			resourceDrawSettings.OffsetSpriteAnchorByTexturePercentile = new Vector2(0.5f, 0.5f);
			resourceDrawSettings.Draw(spriteBatch, ref flag);
			this._hoverMana = flag;
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x005A1254 File Offset: 0x0059F454
		private static void DrawManaText(SpriteBatch spriteBatch)
		{
			Vector2 vector = FontAssets.MouseText.Value.MeasureString(Lang.inter[2].Value);
			Color color = new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor);
			int num = 50;
			if (vector.X >= 45f)
			{
				num = (int)vector.X + 5;
			}
			DynamicSpriteFontExtensionMethods.DrawString(spriteBatch, FontAssets.MouseText.Value, Lang.inter[2].Value, new Vector2((float)(Main.screenWidth - num), 6f), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f, null, null);
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x005A12FC File Offset: 0x0059F4FC
		private void HeartPanelDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			sprite = this._heartLeft;
			drawScale = 1f;
			if (elementIndex == lastElementIndex && elementIndex == firstElementIndex)
			{
				sprite = this._heartSingleFancy;
				offset = new Vector2(-4f, -4f);
				return;
			}
			if (elementIndex == lastElementIndex && lastElementIndex == this._lastHeartPanelIndex)
			{
				sprite = this._heartRightFancy;
				offset = new Vector2(-8f, -4f);
				return;
			}
			if (elementIndex == lastElementIndex)
			{
				sprite = this._heartRight;
				return;
			}
			if (elementIndex != firstElementIndex)
			{
				sprite = this._heartMiddle;
			}
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x005A13A0 File Offset: 0x0059F5A0
		private void HeartFillingDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			sprite = this._heartLeft;
			if (elementIndex < this._playerLifeFruitCount)
			{
				sprite = this._heartFillHoney;
			}
			else
			{
				sprite = this._heartFill;
			}
			float lerpValue = Utils.GetLerpValue(this._lifePerHeart * (float)elementIndex, this._lifePerHeart * (float)(elementIndex + 1), this._currentPlayerLife, true);
			drawScale = lerpValue;
			if (elementIndex == this._lastHeartFillingIndex && lerpValue > 0f)
			{
				drawScale += Main.cursorScale - 1f;
			}
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x005A1430 File Offset: 0x0059F630
		private void StarPanelDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			sprite = this._starTop;
			drawScale = 1f;
			if (elementIndex == lastElementIndex && elementIndex == firstElementIndex)
			{
				sprite = this._starSingle;
				return;
			}
			if (elementIndex == lastElementIndex)
			{
				sprite = this._starBottom;
				offset = new Vector2(0f, 0f);
				return;
			}
			if (elementIndex != firstElementIndex)
			{
				sprite = this._starMiddle;
			}
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x005A14A8 File Offset: 0x0059F6A8
		private void StarFillingDrawer(int elementIndex, int firstElementIndex, int lastElementIndex, out Asset<Texture2D> sprite, out Vector2 offset, out float drawScale, out Rectangle? sourceRect)
		{
			sourceRect = null;
			offset = Vector2.Zero;
			sprite = this._starFill;
			float lerpValue = Utils.GetLerpValue(this._manaPerStar * (float)elementIndex, this._manaPerStar * (float)(elementIndex + 1), this._currentPlayerMana, true);
			drawScale = lerpValue;
			if (elementIndex == this._lastStarFillingIndex && lerpValue > 0f)
			{
				drawScale += Main.cursorScale - 1f;
			}
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x005A151C File Offset: 0x0059F71C
		private void PrepareFields(Player player)
		{
			PlayerStatsSnapshot playerStatsSnapshot = new PlayerStatsSnapshot(player);
			this._playerLifeFruitCount = playerStatsSnapshot.LifeFruitCount;
			this._lifePerHeart = playerStatsSnapshot.LifePerSegment;
			this._currentPlayerLife = (float)playerStatsSnapshot.Life;
			this._manaPerStar = playerStatsSnapshot.ManaPerSegment;
			this._heartCountRow1 = Utils.Clamp<int>((int)((float)playerStatsSnapshot.LifeMax / this._lifePerHeart), 0, 10);
			this._heartCountRow2 = Utils.Clamp<int>((int)((float)(playerStatsSnapshot.LifeMax - 200) / this._lifePerHeart), 0, 10);
			int num = (int)((float)playerStatsSnapshot.Life / this._lifePerHeart);
			this._lastHeartFillingIndex = num;
			this._lastHeartPanelIndex = this._heartCountRow1 + this._heartCountRow2 - 1;
			this._starCount = (int)((float)playerStatsSnapshot.ManaMax / this._manaPerStar);
			this._currentPlayerMana = (float)playerStatsSnapshot.Mana;
			this._lastStarFillingIndex = (int)(this._currentPlayerMana / this._manaPerStar);
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x005A1604 File Offset: 0x0059F804
		public void TryToHover()
		{
			if (this._hoverLife)
			{
				CommonResourceBarMethods.DrawLifeMouseOver();
			}
			if (this._hoverMana)
			{
				CommonResourceBarMethods.DrawManaMouseOver();
			}
		}

		// Token: 0x04005457 RID: 21591
		private float _currentPlayerLife;

		// Token: 0x04005458 RID: 21592
		private float _lifePerHeart;

		// Token: 0x04005459 RID: 21593
		private int _playerLifeFruitCount;

		// Token: 0x0400545A RID: 21594
		private int _lastHeartFillingIndex;

		// Token: 0x0400545B RID: 21595
		private int _lastHeartPanelIndex;

		// Token: 0x0400545C RID: 21596
		private int _heartCountRow1;

		// Token: 0x0400545D RID: 21597
		private int _heartCountRow2;

		// Token: 0x0400545E RID: 21598
		private int _starCount;

		// Token: 0x0400545F RID: 21599
		private int _lastStarFillingIndex;

		// Token: 0x04005460 RID: 21600
		private float _manaPerStar;

		// Token: 0x04005461 RID: 21601
		private float _currentPlayerMana;

		// Token: 0x04005462 RID: 21602
		private Asset<Texture2D> _heartLeft;

		// Token: 0x04005463 RID: 21603
		private Asset<Texture2D> _heartMiddle;

		// Token: 0x04005464 RID: 21604
		private Asset<Texture2D> _heartRight;

		// Token: 0x04005465 RID: 21605
		private Asset<Texture2D> _heartRightFancy;

		// Token: 0x04005466 RID: 21606
		private Asset<Texture2D> _heartFill;

		// Token: 0x04005467 RID: 21607
		private Asset<Texture2D> _heartFillHoney;

		// Token: 0x04005468 RID: 21608
		private Asset<Texture2D> _heartSingleFancy;

		// Token: 0x04005469 RID: 21609
		private Asset<Texture2D> _starTop;

		// Token: 0x0400546A RID: 21610
		private Asset<Texture2D> _starMiddle;

		// Token: 0x0400546B RID: 21611
		private Asset<Texture2D> _starBottom;

		// Token: 0x0400546C RID: 21612
		private Asset<Texture2D> _starSingle;

		// Token: 0x0400546D RID: 21613
		private Asset<Texture2D> _starFill;

		// Token: 0x0400546E RID: 21614
		private bool _hoverLife;

		// Token: 0x0400546F RID: 21615
		private bool _hoverMana;

		// Token: 0x04005470 RID: 21616
		private bool _drawText;

		// Token: 0x04005471 RID: 21617
		[CompilerGenerated]
		private string <NameKey>k__BackingField;

		// Token: 0x04005472 RID: 21618
		[CompilerGenerated]
		private string <ConfigKey>k__BackingField;
	}
}
