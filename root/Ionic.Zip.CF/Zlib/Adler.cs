using System;

namespace Ionic.Zlib
{
	// Token: 0x02000051 RID: 81
	public sealed class Adler
	{
		// Token: 0x0600039E RID: 926 RVA: 0x0001B478 File Offset: 0x00019678
		public static uint Adler32(uint adler, byte[] buf, int index, int len)
		{
			if (buf == null)
			{
				return 1U;
			}
			uint num = adler & 65535U;
			uint num2 = (adler >> 16) & 65535U;
			while (len > 0)
			{
				int i = ((len < Adler.NMAX) ? len : Adler.NMAX);
				len -= i;
				while (i >= 16)
				{
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					num += (uint)buf[index++];
					num2 += num;
					i -= 16;
				}
				if (i != 0)
				{
					do
					{
						num += (uint)buf[index++];
						num2 += num;
					}
					while (--i != 0);
				}
				num %= Adler.BASE;
				num2 %= Adler.BASE;
			}
			return (num2 << 16) | num;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001B606 File Offset: 0x00019806
		public Adler()
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001B5F0 File Offset: 0x000197F0
		// Note: this type is marked as 'beforefieldinit'.
		static Adler()
		{
		}

		// Token: 0x040002D6 RID: 726
		private static readonly uint BASE = 65521U;

		// Token: 0x040002D7 RID: 727
		private static readonly int NMAX = 5552;
	}
}
