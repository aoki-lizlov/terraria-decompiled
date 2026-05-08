using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000A30 RID: 2608
	[ComVisible(true)]
	public interface ISymbolDocumentWriter
	{
		// Token: 0x06006059 RID: 24665
		void SetCheckSum(Guid algorithmId, byte[] checkSum);

		// Token: 0x0600605A RID: 24666
		void SetSource(byte[] source);
	}
}
