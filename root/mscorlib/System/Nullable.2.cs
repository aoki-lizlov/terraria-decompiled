using System;
using System.Collections.Generic;

namespace System
{
	// Token: 0x0200012B RID: 299
	public static class Nullable
	{
		// Token: 0x06000C27 RID: 3111 RVA: 0x0002DAE6 File Offset: 0x0002BCE6
		public static int Compare<T>(T? n1, T? n2) where T : struct
		{
			if (n1 != null)
			{
				if (n2 != null)
				{
					return Comparer<T>.Default.Compare(n1.value, n2.value);
				}
				return 1;
			}
			else
			{
				if (n2 != null)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002DB1F File Offset: 0x0002BD1F
		public static bool Equals<T>(T? n1, T? n2) where T : struct
		{
			if (n1 != null)
			{
				return n2 != null && EqualityComparer<T>.Default.Equals(n1.value, n2.value);
			}
			return n2 == null;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002DB58 File Offset: 0x0002BD58
		public static Type GetUnderlyingType(Type nullableType)
		{
			if (nullableType == null)
			{
				throw new ArgumentNullException("nullableType");
			}
			if (nullableType.IsGenericType && !nullableType.IsGenericTypeDefinition && nullableType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				return nullableType.GetGenericArguments()[0];
			}
			return null;
		}
	}
}
