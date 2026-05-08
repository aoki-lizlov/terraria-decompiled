using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000121 RID: 289
	[Serializable]
	public class MethodAccessException : MemberAccessException
	{
		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002D6CD File Offset: 0x0002B8CD
		public MethodAccessException()
			: base("Attempt to access the method failed.")
		{
			base.HResult = -2146233072;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002D6E5 File Offset: 0x0002B8E5
		public MethodAccessException(string message)
			: base(message)
		{
			base.HResult = -2146233072;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002D6F9 File Offset: 0x0002B8F9
		public MethodAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233072;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002022F File Offset: 0x0001E42F
		protected MethodAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
