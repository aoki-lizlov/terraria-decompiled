using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000BD RID: 189
	[Serializable]
	public class ArithmeticException : SystemException
	{
		// Token: 0x06000548 RID: 1352 RVA: 0x00018BB9 File Offset: 0x00016DB9
		public ArithmeticException()
			: base("Overflow or underflow in the arithmetic operation.")
		{
			base.HResult = -2147024362;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00018BD1 File Offset: 0x00016DD1
		public ArithmeticException(string message)
			: base(message)
		{
			base.HResult = -2147024362;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00018BE5 File Offset: 0x00016DE5
		public ArithmeticException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024362;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000183F5 File Offset: 0x000165F5
		protected ArithmeticException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
