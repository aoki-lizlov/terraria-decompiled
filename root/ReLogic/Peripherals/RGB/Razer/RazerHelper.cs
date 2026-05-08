using System;
using Microsoft.Xna.Framework;

namespace ReLogic.Peripherals.RGB.Razer
{
	// Token: 0x02000035 RID: 53
	internal static class RazerHelper
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00005FFC File Offset: 0x000041FC
		public static uint Vector4ToDeviceColor(Vector4 color)
		{
			uint num = (uint)((int)(color.X * 255f));
			int num2 = (int)(color.Y * 255f);
			int num3 = (int)(color.Z * 255f);
			num3 <<= 16;
			num2 <<= 8;
			return num | (uint)num2 | (uint)num3;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006040 File Offset: 0x00004240
		public static uint XnaColorToDeviceColor(Color color)
		{
			uint r = (uint)color.R;
			int num = (int)color.G;
			int num2 = (int)color.B;
			num2 <<= 16;
			num <<= 8;
			return r | (uint)num | (uint)num2;
		}
	}
}
