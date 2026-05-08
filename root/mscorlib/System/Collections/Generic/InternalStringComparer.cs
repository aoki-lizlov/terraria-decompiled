using System;

namespace System.Collections.Generic
{
	// Token: 0x02000B26 RID: 2854
	[Serializable]
	internal sealed class InternalStringComparer : EqualityComparer<string>
	{
		// Token: 0x060068C8 RID: 26824 RVA: 0x00163554 File Offset: 0x00161754
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x060068C9 RID: 26825 RVA: 0x00163561 File Offset: 0x00161761
		public override bool Equals(string x, string y)
		{
			if (x == null)
			{
				return y == null;
			}
			return x == y || x.Equals(y);
		}

		// Token: 0x060068CA RID: 26826 RVA: 0x00163578 File Offset: 0x00161778
		internal override int IndexOf(string[] array, string value, int startIndex, int count)
		{
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (Array.UnsafeLoad<string>(array, i) == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060068CB RID: 26827 RVA: 0x001635A8 File Offset: 0x001617A8
		public InternalStringComparer()
		{
		}
	}
}
