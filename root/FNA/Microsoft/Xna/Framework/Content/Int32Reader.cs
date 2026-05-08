using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200011E RID: 286
	internal class Int32Reader : ContentTypeReader<int>
	{
		// Token: 0x06001748 RID: 5960 RVA: 0x00039545 File Offset: 0x00037745
		internal Int32Reader()
		{
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0003954D File Offset: 0x0003774D
		protected internal override int Read(ContentReader input, int existingInstance)
		{
			return input.ReadInt32();
		}
	}
}
