using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200013F RID: 319
	public class ResolveEventArgs : EventArgs
	{
		// Token: 0x06000D13 RID: 3347 RVA: 0x00034575 File Offset: 0x00032775
		public ResolveEventArgs(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00034584 File Offset: 0x00032784
		public ResolveEventArgs(string name, Assembly requestingAssembly)
		{
			this.Name = name;
			this.RequestingAssembly = requestingAssembly;
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x0003459A File Offset: 0x0003279A
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x000345A2 File Offset: 0x000327A2
		public Assembly RequestingAssembly
		{
			[CompilerGenerated]
			get
			{
				return this.<RequestingAssembly>k__BackingField;
			}
		}

		// Token: 0x0400115A RID: 4442
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;

		// Token: 0x0400115B RID: 4443
		[CompilerGenerated]
		private readonly Assembly <RequestingAssembly>k__BackingField;
	}
}
