using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000859 RID: 2137
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyKeyFileAttribute : Attribute
	{
		// Token: 0x060047FB RID: 18427 RVA: 0x000EDB18 File Offset: 0x000EBD18
		public AssemblyKeyFileAttribute(string keyFile)
		{
			this.KeyFile = keyFile;
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060047FC RID: 18428 RVA: 0x000EDB27 File Offset: 0x000EBD27
		public string KeyFile
		{
			[CompilerGenerated]
			get
			{
				return this.<KeyFile>k__BackingField;
			}
		}

		// Token: 0x04002DD7 RID: 11735
		[CompilerGenerated]
		private readonly string <KeyFile>k__BackingField;
	}
}
