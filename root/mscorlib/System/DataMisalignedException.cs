using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000D2 RID: 210
	[Serializable]
	public sealed class DataMisalignedException : SystemException
	{
		// Token: 0x060007D5 RID: 2005 RVA: 0x0001D28D File Offset: 0x0001B48D
		public DataMisalignedException()
			: base("A datatype misalignment was detected in a load or store instruction.")
		{
			base.HResult = -2146233023;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001D2A5 File Offset: 0x0001B4A5
		public DataMisalignedException(string message)
			: base(message)
		{
			base.HResult = -2146233023;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001D2B9 File Offset: 0x0001B4B9
		public DataMisalignedException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233023;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000183F5 File Offset: 0x000165F5
		internal DataMisalignedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
