using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x0200043F RID: 1087
	public class NextNatureRenderer : INatureRenderer
	{
		// Token: 0x060030E1 RID: 12513 RVA: 0x005BE048 File Offset: 0x005BC248
		public void DrawNature(Texture2D texture, Vector2 position, Rectangle sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, SideFlags seams = SideFlags.None)
		{
			seams |= NextNatureRenderer.GetOriginSides(sourceRectangle, origin);
			NextNatureRenderer.Entry entry = new NextNatureRenderer.Entry
			{
				Data = new DrawData(texture, position, new Rectangle?(sourceRectangle), color, rotation, origin, scale, effects, 0f),
				IsGlowMask = false,
				Seams = seams
			};
			this._entries.Add(entry);
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x005BE0AC File Offset: 0x005BC2AC
		private static SideFlags GetOriginSides(Rectangle sourceRectangle, Vector2 origin)
		{
			float num = origin.X / (float)sourceRectangle.Width;
			double num2 = (double)(1f - num);
			float num3 = origin.Y / (float)sourceRectangle.Height;
			float num4 = 1f - num3;
			SideFlags sideFlags = SideFlags.None;
			if ((double)num < 0.25)
			{
				sideFlags |= SideFlags.Left;
			}
			if (num2 < 0.25)
			{
				sideFlags |= SideFlags.Right;
			}
			if ((double)num3 < 0.25)
			{
				sideFlags |= SideFlags.Top;
			}
			if ((double)num4 < 0.25)
			{
				sideFlags |= SideFlags.Bottom;
			}
			return sideFlags;
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x005BE12C File Offset: 0x005BC32C
		public void DrawGlowmask(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
		{
			NextNatureRenderer.Entry entry = new NextNatureRenderer.Entry
			{
				Data = new DrawData(texture, position, sourceRectangle, color, rotation, origin, scale, effects, 0f),
				IsGlowMask = true
			};
			this._entries.Add(entry);
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x005BE178 File Offset: 0x005BC378
		public void DrawAfterAllObjects(SpriteBatchBeginner beginner)
		{
			if (this._entries.Count == 0)
			{
				return;
			}
			TimeLogger.StartTimestamp startTimestamp = TimeLogger.Start();
			float num = 0f;
			if (Main.dayTime)
			{
				float num2 = (float)Main.time;
				float num3 = 54000f;
				float num4 = Utils.Remap(num2, 1200f, 5400f, 0f, 1f, true) * Utils.Remap(num2, 1200f, 7200f, 1f, 0f, true) * 0.3f;
				float num5 = Utils.Remap(num2, num3 - 10800f, num3 - 4200f, 0f, 1f, true) * Utils.Remap(num2, num3 - 1800f, num3 - 600f, 1f, 0f, true) * 0.4f;
				num5 *= num5;
				float num6 = Utils.Remap(num2, 0f, 7200f, 0f, 1f, true) * Utils.Remap(num2, num3 - 7200f, num3, 1f, 0f, true) * 0f;
				num = Math.Max(Math.Max(num4, num5), num6);
				if (Main.eclipse)
				{
					num = 0f;
				}
			}
			num *= 0.4f;
			Vector2 lastCelestialBodyPosition = Main.LastCelestialBodyPosition;
			float num7 = Utils.Remap(Math.Min(lastCelestialBodyPosition.X, 1f - lastCelestialBodyPosition.X), 0f, 0.010416667f, 0f, 1f, true);
			num *= num7;
			if (!Main.ShouldDrawSurfaceBackground() || !Main.HorizonHelper.SunVisibilityEnabled)
			{
				num = 0f;
			}
			if (num == 0f)
			{
				this.DrawWithoutShader(beginner, Main.spriteBatch);
			}
			else
			{
				this.DrawWithLitNatureShader(beginner, num, lastCelestialBodyPosition);
			}
			this._entries.Clear();
			TimeLogger.Nature.AddTime(startTimestamp);
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x005BE33C File Offset: 0x005BC53C
		private void DrawWithoutShader(SpriteBatchBeginner beginner, SpriteBatch spriteBatch)
		{
			beginner.Begin(spriteBatch);
			foreach (NextNatureRenderer.Entry entry in this._entries)
			{
				DrawData data = entry.Data;
				data.Draw(spriteBatch);
			}
			spriteBatch.End();
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x005BE3A8 File Offset: 0x005BC5A8
		private void DrawWithLitNatureShader(SpriteBatchBeginner beginner, float visibility, Vector2 sunPosition)
		{
			SpriteDrawBuffer spriteBuffer = Main.spriteBuffer;
			foreach (NextNatureRenderer.Entry entry in this._entries)
			{
				DrawData data = entry.Data;
				data.Draw(spriteBuffer);
			}
			MiscShaderData miscShaderData = GameShaders.Misc["LitNature"];
			Vector2 vector = Vector2.Transform(Main.ReverseGravitySupport(sunPosition * Main.ScreenSize.ToVector2(), 0f), Matrix.Invert(Main.Transform));
			Vector4 vector2 = new Vector4(vector.X, vector.Y, visibility, 0f);
			miscShaderData.UseImage1(Main.HorizonHelper.SunVisibilityPixelTexture);
			miscShaderData.UseSpriteTransformMatrix(new Matrix?(beginner.transformMatrix));
			Color color;
			Color color2;
			HorizonHelper.GetCelestialBodyColors(out color, out color2);
			Color color3 = (Main.dayTime ? color : color2);
			Vector3 vector3 = Main.rgbToHsl(color3);
			color3 = Main.hslToRgb(vector3.X, Utils.Clamp<float>(vector3.Y * 8f, 0f, 1f), vector3.Z * 1f, byte.MaxValue) * 0.5f;
			miscShaderData.UseColor(Color.Lerp(color3, new Color(255, 200, 0), 0.8f));
			int num = 0;
			foreach (NextNatureRenderer.Entry entry2 in this._entries)
			{
				vector2.W = (float)(entry2.IsGlowMask ? ((SideFlags)(-1)) : entry2.Seams);
				miscShaderData.UseShaderSpecificData(vector2);
				miscShaderData.Apply(new DrawData?(entry2.Data));
				spriteBuffer.DrawSingle(num++);
			}
			spriteBuffer.Unbind();
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x005BE598 File Offset: 0x005BC798
		public NextNatureRenderer()
		{
		}

		// Token: 0x0400571D RID: 22301
		private readonly List<NextNatureRenderer.Entry> _entries = new List<NextNatureRenderer.Entry>();

		// Token: 0x02000940 RID: 2368
		private struct Entry
		{
			// Token: 0x04007548 RID: 30024
			public DrawData Data;

			// Token: 0x04007549 RID: 30025
			public SideFlags Seams;

			// Token: 0x0400754A RID: 30026
			public bool IsGlowMask;
		}
	}
}
