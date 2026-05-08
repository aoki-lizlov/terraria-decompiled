using System;
using System.IO;

namespace System.Reflection.Emit
{
	// Token: 0x020008E0 RID: 2272
	internal struct MonoResource
	{
		// Token: 0x04003057 RID: 12375
		public byte[] data;

		// Token: 0x04003058 RID: 12376
		public string name;

		// Token: 0x04003059 RID: 12377
		public string filename;

		// Token: 0x0400305A RID: 12378
		public ResourceAttributes attrs;

		// Token: 0x0400305B RID: 12379
		public int offset;

		// Token: 0x0400305C RID: 12380
		public Stream stream;
	}
}
