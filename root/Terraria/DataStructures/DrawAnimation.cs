using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x02000591 RID: 1425
	public class DrawAnimation
	{
		// Token: 0x06003852 RID: 14418 RVA: 0x00009E46 File Offset: 0x00008046
		public virtual void Update()
		{
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x00631D7A File Offset: 0x0062FF7A
		public virtual Rectangle GetFrame(Texture2D texture, int frameCounterOverride = -1)
		{
			return texture.Frame(1, 1, 0, 0, 0, 0);
		}

		// Token: 0x06003854 RID: 14420 RVA: 0x0000357B File Offset: 0x0000177B
		public DrawAnimation()
		{
		}

		// Token: 0x04005C77 RID: 23671
		public int Frame;

		// Token: 0x04005C78 RID: 23672
		public int FrameCount;

		// Token: 0x04005C79 RID: 23673
		public int TicksPerFrame;

		// Token: 0x04005C7A RID: 23674
		public int FrameCounter;
	}
}
