using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000193 RID: 403
	[Serializable]
	public sealed class ConsoleCancelEventArgs : EventArgs
	{
		// Token: 0x0600131E RID: 4894 RVA: 0x0004E2DE File Offset: 0x0004C4DE
		internal ConsoleCancelEventArgs(ConsoleSpecialKey type)
		{
			this._type = type;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0004E2ED File Offset: 0x0004C4ED
		// (set) Token: 0x06001320 RID: 4896 RVA: 0x0004E2F5 File Offset: 0x0004C4F5
		public bool Cancel
		{
			[CompilerGenerated]
			get
			{
				return this.<Cancel>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Cancel>k__BackingField = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x0004E2FE File Offset: 0x0004C4FE
		public ConsoleSpecialKey SpecialKey
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x0400127D RID: 4733
		private readonly ConsoleSpecialKey _type;

		// Token: 0x0400127E RID: 4734
		[CompilerGenerated]
		private bool <Cancel>k__BackingField;
	}
}
