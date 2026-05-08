using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000075 RID: 117
	public class DefaultNamingStrategy : NamingStrategy
	{
		// Token: 0x06000597 RID: 1431 RVA: 0x00017F2F File Offset: 0x0001612F
		protected override string ResolvePropertyName(string name)
		{
			return name;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00017F1F File Offset: 0x0001611F
		public DefaultNamingStrategy()
		{
		}
	}
}
