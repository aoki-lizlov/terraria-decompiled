using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x02000435 RID: 1077
	public class EmptyHorizonRenderer : IHorizonRenderer
	{
		// Token: 0x060030A9 RID: 12457 RVA: 0x005BB658 File Offset: 0x005B9858
		public void DrawHorizon()
		{
			if (!Main.ShouldDrawSurfaceBackground())
			{
				return;
			}
			foreach (BackgroundGradientDrawer backgroundGradientDrawer in SunGradients.BackgroundDrawers)
			{
				backgroundGradientDrawer.Draw();
			}
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x00009E46 File Offset: 0x00008046
		public void DrawLensFlare()
		{
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x00009E46 File Offset: 0x00008046
		public void ModifyHorizonLight(ref Color color)
		{
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x00009E46 File Offset: 0x00008046
		public void DrawSun(Vector2 sunPosition)
		{
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x00009E46 File Offset: 0x00008046
		public void DrawSurfaceLayer(int layerIndex)
		{
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x00009E46 File Offset: 0x00008046
		public void CloudsStart()
		{
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x005BB6B0 File Offset: 0x005B98B0
		public void DrawCloud(float globalCloudAlpha, Cloud theCloud, int cloudPass, float cY)
		{
			Asset<Texture2D> asset = TextureAssets.Cloud[theCloud.type];
			Color color = theCloud.cloudColor(Main.ColorOfTheSkies);
			if (cloudPass == 1)
			{
				float num = theCloud.scale * 0.8f;
				float num2 = (theCloud.scale + 1f) / 2f * 0.9f;
				color.R = (byte)((float)color.R * num);
				color.G = (byte)((float)color.G * num2);
			}
			if (Main.atmo < 1f)
			{
				color *= Main.atmo;
			}
			Main.spriteBatch.Draw(asset.Value, new Vector2(theCloud.position.X, cY) + asset.Size() / 2f, null, color * globalCloudAlpha, theCloud.rotation, asset.Size() / 2f, theCloud.scale, theCloud.spriteDir, 0f);
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x00009E46 File Offset: 0x00008046
		public void CloudsEnd()
		{
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x0000357B File Offset: 0x0000177B
		public EmptyHorizonRenderer()
		{
		}
	}
}
