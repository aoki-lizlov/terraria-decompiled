using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000158 RID: 344
	[Serializable]
	public class SystemException : Exception
	{
		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003D57A File Offset: 0x0003B77A
		public SystemException()
			: base("System error.")
		{
			base.HResult = -2146233087;
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003D592 File Offset: 0x0003B792
		public SystemException(string message)
			: base(message)
		{
			base.HResult = -2146233087;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003D5A6 File Offset: 0x0003B7A6
		public SystemException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233087;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00018937 File Offset: 0x00016B37
		protected SystemException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
