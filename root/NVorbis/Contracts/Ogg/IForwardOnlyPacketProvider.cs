using System;

namespace NVorbis.Contracts.Ogg
{
	// Token: 0x02000038 RID: 56
	internal interface IForwardOnlyPacketProvider : IPacketProvider
	{
		// Token: 0x0600023A RID: 570
		bool AddPage(byte[] buf, bool isResync);

		// Token: 0x0600023B RID: 571
		void SetEndOfStream();
	}
}
