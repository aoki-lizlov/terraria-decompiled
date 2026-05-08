using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000104 RID: 260
	public class ContentLoadException : Exception
	{
		// Token: 0x060016E2 RID: 5858 RVA: 0x00024236 File Offset: 0x00022436
		public ContentLoadException()
		{
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0002423E File Offset: 0x0002243E
		public ContentLoadException(string message)
			: base(message)
		{
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00024247 File Offset: 0x00022447
		public ContentLoadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
