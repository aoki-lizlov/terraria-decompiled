using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000085 RID: 133
	public class JsonISerializableContract : JsonContainerContract
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x000192D8 File Offset: 0x000174D8
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x000192E0 File Offset: 0x000174E0
		public ObjectConstructor<object> ISerializableCreator
		{
			[CompilerGenerated]
			get
			{
				return this.<ISerializableCreator>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ISerializableCreator>k__BackingField = value;
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000192E9 File Offset: 0x000174E9
		public JsonISerializableContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Serializable;
		}

		// Token: 0x04000287 RID: 647
		[CompilerGenerated]
		private ObjectConstructor<object> <ISerializableCreator>k__BackingField;
	}
}
