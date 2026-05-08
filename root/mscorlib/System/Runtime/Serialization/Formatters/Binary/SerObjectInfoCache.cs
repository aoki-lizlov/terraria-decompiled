using System;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000682 RID: 1666
	internal sealed class SerObjectInfoCache
	{
		// Token: 0x06003EBA RID: 16058 RVA: 0x000DA2A0 File Offset: 0x000D84A0
		internal SerObjectInfoCache(string typeName, string assemblyName, bool hasTypeForwardedFrom)
		{
			this.fullTypeName = typeName;
			this.assemblyString = assemblyName;
			this.hasTypeForwardedFrom = hasTypeForwardedFrom;
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x000DA2C0 File Offset: 0x000D84C0
		internal SerObjectInfoCache(Type type)
		{
			TypeInformation typeInformation = BinaryFormatter.GetTypeInformation(type);
			this.fullTypeName = typeInformation.FullTypeName;
			this.assemblyString = typeInformation.AssemblyString;
			this.hasTypeForwardedFrom = typeInformation.HasTypeForwardedFrom;
		}

		// Token: 0x040028A8 RID: 10408
		internal string fullTypeName;

		// Token: 0x040028A9 RID: 10409
		internal string assemblyString;

		// Token: 0x040028AA RID: 10410
		internal bool hasTypeForwardedFrom;

		// Token: 0x040028AB RID: 10411
		internal MemberInfo[] memberInfos;

		// Token: 0x040028AC RID: 10412
		internal string[] memberNames;

		// Token: 0x040028AD RID: 10413
		internal Type[] memberTypes;
	}
}
