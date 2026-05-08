using System;

namespace System.Collections.Generic
{
	// Token: 0x02000B1B RID: 2843
	[Serializable]
	internal class ObjectComparer<T> : Comparer<T>
	{
		// Token: 0x06006889 RID: 26761 RVA: 0x00162D32 File Offset: 0x00160F32
		public override int Compare(T x, T y)
		{
			return Comparer.Default.Compare(x, y);
		}

		// Token: 0x0600688A RID: 26762 RVA: 0x00162D4A File Offset: 0x00160F4A
		public override bool Equals(object obj)
		{
			return obj is ObjectComparer<T>;
		}

		// Token: 0x0600688B RID: 26763 RVA: 0x00162CCA File Offset: 0x00160ECA
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}

		// Token: 0x0600688C RID: 26764 RVA: 0x00162CDC File Offset: 0x00160EDC
		public ObjectComparer()
		{
		}
	}
}
