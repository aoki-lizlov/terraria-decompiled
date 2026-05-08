using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000713 RID: 1811
	[ComVisible(true)]
	[Guid("f1c3bf77-c3e4-11d3-88e7-00902754c43a")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface ITypeLibExporterNotifySink
	{
		// Token: 0x060040D8 RID: 16600
		void ReportEvent(ExporterEventKind eventKind, int eventCode, string eventMsg);

		// Token: 0x060040D9 RID: 16601
		[return: MarshalAs(UnmanagedType.Interface)]
		object ResolveRef(Assembly assembly);
	}
}
