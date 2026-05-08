using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003E5 RID: 997
	public class EmoteButton : UIElement
	{
		// Token: 0x06002E4F RID: 11855 RVA: 0x005AA184 File Offset: 0x005A8384
		public EmoteButton(int emoteIndex)
		{
			this._texture = Main.Assets.Request<Texture2D>("Images/Extra_" + 48, 1);
			this._textureBorder = Main.Assets.Request<Texture2D>("Images/UI/EmoteBubbleBorder", 1);
			this._emoteIndex = emoteIndex;
			Rectangle frame = this.GetFrame();
			this.Width.Set((float)frame.Width, 0f);
			this.Height.Set((float)frame.Height, 0f);
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x005AA20C File Offset: 0x005A840C
		private Rectangle GetFrame()
		{
			int num = ((this._frameCounter >= 10) ? 1 : 0);
			return this._texture.Frame(8, EmoteBubble.EMOTE_SHEET_VERTICAL_FRAMES, this._emoteIndex % 4 * 2 + num, this._emoteIndex / 4 + 1, 0, 0);
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x005AA254 File Offset: 0x005A8454
		private void UpdateFrame()
		{
			int num = this._frameCounter + 1;
			this._frameCounter = num;
			if (num >= 20)
			{
				this._frameCounter = 0;
			}
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x005AA27D File Offset: 0x005A847D
		public override void Update(GameTime gameTime)
		{
			this.UpdateFrame();
			base.Update(gameTime);
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x005AA28C File Offset: 0x005A848C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Vector2 vector = dimensions.Position() + new Vector2(dimensions.Width, dimensions.Height) / 2f;
			Rectangle frame = this.GetFrame();
			Rectangle rectangle = frame;
			rectangle.X = this._texture.Width() / 8;
			rectangle.Y = 0;
			Vector2 vector2 = frame.Size() / 2f;
			Color white = Color.White;
			Color color = Color.Black;
			if (this._hovered)
			{
				color = Main.OurFavoriteColor;
			}
			spriteBatch.Draw(this._texture.Value, vector, new Rectangle?(rectangle), white, 0f, vector2, 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(this._texture.Value, vector, new Rectangle?(frame), white, 0f, vector2, 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(this._textureBorder.Value, vector - Vector2.One * 2f, null, color, 0f, vector2, 1f, SpriteEffects.None, 0f);
			if (this._hovered)
			{
				string name = EmoteID.Search.GetName(this._emoteIndex);
				string text = "/" + Language.GetTextValue("EmojiName." + name);
				Main.instance.MouseText(text, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x005AA402 File Offset: 0x005A8602
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._hovered = true;
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x005AA427 File Offset: 0x005A8627
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x005AA437 File Offset: 0x005A8637
		public override void LeftClick(UIMouseEvent evt)
		{
			base.LeftClick(evt);
			EmoteBubble.MakeLocalPlayerEmote(this._emoteIndex);
			IngameFancyUI.Close(false);
		}

		// Token: 0x04005560 RID: 21856
		private Asset<Texture2D> _texture;

		// Token: 0x04005561 RID: 21857
		private Asset<Texture2D> _textureBorder;

		// Token: 0x04005562 RID: 21858
		private int _emoteIndex;

		// Token: 0x04005563 RID: 21859
		private bool _hovered;

		// Token: 0x04005564 RID: 21860
		private int _frameCounter;
	}
}
