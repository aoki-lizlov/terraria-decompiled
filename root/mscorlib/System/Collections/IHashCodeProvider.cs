using System;

namespace System.Collections
{
	// Token: 0x02000AA7 RID: 2727
	[Obsolete("Please use IEqualityComparer instead.")]
	public interface IHashCodeProvider
	{
		// Token: 0x060064B4 RID: 25780
		int GetHashCode(object obj);
	}
}
