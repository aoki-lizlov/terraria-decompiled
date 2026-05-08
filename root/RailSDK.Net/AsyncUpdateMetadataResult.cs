using System;

namespace rail
{
	// Token: 0x02000178 RID: 376
	public class AsyncUpdateMetadataResult : EventBase
	{
		// Token: 0x0600189F RID: 6303 RVA: 0x0001105B File Offset: 0x0000F25B
		public AsyncUpdateMetadataResult()
		{
		}

		// Token: 0x0400053E RID: 1342
		public EnumRailSpaceWorkType type;

		// Token: 0x0400053F RID: 1343
		public SpaceWorkID id = new SpaceWorkID();
	}
}
