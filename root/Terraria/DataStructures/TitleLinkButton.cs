using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.Localization;

namespace Terraria.DataStructures
{
	// Token: 0x0200058B RID: 1419
	public class TitleLinkButton
	{
		// Token: 0x0600381D RID: 14365 RVA: 0x006309C8 File Offset: 0x0062EBC8
		public void Draw(SpriteBatch spriteBatch, Vector2 anchorPosition)
		{
			Rectangle rectangle = this.Image.Frame(1, 1, 0, 0, 0, 0);
			if (this.FrameWhenNotSelected != null)
			{
				rectangle = this.FrameWhenNotSelected.Value;
			}
			Vector2 vector = rectangle.Size();
			Vector2 vector2 = anchorPosition - vector / 2f;
			bool flag = false;
			if (Main.MouseScreen.Between(vector2, vector2 + vector))
			{
				Main.LocalPlayer.mouseInterface = true;
				flag = true;
				this.DrawTooltip();
				this.TryClicking();
			}
			Rectangle? rectangle2 = (flag ? this.FrameWehnSelected : this.FrameWhenNotSelected);
			Rectangle rectangle3 = this.Image.Frame(1, 1, 0, 0, 0, 0);
			if (rectangle2 != null)
			{
				rectangle3 = rectangle2.Value;
			}
			Texture2D value = this.Image.Value;
			spriteBatch.Draw(value, anchorPosition, new Rectangle?(rectangle3), Color.White, 0f, rectangle3.Size() / 2f, 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x00630AC4 File Offset: 0x0062ECC4
		private void DrawTooltip()
		{
			Item fakeItem = TitleLinkButton._fakeItem;
			fakeItem.SetDefaults(0, null);
			string textValue = Language.GetTextValue(this.TooltipTextKey);
			fakeItem.SetNameOverride(textValue);
			fakeItem.type = 1;
			fakeItem.scale = 0f;
			fakeItem.rare = 8;
			fakeItem.value = -1;
			Main.HoverItem = TitleLinkButton._fakeItem;
			Main.instance.MouseText("", 0, 0, -1, -1, -1, -1, 0);
			Main.mouseText = true;
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x00630B36 File Offset: 0x0062ED36
		private void TryClicking()
		{
			if (PlayerInput.IgnoreMouseInterface)
			{
				return;
			}
			if (!Main.mouseLeft || !Main.mouseLeftRelease)
			{
				return;
			}
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.mouseLeftRelease = false;
			this.OpenLink();
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x00630B70 File Offset: 0x0062ED70
		private void OpenLink()
		{
			try
			{
				Platform.Get<IPathService>().OpenURL(this.LinkUrl);
			}
			catch
			{
				Console.WriteLine("Failed to open link?!");
			}
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x0000357B File Offset: 0x0000177B
		public TitleLinkButton()
		{
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x00630BAC File Offset: 0x0062EDAC
		// Note: this type is marked as 'beforefieldinit'.
		static TitleLinkButton()
		{
		}

		// Token: 0x04005C4C RID: 23628
		private static Item _fakeItem = new Item();

		// Token: 0x04005C4D RID: 23629
		public string TooltipTextKey;

		// Token: 0x04005C4E RID: 23630
		public string LinkUrl;

		// Token: 0x04005C4F RID: 23631
		public Asset<Texture2D> Image;

		// Token: 0x04005C50 RID: 23632
		public Rectangle? FrameWhenNotSelected;

		// Token: 0x04005C51 RID: 23633
		public Rectangle? FrameWehnSelected;
	}
}
