using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;

namespace System.Resources
{
	// Token: 0x02000834 RID: 2100
	internal sealed class FastResourceComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		// Token: 0x060046F3 RID: 18163 RVA: 0x000E81FD File Offset: 0x000E63FD
		public int GetHashCode(object key)
		{
			return FastResourceComparer.HashFunction((string)key);
		}

		// Token: 0x060046F4 RID: 18164 RVA: 0x000E820A File Offset: 0x000E640A
		public int GetHashCode(string key)
		{
			return FastResourceComparer.HashFunction(key);
		}

		// Token: 0x060046F5 RID: 18165 RVA: 0x000E8214 File Offset: 0x000E6414
		internal static int HashFunction(string key)
		{
			uint num = 5381U;
			for (int i = 0; i < key.Length; i++)
			{
				num = ((num << 5) + num) ^ (uint)key[i];
			}
			return (int)num;
		}

		// Token: 0x060046F6 RID: 18166 RVA: 0x000E8248 File Offset: 0x000E6448
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			string text = (string)a;
			string text2 = (string)b;
			return string.CompareOrdinal(text, text2);
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x0003D4ED File Offset: 0x0003B6ED
		public int Compare(string a, string b)
		{
			return string.CompareOrdinal(a, b);
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x0003D4F6 File Offset: 0x0003B6F6
		public bool Equals(string a, string b)
		{
			return string.Equals(a, b);
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x000E8270 File Offset: 0x000E6470
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			string text = (string)a;
			string text2 = (string)b;
			return string.Equals(text, text2);
		}

		// Token: 0x060046FA RID: 18170 RVA: 0x000E8298 File Offset: 0x000E6498
		[SecurityCritical]
		public unsafe static int CompareOrdinal(string a, byte[] bytes, int bCharLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = a.Length;
			if (num3 > bCharLength)
			{
				num3 = bCharLength;
			}
			if (bCharLength == 0)
			{
				if (a.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				fixed (byte[] array = bytes)
				{
					byte* ptr;
					if (bytes == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					byte* ptr2 = ptr;
					while (num < num3 && num2 == 0)
					{
						int num4 = (int)(*ptr2) | ((int)ptr2[1] << 8);
						num2 = (int)a[num++] - num4;
						ptr2 += 2;
					}
				}
				if (num2 != 0)
				{
					return num2;
				}
				return a.Length - bCharLength;
			}
		}

		// Token: 0x060046FB RID: 18171 RVA: 0x000E831E File Offset: 0x000E651E
		[SecurityCritical]
		public static int CompareOrdinal(byte[] bytes, int aCharLength, string b)
		{
			return -FastResourceComparer.CompareOrdinal(b, bytes, aCharLength);
		}

		// Token: 0x060046FC RID: 18172 RVA: 0x000E832C File Offset: 0x000E652C
		[SecurityCritical]
		internal unsafe static int CompareOrdinal(byte* a, int byteLen, string b)
		{
			int num = 0;
			int num2 = 0;
			int num3 = byteLen >> 1;
			if (num3 > b.Length)
			{
				num3 = b.Length;
			}
			while (num2 < num3 && num == 0)
			{
				num = (int)((char)((int)(*(a++)) | ((int)(*(a++)) << 8)) - b[num2++]);
			}
			if (num != 0)
			{
				return num;
			}
			return byteLen - b.Length * 2;
		}

		// Token: 0x060046FD RID: 18173 RVA: 0x000025BE File Offset: 0x000007BE
		public FastResourceComparer()
		{
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x000E8388 File Offset: 0x000E6588
		// Note: this type is marked as 'beforefieldinit'.
		static FastResourceComparer()
		{
		}

		// Token: 0x04002D4B RID: 11595
		internal static readonly FastResourceComparer Default = new FastResourceComparer();
	}
}
