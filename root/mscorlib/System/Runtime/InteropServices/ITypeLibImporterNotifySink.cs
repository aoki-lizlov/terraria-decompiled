using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000714 RID: 1812
	[ComVisible(true)]
	[Guid("f1c3bf76-c3e4-11d3-88e7-00902754c43a")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface ITypeLibImporterNotifySink
	{
		// Token: 0x060040DA RID: 16602
		void ReportEvent(ImporterEventKind eventKind, int eventCode, string eventMsg);

		// Token: 0x060040DB RID: 16603
		Assembly ResolveRef([MarshalAs(UnmanagedType.Interface)] object typeLib);
	}
}
