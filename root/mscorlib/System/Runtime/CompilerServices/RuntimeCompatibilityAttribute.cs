using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007D1 RID: 2001
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
	[Serializable]
	public sealed class RuntimeCompatibilityAttribute : Attribute
	{
		// Token: 0x060045A8 RID: 17832 RVA: 0x00002050 File Offset: 0x00000250
		public RuntimeCompatibilityAttribute()
		{
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060045A9 RID: 17833 RVA: 0x000E527D File Offset: 0x000E347D
		// (set) Token: 0x060045AA RID: 17834 RVA: 0x000E5285 File Offset: 0x000E3485
		public bool WrapNonExceptionThrows
		{
			[CompilerGenerated]
			get
			{
				return this.<WrapNonExceptionThrows>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<WrapNonExceptionThrows>k__BackingField = value;
			}
		}

		// Token: 0x04002CBE RID: 11454
		[CompilerGenerated]
		private bool <WrapNonExceptionThrows>k__BackingField;
	}
}
