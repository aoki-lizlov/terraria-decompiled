using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000727 RID: 1831
	[Obsolete]
	[Guid("b196b284-bab4-101a-b69c-00aa00341d07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIConnectionPointContainer
	{
		// Token: 0x060041ED RID: 16877
		void EnumConnectionPoints(out UCOMIEnumConnectionPoints ppEnum);

		// Token: 0x060041EE RID: 16878
		void FindConnectionPoint(ref Guid riid, out UCOMIConnectionPoint ppCP);
	}
}
