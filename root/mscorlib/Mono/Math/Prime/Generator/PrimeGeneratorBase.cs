using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200007F RID: 127
	internal abstract class PrimeGeneratorBase
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00015289 File Offset: 0x00013489
		public virtual ConfidenceFactor Confidence
		{
			get
			{
				return ConfidenceFactor.Medium;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0001528C File Offset: 0x0001348C
		public virtual PrimalityTest PrimalityTest
		{
			get
			{
				return new PrimalityTest(PrimalityTests.RabinMillerTest);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0001529A File Offset: 0x0001349A
		public virtual int TrialDivisionBounds
		{
			get
			{
				return 4000;
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000152A1 File Offset: 0x000134A1
		protected bool PostTrialDivisionTests(BigInteger bi)
		{
			return this.PrimalityTest(bi, this.Confidence);
		}

		// Token: 0x060003BE RID: 958
		public abstract BigInteger GenerateNewPrime(int bits);

		// Token: 0x060003BF RID: 959 RVA: 0x000025BE File Offset: 0x000007BE
		protected PrimeGeneratorBase()
		{
		}
	}
}
