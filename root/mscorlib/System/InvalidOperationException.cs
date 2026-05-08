using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000113 RID: 275
	[Serializable]
	public class InvalidOperationException : SystemException
	{
		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002A1B4 File Offset: 0x000283B4
		public InvalidOperationException()
			: base("Operation is not valid due to the current state of the object.")
		{
			base.HResult = -2146233079;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002A1CC File Offset: 0x000283CC
		public InvalidOperationException(string message)
			: base(message)
		{
			base.HResult = -2146233079;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002A1E0 File Offset: 0x000283E0
		public InvalidOperationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233079;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000183F5 File Offset: 0x000165F5
		protected InvalidOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
