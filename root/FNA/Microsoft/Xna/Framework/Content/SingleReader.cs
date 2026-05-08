using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200012B RID: 299
	internal class SingleReader : ContentTypeReader<float>
	{
		// Token: 0x06001769 RID: 5993 RVA: 0x00039EEB File Offset: 0x000380EB
		internal SingleReader()
		{
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00039EF3 File Offset: 0x000380F3
		protected internal override float Read(ContentReader input, float existingInstance)
		{
			return input.ReadSingle();
		}
	}
}
