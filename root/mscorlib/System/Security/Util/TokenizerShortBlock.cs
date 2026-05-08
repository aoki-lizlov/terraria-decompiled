using System;

namespace System.Security.Util
{
	// Token: 0x020003C5 RID: 965
	internal sealed class TokenizerShortBlock
	{
		// Token: 0x0600294E RID: 10574 RVA: 0x00097C80 File Offset: 0x00095E80
		public TokenizerShortBlock()
		{
		}

		// Token: 0x04001E08 RID: 7688
		internal short[] m_block = new short[16];

		// Token: 0x04001E09 RID: 7689
		internal TokenizerShortBlock m_next;
	}
}
