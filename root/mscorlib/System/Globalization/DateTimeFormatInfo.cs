using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x020009B4 RID: 2484
	[Serializable]
	public sealed class DateTimeFormatInfo : IFormatProvider, ICloneable
	{
		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06005ABF RID: 23231 RVA: 0x00133D51 File Offset: 0x00131F51
		private string CultureName
		{
			get
			{
				if (this._name == null)
				{
					this._name = this._cultureData.CultureName;
				}
				return this._name;
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06005AC0 RID: 23232 RVA: 0x00133D72 File Offset: 0x00131F72
		private CultureInfo Culture
		{
			get
			{
				if (this._cultureInfo == null)
				{
					this._cultureInfo = CultureInfo.GetCultureInfo(this.CultureName);
				}
				return this._cultureInfo;
			}
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06005AC1 RID: 23233 RVA: 0x00133D93 File Offset: 0x00131F93
		private string LanguageName
		{
			get
			{
				if (this._langName == null)
				{
					this._langName = this._cultureData.SISO639LANGNAME;
				}
				return this._langName;
			}
		}

		// Token: 0x06005AC2 RID: 23234 RVA: 0x00133DB4 File Offset: 0x00131FB4
		private string[] internalGetAbbreviatedDayOfWeekNames()
		{
			return this.abbreviatedDayNames ?? this.internalGetAbbreviatedDayOfWeekNamesCore();
		}

		// Token: 0x06005AC3 RID: 23235 RVA: 0x00133DC6 File Offset: 0x00131FC6
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetAbbreviatedDayOfWeekNamesCore()
		{
			this.abbreviatedDayNames = this._cultureData.AbbreviatedDayNames(this.Calendar.ID);
			return this.abbreviatedDayNames;
		}

		// Token: 0x06005AC4 RID: 23236 RVA: 0x00133DEA File Offset: 0x00131FEA
		private string[] internalGetSuperShortDayNames()
		{
			return this.m_superShortDayNames ?? this.internalGetSuperShortDayNamesCore();
		}

		// Token: 0x06005AC5 RID: 23237 RVA: 0x00133DFC File Offset: 0x00131FFC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetSuperShortDayNamesCore()
		{
			this.m_superShortDayNames = this._cultureData.SuperShortDayNames(this.Calendar.ID);
			return this.m_superShortDayNames;
		}

		// Token: 0x06005AC6 RID: 23238 RVA: 0x00133E20 File Offset: 0x00132020
		private string[] internalGetDayOfWeekNames()
		{
			return this.dayNames ?? this.internalGetDayOfWeekNamesCore();
		}

		// Token: 0x06005AC7 RID: 23239 RVA: 0x00133E32 File Offset: 0x00132032
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetDayOfWeekNamesCore()
		{
			this.dayNames = this._cultureData.DayNames(this.Calendar.ID);
			return this.dayNames;
		}

		// Token: 0x06005AC8 RID: 23240 RVA: 0x00133E56 File Offset: 0x00132056
		private string[] internalGetAbbreviatedMonthNames()
		{
			return this.abbreviatedMonthNames ?? this.internalGetAbbreviatedMonthNamesCore();
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x00133E68 File Offset: 0x00132068
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetAbbreviatedMonthNamesCore()
		{
			this.abbreviatedMonthNames = this._cultureData.AbbreviatedMonthNames(this.Calendar.ID);
			return this.abbreviatedMonthNames;
		}

		// Token: 0x06005ACA RID: 23242 RVA: 0x00133E8C File Offset: 0x0013208C
		private string[] internalGetMonthNames()
		{
			return this.monthNames ?? this.internalGetMonthNamesCore();
		}

		// Token: 0x06005ACB RID: 23243 RVA: 0x00133E9E File Offset: 0x0013209E
		[MethodImpl(MethodImplOptions.NoInlining)]
		private string[] internalGetMonthNamesCore()
		{
			this.monthNames = this._cultureData.MonthNames(this.Calendar.ID);
			return this.monthNames;
		}

		// Token: 0x06005ACC RID: 23244 RVA: 0x00133EC4 File Offset: 0x001320C4
		public DateTimeFormatInfo()
		{
			this._cultureData = CultureInfo.InvariantCulture._cultureData;
			this.calendar = GregorianCalendar.GetDefaultInstance();
			this.InitializeOverridableProperties(this._cultureData, this.calendar.ID);
		}

		// Token: 0x06005ACD RID: 23245 RVA: 0x00133F1E File Offset: 0x0013211E
		internal DateTimeFormatInfo(CultureData cultureData, Calendar cal)
		{
			this._cultureData = cultureData;
			this.Calendar = cal;
		}

		// Token: 0x06005ACE RID: 23246 RVA: 0x00133F4C File Offset: 0x0013214C
		private void InitializeOverridableProperties(CultureData cultureData, int calendarId)
		{
			if (this.firstDayOfWeek == -1)
			{
				this.firstDayOfWeek = cultureData.IFIRSTDAYOFWEEK;
			}
			if (this.calendarWeekRule == -1)
			{
				this.calendarWeekRule = cultureData.IFIRSTWEEKOFYEAR;
			}
			if (this.amDesignator == null)
			{
				this.amDesignator = cultureData.SAM1159;
			}
			if (this.pmDesignator == null)
			{
				this.pmDesignator = cultureData.SPM2359;
			}
			if (this.timeSeparator == null)
			{
				this.timeSeparator = cultureData.TimeSeparator;
			}
			if (this.dateSeparator == null)
			{
				this.dateSeparator = cultureData.DateSeparator(calendarId);
			}
			this.allLongTimePatterns = this._cultureData.LongTimes;
			this.allShortTimePatterns = this._cultureData.ShortTimes;
			this.allLongDatePatterns = cultureData.LongDates(calendarId);
			this.allShortDatePatterns = cultureData.ShortDates(calendarId);
			this.allYearMonthPatterns = cultureData.YearMonths(calendarId);
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06005ACF RID: 23247 RVA: 0x0013401D File Offset: 0x0013221D
		public static DateTimeFormatInfo InvariantInfo
		{
			get
			{
				if (DateTimeFormatInfo.s_invariantInfo == null)
				{
					DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
					dateTimeFormatInfo.Calendar.SetReadOnlyState(true);
					dateTimeFormatInfo._isReadOnly = true;
					DateTimeFormatInfo.s_invariantInfo = dateTimeFormatInfo;
				}
				return DateTimeFormatInfo.s_invariantInfo;
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06005AD0 RID: 23248 RVA: 0x00134050 File Offset: 0x00132250
		public static DateTimeFormatInfo CurrentInfo
		{
			get
			{
				CultureInfo currentCulture = CultureInfo.CurrentCulture;
				if (!currentCulture._isInherited)
				{
					DateTimeFormatInfo dateTimeInfo = currentCulture.dateTimeInfo;
					if (dateTimeInfo != null)
					{
						return dateTimeInfo;
					}
				}
				return (DateTimeFormatInfo)currentCulture.GetFormat(typeof(DateTimeFormatInfo));
			}
		}

		// Token: 0x06005AD1 RID: 23249 RVA: 0x00134090 File Offset: 0x00132290
		public static DateTimeFormatInfo GetInstance(IFormatProvider provider)
		{
			if (provider == null)
			{
				return DateTimeFormatInfo.CurrentInfo;
			}
			CultureInfo cultureInfo = provider as CultureInfo;
			if (cultureInfo != null && !cultureInfo._isInherited)
			{
				return cultureInfo.DateTimeFormat;
			}
			DateTimeFormatInfo dateTimeFormatInfo = provider as DateTimeFormatInfo;
			if (dateTimeFormatInfo != null)
			{
				return dateTimeFormatInfo;
			}
			DateTimeFormatInfo dateTimeFormatInfo2 = provider.GetFormat(typeof(DateTimeFormatInfo)) as DateTimeFormatInfo;
			if (dateTimeFormatInfo2 == null)
			{
				return DateTimeFormatInfo.CurrentInfo;
			}
			return dateTimeFormatInfo2;
		}

		// Token: 0x06005AD2 RID: 23250 RVA: 0x001340EB File Offset: 0x001322EB
		public object GetFormat(Type formatType)
		{
			if (!(formatType == typeof(DateTimeFormatInfo)))
			{
				return null;
			}
			return this;
		}

		// Token: 0x06005AD3 RID: 23251 RVA: 0x00134102 File Offset: 0x00132302
		public object Clone()
		{
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)base.MemberwiseClone();
			dateTimeFormatInfo.calendar = (Calendar)this.Calendar.Clone();
			dateTimeFormatInfo._isReadOnly = false;
			return dateTimeFormatInfo;
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06005AD4 RID: 23252 RVA: 0x0013412C File Offset: 0x0013232C
		// (set) Token: 0x06005AD5 RID: 23253 RVA: 0x0013414D File Offset: 0x0013234D
		public string AMDesignator
		{
			get
			{
				if (this.amDesignator == null)
				{
					this.amDesignator = this._cultureData.SAM1159;
				}
				return this.amDesignator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.ClearTokenHashTable();
				this.amDesignator = value;
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06005AD6 RID: 23254 RVA: 0x00134182 File Offset: 0x00132382
		// (set) Token: 0x06005AD7 RID: 23255 RVA: 0x0013418C File Offset: 0x0013238C
		public Calendar Calendar
		{
			get
			{
				return this.calendar;
			}
			set
			{
				if (GlobalizationMode.Invariant)
				{
					throw new PlatformNotSupportedException();
				}
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Object cannot be null.");
				}
				if (value == this.calendar)
				{
					return;
				}
				for (int i = 0; i < this.OptionalCalendars.Length; i++)
				{
					if (this.OptionalCalendars[i] == (CalendarId)value.ID)
					{
						if (this.calendar != null)
						{
							this.m_eraNames = null;
							this.m_abbrevEraNames = null;
							this.m_abbrevEnglishEraNames = null;
							this.monthDayPattern = null;
							this.dayNames = null;
							this.abbreviatedDayNames = null;
							this.m_superShortDayNames = null;
							this.monthNames = null;
							this.abbreviatedMonthNames = null;
							this.genitiveMonthNames = null;
							this.m_genitiveAbbreviatedMonthNames = null;
							this.leapYearMonthNames = null;
							this.formatFlags = DateTimeFormatFlags.NotInitialized;
							this.allShortDatePatterns = null;
							this.allLongDatePatterns = null;
							this.allYearMonthPatterns = null;
							this.dateTimeOffsetPattern = null;
							this.longDatePattern = null;
							this.shortDatePattern = null;
							this.yearMonthPattern = null;
							this.fullDateTimePattern = null;
							this.generalShortTimePattern = null;
							this.generalLongTimePattern = null;
							this.dateSeparator = null;
							this.ClearTokenHashTable();
						}
						this.calendar = value;
						this.InitializeOverridableProperties(this._cultureData, this.calendar.ID);
						return;
					}
				}
				throw new ArgumentOutOfRangeException("value", "Not a valid calendar for the given culture.");
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06005AD8 RID: 23256 RVA: 0x001342EA File Offset: 0x001324EA
		private CalendarId[] OptionalCalendars
		{
			get
			{
				if (this.optionalCalendars == null)
				{
					this.optionalCalendars = this._cultureData.GetCalendarIds();
				}
				return this.optionalCalendars;
			}
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x0013430C File Offset: 0x0013250C
		public int GetEra(string eraName)
		{
			if (eraName == null)
			{
				throw new ArgumentNullException("eraName", "String reference not set to an instance of a String.");
			}
			if (eraName.Length == 0)
			{
				return -1;
			}
			for (int i = 0; i < this.EraNames.Length; i++)
			{
				if (this.m_eraNames[i].Length > 0 && this.Culture.CompareInfo.Compare(eraName, this.m_eraNames[i], CompareOptions.IgnoreCase) == 0)
				{
					return i + 1;
				}
			}
			for (int j = 0; j < this.AbbreviatedEraNames.Length; j++)
			{
				if (this.Culture.CompareInfo.Compare(eraName, this.m_abbrevEraNames[j], CompareOptions.IgnoreCase) == 0)
				{
					return j + 1;
				}
			}
			for (int k = 0; k < this.AbbreviatedEnglishEraNames.Length; k++)
			{
				if (CompareInfo.Invariant.Compare(eraName, this.m_abbrevEnglishEraNames[k], CompareOptions.IgnoreCase) == 0)
				{
					return k + 1;
				}
			}
			return -1;
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06005ADA RID: 23258 RVA: 0x001343DA File Offset: 0x001325DA
		internal string[] EraNames
		{
			get
			{
				if (this.m_eraNames == null)
				{
					this.m_eraNames = this._cultureData.EraNames(this.Calendar.ID);
				}
				return this.m_eraNames;
			}
		}

		// Token: 0x06005ADB RID: 23259 RVA: 0x00134406 File Offset: 0x00132606
		public string GetEraName(int era)
		{
			if (era == 0)
			{
				era = this.Calendar.CurrentEraValue;
			}
			if (--era < this.EraNames.Length && era >= 0)
			{
				return this.m_eraNames[era];
			}
			throw new ArgumentOutOfRangeException("era", "Era value was not valid.");
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06005ADC RID: 23260 RVA: 0x00134444 File Offset: 0x00132644
		internal string[] AbbreviatedEraNames
		{
			get
			{
				if (this.m_abbrevEraNames == null)
				{
					this.m_abbrevEraNames = this._cultureData.AbbrevEraNames(this.Calendar.ID);
				}
				return this.m_abbrevEraNames;
			}
		}

		// Token: 0x06005ADD RID: 23261 RVA: 0x00134470 File Offset: 0x00132670
		public string GetAbbreviatedEraName(int era)
		{
			if (this.AbbreviatedEraNames.Length == 0)
			{
				return this.GetEraName(era);
			}
			if (era == 0)
			{
				era = this.Calendar.CurrentEraValue;
			}
			if (--era < this.m_abbrevEraNames.Length && era >= 0)
			{
				return this.m_abbrevEraNames[era];
			}
			throw new ArgumentOutOfRangeException("era", "Era value was not valid.");
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06005ADE RID: 23262 RVA: 0x001344CA File Offset: 0x001326CA
		internal string[] AbbreviatedEnglishEraNames
		{
			get
			{
				if (this.m_abbrevEnglishEraNames == null)
				{
					this.m_abbrevEnglishEraNames = this._cultureData.AbbreviatedEnglishEraNames(this.Calendar.ID);
				}
				return this.m_abbrevEnglishEraNames;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06005ADF RID: 23263 RVA: 0x001344F6 File Offset: 0x001326F6
		// (set) Token: 0x06005AE0 RID: 23264 RVA: 0x00134522 File Offset: 0x00132722
		public string DateSeparator
		{
			get
			{
				if (this.dateSeparator == null)
				{
					this.dateSeparator = this._cultureData.DateSeparator(this.Calendar.ID);
				}
				return this.dateSeparator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.ClearTokenHashTable();
				this.dateSeparator = value;
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06005AE1 RID: 23265 RVA: 0x00134557 File Offset: 0x00132757
		// (set) Token: 0x06005AE2 RID: 23266 RVA: 0x0013457C File Offset: 0x0013277C
		public DayOfWeek FirstDayOfWeek
		{
			get
			{
				if (this.firstDayOfWeek == -1)
				{
					this.firstDayOfWeek = this._cultureData.IFIRSTDAYOFWEEK;
				}
				return (DayOfWeek)this.firstDayOfWeek;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value >= DayOfWeek.Sunday && value <= DayOfWeek.Saturday)
				{
					this.firstDayOfWeek = (int)value;
					return;
				}
				throw new ArgumentOutOfRangeException("value", SR.Format("Valid values are between {0} and {1}, inclusive.", DayOfWeek.Sunday, DayOfWeek.Saturday));
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06005AE3 RID: 23267 RVA: 0x001345CC File Offset: 0x001327CC
		// (set) Token: 0x06005AE4 RID: 23268 RVA: 0x001345F0 File Offset: 0x001327F0
		public CalendarWeekRule CalendarWeekRule
		{
			get
			{
				if (this.calendarWeekRule == -1)
				{
					this.calendarWeekRule = this._cultureData.IFIRSTWEEKOFYEAR;
				}
				return (CalendarWeekRule)this.calendarWeekRule;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value >= CalendarWeekRule.FirstDay && value <= CalendarWeekRule.FirstFourDayWeek)
				{
					this.calendarWeekRule = (int)value;
					return;
				}
				throw new ArgumentOutOfRangeException("value", SR.Format("Valid values are between {0} and {1}, inclusive.", CalendarWeekRule.FirstDay, CalendarWeekRule.FirstFourDayWeek));
			}
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06005AE5 RID: 23269 RVA: 0x00134640 File Offset: 0x00132840
		// (set) Token: 0x06005AE6 RID: 23270 RVA: 0x0013466C File Offset: 0x0013286C
		public string FullDateTimePattern
		{
			get
			{
				if (this.fullDateTimePattern == null)
				{
					this.fullDateTimePattern = this.LongDatePattern + " " + this.LongTimePattern;
				}
				return this.fullDateTimePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.fullDateTimePattern = value;
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06005AE7 RID: 23271 RVA: 0x0013469B File Offset: 0x0013289B
		// (set) Token: 0x06005AE8 RID: 23272 RVA: 0x001346B9 File Offset: 0x001328B9
		public string LongDatePattern
		{
			get
			{
				if (this.longDatePattern == null)
				{
					this.longDatePattern = this.UnclonedLongDatePatterns[0];
				}
				return this.longDatePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.longDatePattern = value;
				this.ClearTokenHashTable();
				this.fullDateTimePattern = null;
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06005AE9 RID: 23273 RVA: 0x001346F5 File Offset: 0x001328F5
		// (set) Token: 0x06005AEA RID: 23274 RVA: 0x00134714 File Offset: 0x00132914
		public string LongTimePattern
		{
			get
			{
				if (this.longTimePattern == null)
				{
					this.longTimePattern = this.UnclonedLongTimePatterns[0];
				}
				return this.longTimePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.longTimePattern = value;
				this.ClearTokenHashTable();
				this.fullDateTimePattern = null;
				this.generalLongTimePattern = null;
				this.dateTimeOffsetPattern = null;
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06005AEB RID: 23275 RVA: 0x00134769 File Offset: 0x00132969
		// (set) Token: 0x06005AEC RID: 23276 RVA: 0x00134795 File Offset: 0x00132995
		public string MonthDayPattern
		{
			get
			{
				if (this.monthDayPattern == null)
				{
					this.monthDayPattern = this._cultureData.MonthDay(this.Calendar.ID);
				}
				return this.monthDayPattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.monthDayPattern = value;
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06005AED RID: 23277 RVA: 0x001347C4 File Offset: 0x001329C4
		// (set) Token: 0x06005AEE RID: 23278 RVA: 0x001347E5 File Offset: 0x001329E5
		public string PMDesignator
		{
			get
			{
				if (this.pmDesignator == null)
				{
					this.pmDesignator = this._cultureData.SPM2359;
				}
				return this.pmDesignator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.ClearTokenHashTable();
				this.pmDesignator = value;
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06005AEF RID: 23279 RVA: 0x0013481A File Offset: 0x00132A1A
		public string RFC1123Pattern
		{
			get
			{
				return "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06005AF0 RID: 23280 RVA: 0x00134821 File Offset: 0x00132A21
		// (set) Token: 0x06005AF1 RID: 23281 RVA: 0x00134840 File Offset: 0x00132A40
		public string ShortDatePattern
		{
			get
			{
				if (this.shortDatePattern == null)
				{
					this.shortDatePattern = this.UnclonedShortDatePatterns[0];
				}
				return this.shortDatePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.shortDatePattern = value;
				this.ClearTokenHashTable();
				this.generalLongTimePattern = null;
				this.generalShortTimePattern = null;
				this.dateTimeOffsetPattern = null;
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06005AF2 RID: 23282 RVA: 0x00134895 File Offset: 0x00132A95
		// (set) Token: 0x06005AF3 RID: 23283 RVA: 0x001348B3 File Offset: 0x00132AB3
		public string ShortTimePattern
		{
			get
			{
				if (this.shortTimePattern == null)
				{
					this.shortTimePattern = this.UnclonedShortTimePatterns[0];
				}
				return this.shortTimePattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.shortTimePattern = value;
				this.ClearTokenHashTable();
				this.generalShortTimePattern = null;
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06005AF4 RID: 23284 RVA: 0x001348EF File Offset: 0x00132AEF
		public string SortableDateTimePattern
		{
			get
			{
				return "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06005AF5 RID: 23285 RVA: 0x001348F6 File Offset: 0x00132AF6
		internal string GeneralShortTimePattern
		{
			get
			{
				if (this.generalShortTimePattern == null)
				{
					this.generalShortTimePattern = this.ShortDatePattern + " " + this.ShortTimePattern;
				}
				return this.generalShortTimePattern;
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06005AF6 RID: 23286 RVA: 0x00134922 File Offset: 0x00132B22
		internal string GeneralLongTimePattern
		{
			get
			{
				if (this.generalLongTimePattern == null)
				{
					this.generalLongTimePattern = this.ShortDatePattern + " " + this.LongTimePattern;
				}
				return this.generalLongTimePattern;
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06005AF7 RID: 23287 RVA: 0x00134950 File Offset: 0x00132B50
		internal string DateTimeOffsetPattern
		{
			get
			{
				if (this.dateTimeOffsetPattern == null)
				{
					string text = this.ShortDatePattern + " " + this.LongTimePattern;
					bool flag = false;
					bool flag2 = false;
					char c = '\'';
					int num = 0;
					while (!flag && num < this.LongTimePattern.Length)
					{
						char c2 = this.LongTimePattern[num];
						if (c2 <= '%')
						{
							if (c2 == '"')
							{
								goto IL_006A;
							}
							if (c2 == '%')
							{
								goto IL_0096;
							}
						}
						else
						{
							if (c2 == '\'')
							{
								goto IL_006A;
							}
							if (c2 == '\\')
							{
								goto IL_0096;
							}
							if (c2 == 'z')
							{
								flag = !flag2;
							}
						}
						IL_009C:
						num++;
						continue;
						IL_006A:
						if (flag2 && c == this.LongTimePattern[num])
						{
							flag2 = false;
							goto IL_009C;
						}
						if (!flag2)
						{
							c = this.LongTimePattern[num];
							flag2 = true;
							goto IL_009C;
						}
						goto IL_009C;
						IL_0096:
						num++;
						goto IL_009C;
					}
					if (!flag)
					{
						text += " zzz";
					}
					this.dateTimeOffsetPattern = text;
				}
				return this.dateTimeOffsetPattern;
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06005AF8 RID: 23288 RVA: 0x00134A30 File Offset: 0x00132C30
		// (set) Token: 0x06005AF9 RID: 23289 RVA: 0x00134A51 File Offset: 0x00132C51
		public string TimeSeparator
		{
			get
			{
				if (this.timeSeparator == null)
				{
					this.timeSeparator = this._cultureData.TimeSeparator;
				}
				return this.timeSeparator;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.ClearTokenHashTable();
				this.timeSeparator = value;
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06005AFA RID: 23290 RVA: 0x00134A86 File Offset: 0x00132C86
		public string UniversalSortableDateTimePattern
		{
			get
			{
				return "yyyy'-'MM'-'dd HH':'mm':'ss'Z'";
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06005AFB RID: 23291 RVA: 0x00134A8D File Offset: 0x00132C8D
		// (set) Token: 0x06005AFC RID: 23292 RVA: 0x00134AAB File Offset: 0x00132CAB
		public string YearMonthPattern
		{
			get
			{
				if (this.yearMonthPattern == null)
				{
					this.yearMonthPattern = this.UnclonedYearMonthPatterns[0];
				}
				return this.yearMonthPattern;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "String reference not set to an instance of a String.");
				}
				this.yearMonthPattern = value;
				this.ClearTokenHashTable();
			}
		}

		// Token: 0x06005AFD RID: 23293 RVA: 0x00134AE0 File Offset: 0x00132CE0
		private static void CheckNullValue(string[] values, int length)
		{
			for (int i = 0; i < length; i++)
			{
				if (values[i] == null)
				{
					throw new ArgumentNullException("value", "Found a null value within an array.");
				}
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06005AFE RID: 23294 RVA: 0x00134B0E File Offset: 0x00132D0E
		// (set) Token: 0x06005AFF RID: 23295 RVA: 0x00134B20 File Offset: 0x00132D20
		public string[] AbbreviatedDayNames
		{
			get
			{
				return (string[])this.internalGetAbbreviatedDayOfWeekNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Array cannot be null.");
				}
				if (value.Length != 7)
				{
					throw new ArgumentException(SR.Format("Length of the array must be {0}.", 7), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length);
				this.ClearTokenHashTable();
				this.abbreviatedDayNames = value;
			}
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06005B00 RID: 23296 RVA: 0x00134B8A File Offset: 0x00132D8A
		// (set) Token: 0x06005B01 RID: 23297 RVA: 0x00134B9C File Offset: 0x00132D9C
		public string[] ShortestDayNames
		{
			get
			{
				return (string[])this.internalGetSuperShortDayNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Array cannot be null.");
				}
				if (value.Length != 7)
				{
					throw new ArgumentException(SR.Format("Length of the array must be {0}.", 7), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length);
				this.m_superShortDayNames = value;
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06005B02 RID: 23298 RVA: 0x00134C00 File Offset: 0x00132E00
		// (set) Token: 0x06005B03 RID: 23299 RVA: 0x00134C14 File Offset: 0x00132E14
		public string[] DayNames
		{
			get
			{
				return (string[])this.internalGetDayOfWeekNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Array cannot be null.");
				}
				if (value.Length != 7)
				{
					throw new ArgumentException(SR.Format("Length of the array must be {0}.", 7), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length);
				this.ClearTokenHashTable();
				this.dayNames = value;
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06005B04 RID: 23300 RVA: 0x00134C7E File Offset: 0x00132E7E
		// (set) Token: 0x06005B05 RID: 23301 RVA: 0x00134C90 File Offset: 0x00132E90
		public string[] AbbreviatedMonthNames
		{
			get
			{
				return (string[])this.internalGetAbbreviatedMonthNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Array cannot be null.");
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(SR.Format("Length of the array must be {0}.", 13), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.ClearTokenHashTable();
				this.abbreviatedMonthNames = value;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06005B06 RID: 23302 RVA: 0x00134CFE File Offset: 0x00132EFE
		// (set) Token: 0x06005B07 RID: 23303 RVA: 0x00134D10 File Offset: 0x00132F10
		public string[] MonthNames
		{
			get
			{
				return (string[])this.internalGetMonthNames().Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Array cannot be null.");
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(SR.Format("Length of the array must be {0}.", 13), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.monthNames = value;
				this.ClearTokenHashTable();
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06005B08 RID: 23304 RVA: 0x00134D7E File Offset: 0x00132F7E
		internal bool HasSpacesInMonthNames
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseSpacesInMonthNames) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06005B09 RID: 23305 RVA: 0x00134D8B File Offset: 0x00132F8B
		internal bool HasSpacesInDayNames
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseSpacesInDayNames) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x06005B0A RID: 23306 RVA: 0x00134D9C File Offset: 0x00132F9C
		internal string internalGetMonthName(int month, MonthNameStyles style, bool abbreviated)
		{
			string[] array;
			if (style != MonthNameStyles.Genitive)
			{
				if (style != MonthNameStyles.LeapYear)
				{
					array = (abbreviated ? this.internalGetAbbreviatedMonthNames() : this.internalGetMonthNames());
				}
				else
				{
					array = this.internalGetLeapYearMonthNames();
				}
			}
			else
			{
				array = this.internalGetGenitiveMonthNames(abbreviated);
			}
			if (month < 1 || month > array.Length)
			{
				throw new ArgumentOutOfRangeException("month", SR.Format("Valid values are between {0} and {1}, inclusive.", 1, array.Length));
			}
			return array[month - 1];
		}

		// Token: 0x06005B0B RID: 23307 RVA: 0x00134E0C File Offset: 0x0013300C
		private string[] internalGetGenitiveMonthNames(bool abbreviated)
		{
			if (abbreviated)
			{
				if (this.m_genitiveAbbreviatedMonthNames == null)
				{
					this.m_genitiveAbbreviatedMonthNames = this._cultureData.AbbreviatedGenitiveMonthNames(this.Calendar.ID);
				}
				return this.m_genitiveAbbreviatedMonthNames;
			}
			if (this.genitiveMonthNames == null)
			{
				this.genitiveMonthNames = this._cultureData.GenitiveMonthNames(this.Calendar.ID);
			}
			return this.genitiveMonthNames;
		}

		// Token: 0x06005B0C RID: 23308 RVA: 0x00134E71 File Offset: 0x00133071
		internal string[] internalGetLeapYearMonthNames()
		{
			if (this.leapYearMonthNames == null)
			{
				this.leapYearMonthNames = this._cultureData.LeapYearMonthNames(this.Calendar.ID);
			}
			return this.leapYearMonthNames;
		}

		// Token: 0x06005B0D RID: 23309 RVA: 0x00134E9D File Offset: 0x0013309D
		public string GetAbbreviatedDayName(DayOfWeek dayofweek)
		{
			if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("dayofweek", SR.Format("Valid values are between {0} and {1}, inclusive.", DayOfWeek.Sunday, DayOfWeek.Saturday));
			}
			return this.internalGetAbbreviatedDayOfWeekNames()[(int)dayofweek];
		}

		// Token: 0x06005B0E RID: 23310 RVA: 0x00134ED0 File Offset: 0x001330D0
		public string GetShortestDayName(DayOfWeek dayOfWeek)
		{
			if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("dayOfWeek", SR.Format("Valid values are between {0} and {1}, inclusive.", DayOfWeek.Sunday, DayOfWeek.Saturday));
			}
			return this.internalGetSuperShortDayNames()[(int)dayOfWeek];
		}

		// Token: 0x06005B0F RID: 23311 RVA: 0x00134F04 File Offset: 0x00133104
		private static string[] GetCombinedPatterns(string[] patterns1, string[] patterns2, string connectString)
		{
			string[] array = new string[patterns1.Length * patterns2.Length];
			int num = 0;
			for (int i = 0; i < patterns1.Length; i++)
			{
				for (int j = 0; j < patterns2.Length; j++)
				{
					array[num++] = patterns1[i] + connectString + patterns2[j];
				}
			}
			return array;
		}

		// Token: 0x06005B10 RID: 23312 RVA: 0x00134F50 File Offset: 0x00133150
		public string[] GetAllDateTimePatterns()
		{
			List<string> list = new List<string>(132);
			for (int i = 0; i < DateTimeFormat.allStandardFormats.Length; i++)
			{
				string[] allDateTimePatterns = this.GetAllDateTimePatterns(DateTimeFormat.allStandardFormats[i]);
				for (int j = 0; j < allDateTimePatterns.Length; j++)
				{
					list.Add(allDateTimePatterns[j]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06005B11 RID: 23313 RVA: 0x00134FA8 File Offset: 0x001331A8
		public string[] GetAllDateTimePatterns(char format)
		{
			if (format <= 'U')
			{
				switch (format)
				{
				case 'D':
					return this.AllLongDatePatterns;
				case 'E':
					goto IL_01AF;
				case 'F':
					break;
				case 'G':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllShortDatePatterns, this.AllLongTimePatterns, " ");
				default:
					switch (format)
					{
					case 'M':
						goto IL_013D;
					case 'N':
					case 'P':
					case 'Q':
					case 'S':
						goto IL_01AF;
					case 'O':
						goto IL_014F;
					case 'R':
						goto IL_0160;
					case 'T':
						return this.AllLongTimePatterns;
					case 'U':
						break;
					default:
						goto IL_01AF;
					}
					break;
				}
				return DateTimeFormatInfo.GetCombinedPatterns(this.AllLongDatePatterns, this.AllLongTimePatterns, " ");
			}
			if (format != 'Y')
			{
				switch (format)
				{
				case 'd':
					return this.AllShortDatePatterns;
				case 'e':
					goto IL_01AF;
				case 'f':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllLongDatePatterns, this.AllShortTimePatterns, " ");
				case 'g':
					return DateTimeFormatInfo.GetCombinedPatterns(this.AllShortDatePatterns, this.AllShortTimePatterns, " ");
				default:
					switch (format)
					{
					case 'm':
						goto IL_013D;
					case 'n':
					case 'p':
					case 'q':
					case 'v':
					case 'w':
					case 'x':
						goto IL_01AF;
					case 'o':
						goto IL_014F;
					case 'r':
						goto IL_0160;
					case 's':
						return new string[] { "yyyy'-'MM'-'dd'T'HH':'mm':'ss" };
					case 't':
						return this.AllShortTimePatterns;
					case 'u':
						return new string[] { this.UniversalSortableDateTimePattern };
					case 'y':
						break;
					default:
						goto IL_01AF;
					}
					break;
				}
			}
			return this.AllYearMonthPatterns;
			IL_013D:
			return new string[] { this.MonthDayPattern };
			IL_014F:
			return new string[] { "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK" };
			IL_0160:
			return new string[] { "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'" };
			IL_01AF:
			throw new ArgumentException(SR.Format("Format specifier '{0}' was invalid.", format), "format");
		}

		// Token: 0x06005B12 RID: 23314 RVA: 0x00135180 File Offset: 0x00133380
		public string GetDayName(DayOfWeek dayofweek)
		{
			if (dayofweek < DayOfWeek.Sunday || dayofweek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("dayofweek", SR.Format("Valid values are between {0} and {1}, inclusive.", DayOfWeek.Sunday, DayOfWeek.Saturday));
			}
			return this.internalGetDayOfWeekNames()[(int)dayofweek];
		}

		// Token: 0x06005B13 RID: 23315 RVA: 0x001351B3 File Offset: 0x001333B3
		public string GetAbbreviatedMonthName(int month)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", SR.Format("Valid values are between {0} and {1}, inclusive.", 1, 13));
			}
			return this.internalGetAbbreviatedMonthNames()[month - 1];
		}

		// Token: 0x06005B14 RID: 23316 RVA: 0x001351EA File Offset: 0x001333EA
		public string GetMonthName(int month)
		{
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", SR.Format("Valid values are between {0} and {1}, inclusive.", 1, 13));
			}
			return this.internalGetMonthNames()[month - 1];
		}

		// Token: 0x06005B15 RID: 23317 RVA: 0x00135224 File Offset: 0x00133424
		private static string[] GetMergedPatterns(string[] patterns, string defaultPattern)
		{
			if (defaultPattern == patterns[0])
			{
				return (string[])patterns.Clone();
			}
			int num = 0;
			while (num < patterns.Length && !(defaultPattern == patterns[num]))
			{
				num++;
			}
			string[] array;
			if (num < patterns.Length)
			{
				array = (string[])patterns.Clone();
				array[num] = array[0];
			}
			else
			{
				array = new string[patterns.Length + 1];
				Array.Copy(patterns, 0, array, 1, patterns.Length);
			}
			array[0] = defaultPattern;
			return array;
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06005B16 RID: 23318 RVA: 0x00135297 File Offset: 0x00133497
		private string[] AllYearMonthPatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedYearMonthPatterns, this.YearMonthPattern);
			}
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06005B17 RID: 23319 RVA: 0x001352AA File Offset: 0x001334AA
		private string[] AllShortDatePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedShortDatePatterns, this.ShortDatePattern);
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06005B18 RID: 23320 RVA: 0x001352BD File Offset: 0x001334BD
		private string[] AllShortTimePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedShortTimePatterns, this.ShortTimePattern);
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06005B19 RID: 23321 RVA: 0x001352D0 File Offset: 0x001334D0
		private string[] AllLongDatePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedLongDatePatterns, this.LongDatePattern);
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06005B1A RID: 23322 RVA: 0x001352E3 File Offset: 0x001334E3
		private string[] AllLongTimePatterns
		{
			get
			{
				return DateTimeFormatInfo.GetMergedPatterns(this.UnclonedLongTimePatterns, this.LongTimePattern);
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06005B1B RID: 23323 RVA: 0x001352F6 File Offset: 0x001334F6
		private string[] UnclonedYearMonthPatterns
		{
			get
			{
				if (this.allYearMonthPatterns == null)
				{
					this.allYearMonthPatterns = this._cultureData.YearMonths(this.Calendar.ID);
				}
				return this.allYearMonthPatterns;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06005B1C RID: 23324 RVA: 0x00135322 File Offset: 0x00133522
		private string[] UnclonedShortDatePatterns
		{
			get
			{
				if (this.allShortDatePatterns == null)
				{
					this.allShortDatePatterns = this._cultureData.ShortDates(this.Calendar.ID);
				}
				return this.allShortDatePatterns;
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06005B1D RID: 23325 RVA: 0x0013534E File Offset: 0x0013354E
		private string[] UnclonedLongDatePatterns
		{
			get
			{
				if (this.allLongDatePatterns == null)
				{
					this.allLongDatePatterns = this._cultureData.LongDates(this.Calendar.ID);
				}
				return this.allLongDatePatterns;
			}
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06005B1E RID: 23326 RVA: 0x0013537A File Offset: 0x0013357A
		private string[] UnclonedShortTimePatterns
		{
			get
			{
				if (this.allShortTimePatterns == null)
				{
					this.allShortTimePatterns = this._cultureData.ShortTimes;
				}
				return this.allShortTimePatterns;
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06005B1F RID: 23327 RVA: 0x0013539B File Offset: 0x0013359B
		private string[] UnclonedLongTimePatterns
		{
			get
			{
				if (this.allLongTimePatterns == null)
				{
					this.allLongTimePatterns = this._cultureData.LongTimes;
				}
				return this.allLongTimePatterns;
			}
		}

		// Token: 0x06005B20 RID: 23328 RVA: 0x001353BC File Offset: 0x001335BC
		public static DateTimeFormatInfo ReadOnly(DateTimeFormatInfo dtfi)
		{
			if (dtfi == null)
			{
				throw new ArgumentNullException("dtfi", "Object cannot be null.");
			}
			if (dtfi.IsReadOnly)
			{
				return dtfi;
			}
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)dtfi.MemberwiseClone();
			dateTimeFormatInfo.calendar = Calendar.ReadOnly(dtfi.Calendar);
			dateTimeFormatInfo._isReadOnly = true;
			return dateTimeFormatInfo;
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06005B21 RID: 23329 RVA: 0x00135409 File Offset: 0x00133609
		public bool IsReadOnly
		{
			get
			{
				return GlobalizationMode.Invariant || this._isReadOnly;
			}
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06005B22 RID: 23330 RVA: 0x0013541A File Offset: 0x0013361A
		public string NativeCalendarName
		{
			get
			{
				return this._cultureData.CalendarName(this.Calendar.ID);
			}
		}

		// Token: 0x06005B23 RID: 23331 RVA: 0x00135434 File Offset: 0x00133634
		public void SetAllDateTimePatterns(string[] patterns, char format)
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException("Instance is read-only.");
			}
			if (patterns == null)
			{
				throw new ArgumentNullException("patterns", "Array cannot be null.");
			}
			if (patterns.Length == 0)
			{
				throw new ArgumentException("Array must not be of length zero.", "patterns");
			}
			for (int i = 0; i < patterns.Length; i++)
			{
				if (patterns[i] == null)
				{
					throw new ArgumentNullException("patterns[" + i.ToString() + "]", "Found a null value within an array.");
				}
			}
			if (format <= 'Y')
			{
				if (format == 'D')
				{
					this.allLongDatePatterns = patterns;
					this.longDatePattern = this.allLongDatePatterns[0];
					goto IL_0126;
				}
				if (format == 'T')
				{
					this.allLongTimePatterns = patterns;
					this.longTimePattern = this.allLongTimePatterns[0];
					goto IL_0126;
				}
				if (format != 'Y')
				{
					goto IL_010B;
				}
			}
			else
			{
				if (format == 'd')
				{
					this.allShortDatePatterns = patterns;
					this.shortDatePattern = this.allShortDatePatterns[0];
					goto IL_0126;
				}
				if (format == 't')
				{
					this.allShortTimePatterns = patterns;
					this.shortTimePattern = this.allShortTimePatterns[0];
					goto IL_0126;
				}
				if (format != 'y')
				{
					goto IL_010B;
				}
			}
			this.allYearMonthPatterns = patterns;
			this.yearMonthPattern = this.allYearMonthPatterns[0];
			goto IL_0126;
			IL_010B:
			throw new ArgumentException(SR.Format("Format specifier '{0}' was invalid.", format), "format");
			IL_0126:
			this.ClearTokenHashTable();
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06005B24 RID: 23332 RVA: 0x0013556D File Offset: 0x0013376D
		// (set) Token: 0x06005B25 RID: 23333 RVA: 0x00135580 File Offset: 0x00133780
		public string[] AbbreviatedMonthGenitiveNames
		{
			get
			{
				return (string[])this.internalGetGenitiveMonthNames(true).Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Array cannot be null.");
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(SR.Format("Length of the array must be {0}.", 13), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.ClearTokenHashTable();
				this.m_genitiveAbbreviatedMonthNames = value;
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06005B26 RID: 23334 RVA: 0x001355EE File Offset: 0x001337EE
		// (set) Token: 0x06005B27 RID: 23335 RVA: 0x00135604 File Offset: 0x00133804
		public string[] MonthGenitiveNames
		{
			get
			{
				return (string[])this.internalGetGenitiveMonthNames(false).Clone();
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException("Instance is read-only.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value", "Array cannot be null.");
				}
				if (value.Length != 13)
				{
					throw new ArgumentException(SR.Format("Length of the array must be {0}.", 13), "value");
				}
				DateTimeFormatInfo.CheckNullValue(value, value.Length - 1);
				this.genitiveMonthNames = value;
				this.ClearTokenHashTable();
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06005B28 RID: 23336 RVA: 0x00135674 File Offset: 0x00133874
		internal string FullTimeSpanPositivePattern
		{
			get
			{
				if (this._fullTimeSpanPositivePattern == null)
				{
					CultureData cultureData;
					if (this._cultureData.UseUserOverride)
					{
						cultureData = CultureData.GetCultureData(this._cultureData.CultureName, false);
					}
					else
					{
						cultureData = this._cultureData;
					}
					string numberDecimalSeparator = new NumberFormatInfo(cultureData).NumberDecimalSeparator;
					this._fullTimeSpanPositivePattern = "d':'h':'mm':'ss'" + numberDecimalSeparator + "'FFFFFFF";
				}
				return this._fullTimeSpanPositivePattern;
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06005B29 RID: 23337 RVA: 0x001356D9 File Offset: 0x001338D9
		internal string FullTimeSpanNegativePattern
		{
			get
			{
				if (this._fullTimeSpanNegativePattern == null)
				{
					this._fullTimeSpanNegativePattern = "'-'" + this.FullTimeSpanPositivePattern;
				}
				return this._fullTimeSpanNegativePattern;
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06005B2A RID: 23338 RVA: 0x001356FF File Offset: 0x001338FF
		internal CompareInfo CompareInfo
		{
			get
			{
				if (this._compareInfo == null)
				{
					this._compareInfo = CompareInfo.GetCompareInfo(this._cultureData.SCOMPAREINFO);
				}
				return this._compareInfo;
			}
		}

		// Token: 0x06005B2B RID: 23339 RVA: 0x00135728 File Offset: 0x00133928
		internal static void ValidateStyles(DateTimeStyles style, string parameterName)
		{
			if ((style & ~(DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind)) != DateTimeStyles.None)
			{
				throw new ArgumentException("An undefined DateTimeStyles value is being used.", parameterName);
			}
			if ((style & DateTimeStyles.AssumeLocal) != DateTimeStyles.None && (style & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
			{
				throw new ArgumentException("The DateTimeStyles values AssumeLocal and AssumeUniversal cannot be used together.", parameterName);
			}
			if ((style & DateTimeStyles.RoundtripKind) != DateTimeStyles.None && (style & (DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal)) != DateTimeStyles.None)
			{
				throw new ArgumentException("The DateTimeStyles value RoundtripKind cannot be used with the values AssumeLocal, AssumeUniversal or AdjustToUniversal.", parameterName);
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06005B2C RID: 23340 RVA: 0x0013577D File Offset: 0x0013397D
		internal DateTimeFormatFlags FormatFlags
		{
			get
			{
				if (this.formatFlags == DateTimeFormatFlags.NotInitialized)
				{
					return this.InitializeFormatFlags();
				}
				return this.formatFlags;
			}
		}

		// Token: 0x06005B2D RID: 23341 RVA: 0x00135798 File Offset: 0x00133998
		[MethodImpl(MethodImplOptions.NoInlining)]
		private DateTimeFormatFlags InitializeFormatFlags()
		{
			this.formatFlags = (DateTimeFormatFlags)(DateTimeFormatInfoScanner.GetFormatFlagGenitiveMonth(this.MonthNames, this.internalGetGenitiveMonthNames(false), this.AbbreviatedMonthNames, this.internalGetGenitiveMonthNames(true)) | DateTimeFormatInfoScanner.GetFormatFlagUseSpaceInMonthNames(this.MonthNames, this.internalGetGenitiveMonthNames(false), this.AbbreviatedMonthNames, this.internalGetGenitiveMonthNames(true)) | DateTimeFormatInfoScanner.GetFormatFlagUseSpaceInDayNames(this.DayNames, this.AbbreviatedDayNames) | DateTimeFormatInfoScanner.GetFormatFlagUseHebrewCalendar(this.Calendar.ID));
			return this.formatFlags;
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06005B2E RID: 23342 RVA: 0x00135814 File Offset: 0x00133A14
		internal bool HasForceTwoDigitYears
		{
			get
			{
				CalendarId calendarId = (CalendarId)this.calendar.ID;
				return calendarId - CalendarId.JAPAN <= 1;
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06005B2F RID: 23343 RVA: 0x00135837 File Offset: 0x00133A37
		internal bool HasYearMonthAdjustment
		{
			get
			{
				return (this.FormatFlags & DateTimeFormatFlags.UseHebrewRule) > DateTimeFormatFlags.None;
			}
		}

		// Token: 0x06005B30 RID: 23344 RVA: 0x00135844 File Offset: 0x00133A44
		internal bool YearMonthAdjustment(ref int year, ref int month, bool parsedMonthName)
		{
			if ((this.FormatFlags & DateTimeFormatFlags.UseHebrewRule) != DateTimeFormatFlags.None)
			{
				if (year < 1000)
				{
					year += 5000;
				}
				if (year < this.Calendar.GetYear(this.Calendar.MinSupportedDateTime) || year > this.Calendar.GetYear(this.Calendar.MaxSupportedDateTime))
				{
					return false;
				}
				if (parsedMonthName && !this.Calendar.IsLeapYear(year))
				{
					if (month >= 8)
					{
						month--;
					}
					else if (month == 7)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06005B31 RID: 23345 RVA: 0x001358CC File Offset: 0x00133ACC
		internal static DateTimeFormatInfo GetJapaneseCalendarDTFI()
		{
			DateTimeFormatInfo dateTimeFormat = DateTimeFormatInfo.s_jajpDTFI;
			if (dateTimeFormat == null && !GlobalizationMode.Invariant)
			{
				dateTimeFormat = new CultureInfo("ja-JP", false).DateTimeFormat;
				dateTimeFormat.Calendar = JapaneseCalendar.GetDefaultInstance();
				DateTimeFormatInfo.s_jajpDTFI = dateTimeFormat;
			}
			return dateTimeFormat;
		}

		// Token: 0x06005B32 RID: 23346 RVA: 0x00135910 File Offset: 0x00133B10
		internal static DateTimeFormatInfo GetTaiwanCalendarDTFI()
		{
			DateTimeFormatInfo dateTimeFormat = DateTimeFormatInfo.s_zhtwDTFI;
			if (dateTimeFormat == null && !GlobalizationMode.Invariant)
			{
				dateTimeFormat = new CultureInfo("zh-TW", false).DateTimeFormat;
				dateTimeFormat.Calendar = TaiwanCalendar.GetDefaultInstance();
				DateTimeFormatInfo.s_zhtwDTFI = dateTimeFormat;
			}
			return dateTimeFormat;
		}

		// Token: 0x06005B33 RID: 23347 RVA: 0x00135954 File Offset: 0x00133B54
		private void ClearTokenHashTable()
		{
			this._dtfiTokenHash = null;
			this.formatFlags = DateTimeFormatFlags.NotInitialized;
		}

		// Token: 0x06005B34 RID: 23348 RVA: 0x00135964 File Offset: 0x00133B64
		internal DateTimeFormatInfo.TokenHashValue[] CreateTokenHashTable()
		{
			DateTimeFormatInfo.TokenHashValue[] array = this._dtfiTokenHash;
			if (array == null)
			{
				array = new DateTimeFormatInfo.TokenHashValue[199];
				if (!GlobalizationMode.Invariant)
				{
					this.LanguageName.Equals("ko");
				}
				string text = this.TimeSeparator.Trim();
				if ("," != text)
				{
					this.InsertHash(array, ",", TokenType.IgnorableSymbol, 0);
				}
				if ("." != text)
				{
					this.InsertHash(array, ".", TokenType.IgnorableSymbol, 0);
				}
				if (!GlobalizationMode.Invariant && "시" != text && "時" != text && "时" != text)
				{
					this.InsertHash(array, this.TimeSeparator, TokenType.SEP_Time, 0);
				}
				this.InsertHash(array, this.AMDesignator, (TokenType)1027, 0);
				this.InsertHash(array, this.PMDesignator, (TokenType)1284, 1);
				bool flag = false;
				if (!GlobalizationMode.Invariant)
				{
					this.PopulateSpecialTokenHashTable(array, ref flag);
				}
				if (!GlobalizationMode.Invariant && this.LanguageName.Equals("ky"))
				{
					this.InsertHash(array, "-", TokenType.IgnorableSymbol, 0);
				}
				else
				{
					this.InsertHash(array, "-", TokenType.SEP_DateOrOffset, 0);
				}
				if (!flag)
				{
					this.InsertHash(array, this.DateSeparator, TokenType.SEP_Date, 0);
				}
				this.AddMonthNames(array, null);
				for (int i = 1; i <= 13; i++)
				{
					this.InsertHash(array, this.GetAbbreviatedMonthName(i), TokenType.MonthToken, i);
				}
				if ((this.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None)
				{
					for (int j = 1; j <= 13; j++)
					{
						string text2 = this.internalGetMonthName(j, MonthNameStyles.Genitive, false);
						this.InsertHash(array, text2, TokenType.MonthToken, j);
					}
				}
				if ((this.FormatFlags & DateTimeFormatFlags.UseLeapYearMonth) != DateTimeFormatFlags.None)
				{
					for (int k = 1; k <= 13; k++)
					{
						string text3 = this.internalGetMonthName(k, MonthNameStyles.LeapYear, false);
						this.InsertHash(array, text3, TokenType.MonthToken, k);
					}
				}
				for (int l = 0; l < 7; l++)
				{
					string text4 = this.GetDayName((DayOfWeek)l);
					this.InsertHash(array, text4, TokenType.DayOfWeekToken, l);
					text4 = this.GetAbbreviatedDayName((DayOfWeek)l);
					this.InsertHash(array, text4, TokenType.DayOfWeekToken, l);
				}
				int[] eras = this.calendar.Eras;
				for (int m = 1; m <= eras.Length; m++)
				{
					this.InsertHash(array, this.GetEraName(m), TokenType.EraToken, m);
					this.InsertHash(array, this.GetAbbreviatedEraName(m), TokenType.EraToken, m);
				}
				this.InsertHash(array, DateTimeFormatInfo.InvariantInfo.AMDesignator, (TokenType)1027, 0);
				this.InsertHash(array, DateTimeFormatInfo.InvariantInfo.PMDesignator, (TokenType)1284, 1);
				for (int n = 1; n <= 12; n++)
				{
					string text5 = DateTimeFormatInfo.InvariantInfo.GetMonthName(n);
					this.InsertHash(array, text5, TokenType.MonthToken, n);
					text5 = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(n);
					this.InsertHash(array, text5, TokenType.MonthToken, n);
				}
				for (int num = 0; num < 7; num++)
				{
					string text6 = DateTimeFormatInfo.InvariantInfo.GetDayName((DayOfWeek)num);
					this.InsertHash(array, text6, TokenType.DayOfWeekToken, num);
					text6 = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedDayName((DayOfWeek)num);
					this.InsertHash(array, text6, TokenType.DayOfWeekToken, num);
				}
				for (int num2 = 0; num2 < this.AbbreviatedEnglishEraNames.Length; num2++)
				{
					this.InsertHash(array, this.AbbreviatedEnglishEraNames[num2], TokenType.EraToken, num2 + 1);
				}
				this.InsertHash(array, "T", TokenType.SEP_LocalTimeMark, 0);
				this.InsertHash(array, "GMT", TokenType.TimeZoneToken, 0);
				this.InsertHash(array, "Z", TokenType.TimeZoneToken, 0);
				this.InsertHash(array, "/", TokenType.SEP_Date, 0);
				this.InsertHash(array, ":", TokenType.SEP_Time, 0);
				this._dtfiTokenHash = array;
			}
			return array;
		}

		// Token: 0x06005B35 RID: 23349 RVA: 0x00135D04 File Offset: 0x00133F04
		private void PopulateSpecialTokenHashTable(DateTimeFormatInfo.TokenHashValue[] temp, ref bool useDateSepAsIgnorableSymbol)
		{
			if (this.LanguageName.Equals("sq"))
			{
				this.InsertHash(temp, "." + this.AMDesignator, (TokenType)1027, 0);
				this.InsertHash(temp, "." + this.PMDesignator, (TokenType)1284, 1);
			}
			this.InsertHash(temp, "年", TokenType.SEP_YearSuff, 0);
			this.InsertHash(temp, "년", TokenType.SEP_YearSuff, 0);
			this.InsertHash(temp, "月", TokenType.SEP_MonthSuff, 0);
			this.InsertHash(temp, "월", TokenType.SEP_MonthSuff, 0);
			this.InsertHash(temp, "日", TokenType.SEP_DaySuff, 0);
			this.InsertHash(temp, "일", TokenType.SEP_DaySuff, 0);
			this.InsertHash(temp, "時", TokenType.SEP_HourSuff, 0);
			this.InsertHash(temp, "时", TokenType.SEP_HourSuff, 0);
			this.InsertHash(temp, "分", TokenType.SEP_MinuteSuff, 0);
			this.InsertHash(temp, "秒", TokenType.SEP_SecondSuff, 0);
			if (!AppContextSwitches.EnforceLegacyJapaneseDateParsing && this.Calendar.ID == 3)
			{
				this.InsertHash(temp, "元", TokenType.YearNumberToken, 1);
				this.InsertHash(temp, "(", TokenType.IgnorableSymbol, 0);
				this.InsertHash(temp, ")", TokenType.IgnorableSymbol, 0);
			}
			if (this.LanguageName.Equals("ko"))
			{
				this.InsertHash(temp, "시", TokenType.SEP_HourSuff, 0);
				this.InsertHash(temp, "분", TokenType.SEP_MinuteSuff, 0);
				this.InsertHash(temp, "초", TokenType.SEP_SecondSuff, 0);
			}
			string[] dateWordsOfDTFI = new DateTimeFormatInfoScanner().GetDateWordsOfDTFI(this);
			DateTimeFormatFlags dateTimeFormatFlags = this.FormatFlags;
			if (dateWordsOfDTFI != null)
			{
				for (int i = 0; i < dateWordsOfDTFI.Length; i++)
				{
					char c = dateWordsOfDTFI[i][0];
					if (c != '\ue000')
					{
						if (c != '\ue001')
						{
							this.InsertHash(temp, dateWordsOfDTFI[i], TokenType.DateWordToken, 0);
							if (this.LanguageName.Equals("eu"))
							{
								this.InsertHash(temp, "." + dateWordsOfDTFI[i], TokenType.DateWordToken, 0);
							}
						}
						else
						{
							string text = dateWordsOfDTFI[i].Substring(1);
							this.InsertHash(temp, text, TokenType.IgnorableSymbol, 0);
							if (this.DateSeparator.Trim(null).Equals(text))
							{
								useDateSepAsIgnorableSymbol = true;
							}
						}
					}
					else
					{
						string text2 = dateWordsOfDTFI[i].Substring(1);
						this.AddMonthNames(temp, text2);
					}
				}
			}
			if (this.LanguageName.Equals("ja"))
			{
				for (int j = 0; j < 7; j++)
				{
					string text3 = "(" + this.GetAbbreviatedDayName((DayOfWeek)j) + ")";
					this.InsertHash(temp, text3, TokenType.DayOfWeekToken, j);
				}
				if (!DateTimeFormatInfo.IsJapaneseCalendar(this.Calendar))
				{
					DateTimeFormatInfo japaneseCalendarDTFI = DateTimeFormatInfo.GetJapaneseCalendarDTFI();
					for (int k = 1; k <= japaneseCalendarDTFI.Calendar.Eras.Length; k++)
					{
						this.InsertHash(temp, japaneseCalendarDTFI.GetEraName(k), TokenType.JapaneseEraToken, k);
						this.InsertHash(temp, japaneseCalendarDTFI.GetAbbreviatedEraName(k), TokenType.JapaneseEraToken, k);
						this.InsertHash(temp, japaneseCalendarDTFI.AbbreviatedEnglishEraNames[k - 1], TokenType.JapaneseEraToken, k);
					}
					return;
				}
			}
			else if (this.CultureName.Equals("zh-TW"))
			{
				DateTimeFormatInfo taiwanCalendarDTFI = DateTimeFormatInfo.GetTaiwanCalendarDTFI();
				for (int l = 1; l <= taiwanCalendarDTFI.Calendar.Eras.Length; l++)
				{
					if (taiwanCalendarDTFI.GetEraName(l).Length > 0)
					{
						this.InsertHash(temp, taiwanCalendarDTFI.GetEraName(l), TokenType.TEraToken, l);
					}
				}
			}
		}

		// Token: 0x06005B36 RID: 23350 RVA: 0x00136079 File Offset: 0x00134279
		private static bool IsJapaneseCalendar(Calendar calendar)
		{
			if (GlobalizationMode.Invariant)
			{
				throw new PlatformNotSupportedException();
			}
			return calendar.GetType() == typeof(JapaneseCalendar);
		}

		// Token: 0x06005B37 RID: 23351 RVA: 0x001360A0 File Offset: 0x001342A0
		private void AddMonthNames(DateTimeFormatInfo.TokenHashValue[] temp, string monthPostfix)
		{
			for (int i = 1; i <= 13; i++)
			{
				string text = this.GetMonthName(i);
				if (text.Length > 0)
				{
					if (monthPostfix != null)
					{
						this.InsertHash(temp, text + monthPostfix, TokenType.MonthToken, i);
					}
					else
					{
						this.InsertHash(temp, text, TokenType.MonthToken, i);
					}
				}
				text = this.GetAbbreviatedMonthName(i);
				this.InsertHash(temp, text, TokenType.MonthToken, i);
			}
		}

		// Token: 0x06005B38 RID: 23352 RVA: 0x001360FC File Offset: 0x001342FC
		private unsafe static bool TryParseHebrewNumber(ref __DTString str, out bool badFormat, out int number)
		{
			number = -1;
			badFormat = false;
			int index = str.Index;
			if (!HebrewNumber.IsDigit((char)(*str.Value[index])))
			{
				return false;
			}
			HebrewNumberParsingContext hebrewNumberParsingContext = new HebrewNumberParsingContext(0);
			HebrewNumberParsingState hebrewNumberParsingState;
			for (;;)
			{
				hebrewNumberParsingState = HebrewNumber.ParseByChar((char)(*str.Value[index++]), ref hebrewNumberParsingContext);
				if (hebrewNumberParsingState <= HebrewNumberParsingState.NotHebrewDigit)
				{
					break;
				}
				if (index >= str.Value.Length || hebrewNumberParsingState == HebrewNumberParsingState.FoundEndOfHebrewNumber)
				{
					goto IL_005C;
				}
			}
			return false;
			IL_005C:
			if (hebrewNumberParsingState != HebrewNumberParsingState.FoundEndOfHebrewNumber)
			{
				return false;
			}
			str.Advance(index - str.Index);
			number = hebrewNumberParsingContext.result;
			return true;
		}

		// Token: 0x06005B39 RID: 23353 RVA: 0x00136183 File Offset: 0x00134383
		private static bool IsHebrewChar(char ch)
		{
			return ch >= '\u0590' && ch <= '\u05ff';
		}

		// Token: 0x06005B3A RID: 23354 RVA: 0x0013619C File Offset: 0x0013439C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsAllowedJapaneseTokenFollowedByNonSpaceLetter(string tokenString, char nextCh)
		{
			return !AppContextSwitches.EnforceLegacyJapaneseDateParsing && this.Calendar.ID == 3 && (nextCh == "元"[0] || (tokenString == "元" && nextCh == "年"[0]));
		}

		// Token: 0x06005B3B RID: 23355 RVA: 0x001361EC File Offset: 0x001343EC
		internal unsafe bool Tokenize(TokenType TokenMask, out TokenType tokenType, out int tokenValue, ref __DTString str)
		{
			tokenType = TokenType.UnknownToken;
			tokenValue = 0;
			char c = str.m_current;
			bool flag = char.IsLetter(c);
			if (flag)
			{
				c = this.Culture.TextInfo.ToLower(c);
				bool flag2;
				if (!GlobalizationMode.Invariant && DateTimeFormatInfo.IsHebrewChar(c) && TokenMask == TokenType.RegularTokenMask && DateTimeFormatInfo.TryParseHebrewNumber(ref str, out flag2, out tokenValue))
				{
					if (flag2)
					{
						tokenType = TokenType.UnknownToken;
						return false;
					}
					tokenType = TokenType.HebrewNumber;
					return true;
				}
			}
			int num = (int)(c % 'Ç');
			int num2 = (int)('\u0001' + c % 'Å');
			int num3 = str.Length - str.Index;
			int num4 = 0;
			DateTimeFormatInfo.TokenHashValue[] array = this._dtfiTokenHash;
			if (array == null)
			{
				array = this.CreateTokenHashTable();
			}
			DateTimeFormatInfo.TokenHashValue tokenHashValue;
			int num6;
			for (;;)
			{
				tokenHashValue = array[num];
				if (tokenHashValue == null)
				{
					return false;
				}
				if ((tokenHashValue.tokenType & TokenMask) > (TokenType)0 && tokenHashValue.tokenString.Length <= num3)
				{
					bool flag3 = true;
					if (flag)
					{
						int num5 = str.Index + tokenHashValue.tokenString.Length;
						if (num5 > str.Length)
						{
							flag3 = false;
						}
						else if (num5 < str.Length)
						{
							char c2 = (char)(*str.Value[num5]);
							flag3 = !char.IsLetter(c2) || this.IsAllowedJapaneseTokenFollowedByNonSpaceLetter(tokenHashValue.tokenString, c2);
						}
					}
					if (flag3 && ((tokenHashValue.tokenString.Length == 1 && *str.Value[str.Index] == (ushort)tokenHashValue.tokenString[0]) || this.Culture.CompareInfo.Compare(str.Value.Slice(str.Index, tokenHashValue.tokenString.Length), tokenHashValue.tokenString, CompareOptions.IgnoreCase) == 0))
					{
						break;
					}
					if ((tokenHashValue.tokenType == TokenType.MonthToken && this.HasSpacesInMonthNames) || (tokenHashValue.tokenType == TokenType.DayOfWeekToken && this.HasSpacesInDayNames))
					{
						num6 = 0;
						if (str.MatchSpecifiedWords(tokenHashValue.tokenString, true, ref num6))
						{
							goto Block_19;
						}
					}
				}
				num4++;
				num += num2;
				if (num >= 199)
				{
					num -= 199;
				}
				if (num4 >= 199)
				{
					return false;
				}
			}
			tokenType = tokenHashValue.tokenType & TokenMask;
			tokenValue = tokenHashValue.tokenValue;
			str.Advance(tokenHashValue.tokenString.Length);
			return true;
			Block_19:
			tokenType = tokenHashValue.tokenType & TokenMask;
			tokenValue = tokenHashValue.tokenValue;
			str.Advance(num6);
			return true;
		}

		// Token: 0x06005B3C RID: 23356 RVA: 0x00136434 File Offset: 0x00134634
		private void InsertAtCurrentHashNode(DateTimeFormatInfo.TokenHashValue[] hashTable, string str, char ch, TokenType tokenType, int tokenValue, int pos, int hashcode, int hashProbe)
		{
			DateTimeFormatInfo.TokenHashValue tokenHashValue = hashTable[hashcode];
			hashTable[hashcode] = new DateTimeFormatInfo.TokenHashValue(str, tokenType, tokenValue);
			while (++pos < 199)
			{
				hashcode += hashProbe;
				if (hashcode >= 199)
				{
					hashcode -= 199;
				}
				DateTimeFormatInfo.TokenHashValue tokenHashValue2 = hashTable[hashcode];
				if (tokenHashValue2 == null || this.Culture.TextInfo.ToLower(tokenHashValue2.tokenString[0]) == ch)
				{
					hashTable[hashcode] = tokenHashValue;
					if (tokenHashValue2 == null)
					{
						return;
					}
					tokenHashValue = tokenHashValue2;
				}
			}
		}

		// Token: 0x06005B3D RID: 23357 RVA: 0x001364B0 File Offset: 0x001346B0
		private void InsertHash(DateTimeFormatInfo.TokenHashValue[] hashTable, string str, TokenType tokenType, int tokenValue)
		{
			if (str == null || str.Length == 0)
			{
				return;
			}
			int num = 0;
			if (char.IsWhiteSpace(str[0]) || char.IsWhiteSpace(str[str.Length - 1]))
			{
				str = str.Trim(null);
				if (str.Length == 0)
				{
					return;
				}
			}
			char c = this.Culture.TextInfo.ToLower(str[0]);
			int num2 = (int)(c % 'Ç');
			int num3 = (int)('\u0001' + c % 'Å');
			DateTimeFormatInfo.TokenHashValue tokenHashValue;
			for (;;)
			{
				tokenHashValue = hashTable[num2];
				if (tokenHashValue == null)
				{
					break;
				}
				if (str.Length >= tokenHashValue.tokenString.Length && this.CompareStringIgnoreCaseOptimized(str, 0, tokenHashValue.tokenString.Length, tokenHashValue.tokenString, 0, tokenHashValue.tokenString.Length))
				{
					goto Block_6;
				}
				num++;
				num2 += num3;
				if (num2 >= 199)
				{
					num2 -= 199;
				}
				if (num >= 199)
				{
					return;
				}
			}
			hashTable[num2] = new DateTimeFormatInfo.TokenHashValue(str, tokenType, tokenValue);
			return;
			Block_6:
			if (str.Length > tokenHashValue.tokenString.Length)
			{
				this.InsertAtCurrentHashNode(hashTable, str, c, tokenType, tokenValue, num, num2, num3);
				return;
			}
			int tokenType2 = (int)tokenHashValue.tokenType;
			if (((tokenType2 & 255) == 0 && (tokenType & TokenType.RegularTokenMask) != (TokenType)0) || ((tokenType2 & 65280) == 0 && (tokenType & TokenType.SeparatorTokenMask) != (TokenType)0))
			{
				tokenHashValue.tokenType |= tokenType;
				if (tokenValue != 0)
				{
					tokenHashValue.tokenValue = tokenValue;
				}
			}
			return;
		}

		// Token: 0x06005B3E RID: 23358 RVA: 0x00136612 File Offset: 0x00134812
		private bool CompareStringIgnoreCaseOptimized(string string1, int offset1, int length1, string string2, int offset2, int length2)
		{
			return (length1 == 1 && length2 == 1 && string1[offset1] == string2[offset2]) || this.Culture.CompareInfo.Compare(string1, offset1, length1, string2, offset2, length2, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x06005B3F RID: 23359 RVA: 0x0013664E File Offset: 0x0013484E
		// Note: this type is marked as 'beforefieldinit'.
		static DateTimeFormatInfo()
		{
		}

		// Token: 0x04003618 RID: 13848
		private static volatile DateTimeFormatInfo s_invariantInfo;

		// Token: 0x04003619 RID: 13849
		[NonSerialized]
		private CultureData _cultureData;

		// Token: 0x0400361A RID: 13850
		private string _name;

		// Token: 0x0400361B RID: 13851
		[NonSerialized]
		private string _langName;

		// Token: 0x0400361C RID: 13852
		[NonSerialized]
		private CompareInfo _compareInfo;

		// Token: 0x0400361D RID: 13853
		[NonSerialized]
		private CultureInfo _cultureInfo;

		// Token: 0x0400361E RID: 13854
		private string amDesignator;

		// Token: 0x0400361F RID: 13855
		private string pmDesignator;

		// Token: 0x04003620 RID: 13856
		private string dateSeparator;

		// Token: 0x04003621 RID: 13857
		private string generalShortTimePattern;

		// Token: 0x04003622 RID: 13858
		private string generalLongTimePattern;

		// Token: 0x04003623 RID: 13859
		private string timeSeparator;

		// Token: 0x04003624 RID: 13860
		private string monthDayPattern;

		// Token: 0x04003625 RID: 13861
		private string dateTimeOffsetPattern;

		// Token: 0x04003626 RID: 13862
		private const string rfc1123Pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";

		// Token: 0x04003627 RID: 13863
		private const string sortableDateTimePattern = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";

		// Token: 0x04003628 RID: 13864
		private const string universalSortableDateTimePattern = "yyyy'-'MM'-'dd HH':'mm':'ss'Z'";

		// Token: 0x04003629 RID: 13865
		private Calendar calendar;

		// Token: 0x0400362A RID: 13866
		private int firstDayOfWeek = -1;

		// Token: 0x0400362B RID: 13867
		private int calendarWeekRule = -1;

		// Token: 0x0400362C RID: 13868
		private string fullDateTimePattern;

		// Token: 0x0400362D RID: 13869
		private string[] abbreviatedDayNames;

		// Token: 0x0400362E RID: 13870
		private string[] m_superShortDayNames;

		// Token: 0x0400362F RID: 13871
		private string[] dayNames;

		// Token: 0x04003630 RID: 13872
		private string[] abbreviatedMonthNames;

		// Token: 0x04003631 RID: 13873
		private string[] monthNames;

		// Token: 0x04003632 RID: 13874
		private string[] genitiveMonthNames;

		// Token: 0x04003633 RID: 13875
		private string[] m_genitiveAbbreviatedMonthNames;

		// Token: 0x04003634 RID: 13876
		private string[] leapYearMonthNames;

		// Token: 0x04003635 RID: 13877
		private string longDatePattern;

		// Token: 0x04003636 RID: 13878
		private string shortDatePattern;

		// Token: 0x04003637 RID: 13879
		private string yearMonthPattern;

		// Token: 0x04003638 RID: 13880
		private string longTimePattern;

		// Token: 0x04003639 RID: 13881
		private string shortTimePattern;

		// Token: 0x0400363A RID: 13882
		private string[] allYearMonthPatterns;

		// Token: 0x0400363B RID: 13883
		private string[] allShortDatePatterns;

		// Token: 0x0400363C RID: 13884
		private string[] allLongDatePatterns;

		// Token: 0x0400363D RID: 13885
		private string[] allShortTimePatterns;

		// Token: 0x0400363E RID: 13886
		private string[] allLongTimePatterns;

		// Token: 0x0400363F RID: 13887
		private string[] m_eraNames;

		// Token: 0x04003640 RID: 13888
		private string[] m_abbrevEraNames;

		// Token: 0x04003641 RID: 13889
		private string[] m_abbrevEnglishEraNames;

		// Token: 0x04003642 RID: 13890
		private CalendarId[] optionalCalendars;

		// Token: 0x04003643 RID: 13891
		private const int DEFAULT_ALL_DATETIMES_SIZE = 132;

		// Token: 0x04003644 RID: 13892
		internal bool _isReadOnly;

		// Token: 0x04003645 RID: 13893
		private DateTimeFormatFlags formatFlags = DateTimeFormatFlags.NotInitialized;

		// Token: 0x04003646 RID: 13894
		private static readonly char[] s_monthSpaces = new char[] { ' ', '\u00a0' };

		// Token: 0x04003647 RID: 13895
		internal const string RoundtripFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";

		// Token: 0x04003648 RID: 13896
		internal const string RoundtripDateTimeUnfixed = "yyyy'-'MM'-'ddTHH':'mm':'ss zzz";

		// Token: 0x04003649 RID: 13897
		private string _fullTimeSpanPositivePattern;

		// Token: 0x0400364A RID: 13898
		private string _fullTimeSpanNegativePattern;

		// Token: 0x0400364B RID: 13899
		internal const DateTimeStyles InvalidDateTimeStyles = ~(DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeLocal | DateTimeStyles.AssumeUniversal | DateTimeStyles.RoundtripKind);

		// Token: 0x0400364C RID: 13900
		[NonSerialized]
		private DateTimeFormatInfo.TokenHashValue[] _dtfiTokenHash;

		// Token: 0x0400364D RID: 13901
		private const int TOKEN_HASH_SIZE = 199;

		// Token: 0x0400364E RID: 13902
		private const int SECOND_PRIME = 197;

		// Token: 0x0400364F RID: 13903
		private const string dateSeparatorOrTimeZoneOffset = "-";

		// Token: 0x04003650 RID: 13904
		private const string invariantDateSeparator = "/";

		// Token: 0x04003651 RID: 13905
		private const string invariantTimeSeparator = ":";

		// Token: 0x04003652 RID: 13906
		internal const string IgnorablePeriod = ".";

		// Token: 0x04003653 RID: 13907
		internal const string IgnorableComma = ",";

		// Token: 0x04003654 RID: 13908
		internal const string CJKYearSuff = "年";

		// Token: 0x04003655 RID: 13909
		internal const string CJKMonthSuff = "月";

		// Token: 0x04003656 RID: 13910
		internal const string CJKDaySuff = "日";

		// Token: 0x04003657 RID: 13911
		internal const string KoreanYearSuff = "년";

		// Token: 0x04003658 RID: 13912
		internal const string KoreanMonthSuff = "월";

		// Token: 0x04003659 RID: 13913
		internal const string KoreanDaySuff = "일";

		// Token: 0x0400365A RID: 13914
		internal const string KoreanHourSuff = "시";

		// Token: 0x0400365B RID: 13915
		internal const string KoreanMinuteSuff = "분";

		// Token: 0x0400365C RID: 13916
		internal const string KoreanSecondSuff = "초";

		// Token: 0x0400365D RID: 13917
		internal const string CJKHourSuff = "時";

		// Token: 0x0400365E RID: 13918
		internal const string ChineseHourSuff = "时";

		// Token: 0x0400365F RID: 13919
		internal const string CJKMinuteSuff = "分";

		// Token: 0x04003660 RID: 13920
		internal const string CJKSecondSuff = "秒";

		// Token: 0x04003661 RID: 13921
		internal const string JapaneseEraStart = "元";

		// Token: 0x04003662 RID: 13922
		internal const string LocalTimeMark = "T";

		// Token: 0x04003663 RID: 13923
		internal const string GMTName = "GMT";

		// Token: 0x04003664 RID: 13924
		internal const string ZuluName = "Z";

		// Token: 0x04003665 RID: 13925
		internal const string KoreanLangName = "ko";

		// Token: 0x04003666 RID: 13926
		internal const string JapaneseLangName = "ja";

		// Token: 0x04003667 RID: 13927
		internal const string EnglishLangName = "en";

		// Token: 0x04003668 RID: 13928
		private static volatile DateTimeFormatInfo s_jajpDTFI;

		// Token: 0x04003669 RID: 13929
		private static volatile DateTimeFormatInfo s_zhtwDTFI;

		// Token: 0x020009B5 RID: 2485
		internal class TokenHashValue
		{
			// Token: 0x06005B40 RID: 23360 RVA: 0x00136668 File Offset: 0x00134868
			internal TokenHashValue(string tokenString, TokenType tokenType, int tokenValue)
			{
				this.tokenString = tokenString;
				this.tokenType = tokenType;
				this.tokenValue = tokenValue;
			}

			// Token: 0x0400366A RID: 13930
			internal string tokenString;

			// Token: 0x0400366B RID: 13931
			internal TokenType tokenType;

			// Token: 0x0400366C RID: 13932
			internal int tokenValue;
		}
	}
}
