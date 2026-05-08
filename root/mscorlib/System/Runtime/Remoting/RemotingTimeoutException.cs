using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x0200053E RID: 1342
	[ComVisible(true)]
	[Serializable]
	public class RemotingTimeoutException : RemotingException
	{
		// Token: 0x06003641 RID: 13889 RVA: 0x000C5361 File Offset: 0x000C3561
		public RemotingTimeoutException()
		{
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000C5369 File Offset: 0x000C3569
		public RemotingTimeoutException(string message)
			: base(message)
		{
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000C5372 File Offset: 0x000C3572
		public RemotingTimeoutException(string message, Exception InnerException)
			: base(message, InnerException)
		{
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000C537C File Offset: 0x000C357C
		internal RemotingTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
