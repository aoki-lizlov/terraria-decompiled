using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200013B RID: 315
	[Serializable]
	public class RankException : SystemException
	{
		// Token: 0x06000CE0 RID: 3296 RVA: 0x00033C39 File Offset: 0x00031E39
		public RankException()
			: base("Attempted to operate on an array with the incorrect number of dimensions.")
		{
			base.HResult = -2146233065;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00033C51 File Offset: 0x00031E51
		public RankException(string message)
			: base(message)
		{
			base.HResult = -2146233065;
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00033C65 File Offset: 0x00031E65
		public RankException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233065;
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000183F5 File Offset: 0x000165F5
		protected RankException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
