using System;
using System.Collections.Generic;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200008C RID: 140
	public class CamelCasePropertyNamesContractResolver : DefaultContractResolver
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x000196C1 File Offset: 0x000178C1
		public CamelCasePropertyNamesContractResolver()
		{
			base.NamingStrategy = new CamelCaseNamingStrategy
			{
				ProcessDictionaryKeys = true,
				OverrideSpecifiedNames = true
			};
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000196E4 File Offset: 0x000178E4
		public override JsonContract ResolveContract(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ResolverContractKey resolverContractKey = new ResolverContractKey(base.GetType(), type);
			Dictionary<ResolverContractKey, JsonContract> dictionary = CamelCasePropertyNamesContractResolver._contractCache;
			JsonContract jsonContract;
			if (dictionary == null || !dictionary.TryGetValue(resolverContractKey, ref jsonContract))
			{
				jsonContract = this.CreateContract(type);
				object typeContractCacheLock = CamelCasePropertyNamesContractResolver.TypeContractCacheLock;
				lock (typeContractCacheLock)
				{
					dictionary = CamelCasePropertyNamesContractResolver._contractCache;
					Dictionary<ResolverContractKey, JsonContract> dictionary2 = ((dictionary != null) ? new Dictionary<ResolverContractKey, JsonContract>(dictionary) : new Dictionary<ResolverContractKey, JsonContract>());
					dictionary2[resolverContractKey] = jsonContract;
					CamelCasePropertyNamesContractResolver._contractCache = dictionary2;
				}
			}
			return jsonContract;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00019784 File Offset: 0x00017984
		internal override PropertyNameTable GetNameTable()
		{
			return CamelCasePropertyNamesContractResolver.NameTable;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001978B File Offset: 0x0001798B
		// Note: this type is marked as 'beforefieldinit'.
		static CamelCasePropertyNamesContractResolver()
		{
		}

		// Token: 0x04000292 RID: 658
		private static readonly object TypeContractCacheLock = new object();

		// Token: 0x04000293 RID: 659
		private static readonly PropertyNameTable NameTable = new PropertyNameTable();

		// Token: 0x04000294 RID: 660
		private static Dictionary<ResolverContractKey, JsonContract> _contractCache;
	}
}
