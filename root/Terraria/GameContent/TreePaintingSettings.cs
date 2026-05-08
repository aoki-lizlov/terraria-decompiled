using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x02000258 RID: 600
	public class TreePaintingSettings
	{
		// Token: 0x0600234B RID: 9035 RVA: 0x0053D694 File Offset: 0x0053B894
		public void ApplyShader(int paintColor, Effect shader)
		{
			shader.Parameters["leafHueTestOffset"].SetValue(this.HueTestOffset);
			shader.Parameters["leafMinHue"].SetValue(this.SpecialGroupMinimalHueValue);
			shader.Parameters["leafMaxHue"].SetValue(this.SpecialGroupMaximumHueValue);
			shader.Parameters["leafMinSat"].SetValue(this.SpecialGroupMinimumSaturationValue);
			shader.Parameters["leafMaxSat"].SetValue(this.SpecialGroupMaximumSaturationValue);
			shader.Parameters["invertSpecialGroupResult"].SetValue(this.InvertSpecialGroupResult);
			int num = Main.ConvertPaintIdToTileShaderIndex(paintColor, this.UseSpecialGroups, this.UseWallShaderHacks);
			shader.CurrentTechnique.Passes[num].Apply();
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x0000357B File Offset: 0x0000177B
		public TreePaintingSettings()
		{
		}

		// Token: 0x04004D62 RID: 19810
		public float SpecialGroupMinimalHueValue;

		// Token: 0x04004D63 RID: 19811
		public float SpecialGroupMaximumHueValue;

		// Token: 0x04004D64 RID: 19812
		public float SpecialGroupMinimumSaturationValue;

		// Token: 0x04004D65 RID: 19813
		public float SpecialGroupMaximumSaturationValue;

		// Token: 0x04004D66 RID: 19814
		public float HueTestOffset;

		// Token: 0x04004D67 RID: 19815
		public bool UseSpecialGroups;

		// Token: 0x04004D68 RID: 19816
		public bool UseWallShaderHacks;

		// Token: 0x04004D69 RID: 19817
		public bool InvertSpecialGroupResult;
	}
}
