using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200077D RID: 1917
	[Guid("00000010-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IRunningObjectTable
	{
		// Token: 0x060044DC RID: 17628
		int Register(int grfFlags, [MarshalAs(UnmanagedType.Interface)] object punkObject, IMoniker pmkObjectName);

		// Token: 0x060044DD RID: 17629
		void Revoke(int dwRegister);

		// Token: 0x060044DE RID: 17630
		[PreserveSig]
		int IsRunning(IMoniker pmkObjectName);

		// Token: 0x060044DF RID: 17631
		[PreserveSig]
		int GetObject(IMoniker pmkObjectName, [MarshalAs(UnmanagedType.Interface)] out object ppunkObject);

		// Token: 0x060044E0 RID: 17632
		void NoteChangeTime(int dwRegister, ref FILETIME pfiletime);

		// Token: 0x060044E1 RID: 17633
		[PreserveSig]
		int GetTimeOfLastChange(IMoniker pmkObjectName, out FILETIME pfiletime);

		// Token: 0x060044E2 RID: 17634
		void EnumRunning(out IEnumMoniker ppenumMoniker);
	}
}
