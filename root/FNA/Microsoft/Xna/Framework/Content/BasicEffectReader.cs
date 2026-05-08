using System;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000109 RID: 265
	internal class BasicEffectReader : ContentTypeReader<BasicEffect>
	{
		// Token: 0x0600171B RID: 5915 RVA: 0x00038CCC File Offset: 0x00036ECC
		protected internal override BasicEffect Read(ContentReader input, BasicEffect existingInstance)
		{
			BasicEffect basicEffect = new BasicEffect(input.ContentManager.GetGraphicsDevice());
			Texture2D texture2D = input.ReadExternalReference<Texture>() as Texture2D;
			if (texture2D != null)
			{
				basicEffect.Texture = texture2D;
				basicEffect.TextureEnabled = true;
			}
			basicEffect.DiffuseColor = input.ReadVector3();
			basicEffect.EmissiveColor = input.ReadVector3();
			basicEffect.SpecularColor = input.ReadVector3();
			basicEffect.SpecularPower = input.ReadSingle();
			basicEffect.Alpha = input.ReadSingle();
			basicEffect.VertexColorEnabled = input.ReadBoolean();
			return basicEffect;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00038D50 File Offset: 0x00036F50
		public BasicEffectReader()
		{
		}
	}
}
