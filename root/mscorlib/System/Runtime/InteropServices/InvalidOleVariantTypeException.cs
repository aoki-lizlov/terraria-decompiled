using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006AB RID: 1707
	[Serializable]
	public class InvalidOleVariantTypeException : SystemException
	{
		// Token: 0x06003FD2 RID: 16338 RVA: 0x000E0741 File Offset: 0x000DE941
		public InvalidOleVariantTypeException()
			: base("Specified OLE variant was invalid.")
		{
			base.HResult = -2146233039;
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x000E0759 File Offset: 0x000DE959
		public InvalidOleVariantTypeException(string message)
			: base(message)
		{
			base.HResult = -2146233039;
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x000E076D File Offset: 0x000DE96D
		public InvalidOleVariantTypeException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233039;
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x000183F5 File Offset: 0x000165F5
		protected InvalidOleVariantTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
