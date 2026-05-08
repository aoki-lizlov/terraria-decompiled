using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000404 RID: 1028
	public class UIScrollbar : UIElement
	{
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x005B18F1 File Offset: 0x005AFAF1
		// (set) Token: 0x06002F2E RID: 12078 RVA: 0x005B18F9 File Offset: 0x005AFAF9
		public float ViewPosition
		{
			get
			{
				return this._viewPosition;
			}
			set
			{
				this._viewPosition = MathHelper.Clamp(value, 0f, this._maxViewSize - this._viewSize);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x005B1919 File Offset: 0x005AFB19
		public bool CanScroll
		{
			get
			{
				return this._maxViewSize != this._viewSize;
			}
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x005B192C File Offset: 0x005AFB2C
		public void GoToBottom()
		{
			this.ViewPosition = this._maxViewSize - this._viewSize;
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x005B1944 File Offset: 0x005AFB44
		public UIScrollbar(UIScrollbar.ColorTheme theme = UIScrollbar.ColorTheme.Blue)
		{
			this._theme = theme;
			this.Width.Set(20f, 0f);
			this.MaxWidth.Set(20f, 0f);
			string text = "Images/UI/Scrollbar";
			if (this._theme == UIScrollbar.ColorTheme.Cyan)
			{
				text = "Images/UI/Scrollbar2";
			}
			this._texture = Main.Assets.Request<Texture2D>(text, 1);
			this._innerTexture = Main.Assets.Request<Texture2D>("Images/UI/ScrollbarInner", 1);
			this.PaddingTop = 5f;
			this.PaddingBottom = 5f;
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x005B19F1 File Offset: 0x005AFBF1
		public void SetView(float viewSize, float maxViewSize)
		{
			viewSize = MathHelper.Clamp(viewSize, 0f, maxViewSize);
			this._viewPosition = MathHelper.Clamp(this._viewPosition, 0f, maxViewSize - viewSize);
			this._viewSize = viewSize;
			this._maxViewSize = maxViewSize;
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x005B18F1 File Offset: 0x005AFAF1
		public float GetValue()
		{
			return this._viewPosition;
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x005B1A28 File Offset: 0x005AFC28
		private Rectangle GetHandleRectangle()
		{
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			if (this._maxViewSize == 0f && this._viewSize == 0f)
			{
				this._viewSize = 1f;
				this._maxViewSize = 1f;
			}
			return new Rectangle((int)innerDimensions.X, (int)(innerDimensions.Y + innerDimensions.Height * (this._viewPosition / this._maxViewSize)) - 3, 20, (int)(innerDimensions.Height * (this._viewSize / this._maxViewSize)) + 7);
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x005B1AB0 File Offset: 0x005AFCB0
		private void DrawBar(SpriteBatch spriteBatch, Texture2D texture, Rectangle dimensions, Color color)
		{
			spriteBatch.Draw(texture, new Rectangle(dimensions.X, dimensions.Y - 6, dimensions.Width, 6), new Rectangle?(new Rectangle(0, 0, texture.Width, 6)), color);
			spriteBatch.Draw(texture, new Rectangle(dimensions.X, dimensions.Y, dimensions.Width, dimensions.Height), new Rectangle?(new Rectangle(0, 6, texture.Width, 4)), color);
			spriteBatch.Draw(texture, new Rectangle(dimensions.X, dimensions.Y + dimensions.Height, dimensions.Width, 6), new Rectangle?(new Rectangle(0, texture.Height - 6, texture.Width, 6)), color);
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x005B1B70 File Offset: 0x005AFD70
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this.AutoHide && !this.CanScroll)
			{
				return;
			}
			CalculatedStyle dimensions = base.GetDimensions();
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			if (this._isDragging)
			{
				float num = UserInterface.ActiveInstance.MousePosition.Y - innerDimensions.Y - this._dragYOffset;
				this._viewPosition = MathHelper.Clamp(num / innerDimensions.Height * this._maxViewSize, 0f, this._maxViewSize - this._viewSize);
			}
			Rectangle handleRectangle = this.GetHandleRectangle();
			Vector2 mousePosition = UserInterface.ActiveInstance.MousePosition;
			bool isHoveringOverHandle = this._isHoveringOverHandle;
			this._isHoveringOverHandle = handleRectangle.Contains(new Point((int)mousePosition.X, (int)mousePosition.Y));
			if (!isHoveringOverHandle && this._isHoveringOverHandle && FocusHelper.AllowUIInputs)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}
			this.DrawBar(spriteBatch, this._texture.Value, dimensions.ToRectangle(), Color.White);
			this.DrawBar(spriteBatch, this._innerTexture.Value, handleRectangle, Color.White * ((this._isDragging || this._isHoveringOverHandle) ? 1f : 0.85f));
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x005B1CA8 File Offset: 0x005AFEA8
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			base.LeftMouseDown(evt);
			if (evt.Target == this)
			{
				Rectangle handleRectangle = this.GetHandleRectangle();
				if (handleRectangle.Contains(new Point((int)evt.MousePosition.X, (int)evt.MousePosition.Y)))
				{
					this._isDragging = true;
					this._dragYOffset = evt.MousePosition.Y - (float)handleRectangle.Y;
					return;
				}
				CalculatedStyle innerDimensions = base.GetInnerDimensions();
				float num = UserInterface.ActiveInstance.MousePosition.Y - innerDimensions.Y - (float)(handleRectangle.Height >> 1);
				this._viewPosition = MathHelper.Clamp(num / innerDimensions.Height * this._maxViewSize, 0f, this._maxViewSize - this._viewSize);
			}
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x005B1D6A File Offset: 0x005AFF6A
		public override void LeftMouseUp(UIMouseEvent evt)
		{
			base.LeftMouseUp(evt);
			this._isDragging = false;
		}

		// Token: 0x04005644 RID: 22084
		private float _viewPosition;

		// Token: 0x04005645 RID: 22085
		private float _viewSize = 1f;

		// Token: 0x04005646 RID: 22086
		private float _maxViewSize = 20f;

		// Token: 0x04005647 RID: 22087
		private bool _isDragging;

		// Token: 0x04005648 RID: 22088
		private bool _isHoveringOverHandle;

		// Token: 0x04005649 RID: 22089
		private float _dragYOffset;

		// Token: 0x0400564A RID: 22090
		public bool AutoHide;

		// Token: 0x0400564B RID: 22091
		private Asset<Texture2D> _texture;

		// Token: 0x0400564C RID: 22092
		private Asset<Texture2D> _innerTexture;

		// Token: 0x0400564D RID: 22093
		private UIScrollbar.ColorTheme _theme;

		// Token: 0x02000937 RID: 2359
		public enum ColorTheme
		{
			// Token: 0x04007524 RID: 29988
			Blue,
			// Token: 0x04007525 RID: 29989
			Cyan
		}
	}
}
