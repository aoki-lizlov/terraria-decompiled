using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x0200053F RID: 1343
	[ComVisible(true)]
	[Serializable]
	public class ServerException : SystemException
	{
		// Token: 0x06003645 RID: 13893 RVA: 0x00092B99 File Offset: 0x00090D99
		public ServerException()
		{
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x0006F1CD File Offset: 0x0006D3CD
		public ServerException(string message)
			: base(message)
		{
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x0006F1D6 File Offset: 0x0006D3D6
		public ServerException(string message, Exception InnerException)
			: base(message, InnerException)
		{
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x000183F5 File Offset: 0x000165F5
		internal ServerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
