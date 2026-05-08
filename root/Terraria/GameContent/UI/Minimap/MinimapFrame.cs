using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;

namespace Terraria.GameContent.UI.Minimap
{
	// Token: 0x020003C3 RID: 963
	public class MinimapFrame : IConfigKeyHolder
	{
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06002D2B RID: 11563 RVA: 0x005A29FA File Offset: 0x005A0BFA
		// (set) Token: 0x06002D2C RID: 11564 RVA: 0x005A2A02 File Offset: 0x005A0C02
		public string ConfigKey
		{
			[CompilerGenerated]
			get
			{
				return this.<ConfigKey>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ConfigKey>k__BackingField = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06002D2D RID: 11565 RVA: 0x005A2A0B File Offset: 0x005A0C0B
		// (set) Token: 0x06002D2E RID: 11566 RVA: 0x005A2A13 File Offset: 0x005A0C13
		public string NameKey
		{
			[CompilerGenerated]
			get
			{
				return this.<NameKey>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<NameKey>k__BackingField = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x005A2A1C File Offset: 0x005A0C1C
		// (set) Token: 0x06002D30 RID: 11568 RVA: 0x005A2A24 File Offset: 0x005A0C24
		public Vector2 MinimapPosition
		{
			[CompilerGenerated]
			get
			{
				return this.<MinimapPosition>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MinimapPosition>k__BackingField = value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x005A2A2D File Offset: 0x005A0C2D
		// (set) Token: 0x06002D32 RID: 11570 RVA: 0x005A2A40 File Offset: 0x005A0C40
		private Vector2 FramePosition
		{
			get
			{
				return this.MinimapPosition + this._frameOffset;
			}
			set
			{
				this.MinimapPosition = value - this._frameOffset;
			}
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x005A2A54 File Offset: 0x005A0C54
		public MinimapFrame(Asset<Texture2D> frameTexture, Vector2 frameOffset)
		{
			this._frameTexture = frameTexture;
			this._frameOffset = frameOffset;
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x005A2A6A File Offset: 0x005A0C6A
		public void SetResetButton(Asset<Texture2D> hoverTexture, Vector2 position)
		{
			this._resetButton = new MinimapFrame.Button(hoverTexture, position, delegate
			{
				this.ResetZoom();
			});
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x005A2A85 File Offset: 0x005A0C85
		private void ResetZoom()
		{
			Main.mapMinimapScale = 1.05f;
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x005A2A91 File Offset: 0x005A0C91
		public void SetZoomInButton(Asset<Texture2D> hoverTexture, Vector2 position)
		{
			this._zoomInButton = new MinimapFrame.Button(hoverTexture, position, delegate
			{
				this.ZoomInButton();
			});
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x005A2AAC File Offset: 0x005A0CAC
		private void ZoomInButton()
		{
			Main.mapMinimapScale *= 1.025f;
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x005A2ABE File Offset: 0x005A0CBE
		public void SetZoomOutButton(Asset<Texture2D> hoverTexture, Vector2 position)
		{
			this._zoomOutButton = new MinimapFrame.Button(hoverTexture, position, delegate
			{
				this.ZoomOutButton();
			});
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x005A2AD9 File Offset: 0x005A0CD9
		private void ZoomOutButton()
		{
			Main.mapMinimapScale *= 0.975f;
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x005A2AEC File Offset: 0x005A0CEC
		public void Update()
		{
			MinimapFrame.Button button = null;
			if (this._zoomInButton.IsHighlighted)
			{
				button = this._zoomInButton;
			}
			if (this._zoomOutButton.IsHighlighted)
			{
				button = this._zoomOutButton;
			}
			if (this._resetButton.IsHighlighted)
			{
				button = this._resetButton;
			}
			this._zoomInButton.IsHighlighted = false;
			this._zoomOutButton.IsHighlighted = false;
			this._resetButton.IsHighlighted = false;
			MinimapFrame.Button buttonUnderMouse = this.GetButtonUnderMouse();
			if (buttonUnderMouse != null && !PlayerInput.IgnoreMouseInterface && !Main.LocalPlayer.controlTorch)
			{
				buttonUnderMouse.IsHighlighted = true;
				Main.LocalPlayer.mouseInterface = true;
				if (button != buttonUnderMouse)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
				if (Main.mouseLeft)
				{
					buttonUnderMouse.Click();
					if (Main.mouseLeftRelease)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
				}
			}
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x005A2BCC File Offset: 0x005A0DCC
		public void DrawBackground(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle((int)this.MinimapPosition.X - 6, (int)this.MinimapPosition.Y - 6, 244, 244), Color.Black * Main.mapMinimapAlpha);
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x005A2C24 File Offset: 0x005A0E24
		public void DrawForeground(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(this._frameTexture.Value, this.FramePosition, Color.White);
			this._zoomInButton.Draw(spriteBatch, this.FramePosition);
			this._zoomOutButton.Draw(spriteBatch, this.FramePosition);
			this._resetButton.Draw(spriteBatch, this.FramePosition);
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x005A2C84 File Offset: 0x005A0E84
		private MinimapFrame.Button GetButtonUnderMouse()
		{
			Vector2 vector = new Vector2((float)Main.mouseX, (float)Main.mouseY);
			if (this._zoomInButton.IsTouchingPoint(vector, this.FramePosition))
			{
				return this._zoomInButton;
			}
			if (this._zoomOutButton.IsTouchingPoint(vector, this.FramePosition))
			{
				return this._zoomOutButton;
			}
			if (this._resetButton.IsTouchingPoint(vector, this.FramePosition))
			{
				return this._resetButton;
			}
			return null;
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x00009E46 File Offset: 0x00008046
		[Conditional("DEBUG")]
		private void ValidateState()
		{
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x005A2CF6 File Offset: 0x005A0EF6
		[CompilerGenerated]
		private void <SetResetButton>b__24_0()
		{
			this.ResetZoom();
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x005A2CFE File Offset: 0x005A0EFE
		[CompilerGenerated]
		private void <SetZoomInButton>b__26_0()
		{
			this.ZoomInButton();
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x005A2D06 File Offset: 0x005A0F06
		[CompilerGenerated]
		private void <SetZoomOutButton>b__28_0()
		{
			this.ZoomOutButton();
		}

		// Token: 0x0400549F RID: 21663
		private const float DEFAULT_ZOOM = 1.05f;

		// Token: 0x040054A0 RID: 21664
		private const float ZOOM_OUT_MULTIPLIER = 0.975f;

		// Token: 0x040054A1 RID: 21665
		private const float ZOOM_IN_MULTIPLIER = 1.025f;

		// Token: 0x040054A2 RID: 21666
		[CompilerGenerated]
		private string <ConfigKey>k__BackingField;

		// Token: 0x040054A3 RID: 21667
		[CompilerGenerated]
		private string <NameKey>k__BackingField;

		// Token: 0x040054A4 RID: 21668
		[CompilerGenerated]
		private Vector2 <MinimapPosition>k__BackingField;

		// Token: 0x040054A5 RID: 21669
		private readonly Asset<Texture2D> _frameTexture;

		// Token: 0x040054A6 RID: 21670
		private readonly Vector2 _frameOffset;

		// Token: 0x040054A7 RID: 21671
		private MinimapFrame.Button _resetButton;

		// Token: 0x040054A8 RID: 21672
		private MinimapFrame.Button _zoomInButton;

		// Token: 0x040054A9 RID: 21673
		private MinimapFrame.Button _zoomOutButton;

		// Token: 0x02000923 RID: 2339
		private class Button
		{
			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x060047EC RID: 18412 RVA: 0x006CC6A7 File Offset: 0x006CA8A7
			private Vector2 Size
			{
				get
				{
					return new Vector2((float)this._hoverTexture.Width(), (float)this._hoverTexture.Height());
				}
			}

			// Token: 0x060047ED RID: 18413 RVA: 0x006CC6C6 File Offset: 0x006CA8C6
			public Button(Asset<Texture2D> hoverTexture, Vector2 position, Action mouseDownCallback)
			{
				this._position = position;
				this._hoverTexture = hoverTexture;
				this._onMouseDown = mouseDownCallback;
			}

			// Token: 0x060047EE RID: 18414 RVA: 0x006CC6E3 File Offset: 0x006CA8E3
			public void Click()
			{
				this._onMouseDown();
			}

			// Token: 0x060047EF RID: 18415 RVA: 0x006CC6F0 File Offset: 0x006CA8F0
			public void Draw(SpriteBatch spriteBatch, Vector2 parentPosition)
			{
				if (!this.IsHighlighted)
				{
					return;
				}
				spriteBatch.Draw(this._hoverTexture.Value, this._position + parentPosition, Color.White);
			}

			// Token: 0x060047F0 RID: 18416 RVA: 0x006CC720 File Offset: 0x006CA920
			public bool IsTouchingPoint(Vector2 testPoint, Vector2 parentPosition)
			{
				Vector2 vector = this._position + parentPosition + this.Size * 0.5f;
				Vector2 vector2 = Vector2.Max(this.Size, new Vector2(22f, 22f)) * 0.5f;
				Vector2 vector3 = testPoint - vector;
				return Math.Abs(vector3.X) < vector2.X && Math.Abs(vector3.Y) < vector2.Y;
			}

			// Token: 0x040074EB RID: 29931
			public bool IsHighlighted;

			// Token: 0x040074EC RID: 29932
			private readonly Vector2 _position;

			// Token: 0x040074ED RID: 29933
			private readonly Asset<Texture2D> _hoverTexture;

			// Token: 0x040074EE RID: 29934
			private readonly Action _onMouseDown;
		}
	}
}
