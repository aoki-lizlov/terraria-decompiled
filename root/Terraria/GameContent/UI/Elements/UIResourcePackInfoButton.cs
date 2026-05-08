using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.IO;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003D0 RID: 976
	public class UIResourcePackInfoButton<T> : UITextPanel<T>
	{
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x005A51AE File Offset: 0x005A33AE
		// (set) Token: 0x06002DA8 RID: 11688 RVA: 0x005A51B6 File Offset: 0x005A33B6
		public ResourcePack ResourcePack
		{
			get
			{
				return this._resourcePack;
			}
			set
			{
				this._resourcePack = value;
			}
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x005A51BF File Offset: 0x005A33BF
		public UIResourcePackInfoButton(T text, float textScale = 1f, bool large = false)
			: base(text, textScale, large)
		{
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale", 1);
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x005A51F8 File Offset: 0x005A33F8
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._drawPanel)
			{
				CalculatedStyle dimensions = base.GetDimensions();
				int num = 10;
				int num2 = 10;
				Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, num, num, num2, num2, Color.Lerp(Color.Black, this._color, 0.8f) * 0.5f);
				if (base.IsMouseHovering)
				{
					Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, num, num, num2, num2, Color.White);
				}
			}
			base.DrawText(spriteBatch);
		}

		// Token: 0x040054EC RID: 21740
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x040054ED RID: 21741
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x040054EE RID: 21742
		private ResourcePack _resourcePack;
	}
}
