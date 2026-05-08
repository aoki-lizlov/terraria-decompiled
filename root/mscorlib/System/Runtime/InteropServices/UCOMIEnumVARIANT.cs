using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200072B RID: 1835
	[Obsolete]
	[Guid("00020404-0000-0000-c000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumVARIANT
	{
		// Token: 0x060041FB RID: 16891
		[PreserveSig]
		int Next(int celt, int rgvar, int pceltFetched);

		// Token: 0x060041FC RID: 16892
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060041FD RID: 16893
		[PreserveSig]
		int Reset();

		// Token: 0x060041FE RID: 16894
		void Clone(int ppenum);
	}
}
