using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000A32 RID: 2610
	[ComVisible(true)]
	public interface ISymbolNamespace
	{
		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06006065 RID: 24677
		string Name { get; }

		// Token: 0x06006066 RID: 24678
		ISymbolNamespace[] GetNamespaces();

		// Token: 0x06006067 RID: 24679
		ISymbolVariable[] GetVariables();
	}
}
