using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000C1 RID: 193
	public class AssemblyLoadEventArgs : EventArgs
	{
		// Token: 0x0600057A RID: 1402 RVA: 0x000190A4 File Offset: 0x000172A4
		public AssemblyLoadEventArgs(Assembly loadedAssembly)
		{
			this.LoadedAssembly = loadedAssembly;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x000190B3 File Offset: 0x000172B3
		public Assembly LoadedAssembly
		{
			[CompilerGenerated]
			get
			{
				return this.<LoadedAssembly>k__BackingField;
			}
		}

		// Token: 0x04000ED5 RID: 3797
		[CompilerGenerated]
		private readonly Assembly <LoadedAssembly>k__BackingField;
	}
}
