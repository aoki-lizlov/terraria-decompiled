using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200007B RID: 123
	[Serializable]
	public sealed class DeviceNotResetException : Exception
	{
		// Token: 0x06001101 RID: 4353 RVA: 0x00024236 File Offset: 0x00022436
		public DeviceNotResetException()
		{
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0002423E File Offset: 0x0002243E
		public DeviceNotResetException(string message)
			: base(message)
		{
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00024247 File Offset: 0x00022447
		public DeviceNotResetException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
