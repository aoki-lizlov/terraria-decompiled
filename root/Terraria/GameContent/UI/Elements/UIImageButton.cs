using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003FF RID: 1023
	public class UIImageButton : UIElement
	{
		// Token: 0x06002F07 RID: 12039 RVA: 0x005B0D7C File Offset: 0x005AEF7C
		public UIImageButton(Asset<Texture2D> texture, Rectangle? frame = null)
		{
			this._texture = texture;
			this._frame = frame;
			this.Width.Set((float)((frame != null) ? frame.Value.Width : this._texture.Width()), 0f);
			this.Height.Set((float)((frame != null) ? frame.Value.Height : this._texture.Height()), 0f);
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x005B0E2F File Offset: 0x005AF02F
		public void SetHoverImage(Asset<Texture2D> texture, Rectangle? frame = null)
		{
			this._borderTexture = texture;
			this._borderFrame = frame;
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x005B0E40 File Offset: 0x005AF040
		public void SetImage(Asset<Texture2D> texture, Rectangle? frame = null)
		{
			this._texture = texture;
			this.Width.Set((float)((this._frame != null) ? this._frame.Value.Width : this._texture.Width()), 0f);
			this.Height.Set((float)((this._frame != null) ? this._frame.Value.Height : this._texture.Height()), 0f);
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x005B0ECC File Offset: 0x005AF0CC
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this._texture.Value, dimensions.Position(), this._frame, this.Color * (base.IsMouseHovering ? this._visibilityActive : this._visibilityInactive));
			if (this._borderTexture != null && base.IsMouseHovering)
			{
				spriteBatch.Draw(this._borderTexture.Value, dimensions.Position(), this._borderFrame, this.BorderColor);
			}
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x005A839B File Offset: 0x005A659B
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x005B0F53 File Offset: 0x005AF153
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x005B0F5C File Offset: 0x005AF15C
		public void SetVisibility(float whenActive, float whenInactive)
		{
			this._visibilityActive = MathHelper.Clamp(whenActive, 0f, 1f);
			this._visibilityInactive = MathHelper.Clamp(whenInactive, 0f, 1f);
		}

		// Token: 0x0400562D RID: 22061
		private Asset<Texture2D> _texture;

		// Token: 0x0400562E RID: 22062
		protected float _visibilityActive = 1f;

		// Token: 0x0400562F RID: 22063
		protected float _visibilityInactive = 0.4f;

		// Token: 0x04005630 RID: 22064
		private Asset<Texture2D> _borderTexture;

		// Token: 0x04005631 RID: 22065
		private Rectangle? _frame;

		// Token: 0x04005632 RID: 22066
		private Rectangle? _borderFrame;

		// Token: 0x04005633 RID: 22067
		public Color Color = Color.White;

		// Token: 0x04005634 RID: 22068
		public Color BorderColor = Color.White;
	}
}
