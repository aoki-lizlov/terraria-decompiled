using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007D9 RID: 2009
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	public sealed class TypeForwardedFromAttribute : Attribute
	{
		// Token: 0x060045B9 RID: 17849 RVA: 0x000E538F File Offset: 0x000E358F
		public TypeForwardedFromAttribute(string assemblyFullName)
		{
			if (string.IsNullOrEmpty(assemblyFullName))
			{
				throw new ArgumentNullException("assemblyFullName");
			}
			this.AssemblyFullName = assemblyFullName;
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060045BA RID: 17850 RVA: 0x000E53B1 File Offset: 0x000E35B1
		public string AssemblyFullName
		{
			[CompilerGenerated]
			get
			{
				return this.<AssemblyFullName>k__BackingField;
			}
		}

		// Token: 0x04002CC4 RID: 11460
		[CompilerGenerated]
		private readonly string <AssemblyFullName>k__BackingField;
	}
}
