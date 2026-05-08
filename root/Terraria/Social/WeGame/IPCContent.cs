using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Terraria.Social.WeGame
{
	// Token: 0x02000131 RID: 305
	public class IPCContent
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001C2A RID: 7210 RVA: 0x004FD8DD File Offset: 0x004FBADD
		// (set) Token: 0x06001C2B RID: 7211 RVA: 0x004FD8E5 File Offset: 0x004FBAE5
		public CancellationToken CancelToken
		{
			[CompilerGenerated]
			get
			{
				return this.<CancelToken>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CancelToken>k__BackingField = value;
			}
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0000357B File Offset: 0x0000177B
		public IPCContent()
		{
		}

		// Token: 0x040015B5 RID: 5557
		public byte[] data;

		// Token: 0x040015B6 RID: 5558
		[CompilerGenerated]
		private CancellationToken <CancelToken>k__BackingField;
	}
}
