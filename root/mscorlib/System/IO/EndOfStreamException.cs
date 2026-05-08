using System;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000922 RID: 2338
	[Serializable]
	public class EndOfStreamException : IOException
	{
		// Token: 0x0600536D RID: 21357 RVA: 0x00118723 File Offset: 0x00116923
		public EndOfStreamException()
			: base("Attempted to read past the end of the stream.")
		{
			base.HResult = -2147024858;
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x0011873B File Offset: 0x0011693B
		public EndOfStreamException(string message)
			: base(message)
		{
			base.HResult = -2147024858;
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x0011874F File Offset: 0x0011694F
		public EndOfStreamException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024858;
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x00118719 File Offset: 0x00116919
		protected EndOfStreamException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
