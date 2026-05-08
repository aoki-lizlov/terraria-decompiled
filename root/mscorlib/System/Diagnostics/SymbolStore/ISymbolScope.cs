using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000A34 RID: 2612
	[ComVisible(true)]
	public interface ISymbolScope
	{
		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06006072 RID: 24690
		int EndOffset { get; }

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06006073 RID: 24691
		ISymbolMethod Method { get; }

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06006074 RID: 24692
		ISymbolScope Parent { get; }

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06006075 RID: 24693
		int StartOffset { get; }

		// Token: 0x06006076 RID: 24694
		ISymbolScope[] GetChildren();

		// Token: 0x06006077 RID: 24695
		ISymbolVariable[] GetLocals();

		// Token: 0x06006078 RID: 24696
		ISymbolNamespace[] GetNamespaces();
	}
}
