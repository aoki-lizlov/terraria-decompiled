using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020004A9 RID: 1193
	[Flags]
	public enum X509KeyStorageFlags
	{
		// Token: 0x04002216 RID: 8726
		DefaultKeySet = 0,
		// Token: 0x04002217 RID: 8727
		UserKeySet = 1,
		// Token: 0x04002218 RID: 8728
		MachineKeySet = 2,
		// Token: 0x04002219 RID: 8729
		Exportable = 4,
		// Token: 0x0400221A RID: 8730
		UserProtected = 8,
		// Token: 0x0400221B RID: 8731
		PersistKeySet = 16,
		// Token: 0x0400221C RID: 8732
		EphemeralKeySet = 32
	}
}
