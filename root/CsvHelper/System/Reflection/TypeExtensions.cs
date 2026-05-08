using System;

namespace System.Reflection
{
	// Token: 0x02000003 RID: 3
	internal static class TypeExtensions
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002188 File Offset: 0x00000388
		public static TypeInfo GetTypeInfo(this Type type)
		{
			return new TypeInfo(type);
		}
	}
}
