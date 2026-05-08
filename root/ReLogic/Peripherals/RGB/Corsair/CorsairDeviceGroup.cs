using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ReLogic.Peripherals.RGB.Corsair
{
	// Token: 0x02000043 RID: 67
	public class CorsairDeviceGroup : RgbDeviceGroup
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x0000744A File Offset: 0x0000564A
		public CorsairDeviceGroup(VendorColorProfile colorProfiles)
		{
			this._colorProfiles = colorProfiles;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007464 File Offset: 0x00005664
		protected override void Initialize()
		{
			if (this._state == CorsairDeviceGroup.State.AnyDevicesAdded)
			{
				return;
			}
			try
			{
				if (this._state < CorsairDeviceGroup.State.HandshakeCompleted)
				{
					NativeMethods.CorsairPerformProtocolHandshake();
					CorsairError corsairError = NativeMethods.CorsairGetLastError();
					if (corsairError != CorsairError.CE_Success)
					{
						throw new DeviceInitializationException("Corsair initialization failed with: " + corsairError);
					}
					this._state = CorsairDeviceGroup.State.HandshakeCompleted;
				}
				if (this._state < CorsairDeviceGroup.State.DeviceControlGranted)
				{
					NativeMethods.CorsairRequestControl(CorsairAccessMode.CAM_ExclusiveLightingControl);
					this._state = CorsairDeviceGroup.State.DeviceControlGranted;
				}
				int num = NativeMethods.CorsairGetDeviceCount();
				for (int i = 0; i < num; i++)
				{
					CorsairDeviceInfo corsairDeviceInfo = (CorsairDeviceInfo)Marshal.PtrToStructure(NativeMethods.CorsairGetDeviceInfo(i), typeof(CorsairDeviceInfo));
					this.AddDeviceIfSupported(i, corsairDeviceInfo);
				}
				Console.WriteLine("Corsair RGB intialized.");
				if (this._state != CorsairDeviceGroup.State.AnyDevicesAdded)
				{
					Console.WriteLine("No usable Corsair RGB devices found. Shutting down Corsair SDK.");
					this.Uninitialize();
				}
			}
			catch (DeviceInitializationException)
			{
				Console.WriteLine("Corsair RGB not supported. (Can be disabled via Config.json)");
				this.Uninitialize();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Corsair RGB not supported: " + ex);
				this.Uninitialize();
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000756C File Offset: 0x0000576C
		protected override void Uninitialize()
		{
			if (this._state == CorsairDeviceGroup.State.Unitialized)
			{
				return;
			}
			try
			{
				this._devices.Clear();
				if (this._state >= CorsairDeviceGroup.State.DeviceControlGranted)
				{
					NativeMethods.CorsairReleaseControl(CorsairAccessMode.CAM_ExclusiveLightingControl);
				}
				Console.WriteLine("Corsair RGB unitialized.");
			}
			catch (ObjectDisposedException)
			{
			}
			catch (Exception ex)
			{
				Console.WriteLine("Corsair RGB failed to uninitialize: " + ex);
			}
			if (this._state >= CorsairDeviceGroup.State.HandshakeCompleted)
			{
				this._state = CorsairDeviceGroup.State.HandshakeCompleted;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000075EC File Offset: 0x000057EC
		private void AddDeviceIfSupported(int deviceIndex, CorsairDeviceInfo deviceInfo)
		{
			if ((deviceInfo.CapsMask & CorsairDeviceCaps.CDC_Lighting) == CorsairDeviceCaps.CDC_None)
			{
				return;
			}
			RgbDevice rgbDevice = null;
			try
			{
				switch (deviceInfo.Type)
				{
				case CorsairDeviceType.CDT_Mouse:
					rgbDevice = CorsairMouse.Create(deviceIndex, deviceInfo, this._colorProfiles.Mouse);
					break;
				case CorsairDeviceType.CDT_Keyboard:
					rgbDevice = CorsairKeyboard.Create(deviceIndex, this._colorProfiles.Keyboard);
					break;
				case CorsairDeviceType.CDT_Headset:
					rgbDevice = CorsairHeadset.Create(deviceIndex, this._colorProfiles.Headset);
					break;
				case CorsairDeviceType.CDT_MouseMat:
					rgbDevice = CorsairMousepad.Create(deviceIndex, this._colorProfiles.Mousepad);
					break;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Corsair RGB failed to create device: " + ex);
				rgbDevice = null;
			}
			if (rgbDevice != null && rgbDevice.LedCount > 0)
			{
				this._devices.Add(rgbDevice);
				this._state = CorsairDeviceGroup.State.AnyDevicesAdded;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000076BC File Offset: 0x000058BC
		public override IEnumerator<RgbDevice> GetEnumerator()
		{
			return this._devices.GetEnumerator();
		}

		// Token: 0x040001B4 RID: 436
		private readonly List<RgbDevice> _devices = new List<RgbDevice>();

		// Token: 0x040001B5 RID: 437
		private CorsairDeviceGroup.State _state;

		// Token: 0x040001B6 RID: 438
		private readonly VendorColorProfile _colorProfiles;

		// Token: 0x020000CB RID: 203
		private enum State
		{
			// Token: 0x040005B8 RID: 1464
			Unitialized,
			// Token: 0x040005B9 RID: 1465
			HandshakeCompleted,
			// Token: 0x040005BA RID: 1466
			DeviceControlGranted,
			// Token: 0x040005BB RID: 1467
			AnyDevicesAdded
		}
	}
}
