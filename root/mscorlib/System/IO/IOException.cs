using System;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x0200092A RID: 2346
	[Serializable]
	public class IOException : SystemException
	{
		// Token: 0x0600538F RID: 21391 RVA: 0x00118BD2 File Offset: 0x00116DD2
		public IOException()
			: base("I/O error occurred.")
		{
			base.HResult = -2146232800;
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x00118BEA File Offset: 0x00116DEA
		public IOException(string message)
			: base(message)
		{
			base.HResult = -2146232800;
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x0002A1A4 File Offset: 0x000283A4
		public IOException(string message, int hresult)
			: base(message)
		{
			base.HResult = hresult;
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x00118BFE File Offset: 0x00116DFE
		public IOException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232800;
		}

		// Token: 0x06005393 RID: 21395 RVA: 0x000183F5 File Offset: 0x000165F5
		protected IOException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
