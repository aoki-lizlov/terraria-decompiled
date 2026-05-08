using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Terraria.Utilities;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000C0 RID: 192
	public class WorldGenRange
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x004DFC24 File Offset: 0x004DDE24
		public int ScaledMinimum
		{
			get
			{
				return this.ScaleValue(this.Minimum);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x004DFC32 File Offset: 0x004DDE32
		public int ScaledMaximum
		{
			get
			{
				return this.ScaleValue(this.Maximum);
			}
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x004DFC40 File Offset: 0x004DDE40
		public WorldGenRange(int minimum, int maximum)
		{
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x004DFC56 File Offset: 0x004DDE56
		public int GetRandom(UnifiedRandom random)
		{
			return random.Next(this.ScaledMinimum, this.ScaledMaximum + 1);
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x004DFC6C File Offset: 0x004DDE6C
		private int ScaleValue(int value)
		{
			double num = 1.0;
			switch (this.ScaleWith)
			{
			case WorldGenRange.ScalingMode.None:
				num = 1.0;
				break;
			case WorldGenRange.ScalingMode.WorldArea:
				num = (double)(Main.maxTilesX * Main.maxTilesY) / 5040000.0;
				break;
			case WorldGenRange.ScalingMode.WorldWidth:
				num = (double)Main.maxTilesX / 4200.0;
				break;
			}
			return (int)(num * (double)value);
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x004DFCD9 File Offset: 0x004DDED9
		// Note: this type is marked as 'beforefieldinit'.
		static WorldGenRange()
		{
		}

		// Token: 0x04001299 RID: 4761
		public static readonly WorldGenRange Empty = new WorldGenRange(0, 0);

		// Token: 0x0400129A RID: 4762
		[JsonProperty("Min")]
		public readonly int Minimum;

		// Token: 0x0400129B RID: 4763
		[JsonProperty("Max")]
		public readonly int Maximum;

		// Token: 0x0400129C RID: 4764
		[JsonProperty]
		[JsonConverter(typeof(StringEnumConverter))]
		public readonly WorldGenRange.ScalingMode ScaleWith;

		// Token: 0x020006EC RID: 1772
		public enum ScalingMode
		{
			// Token: 0x040067F9 RID: 26617
			None,
			// Token: 0x040067FA RID: 26618
			WorldArea,
			// Token: 0x040067FB RID: 26619
			WorldWidth
		}
	}
}
