using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000805 RID: 2053
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IDispatchConstantAttribute : CustomConstantAttribute
	{
		// Token: 0x06004649 RID: 17993 RVA: 0x000E6B52 File Offset: 0x000E4D52
		public IDispatchConstantAttribute()
		{
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x0600464A RID: 17994 RVA: 0x000E6B5A File Offset: 0x000E4D5A
		public override object Value
		{
			get
			{
				return new DispatchWrapper(null);
			}
		}
	}
}
