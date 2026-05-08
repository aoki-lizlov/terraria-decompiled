using System;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200012C RID: 300
	internal class SkinnedEffectReader : ContentTypeReader<SkinnedEffect>
	{
		// Token: 0x0600176B RID: 5995 RVA: 0x00039EFC File Offset: 0x000380FC
		protected internal override SkinnedEffect Read(ContentReader input, SkinnedEffect existingInstance)
		{
			return new SkinnedEffect(input.ContentManager.GetGraphicsDevice())
			{
				Texture = (input.ReadExternalReference<Texture>() as Texture2D),
				WeightsPerVertex = input.ReadInt32(),
				DiffuseColor = input.ReadVector3(),
				EmissiveColor = input.ReadVector3(),
				SpecularColor = input.ReadVector3(),
				SpecularPower = input.ReadSingle(),
				Alpha = input.ReadSingle()
			};
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00039F72 File Offset: 0x00038172
		public SkinnedEffectReader()
		{
		}
	}
}
