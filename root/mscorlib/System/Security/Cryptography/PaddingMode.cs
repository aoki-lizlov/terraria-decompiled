using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000451 RID: 1105
	[ComVisible(true)]
	[Serializable]
	public enum PaddingMode
	{
		// Token: 0x04002005 RID: 8197
		None = 1,
		// Token: 0x04002006 RID: 8198
		PKCS7,
		// Token: 0x04002007 RID: 8199
		Zeros,
		// Token: 0x04002008 RID: 8200
		ANSIX923,
		// Token: 0x04002009 RID: 8201
		ISO10126
	}
}
