using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.Graphics
{
	// Token: 0x020001CE RID: 462
	public struct MagicMissileDrawer
	{
		// Token: 0x06001F7E RID: 8062 RVA: 0x0051C2A4 File Offset: 0x0051A4A4
		public void Draw(Projectile proj)
		{
			MiscShaderData miscShaderData = GameShaders.Misc["MagicMissile"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(2f);
			miscShaderData.Apply(null);
			MagicMissileDrawer._vertexStrip.PrepareStripWithProceduralPadding(proj.oldPos, proj.oldRot, new VertexStrip.StripColorFunction(this.StripColors), new VertexStrip.StripHalfWidthFunction(this.StripWidth), -Main.screenPosition + proj.Size / 2f, false, true);
			MagicMissileDrawer._vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x0051C370 File Offset: 0x0051A570
		private Color StripColors(float progressOnStrip)
		{
			Color color = Color.Lerp(Color.White, Color.Violet, Utils.GetLerpValue(0f, 0.7f, progressOnStrip, true)) * (1f - Utils.GetLerpValue(0f, 0.98f, progressOnStrip, false));
			color.A /= 2;
			return color;
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x0051C3CB File Offset: 0x0051A5CB
		private float StripWidth(float progressOnStrip)
		{
			return MathHelper.Lerp(26f, 32f, Utils.GetLerpValue(0f, 0.2f, progressOnStrip, true)) * Utils.GetLerpValue(0f, 0.07f, progressOnStrip, true);
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x0051C3FF File Offset: 0x0051A5FF
		// Note: this type is marked as 'beforefieldinit'.
		static MagicMissileDrawer()
		{
		}

		// Token: 0x04004A1B RID: 18971
		private static VertexStrip _vertexStrip = new VertexStrip();
	}
}
