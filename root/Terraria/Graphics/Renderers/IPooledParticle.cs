using System;

namespace Terraria.Graphics.Renderers
{
	// Token: 0x02000205 RID: 517
	public interface IPooledParticle : IParticle
	{
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600212F RID: 8495
		bool IsRestingInPool { get; }

		// Token: 0x06002130 RID: 8496
		void RestInPool();

		// Token: 0x06002131 RID: 8497
		void FetchFromPool();
	}
}
