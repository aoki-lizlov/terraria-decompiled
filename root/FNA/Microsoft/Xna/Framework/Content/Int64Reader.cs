using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x0200011F RID: 287
	internal class Int64Reader : ContentTypeReader<long>
	{
		// Token: 0x0600174A RID: 5962 RVA: 0x00039555 File Offset: 0x00037755
		internal Int64Reader()
		{
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0003955D File Offset: 0x0003775D
		protected internal override long Read(ContentReader input, long existingInstance)
		{
			return input.ReadInt64();
		}
	}
}
