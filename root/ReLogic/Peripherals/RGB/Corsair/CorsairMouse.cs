using System;
using Microsoft.Xna.Framework;

namespace ReLogic.Peripherals.RGB.Corsair
{
	// Token: 0x02000047 RID: 71
	internal class CorsairMouse : CorsairGenericDevice
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00007ADB File Offset: 0x00005CDB
		private CorsairMouse(Fragment fragment, CorsairLedPosition[] leds, DeviceColorProfile colorProfile)
			: base(RgbDeviceType.Mouse, fragment, leds, colorProfile)
		{
			base.PreferredLevelOfDetail = EffectDetailLevel.Low;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public static CorsairMouse Create(int deviceIndex, CorsairDeviceInfo deviceInfo, DeviceColorProfile colorProfile)
		{
			CorsairLedPosition[] array;
			switch (deviceInfo.PhysicalLayout)
			{
			case CorsairPhysicalLayout.CPL_Zones1:
				array = new CorsairLedPosition[]
				{
					new CorsairLedPosition
					{
						LedId = CorsairLedId.CLM_1
					}
				};
				break;
			case CorsairPhysicalLayout.CPL_Zones2:
				array = new CorsairLedPosition[]
				{
					new CorsairLedPosition
					{
						LedId = CorsairLedId.CLM_1
					},
					new CorsairLedPosition
					{
						LedId = CorsairLedId.CLM_2
					}
				};
				break;
			case CorsairPhysicalLayout.CPL_Zones3:
				array = new CorsairLedPosition[]
				{
					new CorsairLedPosition
					{
						LedId = CorsairLedId.CLM_1
					},
					new CorsairLedPosition
					{
						LedId = CorsairLedId.CLM_2
					},
					new CorsairLedPosition
					{
						LedId = CorsairLedId.CLM_3
					}
				};
				break;
			case CorsairPhysicalLayout.CPL_Zones4:
				array = new CorsairLedPosition[]
				{
					new CorsairLedPosition
					{
						LedId = CorsairLedId.CLM_1
					},
					new CorsairLedPosition
					{
						LedId = CorsairLedId.CLM_2
					},
					new CorsairLedPosition
					{
						LedId = CorsairLedId.CLM_3
					},
					new CorsairLedPosition
					{
						LedId = CorsairLedId.CLM_4
					}
				};
				break;
			default:
				array = new CorsairLedPosition[0];
				break;
			}
			return new CorsairMouse(Fragment.FromGrid(new Rectangle(27, 0, 1, array.Length)), array, colorProfile);
		}
	}
}
