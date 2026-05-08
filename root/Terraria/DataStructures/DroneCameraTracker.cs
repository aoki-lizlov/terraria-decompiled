using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x0200053F RID: 1343
	public class DroneCameraTracker
	{
		// Token: 0x06003764 RID: 14180 RVA: 0x0062EED0 File Offset: 0x0062D0D0
		public void Track(Projectile proj)
		{
			this._trackedProjectile = proj;
			this._lastTrackedType = proj.type;
		}

		// Token: 0x06003765 RID: 14181 RVA: 0x0062EEE5 File Offset: 0x0062D0E5
		public void WorldClear()
		{
			this._lastTrackedType = 0;
			this._trackedProjectile = null;
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x0062EEF8 File Offset: 0x0062D0F8
		public bool TryTracking(out Vector2 cameraPosition)
		{
			cameraPosition = default(Vector2);
			if (this._trackedProjectile == null || !this._trackedProjectile.active || this._trackedProjectile.type != this._lastTrackedType || this._trackedProjectile.owner != Main.myPlayer || !Main.LocalPlayer.remoteVisionForDrone)
			{
				this._trackedProjectile = null;
				return false;
			}
			cameraPosition = this._trackedProjectile.Center;
			return true;
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x0000357B File Offset: 0x0000177B
		public DroneCameraTracker()
		{
		}

		// Token: 0x04005B97 RID: 23447
		private Projectile _trackedProjectile;

		// Token: 0x04005B98 RID: 23448
		private int _lastTrackedType;
	}
}
