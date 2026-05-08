using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x02000157 RID: 343
	[Serializable]
	public sealed class NoAudioHardwareException : ExternalException
	{
		// Token: 0x0600183B RID: 6203 RVA: 0x0001F5B9 File Offset: 0x0001D7B9
		public NoAudioHardwareException()
		{
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0001F5C1 File Offset: 0x0001D7C1
		public NoAudioHardwareException(string message)
			: base(message)
		{
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0001F5CA File Offset: 0x0001D7CA
		public NoAudioHardwareException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
