using System;

namespace NVorbis.Contracts.Ogg
{
	// Token: 0x02000037 RID: 55
	internal interface ICrc
	{
		// Token: 0x06000237 RID: 567
		void Reset();

		// Token: 0x06000238 RID: 568
		void Update(int nextVal);

		// Token: 0x06000239 RID: 569
		bool Test(uint checkCrc);
	}
}
