using System;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000107 RID: 263
	internal class AlphaTestEffectReader : ContentTypeReader<AlphaTestEffect>
	{
		// Token: 0x06001716 RID: 5910 RVA: 0x00038B90 File Offset: 0x00036D90
		protected internal override AlphaTestEffect Read(ContentReader input, AlphaTestEffect existingInstance)
		{
			return new AlphaTestEffect(input.ContentManager.GetGraphicsDevice())
			{
				Texture = (input.ReadExternalReference<Texture>() as Texture2D),
				AlphaFunction = (CompareFunction)input.ReadInt32(),
				ReferenceAlpha = (int)input.ReadUInt32(),
				DiffuseColor = input.ReadVector3(),
				Alpha = input.ReadSingle(),
				VertexColorEnabled = input.ReadBoolean()
			};
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00038BFA File Offset: 0x00036DFA
		public AlphaTestEffectReader()
		{
		}
	}
}
