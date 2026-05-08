using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000817 RID: 2071
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class RequiredAttributeAttribute : Attribute
	{
		// Token: 0x06004658 RID: 18008 RVA: 0x000E6BD5 File Offset: 0x000E4DD5
		public RequiredAttributeAttribute(Type requiredContract)
		{
			this.requiredContract = requiredContract;
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06004659 RID: 18009 RVA: 0x000E6BE4 File Offset: 0x000E4DE4
		public Type RequiredContract
		{
			get
			{
				return this.requiredContract;
			}
		}

		// Token: 0x04002D0F RID: 11535
		private Type requiredContract;
	}
}
