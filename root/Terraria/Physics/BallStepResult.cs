using System;

namespace Terraria.Physics
{
	// Token: 0x0200007D RID: 125
	public struct BallStepResult
	{
		// Token: 0x0600156A RID: 5482 RVA: 0x004C4976 File Offset: 0x004C2B76
		private BallStepResult(BallState state)
		{
			this.State = state;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x004C497F File Offset: 0x004C2B7F
		public static BallStepResult OutOfBounds()
		{
			return new BallStepResult(BallState.OutOfBounds);
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x004C4987 File Offset: 0x004C2B87
		public static BallStepResult Moving()
		{
			return new BallStepResult(BallState.Moving);
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x004C498F File Offset: 0x004C2B8F
		public static BallStepResult Resting()
		{
			return new BallStepResult(BallState.Resting);
		}

		// Token: 0x04001103 RID: 4355
		public readonly BallState State;
	}
}
