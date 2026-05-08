using System;
using System.Collections.Generic;

namespace ReLogic.Peripherals.RGB.Razer
{
	// Token: 0x02000038 RID: 56
	public sealed class RazerDeviceGroup : RgbDeviceGroup
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00006102 File Offset: 0x00004302
		public RazerDeviceGroup(VendorColorProfile colorProfiles)
		{
			this._colorProfiles = colorProfiles;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000611C File Offset: 0x0000431C
		protected override void Initialize()
		{
			if (this._isInitialized)
			{
				return;
			}
			try
			{
				RzResult rzResult = NativeMethods.Init();
				if (rzResult != RzResult.Success)
				{
					throw new DeviceInitializationException("Unable to initialize Razer Synapse: " + (int)rzResult);
				}
				this._isInitialized = true;
				this._devices.Add(new RazerMouse(this._colorProfiles.Mouse));
				this._devices.Add(new RazerKeyboard(this._colorProfiles.Keyboard));
				this._devices.Add(new RazerMousepad(this._colorProfiles.Mousepad));
				this._devices.Add(new RazerKeypad(this._colorProfiles.Keypad));
				this._devices.Add(new RazerHeadset(this._colorProfiles.Headset));
				this._devices.Add(new RazerLink(this._colorProfiles.Generic));
				Console.WriteLine("Razer Chroma initialized.");
			}
			catch (DeviceInitializationException)
			{
				Console.WriteLine("Razer Chroma not supported. (Can be disabled via Config.json)");
				this.Uninitialize();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Razer Chroma not supported: " + ex);
				this.Uninitialize();
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006250 File Offset: 0x00004450
		protected override void Uninitialize()
		{
			if (!this._isInitialized)
			{
				return;
			}
			try
			{
				this._devices.Clear();
				RzResult rzResult = NativeMethods.UnInit();
				if (rzResult != RzResult.Success)
				{
					throw new DeviceInitializationException("Unable to uninitialize Razer Synapse: " + (int)rzResult);
				}
				Console.WriteLine("Razer Chroma unitialized.");
			}
			catch (Exception ex)
			{
				if (!(ex is ObjectDisposedException))
				{
					Console.WriteLine("Razer Chroma failed to uninitialize: " + ex);
				}
			}
			this._isInitialized = false;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000062D0 File Offset: 0x000044D0
		public override IEnumerator<RgbDevice> GetEnumerator()
		{
			return this._devices.GetEnumerator();
		}

		// Token: 0x0400012B RID: 299
		private readonly List<RgbDevice> _devices = new List<RgbDevice>();

		// Token: 0x0400012C RID: 300
		private bool _isInitialized;

		// Token: 0x0400012D RID: 301
		private readonly VendorColorProfile _colorProfiles;
	}
}
