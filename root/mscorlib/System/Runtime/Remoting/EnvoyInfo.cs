using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x0200052C RID: 1324
	[Serializable]
	internal class EnvoyInfo : IEnvoyInfo
	{
		// Token: 0x06003576 RID: 13686 RVA: 0x000C1DA9 File Offset: 0x000BFFA9
		public EnvoyInfo(IMessageSink sinks)
		{
			this.envoySinks = sinks;
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x000C1DB8 File Offset: 0x000BFFB8
		// (set) Token: 0x06003578 RID: 13688 RVA: 0x000C1DC0 File Offset: 0x000BFFC0
		public IMessageSink EnvoySinks
		{
			get
			{
				return this.envoySinks;
			}
			set
			{
				this.envoySinks = value;
			}
		}

		// Token: 0x0400249F RID: 9375
		private IMessageSink envoySinks;
	}
}
