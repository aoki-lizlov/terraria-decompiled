using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002BD RID: 701
	internal class KeybindsMenuShader : ChromaShader
	{
		// Token: 0x060025BB RID: 9659 RVA: 0x00559500 File Offset: 0x00557700
		[RgbProcessor(new EffectDetailLevel[] { 0, 1 }, IsTransparent = true)]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			float num = (float)Math.Cos((double)(time * 1.5707964f)) * 0.2f + 0.8f;
			Vector4 vector = KeybindsMenuShader._baseColor * num;
			vector.W = KeybindsMenuShader._baseColor.W;
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x00556259 File Offset: 0x00554459
		public KeybindsMenuShader()
		{
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x00559560 File Offset: 0x00557760
		// Note: this type is marked as 'beforefieldinit'.
		static KeybindsMenuShader()
		{
		}

		// Token: 0x04005026 RID: 20518
		private static Vector4 _baseColor = new Color(20, 20, 20, 245).ToVector4();
	}
}
