using System;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x0200094A RID: 2378
	[Serializable]
	public class DriveNotFoundException : IOException
	{
		// Token: 0x0600557F RID: 21887 RVA: 0x00120E47 File Offset: 0x0011F047
		public DriveNotFoundException()
			: base("Could not find the drive. The drive might not be ready or might not be mapped.")
		{
			base.HResult = -2147024893;
		}

		// Token: 0x06005580 RID: 21888 RVA: 0x001186F0 File Offset: 0x001168F0
		public DriveNotFoundException(string message)
			: base(message)
		{
			base.HResult = -2147024893;
		}

		// Token: 0x06005581 RID: 21889 RVA: 0x00118704 File Offset: 0x00116904
		public DriveNotFoundException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024893;
		}

		// Token: 0x06005582 RID: 21890 RVA: 0x00118719 File Offset: 0x00116919
		protected DriveNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
