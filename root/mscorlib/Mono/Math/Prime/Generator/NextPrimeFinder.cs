using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200007E RID: 126
	internal class NextPrimeFinder : SequentialSearchPrimeGeneratorBase
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0001525F File Offset: 0x0001345F
		protected override BigInteger GenerateSearchBase(int bits, object Context)
		{
			if (Context == null)
			{
				throw new ArgumentNullException("Context");
			}
			BigInteger bigInteger = new BigInteger((BigInteger)Context);
			bigInteger.SetBit(0U);
			return bigInteger;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00015281 File Offset: 0x00013481
		public NextPrimeFinder()
		{
		}
	}
}
