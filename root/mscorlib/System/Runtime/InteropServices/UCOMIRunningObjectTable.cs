using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200072E RID: 1838
	[Obsolete]
	[Guid("00000010-0000-0000-c000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIRunningObjectTable
	{
		// Token: 0x06004219 RID: 16921
		void Register(int grfFlags, [MarshalAs(UnmanagedType.Interface)] object punkObject, UCOMIMoniker pmkObjectName, out int pdwRegister);

		// Token: 0x0600421A RID: 16922
		void Revoke(int dwRegister);

		// Token: 0x0600421B RID: 16923
		void IsRunning(UCOMIMoniker pmkObjectName);

		// Token: 0x0600421C RID: 16924
		void GetObject(UCOMIMoniker pmkObjectName, [MarshalAs(UnmanagedType.Interface)] out object ppunkObject);

		// Token: 0x0600421D RID: 16925
		void NoteChangeTime(int dwRegister, ref FILETIME pfiletime);

		// Token: 0x0600421E RID: 16926
		void GetTimeOfLastChange(UCOMIMoniker pmkObjectName, out FILETIME pfiletime);

		// Token: 0x0600421F RID: 16927
		void EnumRunning(out UCOMIEnumMoniker ppenumMoniker);
	}
}
