using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200019B RID: 411
	[Serializable]
	[StructLayout(0, Pack = 4)]
	public struct SteamDatagramHostedAddress
	{
		// Token: 0x060009C3 RID: 2499 RVA: 0x0000F2DC File Offset: 0x0000D4DC
		public void Clear()
		{
			this.m_cbSize = 0;
			this.m_data = new byte[128];
		}

		// Token: 0x04000AB6 RID: 2742
		public int m_cbSize;

		// Token: 0x04000AB7 RID: 2743
		[MarshalAs(30, SizeConst = 128)]
		public byte[] m_data;
	}
}
