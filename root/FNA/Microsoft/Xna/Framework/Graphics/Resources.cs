using System;
using System.IO;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200008F RID: 143
	internal class Resources
	{
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00028E52 File Offset: 0x00027052
		public static byte[] AlphaTestEffect
		{
			get
			{
				if (Resources.alphaTestEffect == null)
				{
					Resources.alphaTestEffect = Resources.GetResource("AlphaTestEffect");
				}
				return Resources.alphaTestEffect;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060011D6 RID: 4566 RVA: 0x00028E6F File Offset: 0x0002706F
		public static byte[] BasicEffect
		{
			get
			{
				if (Resources.basicEffect == null)
				{
					Resources.basicEffect = Resources.GetResource("BasicEffect");
				}
				return Resources.basicEffect;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00028E8C File Offset: 0x0002708C
		public static byte[] DualTextureEffect
		{
			get
			{
				if (Resources.dualTextureEffect == null)
				{
					Resources.dualTextureEffect = Resources.GetResource("DualTextureEffect");
				}
				return Resources.dualTextureEffect;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00028EA9 File Offset: 0x000270A9
		public static byte[] EnvironmentMapEffect
		{
			get
			{
				if (Resources.environmentMapEffect == null)
				{
					Resources.environmentMapEffect = Resources.GetResource("EnvironmentMapEffect");
				}
				return Resources.environmentMapEffect;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00028EC6 File Offset: 0x000270C6
		public static byte[] SkinnedEffect
		{
			get
			{
				if (Resources.skinnedEffect == null)
				{
					Resources.skinnedEffect = Resources.GetResource("SkinnedEffect");
				}
				return Resources.skinnedEffect;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x00028EE3 File Offset: 0x000270E3
		public static byte[] SpriteEffect
		{
			get
			{
				if (Resources.spriteEffect == null)
				{
					Resources.spriteEffect = Resources.GetResource("SpriteEffect");
				}
				return Resources.spriteEffect;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00028F00 File Offset: 0x00027100
		public static byte[] YUVToRGBAEffect
		{
			get
			{
				if (Resources.yuvToRGBAEffect == null)
				{
					Resources.yuvToRGBAEffect = Resources.GetResource("YUVToRGBAEffect");
				}
				return Resources.yuvToRGBAEffect;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x00028F1D File Offset: 0x0002711D
		public static byte[] YUVToRGBAEffectR
		{
			get
			{
				if (Resources.yuvToRGBAEffectR == null)
				{
					Resources.yuvToRGBAEffectR = Resources.GetResource("YUVToRGBAEffectR");
				}
				return Resources.yuvToRGBAEffectR;
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00028F3C File Offset: 0x0002713C
		private static byte[] GetResource(string name)
		{
			Stream manifestResourceStream = typeof(Resources).Assembly.GetManifestResourceStream("Microsoft.Xna.Framework.Graphics.Effect.Resources." + name + ".fxb");
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				manifestResourceStream.CopyTo(memoryStream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000136F5 File Offset: 0x000118F5
		public Resources()
		{
		}

		// Token: 0x04000815 RID: 2069
		private static byte[] alphaTestEffect;

		// Token: 0x04000816 RID: 2070
		private static byte[] basicEffect;

		// Token: 0x04000817 RID: 2071
		private static byte[] dualTextureEffect;

		// Token: 0x04000818 RID: 2072
		private static byte[] environmentMapEffect;

		// Token: 0x04000819 RID: 2073
		private static byte[] skinnedEffect;

		// Token: 0x0400081A RID: 2074
		private static byte[] spriteEffect;

		// Token: 0x0400081B RID: 2075
		private static byte[] yuvToRGBAEffect;

		// Token: 0x0400081C RID: 2076
		private static byte[] yuvToRGBAEffectR;
	}
}
