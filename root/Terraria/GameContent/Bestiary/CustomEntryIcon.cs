using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200033B RID: 827
	public class CustomEntryIcon : IEntryIcon
	{
		// Token: 0x0600284B RID: 10315 RVA: 0x0057308E File Offset: 0x0057128E
		public CustomEntryIcon(string nameLanguageKey, string texturePath, Func<bool> unlockCondition)
		{
			this._text = Language.GetText(nameLanguageKey);
			this._textureAsset = Main.Assets.Request<Texture2D>(texturePath, 1);
			this._unlockCondition = unlockCondition;
			this.UpdateUnlockState(false);
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x005730C2 File Offset: 0x005712C2
		public IEntryIcon CreateClone()
		{
			return new CustomEntryIcon(this._text.Key, this._textureAsset.Name, this._unlockCondition);
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x005730E5 File Offset: 0x005712E5
		public void Update(BestiaryUICollectionInfo providedInfo, Rectangle hitbox, EntryIconDrawSettings settings)
		{
			this.UpdateUnlockState(this.GetUnlockState(providedInfo));
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x005730F4 File Offset: 0x005712F4
		public void Draw(BestiaryUICollectionInfo providedInfo, SpriteBatch spriteBatch, EntryIconDrawSettings settings)
		{
			Rectangle iconbox = settings.iconbox;
			spriteBatch.Draw(this._textureAsset.Value, iconbox.Center.ToVector2() + Vector2.One, new Rectangle?(this._sourceRectangle), Color.White, 0f, this._sourceRectangle.Size() / 2f, 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x00573164 File Offset: 0x00571364
		public string GetHoverText(BestiaryUICollectionInfo providedInfo)
		{
			if (this.GetUnlockState(providedInfo))
			{
				return this._text.Value;
			}
			return "???";
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x00573180 File Offset: 0x00571380
		private void UpdateUnlockState(bool state)
		{
			this._sourceRectangle = this._textureAsset.Frame(2, 1, state.ToInt(), 0, 0, 0);
			this._sourceRectangle.Inflate(-2, -2);
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x00573083 File Offset: 0x00571283
		public bool GetUnlockState(BestiaryUICollectionInfo providedInfo)
		{
			return providedInfo.UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0;
		}

		// Token: 0x04005133 RID: 20787
		private LocalizedText _text;

		// Token: 0x04005134 RID: 20788
		private Asset<Texture2D> _textureAsset;

		// Token: 0x04005135 RID: 20789
		private Rectangle _sourceRectangle;

		// Token: 0x04005136 RID: 20790
		private Func<bool> _unlockCondition;
	}
}
