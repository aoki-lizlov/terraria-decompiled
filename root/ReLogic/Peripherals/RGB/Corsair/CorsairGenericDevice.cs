using System;
using Microsoft.Xna.Framework;

namespace ReLogic.Peripherals.RGB.Corsair
{
	// Token: 0x02000046 RID: 70
	internal class CorsairGenericDevice : RgbDevice
	{
		// Token: 0x060001BF RID: 447 RVA: 0x000079E0 File Offset: 0x00005BE0
		protected CorsairGenericDevice(RgbDeviceType deviceType, Fragment fragment, CorsairLedPosition[] ledPositions, DeviceColorProfile colorProfile)
			: base(RgbDeviceVendor.Corsair, deviceType, fragment, colorProfile)
		{
			this._ledColors = new CorsairLedColor[base.LedCount];
			for (int i = 0; i < ledPositions.Length; i++)
			{
				this._ledColors[i].LedId = ledPositions[i].LedId;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007A34 File Offset: 0x00005C34
		public override void Present()
		{
			for (int i = 0; i < base.LedCount; i++)
			{
				Vector4 processedLedColor = base.GetProcessedLedColor(i);
				this._ledColors[i].R = (int)(processedLedColor.X * 255f);
				this._ledColors[i].G = (int)(processedLedColor.Y * 255f);
				this._ledColors[i].B = (int)(processedLedColor.Z * 255f);
			}
			if (this._ledColors.Length != 0)
			{
				NativeMethods.CorsairSetLedsColorsAsync(this._ledColors.Length, this._ledColors, IntPtr.Zero, IntPtr.Zero);
			}
		}

		// Token: 0x040001B7 RID: 439
		private readonly CorsairLedColor[] _ledColors;
	}
}
