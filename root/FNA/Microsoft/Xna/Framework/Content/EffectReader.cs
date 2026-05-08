using System;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000118 RID: 280
	internal class EffectReader : ContentTypeReader<Effect>
	{
		// Token: 0x0600173B RID: 5947 RVA: 0x000393B0 File Offset: 0x000375B0
		protected internal override Effect Read(ContentReader input, Effect existingInstance)
		{
			int num = input.ReadInt32();
			return new Effect(input.ContentManager.GetGraphicsDevice(), input.ReadBytes(num))
			{
				Name = input.AssetName
			};
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x000393E7 File Offset: 0x000375E7
		public EffectReader()
		{
		}
	}
}
