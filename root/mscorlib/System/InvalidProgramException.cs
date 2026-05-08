using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000114 RID: 276
	[Serializable]
	public sealed class InvalidProgramException : SystemException
	{
		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002A1F5 File Offset: 0x000283F5
		public InvalidProgramException()
			: base("Common Language Runtime detected an invalid program.")
		{
			base.HResult = -2146233030;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002A20D File Offset: 0x0002840D
		public InvalidProgramException(string message)
			: base(message)
		{
			base.HResult = -2146233030;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002A221 File Offset: 0x00028421
		public InvalidProgramException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233030;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x000183F5 File Offset: 0x000165F5
		internal InvalidProgramException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
