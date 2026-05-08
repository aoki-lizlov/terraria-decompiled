using System;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020002B6 RID: 694
	internal class DebugKeyboard : RgbDevice
	{
		// Token: 0x060025A5 RID: 9637 RVA: 0x0055880A File Offset: 0x00556A0A
		private DebugKeyboard(Fragment fragment)
			: base(4, 6, fragment, new DeviceColorProfile())
		{
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x0055881C File Offset: 0x00556A1C
		public static DebugKeyboard Create()
		{
			int num = 400;
			int num2 = 100;
			Point[] array = new Point[num * num2];
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num; j++)
				{
					array[i * num + j] = new Point(j / 10, i / 10);
				}
			}
			Vector2[] array2 = new Vector2[num * num2];
			for (int k = 0; k < num2; k++)
			{
				for (int l = 0; l < num; l++)
				{
					array2[k * num + l] = new Vector2((float)l / (float)num2, (float)k / (float)num2);
				}
			}
			return new DebugKeyboard(Fragment.FromCustom(array, array2));
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Present()
		{
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x005588CC File Offset: 0x00556ACC
		public override void DebugDraw(IDebugDrawer drawer, Vector2 position, float scale)
		{
			for (int i = 0; i < base.LedCount; i++)
			{
				Vector2 ledCanvasPosition = base.GetLedCanvasPosition(i);
				drawer.DrawSquare(new Vector4(ledCanvasPosition * scale + position, scale / 100f, scale / 100f), new Color(base.GetUnprocessedLedColor(i)));
			}
		}
	}
}
