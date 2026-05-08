using System;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000116 RID: 278
	internal class DualTextureEffectReader : ContentTypeReader<DualTextureEffect>
	{
		// Token: 0x06001737 RID: 5943 RVA: 0x00039030 File Offset: 0x00037230
		protected internal override DualTextureEffect Read(ContentReader input, DualTextureEffect existingInstance)
		{
			return new DualTextureEffect(input.ContentManager.GetGraphicsDevice())
			{
				Texture = (input.ReadExternalReference<Texture>() as Texture2D),
				Texture2 = (input.ReadExternalReference<Texture>() as Texture2D),
				DiffuseColor = input.ReadVector3(),
				Alpha = input.ReadSingle(),
				VertexColorEnabled = input.ReadBoolean()
			};
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00039093 File Offset: 0x00037293
		public DualTextureEffectReader()
		{
		}
	}
}
