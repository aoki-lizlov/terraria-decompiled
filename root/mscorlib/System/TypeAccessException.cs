using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000179 RID: 377
	[Serializable]
	public class TypeAccessException : TypeLoadException
	{
		// Token: 0x060011D2 RID: 4562 RVA: 0x0004876F File Offset: 0x0004696F
		public TypeAccessException()
			: base("Attempt to access the type failed.")
		{
			base.HResult = -2146233021;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00048787 File Offset: 0x00046987
		public TypeAccessException(string message)
			: base(message)
		{
			base.HResult = -2146233021;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0004879B File Offset: 0x0004699B
		public TypeAccessException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233021;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0001FBE2 File Offset: 0x0001DDE2
		protected TypeAccessException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
