using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006AC RID: 1708
	[Serializable]
	public class SEHException : ExternalException
	{
		// Token: 0x06003FD6 RID: 16342 RVA: 0x000E0782 File Offset: 0x000DE982
		public SEHException()
		{
			base.HResult = -2147467259;
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x000E0795 File Offset: 0x000DE995
		public SEHException(string message)
			: base(message)
		{
			base.HResult = -2147467259;
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x000E07A9 File Offset: 0x000DE9A9
		public SEHException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2147467259;
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x000E0597 File Offset: 0x000DE797
		protected SEHException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool CanResume()
		{
			return false;
		}
	}
}
