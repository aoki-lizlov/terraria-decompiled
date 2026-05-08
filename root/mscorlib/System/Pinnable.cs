using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200019A RID: 410
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class Pinnable<T>
	{
		// Token: 0x06001332 RID: 4914 RVA: 0x000025BE File Offset: 0x000007BE
		public Pinnable()
		{
		}

		// Token: 0x0400132B RID: 4907
		public T Data;
	}
}
