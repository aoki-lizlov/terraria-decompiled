using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x02000595 RID: 1429
	public struct DrillDebugDraw
	{
		// Token: 0x06003865 RID: 14437 RVA: 0x0063246E File Offset: 0x0063066E
		public DrillDebugDraw(Vector2 p, Color c)
		{
			this.point = p;
			this.color = c;
		}

		// Token: 0x04005C8D RID: 23693
		public Vector2 point;

		// Token: 0x04005C8E RID: 23694
		public Color color;
	}
}
