using System;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x0200002C RID: 44
	internal struct ShaderOperation
	{
		// Token: 0x06000135 RID: 309 RVA: 0x000055DC File Offset: 0x000037DC
		public ShaderOperation(ChromaShader shader, ShaderBlendState blendState, EffectDetailLevel detailLevel)
		{
			this.BlendState = blendState;
			this._shader = shader;
			this._detailLevel = detailLevel;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000055F3 File Offset: 0x000037F3
		public void Process(RgbDevice device, Fragment fragment, float time)
		{
			this._shader.Process(device, fragment, this._detailLevel, time);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005609 File Offset: 0x00003809
		public ShaderOperation WithBlendState(ShaderBlendState blendState)
		{
			return new ShaderOperation(this._shader, blendState, this._detailLevel);
		}

		// Token: 0x04000075 RID: 117
		public readonly ShaderBlendState BlendState;

		// Token: 0x04000076 RID: 118
		private readonly ChromaShader _shader;

		// Token: 0x04000077 RID: 119
		private readonly EffectDetailLevel _detailLevel;
	}
}
