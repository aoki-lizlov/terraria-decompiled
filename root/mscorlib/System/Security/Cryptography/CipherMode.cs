using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000450 RID: 1104
	[ComVisible(true)]
	[Serializable]
	public enum CipherMode
	{
		// Token: 0x04001FFF RID: 8191
		CBC = 1,
		// Token: 0x04002000 RID: 8192
		ECB,
		// Token: 0x04002001 RID: 8193
		OFB,
		// Token: 0x04002002 RID: 8194
		CFB,
		// Token: 0x04002003 RID: 8195
		CTS
	}
}
