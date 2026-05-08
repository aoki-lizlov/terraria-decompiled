using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Shaders
{
	// Token: 0x020001E7 RID: 487
	public static class EffectParameterExtensions
	{
		// Token: 0x06002083 RID: 8323 RVA: 0x00522D37 File Offset: 0x00520F37
		public static ShaderData.EffectParameter<T> GetParameter<T>(this Effect effect, string name)
		{
			return ShaderData.EffectParameter<T>.Get(effect.Parameters[name]);
		}
	}
}
