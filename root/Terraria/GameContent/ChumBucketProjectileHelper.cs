using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x0200024B RID: 587
	public class ChumBucketProjectileHelper
	{
		// Token: 0x06002308 RID: 8968 RVA: 0x0053C4E2 File Offset: 0x0053A6E2
		public void OnPreUpdateAllProjectiles()
		{
			Utils.Swap<Dictionary<Point, int>>(ref this._chumCountsPendingForThisFrame, ref this._chumCountsFromLastFrame);
			this._chumCountsPendingForThisFrame.Clear();
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x0053C500 File Offset: 0x0053A700
		public void AddChumLocation(Vector2 spot)
		{
			Point point = spot.ToTileCoordinates();
			int num = 0;
			this._chumCountsPendingForThisFrame.TryGetValue(point, out num);
			num++;
			this._chumCountsPendingForThisFrame[point] = num;
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x0053C538 File Offset: 0x0053A738
		public int GetChumsInLocation(Point tileCoords)
		{
			int num = 0;
			this._chumCountsFromLastFrame.TryGetValue(tileCoords, out num);
			return num;
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x0053C557 File Offset: 0x0053A757
		public ChumBucketProjectileHelper()
		{
		}

		// Token: 0x04004D44 RID: 19780
		private Dictionary<Point, int> _chumCountsPendingForThisFrame = new Dictionary<Point, int>();

		// Token: 0x04004D45 RID: 19781
		private Dictionary<Point, int> _chumCountsFromLastFrame = new Dictionary<Point, int>();
	}
}
