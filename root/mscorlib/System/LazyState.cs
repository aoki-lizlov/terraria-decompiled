using System;

namespace System
{
	// Token: 0x02000116 RID: 278
	internal enum LazyState
	{
		// Token: 0x040010DF RID: 4319
		NoneViaConstructor,
		// Token: 0x040010E0 RID: 4320
		NoneViaFactory,
		// Token: 0x040010E1 RID: 4321
		NoneException,
		// Token: 0x040010E2 RID: 4322
		PublicationOnlyViaConstructor,
		// Token: 0x040010E3 RID: 4323
		PublicationOnlyViaFactory,
		// Token: 0x040010E4 RID: 4324
		PublicationOnlyWait,
		// Token: 0x040010E5 RID: 4325
		PublicationOnlyException,
		// Token: 0x040010E6 RID: 4326
		ExecutionAndPublicationViaConstructor,
		// Token: 0x040010E7 RID: 4327
		ExecutionAndPublicationViaFactory,
		// Token: 0x040010E8 RID: 4328
		ExecutionAndPublicationException
	}
}
