using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A4 RID: 164
	internal static class CachedAttributeGetter<T> where T : Attribute
	{
		// Token: 0x060007FE RID: 2046 RVA: 0x0002293E File Offset: 0x00020B3E
		public static T GetAttribute(object type)
		{
			return CachedAttributeGetter<T>.TypeAttributeCache.Get(type);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0002294B File Offset: 0x00020B4B
		// Note: this type is marked as 'beforefieldinit'.
		static CachedAttributeGetter()
		{
		}

		// Token: 0x04000326 RID: 806
		private static readonly ThreadSafeStore<object, T> TypeAttributeCache = new ThreadSafeStore<object, T>(new Func<object, T>(JsonTypeReflector.GetAttribute<T>));
	}
}
