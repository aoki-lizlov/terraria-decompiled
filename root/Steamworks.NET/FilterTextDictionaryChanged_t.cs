using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020000FC RID: 252
	[CallbackIdentity(739)]
	[StructLayout(0, Pack = 4)]
	public struct FilterTextDictionaryChanged_t
	{
		// Token: 0x04000311 RID: 785
		public const int k_iCallback = 739;

		// Token: 0x04000312 RID: 786
		public int m_eLanguage;
	}
}
