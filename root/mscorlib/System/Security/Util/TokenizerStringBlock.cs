using System;

namespace System.Security.Util
{
	// Token: 0x020003C6 RID: 966
	internal sealed class TokenizerStringBlock
	{
		// Token: 0x0600294F RID: 10575 RVA: 0x00097C95 File Offset: 0x00095E95
		public TokenizerStringBlock()
		{
		}

		// Token: 0x04001E0A RID: 7690
		internal string[] m_block = new string[16];

		// Token: 0x04001E0B RID: 7691
		internal TokenizerStringBlock m_next;
	}
}
