using System;
using Microsoft.Xna.Framework;

namespace ReLogic.Peripherals.RGB.Razer
{
	// Token: 0x0200003B RID: 59
	internal class RazerMouse : RgbDevice
	{
		// Token: 0x0600018A RID: 394 RVA: 0x000063FB File Offset: 0x000045FB
		public RazerMouse(DeviceColorProfile colorProfile)
			: base(RgbDeviceVendor.Razer, RgbDeviceType.Mouse, Fragment.FromGrid(new Rectangle(27, 0, 7, 9)), colorProfile)
		{
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000642C File Offset: 0x0000462C
		public override void Present()
		{
			for (int i = 0; i < base.LedCount; i++)
			{
				this._effect.Color[i] = RazerHelper.Vector4ToDeviceColor(base.GetProcessedLedColor(i));
			}
			this._handle.SetAsMouseEffect(ref this._effect);
			this._handle.Apply();
		}

		// Token: 0x04000132 RID: 306
		private NativeMethods.CustomMouseEffect _effect = NativeMethods.CustomMouseEffect.Create();

		// Token: 0x04000133 RID: 307
		private readonly EffectHandle _handle = new EffectHandle();
	}
}
