using System;
using System.Collections.Generic;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x02000023 RID: 35
	public abstract class RgbKeyboard : RgbDevice
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00004A24 File Offset: 0x00002C24
		protected RgbKeyboard(RgbDeviceVendor vendor, Fragment fragment, DeviceColorProfile colorProfile)
			: base(vendor, RgbDeviceType.Keyboard, fragment, colorProfile)
		{
		}

		// Token: 0x0600010D RID: 269
		public abstract void Render(IEnumerable<RgbKey> keys);
	}
}
