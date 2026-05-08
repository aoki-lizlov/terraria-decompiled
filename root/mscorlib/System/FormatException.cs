using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000E2 RID: 226
	[Serializable]
	public class FormatException : SystemException
	{
		// Token: 0x06000914 RID: 2324 RVA: 0x00020239 File Offset: 0x0001E439
		public FormatException()
			: base("One of the identified items was in an invalid format.")
		{
			base.HResult = -2146233033;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00020251 File Offset: 0x0001E451
		public FormatException(string message)
			: base(message)
		{
			base.HResult = -2146233033;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00020265 File Offset: 0x0001E465
		public FormatException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233033;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000183F5 File Offset: 0x000165F5
		protected FormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
