using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000DA RID: 218
	[Serializable]
	public class DuplicateWaitObjectException : ArgumentException
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x000200E7 File Offset: 0x0001E2E7
		private static string DuplicateWaitObjectMessage
		{
			get
			{
				if (DuplicateWaitObjectException.s_duplicateWaitObjectMessage == null)
				{
					DuplicateWaitObjectException.s_duplicateWaitObjectMessage = "Duplicate objects in argument.";
				}
				return DuplicateWaitObjectException.s_duplicateWaitObjectMessage;
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00020105 File Offset: 0x0001E305
		public DuplicateWaitObjectException()
			: base(DuplicateWaitObjectException.DuplicateWaitObjectMessage)
		{
			base.HResult = -2146233047;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002011D File Offset: 0x0001E31D
		public DuplicateWaitObjectException(string parameterName)
			: base(DuplicateWaitObjectException.DuplicateWaitObjectMessage, parameterName)
		{
			base.HResult = -2146233047;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00020136 File Offset: 0x0001E336
		public DuplicateWaitObjectException(string parameterName, string message)
			: base(message, parameterName)
		{
			base.HResult = -2146233047;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002014B File Offset: 0x0001E34B
		public DuplicateWaitObjectException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233047;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00018A9F File Offset: 0x00016C9F
		protected DuplicateWaitObjectException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000F64 RID: 3940
		private static volatile string s_duplicateWaitObjectMessage;
	}
}
