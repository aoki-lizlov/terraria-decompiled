using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x0200076D RID: 1901
	[Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IConnectionPointContainer
	{
		// Token: 0x0600448E RID: 17550
		void EnumConnectionPoints(out IEnumConnectionPoints ppEnum);

		// Token: 0x0600448F RID: 17551
		void FindConnectionPoint([In] ref Guid riid, out IConnectionPoint ppCP);
	}
}
