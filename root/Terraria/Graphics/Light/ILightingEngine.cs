using System;
using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Light
{
	// Token: 0x020001FB RID: 507
	public interface ILightingEngine
	{
		// Token: 0x060020DF RID: 8415
		void Rebuild();

		// Token: 0x060020E0 RID: 8416
		void AddLight(int x, int y, Vector3 color);

		// Token: 0x060020E1 RID: 8417
		void ProcessArea(Rectangle area);

		// Token: 0x060020E2 RID: 8418
		Vector3 GetColor(int x, int y);

		// Token: 0x060020E3 RID: 8419
		void Clear();
	}
}
