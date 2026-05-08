using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000860 RID: 2144
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTrademarkAttribute : Attribute
	{
		// Token: 0x06004809 RID: 18441 RVA: 0x000EDBC0 File Offset: 0x000EBDC0
		public AssemblyTrademarkAttribute(string trademark)
		{
			this.Trademark = trademark;
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600480A RID: 18442 RVA: 0x000EDBCF File Offset: 0x000EBDCF
		public string Trademark
		{
			[CompilerGenerated]
			get
			{
				return this.<Trademark>k__BackingField;
			}
		}

		// Token: 0x04002DE5 RID: 11749
		[CompilerGenerated]
		private readonly string <Trademark>k__BackingField;
	}
}
