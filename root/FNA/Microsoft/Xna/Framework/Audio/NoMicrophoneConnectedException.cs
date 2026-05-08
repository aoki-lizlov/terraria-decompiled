using System;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x02000158 RID: 344
	[Serializable]
	public sealed class NoMicrophoneConnectedException : Exception
	{
		// Token: 0x0600183E RID: 6206 RVA: 0x00024236 File Offset: 0x00022436
		public NoMicrophoneConnectedException()
		{
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0002423E File Offset: 0x0002243E
		public NoMicrophoneConnectedException(string message)
			: base(message)
		{
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00024247 File Offset: 0x00022447
		public NoMicrophoneConnectedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
