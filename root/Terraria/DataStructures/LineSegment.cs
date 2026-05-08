using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x02000596 RID: 1430
	public struct LineSegment
	{
		// Token: 0x06003866 RID: 14438 RVA: 0x0063247E File Offset: 0x0063067E
		public LineSegment(Vector2 start, Vector2 end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x04005C8F RID: 23695
		public Vector2 Start;

		// Token: 0x04005C90 RID: 23696
		public Vector2 End;
	}
}
