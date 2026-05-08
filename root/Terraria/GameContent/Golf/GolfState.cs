using System;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.GameContent.Golf
{
	// Token: 0x0200031A RID: 794
	public class GolfState
	{
		// Token: 0x06002772 RID: 10098 RVA: 0x00567B76 File Offset: 0x00565D76
		private void UpdateScoreTime()
		{
			if (this.golfScoreTime < this.golfScoreTimeMax)
			{
				this.golfScoreTime++;
			}
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x00567B94 File Offset: 0x00565D94
		public void ResetScoreTime()
		{
			this.golfScoreTime = 0;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x00567B9D File Offset: 0x00565D9D
		public void SetScoreTime()
		{
			this.golfScoreTime = this.golfScoreTimeMax;
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x00567BAB File Offset: 0x00565DAB
		public float ScoreAdjustment
		{
			get
			{
				return (float)this.golfScoreTime / (float)this.golfScoreTimeMax;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x00567BBC File Offset: 0x00565DBC
		public bool ShouldScoreHole
		{
			get
			{
				return this.golfScoreTime >= this.golfScoreDelay;
			}
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x00567BD0 File Offset: 0x00565DD0
		public bool TryGetCameraTrackingPosition(out Vector2 cameraPosition)
		{
			Projectile lastHitBall = this.GetLastHitBall();
			if (lastHitBall != null && this._waitingForBallToSettle)
			{
				cameraPosition = lastHitBall.Center;
				return true;
			}
			if (this._lastRecordedBallTime + 2.0 >= Main.gameTimeCache.TotalGameTime.TotalSeconds && lastHitBall == null && this._lastRecordedBallLocation != null)
			{
				cameraPosition = this._lastRecordedBallLocation.Value;
				return true;
			}
			cameraPosition = default(Vector2);
			return false;
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x00567C4C File Offset: 0x00565E4C
		public void WorldClear()
		{
			this._lastHitGolfBall = null;
			this._lastRecordedBallLocation = null;
			this._lastRecordedBallTime = 0.0;
			this._lastRecordedSwingCount = 0;
			this._waitingForBallToSettle = false;
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x00567C7E File Offset: 0x00565E7E
		public void CancelBallTracking()
		{
			this._waitingForBallToSettle = false;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x00567C88 File Offset: 0x00565E88
		public void RecordSwing(Projectile golfBall)
		{
			this._lastSwingPosition = golfBall.position;
			this._lastHitGolfBall = golfBall;
			this._lastRecordedSwingCount = (int)golfBall.ai[1];
			this._waitingForBallToSettle = true;
			int golfBallId = this.GetGolfBallId(golfBall);
			if (this._hitRecords[golfBallId] == null || this._lastRecordedSwingCount == 1)
			{
				this._hitRecords[golfBallId] = new GolfBallTrackRecord();
			}
			this._hitRecords[golfBallId].RecordHit(golfBall.position);
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x00567CF9 File Offset: 0x00565EF9
		private int GetGolfBallId(Projectile golfBall)
		{
			return golfBall.whoAmI;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x00567D04 File Offset: 0x00565F04
		public Projectile GetLastHitBall()
		{
			if (this._lastHitGolfBall == null || !this._lastHitGolfBall.active || !ProjectileID.Sets.IsAGolfBall[this._lastHitGolfBall.type] || this._lastHitGolfBall.owner != Main.myPlayer || this._lastRecordedSwingCount != (int)this._lastHitGolfBall.ai[1])
			{
				return null;
			}
			return this._lastHitGolfBall;
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x00567D6C File Offset: 0x00565F6C
		public void Update()
		{
			this.UpdateScoreTime();
			Projectile lastHitBall = this.GetLastHitBall();
			if (lastHitBall == null)
			{
				this._waitingForBallToSettle = false;
				return;
			}
			if (this._waitingForBallToSettle)
			{
				this._waitingForBallToSettle = (int)lastHitBall.localAI[1] == 1;
			}
			bool flag = false;
			int type = Main.LocalPlayer.HeldItem.type;
			if (type == 3611)
			{
				flag = true;
			}
			if (!Item.IsAGolfingItem(Main.LocalPlayer.HeldItem) && !flag)
			{
				this._waitingForBallToSettle = false;
			}
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x00567DE4 File Offset: 0x00565FE4
		public void RecordBallInfo(Projectile golfBall)
		{
			if (this.GetLastHitBall() != golfBall || !this._waitingForBallToSettle)
			{
				return;
			}
			this._lastRecordedBallLocation = new Vector2?(golfBall.Center);
			this._lastRecordedBallTime = Main.gameTimeCache.TotalGameTime.TotalSeconds;
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x00567E2C File Offset: 0x0056602C
		public void LandBall(Projectile golfBall)
		{
			int golfBallId = this.GetGolfBallId(golfBall);
			GolfBallTrackRecord golfBallTrackRecord = this._hitRecords[golfBallId];
			if (golfBallTrackRecord == null)
			{
				return;
			}
			golfBallTrackRecord.RecordHit(golfBall.position);
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x00567E5C File Offset: 0x0056605C
		public int GetGolfBallScore(Projectile golfBall)
		{
			int golfBallId = this.GetGolfBallId(golfBall);
			GolfBallTrackRecord golfBallTrackRecord = this._hitRecords[golfBallId];
			if (golfBallTrackRecord == null)
			{
				return 0;
			}
			return (int)((float)golfBallTrackRecord.GetAccumulatedScore() * this.ScoreAdjustment);
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x00567E90 File Offset: 0x00566090
		public void ResetGolfBall()
		{
			Projectile lastHitBall = this.GetLastHitBall();
			if (lastHitBall == null)
			{
				return;
			}
			if (Vector2.Distance(lastHitBall.position, this._lastSwingPosition) < 1f)
			{
				return;
			}
			lastHitBall.position = this._lastSwingPosition;
			lastHitBall.velocity = Vector2.Zero;
			lastHitBall.ai[1] += 1f;
			lastHitBall.netUpdate2 = true;
			this._lastRecordedSwingCount = (int)lastHitBall.ai[1];
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x00567F03 File Offset: 0x00566103
		public GolfState()
		{
		}

		// Token: 0x040050E0 RID: 20704
		private const int BALL_RETURN_PENALTY = 1;

		// Token: 0x040050E1 RID: 20705
		private int golfScoreTime;

		// Token: 0x040050E2 RID: 20706
		private int golfScoreTimeMax = 3600;

		// Token: 0x040050E3 RID: 20707
		private int golfScoreDelay = 90;

		// Token: 0x040050E4 RID: 20708
		private double _lastRecordedBallTime;

		// Token: 0x040050E5 RID: 20709
		private Vector2? _lastRecordedBallLocation;

		// Token: 0x040050E6 RID: 20710
		private bool _waitingForBallToSettle;

		// Token: 0x040050E7 RID: 20711
		private Vector2 _lastSwingPosition;

		// Token: 0x040050E8 RID: 20712
		private Projectile _lastHitGolfBall;

		// Token: 0x040050E9 RID: 20713
		private int _lastRecordedSwingCount;

		// Token: 0x040050EA RID: 20714
		private GolfBallTrackRecord[] _hitRecords = new GolfBallTrackRecord[1000];
	}
}
