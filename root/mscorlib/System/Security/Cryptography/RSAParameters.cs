using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000479 RID: 1145
	[ComVisible(true)]
	[Serializable]
	public struct RSAParameters
	{
		// Token: 0x04002083 RID: 8323
		public byte[] Exponent;

		// Token: 0x04002084 RID: 8324
		public byte[] Modulus;

		// Token: 0x04002085 RID: 8325
		[NonSerialized]
		public byte[] P;

		// Token: 0x04002086 RID: 8326
		[NonSerialized]
		public byte[] Q;

		// Token: 0x04002087 RID: 8327
		[NonSerialized]
		public byte[] DP;

		// Token: 0x04002088 RID: 8328
		[NonSerialized]
		public byte[] DQ;

		// Token: 0x04002089 RID: 8329
		[NonSerialized]
		public byte[] InverseQ;

		// Token: 0x0400208A RID: 8330
		[NonSerialized]
		public byte[] D;
	}
}
