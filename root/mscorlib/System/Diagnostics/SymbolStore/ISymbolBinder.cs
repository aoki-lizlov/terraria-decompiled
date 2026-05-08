using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000A2D RID: 2605
	[ComVisible(true)]
	public interface ISymbolBinder
	{
		// Token: 0x0600604D RID: 24653
		[Obsolete("This interface is not 64-bit clean.  Use ISymbolBinder1 instead")]
		ISymbolReader GetReader(int importer, string filename, string searchPath);
	}
}
