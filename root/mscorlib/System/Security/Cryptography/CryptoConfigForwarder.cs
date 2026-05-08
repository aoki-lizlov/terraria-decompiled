using System;

namespace System.Security.Cryptography
{
	// Token: 0x020004A5 RID: 1189
	internal static class CryptoConfigForwarder
	{
		// Token: 0x06003110 RID: 12560 RVA: 0x000B679A File Offset: 0x000B499A
		internal static object CreateFromName(string name)
		{
			return CryptoConfig.CreateFromName(name);
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000B67A2 File Offset: 0x000B49A2
		internal static HashAlgorithm CreateDefaultHashAlgorithm()
		{
			return (HashAlgorithm)CryptoConfigForwarder.CreateFromName("System.Security.Cryptography.HashAlgorithm");
		}
	}
}
