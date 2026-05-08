using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200013B RID: 315
	internal class Vector4Reader : ContentTypeReader<Vector4>
	{
		// Token: 0x0600178D RID: 6029 RVA: 0x0003A798 File Offset: 0x00038998
		internal Vector4Reader()
		{
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0003A7A0 File Offset: 0x000389A0
		protected internal override Vector4 Read(ContentReader input, Vector4 existingInstance)
		{
			return input.ReadVector4();
		}
	}
}
