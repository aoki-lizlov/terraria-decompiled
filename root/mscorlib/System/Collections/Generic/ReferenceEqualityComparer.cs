using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000B06 RID: 2822
	internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T> where T : class
	{
		// Token: 0x060067D8 RID: 26584 RVA: 0x000025BE File Offset: 0x000007BE
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x060067D9 RID: 26585 RVA: 0x00160078 File Offset: 0x0015E278
		public bool Equals(T x, T y)
		{
			return x == y;
		}

		// Token: 0x060067DA RID: 26586 RVA: 0x00160088 File Offset: 0x0015E288
		public int GetHashCode(T obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}

		// Token: 0x060067DB RID: 26587 RVA: 0x00160095 File Offset: 0x0015E295
		// Note: this type is marked as 'beforefieldinit'.
		static ReferenceEqualityComparer()
		{
		}

		// Token: 0x04003C3B RID: 15419
		internal static readonly ReferenceEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();
	}
}
