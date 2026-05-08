using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x0200089A RID: 2202
	[Serializable]
	public class TargetException : ApplicationException
	{
		// Token: 0x06004A4D RID: 19021 RVA: 0x000EF798 File Offset: 0x000ED998
		public TargetException()
			: this(null)
		{
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x000EF7A1 File Offset: 0x000ED9A1
		public TargetException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06004A4F RID: 19023 RVA: 0x000EF7AB File Offset: 0x000ED9AB
		public TargetException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146232829;
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x0006F2E5 File Offset: 0x0006D4E5
		protected TargetException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
