using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001F7 RID: 503
	[StructLayout(LayoutKind.Sequential)]
	internal class MonoTypeInfo
	{
		// Token: 0x0600184F RID: 6223 RVA: 0x000025BE File Offset: 0x000007BE
		public MonoTypeInfo()
		{
		}

		// Token: 0x04001599 RID: 5529
		public string full_name;

		// Token: 0x0400159A RID: 5530
		public RuntimeConstructorInfo default_ctor;
	}
}
