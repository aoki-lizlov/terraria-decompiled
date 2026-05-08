using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Audio
{
	// Token: 0x02000154 RID: 340
	[Serializable]
	public sealed class InstancePlayLimitException : ExternalException
	{
		// Token: 0x06001826 RID: 6182 RVA: 0x0001F5B9 File Offset: 0x0001D7B9
		public InstancePlayLimitException()
		{
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0001F5C1 File Offset: 0x0001D7C1
		public InstancePlayLimitException(string message)
			: base(message)
		{
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0001F5CA File Offset: 0x0001D7CA
		public InstancePlayLimitException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
