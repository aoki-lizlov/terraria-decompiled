using System;
using Microsoft.Xna.Framework;

namespace ReLogic.Peripherals.RGB.Razer
{
	// Token: 0x02000037 RID: 55
	internal class RazerLink : RgbDevice
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00006071 File Offset: 0x00004271
		public RazerLink(DeviceColorProfile colorProfile)
			: base(RgbDeviceVendor.Razer, RgbDeviceType.Generic, Fragment.FromGrid(new Rectangle(0, -1, 5, 1)), colorProfile)
		{
			base.PreferredLevelOfDetail = EffectDetailLevel.Low;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000060A8 File Offset: 0x000042A8
		public override void Present()
		{
			for (int i = 0; i < this._effect.Color.Length; i++)
			{
				this._effect.Color[i] = RazerHelper.Vector4ToDeviceColor(base.GetProcessedLedColor(i));
			}
			this._handle.SetAsChromaLinkEffect(ref this._effect);
			this._handle.Apply();
		}

		// Token: 0x04000129 RID: 297
		private NativeMethods.CustomChromaLinkEffect _effect = NativeMethods.CustomChromaLinkEffect.Create();

		// Token: 0x0400012A RID: 298
		private readonly EffectHandle _handle = new EffectHandle();
	}
}
