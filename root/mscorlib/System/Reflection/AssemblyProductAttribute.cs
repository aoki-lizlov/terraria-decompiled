using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x0200085D RID: 2141
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyProductAttribute : Attribute
	{
		// Token: 0x06004802 RID: 18434 RVA: 0x000EDB6C File Offset: 0x000EBD6C
		public AssemblyProductAttribute(string product)
		{
			this.Product = product;
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06004803 RID: 18435 RVA: 0x000EDB7B File Offset: 0x000EBD7B
		public string Product
		{
			[CompilerGenerated]
			get
			{
				return this.<Product>k__BackingField;
			}
		}

		// Token: 0x04002DE1 RID: 11745
		[CompilerGenerated]
		private readonly string <Product>k__BackingField;
	}
}
