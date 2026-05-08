using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x020001D0 RID: 464
	public struct FlameLashDrawer
	{
		// Token: 0x06001F86 RID: 8070 RVA: 0x0051C530 File Offset: 0x0051A730
		public void Draw(Projectile proj)
		{
			this.transitToDark = Utils.GetLerpValue(0f, 6f, proj.localAI[0], true);
			MiscShaderData miscShaderData = GameShaders.Misc["FlameLash"];
			miscShaderData.UseSaturation(-2f);
			miscShaderData.UseOpacity(MathHelper.Lerp(4f, 8f, this.transitToDark));
			miscShaderData.Apply(null);
			FlameLashDrawer._vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, new VertexStrip.StripColorFunction(this.StripColors), new VertexStrip.StripHalfWidthFunction(this.StripWidth), -Main.screenPosition + proj.Size / 2f, false, true);
			FlameLashDrawer._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x0051C628 File Offset: 0x0051A828
		private Color StripColors(float progressOnStrip)
		{
			float lerpValue = Utils.GetLerpValue(0f - 0.1f * this.transitToDark, 0.7f - 0.2f * this.transitToDark, progressOnStrip, true);
			Color color = Color.Lerp(Color.Lerp(Color.White, Color.Orange, this.transitToDark * 0.5f), Color.Red, lerpValue) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
			color.A /= 8;
			return color;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x0051C6B8 File Offset: 0x0051A8B8
		private float StripWidth(float progressOnStrip)
		{
			float num = Utils.GetLerpValue(0f, 0.06f + this.transitToDark * 0.01f, progressOnStrip, true);
			num = 1f - (1f - num) * (1f - num);
			return MathHelper.Lerp(24f + this.transitToDark * 16f, 8f, Utils.GetLerpValue(0f, 1f, progressOnStrip, true)) * num;
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x0051C729 File Offset: 0x0051A929
		// Note: this type is marked as 'beforefieldinit'.
		static FlameLashDrawer()
		{
		}

		// Token: 0x04004A1D RID: 18973
		private static VertexStrip _vertexStrip = new VertexStrip();

		// Token: 0x04004A1E RID: 18974
		private float transitToDark;
	}
}
