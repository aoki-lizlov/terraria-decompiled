using System;

namespace System.Reflection
{
	// Token: 0x020008AB RID: 2219
	public class AssemblyNameProxy : MarshalByRefObject
	{
		// Token: 0x06004AEA RID: 19178 RVA: 0x000F0319 File Offset: 0x000EE519
		public AssemblyName GetAssemblyName(string assemblyFile)
		{
			return AssemblyName.GetAssemblyName(assemblyFile);
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x000543BD File Offset: 0x000525BD
		public AssemblyNameProxy()
		{
		}
	}
}
