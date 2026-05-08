using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x0200053C RID: 1340
	public class MultiPointHitbox
	{
		// Token: 0x0600375F RID: 14175 RVA: 0x0062ED8C File Offset: 0x0062CF8C
		public MultiPointHitbox(Point pointSize, Vector2[] points)
		{
			this.PointSize = pointSize;
			this.Points = points;
			Rectangle rectangle = Utils.CenteredRectangle(points[0], Vector2.Zero);
			foreach (Vector2 vector in points)
			{
				rectangle = rectangle.Including(vector.ToPoint());
			}
			this.BoundingRect = rectangle;
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x0062EDEC File Offset: 0x0062CFEC
		public bool Intersects(Rectangle targetRect)
		{
			targetRect.Inflate(this.PointSize.X / 2, this.PointSize.Y / 2);
			if (!this.BoundingRect.Intersects(targetRect))
			{
				return false;
			}
			foreach (Vector2 vector in this.Points)
			{
				if (targetRect.Contains(vector.ToPoint()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04005B93 RID: 23443
		public readonly Point PointSize;

		// Token: 0x04005B94 RID: 23444
		public readonly Vector2[] Points;

		// Token: 0x04005B95 RID: 23445
		public readonly Rectangle BoundingRect;
	}
}
