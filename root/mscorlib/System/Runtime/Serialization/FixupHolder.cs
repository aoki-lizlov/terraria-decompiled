using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000634 RID: 1588
	[Serializable]
	internal class FixupHolder
	{
		// Token: 0x06003CD0 RID: 15568 RVA: 0x000D3CE6 File Offset: 0x000D1EE6
		internal FixupHolder(long id, object fixupInfo, int fixupType)
		{
			this.m_id = id;
			this.m_fixupInfo = fixupInfo;
			this.m_fixupType = fixupType;
		}

		// Token: 0x040026E7 RID: 9959
		internal const int ArrayFixup = 1;

		// Token: 0x040026E8 RID: 9960
		internal const int MemberFixup = 2;

		// Token: 0x040026E9 RID: 9961
		internal const int DelayedFixup = 4;

		// Token: 0x040026EA RID: 9962
		internal long m_id;

		// Token: 0x040026EB RID: 9963
		internal object m_fixupInfo;

		// Token: 0x040026EC RID: 9964
		internal int m_fixupType;
	}
}
