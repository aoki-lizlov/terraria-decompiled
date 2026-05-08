using System;

namespace System.Security.Policy
{
	// Token: 0x020003E5 RID: 997
	internal interface IBuiltInEvidence
	{
		// Token: 0x06002A65 RID: 10853
		int GetRequiredSize(bool verbose);

		// Token: 0x06002A66 RID: 10854
		int InitFromBuffer(char[] buffer, int position);

		// Token: 0x06002A67 RID: 10855
		int OutputToBuffer(char[] buffer, int position, bool verbose);
	}
}
