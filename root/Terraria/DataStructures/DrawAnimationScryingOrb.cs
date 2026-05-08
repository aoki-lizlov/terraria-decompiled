using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x02000593 RID: 1427
	public class DrawAnimationScryingOrb : DrawAnimation
	{
		// Token: 0x06003858 RID: 14424 RVA: 0x00631EF8 File Offset: 0x006300F8
		public override void Update()
		{
			int num = this.FrameCounter + 1;
			this.FrameCounter = num;
			if (num < this.TicksPerFrame)
			{
				return;
			}
			this.FrameCounter = 0;
			num = this._nextStateCounter - 1;
			this._nextStateCounter = num;
			if (num <= 0)
			{
				if (this._state == DrawAnimationScryingOrb.State.Moving)
				{
					this._state = ((Main.rand.Next(4) == 0) ? DrawAnimationScryingOrb.State.FrozenCenter : DrawAnimationScryingOrb.State.Frozen);
					this._nextStateCounter = Main.rand.Next(7, 10);
					return;
				}
				this._state = DrawAnimationScryingOrb.State.Moving;
				this._nextStateCounter = Main.rand.Next(3, 9);
				if (Main.rand.Next(4) == 0)
				{
					this._dir *= -1;
					return;
				}
			}
			else if (this._state == DrawAnimationScryingOrb.State.Moving)
			{
				this.Frame += this._dir;
				if (this.Frame >= this.FrameCount)
				{
					this.Frame = 1;
					return;
				}
				if (this.Frame <= 0)
				{
					this.Frame = this.FrameCount - 1;
				}
			}
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x00631FF0 File Offset: 0x006301F0
		public override Rectangle GetFrame(Texture2D texture, int frameCounterOverride = -1)
		{
			int num = ((this._state == DrawAnimationScryingOrb.State.FrozenCenter) ? 0 : this.Frame);
			if (frameCounterOverride >= 0)
			{
				num = frameCounterOverride;
			}
			return texture.Frame(1, this.FrameCount, 0, num, 0, -2);
		}

		// Token: 0x0600385A RID: 14426 RVA: 0x00632028 File Offset: 0x00630228
		public DrawAnimationScryingOrb()
		{
		}

		// Token: 0x04005C7D RID: 23677
		private DrawAnimationScryingOrb.State _state;

		// Token: 0x04005C7E RID: 23678
		private int _nextStateCounter;

		// Token: 0x04005C7F RID: 23679
		private int _dir = 1;

		// Token: 0x020009C1 RID: 2497
		private enum State
		{
			// Token: 0x040076DA RID: 30426
			Moving,
			// Token: 0x040076DB RID: 30427
			Frozen,
			// Token: 0x040076DC RID: 30428
			FrozenCenter
		}
	}
}
