using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.Bestiary;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003D9 RID: 985
	public class UIBestiaryEntryIcon : UIElement
	{
		// Token: 0x06002DEB RID: 11755 RVA: 0x005A74AC File Offset: 0x005A56AC
		public UIBestiaryEntryIcon(BestiaryEntry entry, bool isPortrait)
		{
			this._entry = entry;
			this.IgnoresMouseInteraction = true;
			this.OverrideSamplerState = Main.DefaultSamplerState;
			this.UseImmediateMode = true;
			this.Width.Set(0f, 1f);
			this.Height.Set(0f, 1f);
			this._notUnlockedTexture = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Locked", 1);
			this._isPortrait = isPortrait;
			this._collectionInfo = this._entry.UIInfoProvider.GetEntryUICollectionInfo();
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x005A753C File Offset: 0x005A573C
		public override void Update(GameTime gameTime)
		{
			this._collectionInfo = this._entry.UIInfoProvider.GetEntryUICollectionInfo();
			CalculatedStyle dimensions = base.GetDimensions();
			bool flag = base.IsMouseHovering || this.ForceHover;
			this._entry.Icon.Update(this._collectionInfo, dimensions.ToRectangle(), new EntryIconDrawSettings
			{
				iconbox = dimensions.ToRectangle(),
				IsPortrait = this._isPortrait,
				IsHovered = flag
			});
			base.Update(gameTime);
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x005A75C8 File Offset: 0x005A57C8
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			bool unlockState = this._entry.Icon.GetUnlockState(this._collectionInfo);
			bool flag = base.IsMouseHovering || this.ForceHover;
			if (unlockState)
			{
				this._entry.Icon.Draw(this._collectionInfo, spriteBatch, new EntryIconDrawSettings
				{
					iconbox = dimensions.ToRectangle(),
					IsPortrait = this._isPortrait,
					IsHovered = flag
				});
				return;
			}
			Texture2D value = this._notUnlockedTexture.Value;
			spriteBatch.Draw(value, dimensions.Center(), null, Color.White * 0.15f, 0f, value.Size() / 2f, 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x005A769D File Offset: 0x005A589D
		public string GetHoverText()
		{
			return this._entry.Icon.GetHoverText(this._collectionInfo);
		}

		// Token: 0x04005512 RID: 21778
		private BestiaryEntry _entry;

		// Token: 0x04005513 RID: 21779
		private Asset<Texture2D> _notUnlockedTexture;

		// Token: 0x04005514 RID: 21780
		private bool _isPortrait;

		// Token: 0x04005515 RID: 21781
		public bool ForceHover;

		// Token: 0x04005516 RID: 21782
		private BestiaryUICollectionInfo _collectionInfo;
	}
}
