using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006A8 RID: 1704
	public interface ICustomFactory
	{
		// Token: 0x06003FC8 RID: 16328
		MarshalByRefObject CreateInstance(Type serverType);
	}
}
