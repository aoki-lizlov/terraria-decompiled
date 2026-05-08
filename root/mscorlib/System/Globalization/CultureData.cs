using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Globalization
{
	// Token: 0x020009F8 RID: 2552
	[StructLayout(LayoutKind.Sequential)]
	internal class CultureData
	{
		// Token: 0x06005EC2 RID: 24258 RVA: 0x00143D27 File Offset: 0x00141F27
		private CultureData(string name)
		{
			this.sRealName = name;
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06005EC3 RID: 24259 RVA: 0x00143D38 File Offset: 0x00141F38
		public static CultureData Invariant
		{
			get
			{
				if (CultureData.s_Invariant == null)
				{
					CultureData cultureData = new CultureData("");
					cultureData.sISO639Language = "iv";
					cultureData.sAM1159 = "AM";
					cultureData.sPM2359 = "PM";
					cultureData.sTimeSeparator = ":";
					cultureData.saLongTimes = new string[] { "HH:mm:ss" };
					cultureData.saShortTimes = new string[] { "HH:mm", "hh:mm tt", "H:mm", "h:mm tt" };
					cultureData.iFirstDayOfWeek = 0;
					cultureData.iFirstWeekOfYear = 0;
					cultureData.waCalendars = new int[] { 1 };
					cultureData.calendars = new CalendarData[23];
					cultureData.calendars[0] = CalendarData.Invariant;
					cultureData.iDefaultAnsiCodePage = 1252;
					cultureData.iDefaultOemCodePage = 437;
					cultureData.iDefaultMacCodePage = 10000;
					cultureData.iDefaultEbcdicCodePage = 37;
					cultureData.sListSeparator = ",";
					Interlocked.CompareExchange<CultureData>(ref CultureData.s_Invariant, cultureData, null);
				}
				return CultureData.s_Invariant;
			}
		}

		// Token: 0x06005EC4 RID: 24260 RVA: 0x00143E4C File Offset: 0x0014204C
		public static CultureData GetCultureData(string cultureName, bool useUserOverride)
		{
			CultureData cultureData;
			try
			{
				cultureData = new CultureInfo(cultureName, useUserOverride).m_cultureData;
			}
			catch
			{
				cultureData = null;
			}
			return cultureData;
		}

		// Token: 0x06005EC5 RID: 24261 RVA: 0x00143E80 File Offset: 0x00142080
		public static CultureData GetCultureData(string cultureName, bool useUserOverride, int datetimeIndex, int calendarId, int numberIndex, string iso2lang, int ansiCodePage, int oemCodePage, int macCodePage, int ebcdicCodePage, bool rightToLeft, string listSeparator)
		{
			if (string.IsNullOrEmpty(cultureName))
			{
				return CultureData.Invariant;
			}
			CultureData cultureData = new CultureData(cultureName);
			cultureData.fill_culture_data(datetimeIndex);
			cultureData.bUseOverrides = useUserOverride;
			cultureData.calendarId = calendarId;
			cultureData.numberIndex = numberIndex;
			cultureData.sISO639Language = iso2lang;
			cultureData.iDefaultAnsiCodePage = ansiCodePage;
			cultureData.iDefaultOemCodePage = oemCodePage;
			cultureData.iDefaultMacCodePage = macCodePage;
			cultureData.iDefaultEbcdicCodePage = ebcdicCodePage;
			cultureData.isRightToLeft = rightToLeft;
			cultureData.sListSeparator = listSeparator;
			return cultureData;
		}

		// Token: 0x06005EC6 RID: 24262 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		internal static CultureData GetCultureData(int culture, bool bUseUserOverride)
		{
			return null;
		}

		// Token: 0x06005EC7 RID: 24263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void fill_culture_data(int datetimeIndex);

		// Token: 0x06005EC8 RID: 24264 RVA: 0x00143EF8 File Offset: 0x001420F8
		public CalendarData GetCalendar(int calendarId)
		{
			int num = calendarId - 1;
			if (this.calendars == null)
			{
				this.calendars = new CalendarData[23];
			}
			CalendarData calendarData = this.calendars[num];
			if (calendarData == null)
			{
				calendarData = new CalendarData(this.sRealName, calendarId, this.bUseOverrides);
				this.calendars[num] = calendarData;
			}
			return calendarData;
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06005EC9 RID: 24265 RVA: 0x00143F47 File Offset: 0x00142147
		internal string[] LongTimes
		{
			get
			{
				return this.saLongTimes;
			}
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06005ECA RID: 24266 RVA: 0x00143F51 File Offset: 0x00142151
		internal string[] ShortTimes
		{
			get
			{
				return this.saShortTimes;
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06005ECB RID: 24267 RVA: 0x00143F5B File Offset: 0x0014215B
		internal string SISO639LANGNAME
		{
			get
			{
				return this.sISO639Language;
			}
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06005ECC RID: 24268 RVA: 0x00143F63 File Offset: 0x00142163
		internal int IFIRSTDAYOFWEEK
		{
			get
			{
				return this.iFirstDayOfWeek;
			}
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06005ECD RID: 24269 RVA: 0x00143F6B File Offset: 0x0014216B
		internal int IFIRSTWEEKOFYEAR
		{
			get
			{
				return this.iFirstWeekOfYear;
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06005ECE RID: 24270 RVA: 0x00143F73 File Offset: 0x00142173
		internal string SAM1159
		{
			get
			{
				return this.sAM1159;
			}
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06005ECF RID: 24271 RVA: 0x00143F7B File Offset: 0x0014217B
		internal string SPM2359
		{
			get
			{
				return this.sPM2359;
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06005ED0 RID: 24272 RVA: 0x00143F83 File Offset: 0x00142183
		internal string TimeSeparator
		{
			get
			{
				return this.sTimeSeparator;
			}
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06005ED1 RID: 24273 RVA: 0x00143F8C File Offset: 0x0014218C
		internal int[] CalendarIds
		{
			get
			{
				if (this.waCalendars == null)
				{
					string text = this.sISO639Language;
					if (!(text == "ja"))
					{
						if (!(text == "zh"))
						{
							if (!(text == "he"))
							{
								this.waCalendars = new int[] { this.calendarId };
							}
							else
							{
								this.waCalendars = new int[] { this.calendarId, 8 };
							}
						}
						else
						{
							this.waCalendars = new int[] { this.calendarId, 4 };
						}
					}
					else
					{
						this.waCalendars = new int[] { this.calendarId, 3 };
					}
				}
				return this.waCalendars;
			}
		}

		// Token: 0x06005ED2 RID: 24274 RVA: 0x0014404C File Offset: 0x0014224C
		internal CalendarId[] GetCalendarIds()
		{
			CalendarId[] array = new CalendarId[this.CalendarIds.Length];
			for (int i = 0; i < this.CalendarIds.Length; i++)
			{
				array[i] = (CalendarId)this.CalendarIds[i];
			}
			return array;
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06005ED3 RID: 24275 RVA: 0x00144087 File Offset: 0x00142287
		internal bool IsInvariantCulture
		{
			get
			{
				return string.IsNullOrEmpty(this.sRealName);
			}
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06005ED4 RID: 24276 RVA: 0x00144094 File Offset: 0x00142294
		internal string CultureName
		{
			get
			{
				return this.sRealName;
			}
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06005ED5 RID: 24277 RVA: 0x00061F15 File Offset: 0x00060115
		internal string SCOMPAREINFO
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06005ED6 RID: 24278 RVA: 0x00144094 File Offset: 0x00142294
		internal string STEXTINFO
		{
			get
			{
				return this.sRealName;
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06005ED7 RID: 24279 RVA: 0x0000408A File Offset: 0x0000228A
		internal int ILANGUAGE
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06005ED8 RID: 24280 RVA: 0x0014409C File Offset: 0x0014229C
		internal int IDEFAULTANSICODEPAGE
		{
			get
			{
				return this.iDefaultAnsiCodePage;
			}
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06005ED9 RID: 24281 RVA: 0x001440A4 File Offset: 0x001422A4
		internal int IDEFAULTOEMCODEPAGE
		{
			get
			{
				return this.iDefaultOemCodePage;
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06005EDA RID: 24282 RVA: 0x001440AC File Offset: 0x001422AC
		internal int IDEFAULTMACCODEPAGE
		{
			get
			{
				return this.iDefaultMacCodePage;
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06005EDB RID: 24283 RVA: 0x001440B4 File Offset: 0x001422B4
		internal int IDEFAULTEBCDICCODEPAGE
		{
			get
			{
				return this.iDefaultEbcdicCodePage;
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06005EDC RID: 24284 RVA: 0x001440BC File Offset: 0x001422BC
		internal bool IsRightToLeft
		{
			get
			{
				return this.isRightToLeft;
			}
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06005EDD RID: 24285 RVA: 0x001440C4 File Offset: 0x001422C4
		internal string SLIST
		{
			get
			{
				return this.sListSeparator;
			}
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06005EDE RID: 24286 RVA: 0x001440CC File Offset: 0x001422CC
		internal bool UseUserOverride
		{
			get
			{
				return this.bUseOverrides;
			}
		}

		// Token: 0x06005EDF RID: 24287 RVA: 0x001440D4 File Offset: 0x001422D4
		internal string CalendarName(int calendarId)
		{
			return this.GetCalendar(calendarId).sNativeName;
		}

		// Token: 0x06005EE0 RID: 24288 RVA: 0x001440E2 File Offset: 0x001422E2
		internal string[] EraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saEraNames;
		}

		// Token: 0x06005EE1 RID: 24289 RVA: 0x001440F0 File Offset: 0x001422F0
		internal string[] AbbrevEraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevEraNames;
		}

		// Token: 0x06005EE2 RID: 24290 RVA: 0x001440FE File Offset: 0x001422FE
		internal string[] AbbreviatedEnglishEraNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevEnglishEraNames;
		}

		// Token: 0x06005EE3 RID: 24291 RVA: 0x0014410C File Offset: 0x0014230C
		internal string[] ShortDates(int calendarId)
		{
			return this.GetCalendar(calendarId).saShortDates;
		}

		// Token: 0x06005EE4 RID: 24292 RVA: 0x0014411A File Offset: 0x0014231A
		internal string[] LongDates(int calendarId)
		{
			return this.GetCalendar(calendarId).saLongDates;
		}

		// Token: 0x06005EE5 RID: 24293 RVA: 0x00144128 File Offset: 0x00142328
		internal string[] YearMonths(int calendarId)
		{
			return this.GetCalendar(calendarId).saYearMonths;
		}

		// Token: 0x06005EE6 RID: 24294 RVA: 0x00144136 File Offset: 0x00142336
		internal string[] DayNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saDayNames;
		}

		// Token: 0x06005EE7 RID: 24295 RVA: 0x00144144 File Offset: 0x00142344
		internal string[] AbbreviatedDayNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevDayNames;
		}

		// Token: 0x06005EE8 RID: 24296 RVA: 0x00144152 File Offset: 0x00142352
		internal string[] SuperShortDayNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saSuperShortDayNames;
		}

		// Token: 0x06005EE9 RID: 24297 RVA: 0x00144160 File Offset: 0x00142360
		internal string[] MonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saMonthNames;
		}

		// Token: 0x06005EEA RID: 24298 RVA: 0x0014416E File Offset: 0x0014236E
		internal string[] GenitiveMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saMonthGenitiveNames;
		}

		// Token: 0x06005EEB RID: 24299 RVA: 0x0014417C File Offset: 0x0014237C
		internal string[] AbbreviatedMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevMonthNames;
		}

		// Token: 0x06005EEC RID: 24300 RVA: 0x0014418A File Offset: 0x0014238A
		internal string[] AbbreviatedGenitiveMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saAbbrevMonthGenitiveNames;
		}

		// Token: 0x06005EED RID: 24301 RVA: 0x00144198 File Offset: 0x00142398
		internal string[] LeapYearMonthNames(int calendarId)
		{
			return this.GetCalendar(calendarId).saLeapYearMonthNames;
		}

		// Token: 0x06005EEE RID: 24302 RVA: 0x001441A6 File Offset: 0x001423A6
		internal string MonthDay(int calendarId)
		{
			return this.GetCalendar(calendarId).sMonthDay;
		}

		// Token: 0x06005EEF RID: 24303 RVA: 0x001441B4 File Offset: 0x001423B4
		internal string DateSeparator(int calendarId)
		{
			if (calendarId == 3 && !AppContextSwitches.EnforceLegacyJapaneseDateParsing)
			{
				return "/";
			}
			return CultureData.GetDateSeparator(this.ShortDates(calendarId)[0]);
		}

		// Token: 0x06005EF0 RID: 24304 RVA: 0x001441D5 File Offset: 0x001423D5
		private static string GetDateSeparator(string format)
		{
			return CultureData.GetSeparator(format, "dyM");
		}

		// Token: 0x06005EF1 RID: 24305 RVA: 0x001441E4 File Offset: 0x001423E4
		private static string GetSeparator(string format, string timeParts)
		{
			int num = CultureData.IndexOfTimePart(format, 0, timeParts);
			if (num != -1)
			{
				char c = format[num];
				do
				{
					num++;
				}
				while (num < format.Length && format[num] == c);
				int num2 = num;
				if (num2 < format.Length)
				{
					int num3 = CultureData.IndexOfTimePart(format, num2, timeParts);
					if (num3 != -1)
					{
						return CultureData.UnescapeNlsString(format, num2, num3 - 1);
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x00144248 File Offset: 0x00142448
		private static int IndexOfTimePart(string format, int startIndex, string timeParts)
		{
			bool flag = false;
			for (int i = startIndex; i < format.Length; i++)
			{
				if (!flag && timeParts.IndexOf(format[i]) != -1)
				{
					return i;
				}
				char c = format[i];
				if (c != '\'')
				{
					if (c == '\\' && i + 1 < format.Length)
					{
						i++;
						char c2 = format[i];
						if (c2 != '\'' && c2 != '\\')
						{
							i--;
						}
					}
				}
				else
				{
					flag = !flag;
				}
			}
			return -1;
		}

		// Token: 0x06005EF3 RID: 24307 RVA: 0x001442BC File Offset: 0x001424BC
		private static string UnescapeNlsString(string str, int start, int end)
		{
			StringBuilder stringBuilder = null;
			int num = start;
			while (num < str.Length && num <= end)
			{
				char c = str[num];
				if (c != '\'')
				{
					if (c != '\\')
					{
						if (stringBuilder != null)
						{
							stringBuilder.Append(str[num]);
						}
					}
					else
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(str, start, num - start, str.Length);
						}
						num++;
						if (num < str.Length)
						{
							stringBuilder.Append(str[num]);
						}
					}
				}
				else if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(str, start, num - start, str.Length);
				}
				num++;
			}
			if (stringBuilder == null)
			{
				return str.Substring(start, end - start + 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005EF4 RID: 24308 RVA: 0x000025CE File Offset: 0x000007CE
		internal static string[] ReescapeWin32Strings(string[] array)
		{
			return array;
		}

		// Token: 0x06005EF5 RID: 24309 RVA: 0x000025CE File Offset: 0x000007CE
		internal static string ReescapeWin32String(string str)
		{
			return str;
		}

		// Token: 0x06005EF6 RID: 24310 RVA: 0x0000408A File Offset: 0x0000228A
		internal static bool IsCustomCultureId(int cultureId)
		{
			return false;
		}

		// Token: 0x06005EF7 RID: 24311 RVA: 0x00144364 File Offset: 0x00142564
		private unsafe static int strlen(byte* s)
		{
			int num = 0;
			while (s[num] != 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06005EF8 RID: 24312 RVA: 0x00144380 File Offset: 0x00142580
		private unsafe static string idx2string(byte* data, int idx)
		{
			return Encoding.UTF8.GetString(data + idx, CultureData.strlen(data + idx));
		}

		// Token: 0x06005EF9 RID: 24313 RVA: 0x00144397 File Offset: 0x00142597
		private int[] create_group_sizes_array(int gs0, int gs1)
		{
			if (gs0 == -1)
			{
				return new int[0];
			}
			if (gs1 != -1)
			{
				return new int[] { gs0, gs1 };
			}
			return new int[] { gs0 };
		}

		// Token: 0x06005EFA RID: 24314 RVA: 0x001443C4 File Offset: 0x001425C4
		internal unsafe void GetNFIValues(NumberFormatInfo nfi)
		{
			if (!this.IsInvariantCulture)
			{
				CultureData.NumberFormatEntryManaged numberFormatEntryManaged = default(CultureData.NumberFormatEntryManaged);
				byte* ptr = CultureData.fill_number_data(this.numberIndex, ref numberFormatEntryManaged);
				nfi.currencyGroupSizes = this.create_group_sizes_array(numberFormatEntryManaged.currency_group_sizes0, numberFormatEntryManaged.currency_group_sizes1);
				nfi.numberGroupSizes = this.create_group_sizes_array(numberFormatEntryManaged.number_group_sizes0, numberFormatEntryManaged.number_group_sizes1);
				nfi.NaNSymbol = CultureData.idx2string(ptr, numberFormatEntryManaged.nan_symbol);
				nfi.currencyDecimalDigits = numberFormatEntryManaged.currency_decimal_digits;
				nfi.currencyDecimalSeparator = CultureData.idx2string(ptr, numberFormatEntryManaged.currency_decimal_separator);
				nfi.currencyGroupSeparator = CultureData.idx2string(ptr, numberFormatEntryManaged.currency_group_separator);
				nfi.currencyNegativePattern = numberFormatEntryManaged.currency_negative_pattern;
				nfi.currencyPositivePattern = numberFormatEntryManaged.currency_positive_pattern;
				nfi.currencySymbol = CultureData.idx2string(ptr, numberFormatEntryManaged.currency_symbol);
				nfi.negativeInfinitySymbol = CultureData.idx2string(ptr, numberFormatEntryManaged.negative_infinity_symbol);
				nfi.negativeSign = CultureData.idx2string(ptr, numberFormatEntryManaged.negative_sign);
				nfi.numberDecimalDigits = numberFormatEntryManaged.number_decimal_digits;
				nfi.numberDecimalSeparator = CultureData.idx2string(ptr, numberFormatEntryManaged.number_decimal_separator);
				nfi.numberGroupSeparator = CultureData.idx2string(ptr, numberFormatEntryManaged.number_group_separator);
				nfi.numberNegativePattern = numberFormatEntryManaged.number_negative_pattern;
				nfi.perMilleSymbol = CultureData.idx2string(ptr, numberFormatEntryManaged.per_mille_symbol);
				nfi.percentNegativePattern = numberFormatEntryManaged.percent_negative_pattern;
				nfi.percentPositivePattern = numberFormatEntryManaged.percent_positive_pattern;
				nfi.percentSymbol = CultureData.idx2string(ptr, numberFormatEntryManaged.percent_symbol);
				nfi.positiveInfinitySymbol = CultureData.idx2string(ptr, numberFormatEntryManaged.positive_infinity_symbol);
				nfi.positiveSign = CultureData.idx2string(ptr, numberFormatEntryManaged.positive_sign);
			}
			nfi.percentDecimalDigits = nfi.numberDecimalDigits;
			nfi.percentDecimalSeparator = nfi.numberDecimalSeparator;
			nfi.percentGroupSizes = nfi.numberGroupSizes;
			nfi.percentGroupSeparator = nfi.numberGroupSeparator;
		}

		// Token: 0x06005EFB RID: 24315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern byte* fill_number_data(int index, ref CultureData.NumberFormatEntryManaged nfe);

		// Token: 0x040038F9 RID: 14585
		private string sAM1159;

		// Token: 0x040038FA RID: 14586
		private string sPM2359;

		// Token: 0x040038FB RID: 14587
		private string sTimeSeparator;

		// Token: 0x040038FC RID: 14588
		private volatile string[] saLongTimes;

		// Token: 0x040038FD RID: 14589
		private volatile string[] saShortTimes;

		// Token: 0x040038FE RID: 14590
		private int iFirstDayOfWeek;

		// Token: 0x040038FF RID: 14591
		private int iFirstWeekOfYear;

		// Token: 0x04003900 RID: 14592
		private volatile int[] waCalendars;

		// Token: 0x04003901 RID: 14593
		private CalendarData[] calendars;

		// Token: 0x04003902 RID: 14594
		private string sISO639Language;

		// Token: 0x04003903 RID: 14595
		private readonly string sRealName;

		// Token: 0x04003904 RID: 14596
		private bool bUseOverrides;

		// Token: 0x04003905 RID: 14597
		private int calendarId;

		// Token: 0x04003906 RID: 14598
		private int numberIndex;

		// Token: 0x04003907 RID: 14599
		private int iDefaultAnsiCodePage;

		// Token: 0x04003908 RID: 14600
		private int iDefaultOemCodePage;

		// Token: 0x04003909 RID: 14601
		private int iDefaultMacCodePage;

		// Token: 0x0400390A RID: 14602
		private int iDefaultEbcdicCodePage;

		// Token: 0x0400390B RID: 14603
		private bool isRightToLeft;

		// Token: 0x0400390C RID: 14604
		private string sListSeparator;

		// Token: 0x0400390D RID: 14605
		private static CultureData s_Invariant;

		// Token: 0x020009F9 RID: 2553
		internal struct NumberFormatEntryManaged
		{
			// Token: 0x0400390E RID: 14606
			internal int currency_decimal_digits;

			// Token: 0x0400390F RID: 14607
			internal int currency_decimal_separator;

			// Token: 0x04003910 RID: 14608
			internal int currency_group_separator;

			// Token: 0x04003911 RID: 14609
			internal int currency_group_sizes0;

			// Token: 0x04003912 RID: 14610
			internal int currency_group_sizes1;

			// Token: 0x04003913 RID: 14611
			internal int currency_negative_pattern;

			// Token: 0x04003914 RID: 14612
			internal int currency_positive_pattern;

			// Token: 0x04003915 RID: 14613
			internal int currency_symbol;

			// Token: 0x04003916 RID: 14614
			internal int nan_symbol;

			// Token: 0x04003917 RID: 14615
			internal int negative_infinity_symbol;

			// Token: 0x04003918 RID: 14616
			internal int negative_sign;

			// Token: 0x04003919 RID: 14617
			internal int number_decimal_digits;

			// Token: 0x0400391A RID: 14618
			internal int number_decimal_separator;

			// Token: 0x0400391B RID: 14619
			internal int number_group_separator;

			// Token: 0x0400391C RID: 14620
			internal int number_group_sizes0;

			// Token: 0x0400391D RID: 14621
			internal int number_group_sizes1;

			// Token: 0x0400391E RID: 14622
			internal int number_negative_pattern;

			// Token: 0x0400391F RID: 14623
			internal int per_mille_symbol;

			// Token: 0x04003920 RID: 14624
			internal int percent_negative_pattern;

			// Token: 0x04003921 RID: 14625
			internal int percent_positive_pattern;

			// Token: 0x04003922 RID: 14626
			internal int percent_symbol;

			// Token: 0x04003923 RID: 14627
			internal int positive_infinity_symbol;

			// Token: 0x04003924 RID: 14628
			internal int positive_sign;
		}
	}
}
