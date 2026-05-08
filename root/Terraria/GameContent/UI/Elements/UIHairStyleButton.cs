using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003EC RID: 1004
	public class UIHairStyleButton : UIImageButton
	{
		// Token: 0x06002E84 RID: 11908 RVA: 0x005AB670 File Offset: 0x005A9870
		public UIHairStyleButton(Player player, int hairStyleId)
			: base(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanel", 1), null)
		{
			this._player = player;
			this.HairStyleId = hairStyleId;
			this.Width = StyleDimension.FromPixels(44f);
			this.Height = StyleDimension.FromPixels(44f);
			this._selectedBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1);
			this._hoveredBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelBorder", 1);
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x005AB6F6 File Offset: 0x005A98F6
		public void SkipRenderingContent(int timeInFrames)
		{
			this._framesToSkip = timeInFrames;
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x005AB700 File Offset: 0x005A9900
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
			Vector2 vector = new Vector2(-5f, -5f);
			base.DrawSelf(spriteBatch);
			if (this._player.hair == this.HairStyleId)
			{
				spriteBatch.Draw(this._selectedBorderTexture.Value, base.GetDimensions().Center() - this._selectedBorderTexture.Size() / 2f, Color.White);
			}
			if (this._hovered)
			{
				spriteBatch.Draw(this._hoveredBorderTexture.Value, base.GetDimensions().Center() - this._hoveredBorderTexture.Size() / 2f, Color.White);
			}
			if (this._framesToSkip > 0)
			{
				this._framesToSkip--;
				return;
			}
			int head = this._player.head;
			this._player.head = -1;
			int hair = this._player.hair;
			this._player.hair = this.HairStyleId;
			Main.PlayerRenderer.DrawPlayerHead(Main.Camera, this._player, base.GetDimensions().Center() + vector, 1f, 1f, default(Color));
			this._player.hair = hair;
			this._player.head = head;
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x005AB893 File Offset: 0x005A9A93
		public override void LeftMouseDown(UIMouseEvent evt)
		{
			this._player.hair = this.HairStyleId;
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			base.LeftMouseDown(evt);
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x005AB8C2 File Offset: 0x005A9AC2
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this._hovered = true;
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x005AB8D2 File Offset: 0x005A9AD2
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this._hovered = false;
		}

		// Token: 0x040055A2 RID: 21922
		private readonly Player _player;

		// Token: 0x040055A3 RID: 21923
		public readonly int HairStyleId;

		// Token: 0x040055A4 RID: 21924
		private readonly Asset<Texture2D> _selectedBorderTexture;

		// Token: 0x040055A5 RID: 21925
		private readonly Asset<Texture2D> _hoveredBorderTexture;

		// Token: 0x040055A6 RID: 21926
		private bool _hovered;

		// Token: 0x040055A7 RID: 21927
		private bool _soundedHover;

		// Token: 0x040055A8 RID: 21928
		private int _framesToSkip;
	}
}
