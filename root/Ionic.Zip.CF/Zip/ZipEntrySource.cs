using System;

namespace Ionic.Zip
{
	// Token: 0x02000023 RID: 35
	public enum ZipEntrySource
	{
		// Token: 0x040000A9 RID: 169
		None,
		// Token: 0x040000AA RID: 170
		FileSystem,
		// Token: 0x040000AB RID: 171
		Stream,
		// Token: 0x040000AC RID: 172
		ZipFile,
		// Token: 0x040000AD RID: 173
		WriteDelegate,
		// Token: 0x040000AE RID: 174
		JitStream,
		// Token: 0x040000AF RID: 175
		ZipOutputStream
	}
}
