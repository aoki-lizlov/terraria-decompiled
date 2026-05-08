using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
	// Token: 0x020004A7 RID: 1191
	public sealed class PbeParameters
	{
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x000B67B3 File Offset: 0x000B49B3
		public PbeEncryptionAlgorithm EncryptionAlgorithm
		{
			[CompilerGenerated]
			get
			{
				return this.<EncryptionAlgorithm>k__BackingField;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06003113 RID: 12563 RVA: 0x000B67BB File Offset: 0x000B49BB
		public HashAlgorithmName HashAlgorithm
		{
			[CompilerGenerated]
			get
			{
				return this.<HashAlgorithm>k__BackingField;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x000B67C3 File Offset: 0x000B49C3
		public int IterationCount
		{
			[CompilerGenerated]
			get
			{
				return this.<IterationCount>k__BackingField;
			}
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000B67CB File Offset: 0x000B49CB
		public PbeParameters(PbeEncryptionAlgorithm encryptionAlgorithm, HashAlgorithmName hashAlgorithm, int iterationCount)
		{
			if (iterationCount < 1)
			{
				throw new ArgumentOutOfRangeException("iterationCount", iterationCount, "Positive number required.");
			}
			this.EncryptionAlgorithm = encryptionAlgorithm;
			this.HashAlgorithm = hashAlgorithm;
			this.IterationCount = iterationCount;
		}

		// Token: 0x04002209 RID: 8713
		[CompilerGenerated]
		private readonly PbeEncryptionAlgorithm <EncryptionAlgorithm>k__BackingField;

		// Token: 0x0400220A RID: 8714
		[CompilerGenerated]
		private readonly HashAlgorithmName <HashAlgorithm>k__BackingField;

		// Token: 0x0400220B RID: 8715
		[CompilerGenerated]
		private readonly int <IterationCount>k__BackingField;
	}
}
