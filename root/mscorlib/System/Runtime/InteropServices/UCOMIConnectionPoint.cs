using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000726 RID: 1830
	[Obsolete]
	[Guid("b196b286-bab4-101a-b69c-00aa00341d07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIConnectionPoint
	{
		// Token: 0x060041E8 RID: 16872
		void GetConnectionInterface(out Guid pIID);

		// Token: 0x060041E9 RID: 16873
		void GetConnectionPointContainer(out UCOMIConnectionPointContainer ppCPC);

		// Token: 0x060041EA RID: 16874
		void Advise([MarshalAs(UnmanagedType.Interface)] object pUnkSink, out int pdwCookie);

		// Token: 0x060041EB RID: 16875
		void Unadvise(int dwCookie);

		// Token: 0x060041EC RID: 16876
		void EnumConnections(out UCOMIEnumConnections ppEnum);
	}
}
