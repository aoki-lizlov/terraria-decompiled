using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Storage
{
	// Token: 0x0200003E RID: 62
	public class StorageDeviceNotConnectedException : ExternalException
	{
		// Token: 0x06000E73 RID: 3699 RVA: 0x0001F5B9 File Offset: 0x0001D7B9
		public StorageDeviceNotConnectedException()
		{
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0001F5C1 File Offset: 0x0001D7C1
		public StorageDeviceNotConnectedException(string message)
			: base(message)
		{
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0001F5CA File Offset: 0x0001D7CA
		public StorageDeviceNotConnectedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
