using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001A6 RID: 422
	[Serializable]
	public class OutOfMemoryException : SystemException
	{
		// Token: 0x060013C0 RID: 5056 RVA: 0x0004F6C8 File Offset: 0x0004D8C8
		public OutOfMemoryException()
			: base("Insufficient memory to continue the execution of the program.")
		{
			base.HResult = -2147024882;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0004F6E0 File Offset: 0x0004D8E0
		public OutOfMemoryException(string message)
			: base(message)
		{
			base.HResult = -2147024882;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0004F6F4 File Offset: 0x0004D8F4
		public OutOfMemoryException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024882;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000183F5 File Offset: 0x000165F5
		protected OutOfMemoryException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
