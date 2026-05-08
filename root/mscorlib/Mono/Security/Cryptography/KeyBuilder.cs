using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000067 RID: 103
	internal sealed class KeyBuilder
	{
		// Token: 0x06000294 RID: 660 RVA: 0x000025BE File Offset: 0x000007BE
		private KeyBuilder()
		{
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000EA55 File Offset: 0x0000CC55
		private static RandomNumberGenerator Rng
		{
			get
			{
				if (KeyBuilder.rng == null)
				{
					KeyBuilder.rng = RandomNumberGenerator.Create();
				}
				return KeyBuilder.rng;
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000EA70 File Offset: 0x0000CC70
		public static byte[] Key(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000EA90 File Offset: 0x0000CC90
		public static byte[] IV(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x04000DD5 RID: 3541
		private static RandomNumberGenerator rng;
	}
}
