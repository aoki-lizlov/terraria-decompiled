using System;
using Microsoft.Xna.Framework;

namespace ReLogic.Peripherals.RGB.Logitech
{
	// Token: 0x0200003F RID: 63
	internal class LogitechSingleLightDevice : RgbDevice
	{
		// Token: 0x06000197 RID: 407 RVA: 0x00007285 File Offset: 0x00005485
		public LogitechSingleLightDevice(DeviceColorProfile colorProfile)
			: base(RgbDeviceVendor.Logitech, RgbDeviceType.Generic, Fragment.FromGrid(new Rectangle(30, 0, 1, 1)), colorProfile)
		{
			base.PreferredLevelOfDetail = EffectDetailLevel.Low;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000072A8 File Offset: 0x000054A8
		public override void Present()
		{
			if (NativeMethods.LogiLedSetTargetDevice(2))
			{
				Vector4 processedLedColor = base.GetProcessedLedColor(0);
				NativeMethods.LogiLedSetLighting((int)(processedLedColor.X * 100f), (int)(processedLedColor.Y * 100f), (int)(processedLedColor.Z * 100f));
			}
		}
	}
}
