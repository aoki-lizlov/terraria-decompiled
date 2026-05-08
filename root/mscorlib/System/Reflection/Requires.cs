using System;

namespace System.Reflection
{
	// Token: 0x020008A3 RID: 2211
	internal static class Requires
	{
		// Token: 0x06004AB3 RID: 19123 RVA: 0x000EFE9F File Offset: 0x000EE09F
		internal static void NotNull(object obj, string name)
		{
			if (obj == null)
			{
				throw new ArgumentNullException(name);
			}
		}
	}
}
