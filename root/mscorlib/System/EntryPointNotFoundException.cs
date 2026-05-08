using System;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020000DB RID: 219
	[Serializable]
	public class EntryPointNotFoundException : TypeLoadException
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x00020160 File Offset: 0x0001E360
		public EntryPointNotFoundException()
			: base("Entry point was not found.")
		{
			base.HResult = -2146233053;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00020178 File Offset: 0x0001E378
		public EntryPointNotFoundException(string message)
			: base(message)
		{
			base.HResult = -2146233053;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0002018C File Offset: 0x0001E38C
		public EntryPointNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233053;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001FBE2 File Offset: 0x0001DDE2
		protected EntryPointNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
