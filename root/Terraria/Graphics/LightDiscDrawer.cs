using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x020001CF RID: 463
	public struct LightDiscDrawer
	{
		// Token: 0x06001F82 RID: 8066 RVA: 0x0051C40C File Offset: 0x0051A60C
		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["LightDisc"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(2f);
			miscShaderData.Apply(null);
			LightDiscDrawer._vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, new VertexStrip.StripColorFunction(this.StripColors), new VertexStrip.StripHalfWidthFunction(this.StripWidth), -Main.screenPosition + proj.Size / 2f, false, true);
			LightDiscDrawer._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x0051C4D8 File Offset: 0x0051A6D8
		private Color StripColors(float progressOnStrip)
		{
			float num = 1f - progressOnStrip;
			Color color = new Color(48, 63, 150) * (num * num * num * num) * 0.5f;
			color.A = 0;
			return color;
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x0051C51B File Offset: 0x0051A71B
		private float StripWidth(float progressOnStrip)
		{
			return 16f;
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x0051C522 File Offset: 0x0051A722
		// Note: this type is marked as 'beforefieldinit'.
		static LightDiscDrawer()
		{
		}

		// Token: 0x04004A1C RID: 18972
		private static VertexStrip _vertexStrip = new VertexStrip();
	}
}
