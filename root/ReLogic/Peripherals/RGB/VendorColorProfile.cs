using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x0200001B RID: 27
	public class VendorColorProfile
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004517 File Offset: 0x00002717
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00004521 File Offset: 0x00002721
		[JsonProperty]
		public DeviceColorProfile Keyboard
		{
			get
			{
				return this._profiles[0];
			}
			private set
			{
				this._profiles[0] = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000452C File Offset: 0x0000272C
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00004536 File Offset: 0x00002736
		[JsonProperty]
		public DeviceColorProfile Mouse
		{
			get
			{
				return this._profiles[1];
			}
			private set
			{
				this._profiles[1] = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004541 File Offset: 0x00002741
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000454B File Offset: 0x0000274B
		[JsonProperty]
		public DeviceColorProfile Keypad
		{
			get
			{
				return this._profiles[2];
			}
			private set
			{
				this._profiles[2] = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004556 File Offset: 0x00002756
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00004560 File Offset: 0x00002760
		[JsonProperty]
		public DeviceColorProfile Mousepad
		{
			get
			{
				return this._profiles[3];
			}
			private set
			{
				this._profiles[3] = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000456B File Offset: 0x0000276B
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004575 File Offset: 0x00002775
		[JsonProperty]
		public DeviceColorProfile Headset
		{
			get
			{
				return this._profiles[4];
			}
			private set
			{
				this._profiles[4] = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004580 File Offset: 0x00002780
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x0000458A File Offset: 0x0000278A
		[JsonProperty]
		public DeviceColorProfile Generic
		{
			get
			{
				return this._profiles[5];
			}
			private set
			{
				this._profiles[5] = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004595 File Offset: 0x00002795
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x0000459F File Offset: 0x0000279F
		[JsonProperty]
		public DeviceColorProfile Virtual
		{
			get
			{
				return this._profiles[6];
			}
			private set
			{
				this._profiles[6] = value;
			}
		}

		// Token: 0x17000021 RID: 33
		public DeviceColorProfile this[RgbDeviceType type]
		{
			get
			{
				return this._profiles[(int)type];
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000045B4 File Offset: 0x000027B4
		public VendorColorProfile()
		{
			for (int i = 0; i < this._profiles.Length; i++)
			{
				this._profiles[i] = new DeviceColorProfile();
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004610 File Offset: 0x00002810
		public VendorColorProfile(Vector3 multiplier)
		{
			for (int i = 0; i < this._profiles.Length; i++)
			{
				this._profiles[i] = new DeviceColorProfile(multiplier);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000466A File Offset: 0x0000286A
		public void SetColorMultiplier(RgbDeviceType type, DeviceColorProfile profile)
		{
			this._profiles[(int)type] = profile;
		}

		// Token: 0x0400003B RID: 59
		private readonly DeviceColorProfile[] _profiles = new DeviceColorProfile[Enumerable.Max(Enumerable.Cast<int>(Enum.GetValues(typeof(RgbDeviceType)))) + 1];
	}
}
