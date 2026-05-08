using System;
using System.Runtime.InteropServices;

namespace Ionic.Zlib
{
	// Token: 0x0200004D RID: 77
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000E")]
	public class ZlibException : Exception
	{
		// Token: 0x06000394 RID: 916 RVA: 0x0001ADD1 File Offset: 0x00018FD1
		public ZlibException()
		{
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001ADD9 File Offset: 0x00018FD9
		public ZlibException(string s)
			: base(s)
		{
		}
	}
}
