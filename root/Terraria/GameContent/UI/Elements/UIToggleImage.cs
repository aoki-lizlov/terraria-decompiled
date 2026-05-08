using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000407 RID: 1031
	public class UIToggleImage : UIElement
	{
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x005B2602 File Offset: 0x005B0802
		public bool IsOn
		{
			get
			{
				return this._isOn;
			}
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x005B260C File Offset: 0x005B080C
		public UIToggleImage(Asset<Texture2D> texture, int width, int height, Point onTextureOffset, Point offTextureOffset)
		{
			this._onTexture = texture;
			this._offTexture = texture;
			this._offTextureOffset = offTextureOffset;
			this._onTextureOffset = onTextureOffset;
			this._drawWidth = width;
			this._drawHeight = height;
			this.Width.Set((float)width, 0f);
			this.Height.Set((float)height, 0f);
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x005B2688 File Offset: 0x005B0888
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Texture2D texture2D;
			Point point;
			if (this._isOn)
			{
				texture2D = this._onTexture.Value;
				point = this._onTextureOffset;
			}
			else
			{
				texture2D = this._offTexture.Value;
				point = this._offTextureOffset;
			}
			Color color = (base.IsMouseHovering ? Color.White : Color.Silver);
			spriteBatch.Draw(texture2D, new Rectangle((int)dimensions.X, (int)dimensions.Y, this._drawWidth, this._drawHeight), new Rectangle?(new Rectangle(point.X, point.Y, this._drawWidth, this._drawHeight)), color);
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x005B272A File Offset: 0x005B092A
		public override void LeftClick(UIMouseEvent evt)
		{
			this.Toggle();
			base.LeftClick(evt);
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x005B2739 File Offset: 0x005B0939
		public void SetState(bool value)
		{
			this._isOn = value;
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x005B2742 File Offset: 0x005B0942
		public void Toggle()
		{
			this._isOn = !this._isOn;
		}

		// Token: 0x04005665 RID: 22117
		private Asset<Texture2D> _onTexture;

		// Token: 0x04005666 RID: 22118
		private Asset<Texture2D> _offTexture;

		// Token: 0x04005667 RID: 22119
		private int _drawWidth;

		// Token: 0x04005668 RID: 22120
		private int _drawHeight;

		// Token: 0x04005669 RID: 22121
		private Point _onTextureOffset = Point.Zero;

		// Token: 0x0400566A RID: 22122
		private Point _offTextureOffset = Point.Zero;

		// Token: 0x0400566B RID: 22123
		private bool _isOn;
	}
}
