using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000127 RID: 295
	[Serializable]
	public class NotImplementedException : SystemException
	{
		// Token: 0x06000C0E RID: 3086 RVA: 0x0002D90F File Offset: 0x0002BB0F
		public NotImplementedException()
			: base("The method or operation is not implemented.")
		{
			base.HResult = -2147467263;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002D927 File Offset: 0x0002BB27
		public NotImplementedException(string message)
			: base(message)
		{
			base.HResult = -2147467263;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002D93B File Offset: 0x0002BB3B
		public NotImplementedException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2147467263;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000183F5 File Offset: 0x000165F5
		protected NotImplementedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
