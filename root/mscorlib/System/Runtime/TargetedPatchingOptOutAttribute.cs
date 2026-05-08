using System;
using System.Runtime.CompilerServices;

namespace System.Runtime
{
	// Token: 0x0200051E RID: 1310
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class TargetedPatchingOptOutAttribute : Attribute
	{
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06003543 RID: 13635 RVA: 0x000C1990 File Offset: 0x000BFB90
		public string Reason
		{
			[CompilerGenerated]
			get
			{
				return this.<Reason>k__BackingField;
			}
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000C1998 File Offset: 0x000BFB98
		public TargetedPatchingOptOutAttribute(string reason)
		{
			this.Reason = reason;
		}

		// Token: 0x04002489 RID: 9353
		[CompilerGenerated]
		private readonly string <Reason>k__BackingField;
	}
}
