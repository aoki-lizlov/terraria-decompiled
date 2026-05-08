using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200010C RID: 268
	internal class BoundingFrustumReader : ContentTypeReader<BoundingFrustum>
	{
		// Token: 0x06001721 RID: 5921 RVA: 0x00038D83 File Offset: 0x00036F83
		internal BoundingFrustumReader()
		{
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00038D8B File Offset: 0x00036F8B
		protected internal override BoundingFrustum Read(ContentReader input, BoundingFrustum existingInstance)
		{
			return new BoundingFrustum(input.ReadMatrix());
		}
	}
}
