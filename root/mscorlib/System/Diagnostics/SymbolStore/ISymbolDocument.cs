using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000A2F RID: 2607
	[ComVisible(true)]
	public interface ISymbolDocument
	{
		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x0600604F RID: 24655
		Guid CheckSumAlgorithmId { get; }

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06006050 RID: 24656
		Guid DocumentType { get; }

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06006051 RID: 24657
		bool HasEmbeddedSource { get; }

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06006052 RID: 24658
		Guid Language { get; }

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06006053 RID: 24659
		Guid LanguageVendor { get; }

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06006054 RID: 24660
		int SourceLength { get; }

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06006055 RID: 24661
		string URL { get; }

		// Token: 0x06006056 RID: 24662
		int FindClosestLine(int line);

		// Token: 0x06006057 RID: 24663
		byte[] GetCheckSum();

		// Token: 0x06006058 RID: 24664
		byte[] GetSourceRange(int startLine, int startColumn, int endLine, int endColumn);
	}
}
