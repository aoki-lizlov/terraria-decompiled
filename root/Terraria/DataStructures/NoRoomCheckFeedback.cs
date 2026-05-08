using System;
using System.Runtime.CompilerServices;

namespace Terraria.DataStructures
{
	// Token: 0x02000532 RID: 1330
	public class NoRoomCheckFeedback : IRoomCheckFeedback, IRoomCheckFeedback_Spread, IRoomCheckFeedback_Scoring
	{
		// Token: 0x06003709 RID: 14089 RVA: 0x0062C7A8 File Offset: 0x0062A9A8
		public NoRoomCheckFeedback(bool displayText)
		{
			this.DisplayText = displayText;
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x00009E46 File Offset: 0x00008046
		public void BeginSpread(int x, int y)
		{
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x00009E46 File Offset: 0x00008046
		public void StartedInASolidTile(int x, int y)
		{
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x00009E46 File Offset: 0x00008046
		public void TooCloseToWorldEdge(int x, int y, int iteration)
		{
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x00009E46 File Offset: 0x00008046
		public void AnyBlockScannedHere(int x, int y, int iteration)
		{
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x00009E46 File Offset: 0x00008046
		public void RoomTooBig(int x, int y, int iteration)
		{
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x00009E46 File Offset: 0x00008046
		public void BlockingWall(int x, int y, int iteration)
		{
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x00009E46 File Offset: 0x00008046
		public void BlockingOpenGate(int x, int y, int iteration)
		{
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x00009E46 File Offset: 0x00008046
		public void Stinkbug(int x, int y, int iteration)
		{
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x00009E46 File Offset: 0x00008046
		public void EchoStinkbug(int x, int y, int iteration)
		{
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x00009E46 File Offset: 0x00008046
		public void MissingAWall(int x, int y, int iteration)
		{
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x00009E46 File Offset: 0x00008046
		public void UnsafeWall(int x, int y, int iteration)
		{
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x00009E46 File Offset: 0x00008046
		public void EndSpread()
		{
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x00009E46 File Offset: 0x00008046
		public void BeginScoring()
		{
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x00009E46 File Offset: 0x00008046
		public void ReportScore(int x, int y, int score)
		{
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x00009E46 File Offset: 0x00008046
		public void SetAsHighScore(int x, int y, int score)
		{
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x00009E46 File Offset: 0x00008046
		public void EndScoring()
		{
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600371A RID: 14106 RVA: 0x000379E9 File Offset: 0x00035BE9
		public bool StopOnFail
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600371B RID: 14107 RVA: 0x0062C7B7 File Offset: 0x0062A9B7
		// (set) Token: 0x0600371C RID: 14108 RVA: 0x0062C7BF File Offset: 0x0062A9BF
		public bool DisplayText
		{
			[CompilerGenerated]
			get
			{
				return this.<DisplayText>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DisplayText>k__BackingField = value;
			}
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x0062C7C8 File Offset: 0x0062A9C8
		// Note: this type is marked as 'beforefieldinit'.
		static NoRoomCheckFeedback()
		{
		}

		// Token: 0x04005B60 RID: 23392
		public static NoRoomCheckFeedback WithText = new NoRoomCheckFeedback(true);

		// Token: 0x04005B61 RID: 23393
		public static NoRoomCheckFeedback WithoutText = new NoRoomCheckFeedback(false);

		// Token: 0x04005B62 RID: 23394
		[CompilerGenerated]
		private bool <DisplayText>k__BackingField;
	}
}
