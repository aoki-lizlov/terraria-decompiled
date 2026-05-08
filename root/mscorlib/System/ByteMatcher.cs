using System;
using System.Collections;

namespace System
{
	// Token: 0x02000228 RID: 552
	internal class ByteMatcher
	{
		// Token: 0x06001B75 RID: 7029 RVA: 0x00068056 File Offset: 0x00066256
		public void AddMapping(TermInfoStrings key, byte[] val)
		{
			if (val.Length == 0)
			{
				return;
			}
			this.map[val] = key;
			this.starts[(int)val[0]] = true;
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00004088 File Offset: 0x00002288
		public void Sort()
		{
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x00068088 File Offset: 0x00066288
		public bool StartsWith(int c)
		{
			return this.starts[c] != null;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x000680A0 File Offset: 0x000662A0
		public TermInfoStrings Match(char[] buffer, int offset, int length, out int used)
		{
			foreach (object obj in this.map.Keys)
			{
				byte[] array = (byte[])obj;
				int num = 0;
				while (num < array.Length && num < length && (char)array[num] == buffer[offset + num])
				{
					if (array.Length - 1 == num)
					{
						used = array.Length;
						return (TermInfoStrings)this.map[array];
					}
					num++;
				}
			}
			used = 0;
			return (TermInfoStrings)(-1);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x00068140 File Offset: 0x00066340
		public ByteMatcher()
		{
		}

		// Token: 0x040016BB RID: 5819
		private Hashtable map = new Hashtable();

		// Token: 0x040016BC RID: 5820
		private Hashtable starts = new Hashtable();
	}
}
