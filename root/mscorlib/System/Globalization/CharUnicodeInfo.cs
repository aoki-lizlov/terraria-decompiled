using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Globalization
{
	// Token: 0x020009AD RID: 2477
	public static class CharUnicodeInfo
	{
		// Token: 0x06005A27 RID: 23079 RVA: 0x00131CE4 File Offset: 0x0012FEE4
		internal static int InternalConvertToUtf32(string s, int index)
		{
			if (index < s.Length - 1)
			{
				int num = (int)(s[index] - '\ud800');
				if (num <= 1023)
				{
					int num2 = (int)(s[index + 1] - '\udc00');
					if (num2 <= 1023)
					{
						return num * 1024 + num2 + 65536;
					}
				}
			}
			return (int)s[index];
		}

		// Token: 0x06005A28 RID: 23080 RVA: 0x00131D44 File Offset: 0x0012FF44
		internal static int InternalConvertToUtf32(StringBuilder s, int index)
		{
			int num = (int)s[index];
			if (index < s.Length - 1)
			{
				int num2 = num - 55296;
				if (num2 <= 1023)
				{
					int num3 = (int)(s[index + 1] - '\udc00');
					if (num3 <= 1023)
					{
						return num2 * 1024 + num3 + 65536;
					}
				}
			}
			return num;
		}

		// Token: 0x06005A29 RID: 23081 RVA: 0x00131DA0 File Offset: 0x0012FFA0
		internal static int InternalConvertToUtf32(string s, int index, out int charLength)
		{
			charLength = 1;
			if (index < s.Length - 1)
			{
				int num = (int)(s[index] - '\ud800');
				if (num <= 1023)
				{
					int num2 = (int)(s[index + 1] - '\udc00');
					if (num2 <= 1023)
					{
						charLength++;
						return num * 1024 + num2 + 65536;
					}
				}
			}
			return (int)s[index];
		}

		// Token: 0x06005A2A RID: 23082 RVA: 0x00131E08 File Offset: 0x00130008
		internal unsafe static double InternalGetNumericValue(int ch)
		{
			int num = ch >> 8;
			if (num >= CharUnicodeInfo.NumericLevel1Index.Length)
			{
				return -1.0;
			}
			num = (int)(*CharUnicodeInfo.NumericLevel1Index[num]);
			num = (int)(*CharUnicodeInfo.NumericLevel2Index[(num << 4) + ((ch >> 4) & 15)]);
			num = (int)(*CharUnicodeInfo.NumericLevel3Index[(num << 4) + (ch & 15)]);
			ref byte ptr = ref Unsafe.AsRef<byte>(CharUnicodeInfo.NumericValues[num * 8]);
			if (BitConverter.IsLittleEndian)
			{
				return Unsafe.ReadUnaligned<double>(ref ptr);
			}
			return BitConverter.Int64BitsToDouble(BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<long>(ref ptr)));
		}

		// Token: 0x06005A2B RID: 23083 RVA: 0x00131EA8 File Offset: 0x001300A8
		internal unsafe static byte InternalGetDigitValues(int ch, int offset)
		{
			int num = ch >> 8;
			if (num < CharUnicodeInfo.NumericLevel1Index.Length)
			{
				num = (int)(*CharUnicodeInfo.NumericLevel1Index[num]);
				num = (int)(*CharUnicodeInfo.NumericLevel2Index[(num << 4) + ((ch >> 4) & 15)]);
				num = (int)(*CharUnicodeInfo.NumericLevel3Index[(num << 4) + (ch & 15)]);
				return *CharUnicodeInfo.DigitValues[num * 2 + offset];
			}
			return byte.MaxValue;
		}

		// Token: 0x06005A2C RID: 23084 RVA: 0x00131F22 File Offset: 0x00130122
		public static double GetNumericValue(char ch)
		{
			return CharUnicodeInfo.InternalGetNumericValue((int)ch);
		}

		// Token: 0x06005A2D RID: 23085 RVA: 0x00131F2A File Offset: 0x0013012A
		public static double GetNumericValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return CharUnicodeInfo.InternalGetNumericValue(CharUnicodeInfo.InternalConvertToUtf32(s, index));
		}

		// Token: 0x06005A2E RID: 23086 RVA: 0x00131F63 File Offset: 0x00130163
		public static int GetDecimalDigitValue(char ch)
		{
			return (int)((sbyte)CharUnicodeInfo.InternalGetDigitValues((int)ch, 0));
		}

		// Token: 0x06005A2F RID: 23087 RVA: 0x00131F6D File Offset: 0x0013016D
		public static int GetDecimalDigitValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return (int)((sbyte)CharUnicodeInfo.InternalGetDigitValues(CharUnicodeInfo.InternalConvertToUtf32(s, index), 0));
		}

		// Token: 0x06005A30 RID: 23088 RVA: 0x00131FA8 File Offset: 0x001301A8
		public static int GetDigitValue(char ch)
		{
			return (int)((sbyte)CharUnicodeInfo.InternalGetDigitValues((int)ch, 1));
		}

		// Token: 0x06005A31 RID: 23089 RVA: 0x00131FB2 File Offset: 0x001301B2
		public static int GetDigitValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			return (int)((sbyte)CharUnicodeInfo.InternalGetDigitValues(CharUnicodeInfo.InternalConvertToUtf32(s, index), 1));
		}

		// Token: 0x06005A32 RID: 23090 RVA: 0x00131FED File Offset: 0x001301ED
		public static UnicodeCategory GetUnicodeCategory(char ch)
		{
			return CharUnicodeInfo.GetUnicodeCategory((int)ch);
		}

		// Token: 0x06005A33 RID: 23091 RVA: 0x00131FF5 File Offset: 0x001301F5
		public static UnicodeCategory GetUnicodeCategory(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
		}

		// Token: 0x06005A34 RID: 23092 RVA: 0x00132020 File Offset: 0x00130220
		public static UnicodeCategory GetUnicodeCategory(int codePoint)
		{
			return (UnicodeCategory)CharUnicodeInfo.InternalGetCategoryValue(codePoint, 0);
		}

		// Token: 0x06005A35 RID: 23093 RVA: 0x0013202C File Offset: 0x0013022C
		internal unsafe static byte InternalGetCategoryValue(int ch, int offset)
		{
			int num = (int)(*CharUnicodeInfo.CategoryLevel1Index[ch >> 9]);
			num = (int)Unsafe.ReadUnaligned<ushort>(Unsafe.AsRef<byte>(CharUnicodeInfo.CategoryLevel2Index[(num << 6) + ((ch >> 3) & 62)]));
			if (!BitConverter.IsLittleEndian)
			{
				num = (int)BinaryPrimitives.ReverseEndianness((ushort)num);
			}
			num = (int)(*CharUnicodeInfo.CategoryLevel3Index[(num << 4) + (ch & 15)]);
			return *CharUnicodeInfo.CategoriesValue[num * 2 + offset];
		}

		// Token: 0x06005A36 RID: 23094 RVA: 0x001320A7 File Offset: 0x001302A7
		internal static UnicodeCategory InternalGetUnicodeCategory(string value, int index)
		{
			return CharUnicodeInfo.GetUnicodeCategory(CharUnicodeInfo.InternalConvertToUtf32(value, index));
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x001320B5 File Offset: 0x001302B5
		internal static BidiCategory GetBidiCategory(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return (BidiCategory)CharUnicodeInfo.InternalGetCategoryValue(CharUnicodeInfo.InternalConvertToUtf32(s, index), 1);
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x001320E6 File Offset: 0x001302E6
		internal static BidiCategory GetBidiCategory(StringBuilder s, int index)
		{
			return (BidiCategory)CharUnicodeInfo.InternalGetCategoryValue(CharUnicodeInfo.InternalConvertToUtf32(s, index), 1);
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x001320F5 File Offset: 0x001302F5
		internal static UnicodeCategory InternalGetUnicodeCategory(string str, int index, out int charLength)
		{
			return CharUnicodeInfo.GetUnicodeCategory(CharUnicodeInfo.InternalConvertToUtf32(str, index, out charLength));
		}

		// Token: 0x06005A3A RID: 23098 RVA: 0x00132104 File Offset: 0x00130304
		internal static bool IsCombiningCategory(UnicodeCategory uc)
		{
			return uc == UnicodeCategory.NonSpacingMark || uc == UnicodeCategory.SpacingCombiningMark || uc == UnicodeCategory.EnclosingMark;
		}

		// Token: 0x06005A3B RID: 23099 RVA: 0x00132114 File Offset: 0x00130314
		internal static bool IsWhiteSpace(string s, int index)
		{
			UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(s, index);
			return unicodeCategory - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x06005A3C RID: 23100 RVA: 0x00132134 File Offset: 0x00130334
		internal static bool IsWhiteSpace(char c)
		{
			UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
			return unicodeCategory - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06005A3D RID: 23101 RVA: 0x00132152 File Offset: 0x00130352
		private unsafe static ReadOnlySpan<byte> CategoryLevel1Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.B55F94CD2F415D0279D7A1AF2265C4D9A90CE47F8C900D5D09AD088796210838), 2176);
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06005A3E RID: 23102 RVA: 0x00132163 File Offset: 0x00130363
		private unsafe static ReadOnlySpan<byte> CategoryLevel2Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.9086502742CE7F0595B57A4E5B32901FF4CF97959B92F7E91A435E4765AC1115), 5952);
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06005A3F RID: 23103 RVA: 0x00132174 File Offset: 0x00130374
		private unsafe static ReadOnlySpan<byte> CategoryLevel3Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.56073E3CC3FC817690CC306D0DB7EA63EBCB0801359567CA44CA3D3B9BF63854), 10800);
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06005A40 RID: 23104 RVA: 0x00132185 File Offset: 0x00130385
		private unsafe static ReadOnlySpan<byte> CategoriesValue
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.D6691EE5A533DE7E0859066942261B24D0C836D7EE016D2251377BFEE40FEA15), 172);
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06005A41 RID: 23105 RVA: 0x00132196 File Offset: 0x00130396
		private unsafe static ReadOnlySpan<byte> NumericLevel1Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.765BD07ED3CB498A599FFB48B31E077C45B4C2C37CD1547CEA27E60655CF21B6), 761);
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06005A42 RID: 23106 RVA: 0x001321A7 File Offset: 0x001303A7
		private unsafe static ReadOnlySpan<byte> NumericLevel2Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.F7D2AD02ED768134B31339AB059D864789E0A60090CC368B3881EB0631BBAF93), 1024);
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06005A43 RID: 23107 RVA: 0x001321B8 File Offset: 0x001303B8
		private unsafe static ReadOnlySpan<byte> NumericLevel3Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.AB0B9733AAEC4A2806711E41E36D3D0923BAF116156F33445DC2AA58DA5DF877), 1824);
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06005A44 RID: 23108 RVA: 0x001321C9 File Offset: 0x001303C9
		private unsafe static ReadOnlySpan<byte> NumericValues
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.692DE452EE427272A5F6154F04360D24165B56693B08F60D93127DEDC12D1DDE), 1320);
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06005A45 RID: 23109 RVA: 0x001321DA File Offset: 0x001303DA
		private unsafe static ReadOnlySpan<byte> DigitValues
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.1A52279427700E21F7E68A077A8F17857A850718317B7228442260DBA2AF68F0), 330);
			}
		}

		// Token: 0x040035E1 RID: 13793
		internal const char HIGH_SURROGATE_START = '\ud800';

		// Token: 0x040035E2 RID: 13794
		internal const char HIGH_SURROGATE_END = '\udbff';

		// Token: 0x040035E3 RID: 13795
		internal const char LOW_SURROGATE_START = '\udc00';

		// Token: 0x040035E4 RID: 13796
		internal const char LOW_SURROGATE_END = '\udfff';

		// Token: 0x040035E5 RID: 13797
		internal const int HIGH_SURROGATE_RANGE = 1023;

		// Token: 0x040035E6 RID: 13798
		internal const int UNICODE_CATEGORY_OFFSET = 0;

		// Token: 0x040035E7 RID: 13799
		internal const int BIDI_CATEGORY_OFFSET = 1;

		// Token: 0x040035E8 RID: 13800
		internal const int UNICODE_PLANE01_START = 65536;
	}
}
