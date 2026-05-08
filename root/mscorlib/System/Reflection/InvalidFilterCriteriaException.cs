using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000877 RID: 2167
	[Serializable]
	public class InvalidFilterCriteriaException : ApplicationException
	{
		// Token: 0x06004884 RID: 18564 RVA: 0x000EE324 File Offset: 0x000EC524
		public InvalidFilterCriteriaException()
			: this("Specified filter criteria was invalid.")
		{
		}

		// Token: 0x06004885 RID: 18565 RVA: 0x000EE331 File Offset: 0x000EC531
		public InvalidFilterCriteriaException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06004886 RID: 18566 RVA: 0x000EE33B File Offset: 0x000EC53B
		public InvalidFilterCriteriaException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146232831;
		}

		// Token: 0x06004887 RID: 18567 RVA: 0x0006F2E5 File Offset: 0x0006D4E5
		protected InvalidFilterCriteriaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
