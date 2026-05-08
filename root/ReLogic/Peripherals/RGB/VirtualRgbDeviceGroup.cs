using System;
using System.Collections.Generic;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x02000031 RID: 49
	public class VirtualRgbDeviceGroup : RgbDeviceGroup
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00005E9E File Offset: 0x0000409E
		public VirtualRgbDeviceGroup(params RgbDevice[] devices)
		{
			this._devices = new List<RgbDevice>(devices);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005EB2 File Offset: 0x000040B2
		protected override void Initialize()
		{
			this._isInitialized = true;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005EBB File Offset: 0x000040BB
		protected override void Uninitialize()
		{
			this._isInitialized = false;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005EC4 File Offset: 0x000040C4
		public override IEnumerator<RgbDevice> GetEnumerator()
		{
			return this._isInitialized ? this._devices.GetEnumerator() : VirtualRgbDeviceGroup.EmptyList.GetEnumerator();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005EEA File Offset: 0x000040EA
		// Note: this type is marked as 'beforefieldinit'.
		static VirtualRgbDeviceGroup()
		{
		}

		// Token: 0x04000095 RID: 149
		private static readonly List<RgbDevice> EmptyList = new List<RgbDevice>();

		// Token: 0x04000096 RID: 150
		private readonly List<RgbDevice> _devices;

		// Token: 0x04000097 RID: 151
		private bool _isInitialized;
	}
}
