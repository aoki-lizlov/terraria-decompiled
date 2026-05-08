using System;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	// Token: 0x02000AF8 RID: 2808
	[Serializable]
	public class KeyNotFoundException : SystemException
	{
		// Token: 0x0600673E RID: 26430 RVA: 0x0015E1FC File Offset: 0x0015C3FC
		public KeyNotFoundException()
			: base("The given key was not present in the dictionary.")
		{
			base.HResult = -2146232969;
		}

		// Token: 0x0600673F RID: 26431 RVA: 0x0015E214 File Offset: 0x0015C414
		public KeyNotFoundException(string message)
			: base(message)
		{
			base.HResult = -2146232969;
		}

		// Token: 0x06006740 RID: 26432 RVA: 0x0015E228 File Offset: 0x0015C428
		public KeyNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232969;
		}

		// Token: 0x06006741 RID: 26433 RVA: 0x000183F5 File Offset: 0x000165F5
		protected KeyNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
