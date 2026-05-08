using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x02000434 RID: 1076
	public interface IHorizonRenderer
	{
		// Token: 0x060030A1 RID: 12449
		void DrawHorizon();

		// Token: 0x060030A2 RID: 12450
		void ModifyHorizonLight(ref Color color);

		// Token: 0x060030A3 RID: 12451
		void DrawSun(Vector2 sunPosition);

		// Token: 0x060030A4 RID: 12452
		void CloudsStart();

		// Token: 0x060030A5 RID: 12453
		void DrawCloud(float globalCloudAlpha, Cloud theCloud, int cloudPass, float cY);

		// Token: 0x060030A6 RID: 12454
		void CloudsEnd();

		// Token: 0x060030A7 RID: 12455
		void DrawSurfaceLayer(int layerIndex);

		// Token: 0x060030A8 RID: 12456
		void DrawLensFlare();
	}
}
