using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000437 RID: 1079
	public interface ICspAsymmetricAlgorithm
	{
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002D73 RID: 11635
		CspKeyContainerInfo CspKeyContainerInfo { get; }

		// Token: 0x06002D74 RID: 11636
		byte[] ExportCspBlob(bool includePrivateParameters);

		// Token: 0x06002D75 RID: 11637
		void ImportCspBlob(byte[] rawData);
	}
}
