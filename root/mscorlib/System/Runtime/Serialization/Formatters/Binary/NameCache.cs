using System;
using System.Collections.Concurrent;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200068D RID: 1677
	internal sealed class NameCache
	{
		// Token: 0x06003F56 RID: 16214 RVA: 0x000DF2E8 File Offset: 0x000DD4E8
		internal object GetCachedValue(string name)
		{
			this.name = name;
			object obj;
			if (!NameCache.ht.TryGetValue(name, out obj))
			{
				return null;
			}
			return obj;
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x000DF30E File Offset: 0x000DD50E
		internal void SetCachedValue(object value)
		{
			NameCache.ht[this.name] = value;
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x000025BE File Offset: 0x000007BE
		public NameCache()
		{
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x000DF321 File Offset: 0x000DD521
		// Note: this type is marked as 'beforefieldinit'.
		static NameCache()
		{
		}

		// Token: 0x04002935 RID: 10549
		private static ConcurrentDictionary<string, object> ht = new ConcurrentDictionary<string, object>();

		// Token: 0x04002936 RID: 10550
		private string name;
	}
}
