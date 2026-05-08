using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003D1 RID: 977
	public class UISelectableTextPanel<T> : UITextPanel<T>
	{
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06002DAB RID: 11691 RVA: 0x005A52B2 File Offset: 0x005A34B2
		// (set) Token: 0x06002DAC RID: 11692 RVA: 0x005A52BA File Offset: 0x005A34BA
		public Func<UISelectableTextPanel<T>, bool> IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				this._isSelected = value;
			}
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x005A52C3 File Offset: 0x005A34C3
		public UISelectableTextPanel(T text, float textScale = 1f, bool large = false)
			: base(text, textScale, large)
		{
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale", 1);
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x005A52FC File Offset: 0x005A34FC
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._drawPanel)
			{
				CalculatedStyle dimensions = base.GetDimensions();
				int num = 4;
				int num2 = 10;
				int num3 = 10;
				Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, num2, num2, num3, num3, Color.Lerp(Color.Black, this._color, 0.8f) * 0.5f);
				if (this.IsSelected != null && this.IsSelected(this))
				{
					Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X + num, (int)dimensions.Y + num, (int)dimensions.Width - num * 2, (int)dimensions.Height - num * 2, num2, num2, num3, num3, Color.Lerp(this._color, Color.White, 0.7f) * 0.5f);
				}
				if (base.IsMouseHovering)
				{
					Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, num2, num2, num3, num3, Color.White);
				}
			}
			base.DrawText(spriteBatch);
		}

		// Token: 0x040054EF RID: 21743
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x040054F0 RID: 21744
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x040054F1 RID: 21745
		private Func<UISelectableTextPanel<T>, bool> _isSelected;
	}
}
