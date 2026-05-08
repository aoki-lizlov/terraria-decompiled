using System;
using System.Collections.Generic;

namespace System.IO
{
	// Token: 0x0200098F RID: 2447
	internal static class MonoLinqHelper
	{
		// Token: 0x06005945 RID: 22853 RVA: 0x0012F001 File Offset: 0x0012D201
		public static T[] ToArray<T>(this IEnumerable<T> source)
		{
			return EnumerableHelpers.ToArray<T>(source);
		}
	}
}
