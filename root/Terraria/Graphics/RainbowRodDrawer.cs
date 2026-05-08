using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x020001D2 RID: 466
	public struct RainbowRodDrawer
	{
		// Token: 0x06001F8F RID: 8079 RVA: 0x0051C9F8 File Offset: 0x0051ABF8
		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["RainbowRod"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(4f);
			miscShaderData.Apply(null);
			RainbowRodDrawer._vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, new VertexStrip.StripColorFunction(this.StripColors), new VertexStrip.StripHalfWidthFunction(this.StripWidth), -Main.screenPosition + proj.Size / 2f, false, true);
			RainbowRodDrawer._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x0051CAC4 File Offset: 0x0051ACC4
		private Color StripColors(float progressOnStrip)
		{
			Color color = Main.hslToRgb((progressOnStrip * 1.6f - Main.GlobalTimeWrappedHourly) % 1f, 1f, 0.5f, byte.MaxValue);
			Color color2 = Color.Lerp(Color.White, color, Utils.GetLerpValue(-0.2f, 0.5f, progressOnStrip, true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
			color2.A = 0;
			return color2;
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x0051CB3C File Offset: 0x0051AD3C
		private float StripWidth(float progressOnStrip)
		{
			float num = 1f;
			float lerpValue = Utils.GetLerpValue(0f, 0.2f, progressOnStrip, true);
			num *= 1f - (1f - lerpValue) * (1f - lerpValue);
			return MathHelper.Lerp(0f, 32f, num);
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0051CB89 File Offset: 0x0051AD89
		// Note: this type is marked as 'beforefieldinit'.
		static RainbowRodDrawer()
		{
		}

		// Token: 0x04004A26 RID: 18982
		private static VertexStrip _vertexStrip = new VertexStrip();
	}
}
