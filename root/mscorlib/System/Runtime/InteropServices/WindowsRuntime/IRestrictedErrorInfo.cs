using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x0200075B RID: 1883
	[Guid("82BA7092-4C88-427D-A7BC-16DD93FEB67E")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IRestrictedErrorInfo
	{
		// Token: 0x0600442F RID: 17455
		void GetErrorDetails([MarshalAs(UnmanagedType.BStr)] out string description, out int error, [MarshalAs(UnmanagedType.BStr)] out string restrictedDescription, [MarshalAs(UnmanagedType.BStr)] out string capabilitySid);

		// Token: 0x06004430 RID: 17456
		void GetReference([MarshalAs(UnmanagedType.BStr)] out string reference);
	}
}
