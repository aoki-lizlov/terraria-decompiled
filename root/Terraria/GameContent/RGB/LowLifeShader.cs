using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002BE RID: 702
	public class LowLifeShader : ChromaShader
	{
		// Token: 0x060025BE RID: 9662 RVA: 0x0055958C File Offset: 0x0055778C
		[RgbProcessor(new EffectDetailLevel[] { 0, 1 }, IsTransparent = true)]
		private void ProcessAnyDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			float num = (float)Math.Cos((double)(time * 3.1415927f)) * 0.3f + 0.7f;
			Vector4 vector = LowLifeShader._baseColor * num;
			vector.W = LowLifeShader._baseColor.W;
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x00556259 File Offset: 0x00554459
		public LowLifeShader()
		{
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x005595EC File Offset: 0x005577EC
		// Note: this type is marked as 'beforefieldinit'.
		static LowLifeShader()
		{
		}

		// Token: 0x04005027 RID: 20519
		private static Vector4 _baseColor = new Color(40, 0, 8, 255).ToVector4();
	}
}
