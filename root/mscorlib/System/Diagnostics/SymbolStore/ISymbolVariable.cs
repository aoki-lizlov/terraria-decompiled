using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000A35 RID: 2613
	[ComVisible(true)]
	public interface ISymbolVariable
	{
		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x06006079 RID: 24697
		int AddressField1 { get; }

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x0600607A RID: 24698
		int AddressField2 { get; }

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x0600607B RID: 24699
		int AddressField3 { get; }

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x0600607C RID: 24700
		SymAddressKind AddressKind { get; }

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x0600607D RID: 24701
		object Attributes { get; }

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x0600607E RID: 24702
		int EndOffset { get; }

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x0600607F RID: 24703
		string Name { get; }

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06006080 RID: 24704
		int StartOffset { get; }

		// Token: 0x06006081 RID: 24705
		byte[] GetSignature();
	}
}
