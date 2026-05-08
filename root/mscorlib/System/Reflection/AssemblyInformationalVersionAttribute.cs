using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000858 RID: 2136
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyInformationalVersionAttribute : Attribute
	{
		// Token: 0x060047F9 RID: 18425 RVA: 0x000EDB01 File Offset: 0x000EBD01
		public AssemblyInformationalVersionAttribute(string informationalVersion)
		{
			this.InformationalVersion = informationalVersion;
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060047FA RID: 18426 RVA: 0x000EDB10 File Offset: 0x000EBD10
		public string InformationalVersion
		{
			[CompilerGenerated]
			get
			{
				return this.<InformationalVersion>k__BackingField;
			}
		}

		// Token: 0x04002DD6 RID: 11734
		[CompilerGenerated]
		private readonly string <InformationalVersion>k__BackingField;
	}
}
