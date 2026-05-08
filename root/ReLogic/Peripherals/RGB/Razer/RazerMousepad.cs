using System;
using Microsoft.Xna.Framework;

namespace ReLogic.Peripherals.RGB.Razer
{
	// Token: 0x0200003C RID: 60
	internal class RazerMousepad : RgbDevice
	{
		// Token: 0x0600018C RID: 396 RVA: 0x0000647F File Offset: 0x0000467F
		public RazerMousepad(DeviceColorProfile colorProfile)
			: base(RgbDeviceVendor.Razer, RgbDeviceType.Mousepad, Fragment.FromCustom(RazerMousepad.CreatePositionList()), colorProfile)
		{
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000064AC File Offset: 0x000046AC
		private static Point[] CreatePositionList()
		{
			Point[] array = new Point[15];
			Point point;
			point..ctor(26, 0);
			for (int i = 0; i < 5; i++)
			{
				array[i] = new Point(point.X, point.Y + i);
				array[14 - i] = new Point(point.X + 6, point.Y + i);
			}
			for (int j = 5; j < 10; j++)
			{
				array[j] = new Point(j - 5 + point.X + 1, point.Y + 5);
			}
			return array;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006540 File Offset: 0x00004740
		public override void Present()
		{
			for (int i = 0; i < base.LedCount; i++)
			{
				this._effect.Color[i] = RazerHelper.Vector4ToDeviceColor(base.GetProcessedLedColor(i));
			}
			this._handle.SetAsMousepadEffect(ref this._effect);
			this._handle.Apply();
		}

		// Token: 0x04000134 RID: 308
		private NativeMethods.CustomMousepadEffect _effect = NativeMethods.CustomMousepadEffect.Create();

		// Token: 0x04000135 RID: 309
		private readonly EffectHandle _handle = new EffectHandle();
	}
}
