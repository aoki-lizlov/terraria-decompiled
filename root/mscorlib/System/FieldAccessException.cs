using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000E0 RID: 224
	[Serializable]
	public class FieldAccessException : MemberAccessException
	{
		// Token: 0x0600090F RID: 2319 RVA: 0x000201EE File Offset: 0x0001E3EE
		public FieldAccessException()
			: base("Attempted to access a field that is not accessible by the caller.")
		{
			base.HResult = -2146233081;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00020206 File Offset: 0x0001E406
		public FieldAccessException(string message)
			: base(message)
		{
			base.HResult = -2146233081;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0002021A File Offset: 0x0001E41A
		public FieldAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233081;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0002022F File Offset: 0x0001E42F
		protected FieldAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
