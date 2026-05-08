using System;

namespace Terraria.Graphics.CameraModifiers
{
	// Token: 0x0200021B RID: 539
	public interface ICameraModifier
	{
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060021C9 RID: 8649
		string UniqueIdentity { get; }

		// Token: 0x060021CA RID: 8650
		void Update(ref CameraInfo cameraPosition);

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060021CB RID: 8651
		bool IsAScreenShake { get; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060021CC RID: 8652
		bool Finished { get; }
	}
}
