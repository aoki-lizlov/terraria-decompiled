using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000A33 RID: 2611
	[ComVisible(true)]
	public interface ISymbolReader
	{
		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06006068 RID: 24680
		SymbolToken UserEntryPoint { get; }

		// Token: 0x06006069 RID: 24681
		ISymbolDocument GetDocument(string url, Guid language, Guid languageVendor, Guid documentType);

		// Token: 0x0600606A RID: 24682
		ISymbolDocument[] GetDocuments();

		// Token: 0x0600606B RID: 24683
		ISymbolVariable[] GetGlobalVariables();

		// Token: 0x0600606C RID: 24684
		ISymbolMethod GetMethod(SymbolToken method);

		// Token: 0x0600606D RID: 24685
		ISymbolMethod GetMethod(SymbolToken method, int version);

		// Token: 0x0600606E RID: 24686
		ISymbolMethod GetMethodFromDocumentPosition(ISymbolDocument document, int line, int column);

		// Token: 0x0600606F RID: 24687
		ISymbolNamespace[] GetNamespaces();

		// Token: 0x06006070 RID: 24688
		byte[] GetSymAttribute(SymbolToken parent, string name);

		// Token: 0x06006071 RID: 24689
		ISymbolVariable[] GetVariables(SymbolToken parent);
	}
}
