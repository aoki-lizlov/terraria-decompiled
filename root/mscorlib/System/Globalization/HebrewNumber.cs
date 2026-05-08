using System;
using System.Text;

namespace System.Globalization
{
	// Token: 0x020009C1 RID: 2497
	internal class HebrewNumber
	{
		// Token: 0x06005B58 RID: 23384 RVA: 0x000025BE File Offset: 0x000007BE
		private HebrewNumber()
		{
		}

		// Token: 0x06005B59 RID: 23385 RVA: 0x00136F0C File Offset: 0x0013510C
		internal static string ToString(int Number)
		{
			char c = '\0';
			StringBuilder stringBuilder = new StringBuilder();
			if (Number > 5000)
			{
				Number -= 5000;
			}
			int num = Number / 100;
			if (num > 0)
			{
				Number -= num * 100;
				for (int i = 0; i < num / 4; i++)
				{
					stringBuilder.Append('ת');
				}
				int num2 = num % 4;
				if (num2 > 0)
				{
					stringBuilder.Append((char)(1510 + num2));
				}
			}
			int num3 = Number / 10;
			Number %= 10;
			switch (num3)
			{
			case 0:
				c = '\0';
				break;
			case 1:
				c = 'י';
				break;
			case 2:
				c = 'כ';
				break;
			case 3:
				c = 'ל';
				break;
			case 4:
				c = 'מ';
				break;
			case 5:
				c = 'נ';
				break;
			case 6:
				c = 'ס';
				break;
			case 7:
				c = 'ע';
				break;
			case 8:
				c = 'פ';
				break;
			case 9:
				c = 'צ';
				break;
			}
			char c2 = (char)((Number > 0) ? (1488 + Number - 1) : 0);
			if (c2 == 'ה' && c == 'י')
			{
				c2 = 'ו';
				c = 'ט';
			}
			if (c2 == 'ו' && c == 'י')
			{
				c2 = 'ז';
				c = 'ט';
			}
			if (c != '\0')
			{
				stringBuilder.Append(c);
			}
			if (c2 != '\0')
			{
				stringBuilder.Append(c2);
			}
			if (stringBuilder.Length > 1)
			{
				stringBuilder.Insert(stringBuilder.Length - 1, '"');
			}
			else
			{
				stringBuilder.Append('\'');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005B5A RID: 23386 RVA: 0x00137098 File Offset: 0x00135298
		internal static HebrewNumberParsingState ParseByChar(char ch, ref HebrewNumberParsingContext context)
		{
			HebrewNumber.HebrewToken hebrewToken;
			if (ch == '\'')
			{
				hebrewToken = HebrewNumber.HebrewToken.SingleQuote;
			}
			else if (ch == '"')
			{
				hebrewToken = HebrewNumber.HebrewToken.DoubleQuote;
			}
			else
			{
				int num = (int)(ch - 'א');
				if (num < 0 || num >= HebrewNumber.s_hebrewValues.Length)
				{
					return HebrewNumberParsingState.NotHebrewDigit;
				}
				hebrewToken = HebrewNumber.s_hebrewValues[num].token;
				if (hebrewToken == HebrewNumber.HebrewToken.Invalid)
				{
					return HebrewNumberParsingState.NotHebrewDigit;
				}
				context.result += (int)HebrewNumber.s_hebrewValues[num].value;
			}
			context.state = HebrewNumber.s_numberPasingState[(int)(context.state * HebrewNumber.HS.X00 + (sbyte)hebrewToken)];
			if (context.state == HebrewNumber.HS._err)
			{
				return HebrewNumberParsingState.InvalidHebrewNumber;
			}
			if (context.state == HebrewNumber.HS.END)
			{
				return HebrewNumberParsingState.FoundEndOfHebrewNumber;
			}
			return HebrewNumberParsingState.ContinueParsing;
		}

		// Token: 0x06005B5B RID: 23387 RVA: 0x00137132 File Offset: 0x00135332
		internal static bool IsDigit(char ch)
		{
			if (ch >= 'א' && ch <= HebrewNumber.s_maxHebrewNumberCh)
			{
				return HebrewNumber.s_hebrewValues[(int)(ch - 'א')].value >= 0;
			}
			return ch == '\'' || ch == '"';
		}

		// Token: 0x06005B5C RID: 23388 RVA: 0x00137170 File Offset: 0x00135370
		// Note: this type is marked as 'beforefieldinit'.
		static HebrewNumber()
		{
		}

		// Token: 0x040036C3 RID: 14019
		private static readonly HebrewNumber.HebrewValue[] s_hebrewValues = new HebrewNumber.HebrewValue[]
		{
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 2),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 3),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 4),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 5),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit6_7, 6),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit6_7, 7),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit1, 8),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit9, 9),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 10),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 20),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 30),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 40),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 50),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 60),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 70),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 80),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Invalid, -1),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit10, 90),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit100, 100),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit200_300, 200),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit200_300, 300),
			new HebrewNumber.HebrewValue(HebrewNumber.HebrewToken.Digit400, 400)
		};

		// Token: 0x040036C4 RID: 14020
		private const int minHebrewNumberCh = 1488;

		// Token: 0x040036C5 RID: 14021
		private static char s_maxHebrewNumberCh = (char)(1488 + HebrewNumber.s_hebrewValues.Length - 1);

		// Token: 0x040036C6 RID: 14022
		private static readonly HebrewNumber.HS[] s_numberPasingState = new HebrewNumber.HS[]
		{
			HebrewNumber.HS.S400,
			HebrewNumber.HS.X00,
			HebrewNumber.HS.X00,
			HebrewNumber.HS.X0,
			HebrewNumber.HS.X,
			HebrewNumber.HS.X,
			HebrewNumber.HS.X,
			HebrewNumber.HS.S9,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_400,
			HebrewNumber.HS.S400_X00,
			HebrewNumber.HS.S400_X00,
			HebrewNumber.HS.S400_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS.END,
			HebrewNumber.HS.S400_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_400_100,
			HebrewNumber.HS.S400_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_400_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_X00_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X0_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X0_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.X0_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS.END,
			HebrewNumber.HS.X00_DQ,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S400_X00_X0,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_S9,
			HebrewNumber.HS._err,
			HebrewNumber.HS.X00_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.S9_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.S9_DQ,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS.END,
			HebrewNumber.HS.END,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err,
			HebrewNumber.HS._err
		};

		// Token: 0x040036C7 RID: 14023
		private const int HebrewTokenCount = 10;

		// Token: 0x020009C2 RID: 2498
		private enum HebrewToken : short
		{
			// Token: 0x040036C9 RID: 14025
			Invalid = -1,
			// Token: 0x040036CA RID: 14026
			Digit400,
			// Token: 0x040036CB RID: 14027
			Digit200_300,
			// Token: 0x040036CC RID: 14028
			Digit100,
			// Token: 0x040036CD RID: 14029
			Digit10,
			// Token: 0x040036CE RID: 14030
			Digit1,
			// Token: 0x040036CF RID: 14031
			Digit6_7,
			// Token: 0x040036D0 RID: 14032
			Digit7,
			// Token: 0x040036D1 RID: 14033
			Digit9,
			// Token: 0x040036D2 RID: 14034
			SingleQuote,
			// Token: 0x040036D3 RID: 14035
			DoubleQuote
		}

		// Token: 0x020009C3 RID: 2499
		private struct HebrewValue
		{
			// Token: 0x06005B5D RID: 23389 RVA: 0x0013735B File Offset: 0x0013555B
			internal HebrewValue(HebrewNumber.HebrewToken token, short value)
			{
				this.token = token;
				this.value = value;
			}

			// Token: 0x040036D4 RID: 14036
			internal HebrewNumber.HebrewToken token;

			// Token: 0x040036D5 RID: 14037
			internal short value;
		}

		// Token: 0x020009C4 RID: 2500
		internal enum HS : sbyte
		{
			// Token: 0x040036D7 RID: 14039
			_err = -1,
			// Token: 0x040036D8 RID: 14040
			Start,
			// Token: 0x040036D9 RID: 14041
			S400,
			// Token: 0x040036DA RID: 14042
			S400_400,
			// Token: 0x040036DB RID: 14043
			S400_X00,
			// Token: 0x040036DC RID: 14044
			S400_X0,
			// Token: 0x040036DD RID: 14045
			X00_DQ,
			// Token: 0x040036DE RID: 14046
			S400_X00_X0,
			// Token: 0x040036DF RID: 14047
			X0_DQ,
			// Token: 0x040036E0 RID: 14048
			X,
			// Token: 0x040036E1 RID: 14049
			X0,
			// Token: 0x040036E2 RID: 14050
			X00,
			// Token: 0x040036E3 RID: 14051
			S400_DQ,
			// Token: 0x040036E4 RID: 14052
			S400_400_DQ,
			// Token: 0x040036E5 RID: 14053
			S400_400_100,
			// Token: 0x040036E6 RID: 14054
			S9,
			// Token: 0x040036E7 RID: 14055
			X00_S9,
			// Token: 0x040036E8 RID: 14056
			S9_DQ,
			// Token: 0x040036E9 RID: 14057
			END = 100
		}
	}
}
