using System;
using Microsoft.Xna.Framework;

namespace ReLogic.Peripherals.RGB.Corsair
{
	// Token: 0x02000048 RID: 72
	internal class CorsairMousepad : CorsairGenericDevice
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00007C85 File Offset: 0x00005E85
		private CorsairMousepad(Fragment fragment, CorsairLedPosition[] leds, DeviceColorProfile colorProfile)
			: base(RgbDeviceType.Mousepad, fragment, leds, colorProfile)
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007C94 File Offset: 0x00005E94
		public static CorsairMousepad Create(int deviceIndex, DeviceColorProfile colorProfile)
		{
			CorsairLedPosition[] ledPositionsForMouseMatOrKeyboard = CorsairHelper.GetLedPositionsForMouseMatOrKeyboard(deviceIndex);
			return new CorsairMousepad(CorsairHelper.CreateFragment(ledPositionsForMouseMatOrKeyboard, new Vector2(1040f, 0f)), ledPositionsForMouseMatOrKeyboard, colorProfile);
		}
	}
}
