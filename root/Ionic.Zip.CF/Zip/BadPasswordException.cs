using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000010 RID: 16
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000B")]
	[Serializable]
	public class BadPasswordException : ZipException
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00002523 File Offset: 0x00000723
		public BadPasswordException()
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000252B File Offset: 0x0000072B
		public BadPasswordException(string message)
			: base(message)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002534 File Offset: 0x00000734
		public BadPasswordException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
