using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002C0 RID: 704
	public class MoonShader : ChromaShader
	{
		// Token: 0x060025C4 RID: 9668 RVA: 0x0055979C File Offset: 0x0055799C
		public MoonShader(Color skyColor, Color moonRingColor, Color moonCoreColor)
			: this(skyColor, moonRingColor, moonCoreColor, Color.White)
		{
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x005597AC File Offset: 0x005579AC
		public MoonShader(Color skyColor, Color moonColor)
			: this(skyColor, moonColor, moonColor)
		{
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x005597B7 File Offset: 0x005579B7
		public MoonShader(Color skyColor, Color moonRingColor, Color moonCoreColor, Color cloudColor)
		{
			this._skyColor = skyColor.ToVector4();
			this._moonRingColor = moonRingColor.ToVector4();
			this._moonCoreColor = moonCoreColor.ToVector4();
			this._cloudColor = cloudColor.ToVector4();
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x005597F3 File Offset: 0x005579F3
		public override void Update(float elapsedTime)
		{
			if (Main.dayTime)
			{
				this._progress = (float)(Main.time / 54000.0);
				return;
			}
			this._progress = (float)(Main.time / 32400.0);
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x0055982C File Offset: 0x00557A2C
		[RgbProcessor(new EffectDetailLevel[] { 0 })]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = NoiseHelper.GetDynamicNoise(fragment.GetCanvasPositionOfIndex(i) * new Vector2(0.1f, 0.5f) + new Vector2(time * 0.02f, 0f), time / 40f);
				num = (float)Math.Sqrt((double)Math.Max(0f, 1f - 2f * num));
				Vector4 vector = Vector4.Lerp(this._skyColor, this._cloudColor, num * 0.1f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x005598D0 File Offset: 0x00557AD0
		[RgbProcessor(new EffectDetailLevel[] { 1 })]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			if (device.Type != null && device.Type != 6)
			{
				this.ProcessLowDetail(device, fragment, quality, time);
				return;
			}
			Vector2 vector = new Vector2(2f, 0.5f);
			Vector2 vector2 = new Vector2(2.5f, 1f);
			float num = this._progress * 3.1415927f + 3.1415927f;
			Vector2 vector3 = new Vector2((float)Math.Cos((double)num), (float)Math.Sin((double)num)) * vector2 + vector;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num2 = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * new Vector2(0.1f, 0.5f) + new Vector2(time * 0.02f, 0f), time / 40f);
				num2 = (float)Math.Sqrt((double)Math.Max(0f, 1f - 2f * num2));
				float num3 = (canvasPositionOfIndex - vector3).Length();
				Vector4 vector4 = Vector4.Lerp(this._skyColor, this._cloudColor, num2 * 0.15f);
				if (num3 < 0.8f)
				{
					vector4 = Vector4.Lerp(this._moonRingColor, this._moonCoreColor, Math.Min(0.1f, 0.8f - num3) / 0.1f);
				}
				else if (num3 < 1f)
				{
					vector4 = Vector4.Lerp(vector4, this._moonRingColor, Math.Min(0.2f, 1f - num3) / 0.2f);
				}
				fragment.SetColor(i, vector4);
			}
		}

		// Token: 0x0400502B RID: 20523
		private readonly Vector4 _moonCoreColor;

		// Token: 0x0400502C RID: 20524
		private readonly Vector4 _moonRingColor;

		// Token: 0x0400502D RID: 20525
		private readonly Vector4 _skyColor;

		// Token: 0x0400502E RID: 20526
		private readonly Vector4 _cloudColor;

		// Token: 0x0400502F RID: 20527
		private float _progress;
	}
}
