using System;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200008D RID: 141
	public interface IEffectLights
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060011C7 RID: 4551
		// (set) Token: 0x060011C8 RID: 4552
		Vector3 AmbientLightColor { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060011C9 RID: 4553
		DirectionalLight DirectionalLight0 { get; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060011CA RID: 4554
		DirectionalLight DirectionalLight1 { get; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060011CB RID: 4555
		DirectionalLight DirectionalLight2 { get; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060011CC RID: 4556
		// (set) Token: 0x060011CD RID: 4557
		bool LightingEnabled { get; set; }

		// Token: 0x060011CE RID: 4558
		void EnableDefaultLighting();
	}
}
