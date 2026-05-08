using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
	// Token: 0x02000291 RID: 657
	public class LegacyHairShaderData : HairShaderData
	{
		// Token: 0x06002531 RID: 9521 RVA: 0x00553A79 File Offset: 0x00551C79
		public LegacyHairShaderData()
			: base(null, null)
		{
			this._shaderDisabled = true;
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x00553A8C File Offset: 0x00551C8C
		public override Color GetColor(Player player, Color lightColor)
		{
			bool flag = true;
			Color color = this._colorProcessor(player, player.hairColor, ref flag);
			if (flag)
			{
				return new Color(color.ToVector4() * lightColor.ToVector4());
			}
			return color;
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x00553ACD File Offset: 0x00551CCD
		public LegacyHairShaderData UseLegacyMethod(LegacyHairShaderData.ColorProcessingMethod colorProcessor)
		{
			this._colorProcessor = colorProcessor;
			return this;
		}

		// Token: 0x04004F8F RID: 20367
		private LegacyHairShaderData.ColorProcessingMethod _colorProcessor;

		// Token: 0x0200080F RID: 2063
		// (Invoke) Token: 0x060042EA RID: 17130
		public delegate Color ColorProcessingMethod(Player player, Color color, ref bool lighting);
	}
}
