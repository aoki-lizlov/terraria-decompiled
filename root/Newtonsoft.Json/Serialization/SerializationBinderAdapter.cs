using System;
using System.Runtime.Serialization;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007F RID: 127
	internal class SerializationBinderAdapter : ISerializationBinder
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x00018417 File Offset: 0x00016617
		public SerializationBinderAdapter(SerializationBinder serializationBinder)
		{
			this.SerializationBinder = serializationBinder;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00018426 File Offset: 0x00016626
		public Type BindToType(string assemblyName, string typeName)
		{
			return this.SerializationBinder.BindToType(assemblyName, typeName);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00018435 File Offset: 0x00016635
		public void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			this.SerializationBinder.BindToName(serializedType, ref assemblyName, ref typeName);
		}

		// Token: 0x04000279 RID: 633
		public readonly SerializationBinder SerializationBinder;
	}
}
