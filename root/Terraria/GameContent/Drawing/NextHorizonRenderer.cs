using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.Skies;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x0200043A RID: 1082
	public class NextHorizonRenderer : IHorizonRenderer
	{
		// Token: 0x060030C1 RID: 12481 RVA: 0x005BC92C File Offset: 0x005BAB2C
		private void LoadTextures()
		{
			if (NextHorizonRenderer._sunriseTextures != null)
			{
				return;
			}
			NextHorizonRenderer._sunriseTextures = new Asset<Texture2D>[]
			{
				Main.Assets.Request<Texture2D>("Images/Misc/Sunrise/Sunrise_Blue", 1),
				Main.Assets.Request<Texture2D>("Images/Misc/Sunrise/Sunrise_Violet", 1),
				Main.Assets.Request<Texture2D>("Images/Misc/Sunrise/Sunrise_Yellow", 1),
				Main.Assets.Request<Texture2D>("Images/Misc/Sunrise/Sunrise_Aluminum", 1)
			};
			NextHorizonRenderer._sunsetTextures = new Asset<Texture2D>[]
			{
				Main.Assets.Request<Texture2D>("Images/Misc/Sunset/Sunset_Blue", 1),
				Main.Assets.Request<Texture2D>("Images/Misc/Sunset/Sunset_Dark", 1),
				Main.Assets.Request<Texture2D>("Images/Misc/Sunset/Sunset_Pink", 1),
				Main.Assets.Request<Texture2D>("Images/Misc/Sunset/Sunset_Red", 1)
			};
			NextHorizonRenderer._sunflareGradientTexture = Main.Assets.Request<Texture2D>("Images/Misc/Sunflare/colorgradient", 1);
			NextHorizonRenderer._sunflareGradientDitherTexture = Main.Assets.Request<Texture2D>("Images/Misc/Sunflare/colorgradientdither", 1);
			NextHorizonRenderer._sunflarePointBlurryTexture = Main.Assets.Request<Texture2D>("Images/Misc/Sunflare/Lens/PointBlurry", 1);
			NextHorizonRenderer._sunflarePointSharpTexture = Main.Assets.Request<Texture2D>("Images/Misc/Sunflare/Lens/PointSharp", 1);
			NextHorizonRenderer._sunflare1Texture = Main.Assets.Request<Texture2D>("Images/Misc/Sunflare/flare1", 1);
			NextHorizonRenderer._sunflare2Texture = Main.Assets.Request<Texture2D>("Images/Misc/Sunflare/flare2", 1);
			NextHorizonRenderer._bokehTexture = Main.Assets.Request<Texture2D>("Images/Misc/Sunflare/Lens/Flare1", 1);
			NextHorizonRenderer._spectraTexture = Main.Assets.Request<Texture2D>("Images/Misc/Sunflare/Lens/Flare2", 1);
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x005BCA98 File Offset: 0x005BAC98
		private static Rectangle GetGradientRect()
		{
			int num = 400;
			int num2 = (int)((1.0 - Utils.GetLerpValue(40.0, Main.worldSurface, (double)(Main.screenPosition.Y / 16f), false)) * (double)num);
			int num3 = Math.Max(0, num2) - num;
			return new Rectangle(0, num3, Main.screenWidth, Main.screenHeight + num);
		}

		// Token: 0x060030C3 RID: 12483 RVA: 0x005BCAFC File Offset: 0x005BACFC
		public void DrawHorizon()
		{
			if (!Main.ShouldDrawSurfaceBackground())
			{
				return;
			}
			this.LoadTextures();
			int sunriseSunsetTextureIndex = this.GetSunriseSunsetTextureIndex();
			Asset<Texture2D> asset = NextHorizonRenderer._sunriseTextures[sunriseSunsetTextureIndex % NextHorizonRenderer._sunriseTextures.Length];
			Asset<Texture2D> asset2 = NextHorizonRenderer._sunsetTextures[sunriseSunsetTextureIndex % NextHorizonRenderer._sunsetTextures.Length];
			float num;
			float num2;
			float num3;
			NextHorizonRenderer.GetVisibilities(out num, out num2, out num3);
			SpriteBatch spriteBatch = Main.spriteBatch;
			Rectangle gradientRect = NextHorizonRenderer.GetGradientRect();
			foreach (BackgroundGradientDrawer backgroundGradientDrawer in SunGradients.BackgroundDrawers)
			{
				backgroundGradientDrawer.Draw();
			}
			if (num2 != 0f)
			{
				spriteBatch.Draw(asset.Value, gradientRect, Color.White * num2);
			}
			if (num != 0f)
			{
				spriteBatch.Draw(asset2.Value, gradientRect, Color.White * num);
			}
		}

		// Token: 0x060030C4 RID: 12484 RVA: 0x005BB51B File Offset: 0x005B971B
		public float GetMoonStrength()
		{
			return Utils.Remap((float)Math.Abs(4 - Main.moonPhase), 0f, 4f, 0f, 1f, true);
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x005BCBE0 File Offset: 0x005BADE0
		public void DrawSurfaceLayer(int layerIndex)
		{
			if (!Main.ShouldDrawSurfaceBackground())
			{
				return;
			}
			this.LoadTextures();
			SpriteBatch spriteBatch = Main.spriteBatch;
			Rectangle gradientRect = NextHorizonRenderer.GetGradientRect();
			float num;
			float num2;
			float num3;
			NextHorizonRenderer.GetVisibilities(out num, out num2, out num3);
			int sunriseSunsetTextureIndex = this.GetSunriseSunsetTextureIndex();
			List<Color[]> sunrises = SunGradients.Sunrises;
			Color[] array = sunrises[sunriseSunsetTextureIndex % sunrises.Count];
			List<Color[]> sunsets = SunGradients.Sunsets;
			Color[] array2 = sunsets[sunriseSunsetTextureIndex % sunsets.Count];
			Color transparent = Color.Transparent;
			this.BlendColor(ref transparent, array2[0], num);
			this.BlendColor(ref transparent, array[0], num2);
			switch (layerIndex)
			{
			}
			Asset<Texture2D> asset = NextHorizonRenderer._sunriseTextures[sunriseSunsetTextureIndex % NextHorizonRenderer._sunriseTextures.Length];
			Asset<Texture2D> asset2 = NextHorizonRenderer._sunsetTextures[sunriseSunsetTextureIndex % NextHorizonRenderer._sunsetTextures.Length];
			TileBatch tileBatch = Main.tileBatch;
			if (layerIndex == 3)
			{
				float num4 = 0.6f;
				float num5 = 1f;
				spriteBatch.Draw(NextHorizonRenderer._sunflareGradientTexture.Value, gradientRect, null, array[0] * num5 * num2 * num4, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
				spriteBatch.Draw(NextHorizonRenderer._sunflareGradientTexture.Value, gradientRect, null, array2[0] * num5 * num * num4, 0f, Vector2.Zero, SpriteEffects.None, 0f);
			}
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x005BCD83 File Offset: 0x005BAF83
		private int GetSunriseSunsetTextureIndex()
		{
			return Main.HorizonPhase;
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x005BCD8C File Offset: 0x005BAF8C
		public void ModifyHorizonLight(ref Color color)
		{
			if (!Main.ShouldDrawSurfaceBackground())
			{
				return;
			}
			float num;
			float num2;
			float num3;
			NextHorizonRenderer.GetVisibilities(out num, out num2, out num3);
			int sunriseSunsetTextureIndex = this.GetSunriseSunsetTextureIndex();
			List<Color[]> sunrises = SunGradients.Sunrises;
			Color[] array = sunrises[sunriseSunsetTextureIndex % sunrises.Count];
			List<Color[]> sunsets = SunGradients.Sunsets;
			Color[] array2 = sunsets[sunriseSunsetTextureIndex % sunsets.Count];
			this.BlendColor(ref color, array2, num);
			this.BlendColor(ref color, array, num2);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x005BCDFC File Offset: 0x005BAFFC
		public void DrawSun(Vector2 sunPosition)
		{
			float num;
			float num2;
			float num3;
			NextHorizonRenderer.GetVisibilities(out num, out num2, out num3);
			num *= num3;
			num2 *= num3;
			this.LoadTextures();
			Color color = new Color(255, 255, 255, 0);
			SpriteBatch spriteBatch = Main.spriteBatch;
			spriteBatch.Draw(NextHorizonRenderer._sunflare1Texture.Value, sunPosition, null, color * num * 0.75f, 0f, NextHorizonRenderer._sunflare1Texture.Size() / 2f, 3f, SpriteEffects.None, 0f);
			spriteBatch.Draw(NextHorizonRenderer._sunflare1Texture.Value, sunPosition, null, color * num * 0.35f, 0f, NextHorizonRenderer._sunflare1Texture.Size() / 2f, 2f, SpriteEffects.None, 0f);
			spriteBatch.Draw(NextHorizonRenderer._sunflare2Texture.Value, sunPosition, null, color * num2 * 0.7f * 0.5f, 0f, NextHorizonRenderer._sunflare2Texture.Size() / 2f, 2f, SpriteEffects.None, 0f);
			spriteBatch.Draw(NextHorizonRenderer._sunflare2Texture.Value, sunPosition, null, color * num2 * 0.3f * 0.5f, 0f, NextHorizonRenderer._sunflare2Texture.Size() / 2f, 1.5f, SpriteEffects.None, 0f);
			spriteBatch.Draw(NextHorizonRenderer._sunflare2Texture.Value, sunPosition, null, color * num2 * 0.2f * 0.5f, 0f, NextHorizonRenderer._sunflare2Texture.Size() / 2f, 1f, SpriteEffects.None, 0f);
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x005BCFEB File Offset: 0x005BB1EB
		private void BlendColor(ref Color color, Color[] gradient, float opacity)
		{
			this.BlendColor(ref color, gradient[gradient.Length / 2], opacity);
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x005BD000 File Offset: 0x005BB200
		private void BlendColor(ref Color color, Color colorToChoose, float opacity)
		{
			if (opacity <= 0f)
			{
				return;
			}
			Color color2 = new Color((int)Math.Max(color.R, colorToChoose.R), (int)Math.Max(color.G, colorToChoose.G), (int)Math.Max(color.B, colorToChoose.B), (int)Math.Max(color.A, colorToChoose.A));
			color = Color.Lerp(color, color2, opacity);
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x005BD078 File Offset: 0x005BB278
		private static void GetVisibilities(out float sunsetVisibility, out float sunriseVisibility, out float celestialVisibility)
		{
			sunsetVisibility = 1f;
			sunriseVisibility = 1f;
			celestialVisibility = NextHorizonRenderer.GetCelestialEffectPower();
			float num = 1f;
			num *= Main.atmo;
			float num2 = 1f - Main.cloudAlpha;
			num *= num2 * num2;
			num *= 1f - Main.SmoothedMushroomLightInfluence;
			sunriseVisibility *= num;
			sunsetVisibility *= num;
			double time = Main.time;
			double num3 = 54000.0;
			if (Main.dayTime)
			{
				float num4 = 3600f;
				int num5 = 2700;
				float num6 = 10800f;
				float num7 = -10800f;
				float num8 = -3600f;
				sunriseVisibility *= Utils.Remap((float)time, 0f, (float)num5, 0f, 1f, true) * Utils.Remap((float)time, num4, num6, 1f, 0f, true);
				float num9 = Utils.Remap((float)time, (float)num3 + num7, (float)num3 + num8, 0f, 1f, true);
				float num10 = Utils.Remap((float)time, (float)num3 + num8, (float)num3, 1f, 0f, true);
				sunsetVisibility *= num9 * num10 * num10;
				if (Main.eclipse)
				{
					sunsetVisibility = 0f;
					sunriseVisibility = 0f;
				}
			}
			else
			{
				sunriseVisibility = 0f;
				sunsetVisibility = 0f;
			}
			if (Main.gameMenu && WorldGen.drunkWorldGen)
			{
				sunsetVisibility = (sunriseVisibility = 0f);
			}
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x005BD1CE File Offset: 0x005BB3CE
		public void CloudsStart()
		{
			this._drawData.Clear();
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x005BD1DC File Offset: 0x005BB3DC
		public void DrawCloud(float globalCloudAlpha, Cloud theCloud, int cloudPass, float cY)
		{
			Asset<Texture2D> asset = TextureAssets.Cloud[theCloud.type];
			Vector2 vector = new Vector2(theCloud.position.X, cY) + asset.Size() / 2f;
			Color color = theCloud.cloudColor(Main.ColorOfTheSkies);
			this.OriginalColorsForCloud(theCloud, cloudPass, ref color);
			if (Main.atmo < 1f)
			{
				color *= Main.atmo;
			}
			this._drawData.Add(new DrawData(asset.Value, vector, null, color * globalCloudAlpha, theCloud.rotation, asset.Size() / 2f, theCloud.scale, theCloud.spriteDir, 0f));
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x005BD29C File Offset: 0x005BB49C
		private void OriginalColorsForCloud(Cloud theCloud, int cloudPass, ref Color cloudColor)
		{
			if (cloudPass == 1)
			{
				float num = theCloud.scale * 0.8f;
				float num2 = (theCloud.scale + 1f) / 2f * 0.9f;
				cloudColor.R = (byte)((float)cloudColor.R * num);
				cloudColor.G = (byte)((float)cloudColor.G * num2);
			}
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x005BD2F4 File Offset: 0x005BB4F4
		private void BetterColorsForClouds(Cloud theCloud, int cloudPass, ref Vector2 cloudDrawPosition, ref Color cloudColor)
		{
			float num = 0f;
			if (cloudPass != 1)
			{
				if (cloudPass == 2)
				{
					num = 0.35f;
				}
			}
			else
			{
				num = 0.7f;
			}
			if (Main.keyState.IsKeyDown(Keys.LeftShift))
			{
				num = 0f;
			}
			if (num > 0f)
			{
				float num2;
				float num3;
				float num4;
				NextHorizonRenderer.GetVisibilities(out num2, out num3, out num4);
				int sunriseSunsetTextureIndex = this.GetSunriseSunsetTextureIndex();
				List<Color[]> sunrises = SunGradients.Sunrises;
				Color[] array = sunrises[sunriseSunsetTextureIndex % sunrises.Count];
				List<Color[]> sunsets = SunGradients.Sunsets;
				Color[] array2 = sunsets[sunriseSunsetTextureIndex % sunsets.Count];
				float num5 = cloudDrawPosition.Y / (float)Main.screenHeight;
				float alpha = theCloud.Alpha;
				this.BlendColorAlongGradientBasedOnHeight(ref cloudColor, num2, num5, array2, alpha);
				this.BlendColorAlongGradientBasedOnHeight(ref cloudColor, num3, num5, array, alpha);
			}
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x005BD3BC File Offset: 0x005BB5BC
		private void BlendColorAlongGradientBasedOnHeight(ref Color color, float visibility, float normalizedScreenHeight, Color[] gradient, float opacity)
		{
			float num = MathHelper.Clamp(normalizedScreenHeight * (float)gradient.Length, 0f, (float)(gradient.Length - 1));
			float num2 = num % 1f;
			int num3 = (int)Math.Floor((double)num);
			if (num2 == 0f || num3 == gradient.Length - 1)
			{
				this.BlendColor(ref color, gradient[num3] * opacity, visibility);
				return;
			}
			Color color2 = Color.Lerp(gradient[num3], gradient[num3 + 1], num2) * opacity;
			this.BlendColor(ref color, color2, visibility);
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x005BD444 File Offset: 0x005BB644
		private static float GetCelestialEffectPower()
		{
			float num = 1800f;
			float num2 = 1800f;
			float num3 = 0f;
			if (Main.dayTime)
			{
				return Utils.Remap((float)Main.time, 0f, num * 2f, 0f, 1f, true) * Utils.Remap((float)Main.time, 54000f - num, 54000f, 1f, num3, true);
			}
			return Utils.Remap((float)Main.time, 0f, num2 * 2f, 0f, 1f, true) * Utils.Remap((float)Main.time, 32400f - num2, 32400f, 1f, 0f, true);
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x005BD4F4 File Offset: 0x005BB6F4
		public void CloudsEnd()
		{
			if (this._drawData.Count == 0)
			{
				return;
			}
			Main.spriteBatch.End();
			SpriteDrawBuffer spriteBuffer = Main.spriteBuffer;
			foreach (DrawData drawData in this._drawData)
			{
				drawData.Draw(spriteBuffer);
			}
			MiscShaderData miscShaderData = GameShaders.Misc["HorizonClouds"];
			miscShaderData.UseSpriteTransformMatrix(new Matrix?(Main.LatestSurfaceBackgroundBeginner.transformMatrix));
			Color color;
			Color color2;
			HorizonHelper.GetCelestialBodyColors(out color, out color2);
			Color color3 = (Main.dayTime ? color : color2);
			AuroraSky.ModifyTileColor(ref color3, 1f);
			miscShaderData.UseColor(color3);
			Vector2 celestialBodyPosition = NextHorizonRenderer.GetCelestialBodyPosition();
			float num;
			float num2;
			float num3;
			NextHorizonRenderer.GetVisibilities(out num, out num2, out num3);
			float num4 = Math.Max(num, num2) * num3;
			if (!Main.dayTime)
			{
				num4 = Math.Max(num4, num3 * 0.15f);
			}
			num4 *= Utils.Clamp<float>(1f - Main.cloudBGAlpha, 0f, 1f);
			miscShaderData.UseShaderSpecificData(new Vector4(celestialBodyPosition.X, celestialBodyPosition.Y, num4, 0f));
			for (int i = 0; i < this._drawData.Count; i++)
			{
				miscShaderData.Apply(new DrawData?(this._drawData[i]));
				spriteBuffer.DrawSingle(i);
			}
			spriteBuffer.Unbind();
			Main.LatestSurfaceBackgroundBeginner.Begin(Main.spriteBatch);
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x005BD680 File Offset: 0x005BB880
		private static Vector2 GetCelestialBodyPosition()
		{
			return Main.LastCelestialBodyPosition * Main.ScreenSize.ToVector2();
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x005BD698 File Offset: 0x005BB898
		public void DrawLensFlare()
		{
			if (!Main.ShouldDrawSurfaceBackground() || !Main.HorizonHelper.SunVisibilityEnabled)
			{
				return;
			}
			SpriteBatch spriteBatch = Main.spriteBatch;
			Vector2 celestialBodyPosition = NextHorizonRenderer.GetCelestialBodyPosition();
			Vector2 vector = Main.ScreenSize.ToVector2() / 2f;
			float num;
			float num2;
			float num3;
			NextHorizonRenderer.GetVisibilities(out num, out num2, out num3);
			float num4 = this.AdjustIntensity(num2, num3);
			float num5 = this.AdjustIntensity(num, num3);
			if ((double)num4 <= 0.01 && (double)num5 <= 0.01)
			{
				return;
			}
			Main.LatestSurfaceBackgroundBeginner.Begin(spriteBatch, SpriteSortMode.Immediate);
			EffectPass effectPass = Main.pixelShader.CurrentTechnique.Passes[0];
			MiscShaderData miscShaderData = GameShaders.Misc["LensFlare"];
			miscShaderData.UseImage1(Main.HorizonHelper.SunVisibilityPixelTexture);
			miscShaderData.Apply(null);
			this.DrawSunriseFlare(spriteBatch, celestialBodyPosition, vector, num4);
			this.DrawSunsetFlare(spriteBatch, celestialBodyPosition, vector, num5);
			spriteBatch.End();
			effectPass.Apply();
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x005BD78C File Offset: 0x005BB98C
		private float AdjustIntensity(float temporalIntensity, float celestialVisibility)
		{
			float num = temporalIntensity * celestialVisibility;
			num *= num * num;
			int sunScorchCounter = Main.SceneMetrics.PerspectivePlayer.sunScorchCounter;
			if (sunScorchCounter > 0)
			{
				float num2 = Utils.GetLerpValue(0f, 300f, (float)sunScorchCounter, true);
				num2 = 1f - num2;
				num = 1f - num2 * num2;
				num *= celestialVisibility;
				num *= 5f;
			}
			return num;
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x005BD7EC File Offset: 0x005BB9EC
		private void DrawSunsetFlare(SpriteBatch spriteBatch, Vector2 sunPosition, Vector2 screenCenter, float intensity)
		{
			if (intensity <= 0.01f)
			{
				return;
			}
			this.LoadTextures();
			LensFlareElement lensFlareElement = default(LensFlareElement);
			lensFlareElement.Texture = NextHorizonRenderer._sunflarePointBlurryTexture;
			lensFlareElement.RepeatTimes = 3;
			lensFlareElement.DistanceStart = 0.33f;
			lensFlareElement.DistanceAlongIndex = 0.05f;
			lensFlareElement.ScaleStart = 0.3f;
			lensFlareElement.ScaleOverIndex = -0.04f;
			lensFlareElement.Color = new Color(43, 32, 0, 0) * 0.47058824f;
			lensFlareElement.IntensityOverIndex = -0.125f;
			lensFlareElement.Draw(spriteBatch, sunPosition, screenCenter, intensity);
			lensFlareElement = default(LensFlareElement);
			lensFlareElement.Texture = NextHorizonRenderer._sunflarePointSharpTexture;
			lensFlareElement.RepeatTimes = 3;
			lensFlareElement.DistanceStart = 0.03f;
			lensFlareElement.DistanceAlongIndex = 0.05f;
			lensFlareElement.ScaleStart = 0.3f;
			lensFlareElement.ScaleOverIndex = 0.04f;
			lensFlareElement.Color = new Color(43, 32, 0, 0) * 0.47058824f;
			lensFlareElement.IntensityOverIndex = -0.125f;
			lensFlareElement.Draw(spriteBatch, sunPosition, screenCenter, intensity);
			lensFlareElement = default(LensFlareElement);
			lensFlareElement.Texture = NextHorizonRenderer._sunflarePointBlurryTexture;
			lensFlareElement.RepeatTimes = 1;
			lensFlareElement.DistanceStart = 0.41f;
			lensFlareElement.ScaleStart = 0.3f;
			lensFlareElement.Color = new Color(255, 0, 65, 0) * 0.11764706f;
			lensFlareElement.Draw(spriteBatch, sunPosition, screenCenter, intensity);
			lensFlareElement = default(LensFlareElement);
			lensFlareElement.Texture = NextHorizonRenderer._bokehTexture;
			lensFlareElement.RepeatTimes = 1;
			lensFlareElement.DistanceStart = 0.475f;
			lensFlareElement.ScaleStart = 0.3f;
			lensFlareElement.Color = new Color(255, 255, 255, 0) * 0.15686275f;
			lensFlareElement.Draw(spriteBatch, sunPosition, screenCenter, intensity);
			lensFlareElement = default(LensFlareElement);
			lensFlareElement.Texture = NextHorizonRenderer._bokehTexture;
			lensFlareElement.RepeatTimes = 6;
			lensFlareElement.DistanceStart = 0.225f;
			lensFlareElement.DistanceAlongIndex = 0.04f;
			lensFlareElement.ScaleStart = 0.24f;
			lensFlareElement.ScaleOverIndex = -0.04f;
			lensFlareElement.Color = new Color(255, 255, 255, 0) * 0.078431375f;
			lensFlareElement.Draw(spriteBatch, sunPosition, screenCenter, intensity);
			lensFlareElement = default(LensFlareElement);
			lensFlareElement.Texture = NextHorizonRenderer._sunflarePointBlurryTexture;
			lensFlareElement.RepeatTimes = 1;
			lensFlareElement.DistanceStart = 0.6f;
			lensFlareElement.ScaleStart = 1f;
			lensFlareElement.Color = new Color(255, 157, 0, 0) * 0.15686275f;
			lensFlareElement.Draw(spriteBatch, sunPosition, screenCenter, intensity);
			lensFlareElement = default(LensFlareElement);
			lensFlareElement.Texture = NextHorizonRenderer._spectraTexture;
			lensFlareElement.RepeatTimes = 1;
			lensFlareElement.DistanceStart = 0.65f;
			lensFlareElement.ScaleStart = 0.4f;
			lensFlareElement.Rotation = 3.1415927f;
			lensFlareElement.Color = new Color(255, 255, 255, 0) * 0.039215688f;
			lensFlareElement.Draw(spriteBatch, sunPosition, screenCenter, intensity);
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x005BDB20 File Offset: 0x005BBD20
		private void DrawSunriseFlare(SpriteBatch spriteBatch, Vector2 sunPosition, Vector2 screenCenter, float intensity)
		{
			if (intensity <= 0.01f)
			{
				return;
			}
			this.LoadTextures();
			LensFlareElement lensFlareElement = default(LensFlareElement);
			lensFlareElement.Texture = NextHorizonRenderer._sunflarePointSharpTexture;
			lensFlareElement.RepeatTimes = 3;
			lensFlareElement.DistanceStart = 0.33f;
			lensFlareElement.DistanceAlongIndex = 0.05f;
			lensFlareElement.ScaleStart = 0.3f;
			lensFlareElement.ScaleOverIndex = -0.04f;
			lensFlareElement.Color = new Color(0, 32, 43, 0) * 0.47058824f;
			lensFlareElement.IntensityOverIndex = -0.125f;
			lensFlareElement.Draw(spriteBatch, sunPosition, screenCenter, intensity);
			lensFlareElement = default(LensFlareElement);
			lensFlareElement.Texture = NextHorizonRenderer._sunflarePointSharpTexture;
			lensFlareElement.RepeatTimes = 3;
			lensFlareElement.DistanceStart = 0.03f;
			lensFlareElement.DistanceAlongIndex = 0.05f;
			lensFlareElement.ScaleStart = 0.3f;
			lensFlareElement.ScaleOverIndex = 0.04f;
			lensFlareElement.Color = new Color(0, 32, 43, 0) * 0.47058824f;
			lensFlareElement.IntensityOverIndex = -0.125f;
			lensFlareElement.Draw(spriteBatch, sunPosition, screenCenter, intensity);
			lensFlareElement = default(LensFlareElement);
			lensFlareElement.Texture = NextHorizonRenderer._sunflarePointBlurryTexture;
			lensFlareElement.RepeatTimes = 1;
			lensFlareElement.DistanceStart = 0.41f;
			lensFlareElement.ScaleStart = 0.3f;
			lensFlareElement.Color = new Color(65, 0, 255, 0) * 0.11764706f;
			lensFlareElement.Draw(spriteBatch, sunPosition, screenCenter, intensity);
			lensFlareElement = default(LensFlareElement);
			lensFlareElement.Texture = NextHorizonRenderer._bokehTexture;
			lensFlareElement.RepeatTimes = 1;
			lensFlareElement.DistanceStart = 0.525f;
			lensFlareElement.Rotation = 0.01f;
			lensFlareElement.ScaleStart = 0.3f;
			lensFlareElement.Color = new Color(255, 255, 255, 0) * 0.15686275f;
			lensFlareElement.Draw(spriteBatch, sunPosition, screenCenter, intensity);
			lensFlareElement = default(LensFlareElement);
			lensFlareElement.Texture = NextHorizonRenderer._bokehTexture;
			lensFlareElement.RepeatTimes = 6;
			lensFlareElement.DistanceStart = 0.225f;
			lensFlareElement.DistanceAlongIndex = 0.04f;
			lensFlareElement.ScaleStart = 0.24f;
			lensFlareElement.ScaleOverIndex = -0.04f;
			lensFlareElement.Color = new Color(255, 255, 255, 0) * 0.078431375f;
			lensFlareElement.Draw(spriteBatch, sunPosition, screenCenter, intensity);
			lensFlareElement = default(LensFlareElement);
			lensFlareElement.Texture = NextHorizonRenderer._sunflarePointBlurryTexture;
			lensFlareElement.RepeatTimes = 1;
			lensFlareElement.DistanceStart = 0.6f;
			lensFlareElement.ScaleStart = 1f;
			lensFlareElement.Color = new Color(0, 157, 255, 0) * 0.15686275f;
			lensFlareElement.Draw(spriteBatch, sunPosition, screenCenter, intensity);
			lensFlareElement = default(LensFlareElement);
			lensFlareElement.Texture = NextHorizonRenderer._spectraTexture;
			lensFlareElement.RepeatTimes = 1;
			lensFlareElement.DistanceStart = 0.65f;
			lensFlareElement.ScaleStart = 0.38f;
			lensFlareElement.Rotation = 3.1415927f;
			lensFlareElement.Color = new Color(255, 255, 255, 0) * 0.039215688f;
			lensFlareElement.Draw(spriteBatch, sunPosition, screenCenter, intensity);
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x005BDE60 File Offset: 0x005BC060
		public NextHorizonRenderer()
		{
		}

		// Token: 0x04005703 RID: 22275
		private static Asset<Texture2D>[] _sunriseTextures;

		// Token: 0x04005704 RID: 22276
		private static Asset<Texture2D>[] _sunsetTextures;

		// Token: 0x04005705 RID: 22277
		private static Asset<Texture2D> _sunflareGradientTexture;

		// Token: 0x04005706 RID: 22278
		private static Asset<Texture2D> _sunflareGradientDitherTexture;

		// Token: 0x04005707 RID: 22279
		private static Asset<Texture2D> _sunflarePointBlurryTexture;

		// Token: 0x04005708 RID: 22280
		private static Asset<Texture2D> _sunflarePointSharpTexture;

		// Token: 0x04005709 RID: 22281
		private static Asset<Texture2D> _bokehTexture;

		// Token: 0x0400570A RID: 22282
		private static Asset<Texture2D> _spectraTexture;

		// Token: 0x0400570B RID: 22283
		private static Asset<Texture2D> _sunflare1Texture;

		// Token: 0x0400570C RID: 22284
		private static Asset<Texture2D> _sunflare2Texture;

		// Token: 0x0400570D RID: 22285
		private List<DrawData> _drawData = new List<DrawData>(200);
	}
}
