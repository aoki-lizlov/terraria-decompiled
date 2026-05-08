using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
	// Token: 0x02000299 RID: 665
	public class BloodMoonScreenShaderData : ScreenShaderData
	{
		// Token: 0x06002551 RID: 9553 RVA: 0x00553E64 File Offset: 0x00552064
		public BloodMoonScreenShaderData(string passName)
			: base(passName)
		{
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x00555724 File Offset: 0x00553924
		public override void Update(GameTime gameTime)
		{
			float num = 1f - Utils.SmoothStep((float)Main.worldSurface + 50f, (float)Main.rockLayer + 100f, (Main.screenPosition.Y + (float)(Main.screenHeight / 2)) / 16f);
			if (Main.remixWorld)
			{
				num = Utils.SmoothStep((float)(Main.rockLayer + Main.worldSurface) / 2f, (float)Main.rockLayer, (Main.screenPosition.Y + (float)(Main.screenHeight / 2)) / 16f);
			}
			if (Main.shimmerAlpha > 0f)
			{
				num *= 1f - Main.shimmerAlpha;
			}
			base.UseOpacity(num * 0.75f);
		}
	}
}
