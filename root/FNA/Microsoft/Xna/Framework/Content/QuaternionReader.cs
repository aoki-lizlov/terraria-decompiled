using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000126 RID: 294
	internal class QuaternionReader : ContentTypeReader<Quaternion>
	{
		// Token: 0x0600175C RID: 5980 RVA: 0x00039A38 File Offset: 0x00037C38
		internal QuaternionReader()
		{
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00039A40 File Offset: 0x00037C40
		protected internal override Quaternion Read(ContentReader input, Quaternion existingInstance)
		{
			return input.ReadQuaternion();
		}
	}
}
