using System;
using System.Reflection;
using System.Threading;

namespace System
{
	// Token: 0x020001F8 RID: 504
	internal sealed class TypeNameParser
	{
		// Token: 0x06001850 RID: 6224 RVA: 0x0005E2B0 File Offset: 0x0005C4B0
		internal static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
		{
			return TypeSpec.Parse(typeName).Resolve(assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackMark);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x000025BE File Offset: 0x000007BE
		public TypeNameParser()
		{
		}
	}
}
