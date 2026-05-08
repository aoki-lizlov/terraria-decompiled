using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000071 RID: 113
	internal static class StringUtils
	{
		// Token: 0x06000570 RID: 1392 RVA: 0x00017974 File Offset: 0x00015B74
		public static string FormatWith(this string format, IFormatProvider provider, object arg0)
		{
			return format.FormatWith(provider, new object[] { arg0 });
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00017987 File Offset: 0x00015B87
		public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1)
		{
			return format.FormatWith(provider, new object[] { arg0, arg1 });
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001799E File Offset: 0x00015B9E
		public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1, object arg2)
		{
			return format.FormatWith(provider, new object[] { arg0, arg1, arg2 });
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000179BA File Offset: 0x00015BBA
		public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1, object arg2, object arg3)
		{
			return format.FormatWith(provider, new object[] { arg0, arg1, arg2, arg3 });
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000179DB File Offset: 0x00015BDB
		private static string FormatWith(this string format, IFormatProvider provider, params object[] args)
		{
			ValidationUtils.ArgumentNotNull(format, "format");
			return string.Format(provider, format, args);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000179F0 File Offset: 0x00015BF0
		public static bool IsWhiteSpace(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (!char.IsWhiteSpace(s.get_Chars(i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00017A37 File Offset: 0x00015C37
		public static StringWriter CreateStringWriter(int capacity)
		{
			return new StringWriter(new StringBuilder(capacity), CultureInfo.InvariantCulture);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00017A4C File Offset: 0x00015C4C
		public static void ToCharAsUnicode(char c, char[] buffer)
		{
			buffer[0] = '\\';
			buffer[1] = 'u';
			buffer[2] = MathUtils.IntToHex((int)((c >> 12) & '\u000f'));
			buffer[3] = MathUtils.IntToHex((int)((c >> 8) & '\u000f'));
			buffer[4] = MathUtils.IntToHex((int)((c >> 4) & '\u000f'));
			buffer[5] = MathUtils.IntToHex((int)(c & '\u000f'));
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00017A9C File Offset: 0x00015C9C
		public static TSource ForgivingCaseSensitiveFind<TSource>(this IEnumerable<TSource> source, Func<TSource, string> valueSelector, string testValue)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (valueSelector == null)
			{
				throw new ArgumentNullException("valueSelector");
			}
			IEnumerable<TSource> enumerable = Enumerable.Where<TSource>(source, (TSource s) => string.Equals(valueSelector.Invoke(s), testValue, 5));
			if (Enumerable.Count<TSource>(enumerable) <= 1)
			{
				return Enumerable.SingleOrDefault<TSource>(enumerable);
			}
			return Enumerable.SingleOrDefault<TSource>(Enumerable.Where<TSource>(source, (TSource s) => string.Equals(valueSelector.Invoke(s), testValue, 4)));
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00017B18 File Offset: 0x00015D18
		public static string ToCamelCase(string s)
		{
			if (string.IsNullOrEmpty(s) || !char.IsUpper(s.get_Chars(0)))
			{
				return s;
			}
			char[] array = s.ToCharArray();
			int num = 0;
			while (num < array.Length && (num != 1 || char.IsUpper(array[num])))
			{
				bool flag = num + 1 < array.Length;
				if (num > 0 && flag && !char.IsUpper(array[num + 1]))
				{
					break;
				}
				char c = char.ToLower(array[num], CultureInfo.InvariantCulture);
				array[num] = c;
				num++;
			}
			return new string(array);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00017B98 File Offset: 0x00015D98
		public static string ToSnakeCase(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringUtils.SnakeCaseState snakeCaseState = StringUtils.SnakeCaseState.Start;
			for (int i = 0; i < s.Length; i++)
			{
				if (s.get_Chars(i) == ' ')
				{
					if (snakeCaseState != StringUtils.SnakeCaseState.Start)
					{
						snakeCaseState = StringUtils.SnakeCaseState.NewWord;
					}
				}
				else if (char.IsUpper(s.get_Chars(i)))
				{
					switch (snakeCaseState)
					{
					case StringUtils.SnakeCaseState.Lower:
					case StringUtils.SnakeCaseState.NewWord:
						stringBuilder.Append('_');
						break;
					case StringUtils.SnakeCaseState.Upper:
					{
						bool flag = i + 1 < s.Length;
						if (i > 0 && flag)
						{
							char c = s.get_Chars(i + 1);
							if (!char.IsUpper(c) && c != '_')
							{
								stringBuilder.Append('_');
							}
						}
						break;
					}
					}
					char c2 = char.ToLower(s.get_Chars(i), CultureInfo.InvariantCulture);
					stringBuilder.Append(c2);
					snakeCaseState = StringUtils.SnakeCaseState.Upper;
				}
				else if (s.get_Chars(i) == '_')
				{
					stringBuilder.Append('_');
					snakeCaseState = StringUtils.SnakeCaseState.Start;
				}
				else
				{
					if (snakeCaseState == StringUtils.SnakeCaseState.NewWord)
					{
						stringBuilder.Append('_');
					}
					stringBuilder.Append(s.get_Chars(i));
					snakeCaseState = StringUtils.SnakeCaseState.Lower;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00017CA7 File Offset: 0x00015EA7
		public static bool IsHighSurrogate(char c)
		{
			return char.IsHighSurrogate(c);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00017CAF File Offset: 0x00015EAF
		public static bool IsLowSurrogate(char c)
		{
			return char.IsLowSurrogate(c);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00017CB7 File Offset: 0x00015EB7
		public static bool StartsWith(this string source, char value)
		{
			return source.Length > 0 && source.get_Chars(0) == value;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00017CCE File Offset: 0x00015ECE
		public static bool EndsWith(this string source, char value)
		{
			return source.Length > 0 && source.get_Chars(source.Length - 1) == value;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00017CEC File Offset: 0x00015EEC
		public static string Trim(this string s, int start, int length)
		{
			if (s == null)
			{
				throw new ArgumentNullException();
			}
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = start + length - 1;
			if (num >= s.Length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			while (start < num)
			{
				if (!char.IsWhiteSpace(s.get_Chars(start)))
				{
					IL_006C:
					while (num >= start && char.IsWhiteSpace(s.get_Chars(num)))
					{
						num--;
					}
					return s.Substring(start, num - start + 1);
				}
				start++;
			}
			goto IL_006C;
		}

		// Token: 0x04000264 RID: 612
		public const string CarriageReturnLineFeed = "\r\n";

		// Token: 0x04000265 RID: 613
		public const string Empty = "";

		// Token: 0x04000266 RID: 614
		public const char CarriageReturn = '\r';

		// Token: 0x04000267 RID: 615
		public const char LineFeed = '\n';

		// Token: 0x04000268 RID: 616
		public const char Tab = '\t';

		// Token: 0x0200013C RID: 316
		internal enum SnakeCaseState
		{
			// Token: 0x040004A5 RID: 1189
			Start,
			// Token: 0x040004A6 RID: 1190
			Lower,
			// Token: 0x040004A7 RID: 1191
			Upper,
			// Token: 0x040004A8 RID: 1192
			NewWord
		}

		// Token: 0x0200013D RID: 317
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0<TSource>
		{
			// Token: 0x06000CE2 RID: 3298 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x06000CE3 RID: 3299 RVA: 0x00031327 File Offset: 0x0002F527
			internal bool <ForgivingCaseSensitiveFind>b__0(TSource s)
			{
				return string.Equals(this.valueSelector.Invoke(s), this.testValue, 5);
			}

			// Token: 0x06000CE4 RID: 3300 RVA: 0x00031341 File Offset: 0x0002F541
			internal bool <ForgivingCaseSensitiveFind>b__1(TSource s)
			{
				return string.Equals(this.valueSelector.Invoke(s), this.testValue, 4);
			}

			// Token: 0x040004A9 RID: 1193
			public Func<TSource, string> valueSelector;

			// Token: 0x040004AA RID: 1194
			public string testValue;
		}
	}
}
