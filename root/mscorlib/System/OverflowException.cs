using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000132 RID: 306
	[Serializable]
	public class OverflowException : ArithmeticException
	{
		// Token: 0x06000CA0 RID: 3232 RVA: 0x00032915 File Offset: 0x00030B15
		public OverflowException()
			: base("Arithmetic operation resulted in an overflow.")
		{
			base.HResult = -2146233066;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0003292D File Offset: 0x00030B2D
		public OverflowException(string message)
			: base(message)
		{
			base.HResult = -2146233066;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00032941 File Offset: 0x00030B41
		public OverflowException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233066;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0001FB97 File Offset: 0x0001DD97
		protected OverflowException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
