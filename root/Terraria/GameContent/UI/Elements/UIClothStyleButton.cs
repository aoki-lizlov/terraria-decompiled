using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003EA RID: 1002
	public class UIClothStyleButton : UIElement
	{
		// Token: 0x06002E79 RID: 11897 RVA: 0x005AB144 File Offset: 0x005A9344
		public UIClothStyleButton(Player player, int clothStyleId, Action prepareAction = null)
		{
			this._player = player;
			this.ClothStyleId = clothStyleId;
			this.PrepareAction = prepareAction;
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(80f);
			this._BasePanelTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel", 1);
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1);
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
			this._char = new UICharacter(this._player, false, false, 1f, false)
			{
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			base.Append(this._char);
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x005AB20C File Offset: 0x005A940C
		public override void Draw(SpriteBatch spriteBatch)
		{
			this._realSkinVariant = this._player.skinVariant;
			this._player.skinVariant = this.ClothStyleId;
			int hair = this._player.hair;
			if (this.PrepareAction != null)
			{
				this.PrepareAction();
			}
			this._player.PlayerFrame();
			base.Draw(spriteBatch);
			this._player.skinVariant = this._realSkinVariant;
			this._player.hair = hair;
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x005AB28C File Offset: 0x005A948C
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
			Utils.DrawSplicedPanel(spriteBatch, this._BasePanelTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.White * 0.5f);
			if (this._realSkinVariant == this.ClothStyleId)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._selectedBorderTexture.Value, (int)dimensions.X + 3, (int)dimensions.Y + 3, (int)dimensions.Width - 6, (int)dimensions.Height - 6, 10, 10, 10, 10, Color.White);
			}
			if (this._hovered)
			{
				Utils.DrawSplicedPanel(spriteBatch, this._hoveredBorderTexture.Value, (int)dimensions.X, (int)dimensions.Y, (int)dimensions.Width, (int)dimensions.Height, 10, 10, 10, 10, Color.White);
			}
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x005AB11B File Offset: 0x005A931B
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			base.LeftMouseDown(evt);
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x005AB3AB File Offset: 0x005A95AB
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
			this._char.SetAnimated(true);
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x005AB3C7 File Offset: 0x005A95C7
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
			this._char.SetAnimated(false);
		}

		// Token: 0x04005590 RID: 21904
		private readonly Player _player;

		// Token: 0x04005591 RID: 21905
		public readonly int ClothStyleId;

		// Token: 0x04005592 RID: 21906
		private readonly Asset<Texture2D> _BasePanelTexture;

		// Token: 0x04005593 RID: 21907
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x04005594 RID: 21908
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x04005595 RID: 21909
		private readonly UICharacter _char;

		// Token: 0x04005596 RID: 21910
		private bool _hovered;

		// Token: 0x04005597 RID: 21911
		private bool _soundedHover;

		// Token: 0x04005598 RID: 21912
		private int _realSkinVariant;

		// Token: 0x04005599 RID: 21913
		private Action PrepareAction;
	}
}
