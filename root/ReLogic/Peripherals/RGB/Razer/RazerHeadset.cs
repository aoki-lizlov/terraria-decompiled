using System;
using Microsoft.Xna.Framework;

namespace ReLogic.Peripherals.RGB.Razer
{
	// Token: 0x02000039 RID: 57
	internal class RazerHeadset : RgbDevice
	{
		// Token: 0x06000186 RID: 390 RVA: 0x000062E2 File Offset: 0x000044E2
		public RazerHeadset(DeviceColorProfile colorProfile)
			: base(RgbDeviceVendor.Razer, RgbDeviceType.Headset, Fragment.FromGrid(new Rectangle(-5, 0, 5, 1)), colorProfile)
		{
			base.PreferredLevelOfDetail = EffectDetailLevel.Low;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000631C File Offset: 0x0000451C
		public override void Present()
		{
			for (int i = 0; i < this._effect.Color.Length; i++)
			{
				this._effect.Color[i] = RazerHelper.Vector4ToDeviceColor(base.GetProcessedLedColor(i));
			}
			this._handle.SetAsHeadsetEffect(ref this._effect);
			this._handle.Apply();
		}

		// Token: 0x0400012E RID: 302
		private NativeMethods.CustomHeadsetEffect _effect = NativeMethods.CustomHeadsetEffect.Create();

		// Token: 0x0400012F RID: 303
		private readonly EffectHandle _handle = new EffectHandle();
	}
}
