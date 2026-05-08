using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003ED RID: 1005
	public class UISlicedImage : UIElement
	{
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x005AB8E2 File Offset: 0x005A9AE2
		// (set) Token: 0x06002E8B RID: 11915 RVA: 0x005AB8EA File Offset: 0x005A9AEA
		public Color Color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x005AB8F4 File Offset: 0x005A9AF4
		public UISlicedImage(Asset<Texture2D> texture)
		{
			this._texture = texture;
			this.Width.Set((float)this._texture.Width(), 0f);
			this.Height.Set((float)this._texture.Height(), 0f);
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x005AB946 File Offset: 0x005A9B46
		public void SetImage(Asset<Texture2D> texture)
		{
			this._texture = texture;
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x005AB950 File Offset: 0x005A9B50
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			Utils.DrawSplicedPanel(spriteBatch, this._texture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, this._leftSliceDepth, this._rightSliceDepth, this._topSliceDepth, this._bottomSliceDepth, this._color);
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x005AB9AF File Offset: 0x005A9BAF
		public void SetSliceDepths(int top, int bottom, int left, int right)
		{
			this._leftSliceDepth = left;
			this._rightSliceDepth = right;
			this._topSliceDepth = top;
			this._bottomSliceDepth = bottom;
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x005AB9CE File Offset: 0x005A9BCE
		public void SetSliceDepths(int fluff)
		{
			this._leftSliceDepth = fluff;
			this._rightSliceDepth = fluff;
			this._topSliceDepth = fluff;
			this._bottomSliceDepth = fluff;
		}

		// Token: 0x040055A9 RID: 21929
		private Asset<Texture2D> _texture;

		// Token: 0x040055AA RID: 21930
		private Color _color;

		// Token: 0x040055AB RID: 21931
		private int _leftSliceDepth;

		// Token: 0x040055AC RID: 21932
		private int _rightSliceDepth;

		// Token: 0x040055AD RID: 21933
		private int _topSliceDepth;

		// Token: 0x040055AE RID: 21934
		private int _bottomSliceDepth;
	}
}
