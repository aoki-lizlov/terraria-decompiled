using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Golf
{
	// Token: 0x02000318 RID: 792
	public class GolfBallTrackRecord
	{
		// Token: 0x0600275F RID: 10079 RVA: 0x0056735D File Offset: 0x0056555D
		public GolfBallTrackRecord()
		{
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x00567370 File Offset: 0x00565570
		public void RecordHit(Vector2 position)
		{
			this._hitLocations.Add(position);
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x00567380 File Offset: 0x00565580
		public int GetAccumulatedScore()
		{
			double num;
			int num2;
			this.GetTrackInfo(out num, out num2);
			int num3 = (int)(num / 16.0);
			int num4 = num2 + 2;
			return num3 / num4;
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x005673AC File Offset: 0x005655AC
		private void GetTrackInfo(out double totalDistancePassed, out int hitsMade)
		{
			hitsMade = 0;
			totalDistancePassed = 0.0;
			int i = 0;
			while (i < this._hitLocations.Count - 1)
			{
				totalDistancePassed += (double)Vector2.Distance(this._hitLocations[i], this._hitLocations[i + 1]);
				i++;
				hitsMade++;
			}
		}

		// Token: 0x040050D9 RID: 20697
		private List<Vector2> _hitLocations = new List<Vector2>();
	}
}
