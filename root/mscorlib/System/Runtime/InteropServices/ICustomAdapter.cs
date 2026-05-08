using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006A7 RID: 1703
	public interface ICustomAdapter
	{
		// Token: 0x06003FC7 RID: 16327
		[return: MarshalAs(UnmanagedType.IUnknown)]
		object GetUnderlyingObject();
	}
}
