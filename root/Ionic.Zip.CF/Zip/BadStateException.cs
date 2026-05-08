using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000014 RID: 20
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00007")]
	[Serializable]
	public class BadStateException : ZipException
	{
		// Token: 0x06000054 RID: 84 RVA: 0x0000257B File Offset: 0x0000077B
		public BadStateException()
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002583 File Offset: 0x00000783
		public BadStateException(string message)
			: base(message)
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000258C File Offset: 0x0000078C
		public BadStateException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
