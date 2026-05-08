using System;
using System.Runtime.Serialization;

namespace System.Security
{
	// Token: 0x02000394 RID: 916
	[Serializable]
	public class VerificationException : SystemException
	{
		// Token: 0x060027FE RID: 10238 RVA: 0x00092B58 File Offset: 0x00090D58
		public VerificationException()
			: base("Operation could destabilize the runtime.")
		{
			base.HResult = -2146233075;
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x00092B70 File Offset: 0x00090D70
		public VerificationException(string message)
			: base(message)
		{
			base.HResult = -2146233075;
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x00092B84 File Offset: 0x00090D84
		public VerificationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233075;
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x000183F5 File Offset: 0x000165F5
		protected VerificationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
