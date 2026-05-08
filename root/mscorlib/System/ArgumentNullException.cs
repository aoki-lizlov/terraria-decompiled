using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000BB RID: 187
	[Serializable]
	public class ArgumentNullException : ArgumentException
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x00018A44 File Offset: 0x00016C44
		public ArgumentNullException()
			: base("Value cannot be null.")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00018A5C File Offset: 0x00016C5C
		public ArgumentNullException(string paramName)
			: base("Value cannot be null.", paramName)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00018A75 File Offset: 0x00016C75
		public ArgumentNullException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00018A8A File Offset: 0x00016C8A
		public ArgumentNullException(string paramName, string message)
			: base(message, paramName)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00018A9F File Offset: 0x00016C9F
		protected ArgumentNullException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
