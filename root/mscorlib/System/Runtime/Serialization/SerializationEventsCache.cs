using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000628 RID: 1576
	internal static class SerializationEventsCache
	{
		// Token: 0x06003C46 RID: 15430 RVA: 0x000D16FC File Offset: 0x000CF8FC
		internal static SerializationEvents GetSerializationEventsForType(Type t)
		{
			return SerializationEventsCache.s_cache.GetOrAdd(t, (Type type) => new SerializationEvents(type));
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x000D1728 File Offset: 0x000CF928
		// Note: this type is marked as 'beforefieldinit'.
		static SerializationEventsCache()
		{
		}

		// Token: 0x040026AC RID: 9900
		private static readonly ConcurrentDictionary<Type, SerializationEvents> s_cache = new ConcurrentDictionary<Type, SerializationEvents>();

		// Token: 0x02000629 RID: 1577
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003C48 RID: 15432 RVA: 0x000D1734 File Offset: 0x000CF934
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003C49 RID: 15433 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06003C4A RID: 15434 RVA: 0x000D1740 File Offset: 0x000CF940
			internal SerializationEvents <GetSerializationEventsForType>b__1_0(Type type)
			{
				return new SerializationEvents(type);
			}

			// Token: 0x040026AD RID: 9901
			public static readonly SerializationEventsCache.<>c <>9 = new SerializationEventsCache.<>c();

			// Token: 0x040026AE RID: 9902
			public static Func<Type, SerializationEvents> <>9__1_0;
		}
	}
}
