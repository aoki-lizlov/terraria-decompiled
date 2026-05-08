using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x02000433 RID: 1075
	public class HorizonHelper
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x005BB005 File Offset: 0x005B9205
		public bool SunVisibilityEnabled
		{
			get
			{
				return this._targetUpToDate;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x005BB00D File Offset: 0x005B920D
		public Texture2D SunVisibilityPixelTexture
		{
			get
			{
				return this._pixelTarget;
			}
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x005BB018 File Offset: 0x005B9218
		public HorizonHelper()
		{
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x005BB068 File Offset: 0x005B9268
		public void UpdateSunVisibility(RenderTarget2D bigTarget)
		{
			this._targetUpToDate = false;
			if (!Main.ForegroundSunlightEffects || bigTarget == null)
			{
				return;
			}
			TimeLogger.StartTimestamp startTimestamp = TimeLogger.Start();
			GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
			if (this._tinyTarget == null || this._tinyTarget.IsContentLost)
			{
				this._tinyTarget = new RenderTarget2D(graphicsDevice, this.SmallTextureSize, this.SmallTextureSize, true, SurfaceFormat.Alpha8, DepthFormat.None);
			}
			if (this._pixelTarget == null || this._pixelTarget.IsContentLost)
			{
				this._pixelTarget = new RenderTarget2D(graphicsDevice, 1, 1, false, SurfaceFormat.Alpha8, DepthFormat.None);
			}
			Rectangle rectangle = Utils.CenteredRectangle(Main.ReverseGravitySupport(Main.LastCelestialBodyPosition * Main.ScreenSize.ToVector2(), 0f), new Vector2((float)this.SampleAreaSize) * Main.BackgroundViewMatrix.RenderZoom);
			if (HorizonHelper.DebugSunVisibility)
			{
				this.Test_DrawSmallTarget(bigTarget, rectangle);
			}
			graphicsDevice.SetRenderTarget(this._tinyTarget);
			graphicsDevice.Clear(Color.Transparent);
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
			Main.spriteBatch.Draw(bigTarget, this._tinyTarget.Bounds, new Rectangle?(rectangle), Color.White);
			Main.spriteBatch.End();
			graphicsDevice.SetRenderTarget(this._pixelTarget);
			graphicsDevice.Clear(Color.White);
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, this._horizonBlendState, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
			Main.spriteBatch.Draw(this._tinyTarget, this._pixelTarget.Bounds, Color.White);
			Main.spriteBatch.End();
			graphicsDevice.SetRenderTarget(null);
			this._targetUpToDate = true;
			TimeLogger.SunVisibility.AddTime(startTimestamp);
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x005BB21C File Offset: 0x005B941C
		private void Test_DrawSmallTarget(RenderTarget2D bigTarget, Rectangle sunSampleRect)
		{
			GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
			graphicsDevice.SetRenderTarget(bigTarget);
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, new BlendState
			{
				ColorDestinationBlend = Blend.Zero,
				ColorSourceBlend = Blend.SourceAlpha,
				AlphaDestinationBlend = Blend.Zero,
				AlphaSourceBlend = Blend.SourceAlpha
			}, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
			Main.spriteBatch.Draw(this._tinyTarget, new Rectangle(0, 0, sunSampleRect.Width, sunSampleRect.Height), Color.White);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin();
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(sunSampleRect.Left, sunSampleRect.Top, 1, sunSampleRect.Height), Color.Red);
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(sunSampleRect.Right, sunSampleRect.Top, 1, sunSampleRect.Height), Color.Red);
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(sunSampleRect.Left, sunSampleRect.Top, sunSampleRect.Width, 1), Color.Red);
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(sunSampleRect.Left, sunSampleRect.Bottom, sunSampleRect.Width, 1), Color.Red);
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(sunSampleRect.Width, 0, 1, sunSampleRect.Height), Color.Red);
			Main.spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, sunSampleRect.Height, sunSampleRect.Width, 1), Color.Red);
			byte[] array = new byte[1];
			this._pixelTarget.GetData<byte>(array);
			float num = (float)array[0] / 255f;
			Utils.DrawBorderString(Main.spriteBatch, string.Format("{0:F3}", num), new Vector2(10f, (float)(sunSampleRect.Height + 20)), Color.White, 1f, 0f, 0f, -1);
			Main.spriteBatch.End();
			graphicsDevice.SetRenderTarget(null);
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x005BB447 File Offset: 0x005B9647
		public static void GetCelestialBodyColors(out Color sunColor, out Color moonColor)
		{
			sunColor = new Color(255, 246, 204);
			moonColor = HorizonHelper.GetMoonColor() * HorizonHelper.GetMoonStrength();
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x005BB478 File Offset: 0x005B9678
		private static Color GetMoonColor()
		{
			Color color = new Color(230, 235, 255);
			int num = Main.moonType;
			if (!TextureAssets.Moon.IndexInRange(num))
			{
				num = Utils.Clamp<int>(num, 0, 8);
			}
			color = HorizonHelper.MoonColors[num];
			if (Main.pumpkinMoon)
			{
				color = new Color(255, 225, 180);
			}
			if (Main.snowMoon)
			{
				color = new Color(220, 220, 255);
			}
			if (WorldGen.drunkWorldGen)
			{
				color = new Color(255, 255, 255);
			}
			return color;
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x005BB51B File Offset: 0x005B971B
		public static float GetMoonStrength()
		{
			return Utils.Remap((float)Math.Abs(4 - Main.moonPhase), 0f, 4f, 0f, 1f, true);
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x005BB544 File Offset: 0x005B9744
		// Note: this type is marked as 'beforefieldinit'.
		static HorizonHelper()
		{
		}

		// Token: 0x040056EA RID: 22250
		public static bool DebugSunVisibility = false;

		// Token: 0x040056EB RID: 22251
		private readonly int SampleAreaSize = 128;

		// Token: 0x040056EC RID: 22252
		private readonly int SmallTextureSize = 64;

		// Token: 0x040056ED RID: 22253
		private RenderTarget2D _tinyTarget;

		// Token: 0x040056EE RID: 22254
		private RenderTarget2D _pixelTarget;

		// Token: 0x040056EF RID: 22255
		private bool _targetUpToDate;

		// Token: 0x040056F0 RID: 22256
		private BlendState _horizonBlendState = new BlendState
		{
			AlphaSourceBlend = Blend.Zero,
			AlphaDestinationBlend = Blend.InverseSourceAlpha,
			ColorSourceBlend = Blend.Zero,
			ColorDestinationBlend = Blend.InverseSourceAlpha
		};

		// Token: 0x040056F1 RID: 22257
		private static Color[] MoonColors = new Color[]
		{
			new Color(230, 235, 255),
			new Color(250, 235, 160),
			new Color(230, 255, 230),
			new Color(160, 240, 255),
			new Color(180, 255, 255),
			new Color(230, 255, 230),
			new Color(255, 180, 255),
			new Color(255, 200, 180),
			new Color(225, 180, 255)
		};
	}
}
