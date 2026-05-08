using System;
using System.Reflection;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000665 RID: 1637
	internal sealed class BinaryAssemblyInfo
	{
		// Token: 0x06003DA8 RID: 15784 RVA: 0x000D5BB2 File Offset: 0x000D3DB2
		internal BinaryAssemblyInfo(string assemblyString)
		{
			this.assemblyString = assemblyString;
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x000D5BC1 File Offset: 0x000D3DC1
		internal BinaryAssemblyInfo(string assemblyString, Assembly assembly)
		{
			this.assemblyString = assemblyString;
			this.assembly = assembly;
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x000D5BD8 File Offset: 0x000D3DD8
		internal Assembly GetAssembly()
		{
			if (this.assembly == null)
			{
				this.assembly = FormatterServices.LoadAssemblyFromStringNoThrow(this.assemblyString);
				if (this.assembly == null)
				{
					throw new SerializationException(Environment.GetResourceString("Unable to find assembly '{0}'.", new object[] { this.assemblyString }));
				}
			}
			return this.assembly;
		}

		// Token: 0x040027B8 RID: 10168
		internal string assemblyString;

		// Token: 0x040027B9 RID: 10169
		private Assembly assembly;
	}
}
