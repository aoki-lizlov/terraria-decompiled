using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000C0 RID: 192
	[Serializable]
	public class ArrayTypeMismatchException : SystemException
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x00019063 File Offset: 0x00017263
		public ArrayTypeMismatchException()
			: base("Attempted to access an element as a type incompatible with the array.")
		{
			base.HResult = -2146233085;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001907B File Offset: 0x0001727B
		public ArrayTypeMismatchException(string message)
			: base(message)
		{
			base.HResult = -2146233085;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001908F File Offset: 0x0001728F
		public ArrayTypeMismatchException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233085;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000183F5 File Offset: 0x000165F5
		protected ArrayTypeMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
