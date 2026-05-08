using System;
using Microsoft.Xna.Framework;

namespace ReLogic.Peripherals.RGB.Razer
{
	// Token: 0x0200003A RID: 58
	internal class RazerKeypad : RgbDevice
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00006376 File Offset: 0x00004576
		public RazerKeypad(DeviceColorProfile colorProfile)
			: base(RgbDeviceVendor.Razer, RgbDeviceType.Keypad, Fragment.FromGrid(new Rectangle(-10, 0, 5, 4)), colorProfile)
		{
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000063A8 File Offset: 0x000045A8
		public override void Present()
		{
			for (int i = 0; i < base.LedCount; i++)
			{
				this._effect.Color[i] = RazerHelper.Vector4ToDeviceColor(base.GetProcessedLedColor(i));
			}
			this._handle.SetAsKeypadEffect(ref this._effect);
			this._handle.Apply();
		}

		// Token: 0x04000130 RID: 304
		private NativeMethods.CustomKeypadEffect _effect = NativeMethods.CustomKeypadEffect.Create();

		// Token: 0x04000131 RID: 305
		private readonly EffectHandle _handle = new EffectHandle();
	}
}
