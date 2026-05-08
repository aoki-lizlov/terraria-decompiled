using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000D8 RID: 216
	[Serializable]
	public class DllNotFoundException : TypeLoadException
	{
		// Token: 0x060008C1 RID: 2241 RVA: 0x0001FBA1 File Offset: 0x0001DDA1
		public DllNotFoundException()
			: base("Dll was not found.")
		{
			base.HResult = -2146233052;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001FBB9 File Offset: 0x0001DDB9
		public DllNotFoundException(string message)
			: base(message)
		{
			base.HResult = -2146233052;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001FBCD File Offset: 0x0001DDCD
		public DllNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233052;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001FBE2 File Offset: 0x0001DDE2
		protected DllNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
