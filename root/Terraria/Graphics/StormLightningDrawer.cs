using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.Utilities.Terraria.Utilities;

namespace Terraria.Graphics
{
	// Token: 0x020001D1 RID: 465
	public struct StormLightningDrawer
	{
		// Token: 0x06001F8A RID: 8074 RVA: 0x0051C738 File Offset: 0x0051A938
		public void Draw(Vector2[] positions, float[] rotations, float width, Color color, float progress, bool isMainBolt, FloatRange progressRange, float intensity)
		{
			this._width = width;
			this._color = color;
			this._isMainBolt = isMainBolt;
			this._progress = progress;
			this._progressRange = progressRange;
			this._taperEnd = this._progressRange.Maximum < 1f;
			MiscShaderData miscShaderData = GameShaders.Misc["StormLightning"];
			miscShaderData.UseSaturation(intensity);
			miscShaderData.UseOpacity(Utils.Remap(this._progress, 0.1f, 0.25f, 0.5f, 1f, true) * Utils.Remap(this._progress, 0.25f, 0.75f, 1f, 0f, true));
			miscShaderData.Apply(null);
			StormLightningDrawer._vertexStrip.PrepareStrip(positions, rotations, new VertexStrip.StripColorFunction(this.StripColors), new VertexStrip.StripHalfWidthFunction(this.StripWidth), -Main.screenPosition, null, false);
			StormLightningDrawer._vertexStrip.DrawTrail();
			Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x0051C87C File Offset: 0x0051AA7C
		private static float WaveTransition(float progressOnStrip, float waveProgress, float transitionLength, float from, float to)
		{
			return Utils.Remap(progressOnStrip, MathHelper.Lerp(-transitionLength, 1f, waveProgress), MathHelper.Lerp(0f, 1f + transitionLength, waveProgress), to, from, true);
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x0051C8A8 File Offset: 0x0051AAA8
		private Color StripColors(float progressOnStrip)
		{
			progressOnStrip = this._progressRange.Lerp(progressOnStrip);
			float num = Utils.Remap(this._progress, 0f, 0.15f, 0f, 1f, true);
			float num2 = StormLightningDrawer.WaveTransition(progressOnStrip, num, 0.5f, 0f, 1f);
			float num3 = Utils.Remap(this._progress, 0.25f, 1f, 0f, 1f, true);
			float num4 = StormLightningDrawer.WaveTransition(progressOnStrip, num3, 0.5f, 1f, 0f);
			float num5 = num2 * num4;
			return this._color * num5;
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x0051C944 File Offset: 0x0051AB44
		private float StripWidth(float progressOnStrip)
		{
			progressOnStrip = this._progressRange.Lerp(progressOnStrip);
			float num = this._width;
			num *= Utils.Remap(progressOnStrip, 0.5f, 1f, 1f, 0.5f, true);
			num *= Utils.Remap(this._progress, 0.5f, 1f, 1f, 0.5f, true);
			if (this._taperEnd)
			{
				num *= Utils.Remap(this._progressRange.Maximum - progressOnStrip, 0.1f, 0f, 1f, this._isMainBolt ? 0.5f : 0f, true);
			}
			return num;
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x0051C9E9 File Offset: 0x0051ABE9
		// Note: this type is marked as 'beforefieldinit'.
		static StormLightningDrawer()
		{
		}

		// Token: 0x04004A1F RID: 18975
		private static VertexStrip _vertexStrip = new VertexStrip();

		// Token: 0x04004A20 RID: 18976
		private float _width;

		// Token: 0x04004A21 RID: 18977
		private Color _color;

		// Token: 0x04004A22 RID: 18978
		private bool _isMainBolt;

		// Token: 0x04004A23 RID: 18979
		private float _progress;

		// Token: 0x04004A24 RID: 18980
		private FloatRange _progressRange;

		// Token: 0x04004A25 RID: 18981
		private bool _taperEnd;
	}
}
