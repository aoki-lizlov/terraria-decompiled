using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Versioning
{
	// Token: 0x0200060E RID: 1550
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class ComponentGuaranteesAttribute : Attribute
	{
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x000D0B8F File Offset: 0x000CED8F
		public ComponentGuaranteesOptions Guarantees
		{
			[CompilerGenerated]
			get
			{
				return this.<Guarantees>k__BackingField;
			}
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x000D0B97 File Offset: 0x000CED97
		public ComponentGuaranteesAttribute(ComponentGuaranteesOptions guarantees)
		{
			this.Guarantees = guarantees;
		}

		// Token: 0x04002674 RID: 9844
		[CompilerGenerated]
		private readonly ComponentGuaranteesOptions <Guarantees>k__BackingField;
	}
}
