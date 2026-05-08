using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003EB RID: 1003
	public class UIDifficultyButton : UIElement
	{
		// Token: 0x06002E7F RID: 11903 RVA: 0x005AB3E4 File Offset: 0x005A95E4
		public UIDifficultyButton(Player player, LocalizedText title, LocalizedText description, byte difficulty, Color color)
		{
			this._player = player;
			this._difficulty = difficulty;
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(110f);
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/PanelGrayscale", 1);
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1);
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
			this._color = color;
			UIText uitext = new UIText(title, 0.9f, false)
			{
				HAlign = 0.5f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(-10f, 1f),
				Top = StyleDimension.FromPixels(5f)
			};
			base.Append(uitext);
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x005AB4C0 File Offset: 0x005A96C0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (this._hovered)
			{
				if (!this._soundedHover)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
				this._soundedHover = true;
			}
			else
			{
				this._soundedHover = false;
			}
			CalculatedStyle dimensions = base.GetDimensions();
			int num = 7;
			if (dimensions.Height < 30f)
			{
				num = 5;
			}
			int num2 = 10;
			int num3 = 10;
			bool flag = this._difficulty == this._player.difficulty;
			Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, num2, num2, num3, num3, Color.Lerp(Color.Black, this._color, 0.8f) * 0.5f);
			if (flag)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X + num, (int)dimensions.Y + num - 2, (int)dimensions.Width - num * 2, (int)dimensions.Height - num * 2, num2, num2, num3, num3, Color.Lerp(this._color, Color.White, 0.7f) * 0.5f);
			}
			if (this._hovered)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, num2, num2, num3, num3, Color.White);
			}
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x005AB621 File Offset: 0x005A9821
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			this._player.difficulty = this._difficulty;
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.LeftMouseDown(evt);
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x005AB650 File Offset: 0x005A9850
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x005AB660 File Offset: 0x005A9860
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x0400559A RID: 21914
		private readonly Player _player;

		// Token: 0x0400559B RID: 21915
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x0400559C RID: 21916
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x0400559D RID: 21917
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x0400559E RID: 21918
		private readonly byte _difficulty;

		// Token: 0x0400559F RID: 21919
		private readonly Color _color;

		// Token: 0x040055A0 RID: 21920
		private bool _hovered;

		// Token: 0x040055A1 RID: 21921
		private bool _soundedHover;
	}
}
