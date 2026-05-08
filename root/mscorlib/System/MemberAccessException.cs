using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200011D RID: 285
	[Serializable]
	public class MemberAccessException : SystemException
	{
		// Token: 0x06000B7B RID: 2939 RVA: 0x0002B1A7 File Offset: 0x000293A7
		public MemberAccessException()
			: base("Cannot access member.")
		{
			base.HResult = -2146233062;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002B1BF File Offset: 0x000293BF
		public MemberAccessException(string message)
			: base(message)
		{
			base.HResult = -2146233062;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002B1D3 File Offset: 0x000293D3
		public MemberAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233062;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x000183F5 File Offset: 0x000165F5
		protected MemberAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
