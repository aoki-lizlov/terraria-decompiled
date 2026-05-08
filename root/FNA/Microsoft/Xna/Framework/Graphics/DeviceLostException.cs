using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200007A RID: 122
	[Serializable]
	public sealed class DeviceLostException : Exception
	{
		// Token: 0x060010FE RID: 4350 RVA: 0x00024236 File Offset: 0x00022436
		public DeviceLostException()
		{
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0002423E File Offset: 0x0002243E
		public DeviceLostException(string message)
			: base(message)
		{
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00024247 File Offset: 0x00022447
		public DeviceLostException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
