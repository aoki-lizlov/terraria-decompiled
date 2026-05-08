using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x0200053B RID: 1339
	[ComVisible(true)]
	[Serializable]
	public class RemotingException : SystemException
	{
		// Token: 0x060035FF RID: 13823 RVA: 0x00092B99 File Offset: 0x00090D99
		public RemotingException()
		{
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x0006F1CD File Offset: 0x0006D3CD
		public RemotingException(string message)
			: base(message)
		{
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000183F5 File Offset: 0x000165F5
		protected RemotingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x0006F1D6 File Offset: 0x0006D3D6
		public RemotingException(string message, Exception InnerException)
			: base(message, InnerException)
		{
		}
	}
}
