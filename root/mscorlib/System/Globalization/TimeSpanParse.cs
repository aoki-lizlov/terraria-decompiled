using System;
using System.Text;

namespace System.Globalization
{
	// Token: 0x020009CB RID: 2507
	internal static class TimeSpanParse
	{
		// Token: 0x06005B78 RID: 23416 RVA: 0x00137EE8 File Offset: 0x001360E8
		internal static long Pow10(int pow)
		{
			switch (pow)
			{
			case 0:
				return 1L;
			case 1:
				return 10L;
			case 2:
				return 100L;
			case 3:
				return 1000L;
			case 4:
				return 10000L;
			case 5:
				return 100000L;
			case 6:
				return 1000000L;
			case 7:
				return 10000000L;
			default:
				return (long)Math.Pow(10.0, (double)pow);
			}
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x00137F5C File Offset: 0x0013615C
		private static bool TryTimeToTicks(bool positive, TimeSpanParse.TimeSpanToken days, TimeSpanParse.TimeSpanToken hours, TimeSpanParse.TimeSpanToken minutes, TimeSpanParse.TimeSpanToken seconds, TimeSpanParse.TimeSpanToken fraction, out long result)
		{
			if (days._num > 10675199 || hours._num > 23 || minutes._num > 59 || seconds._num > 59 || fraction.IsInvalidFraction())
			{
				result = 0L;
				return false;
			}
			long num = ((long)days._num * 3600L * 24L + (long)hours._num * 3600L + (long)minutes._num * 60L + (long)seconds._num) * 1000L;
			if (num > 922337203685477L || num < -922337203685477L)
			{
				result = 0L;
				return false;
			}
			long num2 = (long)fraction._num;
			if (num2 != 0L)
			{
				long num3 = 1000000L;
				if (fraction._zeroes > 0)
				{
					long num4 = TimeSpanParse.Pow10(fraction._zeroes);
					num3 /= num4;
				}
				while (num2 < num3)
				{
					num2 *= 10L;
				}
			}
			result = num * 10000L + num2;
			if (positive && result < 0L)
			{
				result = 0L;
				return false;
			}
			return true;
		}

		// Token: 0x06005B7A RID: 23418 RVA: 0x0013805C File Offset: 0x0013625C
		internal static TimeSpan Parse(ReadOnlySpan<char> input, IFormatProvider formatProvider)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(true);
			TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref timeSpanResult);
			return timeSpanResult.parsedTimeSpan;
		}

		// Token: 0x06005B7B RID: 23419 RVA: 0x00138084 File Offset: 0x00136284
		internal static bool TryParse(ReadOnlySpan<char> input, IFormatProvider formatProvider, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(false);
			if (TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x001380BC File Offset: 0x001362BC
		internal static TimeSpan ParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(true);
			TimeSpanParse.TryParseExactTimeSpan(input, format, formatProvider, styles, ref timeSpanResult);
			return timeSpanResult.parsedTimeSpan;
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x001380E4 File Offset: 0x001362E4
		internal static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(false);
			if (TimeSpanParse.TryParseExactTimeSpan(input, format, formatProvider, styles, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x06005B7E RID: 23422 RVA: 0x00138120 File Offset: 0x00136320
		internal static TimeSpan ParseExactMultiple(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(true);
			TimeSpanParse.TryParseExactMultipleTimeSpan(input, formats, formatProvider, styles, ref timeSpanResult);
			return timeSpanResult.parsedTimeSpan;
		}

		// Token: 0x06005B7F RID: 23423 RVA: 0x00138148 File Offset: 0x00136348
		internal static bool TryParseExactMultiple(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(false);
			if (TimeSpanParse.TryParseExactMultipleTimeSpan(input, formats, formatProvider, styles, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x00138184 File Offset: 0x00136384
		private static bool TryParseTimeSpan(ReadOnlySpan<char> input, TimeSpanParse.TimeSpanStandardStyles style, IFormatProvider formatProvider, ref TimeSpanParse.TimeSpanResult result)
		{
			input = input.Trim();
			if (input.IsEmpty)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			TimeSpanParse.TimeSpanTokenizer timeSpanTokenizer = new TimeSpanParse.TimeSpanTokenizer(input);
			TimeSpanParse.TimeSpanRawInfo timeSpanRawInfo = default(TimeSpanParse.TimeSpanRawInfo);
			timeSpanRawInfo.Init(DateTimeFormatInfo.GetInstance(formatProvider));
			TimeSpanParse.TimeSpanToken timeSpanToken = timeSpanTokenizer.GetNextToken();
			while (timeSpanToken._ttt != TimeSpanParse.TTT.End)
			{
				if (!timeSpanRawInfo.ProcessToken(ref timeSpanToken, ref result))
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				timeSpanToken = timeSpanTokenizer.GetNextToken();
			}
			return TimeSpanParse.ProcessTerminalState(ref timeSpanRawInfo, style, ref result) || result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
		}

		// Token: 0x06005B81 RID: 23425 RVA: 0x00138220 File Offset: 0x00136420
		private static bool ProcessTerminalState(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._lastSeenTTT == TimeSpanParse.TTT.Num)
			{
				TimeSpanParse.TimeSpanToken timeSpanToken = default(TimeSpanParse.TimeSpanToken);
				timeSpanToken._ttt = TimeSpanParse.TTT.Sep;
				if (!raw.ProcessToken(ref timeSpanToken, ref result))
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
			}
			switch (raw._numCount)
			{
			case 1:
				return TimeSpanParse.ProcessTerminal_D(ref raw, style, ref result);
			case 2:
				return TimeSpanParse.ProcessTerminal_HM(ref raw, style, ref result);
			case 3:
				return TimeSpanParse.ProcessTerminal_HM_S_D(ref raw, style, ref result);
			case 4:
				return TimeSpanParse.ProcessTerminal_HMS_F_D(ref raw, style, ref result);
			case 5:
				return TimeSpanParse.ProcessTerminal_DHMSF(ref raw, style, ref result);
			default:
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
		}

		// Token: 0x06005B82 RID: 23426 RVA: 0x001382C0 File Offset: 0x001364C0
		private static bool ProcessTerminal_DHMSF(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 6)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			long num;
			if (!TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, raw._numbers4, out num))
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
				}
			}
			result.parsedTimeSpan = new TimeSpan(num);
			return true;
		}

		// Token: 0x06005B83 RID: 23427 RVA: 0x001383BC File Offset: 0x001365BC
		private static bool ProcessTerminal_HMS_F_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 5 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			long num = 0L;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			if (flag)
			{
				if (raw.FullHMSFMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSFMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMSFMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSFMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, raw._numbers3, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, raw._numbers3, out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag4)
			{
				if (!flag3)
				{
					num = -num;
					if (num > 0L)
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
					}
				}
				result.parsedTimeSpan = new TimeSpan(num);
				return true;
			}
			if (!flag5)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x00138780 File Offset: 0x00136980
		private static bool ProcessTerminal_HM_S_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 4 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			long num = 0L;
			if (flag)
			{
				if (raw.FullHMSMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMSMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, raw._numbers1, raw._numbers2, timeSpanToken, timeSpanToken, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, raw._numbers2, out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag4)
			{
				if (!flag3)
				{
					num = -num;
					if (num > 0L)
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
					}
				}
				result.parsedTimeSpan = new TimeSpan(num);
				return true;
			}
			if (!flag5)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x00138AFC File Offset: 0x00136CFC
		private static bool ProcessTerminal_HM(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 3 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullHMMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullHMMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullHMMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			long num = 0L;
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			if (!TimeSpanParse.TryTimeToTicks(flag3, timeSpanToken, raw._numbers0, raw._numbers1, timeSpanToken, timeSpanToken, out num))
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
				}
			}
			result.parsedTimeSpan = new TimeSpan(num);
			return true;
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x00138BFC File Offset: 0x00136DFC
		private static bool ProcessTerminal_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw._sepCount != 2 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullDMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullDMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullDMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullDMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			long num = 0L;
			TimeSpanParse.TimeSpanToken timeSpanToken = new TimeSpanParse.TimeSpanToken(0);
			if (!TimeSpanParse.TryTimeToTicks(flag3, raw._numbers0, timeSpanToken, timeSpanToken, timeSpanToken, timeSpanToken, out num))
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
				}
			}
			result.parsedTimeSpan = new TimeSpan(num);
			return true;
		}

		// Token: 0x06005B87 RID: 23431 RVA: 0x00138CF8 File Offset: 0x00136EF8
		private unsafe static bool TryParseExactTimeSpan(ReadOnlySpan<char> input, ReadOnlySpan<char> format, IFormatProvider formatProvider, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			if (format.Length == 0)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format specifier was invalid.", null, null);
			}
			if (format.Length == 1)
			{
				char c = (char)(*format[0]);
				if (c <= 'T')
				{
					if (c == 'G')
					{
						return TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Localized | TimeSpanParse.TimeSpanStandardStyles.RequireFull, formatProvider, ref result);
					}
					if (c != 'T')
					{
						goto IL_006C;
					}
				}
				else if (c != 'c')
				{
					if (c == 'g')
					{
						return TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Localized, formatProvider, ref result);
					}
					if (c != 't')
					{
						goto IL_006C;
					}
				}
				return TimeSpanParse.TryParseTimeSpanConstant(input, ref result);
				IL_006C:
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format specifier was invalid.", null, null);
			}
			return TimeSpanParse.TryParseByFormat(input, format, styles, ref result);
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x00138D8C File Offset: 0x00136F8C
		private unsafe static bool TryParseByFormat(ReadOnlySpan<char> input, ReadOnlySpan<char> format, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int i = 0;
			int num7 = 0;
			TimeSpanParse.TimeSpanTokenizer timeSpanTokenizer = new TimeSpanParse.TimeSpanTokenizer(input, -1);
			while (i < format.Length)
			{
				char c = (char)(*format[i]);
				if (c <= 'F')
				{
					if (c <= '%')
					{
						if (c != '"')
						{
							if (c != '%')
							{
								goto IL_02E1;
							}
							int num8 = DateTimeFormat.ParseNextChar(format, i);
							if (num8 >= 0 && num8 != 37)
							{
								num7 = 1;
								goto IL_02F0;
							}
							return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
						}
					}
					else if (c != '\'')
					{
						if (c != 'F')
						{
							goto IL_02E1;
						}
						num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						if (num7 > 7 || flag5)
						{
							return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
						}
						TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, num7, out num5, out num6);
						flag5 = true;
						goto IL_02F0;
					}
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					if (!DateTimeParse.TryParseQuoteString(format, i, stringBuilder, out num7))
					{
						StringBuilderCache.Release(stringBuilder);
						return result.SetFailure(TimeSpanParse.ParseFailureKind.FormatWithParameter, "Cannot find a matching quote character for the character '{0}'.", c, null);
					}
					if (!TimeSpanParse.ParseExactLiteral(ref timeSpanTokenizer, stringBuilder))
					{
						StringBuilderCache.Release(stringBuilder);
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
					}
					StringBuilderCache.Release(stringBuilder);
				}
				else if (c <= 'h')
				{
					if (c != '\\')
					{
						switch (c)
						{
						case 'd':
						{
							num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int num9 = 0;
							if (num7 > 8 || flag || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, (num7 < 2) ? 1 : num7, (num7 < 2) ? 8 : num7, out num9, out num))
							{
								return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
							}
							flag = true;
							break;
						}
						case 'e':
						case 'g':
							goto IL_02E1;
						case 'f':
							num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num7 > 7 || flag5 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, num7, out num5, out num6))
							{
								return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
							}
							flag5 = true;
							break;
						case 'h':
							num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num7 > 2 || flag2 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, out num2))
							{
								return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
							}
							flag2 = true;
							break;
						default:
							goto IL_02E1;
						}
					}
					else
					{
						int num8 = DateTimeFormat.ParseNextChar(format, i);
						if (num8 < 0 || timeSpanTokenizer.NextChar != (char)num8)
						{
							return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
						}
						num7 = 2;
					}
				}
				else if (c != 'm')
				{
					if (c != 's')
					{
						goto IL_02E1;
					}
					num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num7 > 2 || flag4 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, out num4))
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
					}
					flag4 = true;
				}
				else
				{
					num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num7 > 2 || flag3 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, out num3))
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
					}
					flag3 = true;
				}
				IL_02F0:
				i += num7;
				continue;
				IL_02E1:
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Input string was not in a correct format.", null, null);
			}
			if (!timeSpanTokenizer.EOL)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			bool flag6 = (styles & TimeSpanStyles.AssumeNegative) == TimeSpanStyles.None;
			long num10;
			if (TimeSpanParse.TryTimeToTicks(flag6, new TimeSpanParse.TimeSpanToken(num), new TimeSpanParse.TimeSpanToken(num2), new TimeSpanParse.TimeSpanToken(num3), new TimeSpanParse.TimeSpanToken(num4), new TimeSpanParse.TimeSpanToken(num6, num5), out num10))
			{
				if (!flag6)
				{
					num10 = -num10;
				}
				result.parsedTimeSpan = new TimeSpan(num10);
				return true;
			}
			return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x00139114 File Offset: 0x00137314
		private static bool ParseExactDigits(ref TimeSpanParse.TimeSpanTokenizer tokenizer, int minDigitLength, out int result)
		{
			result = 0;
			int num = 0;
			int num2 = ((minDigitLength == 1) ? 2 : minDigitLength);
			return TimeSpanParse.ParseExactDigits(ref tokenizer, minDigitLength, num2, out num, out result);
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x0013913C File Offset: 0x0013733C
		private static bool ParseExactDigits(ref TimeSpanParse.TimeSpanTokenizer tokenizer, int minDigitLength, int maxDigitLength, out int zeroes, out int result)
		{
			int num = 0;
			int num2 = 0;
			int i;
			for (i = 0; i < maxDigitLength; i++)
			{
				char nextChar = tokenizer.NextChar;
				if (nextChar < '0' || nextChar > '9')
				{
					tokenizer.BackOne();
					break;
				}
				num = num * 10 + (int)(nextChar - '0');
				if (num == 0)
				{
					num2++;
				}
			}
			zeroes = num2;
			result = num;
			return i >= minDigitLength;
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x00139194 File Offset: 0x00137394
		private static bool ParseExactLiteral(ref TimeSpanParse.TimeSpanTokenizer tokenizer, StringBuilder enquotedString)
		{
			for (int i = 0; i < enquotedString.Length; i++)
			{
				if (enquotedString[i] != tokenizer.NextChar)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005B8C RID: 23436 RVA: 0x001391C4 File Offset: 0x001373C4
		private static bool TryParseTimeSpanConstant(ReadOnlySpan<char> input, ref TimeSpanParse.TimeSpanResult result)
		{
			return default(TimeSpanParse.StringParser).TryParse(input, ref result);
		}

		// Token: 0x06005B8D RID: 23437 RVA: 0x001391E4 File Offset: 0x001373E4
		private static bool TryParseExactMultipleTimeSpan(ReadOnlySpan<char> input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			if (formats == null)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "String reference not set to an instance of a String.", null, "formats");
			}
			if (input.Length == 0)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
			}
			if (formats.Length == 0)
			{
				return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format specifier was invalid.", null, null);
			}
			for (int i = 0; i < formats.Length; i++)
			{
				if (formats[i] == null || formats[i].Length == 0)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format specifier was invalid.", null, null);
				}
				TimeSpanParse.TimeSpanResult timeSpanResult = new TimeSpanParse.TimeSpanResult(false);
				if (TimeSpanParse.TryParseExactTimeSpan(input, formats[i], formatProvider, styles, ref timeSpanResult))
				{
					result.parsedTimeSpan = timeSpanResult.parsedTimeSpan;
					return true;
				}
			}
			return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
		}

		// Token: 0x06005B8E RID: 23438 RVA: 0x0013929C File Offset: 0x0013749C
		internal static void ValidateStyles(TimeSpanStyles style, string parameterName)
		{
			if (style != TimeSpanStyles.None && style != TimeSpanStyles.AssumeNegative)
			{
				throw new ArgumentException(Environment.GetResourceString("An undefined TimeSpanStyles value is being used."), parameterName);
			}
		}

		// Token: 0x0400371F RID: 14111
		private const int MaxFractionDigits = 7;

		// Token: 0x04003720 RID: 14112
		private const int MaxDays = 10675199;

		// Token: 0x04003721 RID: 14113
		private const int MaxHours = 23;

		// Token: 0x04003722 RID: 14114
		private const int MaxMinutes = 59;

		// Token: 0x04003723 RID: 14115
		private const int MaxSeconds = 59;

		// Token: 0x04003724 RID: 14116
		private const int MaxFraction = 9999999;

		// Token: 0x020009CC RID: 2508
		private enum ParseFailureKind : byte
		{
			// Token: 0x04003726 RID: 14118
			None,
			// Token: 0x04003727 RID: 14119
			ArgumentNull,
			// Token: 0x04003728 RID: 14120
			Format,
			// Token: 0x04003729 RID: 14121
			FormatWithParameter,
			// Token: 0x0400372A RID: 14122
			Overflow
		}

		// Token: 0x020009CD RID: 2509
		[Flags]
		private enum TimeSpanStandardStyles : byte
		{
			// Token: 0x0400372C RID: 14124
			None = 0,
			// Token: 0x0400372D RID: 14125
			Invariant = 1,
			// Token: 0x0400372E RID: 14126
			Localized = 2,
			// Token: 0x0400372F RID: 14127
			RequireFull = 4,
			// Token: 0x04003730 RID: 14128
			Any = 3
		}

		// Token: 0x020009CE RID: 2510
		private enum TTT : byte
		{
			// Token: 0x04003732 RID: 14130
			None,
			// Token: 0x04003733 RID: 14131
			End,
			// Token: 0x04003734 RID: 14132
			Num,
			// Token: 0x04003735 RID: 14133
			Sep,
			// Token: 0x04003736 RID: 14134
			NumOverflow
		}

		// Token: 0x020009CF RID: 2511
		private ref struct TimeSpanToken
		{
			// Token: 0x06005B8F RID: 23439 RVA: 0x001392B8 File Offset: 0x001374B8
			public TimeSpanToken(TimeSpanParse.TTT type)
			{
				this = new TimeSpanParse.TimeSpanToken(type, 0, 0, default(ReadOnlySpan<char>));
			}

			// Token: 0x06005B90 RID: 23440 RVA: 0x001392D8 File Offset: 0x001374D8
			public TimeSpanToken(int number)
			{
				this = new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, number, 0, default(ReadOnlySpan<char>));
			}

			// Token: 0x06005B91 RID: 23441 RVA: 0x001392F8 File Offset: 0x001374F8
			public TimeSpanToken(int number, int leadingZeroes)
			{
				this = new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, number, leadingZeroes, default(ReadOnlySpan<char>));
			}

			// Token: 0x06005B92 RID: 23442 RVA: 0x00139317 File Offset: 0x00137517
			public TimeSpanToken(TimeSpanParse.TTT type, int number, int leadingZeroes, ReadOnlySpan<char> separator)
			{
				this._ttt = type;
				this._num = number;
				this._zeroes = leadingZeroes;
				this._sep = separator;
			}

			// Token: 0x06005B93 RID: 23443 RVA: 0x00139338 File Offset: 0x00137538
			public bool IsInvalidFraction()
			{
				return this._num > 9999999 || this._zeroes > 7 || (this._num != 0 && this._zeroes != 0 && (long)this._num >= 9999999L / TimeSpanParse.Pow10(this._zeroes - 1));
			}

			// Token: 0x04003737 RID: 14135
			internal TimeSpanParse.TTT _ttt;

			// Token: 0x04003738 RID: 14136
			internal int _num;

			// Token: 0x04003739 RID: 14137
			internal int _zeroes;

			// Token: 0x0400373A RID: 14138
			internal ReadOnlySpan<char> _sep;
		}

		// Token: 0x020009D0 RID: 2512
		private ref struct TimeSpanTokenizer
		{
			// Token: 0x06005B94 RID: 23444 RVA: 0x0013938F File Offset: 0x0013758F
			internal TimeSpanTokenizer(ReadOnlySpan<char> input)
			{
				this = new TimeSpanParse.TimeSpanTokenizer(input, 0);
			}

			// Token: 0x06005B95 RID: 23445 RVA: 0x00139399 File Offset: 0x00137599
			internal TimeSpanTokenizer(ReadOnlySpan<char> input, int startPosition)
			{
				this._value = input;
				this._pos = startPosition;
			}

			// Token: 0x06005B96 RID: 23446 RVA: 0x001393AC File Offset: 0x001375AC
			internal unsafe TimeSpanParse.TimeSpanToken GetNextToken()
			{
				int pos = this._pos;
				if (pos >= this._value.Length)
				{
					return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.End);
				}
				int num = (int)(*this._value[pos] - 48);
				if (num <= 9)
				{
					int num2 = 0;
					if (num == 0)
					{
						num2 = 1;
						int num4;
						for (;;)
						{
							int num3 = this._pos + 1;
							this._pos = num3;
							if (num3 >= this._value.Length || (num4 = (int)(*this._value[this._pos] - 48)) > 9)
							{
								break;
							}
							if (num4 != 0)
							{
								goto IL_0099;
							}
							num2++;
						}
						return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, 0, num2, default(ReadOnlySpan<char>));
						IL_0099:
						num = num4;
					}
					do
					{
						int num3 = this._pos + 1;
						this._pos = num3;
						if (num3 >= this._value.Length)
						{
							goto IL_00F6;
						}
						int num5 = (int)(*this._value[this._pos] - 48);
						if (num5 > 9)
						{
							goto IL_00F6;
						}
						num = num * 10 + num5;
					}
					while (((long)num & (long)((ulong)(-268435456))) == 0L);
					return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.NumOverflow);
					IL_00F6:
					return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Num, num, num2, default(ReadOnlySpan<char>));
				}
				int num6 = 1;
				for (;;)
				{
					int num3 = this._pos + 1;
					this._pos = num3;
					if (num3 >= this._value.Length || *this._value[this._pos] - 48 <= 9)
					{
						break;
					}
					num6++;
				}
				return new TimeSpanParse.TimeSpanToken(TimeSpanParse.TTT.Sep, 0, 0, this._value.Slice(pos, num6));
			}

			// Token: 0x17000EFD RID: 3837
			// (get) Token: 0x06005B97 RID: 23447 RVA: 0x00139519 File Offset: 0x00137719
			internal bool EOL
			{
				get
				{
					return this._pos >= this._value.Length - 1;
				}
			}

			// Token: 0x06005B98 RID: 23448 RVA: 0x00139533 File Offset: 0x00137733
			internal void BackOne()
			{
				if (this._pos > 0)
				{
					this._pos--;
				}
			}

			// Token: 0x17000EFE RID: 3838
			// (get) Token: 0x06005B99 RID: 23449 RVA: 0x0013954C File Offset: 0x0013774C
			internal unsafe char NextChar
			{
				get
				{
					int num = this._pos + 1;
					this._pos = num;
					int num2 = num;
					if (num2 >= this._value.Length)
					{
						return '\0';
					}
					return (char)(*this._value[num2]);
				}
			}

			// Token: 0x0400373B RID: 14139
			private ReadOnlySpan<char> _value;

			// Token: 0x0400373C RID: 14140
			private int _pos;
		}

		// Token: 0x020009D1 RID: 2513
		private ref struct TimeSpanRawInfo
		{
			// Token: 0x17000EFF RID: 3839
			// (get) Token: 0x06005B9A RID: 23450 RVA: 0x00139588 File Offset: 0x00137788
			internal TimeSpanFormat.FormatLiterals PositiveInvariant
			{
				get
				{
					return TimeSpanFormat.PositiveInvariantFormatLiterals;
				}
			}

			// Token: 0x17000F00 RID: 3840
			// (get) Token: 0x06005B9B RID: 23451 RVA: 0x0013958F File Offset: 0x0013778F
			internal TimeSpanFormat.FormatLiterals NegativeInvariant
			{
				get
				{
					return TimeSpanFormat.NegativeInvariantFormatLiterals;
				}
			}

			// Token: 0x17000F01 RID: 3841
			// (get) Token: 0x06005B9C RID: 23452 RVA: 0x00139596 File Offset: 0x00137796
			internal TimeSpanFormat.FormatLiterals PositiveLocalized
			{
				get
				{
					if (!this._posLocInit)
					{
						this._posLoc = default(TimeSpanFormat.FormatLiterals);
						this._posLoc.Init(this._fullPosPattern, false);
						this._posLocInit = true;
					}
					return this._posLoc;
				}
			}

			// Token: 0x17000F02 RID: 3842
			// (get) Token: 0x06005B9D RID: 23453 RVA: 0x001395D0 File Offset: 0x001377D0
			internal TimeSpanFormat.FormatLiterals NegativeLocalized
			{
				get
				{
					if (!this._negLocInit)
					{
						this._negLoc = default(TimeSpanFormat.FormatLiterals);
						this._negLoc.Init(this._fullNegPattern, false);
						this._negLocInit = true;
					}
					return this._negLoc;
				}
			}

			// Token: 0x06005B9E RID: 23454 RVA: 0x0013960C File Offset: 0x0013780C
			internal bool FullAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 5 && this._numCount == 4 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.AppCompatLiteral) && this._literals4.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06005B9F RID: 23455 RVA: 0x001396AC File Offset: 0x001378AC
			internal bool PartialAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 4 && this._numCount == 3 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.AppCompatLiteral) && this._literals3.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06005BA0 RID: 23456 RVA: 0x00139730 File Offset: 0x00137930
			internal bool FullMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 6 && this._numCount == 5 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals4.EqualsOrdinal(pattern.SecondFractionSep) && this._literals5.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06005BA1 RID: 23457 RVA: 0x001397EC File Offset: 0x001379EC
			internal bool FullDMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 2 && this._numCount == 1 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06005BA2 RID: 23458 RVA: 0x00139840 File Offset: 0x00137A40
			internal bool FullHMMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 3 && this._numCount == 2 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06005BA3 RID: 23459 RVA: 0x001398AC File Offset: 0x00137AAC
			internal bool FullDHMMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 4 && this._numCount == 3 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06005BA4 RID: 23460 RVA: 0x00139930 File Offset: 0x00137B30
			internal bool FullHMSMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 4 && this._numCount == 3 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals3.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06005BA5 RID: 23461 RVA: 0x001399B4 File Offset: 0x00137BB4
			internal bool FullDHMSMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 5 && this._numCount == 4 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.DayHourSep) && this._literals2.EqualsOrdinal(pattern.HourMinuteSep) && this._literals3.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals4.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06005BA6 RID: 23462 RVA: 0x00139A54 File Offset: 0x00137C54
			internal bool FullHMSFMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this._sepCount == 5 && this._numCount == 4 && this._literals0.EqualsOrdinal(pattern.Start) && this._literals1.EqualsOrdinal(pattern.HourMinuteSep) && this._literals2.EqualsOrdinal(pattern.MinuteSecondSep) && this._literals3.EqualsOrdinal(pattern.SecondFractionSep) && this._literals4.EqualsOrdinal(pattern.End);
			}

			// Token: 0x06005BA7 RID: 23463 RVA: 0x00139AF4 File Offset: 0x00137CF4
			internal void Init(DateTimeFormatInfo dtfi)
			{
				this._lastSeenTTT = TimeSpanParse.TTT.None;
				this._tokenCount = 0;
				this._sepCount = 0;
				this._numCount = 0;
				this._fullPosPattern = dtfi.FullTimeSpanPositivePattern;
				this._fullNegPattern = dtfi.FullTimeSpanNegativePattern;
				this._posLocInit = false;
				this._negLocInit = false;
			}

			// Token: 0x06005BA8 RID: 23464 RVA: 0x00139B44 File Offset: 0x00137D44
			internal bool ProcessToken(ref TimeSpanParse.TimeSpanToken tok, ref TimeSpanParse.TimeSpanResult result)
			{
				switch (tok._ttt)
				{
				case TimeSpanParse.TTT.Num:
					if ((this._tokenCount == 0 && !this.AddSep(default(ReadOnlySpan<char>), ref result)) || !this.AddNum(tok, ref result))
					{
						return false;
					}
					break;
				case TimeSpanParse.TTT.Sep:
					if (!this.AddSep(tok._sep, ref result))
					{
						return false;
					}
					break;
				case TimeSpanParse.TTT.NumOverflow:
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
				default:
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				this._lastSeenTTT = tok._ttt;
				return true;
			}

			// Token: 0x06005BA9 RID: 23465 RVA: 0x00139BD8 File Offset: 0x00137DD8
			private bool AddSep(ReadOnlySpan<char> sep, ref TimeSpanParse.TimeSpanResult result)
			{
				if (this._sepCount >= 6 || this._tokenCount >= 11)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				int sepCount = this._sepCount;
				this._sepCount = sepCount + 1;
				switch (sepCount)
				{
				case 0:
					this._literals0 = sep;
					break;
				case 1:
					this._literals1 = sep;
					break;
				case 2:
					this._literals2 = sep;
					break;
				case 3:
					this._literals3 = sep;
					break;
				case 4:
					this._literals4 = sep;
					break;
				default:
					this._literals5 = sep;
					break;
				}
				this._tokenCount++;
				return true;
			}

			// Token: 0x06005BAA RID: 23466 RVA: 0x00139C78 File Offset: 0x00137E78
			private bool AddNum(TimeSpanParse.TimeSpanToken num, ref TimeSpanParse.TimeSpanResult result)
			{
				if (this._numCount >= 5 || this._tokenCount >= 11)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				int numCount = this._numCount;
				this._numCount = numCount + 1;
				switch (numCount)
				{
				case 0:
					this._numbers0 = num;
					break;
				case 1:
					this._numbers1 = num;
					break;
				case 2:
					this._numbers2 = num;
					break;
				case 3:
					this._numbers3 = num;
					break;
				default:
					this._numbers4 = num;
					break;
				}
				this._tokenCount++;
				return true;
			}

			// Token: 0x0400373D RID: 14141
			internal TimeSpanParse.TTT _lastSeenTTT;

			// Token: 0x0400373E RID: 14142
			internal int _tokenCount;

			// Token: 0x0400373F RID: 14143
			internal int _sepCount;

			// Token: 0x04003740 RID: 14144
			internal int _numCount;

			// Token: 0x04003741 RID: 14145
			private TimeSpanFormat.FormatLiterals _posLoc;

			// Token: 0x04003742 RID: 14146
			private TimeSpanFormat.FormatLiterals _negLoc;

			// Token: 0x04003743 RID: 14147
			private bool _posLocInit;

			// Token: 0x04003744 RID: 14148
			private bool _negLocInit;

			// Token: 0x04003745 RID: 14149
			private string _fullPosPattern;

			// Token: 0x04003746 RID: 14150
			private string _fullNegPattern;

			// Token: 0x04003747 RID: 14151
			private const int MaxTokens = 11;

			// Token: 0x04003748 RID: 14152
			private const int MaxLiteralTokens = 6;

			// Token: 0x04003749 RID: 14153
			private const int MaxNumericTokens = 5;

			// Token: 0x0400374A RID: 14154
			internal TimeSpanParse.TimeSpanToken _numbers0;

			// Token: 0x0400374B RID: 14155
			internal TimeSpanParse.TimeSpanToken _numbers1;

			// Token: 0x0400374C RID: 14156
			internal TimeSpanParse.TimeSpanToken _numbers2;

			// Token: 0x0400374D RID: 14157
			internal TimeSpanParse.TimeSpanToken _numbers3;

			// Token: 0x0400374E RID: 14158
			internal TimeSpanParse.TimeSpanToken _numbers4;

			// Token: 0x0400374F RID: 14159
			internal ReadOnlySpan<char> _literals0;

			// Token: 0x04003750 RID: 14160
			internal ReadOnlySpan<char> _literals1;

			// Token: 0x04003751 RID: 14161
			internal ReadOnlySpan<char> _literals2;

			// Token: 0x04003752 RID: 14162
			internal ReadOnlySpan<char> _literals3;

			// Token: 0x04003753 RID: 14163
			internal ReadOnlySpan<char> _literals4;

			// Token: 0x04003754 RID: 14164
			internal ReadOnlySpan<char> _literals5;
		}

		// Token: 0x020009D2 RID: 2514
		private struct TimeSpanResult
		{
			// Token: 0x06005BAB RID: 23467 RVA: 0x00139D0B File Offset: 0x00137F0B
			internal TimeSpanResult(bool throwOnFailure)
			{
				this.parsedTimeSpan = default(TimeSpan);
				this._throwOnFailure = throwOnFailure;
			}

			// Token: 0x06005BAC RID: 23468 RVA: 0x00139D20 File Offset: 0x00137F20
			internal bool SetFailure(TimeSpanParse.ParseFailureKind kind, string resourceKey, object messageArgument = null, string argumentName = null)
			{
				if (!this._throwOnFailure)
				{
					return false;
				}
				string resourceString = SR.GetResourceString(resourceKey);
				switch (kind)
				{
				case TimeSpanParse.ParseFailureKind.ArgumentNull:
					throw new ArgumentNullException(argumentName, resourceString);
				case TimeSpanParse.ParseFailureKind.FormatWithParameter:
					throw new FormatException(SR.Format(resourceString, messageArgument));
				case TimeSpanParse.ParseFailureKind.Overflow:
					throw new OverflowException(resourceString);
				}
				throw new FormatException(resourceString);
			}

			// Token: 0x04003755 RID: 14165
			internal TimeSpan parsedTimeSpan;

			// Token: 0x04003756 RID: 14166
			private readonly bool _throwOnFailure;
		}

		// Token: 0x020009D3 RID: 2515
		private ref struct StringParser
		{
			// Token: 0x06005BAD RID: 23469 RVA: 0x00139D7C File Offset: 0x00137F7C
			internal unsafe void NextChar()
			{
				if (this._pos < this._len)
				{
					this._pos++;
				}
				this._ch = (char)((this._pos < this._len) ? (*this._str[this._pos]) : 0);
			}

			// Token: 0x06005BAE RID: 23470 RVA: 0x00139DD0 File Offset: 0x00137FD0
			internal unsafe char NextNonDigit()
			{
				for (int i = this._pos; i < this._len; i++)
				{
					char c = (char)(*this._str[i]);
					if (c < '0' || c > '9')
					{
						return c;
					}
				}
				return '\0';
			}

			// Token: 0x06005BAF RID: 23471 RVA: 0x00139E10 File Offset: 0x00138010
			internal bool TryParse(ReadOnlySpan<char> input, ref TimeSpanParse.TimeSpanResult result)
			{
				result.parsedTimeSpan = default(TimeSpan);
				this._str = input;
				this._len = input.Length;
				this._pos = -1;
				this.NextChar();
				this.SkipBlanks();
				bool flag = false;
				if (this._ch == '-')
				{
					flag = true;
					this.NextChar();
				}
				long num;
				if (this.NextNonDigit() == ':')
				{
					if (!this.ParseTime(out num, ref result))
					{
						return false;
					}
				}
				else
				{
					int num2;
					if (!this.ParseInt(10675199, out num2, ref result))
					{
						return false;
					}
					num = (long)num2 * 864000000000L;
					if (this._ch == '.')
					{
						this.NextChar();
						long num3;
						if (!this.ParseTime(out num3, ref result))
						{
							return false;
						}
						num += num3;
					}
				}
				if (flag)
				{
					num = -num;
					if (num > 0L)
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
					}
				}
				else if (num < 0L)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
				}
				this.SkipBlanks();
				if (this._pos < this._len)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				result.parsedTimeSpan = new TimeSpan(num);
				return true;
			}

			// Token: 0x06005BB0 RID: 23472 RVA: 0x00139F1C File Offset: 0x0013811C
			internal bool ParseInt(int max, out int i, ref TimeSpanParse.TimeSpanResult result)
			{
				i = 0;
				int pos = this._pos;
				while (this._ch >= '0' && this._ch <= '9')
				{
					if (((long)i & (long)((ulong)(-268435456))) != 0L)
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
					}
					i = i * 10 + (int)this._ch - 48;
					if (i < 0)
					{
						return result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
					}
					this.NextChar();
				}
				if (pos == this._pos)
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				return i <= max || result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.", null, null);
			}

			// Token: 0x06005BB1 RID: 23473 RVA: 0x00139FBC File Offset: 0x001381BC
			internal bool ParseTime(out long time, ref TimeSpanParse.TimeSpanResult result)
			{
				time = 0L;
				int num;
				if (!this.ParseInt(23, out num, ref result))
				{
					return false;
				}
				time = (long)num * 36000000000L;
				if (this._ch != ':')
				{
					return result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "String was not recognized as a valid TimeSpan.", null, null);
				}
				this.NextChar();
				if (!this.ParseInt(59, out num, ref result))
				{
					return false;
				}
				time += (long)num * 600000000L;
				if (this._ch == ':')
				{
					this.NextChar();
					if (this._ch != '.')
					{
						if (!this.ParseInt(59, out num, ref result))
						{
							return false;
						}
						time += (long)num * 10000000L;
					}
					if (this._ch == '.')
					{
						this.NextChar();
						int num2 = 10000000;
						while (num2 > 1 && this._ch >= '0' && this._ch <= '9')
						{
							num2 /= 10;
							time += (long)((int)(this._ch - '0') * num2);
							this.NextChar();
						}
					}
				}
				return true;
			}

			// Token: 0x06005BB2 RID: 23474 RVA: 0x0013A0AA File Offset: 0x001382AA
			internal void SkipBlanks()
			{
				while (this._ch == ' ' || this._ch == '\t')
				{
					this.NextChar();
				}
			}

			// Token: 0x04003757 RID: 14167
			private ReadOnlySpan<char> _str;

			// Token: 0x04003758 RID: 14168
			private char _ch;

			// Token: 0x04003759 RID: 14169
			private int _pos;

			// Token: 0x0400375A RID: 14170
			private int _len;
		}
	}
}
