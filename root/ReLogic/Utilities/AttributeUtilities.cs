using System;
using System.Linq;
using System.Reflection;

namespace ReLogic.Utilities
{
	// Token: 0x02000002 RID: 2
	public static class AttributeUtilities
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static T GetAttribute<T>(this MethodBase method) where T : Attribute
		{
			return (T)((object)Enumerable.SingleOrDefault<object>(method.GetCustomAttributes(typeof(T), false)));
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002070 File Offset: 0x00000270
		public static T GetAttribute<T>(this Enum value) where T : Attribute
		{
			Type type = value.GetType();
			string name = Enum.GetName(type, value);
			return Enumerable.SingleOrDefault<T>(Enumerable.OfType<T>(type.GetField(name).GetCustomAttributes(false)));
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020A1 File Offset: 0x000002A1
		public static A GetCacheableAttribute<T, A>() where A : Attribute
		{
			return AttributeUtilities.TypeAttributeCache<T, A>.Value;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020A8 File Offset: 0x000002A8
		public static T GetAttribute<T>(this Type type) where T : Attribute
		{
			return Enumerable.SingleOrDefault<T>(Enumerable.OfType<T>(type.GetCustomAttributes(false)));
		}

		// Token: 0x020000AA RID: 170
		private static class TypeAttributeCache<T, A> where A : Attribute
		{
			// Token: 0x06000400 RID: 1024 RVA: 0x0000DD6D File Offset: 0x0000BF6D
			// Note: this type is marked as 'beforefieldinit'.
			static TypeAttributeCache()
			{
			}

			// Token: 0x0400053C RID: 1340
			public static readonly A Value = typeof(T).GetAttribute<A>();
		}
	}
}
