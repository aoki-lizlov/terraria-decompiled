using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x0200052E RID: 1326
	[ComVisible(true)]
	public interface IEnvoyInfo
	{
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600357B RID: 13691
		// (set) Token: 0x0600357C RID: 13692
		IMessageSink EnvoySinks { get; set; }
	}
}
