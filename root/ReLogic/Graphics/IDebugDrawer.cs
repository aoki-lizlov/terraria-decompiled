using System;
using Microsoft.Xna.Framework;

namespace ReLogic.Graphics
{
	// Token: 0x02000087 RID: 135
	public interface IDebugDrawer
	{
		// Token: 0x06000311 RID: 785
		void DrawSquare(Vector4 positionAndSize, Color color);

		// Token: 0x06000312 RID: 786
		void DrawSquareFromCenter(Vector2 center, Vector2 size, float rotation, Color color);
	}
}
