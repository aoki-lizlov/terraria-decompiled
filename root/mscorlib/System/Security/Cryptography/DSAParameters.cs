using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200045A RID: 1114
	[ComVisible(true)]
	[Serializable]
	public struct DSAParameters
	{
		// Token: 0x04002024 RID: 8228
		public byte[] P;

		// Token: 0x04002025 RID: 8229
		public byte[] Q;

		// Token: 0x04002026 RID: 8230
		public byte[] G;

		// Token: 0x04002027 RID: 8231
		public byte[] Y;

		// Token: 0x04002028 RID: 8232
		public byte[] J;

		// Token: 0x04002029 RID: 8233
		[NonSerialized]
		public byte[] X;

		// Token: 0x0400202A RID: 8234
		public byte[] Seed;

		// Token: 0x0400202B RID: 8235
		public int Counter;
	}
}
