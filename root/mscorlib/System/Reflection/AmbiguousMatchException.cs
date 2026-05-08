using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x0200084C RID: 2124
	[Serializable]
	public sealed class AmbiguousMatchException : SystemException
	{
		// Token: 0x060047DD RID: 18397 RVA: 0x000ED9CC File Offset: 0x000EBBCC
		public AmbiguousMatchException()
			: base("Ambiguous match found.")
		{
			base.HResult = -2147475171;
		}

		// Token: 0x060047DE RID: 18398 RVA: 0x000ED9E4 File Offset: 0x000EBBE4
		public AmbiguousMatchException(string message)
			: base(message)
		{
			base.HResult = -2147475171;
		}

		// Token: 0x060047DF RID: 18399 RVA: 0x000ED9F8 File Offset: 0x000EBBF8
		public AmbiguousMatchException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2147475171;
		}

		// Token: 0x060047E0 RID: 18400 RVA: 0x000183F5 File Offset: 0x000165F5
		internal AmbiguousMatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
