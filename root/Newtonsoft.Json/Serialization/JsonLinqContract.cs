using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000086 RID: 134
	public class JsonLinqContract : JsonContract
	{
		// Token: 0x0600063A RID: 1594 RVA: 0x000192F9 File Offset: 0x000174F9
		public JsonLinqContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Linq;
		}
	}
}
