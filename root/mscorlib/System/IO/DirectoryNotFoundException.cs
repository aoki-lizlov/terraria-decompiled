using System;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x02000921 RID: 2337
	[Serializable]
	public class DirectoryNotFoundException : IOException
	{
		// Token: 0x06005369 RID: 21353 RVA: 0x001186D8 File Offset: 0x001168D8
		public DirectoryNotFoundException()
			: base("Attempted to access a path that is not on the disk.")
		{
			base.HResult = -2147024893;
		}

		// Token: 0x0600536A RID: 21354 RVA: 0x001186F0 File Offset: 0x001168F0
		public DirectoryNotFoundException(string message)
			: base(message)
		{
			base.HResult = -2147024893;
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x00118704 File Offset: 0x00116904
		public DirectoryNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024893;
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x00118719 File Offset: 0x00116919
		protected DirectoryNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
