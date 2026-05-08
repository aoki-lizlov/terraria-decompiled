using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000B9 RID: 185
	[Serializable]
	public class ApplicationException : Exception
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x000188F6 File Offset: 0x00016AF6
		public ApplicationException()
			: base("Error in the application.")
		{
			base.HResult = -2146232832;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001890E File Offset: 0x00016B0E
		public ApplicationException(string message)
			: base(message)
		{
			base.HResult = -2146232832;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00018922 File Offset: 0x00016B22
		public ApplicationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232832;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00018937 File Offset: 0x00016B37
		protected ApplicationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
