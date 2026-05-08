using System;
using Microsoft.Xna.Framework;

namespace ReLogic.Peripherals.RGB.Corsair
{
	// Token: 0x02000044 RID: 68
	internal class CorsairHeadset : CorsairGenericDevice
	{
		// Token: 0x060001BB RID: 443 RVA: 0x000076CE File Offset: 0x000058CE
		private CorsairHeadset(Fragment fragment, CorsairLedPosition[] ledPositions, DeviceColorProfile colorProfile)
			: base(RgbDeviceType.Headset, fragment, ledPositions, colorProfile)
		{
			base.PreferredLevelOfDetail = EffectDetailLevel.Low;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000076E4 File Offset: 0x000058E4
		public static CorsairHeadset Create(int deviceIndex, DeviceColorProfile colorProfile)
		{
			CorsairLedPosition[] array = new CorsairLedPosition[]
			{
				new CorsairLedPosition
				{
					Width = 1.0,
					Height = 1.0,
					LedId = CorsairLedId.CLH_LeftLogo
				},
				new CorsairLedPosition
				{
					Width = 1.0,
					Height = 1.0,
					LedId = CorsairLedId.CLH_RightLogo
				}
			};
			return new CorsairHeadset(Fragment.FromGrid(new Rectangle(-2, 0, 2, 1)), array, colorProfile);
		}
	}
}
