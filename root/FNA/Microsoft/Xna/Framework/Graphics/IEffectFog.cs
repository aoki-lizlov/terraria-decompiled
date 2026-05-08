using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200008C RID: 140
	public interface IEffectFog
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060011BF RID: 4543
		// (set) Token: 0x060011C0 RID: 4544
		Vector3 FogColor { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060011C1 RID: 4545
		// (set) Token: 0x060011C2 RID: 4546
		bool FogEnabled { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060011C3 RID: 4547
		// (set) Token: 0x060011C4 RID: 4548
		float FogEnd { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060011C5 RID: 4549
		// (set) Token: 0x060011C6 RID: 4550
		float FogStart { get; set; }
	}
}
