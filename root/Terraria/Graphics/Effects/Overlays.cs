using System;

namespace Terraria.Graphics.Effects
{
	// Token: 0x020001F4 RID: 500
	public static class Overlays
	{
		// Token: 0x060020C5 RID: 8389 RVA: 0x00523885 File Offset: 0x00521A85
		// Note: this type is marked as 'beforefieldinit'.
		static Overlays()
		{
		}

		// Token: 0x04004B30 RID: 19248
		public static OverlayManager Scene = new OverlayManager();

		// Token: 0x04004B31 RID: 19249
		public static OverlayManager FilterFallback = new OverlayManager();
	}
}
