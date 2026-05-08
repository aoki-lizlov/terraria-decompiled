using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000452 RID: 1106
	[ComVisible(true)]
	public sealed class KeySizes
	{
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06002E31 RID: 11825 RVA: 0x000A68EC File Offset: 0x000A4AEC
		public int MinSize
		{
			get
			{
				return this.m_minSize;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06002E32 RID: 11826 RVA: 0x000A68F4 File Offset: 0x000A4AF4
		public int MaxSize
		{
			get
			{
				return this.m_maxSize;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06002E33 RID: 11827 RVA: 0x000A68FC File Offset: 0x000A4AFC
		public int SkipSize
		{
			get
			{
				return this.m_skipSize;
			}
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x000A6904 File Offset: 0x000A4B04
		public KeySizes(int minSize, int maxSize, int skipSize)
		{
			this.m_minSize = minSize;
			this.m_maxSize = maxSize;
			this.m_skipSize = skipSize;
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000A6924 File Offset: 0x000A4B24
		internal bool IsLegal(int keySize)
		{
			int num = keySize - this.MinSize;
			bool flag = num >= 0 && keySize <= this.MaxSize;
			if (this.SkipSize != 0)
			{
				return flag && num % this.SkipSize == 0;
			}
			return flag;
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000A6968 File Offset: 0x000A4B68
		internal static bool IsLegalKeySize(KeySizes[] legalKeys, int size)
		{
			for (int i = 0; i < legalKeys.Length; i++)
			{
				if (legalKeys[i].IsLegal(size))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400200A RID: 8202
		private int m_minSize;

		// Token: 0x0400200B RID: 8203
		private int m_maxSize;

		// Token: 0x0400200C RID: 8204
		private int m_skipSize;
	}
}
