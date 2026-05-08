using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x0200085B RID: 2139
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class AssemblyMetadataAttribute : Attribute
	{
		// Token: 0x060047FF RID: 18431 RVA: 0x000EDB46 File Offset: 0x000EBD46
		public AssemblyMetadataAttribute(string key, string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06004800 RID: 18432 RVA: 0x000EDB5C File Offset: 0x000EBD5C
		public string Key
		{
			[CompilerGenerated]
			get
			{
				return this.<Key>k__BackingField;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06004801 RID: 18433 RVA: 0x000EDB64 File Offset: 0x000EBD64
		public string Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
		}

		// Token: 0x04002DD9 RID: 11737
		[CompilerGenerated]
		private readonly string <Key>k__BackingField;

		// Token: 0x04002DDA RID: 11738
		[CompilerGenerated]
		private readonly string <Value>k__BackingField;
	}
}
