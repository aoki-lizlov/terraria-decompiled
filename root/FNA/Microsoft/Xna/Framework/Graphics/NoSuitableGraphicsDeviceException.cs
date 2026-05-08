using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000A8 RID: 168
	[Serializable]
	public sealed class NoSuitableGraphicsDeviceException : Exception
	{
		// Token: 0x060013EC RID: 5100 RVA: 0x00024236 File Offset: 0x00022436
		public NoSuitableGraphicsDeviceException()
		{
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0002423E File Offset: 0x0002243E
		public NoSuitableGraphicsDeviceException(string message)
			: base(message)
		{
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x00024247 File Offset: 0x00022447
		public NoSuitableGraphicsDeviceException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
