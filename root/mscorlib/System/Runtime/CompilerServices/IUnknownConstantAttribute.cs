using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000812 RID: 2066
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class IUnknownConstantAttribute : CustomConstantAttribute
	{
		// Token: 0x06004650 RID: 18000 RVA: 0x000E6B52 File Offset: 0x000E4D52
		public IUnknownConstantAttribute()
		{
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06004651 RID: 18001 RVA: 0x000E6B91 File Offset: 0x000E4D91
		public override object Value
		{
			get
			{
				return new UnknownWrapper(null);
			}
		}
	}
}
