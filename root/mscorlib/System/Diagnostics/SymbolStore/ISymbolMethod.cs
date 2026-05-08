using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000A31 RID: 2609
	[ComVisible(true)]
	public interface ISymbolMethod
	{
		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x0600605B RID: 24667
		ISymbolScope RootScope { get; }

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x0600605C RID: 24668
		int SequencePointCount { get; }

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x0600605D RID: 24669
		SymbolToken Token { get; }

		// Token: 0x0600605E RID: 24670
		ISymbolNamespace GetNamespace();

		// Token: 0x0600605F RID: 24671
		int GetOffset(ISymbolDocument document, int line, int column);

		// Token: 0x06006060 RID: 24672
		ISymbolVariable[] GetParameters();

		// Token: 0x06006061 RID: 24673
		int[] GetRanges(ISymbolDocument document, int line, int column);

		// Token: 0x06006062 RID: 24674
		ISymbolScope GetScope(int offset);

		// Token: 0x06006063 RID: 24675
		void GetSequencePoints(int[] offsets, ISymbolDocument[] documents, int[] lines, int[] columns, int[] endLines, int[] endColumns);

		// Token: 0x06006064 RID: 24676
		bool GetSourceStartEnd(ISymbolDocument[] docs, int[] lines, int[] columns);
	}
}
