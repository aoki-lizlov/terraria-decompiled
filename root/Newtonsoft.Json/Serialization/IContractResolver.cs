using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000090 RID: 144
	public interface IContractResolver
	{
		// Token: 0x0600069D RID: 1693
		JsonContract ResolveContract(Type type);
	}
}
