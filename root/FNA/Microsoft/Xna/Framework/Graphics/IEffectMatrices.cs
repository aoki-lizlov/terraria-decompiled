using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200008E RID: 142
	public interface IEffectMatrices
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060011CF RID: 4559
		// (set) Token: 0x060011D0 RID: 4560
		Matrix Projection { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060011D1 RID: 4561
		// (set) Token: 0x060011D2 RID: 4562
		Matrix View { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060011D3 RID: 4563
		// (set) Token: 0x060011D4 RID: 4564
		Matrix World { get; set; }
	}
}
