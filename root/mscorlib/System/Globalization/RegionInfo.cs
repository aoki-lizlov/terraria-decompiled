using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Globalization
{
	// Token: 0x02000A05 RID: 2565
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class RegionInfo
	{
		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06005F7D RID: 24445 RVA: 0x0014BC64 File Offset: 0x00149E64
		public static RegionInfo CurrentRegion
		{
			get
			{
				RegionInfo regionInfo = RegionInfo.currentRegion;
				if (regionInfo == null)
				{
					CultureInfo currentCulture = CultureInfo.CurrentCulture;
					if (currentCulture != null)
					{
						regionInfo = new RegionInfo(currentCulture);
					}
					if (Interlocked.CompareExchange<RegionInfo>(ref RegionInfo.currentRegion, regionInfo, null) != null)
					{
						regionInfo = RegionInfo.currentRegion;
					}
				}
				return regionInfo;
			}
		}

		// Token: 0x06005F7E RID: 24446 RVA: 0x0014BC9F File Offset: 0x00149E9F
		public RegionInfo(int culture)
		{
			if (!this.GetByTerritory(CultureInfo.GetCultureInfo(culture)))
			{
				throw new ArgumentException(string.Format("Region ID {0} (0x{0:X4}) is not a supported region.", culture), "culture");
			}
		}

		// Token: 0x06005F7F RID: 24447 RVA: 0x0014BCD0 File Offset: 0x00149ED0
		public RegionInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			if (this.construct_internal_region_from_name(name.ToUpperInvariant()))
			{
				return;
			}
			if (!this.GetByTerritory(CultureInfo.GetCultureInfo(name)))
			{
				throw new ArgumentException(string.Format("Region name {0} is not supported.", name), "name");
			}
		}

		// Token: 0x06005F80 RID: 24448 RVA: 0x0014BD20 File Offset: 0x00149F20
		private RegionInfo(CultureInfo ci)
		{
			if (ci.LCID == 127)
			{
				this.regionId = 244;
				this.iso2Name = "IV";
				this.iso3Name = "ivc";
				this.win3Name = "IVC";
				this.nativeName = (this.englishName = "Invariant Country");
				this.currencySymbol = "¤";
				this.isoCurrencySymbol = "XDR";
				this.currencyEnglishName = (this.currencyNativeName = "International Monetary Fund");
				return;
			}
			if (ci.Territory == null)
			{
				throw new NotImplementedException("Neutral region info");
			}
			this.construct_internal_region_from_name(ci.Territory.ToUpperInvariant());
		}

		// Token: 0x06005F81 RID: 24449 RVA: 0x0014BDCD File Offset: 0x00149FCD
		private bool GetByTerritory(CultureInfo ci)
		{
			if (ci == null)
			{
				throw new Exception("INTERNAL ERROR: should not happen.");
			}
			return !ci.IsNeutralCulture && ci.Territory != null && this.construct_internal_region_from_name(ci.Territory.ToUpperInvariant());
		}

		// Token: 0x06005F82 RID: 24450
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_region_from_name(string name);

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06005F83 RID: 24451 RVA: 0x0014BE00 File Offset: 0x0014A000
		[ComVisible(false)]
		public virtual string CurrencyEnglishName
		{
			get
			{
				return this.currencyEnglishName;
			}
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06005F84 RID: 24452 RVA: 0x0014BE08 File Offset: 0x0014A008
		public virtual string CurrencySymbol
		{
			get
			{
				return this.currencySymbol;
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06005F85 RID: 24453 RVA: 0x0014BE10 File Offset: 0x0014A010
		[MonoTODO("DisplayName currently only returns the EnglishName")]
		public virtual string DisplayName
		{
			get
			{
				return this.englishName;
			}
		}

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06005F86 RID: 24454 RVA: 0x0014BE10 File Offset: 0x0014A010
		public virtual string EnglishName
		{
			get
			{
				return this.englishName;
			}
		}

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06005F87 RID: 24455 RVA: 0x0014BE18 File Offset: 0x0014A018
		[ComVisible(false)]
		public virtual int GeoId
		{
			get
			{
				return this.regionId;
			}
		}

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06005F88 RID: 24456 RVA: 0x0014BE20 File Offset: 0x0014A020
		public virtual bool IsMetric
		{
			get
			{
				string text = this.iso2Name;
				return !(text == "US") && !(text == "UK");
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06005F89 RID: 24457 RVA: 0x0014BE51 File Offset: 0x0014A051
		public virtual string ISOCurrencySymbol
		{
			get
			{
				return this.isoCurrencySymbol;
			}
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06005F8A RID: 24458 RVA: 0x0014BE59 File Offset: 0x0014A059
		[ComVisible(false)]
		public virtual string NativeName
		{
			get
			{
				return this.nativeName;
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06005F8B RID: 24459 RVA: 0x0014BE61 File Offset: 0x0014A061
		[ComVisible(false)]
		public virtual string CurrencyNativeName
		{
			get
			{
				return this.currencyNativeName;
			}
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06005F8C RID: 24460 RVA: 0x0014BE69 File Offset: 0x0014A069
		public virtual string Name
		{
			get
			{
				return this.iso2Name;
			}
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06005F8D RID: 24461 RVA: 0x0014BE71 File Offset: 0x0014A071
		public virtual string ThreeLetterISORegionName
		{
			get
			{
				return this.iso3Name;
			}
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06005F8E RID: 24462 RVA: 0x0014BE79 File Offset: 0x0014A079
		public virtual string ThreeLetterWindowsRegionName
		{
			get
			{
				return this.win3Name;
			}
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06005F8F RID: 24463 RVA: 0x0014BE69 File Offset: 0x0014A069
		public virtual string TwoLetterISORegionName
		{
			get
			{
				return this.iso2Name;
			}
		}

		// Token: 0x06005F90 RID: 24464 RVA: 0x0014BE84 File Offset: 0x0014A084
		public override bool Equals(object value)
		{
			RegionInfo regionInfo = value as RegionInfo;
			return regionInfo != null && this.Name == regionInfo.Name;
		}

		// Token: 0x06005F91 RID: 24465 RVA: 0x0014BEAE File Offset: 0x0014A0AE
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06005F92 RID: 24466 RVA: 0x0014BEBB File Offset: 0x0014A0BB
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06005F93 RID: 24467 RVA: 0x0014BEC3 File Offset: 0x0014A0C3
		internal static void ClearCachedData()
		{
			RegionInfo.currentRegion = null;
		}

		// Token: 0x0400398E RID: 14734
		private static RegionInfo currentRegion;

		// Token: 0x0400398F RID: 14735
		private int regionId;

		// Token: 0x04003990 RID: 14736
		private string iso2Name;

		// Token: 0x04003991 RID: 14737
		private string iso3Name;

		// Token: 0x04003992 RID: 14738
		private string win3Name;

		// Token: 0x04003993 RID: 14739
		private string englishName;

		// Token: 0x04003994 RID: 14740
		private string nativeName;

		// Token: 0x04003995 RID: 14741
		private string currencySymbol;

		// Token: 0x04003996 RID: 14742
		private string isoCurrencySymbol;

		// Token: 0x04003997 RID: 14743
		private string currencyEnglishName;

		// Token: 0x04003998 RID: 14744
		private string currencyNativeName;
	}
}
