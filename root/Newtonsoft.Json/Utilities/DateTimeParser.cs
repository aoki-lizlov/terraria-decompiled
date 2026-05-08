using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000045 RID: 69
	internal struct DateTimeParser
	{
		// Token: 0x060003BF RID: 959 RVA: 0x0000F488 File Offset: 0x0000D688
		static DateTimeParser()
		{
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000F56E File Offset: 0x0000D76E
		public bool Parse(char[] text, int startIndex, int length)
		{
			this._text = text;
			this._end = startIndex + length;
			return this.ParseDate(startIndex) && this.ParseChar(DateTimeParser.Lzyyyy_MM_dd + startIndex, 'T') && this.ParseTimeAndZoneAndWhitespace(DateTimeParser.Lzyyyy_MM_ddT + startIndex);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000F5AC File Offset: 0x0000D7AC
		private bool ParseDate(int start)
		{
			return this.Parse4Digit(start, out this.Year) && 1 <= this.Year && this.ParseChar(start + DateTimeParser.Lzyyyy, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_, out this.Month) && 1 <= this.Month && this.Month <= 12 && this.ParseChar(start + DateTimeParser.Lzyyyy_MM, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_MM_, out this.Day) && 1 <= this.Day && this.Day <= DateTime.DaysInMonth(this.Year, this.Month);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000F65D File Offset: 0x0000D85D
		private bool ParseTimeAndZoneAndWhitespace(int start)
		{
			return this.ParseTime(ref start) && this.ParseZone(start);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000F674 File Offset: 0x0000D874
		private bool ParseTime(ref int start)
		{
			if (!this.Parse2Digit(start, out this.Hour) || this.Hour > 24 || !this.ParseChar(start + DateTimeParser.LzHH, ':') || !this.Parse2Digit(start + DateTimeParser.LzHH_, out this.Minute) || this.Minute >= 60 || !this.ParseChar(start + DateTimeParser.LzHH_mm, ':') || !this.Parse2Digit(start + DateTimeParser.LzHH_mm_, out this.Second) || this.Second >= 60 || (this.Hour == 24 && (this.Minute != 0 || this.Second != 0)))
			{
				return false;
			}
			start += DateTimeParser.LzHH_mm_ss;
			if (this.ParseChar(start, '.'))
			{
				this.Fraction = 0;
				int num = 0;
				for (;;)
				{
					int num2 = start + 1;
					start = num2;
					if (num2 >= this._end || num >= 7)
					{
						break;
					}
					int num3 = (int)(this._text[start] - '0');
					if (num3 < 0 || num3 > 9)
					{
						break;
					}
					this.Fraction = this.Fraction * 10 + num3;
					num++;
				}
				if (num < 7)
				{
					if (num == 0)
					{
						return false;
					}
					this.Fraction *= DateTimeParser.Power10[7 - num];
				}
				if (this.Hour == 24 && this.Fraction != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000F7B4 File Offset: 0x0000D9B4
		private bool ParseZone(int start)
		{
			if (start < this._end)
			{
				char c = this._text[start];
				if (c == 'Z' || c == 'z')
				{
					this.Zone = ParserTimeZone.Utc;
					start++;
				}
				else
				{
					if (start + 2 < this._end && this.Parse2Digit(start + DateTimeParser.Lz_, out this.ZoneHour) && this.ZoneHour <= 99)
					{
						if (c != '+')
						{
							if (c == '-')
							{
								this.Zone = ParserTimeZone.LocalWestOfUtc;
								start += DateTimeParser.Lz_zz;
							}
						}
						else
						{
							this.Zone = ParserTimeZone.LocalEastOfUtc;
							start += DateTimeParser.Lz_zz;
						}
					}
					if (start < this._end)
					{
						if (this.ParseChar(start, ':'))
						{
							start++;
							if (start + 1 < this._end && this.Parse2Digit(start, out this.ZoneMinute) && this.ZoneMinute <= 99)
							{
								start += 2;
							}
						}
						else if (start + 1 < this._end && this.Parse2Digit(start, out this.ZoneMinute) && this.ZoneMinute <= 99)
						{
							start += 2;
						}
					}
				}
			}
			return start == this._end;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
		private bool Parse4Digit(int start, out int num)
		{
			if (start + 3 < this._end)
			{
				int num2 = (int)(this._text[start] - '0');
				int num3 = (int)(this._text[start + 1] - '0');
				int num4 = (int)(this._text[start + 2] - '0');
				int num5 = (int)(this._text[start + 3] - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10 && 0 <= num4 && num4 < 10 && 0 <= num5 && num5 < 10)
				{
					num = ((num2 * 10 + num3) * 10 + num4) * 10 + num5;
					return true;
				}
			}
			num = 0;
			return false;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000F94C File Offset: 0x0000DB4C
		private bool Parse2Digit(int start, out int num)
		{
			if (start + 1 < this._end)
			{
				int num2 = (int)(this._text[start] - '0');
				int num3 = (int)(this._text[start + 1] - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10)
				{
					num = num2 * 10 + num3;
					return true;
				}
			}
			num = 0;
			return false;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000F99E File Offset: 0x0000DB9E
		private bool ParseChar(int start, char ch)
		{
			return start < this._end && this._text[start] == ch;
		}

		// Token: 0x040001C4 RID: 452
		public int Year;

		// Token: 0x040001C5 RID: 453
		public int Month;

		// Token: 0x040001C6 RID: 454
		public int Day;

		// Token: 0x040001C7 RID: 455
		public int Hour;

		// Token: 0x040001C8 RID: 456
		public int Minute;

		// Token: 0x040001C9 RID: 457
		public int Second;

		// Token: 0x040001CA RID: 458
		public int Fraction;

		// Token: 0x040001CB RID: 459
		public int ZoneHour;

		// Token: 0x040001CC RID: 460
		public int ZoneMinute;

		// Token: 0x040001CD RID: 461
		public ParserTimeZone Zone;

		// Token: 0x040001CE RID: 462
		private char[] _text;

		// Token: 0x040001CF RID: 463
		private int _end;

		// Token: 0x040001D0 RID: 464
		private static readonly int[] Power10 = new int[] { -1, 10, 100, 1000, 10000, 100000, 1000000 };

		// Token: 0x040001D1 RID: 465
		private static readonly int Lzyyyy = "yyyy".Length;

		// Token: 0x040001D2 RID: 466
		private static readonly int Lzyyyy_ = "yyyy-".Length;

		// Token: 0x040001D3 RID: 467
		private static readonly int Lzyyyy_MM = "yyyy-MM".Length;

		// Token: 0x040001D4 RID: 468
		private static readonly int Lzyyyy_MM_ = "yyyy-MM-".Length;

		// Token: 0x040001D5 RID: 469
		private static readonly int Lzyyyy_MM_dd = "yyyy-MM-dd".Length;

		// Token: 0x040001D6 RID: 470
		private static readonly int Lzyyyy_MM_ddT = "yyyy-MM-ddT".Length;

		// Token: 0x040001D7 RID: 471
		private static readonly int LzHH = "HH".Length;

		// Token: 0x040001D8 RID: 472
		private static readonly int LzHH_ = "HH:".Length;

		// Token: 0x040001D9 RID: 473
		private static readonly int LzHH_mm = "HH:mm".Length;

		// Token: 0x040001DA RID: 474
		private static readonly int LzHH_mm_ = "HH:mm:".Length;

		// Token: 0x040001DB RID: 475
		private static readonly int LzHH_mm_ss = "HH:mm:ss".Length;

		// Token: 0x040001DC RID: 476
		private static readonly int Lz_ = "-".Length;

		// Token: 0x040001DD RID: 477
		private static readonly int Lz_zz = "-zz".Length;

		// Token: 0x040001DE RID: 478
		private const short MaxFractionDigits = 7;
	}
}
