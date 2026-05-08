using System;
using System.Collections.Generic;

namespace ReLogic.Peripherals.RGB.Logitech
{
	// Token: 0x02000040 RID: 64
	public class LogitechDeviceGroup : RgbDeviceGroup
	{
		// Token: 0x06000199 RID: 409 RVA: 0x000072F2 File Offset: 0x000054F2
		public LogitechDeviceGroup(VendorColorProfile colorProfiles)
		{
			this._colorProfiles = colorProfiles;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000730C File Offset: 0x0000550C
		protected override void Initialize()
		{
			if (this._isInitialized)
			{
				return;
			}
			try
			{
				if (!NativeMethods.LogiLedInit())
				{
					throw new DeviceInitializationException("LogitechGSDK failed to initialize.");
				}
				this._isInitialized = true;
				if (!NativeMethods.LogiLedSetTargetDevice(6))
				{
					throw new DeviceInitializationException("LogitechGSDK failed to target RGB devices.");
				}
				this._devices.Add(new LogitechKeyboard(this._colorProfiles.Keyboard));
				this._devices.Add(new LogitechSingleLightDevice(this._colorProfiles.Generic));
				Console.WriteLine("Logitech RGB initialized.");
			}
			catch (DeviceInitializationException)
			{
				Console.WriteLine("Logitech RGB not supported. (Can be disabled via Config.json)");
				this.Uninitialize();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Logitech RGB not supported: " + ex);
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000073D4 File Offset: 0x000055D4
		protected override void Uninitialize()
		{
			if (!this._isInitialized)
			{
				return;
			}
			try
			{
				NativeMethods.LogiLedShutdown();
				Console.WriteLine("Logitech RGB uninitialized.");
			}
			catch (ObjectDisposedException)
			{
			}
			catch (Exception ex)
			{
				Console.WriteLine("Logitech RGB failed to uninitialize: " + ex);
			}
			this._isInitialized = false;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007438 File Offset: 0x00005638
		public override IEnumerator<RgbDevice> GetEnumerator()
		{
			return this._devices.GetEnumerator();
		}

		// Token: 0x0400013D RID: 317
		private readonly List<RgbDevice> _devices = new List<RgbDevice>();

		// Token: 0x0400013E RID: 318
		private bool _isInitialized;

		// Token: 0x0400013F RID: 319
		private readonly VendorColorProfile _colorProfiles;
	}
}
