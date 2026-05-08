using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007AB RID: 1963
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	public sealed class AsyncMethodBuilderAttribute : Attribute
	{
		// Token: 0x06004555 RID: 17749 RVA: 0x000E4BF7 File Offset: 0x000E2DF7
		public AsyncMethodBuilderAttribute(Type builderType)
		{
			this.BuilderType = builderType;
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06004556 RID: 17750 RVA: 0x000E4C06 File Offset: 0x000E2E06
		public Type BuilderType
		{
			[CompilerGenerated]
			get
			{
				return this.<BuilderType>k__BackingField;
			}
		}

		// Token: 0x04002CA4 RID: 11428
		[CompilerGenerated]
		private readonly Type <BuilderType>k__BackingField;
	}
}
