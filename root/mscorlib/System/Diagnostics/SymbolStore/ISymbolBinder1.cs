using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000A2E RID: 2606
	[ComVisible(true)]
	public interface ISymbolBinder1
	{
		// Token: 0x0600604E RID: 24654
		ISymbolReader GetReader(IntPtr importer, string filename, string searchPath);
	}
}
