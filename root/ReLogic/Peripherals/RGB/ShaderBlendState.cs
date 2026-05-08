using System;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x02000027 RID: 39
	public struct ShaderBlendState
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00004A66 File Offset: 0x00002C66
		public ShaderBlendState(BlendMode blendMode, float alpha = 1f)
		{
			this.GlobalOpacity = alpha;
			this.Mode = blendMode;
		}

		// Token: 0x04000067 RID: 103
		public readonly float GlobalOpacity;

		// Token: 0x04000068 RID: 104
		public readonly BlendMode Mode;
	}
}
