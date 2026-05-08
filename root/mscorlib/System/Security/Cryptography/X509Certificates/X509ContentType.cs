using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020004A8 RID: 1192
	public enum X509ContentType
	{
		// Token: 0x0400220D RID: 8717
		Unknown,
		// Token: 0x0400220E RID: 8718
		Cert,
		// Token: 0x0400220F RID: 8719
		SerializedCert,
		// Token: 0x04002210 RID: 8720
		Pfx,
		// Token: 0x04002211 RID: 8721
		Pkcs12 = 3,
		// Token: 0x04002212 RID: 8722
		SerializedStore,
		// Token: 0x04002213 RID: 8723
		Pkcs7,
		// Token: 0x04002214 RID: 8724
		Authenticode
	}
}
