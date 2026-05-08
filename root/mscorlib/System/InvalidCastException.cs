using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000112 RID: 274
	[Serializable]
	public class InvalidCastException : SystemException
	{
		// Token: 0x06000ABB RID: 2747 RVA: 0x0002A163 File Offset: 0x00028363
		public InvalidCastException()
			: base("Specified cast is not valid.")
		{
			base.HResult = -2147467262;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0002A17B File Offset: 0x0002837B
		public InvalidCastException(string message)
			: base(message)
		{
			base.HResult = -2147467262;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0002A18F File Offset: 0x0002838F
		public InvalidCastException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467262;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002A1A4 File Offset: 0x000283A4
		public InvalidCastException(string message, int errorCode)
			: base(message)
		{
			base.HResult = errorCode;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x000183F5 File Offset: 0x000165F5
		protected InvalidCastException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
