using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000079 RID: 121
	public interface ISerializationBinder
	{
		// Token: 0x060005A3 RID: 1443
		Type BindToType(string assemblyName, string typeName);

		// Token: 0x060005A4 RID: 1444
		void BindToName(Type serializedType, out string assemblyName, out string typeName);
	}
}
