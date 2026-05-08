using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x0200000F RID: 15
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00006")]
	[Serializable]
	public class ZipException : Exception
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002508 File Offset: 0x00000708
		public ZipException()
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002510 File Offset: 0x00000710
		public ZipException(string message)
			: base(message)
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002519 File Offset: 0x00000719
		public ZipException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
