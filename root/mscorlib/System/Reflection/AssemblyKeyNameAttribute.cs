using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x0200085A RID: 2138
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyKeyNameAttribute : Attribute
	{
		// Token: 0x060047FD RID: 18429 RVA: 0x000EDB2F File Offset: 0x000EBD2F
		public AssemblyKeyNameAttribute(string keyName)
		{
			this.KeyName = keyName;
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x060047FE RID: 18430 RVA: 0x000EDB3E File Offset: 0x000EBD3E
		public string KeyName
		{
			[CompilerGenerated]
			get
			{
				return this.<KeyName>k__BackingField;
			}
		}

		// Token: 0x04002DD8 RID: 11736
		[CompilerGenerated]
		private readonly string <KeyName>k__BackingField;
	}
}
