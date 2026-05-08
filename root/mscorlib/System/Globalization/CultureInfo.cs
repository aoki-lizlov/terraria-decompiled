using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Globalization
{
	// Token: 0x02000A00 RID: 2560
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class CultureInfo : ICloneable, IFormatProvider
	{
		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06005F0E RID: 24334 RVA: 0x00147E81 File Offset: 0x00146081
		internal CultureData _cultureData
		{
			get
			{
				return this.m_cultureData;
			}
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06005F0F RID: 24335 RVA: 0x00147E89 File Offset: 0x00146089
		internal bool _isInherited
		{
			get
			{
				return this.m_isInherited;
			}
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06005F10 RID: 24336 RVA: 0x00147E91 File Offset: 0x00146091
		public static CultureInfo InvariantCulture
		{
			get
			{
				return CultureInfo.invariant_culture_info;
			}
		}

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06005F11 RID: 24337 RVA: 0x00147E9A File Offset: 0x0014609A
		// (set) Token: 0x06005F12 RID: 24338 RVA: 0x00147EA6 File Offset: 0x001460A6
		public static CultureInfo CurrentCulture
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture;
			}
			set
			{
				Thread.CurrentThread.CurrentCulture = value;
			}
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06005F13 RID: 24339 RVA: 0x00147EB3 File Offset: 0x001460B3
		// (set) Token: 0x06005F14 RID: 24340 RVA: 0x00147EBF File Offset: 0x001460BF
		public static CultureInfo CurrentUICulture
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture;
			}
			set
			{
				Thread.CurrentThread.CurrentUICulture = value;
			}
		}

		// Token: 0x06005F15 RID: 24341 RVA: 0x00147ECC File Offset: 0x001460CC
		internal static CultureInfo ConstructCurrentCulture()
		{
			if (CultureInfo.default_current_culture != null)
			{
				return CultureInfo.default_current_culture;
			}
			if (GlobalizationMode.Invariant)
			{
				return CultureInfo.InvariantCulture;
			}
			string current_locale_name = CultureInfo.get_current_locale_name();
			CultureInfo cultureInfo = null;
			if (current_locale_name != null)
			{
				try
				{
					cultureInfo = CultureInfo.CreateSpecificCulture(current_locale_name);
				}
				catch
				{
				}
			}
			if (cultureInfo == null)
			{
				cultureInfo = CultureInfo.InvariantCulture;
			}
			else
			{
				cultureInfo.m_isReadOnly = true;
				cultureInfo.m_useUserOverride = true;
			}
			CultureInfo.default_current_culture = cultureInfo;
			return cultureInfo;
		}

		// Token: 0x06005F16 RID: 24342 RVA: 0x00147F3C File Offset: 0x0014613C
		internal static CultureInfo ConstructCurrentUICulture()
		{
			return CultureInfo.ConstructCurrentCulture();
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06005F17 RID: 24343 RVA: 0x00147F43 File Offset: 0x00146143
		internal string Territory
		{
			get
			{
				return this.territory;
			}
		}

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06005F18 RID: 24344 RVA: 0x00147F4B File Offset: 0x0014614B
		internal string _name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06005F19 RID: 24345 RVA: 0x00147F54 File Offset: 0x00146154
		[ComVisible(false)]
		public CultureTypes CultureTypes
		{
			get
			{
				CultureTypes cultureTypes = (CultureTypes)0;
				foreach (object obj in Enum.GetValues(typeof(CultureTypes)))
				{
					CultureTypes cultureTypes2 = (CultureTypes)obj;
					if (Array.IndexOf<CultureInfo>(CultureInfo.GetCultures(cultureTypes2), this) >= 0)
					{
						cultureTypes |= cultureTypes2;
					}
				}
				return cultureTypes;
			}
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x00147FC8 File Offset: 0x001461C8
		[ComVisible(false)]
		public CultureInfo GetConsoleFallbackUICulture()
		{
			string name = this.Name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1260172255U)
			{
				if (num <= 939759947U)
				{
					if (num <= 249681006U)
					{
						if (num <= 198587497U)
						{
							if (num != 64366545U)
							{
								if (num != 77939050U)
								{
									if (num != 198587497U)
									{
										goto IL_06C2;
									}
									if (!(name == "ar-SA"))
									{
										goto IL_06C2;
									}
								}
								else if (!(name == "mr-IN"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "ar-SY"))
							{
								goto IL_06C2;
							}
						}
						else if (num != 233820021U)
						{
							if (num != 236085687U)
							{
								if (num != 249681006U)
								{
									goto IL_06C2;
								}
								if (!(name == "hi-IN"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "ar-KW"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ar-EG"))
						{
							goto IL_06C2;
						}
					}
					else if (num <= 469295067U)
					{
						if (num != 419506663U)
						{
							if (num != 434712723U)
							{
								if (num != 469295067U)
								{
									goto IL_06C2;
								}
								if (!(name == "ar-AE"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "sa-IN"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ar-BH"))
						{
							goto IL_06C2;
						}
					}
					else if (num != 511763911U)
					{
						if (num != 907337542U)
						{
							if (num != 939759947U)
							{
								goto IL_06C2;
							}
							if (!(name == "ar-MA"))
							{
								goto IL_06C2;
							}
							goto IL_06B7;
						}
						else if (!(name == "ar-JO"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "vi-VN"))
					{
						goto IL_06C2;
					}
				}
				else if (num <= 1074569279U)
				{
					if (num <= 1011170994U)
					{
						if (num != 944060518U)
						{
							if (num != 944899161U)
							{
								if (num != 1011170994U)
								{
									goto IL_06C2;
								}
								if (!(name == "te"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "sa"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ta"))
						{
							goto IL_06C2;
						}
					}
					else if (num != 1011465184U)
					{
						if (num != 1070729495U)
						{
							if (num != 1074569279U)
							{
								goto IL_06C2;
							}
							if (!(name == "ar-IQ"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ar-QA"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "vi"))
					{
						goto IL_06C2;
					}
				}
				else if (num <= 1123180923U)
				{
					if (num != 1094514636U)
					{
						if (num != 1095059089U)
						{
							if (num != 1123180923U)
							{
								goto IL_06C2;
							}
							if (!(name == "ar-DZ"))
							{
								goto IL_06C2;
							}
							goto IL_06B7;
						}
						else if (!(name == "th"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "kn"))
					{
						goto IL_06C2;
					}
				}
				else if (num != 1141238470U)
				{
					if (num != 1162022470U)
					{
						if (num != 1260172255U)
						{
							goto IL_06C2;
						}
						if (!(name == "dv"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "ur"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "ar-LY"))
				{
					goto IL_06C2;
				}
			}
			else if (num <= 1756775346U)
			{
				if (num <= 1527123707U)
				{
					if (num <= 1429081278U)
					{
						if (num != 1277200137U)
						{
							if (num != 1347311754U)
							{
								if (num != 1429081278U)
								{
									goto IL_06C2;
								}
								if (!(name == "mr"))
								{
									goto IL_06C2;
								}
							}
							else if (!(name == "pa"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "gu"))
						{
							goto IL_06C2;
						}
					}
					else if (num != 1456070279U)
					{
						if (num != 1458211363U)
						{
							if (num != 1527123707U)
							{
								goto IL_06C2;
							}
							if (!(name == "ar-LB"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "gu-IN"))
						{
							goto IL_06C2;
						}
					}
					else
					{
						if (!(name == "ar-TN"))
						{
							goto IL_06C2;
						}
						goto IL_06B7;
					}
				}
				else if (num <= 1622153968U)
				{
					if (num != 1547363254U)
					{
						if (num != 1562713850U)
						{
							if (num != 1622153968U)
							{
								goto IL_06C2;
							}
							if (!(name == "kok-IN"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "ar"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "he"))
					{
						goto IL_06C2;
					}
				}
				else if (num != 1680010088U)
				{
					if (num != 1748694682U)
					{
						if (num != 1756775346U)
						{
							goto IL_06C2;
						}
						if (!(name == "ta-IN"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "hi"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "fa"))
				{
					goto IL_06C2;
				}
			}
			else if (num <= 3073845542U)
			{
				if (num <= 2153224060U)
				{
					if (num != 1846834581U)
					{
						if (num != 2046577884U)
						{
							if (num != 2153224060U)
							{
								goto IL_06C2;
							}
							if (!(name == "he-IL"))
							{
								goto IL_06C2;
							}
						}
						else if (!(name == "kok"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "dv-MV"))
					{
						goto IL_06C2;
					}
				}
				else if (num != 2902799296U)
				{
					if (num != 3060605246U)
					{
						if (num != 3073845542U)
						{
							goto IL_06C2;
						}
						if (!(name == "te-IN"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "pa-IN"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "kn-IN"))
				{
					goto IL_06C2;
				}
			}
			else if (num <= 3477219856U)
			{
				if (num != 3294142633U)
				{
					if (num != 3311105148U)
					{
						if (num != 3477219856U)
						{
							goto IL_06C2;
						}
						if (!(name == "fa-IR"))
						{
							goto IL_06C2;
						}
					}
					else if (!(name == "syr-SY"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "syr"))
				{
					goto IL_06C2;
				}
			}
			else if (num != 3957656723U)
			{
				if (num != 4027935912U)
				{
					if (num != 4091062904U)
					{
						goto IL_06C2;
					}
					if (!(name == "th-TH"))
					{
						goto IL_06C2;
					}
				}
				else if (!(name == "ur-PK"))
				{
					goto IL_06C2;
				}
			}
			else if (!(name == "ar-YE"))
			{
				goto IL_06C2;
			}
			return CultureInfo.GetCultureInfo("en");
			IL_06B7:
			return CultureInfo.GetCultureInfo("fr");
			IL_06C2:
			if ((this.CultureTypes & CultureTypes.WindowsOnlyCultures) == (CultureTypes)0)
			{
				return this;
			}
			return CultureInfo.InvariantCulture;
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06005F1B RID: 24347 RVA: 0x001486AC File Offset: 0x001468AC
		[ComVisible(false)]
		public string IetfLanguageTag
		{
			get
			{
				string name = this.Name;
				if (name == "zh-CHS")
				{
					return "zh-Hans";
				}
				if (!(name == "zh-CHT"))
				{
					return this.Name;
				}
				return "zh-Hant";
			}
		}

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06005F1C RID: 24348 RVA: 0x001486F0 File Offset: 0x001468F0
		[ComVisible(false)]
		public virtual int KeyboardLayoutId
		{
			get
			{
				int lcid = this.LCID;
				if (lcid <= 1034)
				{
					if (lcid == 4)
					{
						return 2052;
					}
					if (lcid == 1034)
					{
						return 3082;
					}
				}
				else
				{
					if (lcid == 31748)
					{
						return 1028;
					}
					if (lcid == 31770)
					{
						return 2074;
					}
				}
				if (this.LCID >= 1024)
				{
					return this.LCID;
				}
				return this.LCID + 1024;
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06005F1D RID: 24349 RVA: 0x00148764 File Offset: 0x00146964
		public virtual int LCID
		{
			get
			{
				return this.cultureID;
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06005F1E RID: 24350 RVA: 0x00147F4B File Offset: 0x0014614B
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06005F1F RID: 24351 RVA: 0x0014876C File Offset: 0x0014696C
		public virtual string NativeName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.nativename;
			}
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06005F20 RID: 24352 RVA: 0x00148782 File Offset: 0x00146982
		internal string NativeCalendarName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.native_calendar_names[(this.default_calendar_type >> 8) - 1];
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06005F21 RID: 24353 RVA: 0x001487A3 File Offset: 0x001469A3
		public virtual Calendar Calendar
		{
			get
			{
				if (this.calendar == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					this.calendar = CultureInfo.CreateCalendar(this.default_calendar_type);
				}
				return this.calendar;
			}
		}

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06005F22 RID: 24354 RVA: 0x001487D2 File Offset: 0x001469D2
		[MonoLimitation("Optional calendars are not supported only default calendar is returned")]
		public virtual Calendar[] OptionalCalendars
		{
			get
			{
				return new Calendar[] { this.Calendar };
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06005F23 RID: 24355 RVA: 0x001487E4 File Offset: 0x001469E4
		public virtual CultureInfo Parent
		{
			get
			{
				if (this.parent_culture == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					if (this.parent_lcid == this.cultureID)
					{
						if (this.parent_lcid == 31748 && this.EnglishName[this.EnglishName.Length - 1] == 'y')
						{
							return this.parent_culture = new CultureInfo("zh-Hant");
						}
						if (this.parent_lcid == 4 && this.EnglishName[this.EnglishName.Length - 1] == 'y')
						{
							return this.parent_culture = new CultureInfo("zh-Hans");
						}
						return null;
					}
					else if (this.parent_lcid == 127)
					{
						this.parent_culture = CultureInfo.InvariantCulture;
					}
					else if (this.cultureID == 127)
					{
						this.parent_culture = this;
					}
					else if (this.cultureID == 1028)
					{
						this.parent_culture = new CultureInfo("zh-CHT");
					}
					else
					{
						this.parent_culture = new CultureInfo(this.parent_lcid);
					}
				}
				return this.parent_culture;
			}
		}

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06005F24 RID: 24356 RVA: 0x001488F0 File Offset: 0x00146AF0
		public virtual TextInfo TextInfo
		{
			get
			{
				if (this.textInfo == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					lock (this)
					{
						if (this.textInfo == null)
						{
							this.textInfo = this.CreateTextInfo(this.m_isReadOnly);
						}
					}
				}
				return this.textInfo;
			}
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06005F25 RID: 24357 RVA: 0x00148964 File Offset: 0x00146B64
		public virtual string ThreeLetterISOLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.iso3lang;
			}
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06005F26 RID: 24358 RVA: 0x0014897A File Offset: 0x00146B7A
		public virtual string ThreeLetterWindowsLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.win3lang;
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06005F27 RID: 24359 RVA: 0x00148990 File Offset: 0x00146B90
		public virtual string TwoLetterISOLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.iso2lang;
			}
		}

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06005F28 RID: 24360 RVA: 0x001489A6 File Offset: 0x00146BA6
		public bool UseUserOverride
		{
			get
			{
				return this.m_useUserOverride;
			}
		}

		// Token: 0x06005F29 RID: 24361 RVA: 0x001489B0 File Offset: 0x00146BB0
		public void ClearCachedData()
		{
			object obj = CultureInfo.shared_table_lock;
			lock (obj)
			{
				CultureInfo.shared_by_number = null;
				CultureInfo.shared_by_name = null;
			}
			CultureInfo.default_current_culture = null;
			RegionInfo.ClearCachedData();
			TimeZone.ClearCachedData();
			TimeZoneInfo.ClearCachedData();
		}

		// Token: 0x06005F2A RID: 24362 RVA: 0x00148A0C File Offset: 0x00146C0C
		public virtual object Clone()
		{
			if (!this.constructed)
			{
				this.Construct();
			}
			CultureInfo cultureInfo = (CultureInfo)base.MemberwiseClone();
			cultureInfo.m_isReadOnly = false;
			cultureInfo.cached_serialized_form = null;
			if (!this.IsNeutralCulture)
			{
				cultureInfo.NumberFormat = (NumberFormatInfo)this.NumberFormat.Clone();
				cultureInfo.DateTimeFormat = (DateTimeFormatInfo)this.DateTimeFormat.Clone();
			}
			return cultureInfo;
		}

		// Token: 0x06005F2B RID: 24363 RVA: 0x00148A78 File Offset: 0x00146C78
		public override bool Equals(object value)
		{
			CultureInfo cultureInfo = value as CultureInfo;
			return cultureInfo != null && cultureInfo.cultureID == this.cultureID && cultureInfo.m_name == this.m_name;
		}

		// Token: 0x06005F2C RID: 24364 RVA: 0x00148AB0 File Offset: 0x00146CB0
		public static CultureInfo[] GetCultures(CultureTypes types)
		{
			bool flag = (types & CultureTypes.NeutralCultures) > (CultureTypes)0;
			bool flag2 = (types & CultureTypes.SpecificCultures) > (CultureTypes)0;
			bool flag3 = (types & CultureTypes.InstalledWin32Cultures) > (CultureTypes)0;
			CultureInfo[] array = CultureInfo.internal_get_cultures(flag, flag2, flag3);
			int i = 0;
			if (flag && array.Length != 0 && array[0] == null)
			{
				array[i++] = (CultureInfo)CultureInfo.InvariantCulture.Clone();
			}
			while (i < array.Length)
			{
				CultureInfo cultureInfo = array[i];
				CultureInfo.Data textInfoData = cultureInfo.GetTextInfoData();
				CultureInfo cultureInfo2 = array[i];
				string name = cultureInfo.m_name;
				bool flag4 = false;
				int num = cultureInfo.datetime_index;
				int calendarType = cultureInfo.CalendarType;
				int num2 = cultureInfo.number_index;
				string text = cultureInfo.iso2lang;
				int ansi = textInfoData.ansi;
				int oem = textInfoData.oem;
				int mac = textInfoData.mac;
				int ebcdic = textInfoData.ebcdic;
				bool right_to_left = textInfoData.right_to_left;
				char list_sep = (char)textInfoData.list_sep;
				cultureInfo2.m_cultureData = CultureData.GetCultureData(name, flag4, num, calendarType, num2, text, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
				i++;
			}
			return array;
		}

		// Token: 0x06005F2D RID: 24365 RVA: 0x00148B85 File Offset: 0x00146D85
		private unsafe CultureInfo.Data GetTextInfoData()
		{
			return *(CultureInfo.Data*)this.textinfo_data;
		}

		// Token: 0x06005F2E RID: 24366 RVA: 0x00148B92 File Offset: 0x00146D92
		public override int GetHashCode()
		{
			return this.cultureID.GetHashCode();
		}

		// Token: 0x06005F2F RID: 24367 RVA: 0x00148BA0 File Offset: 0x00146DA0
		public static CultureInfo ReadOnly(CultureInfo ci)
		{
			if (ci == null)
			{
				throw new ArgumentNullException("ci");
			}
			if (ci.m_isReadOnly)
			{
				return ci;
			}
			CultureInfo cultureInfo = (CultureInfo)ci.Clone();
			cultureInfo.m_isReadOnly = true;
			if (cultureInfo.numInfo != null)
			{
				cultureInfo.numInfo = NumberFormatInfo.ReadOnly(cultureInfo.numInfo);
			}
			if (cultureInfo.dateTimeInfo != null)
			{
				cultureInfo.dateTimeInfo = DateTimeFormatInfo.ReadOnly(cultureInfo.dateTimeInfo);
			}
			if (cultureInfo.textInfo != null)
			{
				cultureInfo.textInfo = TextInfo.ReadOnly(cultureInfo.textInfo);
			}
			return cultureInfo;
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x00147F4B File Offset: 0x0014614B
		public override string ToString()
		{
			return this.m_name;
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06005F31 RID: 24369 RVA: 0x00148C38 File Offset: 0x00146E38
		public virtual CompareInfo CompareInfo
		{
			get
			{
				if (this.compareInfo == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					lock (this)
					{
						if (this.compareInfo == null)
						{
							this.compareInfo = new CompareInfo(this);
						}
					}
				}
				return this.compareInfo;
			}
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06005F32 RID: 24370 RVA: 0x00148CA8 File Offset: 0x00146EA8
		public virtual bool IsNeutralCulture
		{
			get
			{
				if (this.cultureID == 127)
				{
					return false;
				}
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.territory == null;
			}
		}

		// Token: 0x06005F33 RID: 24371 RVA: 0x00004088 File Offset: 0x00002288
		private void CheckNeutral()
		{
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06005F34 RID: 24372 RVA: 0x00148CD0 File Offset: 0x00146ED0
		// (set) Token: 0x06005F35 RID: 24373 RVA: 0x00148D10 File Offset: 0x00146F10
		public virtual NumberFormatInfo NumberFormat
		{
			get
			{
				if (this.numInfo == null)
				{
					this.numInfo = new NumberFormatInfo(this.m_cultureData)
					{
						isReadOnly = this.m_isReadOnly
					};
				}
				return this.numInfo;
			}
			set
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				if (this.m_isReadOnly)
				{
					throw new InvalidOperationException("This instance is read only");
				}
				if (value == null)
				{
					throw new ArgumentNullException("NumberFormat");
				}
				this.numInfo = value;
			}
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06005F36 RID: 24374 RVA: 0x00148D4C File Offset: 0x00146F4C
		// (set) Token: 0x06005F37 RID: 24375 RVA: 0x00148DC3 File Offset: 0x00146FC3
		public virtual DateTimeFormatInfo DateTimeFormat
		{
			get
			{
				if (this.dateTimeInfo != null)
				{
					return this.dateTimeInfo;
				}
				if (!this.constructed)
				{
					this.Construct();
				}
				this.CheckNeutral();
				DateTimeFormatInfo dateTimeFormatInfo;
				if (GlobalizationMode.Invariant)
				{
					dateTimeFormatInfo = new DateTimeFormatInfo();
				}
				else
				{
					dateTimeFormatInfo = new DateTimeFormatInfo(this.m_cultureData, this.Calendar);
				}
				dateTimeFormatInfo._isReadOnly = this.m_isReadOnly;
				Thread.MemoryBarrier();
				this.dateTimeInfo = dateTimeFormatInfo;
				return this.dateTimeInfo;
			}
			set
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				if (this.m_isReadOnly)
				{
					throw new InvalidOperationException("This instance is read only");
				}
				if (value == null)
				{
					throw new ArgumentNullException("DateTimeFormat");
				}
				this.dateTimeInfo = value;
			}
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06005F38 RID: 24376 RVA: 0x00148DFD File Offset: 0x00146FFD
		public virtual string DisplayName
		{
			get
			{
				return this.EnglishName;
			}
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06005F39 RID: 24377 RVA: 0x00148E05 File Offset: 0x00147005
		public virtual string EnglishName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.englishname;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06005F3A RID: 24378 RVA: 0x00147F3C File Offset: 0x0014613C
		public static CultureInfo InstalledUICulture
		{
			get
			{
				return CultureInfo.ConstructCurrentCulture();
			}
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06005F3B RID: 24379 RVA: 0x00148E1B File Offset: 0x0014701B
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x00148E24 File Offset: 0x00147024
		public virtual object GetFormat(Type formatType)
		{
			object obj = null;
			if (formatType == typeof(NumberFormatInfo))
			{
				obj = this.NumberFormat;
			}
			else if (formatType == typeof(DateTimeFormatInfo))
			{
				obj = this.DateTimeFormat;
			}
			return obj;
		}

		// Token: 0x06005F3D RID: 24381 RVA: 0x00148E68 File Offset: 0x00147068
		private void Construct()
		{
			this.construct_internal_locale_from_lcid(this.cultureID);
			this.constructed = true;
		}

		// Token: 0x06005F3E RID: 24382
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_locale_from_lcid(int lcid);

		// Token: 0x06005F3F RID: 24383
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_locale_from_name(string name);

		// Token: 0x06005F40 RID: 24384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_current_locale_name();

		// Token: 0x06005F41 RID: 24385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CultureInfo[] internal_get_cultures(bool neutral, bool specific, bool installed);

		// Token: 0x06005F42 RID: 24386 RVA: 0x00148E80 File Offset: 0x00147080
		private void ConstructInvariant(bool read_only)
		{
			this.cultureID = 127;
			this.numInfo = NumberFormatInfo.InvariantInfo;
			if (!read_only)
			{
				this.numInfo = (NumberFormatInfo)this.numInfo.Clone();
			}
			this.textInfo = TextInfo.Invariant;
			this.m_name = string.Empty;
			this.englishname = (this.nativename = "Invariant Language (Invariant Country)");
			this.iso3lang = "IVL";
			this.iso2lang = "iv";
			this.win3lang = "IVL";
			this.default_calendar_type = 257;
		}

		// Token: 0x06005F43 RID: 24387 RVA: 0x00148F17 File Offset: 0x00147117
		private TextInfo CreateTextInfo(bool readOnly)
		{
			TextInfo textInfo = new TextInfo(this.m_cultureData);
			textInfo.SetReadOnlyState(readOnly);
			return textInfo;
		}

		// Token: 0x06005F44 RID: 24388 RVA: 0x00148F2B File Offset: 0x0014712B
		public CultureInfo(int culture)
			: this(culture, true)
		{
		}

		// Token: 0x06005F45 RID: 24389 RVA: 0x00148F35 File Offset: 0x00147135
		public CultureInfo(int culture, bool useUserOverride)
			: this(culture, useUserOverride, false)
		{
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x00148F40 File Offset: 0x00147140
		private CultureInfo(int culture, bool useUserOverride, bool read_only)
		{
			if (culture < 0)
			{
				throw new ArgumentOutOfRangeException("culture", "Positive number required.");
			}
			this.constructed = true;
			this.m_isReadOnly = read_only;
			this.m_useUserOverride = useUserOverride;
			if (culture == 127)
			{
				this.m_cultureData = CultureData.Invariant;
				this.ConstructInvariant(read_only);
				return;
			}
			if (!this.construct_internal_locale_from_lcid(culture))
			{
				string text = string.Format(CultureInfo.InvariantCulture, "Culture ID {0} (0x{1}) is not a supported culture.", culture.ToString(CultureInfo.InvariantCulture), culture.ToString("X4", CultureInfo.InvariantCulture));
				throw new CultureNotFoundException("culture", text);
			}
			CultureInfo.Data textInfoData = this.GetTextInfoData();
			string name = this.m_name;
			bool useUserOverride2 = this.m_useUserOverride;
			int num = this.datetime_index;
			int calendarType = this.CalendarType;
			int num2 = this.number_index;
			string text2 = this.iso2lang;
			int ansi = textInfoData.ansi;
			int oem = textInfoData.oem;
			int mac = textInfoData.mac;
			int ebcdic = textInfoData.ebcdic;
			bool right_to_left = textInfoData.right_to_left;
			char list_sep = (char)textInfoData.list_sep;
			this.m_cultureData = CultureData.GetCultureData(name, useUserOverride2, num, calendarType, num2, text2, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x00149038 File Offset: 0x00147238
		public CultureInfo(string name)
			: this(name, true)
		{
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x00149042 File Offset: 0x00147242
		public CultureInfo(string name, bool useUserOverride)
			: this(name, useUserOverride, false)
		{
		}

		// Token: 0x06005F49 RID: 24393 RVA: 0x00149050 File Offset: 0x00147250
		private CultureInfo(string name, bool useUserOverride, bool read_only)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.constructed = true;
			this.m_isReadOnly = read_only;
			this.m_useUserOverride = useUserOverride;
			this.m_isInherited = base.GetType() != typeof(CultureInfo);
			if (name.Length == 0)
			{
				this.m_cultureData = CultureData.Invariant;
				this.ConstructInvariant(read_only);
				return;
			}
			if (!this.construct_internal_locale_from_name(name.ToLowerInvariant()))
			{
				throw CultureInfo.CreateNotFoundException(name);
			}
			CultureInfo.Data textInfoData = this.GetTextInfoData();
			string name2 = this.m_name;
			int num = this.datetime_index;
			int calendarType = this.CalendarType;
			int num2 = this.number_index;
			string text = this.iso2lang;
			int ansi = textInfoData.ansi;
			int oem = textInfoData.oem;
			int mac = textInfoData.mac;
			int ebcdic = textInfoData.ebcdic;
			bool right_to_left = textInfoData.right_to_left;
			char list_sep = (char)textInfoData.list_sep;
			this.m_cultureData = CultureData.GetCultureData(name2, useUserOverride, num, calendarType, num2, text, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
		}

		// Token: 0x06005F4A RID: 24394 RVA: 0x0014912E File Offset: 0x0014732E
		private CultureInfo()
		{
			this.constructed = true;
		}

		// Token: 0x06005F4B RID: 24395 RVA: 0x0014913D File Offset: 0x0014733D
		private static void insert_into_shared_tables(CultureInfo c)
		{
			if (CultureInfo.shared_by_number == null)
			{
				CultureInfo.shared_by_number = new Dictionary<int, CultureInfo>();
				CultureInfo.shared_by_name = new Dictionary<string, CultureInfo>();
			}
			CultureInfo.shared_by_number[c.cultureID] = c;
			CultureInfo.shared_by_name[c.m_name] = c;
		}

		// Token: 0x06005F4C RID: 24396 RVA: 0x0014917C File Offset: 0x0014737C
		public static CultureInfo GetCultureInfo(int culture)
		{
			if (culture < 1)
			{
				throw new ArgumentOutOfRangeException("culture", "Positive number required.");
			}
			object obj = CultureInfo.shared_table_lock;
			CultureInfo cultureInfo2;
			lock (obj)
			{
				CultureInfo cultureInfo;
				if (CultureInfo.shared_by_number != null && CultureInfo.shared_by_number.TryGetValue(culture, out cultureInfo))
				{
					cultureInfo2 = cultureInfo;
				}
				else
				{
					cultureInfo = new CultureInfo(culture, false, true);
					CultureInfo.insert_into_shared_tables(cultureInfo);
					cultureInfo2 = cultureInfo;
				}
			}
			return cultureInfo2;
		}

		// Token: 0x06005F4D RID: 24397 RVA: 0x001491F8 File Offset: 0x001473F8
		public static CultureInfo GetCultureInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = CultureInfo.shared_table_lock;
			CultureInfo cultureInfo2;
			lock (obj)
			{
				CultureInfo cultureInfo;
				if (CultureInfo.shared_by_name != null && CultureInfo.shared_by_name.TryGetValue(name, out cultureInfo))
				{
					cultureInfo2 = cultureInfo;
				}
				else
				{
					cultureInfo = new CultureInfo(name, false, true);
					CultureInfo.insert_into_shared_tables(cultureInfo);
					cultureInfo2 = cultureInfo;
				}
			}
			return cultureInfo2;
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x0014926C File Offset: 0x0014746C
		[MonoTODO("Currently it ignores the altName parameter")]
		public static CultureInfo GetCultureInfo(string name, string altName)
		{
			if (name == null)
			{
				throw new ArgumentNullException("null");
			}
			if (altName == null)
			{
				throw new ArgumentNullException("null");
			}
			return CultureInfo.GetCultureInfo(name);
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x00149290 File Offset: 0x00147490
		public static CultureInfo GetCultureInfoByIetfLanguageTag(string name)
		{
			if (name == "zh-Hans")
			{
				return CultureInfo.GetCultureInfo("zh-CHS");
			}
			if (!(name == "zh-Hant"))
			{
				return CultureInfo.GetCultureInfo(name);
			}
			return CultureInfo.GetCultureInfo("zh-CHT");
		}

		// Token: 0x06005F50 RID: 24400 RVA: 0x001492CC File Offset: 0x001474CC
		internal static CultureInfo CreateCulture(string name, bool reference)
		{
			bool flag = name.Length == 0;
			bool flag2;
			bool flag3;
			if (reference)
			{
				flag2 = !flag;
				flag3 = false;
			}
			else
			{
				flag3 = false;
				flag2 = !flag;
			}
			return new CultureInfo(name, flag2, flag3);
		}

		// Token: 0x06005F51 RID: 24401 RVA: 0x00149304 File Offset: 0x00147504
		public static CultureInfo CreateSpecificCulture(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				return CultureInfo.InvariantCulture;
			}
			string text = name;
			name = name.ToLowerInvariant();
			CultureInfo cultureInfo = new CultureInfo();
			if (!cultureInfo.construct_internal_locale_from_name(name))
			{
				int num = name.Length - 1;
				if (num > 0)
				{
					while ((num = name.LastIndexOf('-', num - 1)) > 0 && !cultureInfo.construct_internal_locale_from_name(name.Substring(0, num)))
					{
					}
				}
				if (num <= 0)
				{
					throw CultureInfo.CreateNotFoundException(text);
				}
			}
			if (cultureInfo.IsNeutralCulture)
			{
				cultureInfo = CultureInfo.CreateSpecificCultureFromNeutral(cultureInfo.Name);
			}
			CultureInfo.Data textInfoData = cultureInfo.GetTextInfoData();
			CultureInfo cultureInfo2 = cultureInfo;
			string name2 = cultureInfo.m_name;
			bool flag = false;
			int num2 = cultureInfo.datetime_index;
			int calendarType = cultureInfo.CalendarType;
			int num3 = cultureInfo.number_index;
			string text2 = cultureInfo.iso2lang;
			int ansi = textInfoData.ansi;
			int oem = textInfoData.oem;
			int mac = textInfoData.mac;
			int ebcdic = textInfoData.ebcdic;
			bool right_to_left = textInfoData.right_to_left;
			char list_sep = (char)textInfoData.list_sep;
			cultureInfo2.m_cultureData = CultureData.GetCultureData(name2, flag, num2, calendarType, num3, text2, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
			return cultureInfo;
		}

		// Token: 0x06005F52 RID: 24402 RVA: 0x001493F4 File Offset: 0x001475F4
		private static CultureInfo CreateSpecificCultureFromNeutral(string name)
		{
			string text = name.ToLowerInvariant();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			int num2;
			if (num <= 1344898993U)
			{
				if (num <= 1128614327U)
				{
					if (num <= 1025408520U)
					{
						if (num <= 975938470U)
						{
							if (num <= 926444256U)
							{
								if (num <= 896475900U)
								{
									if (num != 275533995U)
									{
										if (num == 896475900U)
										{
											if (text == "arn")
											{
												num2 = 1146;
												goto IL_1B49;
											}
										}
									}
									else if (text == "nso")
									{
										num2 = 1132;
										goto IL_1B49;
									}
								}
								else if (num != 925484199U)
								{
									if (num == 926444256U)
									{
										if (text == "id")
										{
											num2 = 1057;
											goto IL_1B49;
										}
									}
								}
								else if (text == "mn-cyrl")
								{
									num2 = 1104;
									goto IL_1B49;
								}
							}
							else if (num <= 944060518U)
							{
								if (num != 942383232U)
								{
									if (num == 944060518U)
									{
										if (text == "ta")
										{
											num2 = 1097;
											goto IL_1B49;
										}
									}
								}
								else if (text == "be")
								{
									num2 = 1059;
									goto IL_1B49;
								}
							}
							else if (num != 944899161U)
							{
								if (num == 975938470U)
								{
									if (text == "bg")
									{
										num2 = 1026;
										goto IL_1B49;
									}
								}
							}
							else if (text == "sa")
							{
								num2 = 1103;
								goto IL_1B49;
							}
						}
						else if (num <= 996684602U)
						{
							if (num <= 977615756U)
							{
								if (num != 976777113U)
								{
									if (num == 977615756U)
									{
										if (text == "tg")
										{
											num2 = 1064;
											goto IL_1B49;
										}
									}
								}
								else if (text == "ig")
								{
									num2 = 1136;
									goto IL_1B49;
								}
							}
							else if (num != 991980614U)
							{
								if (num == 996684602U)
								{
									if (text == "mn-mong")
									{
										num2 = 2128;
										goto IL_1B49;
									}
								}
							}
							else if (text == "gd")
							{
								num2 = 1169;
								goto IL_1B49;
							}
						}
						else if (num <= 1011170994U)
						{
							if (num != 1009493708U)
							{
								if (num == 1011170994U)
								{
									if (text == "te")
									{
										num2 = 1098;
										goto IL_1B49;
									}
								}
							}
							else if (text == "ba")
							{
								num2 = 1133;
								goto IL_1B49;
							}
						}
						else if (num != 1011465184U)
						{
							if (num != 1012009637U)
							{
								if (num == 1025408520U)
								{
									if (text == "tzm-latn")
									{
										num2 = 2143;
										goto IL_1B49;
									}
								}
							}
							else if (text == "se")
							{
								num2 = 1083;
								goto IL_1B49;
							}
						}
						else if (text == "vi")
						{
							num2 = 1066;
							goto IL_1B49;
						}
					}
					else if (num <= 1092248970U)
					{
						if (num <= 1058693732U)
						{
							if (num <= 1044726232U)
							{
								if (num != 1044181779U)
								{
									if (num == 1044726232U)
									{
										if (text == "tk")
										{
											num2 = 1090;
											goto IL_1B49;
										}
									}
								}
								else if (text == "kk")
								{
									num2 = 1087;
									goto IL_1B49;
								}
							}
							else if (num != 1045564875U)
							{
								if (num == 1058693732U)
								{
									if (text == "el")
									{
										num2 = 1032;
										goto IL_1B49;
									}
								}
							}
							else if (text == "sk")
							{
								num2 = 1051;
								goto IL_1B49;
							}
						}
						else if (num <= 1076162899U)
						{
							if (num != 1075868709U)
							{
								if (num == 1076162899U)
								{
									if (text == "am")
									{
										num2 = 1118;
										goto IL_1B49;
									}
								}
							}
							else if (text == "ga")
							{
								num2 = 2108;
								goto IL_1B49;
							}
						}
						else if (num != 1079120113U)
						{
							if (num != 1087741671U)
							{
								if (num == 1092248970U)
								{
									if (text == "en")
									{
										num2 = 1033;
										goto IL_1B49;
									}
								}
							}
							else if (text == "az-cyrl")
							{
								num2 = 2092;
								goto IL_1B49;
							}
						}
						else if (text == "si")
						{
							num2 = 1115;
							goto IL_1B49;
						}
					}
					else if (num <= 1110556780U)
					{
						if (num <= 1095059089U)
						{
							if (num != 1094514636U)
							{
								if (num == 1095059089U)
								{
									if (text == "th")
									{
										num2 = 1054;
										goto IL_1B49;
									}
								}
							}
							else if (text == "kn")
							{
								num2 = 1099;
								goto IL_1B49;
							}
						}
						else if (num != 1110159422U)
						{
							if (num == 1110556780U)
							{
								if (text == "lo")
								{
									num2 = 1108;
									goto IL_1B49;
								}
							}
						}
						else if (text == "bo")
						{
							num2 = 1105;
							goto IL_1B49;
						}
					}
					else if (num <= 1126201566U)
					{
						if (num != 1111292255U)
						{
							if (num == 1126201566U)
							{
								if (text == "gl")
								{
									num2 = 1110;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ko")
						{
							num2 = 1042;
							goto IL_1B49;
						}
					}
					else if (num != 1126937041U)
					{
						if (num != 1128069874U)
						{
							if (num == 1128614327U)
							{
								if (text == "tn")
								{
									num2 = 1074;
									goto IL_1B49;
								}
							}
						}
						else if (text == "kl")
						{
							num2 = 1135;
							goto IL_1B49;
						}
					}
					else if (text == "bn")
					{
						num2 = 1093;
						goto IL_1B49;
					}
				}
				else if (num <= 1213341065U)
				{
					if (num <= 1177122803U)
					{
						if (num <= 1162022470U)
						{
							if (num <= 1144553303U)
							{
								if (num != 1129452970U)
								{
									if (num == 1144553303U)
									{
										if (text == "ii")
										{
											num2 = 1144;
											goto IL_1B49;
										}
									}
								}
								else if (text == "sl")
								{
									num2 = 1060;
									goto IL_1B49;
								}
							}
							else if (num != 1144847493U)
							{
								if (num == 1162022470U)
								{
									if (text == "ur")
									{
										num2 = 1056;
										goto IL_1B49;
									}
								}
							}
							else if (text == "km")
							{
								num2 = 1107;
								goto IL_1B49;
							}
						}
						else if (num <= 1163008208U)
						{
							if (num != 1162757945U)
							{
								if (num == 1163008208U)
								{
									if (text == "sr")
									{
										num2 = 9242;
										goto IL_1B49;
									}
								}
							}
							else if (text == "pl")
							{
								num2 = 1045;
								goto IL_1B49;
							}
						}
						else if (num != 1164435231U)
						{
							if (num != 1176137065U)
							{
								if (num == 1177122803U)
								{
									if (text == "cs")
									{
										num2 = 1029;
										goto IL_1B49;
									}
								}
							}
							else if (text == "es")
							{
								num2 = 3082;
								goto IL_1B49;
							}
						}
						else if (text == "zh")
						{
							num2 = 2052;
							goto IL_1B49;
						}
					}
					else if (num <= 1195724803U)
					{
						if (num <= 1194444875U)
						{
							if (num != 1192914684U)
							{
								if (num == 1194444875U)
								{
									if (text == "lb")
									{
										num2 = 1134;
										goto IL_1B49;
									}
								}
							}
							else if (text == "et")
							{
								num2 = 1061;
								goto IL_1B49;
							}
						}
						else if (num != 1194886160U)
						{
							if (num == 1195724803U)
							{
								if (text == "tr")
								{
									num2 = 1055;
									goto IL_1B49;
								}
							}
						}
						else if (text == "it")
						{
							num2 = 1040;
							goto IL_1B49;
						}
					}
					else if (num <= 1211324057U)
					{
						if (num != 1209692303U)
						{
							if (num == 1211324057U)
							{
								if (text == "iu-cans")
								{
									num2 = 1117;
									goto IL_1B49;
								}
							}
						}
						else if (text == "eu")
						{
							num2 = 1069;
							goto IL_1B49;
						}
					}
					else if (num != 1211663779U)
					{
						if (num != 1211957969U)
						{
							if (num == 1213341065U)
							{
								if (text == "sq")
								{
									num2 = 1052;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ka")
						{
							num2 = 1079;
							goto IL_1B49;
						}
					}
					else if (text == "iu")
					{
						num2 = 2141;
						goto IL_1B49;
					}
				}
				else if (num <= 1277200137U)
				{
					if (num <= 1231251517U)
					{
						if (num <= 1227161470U)
						{
							if (num != 1213488160U)
							{
								if (num == 1227161470U)
								{
									if (text == "af")
									{
										num2 = 1078;
										goto IL_1B49;
									}
								}
							}
							else if (text == "ru")
							{
								num2 = 1049;
								goto IL_1B49;
							}
						}
						else if (num != 1230118684U)
						{
							if (num == 1231251517U)
							{
								if (text == "xh")
								{
									num2 = 1076;
									goto IL_1B49;
								}
							}
						}
						else if (text == "sv")
						{
							num2 = 1053;
							goto IL_1B49;
						}
					}
					else if (num <= 1246896303U)
					{
						if (num != 1237973804U)
						{
							if (num == 1246896303U)
							{
								if (text == "sw")
								{
									num2 = 1089;
									goto IL_1B49;
								}
							}
						}
						else if (text == "uz-latn")
						{
							num2 = 1091;
							goto IL_1B49;
						}
					}
					else if (num != 1247043398U)
					{
						if (num != 1260172255U)
						{
							if (num == 1277200137U)
							{
								if (text == "gu")
								{
									num2 = 1095;
									goto IL_1B49;
								}
							}
						}
						else if (text == "dv")
						{
							num2 = 1125;
							goto IL_1B49;
						}
					}
					else if (text == "rw")
					{
						num2 = 1159;
						goto IL_1B49;
					}
				}
				else if (num <= 1296390517U)
				{
					if (num <= 1278921350U)
					{
						if (num != 1277347232U)
						{
							if (num == 1278921350U)
							{
								if (text == "hu")
								{
									num2 = 1038;
									goto IL_1B49;
								}
							}
						}
						else if (text == "fy")
						{
							num2 = 1122;
							goto IL_1B49;
						}
					}
					else if (num != 1296243422U)
					{
						if (num == 1296390517U)
						{
							if (text == "tt")
							{
								num2 = 1092;
								goto IL_1B49;
							}
						}
					}
					else if (text == "uz")
					{
						num2 = 1091;
						goto IL_1B49;
					}
				}
				else if (num <= 1312329493U)
				{
					if (num != 1311490850U)
					{
						if (num == 1312329493U)
						{
							if (text == "is")
							{
								num2 = 1039;
								goto IL_1B49;
							}
						}
					}
					else if (text == "bs")
					{
						num2 = 5146;
						goto IL_1B49;
					}
				}
				else if (num != 1328268469U)
				{
					if (num != 1329254207U)
					{
						if (num == 1344898993U)
						{
							if (text == "cy")
							{
								num2 = 1106;
								goto IL_1B49;
							}
						}
					}
					else if (text == "hr")
					{
						num2 = 1050;
						goto IL_1B49;
					}
				}
				else if (text == "br")
				{
					num2 = 1150;
					goto IL_1B49;
				}
			}
			else if (num <= 1646454850U)
			{
				if (num <= 1545391778U)
				{
					if (num <= 1462636516U)
					{
						if (num <= 1428492898U)
						{
							if (num <= 1347311754U)
							{
								if (num != 1346178921U)
								{
									if (num == 1347311754U)
									{
										if (text == "pa")
										{
											num2 = 1094;
											goto IL_1B49;
										}
									}
								}
								else if (text == "ky")
								{
									num2 = 1088;
									goto IL_1B49;
								}
							}
							else if (num != 1424802581U)
							{
								if (num == 1428492898U)
								{
									if (text == "az")
									{
										num2 = 1068;
										goto IL_1B49;
									}
								}
							}
							else if (text == "tg-cyrl")
							{
								num2 = 1064;
								goto IL_1B49;
							}
						}
						else if (num <= 1429850248U)
						{
							if (num != 1429081278U)
							{
								if (num == 1429850248U)
								{
									if (text == "gsw")
									{
										num2 = 1156;
										goto IL_1B49;
									}
								}
							}
							else if (text == "mr")
							{
								num2 = 1102;
								goto IL_1B49;
							}
						}
						else if (num != 1445858897U)
						{
							if (num != 1461901041U)
							{
								if (num == 1462636516U)
								{
									if (text == "mt")
									{
										num2 = 1082;
										goto IL_1B49;
									}
								}
							}
							else if (text == "fr")
							{
								num2 = 1036;
								goto IL_1B49;
							}
						}
						else if (text == "ms")
						{
							num2 = 1086;
							goto IL_1B49;
						}
					}
					else if (num <= 1479958588U)
					{
						if (num <= 1478281302U)
						{
							if (num != 1463180969U)
							{
								if (num == 1478281302U)
								{
									if (text == "da")
									{
										num2 = 1030;
										goto IL_1B49;
									}
								}
							}
							else if (text == "nb")
							{
								num2 = 1044;
								goto IL_1B49;
							}
						}
						else if (num != 1479119945U)
						{
							if (num == 1479958588U)
							{
								if (text == "ne")
								{
									num2 = 1121;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ca")
						{
							num2 = 1027;
							goto IL_1B49;
						}
					}
					else if (num <= 1483209992U)
					{
						if (num != 1480252778U)
						{
							if (num == 1483209992U)
							{
								if (text == "zu")
								{
									num2 = 1077;
									goto IL_1B49;
								}
							}
						}
						else if (text == "hy")
						{
							num2 = 1067;
							goto IL_1B49;
						}
					}
					else if (num != 1514352469U)
					{
						if (num != 1529997255U)
						{
							if (num == 1545391778U)
							{
								if (text == "de")
								{
									num2 = 1031;
									goto IL_1B49;
								}
							}
						}
						else if (text == "lv")
						{
							num2 = 1062;
							goto IL_1B49;
						}
					}
					else if (text == "ug")
					{
						num2 = 1152;
						goto IL_1B49;
					}
				}
				else if (num <= 1579491469U)
				{
					if (num <= 1551553596U)
					{
						if (num <= 1546524611U)
						{
							if (num != 1545789136U)
							{
								if (num == 1546524611U)
								{
									if (text == "mi")
									{
										num2 = 1153;
										goto IL_1B49;
									}
								}
							}
							else if (text == "fi")
							{
								num2 = 1035;
								goto IL_1B49;
							}
						}
						else if (num != 1547363254U)
						{
							if (num == 1551553596U)
							{
								if (text == "prs")
								{
									num2 = 1164;
									goto IL_1B49;
								}
							}
						}
						else if (text == "he")
						{
							num2 = 1037;
							goto IL_1B49;
						}
					}
					else if (num <= 1563552493U)
					{
						if (num != 1562713850U)
						{
							if (num == 1563552493U)
							{
								if (text == "lt")
								{
									num2 = 1063;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ar")
						{
							num2 = 1025;
							goto IL_1B49;
						}
					}
					else if (num != 1563699588U)
					{
						if (num != 1565420801U)
						{
							if (num == 1579491469U)
							{
								if (text == "as")
								{
									num2 = 1101;
									goto IL_1B49;
								}
							}
						}
						else if (text == "pt")
						{
							num2 = 1046;
							goto IL_1B49;
						}
					}
					else if (text == "or")
					{
						num2 = 1096;
						goto IL_1B49;
					}
				}
				else if (num <= 1596857468U)
				{
					if (num <= 1581462945U)
					{
						if (num != 1580079849U)
						{
							if (num == 1581462945U)
							{
								if (text == "uk")
								{
									num2 = 1058;
									goto IL_1B49;
								}
							}
						}
						else if (text == "mk")
						{
							num2 = 1071;
							goto IL_1B49;
						}
					}
					else if (num != 1582198420U)
					{
						if (num == 1596857468U)
						{
							if (text == "ml")
							{
								num2 = 1100;
								goto IL_1B49;
							}
						}
					}
					else if (text == "ps")
					{
						num2 = 1123;
						goto IL_1B49;
					}
				}
				else if (num <= 1616151016U)
				{
					if (num != 1614473730U)
					{
						if (num == 1616151016U)
						{
							if (text == "rm")
							{
								num2 = 1047;
								goto IL_1B49;
							}
						}
					}
					else if (text == "ha")
					{
						num2 = 1128;
						goto IL_1B49;
					}
				}
				else if (num != 1630412706U)
				{
					if (num != 1630957159U)
					{
						if (num == 1646454850U)
						{
							if (text == "fo")
							{
								num2 = 1080;
								goto IL_1B49;
							}
						}
					}
					else if (text == "nl")
					{
						num2 = 1043;
						goto IL_1B49;
					}
				}
				else if (text == "mn")
				{
					num2 = 1104;
					goto IL_1B49;
				}
			}
			else if (num <= 3012500870U)
			{
				if (num <= 1748694682U)
				{
					if (num <= 1649706254U)
					{
						if (num <= 1647734778U)
						{
							if (num != 1646896135U)
							{
								if (num == 1647734778U)
								{
									if (text == "no")
									{
										num2 = 1044;
										goto IL_1B49;
									}
								}
							}
							else if (text == "co")
							{
								num2 = 1155;
								goto IL_1B49;
							}
						}
						else if (num != 1648867611U)
						{
							if (num == 1649706254U)
							{
								if (text == "ro")
								{
									num2 = 1048;
									goto IL_1B49;
								}
							}
						}
						else if (text == "wo")
						{
							num2 = 1160;
							goto IL_1B49;
						}
					}
					else if (num <= 1664512397U)
					{
						if (num != 1650441729U)
						{
							if (num == 1664512397U)
							{
								if (text == "nn")
								{
									num2 = 2068;
									goto IL_1B49;
								}
							}
						}
						else if (text == "yo")
						{
							num2 = 1130;
							goto IL_1B49;
						}
					}
					else if (num != 1680010088U)
					{
						if (num != 1680473867U)
						{
							if (num == 1748694682U)
							{
								if (text == "hi")
								{
									num2 = 1081;
									goto IL_1B49;
								}
							}
						}
						else if (text == "iu-latn")
						{
							num2 = 2141;
							goto IL_1B49;
						}
					}
					else if (text == "fa")
					{
						num2 = 1065;
						goto IL_1B49;
					}
				}
				else if (num <= 2046577884U)
				{
					if (num <= 1816099348U)
					{
						if (num != 1790977000U)
						{
							if (num == 1816099348U)
							{
								if (text == "ja")
								{
									num2 = 1041;
									goto IL_1B49;
								}
							}
						}
						else if (text == "bs-latn")
						{
							num2 = 5146;
							goto IL_1B49;
						}
					}
					else if (num != 1848919111U)
					{
						if (num == 2046577884U)
						{
							if (text == "kok")
							{
								num2 = 1111;
								goto IL_1B49;
							}
						}
					}
					else if (text == "oc")
					{
						num2 = 1154;
						goto IL_1B49;
					}
				}
				else
				{
					if (num <= 2197937899U)
					{
						if (num != 2180460995U)
						{
							if (num != 2197937899U)
							{
								goto IL_1B38;
							}
							if (!(text == "zh-hant"))
							{
								goto IL_1B38;
							}
						}
						else if (!(text == "zh-cht"))
						{
							goto IL_1B38;
						}
						num2 = 3076;
						goto IL_1B49;
					}
					if (num != 2264349090U)
					{
						if (num != 2281825994U)
						{
							if (num != 3012500870U)
							{
								goto IL_1B38;
							}
							if (!(text == "sr-latn"))
							{
								goto IL_1B38;
							}
							num2 = 9242;
							goto IL_1B49;
						}
						else if (!(text == "zh-hans"))
						{
							goto IL_1B38;
						}
					}
					else if (!(text == "zh-chs"))
					{
						goto IL_1B38;
					}
					num2 = 2052;
					goto IL_1B49;
				}
			}
			else if (num <= 3795602801U)
			{
				if (num <= 3294142633U)
				{
					if (num <= 3224459074U)
					{
						if (num != 3174420263U)
						{
							if (num == 3224459074U)
							{
								if (text == "tzm")
								{
									num2 = 2143;
									goto IL_1B49;
								}
							}
						}
						else if (text == "bs-cyrl")
						{
							num2 = 8218;
							goto IL_1B49;
						}
					}
					else if (num != 3240320582U)
					{
						if (num == 3294142633U)
						{
							if (text == "syr")
							{
								num2 = 1114;
								goto IL_1B49;
							}
						}
					}
					else if (text == "dsb")
					{
						num2 = 2094;
						goto IL_1B49;
					}
				}
				else if (num <= 3659307299U)
				{
					if (num != 3336872436U)
					{
						if (num == 3659307299U)
						{
							if (text == "sah")
							{
								num2 = 1157;
								goto IL_1B49;
							}
						}
					}
					else if (text == "fil")
					{
						num2 = 1124;
						goto IL_1B49;
					}
				}
				else if (num != 3678056394U)
				{
					if (num != 3761944489U)
					{
						if (num == 3795602801U)
						{
							if (text == "sr-cyrl")
							{
								num2 = 10266;
								goto IL_1B49;
							}
						}
					}
					else if (text == "smn")
					{
						num2 = 9275;
						goto IL_1B49;
					}
				}
				else if (text == "sms")
				{
					num2 = 8251;
					goto IL_1B49;
				}
			}
			else if (num <= 3953034599U)
			{
				if (num <= 3912943060U)
				{
					if (num != 3829054965U)
					{
						if (num == 3912943060U)
						{
							if (text == "sma")
							{
								num2 = 7227;
								goto IL_1B49;
							}
						}
					}
					else if (text == "smj")
					{
						num2 = 5179;
						goto IL_1B49;
					}
				}
				else if (num != 3918412059U)
				{
					if (num == 3953034599U)
					{
						if (text == "moh")
						{
							num2 = 1148;
							goto IL_1B49;
						}
					}
				}
				else if (text == "uz-cyrl")
				{
					num2 = 2115;
					goto IL_1B49;
				}
			}
			else if (num <= 4041297251U)
			{
				if (num != 3999162536U)
				{
					if (num == 4041297251U)
					{
						if (text == "quz")
						{
							num2 = 1131;
							goto IL_1B49;
						}
					}
				}
				else if (text == "az-latn")
				{
					num2 = 1068;
					goto IL_1B49;
				}
			}
			else if (num != 4103207754U)
			{
				if (num != 4276183917U)
				{
					if (num == 4280271688U)
					{
						if (text == "ha-latn")
						{
							num2 = 1128;
							goto IL_1B49;
						}
					}
				}
				else if (text == "qut")
				{
					num2 = 1158;
					goto IL_1B49;
				}
			}
			else if (text == "hsb")
			{
				num2 = 1070;
				goto IL_1B49;
			}
			IL_1B38:
			throw new NotImplementedException("Mapping for neutral culture " + name);
			IL_1B49:
			return new CultureInfo(num2);
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06005F53 RID: 24403 RVA: 0x0014AF50 File Offset: 0x00149150
		internal int CalendarType
		{
			get
			{
				switch (this.default_calendar_type >> 8)
				{
				case 1:
					return 1;
				case 2:
					return 7;
				case 3:
					return 23;
				case 4:
					return 6;
				default:
					throw new NotImplementedException("CalendarType");
				}
			}
		}

		// Token: 0x06005F54 RID: 24404 RVA: 0x0014AF94 File Offset: 0x00149194
		private static Calendar CreateCalendar(int calendarType)
		{
			string text;
			switch (calendarType >> 8)
			{
			case 1:
				return new GregorianCalendar((GregorianCalendarTypes)(calendarType & 255));
			case 2:
				text = "System.Globalization.ThaiBuddhistCalendar";
				break;
			case 3:
				text = "System.Globalization.UmAlQuraCalendar";
				break;
			case 4:
				text = "System.Globalization.HijriCalendar";
				break;
			default:
				throw new NotImplementedException("Unknown calendar type: " + calendarType.ToString());
			}
			Type type = Type.GetType(text, false);
			if (type == null)
			{
				return new GregorianCalendar(GregorianCalendarTypes.Localized);
			}
			return (Calendar)Activator.CreateInstance(type);
		}

		// Token: 0x06005F55 RID: 24405 RVA: 0x0014B020 File Offset: 0x00149220
		private static Exception CreateNotFoundException(string name)
		{
			return new CultureNotFoundException("name", "Culture name " + name + " is not supported.");
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06005F56 RID: 24406 RVA: 0x0014B03C File Offset: 0x0014923C
		// (set) Token: 0x06005F57 RID: 24407 RVA: 0x0014B045 File Offset: 0x00149245
		public static CultureInfo DefaultThreadCurrentCulture
		{
			get
			{
				return CultureInfo.s_DefaultThreadCurrentCulture;
			}
			set
			{
				CultureInfo.s_DefaultThreadCurrentCulture = value;
			}
		}

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06005F58 RID: 24408 RVA: 0x0014B04F File Offset: 0x0014924F
		// (set) Token: 0x06005F59 RID: 24409 RVA: 0x0014B058 File Offset: 0x00149258
		public static CultureInfo DefaultThreadCurrentUICulture
		{
			get
			{
				return CultureInfo.s_DefaultThreadCurrentUICulture;
			}
			set
			{
				CultureInfo.s_DefaultThreadCurrentUICulture = value;
			}
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06005F5A RID: 24410 RVA: 0x00147F4B File Offset: 0x0014614B
		internal string SortName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06005F5B RID: 24411 RVA: 0x0014B062 File Offset: 0x00149262
		internal static CultureInfo UserDefaultUICulture
		{
			get
			{
				return CultureInfo.ConstructCurrentUICulture();
			}
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06005F5C RID: 24412 RVA: 0x00147F3C File Offset: 0x0014613C
		internal static CultureInfo UserDefaultCulture
		{
			get
			{
				return CultureInfo.ConstructCurrentCulture();
			}
		}

		// Token: 0x06005F5D RID: 24413 RVA: 0x0014B06C File Offset: 0x0014926C
		internal static void CheckDomainSafetyObject(object obj, object container)
		{
			if (obj.GetType().Assembly != typeof(CultureInfo).Assembly)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cannot set sub-classed {0} object to {1} object."), obj.GetType(), container.GetType()));
			}
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06005F5E RID: 24414 RVA: 0x0014B0C0 File Offset: 0x001492C0
		internal bool HasInvariantCultureName
		{
			get
			{
				return this.Name == CultureInfo.InvariantCulture.Name;
			}
		}

		// Token: 0x06005F5F RID: 24415 RVA: 0x0014B0D8 File Offset: 0x001492D8
		internal static bool VerifyCultureName(string cultureName, bool throwException)
		{
			int i = 0;
			while (i < cultureName.Length)
			{
				char c = cultureName[i];
				if (!char.IsLetterOrDigit(c) && c != '-' && c != '_')
				{
					if (throwException)
					{
						throw new ArgumentException(Environment.GetResourceString("The given culture name '{0}' cannot be used to locate a resource file. Resource filenames must consist of only letters, numbers, hyphens or underscores.", new object[] { cultureName }));
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			return true;
		}

		// Token: 0x06005F60 RID: 24416 RVA: 0x0014B130 File Offset: 0x00149330
		internal static bool VerifyCultureName(CultureInfo culture, bool throwException)
		{
			return !culture.m_isInherited || CultureInfo.VerifyCultureName(culture.Name, throwException);
		}

		// Token: 0x06005F61 RID: 24417 RVA: 0x0014B148 File Offset: 0x00149348
		// Note: this type is marked as 'beforefieldinit'.
		static CultureInfo()
		{
		}

		// Token: 0x04003956 RID: 14678
		private static volatile CultureInfo invariant_culture_info = new CultureInfo(127, false, true);

		// Token: 0x04003957 RID: 14679
		private static object shared_table_lock = new object();

		// Token: 0x04003958 RID: 14680
		private static CultureInfo default_current_culture;

		// Token: 0x04003959 RID: 14681
		private bool m_isReadOnly;

		// Token: 0x0400395A RID: 14682
		private int cultureID;

		// Token: 0x0400395B RID: 14683
		[NonSerialized]
		private int parent_lcid;

		// Token: 0x0400395C RID: 14684
		[NonSerialized]
		private int datetime_index;

		// Token: 0x0400395D RID: 14685
		[NonSerialized]
		private int number_index;

		// Token: 0x0400395E RID: 14686
		[NonSerialized]
		private int default_calendar_type;

		// Token: 0x0400395F RID: 14687
		private bool m_useUserOverride;

		// Token: 0x04003960 RID: 14688
		internal volatile NumberFormatInfo numInfo;

		// Token: 0x04003961 RID: 14689
		internal volatile DateTimeFormatInfo dateTimeInfo;

		// Token: 0x04003962 RID: 14690
		private volatile TextInfo textInfo;

		// Token: 0x04003963 RID: 14691
		internal string m_name;

		// Token: 0x04003964 RID: 14692
		[NonSerialized]
		private string englishname;

		// Token: 0x04003965 RID: 14693
		[NonSerialized]
		private string nativename;

		// Token: 0x04003966 RID: 14694
		[NonSerialized]
		private string iso3lang;

		// Token: 0x04003967 RID: 14695
		[NonSerialized]
		private string iso2lang;

		// Token: 0x04003968 RID: 14696
		[NonSerialized]
		private string win3lang;

		// Token: 0x04003969 RID: 14697
		[NonSerialized]
		private string territory;

		// Token: 0x0400396A RID: 14698
		[NonSerialized]
		private string[] native_calendar_names;

		// Token: 0x0400396B RID: 14699
		private volatile CompareInfo compareInfo;

		// Token: 0x0400396C RID: 14700
		[NonSerialized]
		private unsafe readonly void* textinfo_data;

		// Token: 0x0400396D RID: 14701
		private int m_dataItem;

		// Token: 0x0400396E RID: 14702
		private Calendar calendar;

		// Token: 0x0400396F RID: 14703
		[NonSerialized]
		private CultureInfo parent_culture;

		// Token: 0x04003970 RID: 14704
		[NonSerialized]
		private bool constructed;

		// Token: 0x04003971 RID: 14705
		[NonSerialized]
		internal byte[] cached_serialized_form;

		// Token: 0x04003972 RID: 14706
		[NonSerialized]
		internal CultureData m_cultureData;

		// Token: 0x04003973 RID: 14707
		[NonSerialized]
		internal bool m_isInherited;

		// Token: 0x04003974 RID: 14708
		internal const int InvariantCultureId = 127;

		// Token: 0x04003975 RID: 14709
		private const int CalendarTypeBits = 8;

		// Token: 0x04003976 RID: 14710
		internal const int LOCALE_INVARIANT = 127;

		// Token: 0x04003977 RID: 14711
		private const string MSG_READONLY = "This instance is read only";

		// Token: 0x04003978 RID: 14712
		private static volatile CultureInfo s_DefaultThreadCurrentUICulture;

		// Token: 0x04003979 RID: 14713
		private static volatile CultureInfo s_DefaultThreadCurrentCulture;

		// Token: 0x0400397A RID: 14714
		private static Dictionary<int, CultureInfo> shared_by_number;

		// Token: 0x0400397B RID: 14715
		private static Dictionary<string, CultureInfo> shared_by_name;

		// Token: 0x0400397C RID: 14716
		internal static readonly bool IsTaiwanSku;

		// Token: 0x02000A01 RID: 2561
		private struct Data
		{
			// Token: 0x0400397D RID: 14717
			public int ansi;

			// Token: 0x0400397E RID: 14718
			public int ebcdic;

			// Token: 0x0400397F RID: 14719
			public int mac;

			// Token: 0x04003980 RID: 14720
			public int oem;

			// Token: 0x04003981 RID: 14721
			public bool right_to_left;

			// Token: 0x04003982 RID: 14722
			public byte list_sep;
		}
	}
}
