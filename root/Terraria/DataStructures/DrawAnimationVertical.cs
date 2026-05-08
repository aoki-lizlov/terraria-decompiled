using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x02000592 RID: 1426
	public class DrawAnimationVertical : DrawAnimation
	{
		// Token: 0x06003855 RID: 14421 RVA: 0x00631D88 File Offset: 0x0062FF88
		public DrawAnimationVertical(int ticksperframe, int frameCount, bool pingPong = false)
		{
			this.Frame = 0;
			this.FrameCounter = 0;
			this.FrameCount = frameCount;
			this.TicksPerFrame = ticksperframe;
			this.PingPong = pingPong;
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x00631DB4 File Offset: 0x0062FFB4
		public override void Update()
		{
			if (this.NotActuallyAnimating)
			{
				return;
			}
			int num = this.FrameCounter + 1;
			this.FrameCounter = num;
			if (num >= this.TicksPerFrame)
			{
				this.FrameCounter = 0;
				if (this.PingPong)
				{
					num = this.Frame + 1;
					this.Frame = num;
					if (num >= this.FrameCount * 2 - 2)
					{
						this.Frame = 0;
						return;
					}
				}
				else
				{
					num = this.Frame + 1;
					this.Frame = num;
					if (num >= this.FrameCount)
					{
						this.Frame = 0;
					}
				}
			}
		}

		// Token: 0x06003857 RID: 14423 RVA: 0x00631E38 File Offset: 0x00630038
		public override Rectangle GetFrame(Texture2D texture, int frameCounterOverride = -1)
		{
			if (frameCounterOverride != -1)
			{
				int num = frameCounterOverride / this.TicksPerFrame;
				int num2 = this.FrameCount;
				if (this.PingPong)
				{
					num2 = num2 * 2 - 1;
				}
				int num3 = num % num2;
				if (this.PingPong && num3 >= this.FrameCount)
				{
					num3 = this.FrameCount * 2 - 2 - num3;
				}
				Rectangle rectangle = texture.Frame(1, this.FrameCount, 0, num3, 0, 0);
				rectangle.Height -= 2;
				return rectangle;
			}
			int num4 = this.Frame;
			if (this.PingPong && this.Frame >= this.FrameCount)
			{
				num4 = this.FrameCount * 2 - 2 - this.Frame;
			}
			Rectangle rectangle2 = texture.Frame(1, this.FrameCount, 0, num4, 0, 0);
			rectangle2.Height -= 2;
			return rectangle2;
		}

		// Token: 0x04005C7B RID: 23675
		public bool PingPong;

		// Token: 0x04005C7C RID: 23676
		public bool NotActuallyAnimating;
	}
}
