using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x020001CD RID: 461
	public struct EmpressBladeDrawer
	{
		// Token: 0x06001F7A RID: 8058 RVA: 0x0051C14C File Offset: 0x0051A34C
		public void Draw(Projectile proj)
		{
			float num = proj.ai[1];
			MiscShaderData miscShaderData = GameShaders.Misc["EmpressBlade"];
			int num2 = 1;
			int num3 = 0;
			int num4 = 0;
			float num5 = 0.6f;
			miscShaderData.UseShaderSpecificData(new Vector4((float)num2, (float)num3, (float)num4, num5));
			miscShaderData.Apply(null);
			EmpressBladeDrawer._vertexStrip.PrepareStrip(proj.oldPos, proj.oldRot, new VertexStrip.StripColorFunction(this.StripColors), new VertexStrip.StripHalfWidthFunction(this.StripWidth), -Main.screenPosition + proj.Size / 2f, new int?(proj.oldPos.Length), true);
			EmpressBladeDrawer._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0051C234 File Offset: 0x0051A434
		private Color StripColors(float progressOnStrip)
		{
			Color color = Color.Lerp(this.ColorStart, this.ColorEnd, Utils.GetLerpValue(0f, 0.7f, progressOnStrip, true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, true));
			color.A /= 2;
			return color;
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x0051C291 File Offset: 0x0051A491
		private float StripWidth(float progressOnStrip)
		{
			return 36f;
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0051C298 File Offset: 0x0051A498
		// Note: this type is marked as 'beforefieldinit'.
		static EmpressBladeDrawer()
		{
		}

		// Token: 0x04004A16 RID: 18966
		public const int TotalIllusions = 1;

		// Token: 0x04004A17 RID: 18967
		public const int FramesPerImportantTrail = 60;

		// Token: 0x04004A18 RID: 18968
		private static VertexStrip _vertexStrip = new VertexStrip();

		// Token: 0x04004A19 RID: 18969
		public Color ColorStart;

		// Token: 0x04004A1A RID: 18970
		public Color ColorEnd;
	}
}
