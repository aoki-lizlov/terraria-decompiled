using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.CameraModifiers
{
	// Token: 0x0200021C RID: 540
	public struct CameraInfo
	{
		// Token: 0x060021CD RID: 8653 RVA: 0x005329EB File Offset: 0x00530BEB
		public CameraInfo(Vector2 position)
		{
			this.OriginalCameraPosition = position;
			this.OriginalCameraCenter = position + Main.ScreenSize.ToVector2() / 2f;
			this.CameraPosition = this.OriginalCameraPosition;
		}

		// Token: 0x04004C39 RID: 19513
		public Vector2 CameraPosition;

		// Token: 0x04004C3A RID: 19514
		public Vector2 OriginalCameraCenter;

		// Token: 0x04004C3B RID: 19515
		public Vector2 OriginalCameraPosition;
	}
}
