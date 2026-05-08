using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006A9 RID: 1705
	public interface ICustomMarshaler
	{
		// Token: 0x06003FC9 RID: 16329
		object MarshalNativeToManaged(IntPtr pNativeData);

		// Token: 0x06003FCA RID: 16330
		IntPtr MarshalManagedToNative(object ManagedObj);

		// Token: 0x06003FCB RID: 16331
		void CleanUpNativeData(IntPtr pNativeData);

		// Token: 0x06003FCC RID: 16332
		void CleanUpManagedData(object ManagedObj);

		// Token: 0x06003FCD RID: 16333
		int GetNativeDataSize();
	}
}
