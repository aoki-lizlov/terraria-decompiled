using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200011D RID: 285
	internal class Int16Reader : ContentTypeReader<short>
	{
		// Token: 0x06001746 RID: 5958 RVA: 0x00039535 File Offset: 0x00037735
		internal Int16Reader()
		{
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0003953D File Offset: 0x0003773D
		protected internal override short Read(ContentReader input, short existingInstance)
		{
			return input.ReadInt16();
		}
	}
}
