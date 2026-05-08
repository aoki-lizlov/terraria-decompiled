using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017A RID: 378
	public struct MatchMakingKeyValuePair_t
	{
		// Token: 0x060008B1 RID: 2225 RVA: 0x0000C5DB File Offset: 0x0000A7DB
		private MatchMakingKeyValuePair_t(string strKey, string strValue)
		{
			this.m_szKey = strKey;
			this.m_szValue = strValue;
		}

		// Token: 0x04000A21 RID: 2593
		[MarshalAs(23, SizeConst = 256)]
		public string m_szKey;

		// Token: 0x04000A22 RID: 2594
		[MarshalAs(23, SizeConst = 256)]
		public string m_szValue;
	}
}
