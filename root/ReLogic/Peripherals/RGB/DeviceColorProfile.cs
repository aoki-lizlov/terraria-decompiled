using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace ReLogic.Peripherals.RGB
{
	// Token: 0x02000017 RID: 23
	public class DeviceColorProfile
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000432A File Offset: 0x0000252A
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00004337 File Offset: 0x00002537
		[JsonProperty("R")]
		private float RedMultiplier
		{
			get
			{
				return this._multiplier.X;
			}
			set
			{
				this._multiplier.X = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004345 File Offset: 0x00002545
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00004352 File Offset: 0x00002552
		[JsonProperty("G")]
		private float GreenMultiplier
		{
			get
			{
				return this._multiplier.Y;
			}
			set
			{
				this._multiplier.Y = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004360 File Offset: 0x00002560
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000436D File Offset: 0x0000256D
		[JsonProperty("B")]
		private float BlueMultiplier
		{
			get
			{
				return this._multiplier.Z;
			}
			set
			{
				this._multiplier.Z = value;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000437B File Offset: 0x0000257B
		public DeviceColorProfile()
		{
			this._multiplier = Vector3.One;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000438E File Offset: 0x0000258E
		public DeviceColorProfile(Vector3 multiplier)
		{
			this._multiplier = multiplier;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000043A0 File Offset: 0x000025A0
		public void Apply(ref Vector4 color)
		{
			color.X *= this._multiplier.X;
			color.Y *= this._multiplier.Y;
			color.Z *= this._multiplier.Z;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000043EC File Offset: 0x000025EC
		public void Apply(ref Vector3 color)
		{
			color.X *= this._multiplier.X;
			color.Y *= this._multiplier.Y;
			color.Z *= this._multiplier.Z;
		}

		// Token: 0x04000034 RID: 52
		private Vector3 _multiplier;
	}
}
