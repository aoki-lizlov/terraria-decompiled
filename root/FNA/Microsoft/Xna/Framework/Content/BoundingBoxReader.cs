using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200010B RID: 267
	internal class BoundingBoxReader : ContentTypeReader<BoundingBox>
	{
		// Token: 0x0600171F RID: 5919 RVA: 0x00038D68 File Offset: 0x00036F68
		protected internal override BoundingBox Read(ContentReader input, BoundingBox existingInstance)
		{
			return new BoundingBox(input.ReadVector3(), input.ReadVector3());
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x00038D7B File Offset: 0x00036F7B
		public BoundingBoxReader()
		{
		}
	}
}
