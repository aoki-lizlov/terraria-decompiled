using System;
using System.Runtime.CompilerServices;

namespace Steamworks
{
	// Token: 0x02000186 RID: 390
	[AttributeUsage(8, AllowMultiple = false)]
	internal class CallbackIdentityAttribute : Attribute
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0000D177 File Offset: 0x0000B377
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x0000D17F File Offset: 0x0000B37F
		public int Identity
		{
			[CompilerGenerated]
			get
			{
				return this.<Identity>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Identity>k__BackingField = value;
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0000D188 File Offset: 0x0000B388
		public CallbackIdentityAttribute(int callbackNum)
		{
			this.Identity = callbackNum;
		}

		// Token: 0x04000A56 RID: 2646
		[CompilerGenerated]
		private int <Identity>k__BackingField;
	}
}
