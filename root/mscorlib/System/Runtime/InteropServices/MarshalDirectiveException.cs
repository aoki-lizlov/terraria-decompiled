using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000698 RID: 1688
	[Serializable]
	public class MarshalDirectiveException : SystemException
	{
		// Token: 0x06003F72 RID: 16242 RVA: 0x000DF893 File Offset: 0x000DDA93
		public MarshalDirectiveException()
			: base("Marshaling directives are invalid.")
		{
			base.HResult = -2146233035;
		}

		// Token: 0x06003F73 RID: 16243 RVA: 0x000DF8AB File Offset: 0x000DDAAB
		public MarshalDirectiveException(string message)
			: base(message)
		{
			base.HResult = -2146233035;
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x000DF8BF File Offset: 0x000DDABF
		public MarshalDirectiveException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233035;
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x000183F5 File Offset: 0x000165F5
		protected MarshalDirectiveException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
