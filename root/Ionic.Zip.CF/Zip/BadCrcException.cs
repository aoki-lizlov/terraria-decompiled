using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000012 RID: 18
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00009")]
	[Serializable]
	public class BadCrcException : ZipException
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002559 File Offset: 0x00000759
		public BadCrcException()
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002561 File Offset: 0x00000761
		public BadCrcException(string message)
			: base(message)
		{
		}
	}
}
