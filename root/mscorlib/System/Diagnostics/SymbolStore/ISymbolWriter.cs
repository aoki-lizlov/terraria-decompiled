using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000A36 RID: 2614
	[ComVisible(true)]
	public interface ISymbolWriter
	{
		// Token: 0x06006082 RID: 24706
		void Close();

		// Token: 0x06006083 RID: 24707
		void CloseMethod();

		// Token: 0x06006084 RID: 24708
		void CloseNamespace();

		// Token: 0x06006085 RID: 24709
		void CloseScope(int endOffset);

		// Token: 0x06006086 RID: 24710
		ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType);

		// Token: 0x06006087 RID: 24711
		void DefineField(SymbolToken parent, string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3);

		// Token: 0x06006088 RID: 24712
		void DefineGlobalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3);

		// Token: 0x06006089 RID: 24713
		void DefineLocalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset);

		// Token: 0x0600608A RID: 24714
		void DefineParameter(string name, ParameterAttributes attributes, int sequence, SymAddressKind addrKind, int addr1, int addr2, int addr3);

		// Token: 0x0600608B RID: 24715
		void DefineSequencePoints(ISymbolDocumentWriter document, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns);

		// Token: 0x0600608C RID: 24716
		void Initialize(IntPtr emitter, string filename, bool fFullBuild);

		// Token: 0x0600608D RID: 24717
		void OpenMethod(SymbolToken method);

		// Token: 0x0600608E RID: 24718
		void OpenNamespace(string name);

		// Token: 0x0600608F RID: 24719
		int OpenScope(int startOffset);

		// Token: 0x06006090 RID: 24720
		void SetMethodSourceRange(ISymbolDocumentWriter startDoc, int startLine, int startColumn, ISymbolDocumentWriter endDoc, int endLine, int endColumn);

		// Token: 0x06006091 RID: 24721
		void SetScopeRange(int scopeID, int startOffset, int endOffset);

		// Token: 0x06006092 RID: 24722
		void SetSymAttribute(SymbolToken parent, string name, byte[] data);

		// Token: 0x06006093 RID: 24723
		void SetUnderlyingWriter(IntPtr underlyingWriter);

		// Token: 0x06006094 RID: 24724
		void SetUserEntryPoint(SymbolToken entryMethod);

		// Token: 0x06006095 RID: 24725
		void UsingNamespace(string fullName);
	}
}
