using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x02000080 RID: 128
	internal class SequentialSearchPrimeGeneratorBase : PrimeGeneratorBase
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x000152B5 File Offset: 0x000134B5
		protected virtual BigInteger GenerateSearchBase(int bits, object context)
		{
			BigInteger bigInteger = BigInteger.GenerateRandom(bits);
			bigInteger.SetBit(0U);
			return bigInteger;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000152C4 File Offset: 0x000134C4
		public override BigInteger GenerateNewPrime(int bits)
		{
			return this.GenerateNewPrime(bits, null);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000152D0 File Offset: 0x000134D0
		public virtual BigInteger GenerateNewPrime(int bits, object context)
		{
			BigInteger bigInteger = this.GenerateSearchBase(bits, context);
			uint num = bigInteger % 3234846615U;
			int trialDivisionBounds = this.TrialDivisionBounds;
			uint[] smallPrimes = BigInteger.smallPrimes;
			for (;;)
			{
				if (num % 3U != 0U && num % 5U != 0U && num % 7U != 0U && num % 11U != 0U && num % 13U != 0U && num % 17U != 0U && num % 19U != 0U && num % 23U != 0U && num % 29U != 0U)
				{
					int num2 = 10;
					while (num2 < smallPrimes.Length && (ulong)smallPrimes[num2] <= (ulong)((long)trialDivisionBounds))
					{
						if (bigInteger % smallPrimes[num2] == 0U)
						{
							goto IL_009D;
						}
						num2++;
					}
					if (this.IsPrimeAcceptable(bigInteger, context) && this.PrimalityTest(bigInteger, this.Confidence))
					{
						break;
					}
				}
				IL_009D:
				num += 2U;
				if (num >= 3234846615U)
				{
					num -= 3234846615U;
				}
				bigInteger.Incr2();
			}
			return bigInteger;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00003FB7 File Offset: 0x000021B7
		protected virtual bool IsPrimeAcceptable(BigInteger bi, object context)
		{
			return true;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00015398 File Offset: 0x00013598
		public SequentialSearchPrimeGeneratorBase()
		{
		}
	}
}
