using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020009DD RID: 2525
	[StructLayout(LayoutKind.Sequential)]
	internal class CalendarData
	{
		// Token: 0x06005C48 RID: 23624 RVA: 0x0013BDE3 File Offset: 0x00139FE3
		private CalendarData()
		{
		}

		// Token: 0x06005C49 RID: 23625 RVA: 0x0013BDF8 File Offset: 0x00139FF8
		static CalendarData()
		{
			CalendarData calendarData = new CalendarData();
			calendarData.sNativeName = "Gregorian Calendar";
			calendarData.iTwoDigitYearMax = 2029;
			calendarData.iCurrentEra = 1;
			calendarData.saShortDates = new string[] { "MM/dd/yyyy", "yyyy-MM-dd" };
			calendarData.saLongDates = new string[] { "dddd, dd MMMM yyyy" };
			calendarData.saYearMonths = new string[] { "yyyy MMMM" };
			calendarData.sMonthDay = "MMMM dd";
			calendarData.saEraNames = new string[] { "A.D." };
			calendarData.saAbbrevEraNames = new string[] { "AD" };
			calendarData.saAbbrevEnglishEraNames = new string[] { "AD" };
			calendarData.saDayNames = new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
			calendarData.saAbbrevDayNames = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
			calendarData.saSuperShortDayNames = new string[] { "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa" };
			calendarData.saMonthNames = new string[]
			{
				"January",
				"February",
				"March",
				"April",
				"May",
				"June",
				"July",
				"August",
				"September",
				"October",
				"November",
				"December",
				string.Empty
			};
			calendarData.saAbbrevMonthNames = new string[]
			{
				"Jan",
				"Feb",
				"Mar",
				"Apr",
				"May",
				"Jun",
				"Jul",
				"Aug",
				"Sep",
				"Oct",
				"Nov",
				"Dec",
				string.Empty
			};
			calendarData.saMonthGenitiveNames = calendarData.saMonthNames;
			calendarData.saAbbrevMonthGenitiveNames = calendarData.saAbbrevMonthNames;
			calendarData.saLeapYearMonthNames = calendarData.saMonthNames;
			calendarData.bUseUserOverrides = false;
			CalendarData.Invariant = calendarData;
		}

		// Token: 0x06005C4A RID: 23626 RVA: 0x0013C194 File Offset: 0x0013A394
		internal CalendarData(string localeName, int calendarId, bool bUseUserOverrides)
		{
			this.bUseUserOverrides = bUseUserOverrides;
			if (!CalendarData.nativeGetCalendarData(this, localeName, calendarId))
			{
				if (this.sNativeName == null)
				{
					this.sNativeName = string.Empty;
				}
				if (this.saShortDates == null)
				{
					this.saShortDates = CalendarData.Invariant.saShortDates;
				}
				if (this.saYearMonths == null)
				{
					this.saYearMonths = CalendarData.Invariant.saYearMonths;
				}
				if (this.saLongDates == null)
				{
					this.saLongDates = CalendarData.Invariant.saLongDates;
				}
				if (this.sMonthDay == null)
				{
					this.sMonthDay = CalendarData.Invariant.sMonthDay;
				}
				if (this.saEraNames == null)
				{
					this.saEraNames = CalendarData.Invariant.saEraNames;
				}
				if (this.saAbbrevEraNames == null)
				{
					this.saAbbrevEraNames = CalendarData.Invariant.saAbbrevEraNames;
				}
				if (this.saAbbrevEnglishEraNames == null)
				{
					this.saAbbrevEnglishEraNames = CalendarData.Invariant.saAbbrevEnglishEraNames;
				}
				if (this.saDayNames == null)
				{
					this.saDayNames = CalendarData.Invariant.saDayNames;
				}
				if (this.saAbbrevDayNames == null)
				{
					this.saAbbrevDayNames = CalendarData.Invariant.saAbbrevDayNames;
				}
				if (this.saSuperShortDayNames == null)
				{
					this.saSuperShortDayNames = CalendarData.Invariant.saSuperShortDayNames;
				}
				if (this.saMonthNames == null)
				{
					this.saMonthNames = CalendarData.Invariant.saMonthNames;
				}
				if (this.saAbbrevMonthNames == null)
				{
					this.saAbbrevMonthNames = CalendarData.Invariant.saAbbrevMonthNames;
				}
			}
			this.saShortDates = CultureData.ReescapeWin32Strings(this.saShortDates);
			this.saLongDates = CultureData.ReescapeWin32Strings(this.saLongDates);
			this.saYearMonths = CultureData.ReescapeWin32Strings(this.saYearMonths);
			this.sMonthDay = CultureData.ReescapeWin32String(this.sMonthDay);
			if ((ushort)calendarId == 4)
			{
				if (CultureInfo.IsTaiwanSku)
				{
					this.sNativeName = "中華民國曆";
				}
				else
				{
					this.sNativeName = string.Empty;
				}
			}
			if (this.saMonthGenitiveNames == null || string.IsNullOrEmpty(this.saMonthGenitiveNames[0]))
			{
				this.saMonthGenitiveNames = this.saMonthNames;
			}
			if (this.saAbbrevMonthGenitiveNames == null || string.IsNullOrEmpty(this.saAbbrevMonthGenitiveNames[0]))
			{
				this.saAbbrevMonthGenitiveNames = this.saAbbrevMonthNames;
			}
			if (this.saLeapYearMonthNames == null || string.IsNullOrEmpty(this.saLeapYearMonthNames[0]))
			{
				this.saLeapYearMonthNames = this.saMonthNames;
			}
			this.InitializeEraNames(localeName, calendarId);
			this.InitializeAbbreviatedEraNames(localeName, calendarId);
			if (!GlobalizationMode.Invariant && calendarId == 3)
			{
				this.saAbbrevEnglishEraNames = CalendarData.GetJapaneseEnglishEraNames();
			}
			else
			{
				this.saAbbrevEnglishEraNames = new string[] { "" };
			}
			this.iCurrentEra = this.saEraNames.Length;
		}

		// Token: 0x06005C4B RID: 23627 RVA: 0x0013C414 File Offset: 0x0013A614
		private void InitializeEraNames(string localeName, int calendarId)
		{
			switch ((ushort)calendarId)
			{
			case 1:
				if (this.saEraNames == null || this.saEraNames.Length == 0 || string.IsNullOrEmpty(this.saEraNames[0]))
				{
					this.saEraNames = new string[] { "A.D." };
					return;
				}
				return;
			case 2:
			case 13:
				this.saEraNames = new string[] { "A.D." };
				return;
			case 3:
			case 14:
				this.saEraNames = CalendarData.GetJapaneseEraNames();
				return;
			case 4:
				if (CultureInfo.IsTaiwanSku)
				{
					this.saEraNames = new string[] { "中華民國" };
					return;
				}
				this.saEraNames = new string[] { string.Empty };
				return;
			case 5:
				this.saEraNames = new string[] { "단기" };
				return;
			case 6:
			case 23:
				if (localeName == "dv-MV")
				{
					this.saEraNames = new string[] { "ހ\u07a8ޖ\u07b0ރ\u07a9" };
					return;
				}
				this.saEraNames = new string[] { "بعد الهجرة" };
				return;
			case 7:
				this.saEraNames = new string[] { "พ.ศ." };
				return;
			case 8:
				this.saEraNames = new string[] { "C.E." };
				return;
			case 9:
				this.saEraNames = new string[] { "ap. J.-C." };
				return;
			case 10:
			case 11:
			case 12:
				this.saEraNames = new string[] { "م" };
				return;
			case 22:
				if (this.saEraNames == null || this.saEraNames.Length == 0 || string.IsNullOrEmpty(this.saEraNames[0]))
				{
					this.saEraNames = new string[] { "ه.ش" };
					return;
				}
				return;
			}
			this.saEraNames = CalendarData.Invariant.saEraNames;
		}

		// Token: 0x06005C4C RID: 23628 RVA: 0x0013C5FC File Offset: 0x0013A7FC
		private static string[] GetJapaneseEraNames()
		{
			if (GlobalizationMode.Invariant)
			{
				throw new PlatformNotSupportedException();
			}
			return JapaneseCalendar.EraNames();
		}

		// Token: 0x06005C4D RID: 23629 RVA: 0x0013C610 File Offset: 0x0013A810
		private static string[] GetJapaneseEnglishEraNames()
		{
			if (GlobalizationMode.Invariant)
			{
				throw new PlatformNotSupportedException();
			}
			return JapaneseCalendar.EnglishEraNames();
		}

		// Token: 0x06005C4E RID: 23630 RVA: 0x0013C624 File Offset: 0x0013A824
		private void InitializeAbbreviatedEraNames(string localeName, int calendarId)
		{
			CalendarId calendarId2 = (CalendarId)calendarId;
			if (calendarId2 <= CalendarId.JULIAN)
			{
				switch (calendarId2)
				{
				case CalendarId.GREGORIAN:
					if (this.saAbbrevEraNames == null || this.saAbbrevEraNames.Length == 0 || string.IsNullOrEmpty(this.saAbbrevEraNames[0]))
					{
						this.saAbbrevEraNames = new string[] { "AD" };
						return;
					}
					return;
				case CalendarId.GREGORIAN_US:
					break;
				case CalendarId.JAPAN:
					goto IL_0096;
				case CalendarId.TAIWAN:
					this.saAbbrevEraNames = new string[1];
					if (this.saEraNames[0].Length == 4)
					{
						this.saAbbrevEraNames[0] = this.saEraNames[0].Substring(2, 2);
						return;
					}
					this.saAbbrevEraNames[0] = this.saEraNames[0];
					return;
				case CalendarId.KOREA:
					goto IL_0159;
				case CalendarId.HIJRI:
					goto IL_00B0;
				default:
					if (calendarId2 != CalendarId.JULIAN)
					{
						goto IL_0159;
					}
					break;
				}
				this.saAbbrevEraNames = new string[] { "AD" };
				return;
			}
			if (calendarId2 != CalendarId.JAPANESELUNISOLAR)
			{
				if (calendarId2 != CalendarId.PERSIAN)
				{
					if (calendarId2 != CalendarId.UMALQURA)
					{
						goto IL_0159;
					}
					goto IL_00B0;
				}
				else
				{
					if (this.saAbbrevEraNames == null || this.saAbbrevEraNames.Length == 0 || string.IsNullOrEmpty(this.saAbbrevEraNames[0]))
					{
						this.saAbbrevEraNames = this.saEraNames;
						return;
					}
					return;
				}
			}
			IL_0096:
			if (GlobalizationMode.Invariant)
			{
				throw new PlatformNotSupportedException();
			}
			this.saAbbrevEraNames = this.saEraNames;
			return;
			IL_00B0:
			if (localeName == "dv-MV")
			{
				this.saAbbrevEraNames = new string[] { "ހ." };
				return;
			}
			this.saAbbrevEraNames = new string[] { "هـ" };
			return;
			IL_0159:
			this.saAbbrevEraNames = this.saEraNames;
		}

		// Token: 0x06005C4F RID: 23631 RVA: 0x0013C796 File Offset: 0x0013A996
		internal static CalendarData GetCalendarData(int calendarId)
		{
			return CultureInfo.GetCultureInfo(CalendarData.CalendarIdToCultureName(calendarId)).m_cultureData.GetCalendar(calendarId);
		}

		// Token: 0x06005C50 RID: 23632 RVA: 0x0013C7B0 File Offset: 0x0013A9B0
		private static string CalendarIdToCultureName(int calendarId)
		{
			switch (calendarId)
			{
			case 2:
				return "fa-IR";
			case 3:
				return "ja-JP";
			case 4:
				return "zh-TW";
			case 5:
				return "ko-KR";
			case 6:
			case 10:
			case 23:
				return "ar-SA";
			case 7:
				return "th-TH";
			case 8:
				return "he-IL";
			case 9:
				return "ar-DZ";
			case 11:
			case 12:
				return "ar-IQ";
			}
			return "en-US";
		}

		// Token: 0x06005C51 RID: 23633 RVA: 0x0011B48C File Offset: 0x0011968C
		public static int nativeGetTwoDigitYearMax(int calID)
		{
			return -1;
		}

		// Token: 0x06005C52 RID: 23634 RVA: 0x0013C85A File Offset: 0x0013AA5A
		private static bool nativeGetCalendarData(CalendarData data, string localeName, int calendarId)
		{
			if (data.fill_calendar_data(localeName.ToLowerInvariant(), calendarId))
			{
				if ((ushort)calendarId == 8)
				{
					data.saMonthNames = CalendarData.HEBREW_MONTH_NAMES;
					data.saLeapYearMonthNames = CalendarData.HEBREW_LEAP_MONTH_NAMES;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06005C53 RID: 23635
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool fill_calendar_data(string localeName, int datetimeIndex);

		// Token: 0x040037DE RID: 14302
		internal const int MAX_CALENDARS = 23;

		// Token: 0x040037DF RID: 14303
		internal string sNativeName;

		// Token: 0x040037E0 RID: 14304
		internal string[] saShortDates;

		// Token: 0x040037E1 RID: 14305
		internal string[] saYearMonths;

		// Token: 0x040037E2 RID: 14306
		internal string[] saLongDates;

		// Token: 0x040037E3 RID: 14307
		internal string sMonthDay;

		// Token: 0x040037E4 RID: 14308
		internal string[] saEraNames;

		// Token: 0x040037E5 RID: 14309
		internal string[] saAbbrevEraNames;

		// Token: 0x040037E6 RID: 14310
		internal string[] saAbbrevEnglishEraNames;

		// Token: 0x040037E7 RID: 14311
		internal string[] saDayNames;

		// Token: 0x040037E8 RID: 14312
		internal string[] saAbbrevDayNames;

		// Token: 0x040037E9 RID: 14313
		internal string[] saSuperShortDayNames;

		// Token: 0x040037EA RID: 14314
		internal string[] saMonthNames;

		// Token: 0x040037EB RID: 14315
		internal string[] saAbbrevMonthNames;

		// Token: 0x040037EC RID: 14316
		internal string[] saMonthGenitiveNames;

		// Token: 0x040037ED RID: 14317
		internal string[] saAbbrevMonthGenitiveNames;

		// Token: 0x040037EE RID: 14318
		internal string[] saLeapYearMonthNames;

		// Token: 0x040037EF RID: 14319
		internal int iTwoDigitYearMax = 2029;

		// Token: 0x040037F0 RID: 14320
		internal int iCurrentEra;

		// Token: 0x040037F1 RID: 14321
		internal bool bUseUserOverrides;

		// Token: 0x040037F2 RID: 14322
		internal static CalendarData Invariant;

		// Token: 0x040037F3 RID: 14323
		private static string[] HEBREW_MONTH_NAMES = new string[]
		{
			"תשרי", "חשון", "כסלו", "טבת", "שבט", "אדר", "אדר ב", "ניסן", "אייר", "סיון",
			"תמוז", "אב", "אלול"
		};

		// Token: 0x040037F4 RID: 14324
		private static string[] HEBREW_LEAP_MONTH_NAMES = new string[]
		{
			"תשרי", "חשון", "כסלו", "טבת", "שבט", "אדר א", "אדר ב", "ניסן", "אייר", "סיון",
			"תמוז", "אב", "אלול"
		};
	}
}
