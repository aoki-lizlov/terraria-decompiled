using System;
using System.Text;

namespace System.Globalization
{
	// Token: 0x02000A03 RID: 2563
	internal class Bootstring
	{
		// Token: 0x06005F76 RID: 24438 RVA: 0x0014B7F8 File Offset: 0x001499F8
		public Bootstring(char delimiter, int baseNum, int tmin, int tmax, int skew, int damp, int initialBias, int initialN)
		{
			this.delimiter = delimiter;
			this.base_num = baseNum;
			this.tmin = tmin;
			this.tmax = tmax;
			this.skew = skew;
			this.damp = damp;
			this.initial_bias = initialBias;
			this.initial_n = initialN;
		}

		// Token: 0x06005F77 RID: 24439 RVA: 0x0014B848 File Offset: 0x00149A48
		public string Encode(string s, int offset)
		{
			int num = this.initial_n;
			int num2 = 0;
			int num3 = this.initial_bias;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] < '\u0080')
				{
					stringBuilder.Append(s[i]);
				}
			}
			int length;
			int j = (length = stringBuilder.Length);
			if (length > 0)
			{
				stringBuilder.Append(this.delimiter);
			}
			while (j < s.Length)
			{
				int num4 = int.MaxValue;
				for (int k = 0; k < s.Length; k++)
				{
					if ((int)s[k] >= num && (int)s[k] < num4)
					{
						num4 = (int)s[k];
					}
				}
				checked
				{
					num2 += (num4 - num) * (j + 1);
					num = num4;
					foreach (char c in s)
					{
						if ((int)c < num || c < '\u0080')
						{
							num2++;
						}
						unchecked
						{
							if ((int)c == num)
							{
								int num5 = num2;
								int num6 = this.base_num;
								for (;;)
								{
									int num7 = ((num6 <= num3 + this.tmin) ? this.tmin : ((num6 >= num3 + this.tmax) ? this.tmax : (num6 - num3)));
									if (num5 < num7)
									{
										break;
									}
									stringBuilder.Append(this.EncodeDigit(num7 + (num5 - num7) % (this.base_num - num7)));
									num5 = (num5 - num7) / (this.base_num - num7);
									num6 += this.base_num;
								}
								stringBuilder.Append(this.EncodeDigit(num5));
								num3 = this.Adapt(num2, j + 1, j == length);
								num2 = 0;
								j++;
							}
						}
					}
				}
				num2++;
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005F78 RID: 24440 RVA: 0x0014BA17 File Offset: 0x00149C17
		private char EncodeDigit(int d)
		{
			return (char)((d < 26) ? (d + 97) : (d - 26 + 48));
		}

		// Token: 0x06005F79 RID: 24441 RVA: 0x0014BA2C File Offset: 0x00149C2C
		private int DecodeDigit(char c)
		{
			if (c - '0' < '\n')
			{
				return (int)(c - '\u0016');
			}
			if (c - 'A' < '\u001a')
			{
				return (int)(c - 'A');
			}
			if (c - 'a' >= '\u001a')
			{
				return this.base_num;
			}
			return (int)(c - 'a');
		}

		// Token: 0x06005F7A RID: 24442 RVA: 0x0014BA5C File Offset: 0x00149C5C
		private int Adapt(int delta, int numPoints, bool firstTime)
		{
			if (firstTime)
			{
				delta /= this.damp;
			}
			else
			{
				delta /= 2;
			}
			delta += delta / numPoints;
			int num = 0;
			while (delta > (this.base_num - this.tmin) * this.tmax / 2)
			{
				delta /= this.base_num - this.tmin;
				num += this.base_num;
			}
			return num + (this.base_num - this.tmin + 1) * delta / (delta + this.skew);
		}

		// Token: 0x06005F7B RID: 24443 RVA: 0x0014BAD8 File Offset: 0x00149CD8
		public string Decode(string s, int offset)
		{
			int num = this.initial_n;
			int num2 = 0;
			int num3 = this.initial_bias;
			int num4 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == this.delimiter)
				{
					num4 = i;
				}
			}
			if (num4 < 0)
			{
				return s;
			}
			stringBuilder.Append(s, 0, num4);
			int j = ((num4 > 0) ? (num4 + 1) : 0);
			while (j < s.Length)
			{
				int num5 = num2;
				int num6 = 1;
				int num7 = this.base_num;
				for (;;)
				{
					int num8 = this.DecodeDigit(s[j++]);
					num2 += num8 * num6;
					int num9 = ((num7 <= num3 + this.tmin) ? this.tmin : ((num7 >= num3 + this.tmax) ? this.tmax : (num7 - num3)));
					if (num8 < num9)
					{
						break;
					}
					num6 *= this.base_num - num9;
					num7 += this.base_num;
				}
				num3 = this.Adapt(num2 - num5, stringBuilder.Length + 1, num5 == 0);
				num += num2 / (stringBuilder.Length + 1);
				num2 %= stringBuilder.Length + 1;
				if (num < 128)
				{
					throw new ArgumentException(string.Format("Invalid Bootstring decode result, at {0}", offset + j));
				}
				stringBuilder.Insert(num2, (char)num);
				num2++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04003986 RID: 14726
		private readonly char delimiter;

		// Token: 0x04003987 RID: 14727
		private readonly int base_num;

		// Token: 0x04003988 RID: 14728
		private readonly int tmin;

		// Token: 0x04003989 RID: 14729
		private readonly int tmax;

		// Token: 0x0400398A RID: 14730
		private readonly int skew;

		// Token: 0x0400398B RID: 14731
		private readonly int damp;

		// Token: 0x0400398C RID: 14732
		private readonly int initial_bias;

		// Token: 0x0400398D RID: 14733
		private readonly int initial_n;
	}
}
