using System;
using System.Runtime.Serialization;

namespace System.Runtime
{
	// Token: 0x0200051F RID: 1311
	[Serializable]
	public sealed class AmbiguousImplementationException : Exception
	{
		// Token: 0x06003545 RID: 13637 RVA: 0x000C19A7 File Offset: 0x000BFBA7
		public AmbiguousImplementationException()
			: base("Ambiguous implementation found.")
		{
			base.HResult = -2146234262;
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000C19BF File Offset: 0x000BFBBF
		public AmbiguousImplementationException(string message)
			: base(message)
		{
			base.HResult = -2146234262;
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000C19D3 File Offset: 0x000BFBD3
		public AmbiguousImplementationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146234262;
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x00018937 File Offset: 0x00016B37
		private AmbiguousImplementationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
