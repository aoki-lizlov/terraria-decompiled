using System;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200011A RID: 282
	internal class EnvironmentMapEffectReader : ContentTypeReader<EnvironmentMapEffect>
	{
		// Token: 0x06001740 RID: 5952 RVA: 0x00039430 File Offset: 0x00037630
		protected internal override EnvironmentMapEffect Read(ContentReader input, EnvironmentMapEffect existingInstance)
		{
			return new EnvironmentMapEffect(input.ContentManager.GetGraphicsDevice())
			{
				Texture = (input.ReadExternalReference<Texture>() as Texture2D),
				EnvironmentMap = input.ReadExternalReference<TextureCube>(),
				EnvironmentMapAmount = input.ReadSingle(),
				EnvironmentMapSpecular = input.ReadVector3(),
				FresnelFactor = input.ReadSingle(),
				DiffuseColor = input.ReadVector3(),
				EmissiveColor = input.ReadVector3(),
				Alpha = input.ReadSingle()
			};
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x000394B2 File Offset: 0x000376B2
		public EnvironmentMapEffectReader()
		{
		}
	}
}
