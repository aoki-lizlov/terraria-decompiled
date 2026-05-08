using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A2 RID: 162
	public class JsonStringContract : JsonPrimitiveContract
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x000223B1 File Offset: 0x000205B1
		public JsonStringContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.String;
		}
	}
}
