using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000093 RID: 147
	[Flags]
	internal enum EffectDirtyFlags
	{
		// Token: 0x04000865 RID: 2149
		WorldViewProj = 1,
		// Token: 0x04000866 RID: 2150
		World = 2,
		// Token: 0x04000867 RID: 2151
		EyePosition = 4,
		// Token: 0x04000868 RID: 2152
		MaterialColor = 8,
		// Token: 0x04000869 RID: 2153
		Fog = 16,
		// Token: 0x0400086A RID: 2154
		FogEnable = 32,
		// Token: 0x0400086B RID: 2155
		AlphaTest = 64,
		// Token: 0x0400086C RID: 2156
		ShaderIndex = 128,
		// Token: 0x0400086D RID: 2157
		All = -1
	}
}
