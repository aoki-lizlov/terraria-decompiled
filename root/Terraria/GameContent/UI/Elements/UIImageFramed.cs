using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000400 RID: 1024
	public class UIImageFramed : UIElement, IColorable
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06002F0E RID: 12046 RVA: 0x005B0F8A File Offset: 0x005AF18A
		// (set) Token: 0x06002F0F RID: 12047 RVA: 0x005B0F92 File Offset: 0x005AF192
		public Color Color
		{
			[CompilerGenerated]
			get
			{
				return this.<Color>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Color>k__BackingField = value;
			}
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x005B0F9C File Offset: 0x005AF19C
		public UIImageFramed(Asset<Texture2D> texture, Rectangle frame)
		{
			this._texture = texture;
			this._frame = frame;
			this.Width.Set((float)this._frame.Width, 0f);
			this.Height.Set((float)this._frame.Height, 0f);
			this.Color = Color.White;
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x005B1000 File Offset: 0x005AF200
		public void SetImage(Asset<Texture2D> texture, Rectangle frame)
		{
			this._texture = texture;
			this._frame = frame;
			this.Width.Set((float)this._frame.Width, 0f);
			this.Height.Set((float)this._frame.Height, 0f);
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x005B1054 File Offset: 0x005AF254
		public void SetFrame(Rectangle frame)
		{
			this._frame = frame;
			this.Width.Set((float)this._frame.Width, 0f);
			this.Height.Set((float)this._frame.Height, 0f);
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x005B10A0 File Offset: 0x005AF2A0
		public void SetFrame(int frameCountHorizontal, int frameCountVertical, int frameX, int frameY, int sizeOffsetX, int sizeOffsetY)
		{
			this.SetFrame(this._texture.Frame(frameCountHorizontal, frameCountVertical, frameX, frameY, 0, 0).OffsetSize(sizeOffsetX, sizeOffsetY));
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x005B10C4 File Offset: 0x005AF2C4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			spriteBatch.Draw(this._texture.Value, dimensions.Position(), new Rectangle?(this._frame), this.Color);
		}

		// Token: 0x04005635 RID: 22069
		private Asset<Texture2D> _texture;

		// Token: 0x04005636 RID: 22070
		private Rectangle _frame;

		// Token: 0x04005637 RID: 22071
		[CompilerGenerated]
		private Color <Color>k__BackingField;
	}
}
