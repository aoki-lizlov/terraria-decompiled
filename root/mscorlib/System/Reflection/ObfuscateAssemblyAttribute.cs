using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000883 RID: 2179
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class ObfuscateAssemblyAttribute : Attribute
	{
		// Token: 0x06004924 RID: 18724 RVA: 0x000EEBA4 File Offset: 0x000ECDA4
		public ObfuscateAssemblyAttribute(bool assemblyIsPrivate)
		{
			this.AssemblyIsPrivate = assemblyIsPrivate;
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06004925 RID: 18725 RVA: 0x000EEBBA File Offset: 0x000ECDBA
		public bool AssemblyIsPrivate
		{
			[CompilerGenerated]
			get
			{
				return this.<AssemblyIsPrivate>k__BackingField;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06004926 RID: 18726 RVA: 0x000EEBC2 File Offset: 0x000ECDC2
		// (set) Token: 0x06004927 RID: 18727 RVA: 0x000EEBCA File Offset: 0x000ECDCA
		public bool StripAfterObfuscation
		{
			[CompilerGenerated]
			get
			{
				return this.<StripAfterObfuscation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<StripAfterObfuscation>k__BackingField = value;
			}
		} = true;

		// Token: 0x04002E72 RID: 11890
		[CompilerGenerated]
		private readonly bool <AssemblyIsPrivate>k__BackingField;

		// Token: 0x04002E73 RID: 11891
		[CompilerGenerated]
		private bool <StripAfterObfuscation>k__BackingField;
	}
}
