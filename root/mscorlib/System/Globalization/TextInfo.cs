using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Globalization
{
	// Token: 0x020009F2 RID: 2546
	[ComVisible(true)]
	[Serializable]
	public class TextInfo : ICloneable, IDeserializationCallback
	{
		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06005E37 RID: 24119 RVA: 0x001421D5 File Offset: 0x001403D5
		internal static TextInfo Invariant
		{
			get
			{
				if (TextInfo.s_Invariant == null)
				{
					TextInfo.s_Invariant = new TextInfo(CultureData.Invariant);
				}
				return TextInfo.s_Invariant;
			}
		}

		// Token: 0x06005E38 RID: 24120 RVA: 0x001421F8 File Offset: 0x001403F8
		internal TextInfo(CultureData cultureData)
		{
			this.m_cultureData = cultureData;
			this.m_cultureName = this.m_cultureData.CultureName;
			this.m_textInfoName = this.m_cultureData.STEXTINFO;
		}

		// Token: 0x06005E39 RID: 24121 RVA: 0x00142229 File Offset: 0x00140429
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_cultureData = null;
			this.m_cultureName = null;
		}

		// Token: 0x06005E3A RID: 24122 RVA: 0x0014223C File Offset: 0x0014043C
		private void OnDeserialized()
		{
			if (this.m_cultureData == null)
			{
				if (this.m_cultureName == null)
				{
					if (this.customCultureName != null)
					{
						this.m_cultureName = this.customCultureName;
					}
					else if (this.m_win32LangID == 0)
					{
						this.m_cultureName = "ar-SA";
					}
					else
					{
						this.m_cultureName = CultureInfo.GetCultureInfo(this.m_win32LangID).m_cultureData.CultureName;
					}
				}
				this.m_cultureData = CultureInfo.GetCultureInfo(this.m_cultureName).m_cultureData;
				this.m_textInfoName = this.m_cultureData.STEXTINFO;
			}
		}

		// Token: 0x06005E3B RID: 24123 RVA: 0x001422C6 File Offset: 0x001404C6
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x06005E3C RID: 24124 RVA: 0x001422CE File Offset: 0x001404CE
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.m_useUserOverride = false;
			this.customCultureName = this.m_cultureName;
			this.m_win32LangID = CultureInfo.GetCultureInfo(this.m_cultureName).LCID;
		}

		// Token: 0x06005E3D RID: 24125 RVA: 0x001422F9 File Offset: 0x001404F9
		internal static int GetHashCodeOrdinalIgnoreCase(string s)
		{
			return TextInfo.GetHashCodeOrdinalIgnoreCase(s, false, 0L);
		}

		// Token: 0x06005E3E RID: 24126 RVA: 0x00142304 File Offset: 0x00140504
		internal static int GetHashCodeOrdinalIgnoreCase(string s, bool forceRandomizedHashing, long additionalEntropy)
		{
			return TextInfo.Invariant.GetCaseInsensitiveHashCode(s, forceRandomizedHashing, additionalEntropy);
		}

		// Token: 0x06005E3F RID: 24127 RVA: 0x00142313 File Offset: 0x00140513
		[SecuritySafeCritical]
		internal static int CompareOrdinalIgnoreCaseEx(string strA, int indexA, string strB, int indexB, int lengthA, int lengthB)
		{
			return TextInfo.InternalCompareStringOrdinalIgnoreCase(strA, indexA, strB, indexB, lengthA, lengthB);
		}

		// Token: 0x06005E40 RID: 24128 RVA: 0x00142324 File Offset: 0x00140524
		internal static int IndexOfStringOrdinalIgnoreCase(string source, string value, int startIndex, int count)
		{
			if (source.Length == 0 && value.Length == 0)
			{
				return 0;
			}
			int num = startIndex + count - value.Length;
			while (startIndex <= num)
			{
				if (TextInfo.CompareOrdinalIgnoreCaseEx(source, startIndex, value, 0, value.Length, value.Length) == 0)
				{
					return startIndex;
				}
				startIndex++;
			}
			return -1;
		}

		// Token: 0x06005E41 RID: 24129 RVA: 0x00142374 File Offset: 0x00140574
		internal static int LastIndexOfStringOrdinalIgnoreCase(string source, string value, int startIndex, int count)
		{
			if (value.Length == 0)
			{
				return startIndex;
			}
			int num = startIndex - count + 1;
			if (value.Length > 0)
			{
				startIndex -= value.Length - 1;
			}
			while (startIndex >= num)
			{
				if (TextInfo.CompareOrdinalIgnoreCaseEx(source, startIndex, value, 0, value.Length, value.Length) == 0)
				{
					return startIndex;
				}
				startIndex--;
			}
			return -1;
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06005E42 RID: 24130 RVA: 0x001423CB File Offset: 0x001405CB
		public virtual int ANSICodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTANSICODEPAGE;
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06005E43 RID: 24131 RVA: 0x001423D8 File Offset: 0x001405D8
		public virtual int OEMCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTOEMCODEPAGE;
			}
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06005E44 RID: 24132 RVA: 0x001423E5 File Offset: 0x001405E5
		public virtual int MacCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTMACCODEPAGE;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06005E45 RID: 24133 RVA: 0x001423F2 File Offset: 0x001405F2
		public virtual int EBCDICCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTEBCDICCODEPAGE;
			}
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06005E46 RID: 24134 RVA: 0x001423FF File Offset: 0x001405FF
		[ComVisible(false)]
		public int LCID
		{
			get
			{
				return CultureInfo.GetCultureInfo(this.m_textInfoName).LCID;
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06005E47 RID: 24135 RVA: 0x00142411 File Offset: 0x00140611
		[ComVisible(false)]
		public string CultureName
		{
			get
			{
				return this.m_textInfoName;
			}
		}

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06005E48 RID: 24136 RVA: 0x00142419 File Offset: 0x00140619
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		// Token: 0x06005E49 RID: 24137 RVA: 0x00142421 File Offset: 0x00140621
		[ComVisible(false)]
		public virtual object Clone()
		{
			object obj = base.MemberwiseClone();
			((TextInfo)obj).SetReadOnlyState(false);
			return obj;
		}

		// Token: 0x06005E4A RID: 24138 RVA: 0x00142435 File Offset: 0x00140635
		[ComVisible(false)]
		public static TextInfo ReadOnly(TextInfo textInfo)
		{
			if (textInfo == null)
			{
				throw new ArgumentNullException("textInfo");
			}
			if (textInfo.IsReadOnly)
			{
				return textInfo;
			}
			TextInfo textInfo2 = (TextInfo)textInfo.MemberwiseClone();
			textInfo2.SetReadOnlyState(true);
			return textInfo2;
		}

		// Token: 0x06005E4B RID: 24139 RVA: 0x00142461 File Offset: 0x00140661
		private void VerifyWritable()
		{
			if (this.m_isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
			}
		}

		// Token: 0x06005E4C RID: 24140 RVA: 0x0014247B File Offset: 0x0014067B
		internal void SetReadOnlyState(bool readOnly)
		{
			this.m_isReadOnly = readOnly;
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06005E4D RID: 24141 RVA: 0x00142484 File Offset: 0x00140684
		// (set) Token: 0x06005E4E RID: 24142 RVA: 0x001424A5 File Offset: 0x001406A5
		public virtual string ListSeparator
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_listSeparator == null)
				{
					this.m_listSeparator = this.m_cultureData.SLIST;
				}
				return this.m_listSeparator;
			}
			[ComVisible(false)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.m_listSeparator = value;
			}
		}

		// Token: 0x06005E4F RID: 24143 RVA: 0x001424CC File Offset: 0x001406CC
		[SecuritySafeCritical]
		public virtual char ToLower(char c)
		{
			if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
			{
				return TextInfo.ToLowerAsciiInvariant(c);
			}
			return this.ToLowerInternal(c);
		}

		// Token: 0x06005E50 RID: 24144 RVA: 0x001424EC File Offset: 0x001406EC
		[SecuritySafeCritical]
		public virtual string ToLower(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return this.ToLowerInternal(str);
		}

		// Token: 0x06005E51 RID: 24145 RVA: 0x00142503 File Offset: 0x00140703
		private static char ToLowerAsciiInvariant(char c)
		{
			if ('A' <= c && c <= 'Z')
			{
				c |= ' ';
			}
			return c;
		}

		// Token: 0x06005E52 RID: 24146 RVA: 0x00142517 File Offset: 0x00140717
		[SecuritySafeCritical]
		public virtual char ToUpper(char c)
		{
			if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
			{
				return TextInfo.ToUpperAsciiInvariant(c);
			}
			return this.ToUpperInternal(c);
		}

		// Token: 0x06005E53 RID: 24147 RVA: 0x00142537 File Offset: 0x00140737
		[SecuritySafeCritical]
		public virtual string ToUpper(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return this.ToUpperInternal(str);
		}

		// Token: 0x06005E54 RID: 24148 RVA: 0x0014254E File Offset: 0x0014074E
		internal static char ToUpperAsciiInvariant(char c)
		{
			if ('a' <= c && c <= 'z')
			{
				c = (char)((int)c & -33);
			}
			return c;
		}

		// Token: 0x06005E55 RID: 24149 RVA: 0x00142562 File Offset: 0x00140762
		private static bool IsAscii(char c)
		{
			return c < '\u0080';
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06005E56 RID: 24150 RVA: 0x0014256C File Offset: 0x0014076C
		private bool IsAsciiCasingSameAsInvariant
		{
			get
			{
				if (this.m_IsAsciiCasingSameAsInvariant == null)
				{
					this.m_IsAsciiCasingSameAsInvariant = new bool?(!(this.m_cultureData.SISO639LANGNAME == "az") && !(this.m_cultureData.SISO639LANGNAME == "tr"));
				}
				return this.m_IsAsciiCasingSameAsInvariant.Value;
			}
		}

		// Token: 0x06005E57 RID: 24151 RVA: 0x001425D0 File Offset: 0x001407D0
		public override bool Equals(object obj)
		{
			TextInfo textInfo = obj as TextInfo;
			return textInfo != null && this.CultureName.Equals(textInfo.CultureName);
		}

		// Token: 0x06005E58 RID: 24152 RVA: 0x001425FA File Offset: 0x001407FA
		public override int GetHashCode()
		{
			return this.CultureName.GetHashCode();
		}

		// Token: 0x06005E59 RID: 24153 RVA: 0x00142607 File Offset: 0x00140807
		public override string ToString()
		{
			return "TextInfo - " + this.m_cultureData.CultureName;
		}

		// Token: 0x06005E5A RID: 24154 RVA: 0x00142620 File Offset: 0x00140820
		public string ToTitleCase(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			for (int i = 0; i < str.Length; i++)
			{
				int num;
				UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, i, out num);
				if (char.CheckLetter(unicodeCategory))
				{
					i = this.AddTitlecaseLetter(ref stringBuilder, ref str, i, num) + 1;
					int num2 = i;
					bool flag = unicodeCategory == UnicodeCategory.LowercaseLetter;
					while (i < str.Length)
					{
						unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, i, out num);
						if (TextInfo.IsLetterCategory(unicodeCategory))
						{
							if (unicodeCategory == UnicodeCategory.LowercaseLetter)
							{
								flag = true;
							}
							i += num;
						}
						else if (str[i] == '\'')
						{
							i++;
							if (flag)
							{
								if (text == null)
								{
									text = this.ToLower(str);
								}
								stringBuilder.Append(text, num2, i - num2);
							}
							else
							{
								stringBuilder.Append(str, num2, i - num2);
							}
							num2 = i;
							flag = true;
						}
						else
						{
							if (TextInfo.IsWordSeparator(unicodeCategory))
							{
								break;
							}
							i += num;
						}
					}
					int num3 = i - num2;
					if (num3 > 0)
					{
						if (flag)
						{
							if (text == null)
							{
								text = this.ToLower(str);
							}
							stringBuilder.Append(text, num2, num3);
						}
						else
						{
							stringBuilder.Append(str, num2, num3);
						}
					}
					if (i < str.Length)
					{
						i = TextInfo.AddNonLetter(ref stringBuilder, ref str, i, num);
					}
				}
				else
				{
					i = TextInfo.AddNonLetter(ref stringBuilder, ref str, i, num);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005E5B RID: 24155 RVA: 0x0014276D File Offset: 0x0014096D
		private static int AddNonLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
		{
			if (charLen == 2)
			{
				result.Append(input[inputIndex++]);
				result.Append(input[inputIndex]);
			}
			else
			{
				result.Append(input[inputIndex]);
			}
			return inputIndex;
		}

		// Token: 0x06005E5C RID: 24156 RVA: 0x001427AC File Offset: 0x001409AC
		private int AddTitlecaseLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
		{
			if (charLen == 2)
			{
				result.Append(this.ToUpper(input.Substring(inputIndex, charLen)));
				inputIndex++;
			}
			else
			{
				char c = input[inputIndex];
				switch (c)
				{
				case 'Ǆ':
				case 'ǅ':
				case 'ǆ':
					result.Append('ǅ');
					break;
				case 'Ǉ':
				case 'ǈ':
				case 'ǉ':
					result.Append('ǈ');
					break;
				case 'Ǌ':
				case 'ǋ':
				case 'ǌ':
					result.Append('ǋ');
					break;
				default:
					switch (c)
					{
					case 'Ǳ':
					case 'ǲ':
					case 'ǳ':
						result.Append('ǲ');
						break;
					default:
						result.Append(this.ToUpper(input[inputIndex]));
						break;
					}
					break;
				}
			}
			return inputIndex;
		}

		// Token: 0x06005E5D RID: 24157 RVA: 0x00142886 File Offset: 0x00140A86
		private static bool IsWordSeparator(UnicodeCategory category)
		{
			return (536672256 & (1 << (int)category)) != 0;
		}

		// Token: 0x06005E5E RID: 24158 RVA: 0x00142897 File Offset: 0x00140A97
		private static bool IsLetterCategory(UnicodeCategory uc)
		{
			return uc == UnicodeCategory.UppercaseLetter || uc == UnicodeCategory.LowercaseLetter || uc == UnicodeCategory.TitlecaseLetter || uc == UnicodeCategory.ModifierLetter || uc == UnicodeCategory.OtherLetter;
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06005E5F RID: 24159 RVA: 0x001428AE File Offset: 0x00140AAE
		[ComVisible(false)]
		public bool IsRightToLeft
		{
			get
			{
				return this.m_cultureData.IsRightToLeft;
			}
		}

		// Token: 0x06005E60 RID: 24160 RVA: 0x001422C6 File Offset: 0x001404C6
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialized();
		}

		// Token: 0x06005E61 RID: 24161 RVA: 0x001428BB File Offset: 0x00140ABB
		[SecuritySafeCritical]
		internal int GetCaseInsensitiveHashCode(string str)
		{
			return this.GetCaseInsensitiveHashCode(str, false, 0L);
		}

		// Token: 0x06005E62 RID: 24162 RVA: 0x001428C7 File Offset: 0x00140AC7
		[SecuritySafeCritical]
		internal int GetCaseInsensitiveHashCode(string str, bool forceRandomizedHashing, long additionalEntropy)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (this != TextInfo.s_Invariant)
			{
				return StringComparer.CurrentCultureIgnoreCase.GetHashCode(str);
			}
			return this.GetInvariantCaseInsensitiveHashCode(str);
		}

		// Token: 0x06005E63 RID: 24163 RVA: 0x001428F4 File Offset: 0x00140AF4
		private unsafe int GetInvariantCaseInsensitiveHashCode(string str)
		{
			char* ptr = str;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr;
			char* ptr3 = ptr2 + str.Length - 1;
			int num = 0;
			while (ptr2 < ptr3)
			{
				num = (num << 5) - num + (int)char.ToUpperInvariant(*ptr2);
				num = (num << 5) - num + (int)char.ToUpperInvariant(ptr2[1]);
				ptr2 += 2;
			}
			ptr3++;
			if (ptr2 < ptr3)
			{
				num = (num << 5) - num + (int)char.ToUpperInvariant(*ptr2);
			}
			return num;
		}

		// Token: 0x06005E64 RID: 24164 RVA: 0x00142970 File Offset: 0x00140B70
		private unsafe string ToUpperInternal(string str)
		{
			if (str.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(str.Length);
			fixed (string text2 = str)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text3 = text)
				{
					char* ptr2 = text3;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr2;
					char* ptr4 = ptr;
					for (int i = 0; i < str.Length; i++)
					{
						*ptr3 = this.ToUpper(*ptr4);
						ptr4++;
						ptr3++;
					}
					text2 = null;
				}
				return text;
			}
		}

		// Token: 0x06005E65 RID: 24165 RVA: 0x001429F4 File Offset: 0x00140BF4
		private unsafe string ToLowerInternal(string str)
		{
			if (str.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(str.Length);
			fixed (string text2 = str)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text3 = text)
				{
					char* ptr2 = text3;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr2;
					char* ptr4 = ptr;
					for (int i = 0; i < str.Length; i++)
					{
						*ptr3 = this.ToLower(*ptr4);
						ptr4++;
						ptr3++;
					}
					text2 = null;
				}
				return text;
			}
		}

		// Token: 0x06005E66 RID: 24166 RVA: 0x00142A78 File Offset: 0x00140C78
		private char ToUpperInternal(char c)
		{
			if (!this.m_cultureData.IsInvariantCulture)
			{
				if (c <= 'ǲ')
				{
					if (c > 'ſ')
					{
						if (c <= 'ǈ')
						{
							if (c != 'ǅ' && c != 'ǈ')
							{
								goto IL_015A;
							}
						}
						else if (c != 'ǋ' && c != 'ǲ')
						{
							goto IL_015A;
						}
						return c - '\u0001';
					}
					if (c == 'µ')
					{
						return 'Μ';
					}
					if (c == 'ı')
					{
						return 'I';
					}
					if (c == 'ſ')
					{
						return 'S';
					}
				}
				else if (c <= 'ϰ')
				{
					if (c <= 'ς')
					{
						if (c == '\u0345')
						{
							return 'Ι';
						}
						if (c == 'ς')
						{
							return 'Σ';
						}
					}
					else
					{
						switch (c)
						{
						case 'ϐ':
							return 'Β';
						case 'ϑ':
							return 'Θ';
						case 'ϒ':
						case 'ϓ':
						case 'ϔ':
							break;
						case 'ϕ':
							return 'Φ';
						case 'ϖ':
							return 'Π';
						default:
							if (c == 'ϰ')
							{
								return 'Κ';
							}
							break;
						}
					}
				}
				else if (c <= 'ϵ')
				{
					if (c == 'ϱ')
					{
						return 'Ρ';
					}
					if (c == 'ϵ')
					{
						return 'Ε';
					}
				}
				else
				{
					if (c == 'ẛ')
					{
						return 'Ṡ';
					}
					if (c == 'ι')
					{
						return 'Ι';
					}
				}
				IL_015A:
				if (!this.IsAsciiCasingSameAsInvariant)
				{
					if (c == 'i')
					{
						return 'İ';
					}
					if (TextInfo.IsAscii(c))
					{
						return TextInfo.ToUpperAsciiInvariant(c);
					}
				}
			}
			if (c >= 'à' && c <= 'ֆ')
			{
				return TextInfoToUpperData.range_00e0_0586[(int)(c - 'à')];
			}
			if (c >= 'ḁ' && c <= 'ῳ')
			{
				return TextInfoToUpperData.range_1e01_1ff3[(int)(c - 'ḁ')];
			}
			if (c >= 'ⅰ' && c <= 'ↄ')
			{
				return TextInfoToUpperData.range_2170_2184[(int)(c - 'ⅰ')];
			}
			if (c >= 'ⓐ' && c <= 'ⓩ')
			{
				return TextInfoToUpperData.range_24d0_24e9[(int)(c - 'ⓐ')];
			}
			if (c >= 'ⰰ' && c <= 'ⳣ')
			{
				return TextInfoToUpperData.range_2c30_2ce3[(int)(c - 'ⰰ')];
			}
			if (c >= 'ⴀ' && c <= 'ⴥ')
			{
				return TextInfoToUpperData.range_2d00_2d25[(int)(c - 'ⴀ')];
			}
			if (c >= 'ꙁ' && c <= 'ꚗ')
			{
				return TextInfoToUpperData.range_a641_a697[(int)(c - 'ꙁ')];
			}
			if (c >= 'ꜣ' && c <= 'ꞌ')
			{
				return TextInfoToUpperData.range_a723_a78c[(int)(c - 'ꜣ')];
			}
			if ('ａ' <= c && c <= 'ｚ')
			{
				return c - ' ';
			}
			if (c == 'ᵹ')
			{
				return 'Ᵹ';
			}
			if (c == 'ᵽ')
			{
				return 'Ᵽ';
			}
			if (c != 'ⅎ')
			{
				return c;
			}
			return 'Ⅎ';
		}

		// Token: 0x06005E67 RID: 24167 RVA: 0x00142D34 File Offset: 0x00140F34
		private char ToLowerInternal(char c)
		{
			if (!this.m_cultureData.IsInvariantCulture)
			{
				if (c <= 'ǲ')
				{
					if (c <= 'ǅ')
					{
						if (c == 'İ')
						{
							return 'i';
						}
						if (c != 'ǅ')
						{
							goto IL_00D3;
						}
					}
					else if (c != 'ǈ' && c != 'ǋ' && c != 'ǲ')
					{
						goto IL_00D3;
					}
					return c + '\u0001';
				}
				if (c <= 'ẞ')
				{
					switch (c)
					{
					case 'ϒ':
						return 'υ';
					case 'ϓ':
						return 'ύ';
					case 'ϔ':
						return 'ϋ';
					default:
						if (c == 'ϴ')
						{
							return 'θ';
						}
						if (c == 'ẞ')
						{
							return 'ß';
						}
						break;
					}
				}
				else
				{
					if (c == 'Ω')
					{
						return 'ω';
					}
					if (c == 'K')
					{
						return 'k';
					}
					if (c == 'Å')
					{
						return 'å';
					}
				}
				IL_00D3:
				if (!this.IsAsciiCasingSameAsInvariant)
				{
					if (c == 'I')
					{
						return 'ı';
					}
					if (TextInfo.IsAscii(c))
					{
						return TextInfo.ToLowerAsciiInvariant(c);
					}
				}
			}
			if (c >= 'À' && c <= 'Ֆ')
			{
				return TextInfoToLowerData.range_00c0_0556[(int)(c - 'À')];
			}
			if (c >= 'Ⴀ' && c <= 'Ⴥ')
			{
				return TextInfoToLowerData.range_10a0_10c5[(int)(c - 'Ⴀ')];
			}
			if (c >= 'Ḁ' && c <= 'ῼ')
			{
				return TextInfoToLowerData.range_1e00_1ffc[(int)(c - 'Ḁ')];
			}
			if (c >= 'Ⅰ' && c <= 'Ⅿ')
			{
				return TextInfoToLowerData.range_2160_216f[(int)(c - 'Ⅰ')];
			}
			if (c >= 'Ⓐ' && c <= 'Ⓩ')
			{
				return TextInfoToLowerData.range_24b6_24cf[(int)(c - 'Ⓐ')];
			}
			if (c >= 'Ⰰ' && c <= 'Ⱞ')
			{
				return TextInfoToLowerData.range_2c00_2c2e[(int)(c - 'Ⰰ')];
			}
			if (c >= 'Ⱡ' && c <= 'Ⳣ')
			{
				return TextInfoToLowerData.range_2c60_2ce2[(int)(c - 'Ⱡ')];
			}
			if (c >= 'Ꙁ' && c <= 'Ꚗ')
			{
				return TextInfoToLowerData.range_a640_a696[(int)(c - 'Ꙁ')];
			}
			if (c >= 'Ꜣ' && c <= 'Ꞌ')
			{
				return TextInfoToLowerData.range_a722_a78b[(int)(c - 'Ꜣ')];
			}
			if ('Ａ' <= c && c <= 'Ｚ')
			{
				return c + ' ';
			}
			if (c == 'Ⅎ')
			{
				return 'ⅎ';
			}
			if (c != 'Ↄ')
			{
				return c;
			}
			return 'ↄ';
		}

		// Token: 0x06005E68 RID: 24168 RVA: 0x00142F7C File Offset: 0x0014117C
		internal unsafe static int InternalCompareStringOrdinalIgnoreCase(string strA, int indexA, string strB, int indexB, int lenA, int lenB)
		{
			if (strA == null)
			{
				if (strB != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (strB == null)
				{
					return 1;
				}
				int num = Math.Min(lenA, strA.Length - indexA);
				int num2 = Math.Min(lenB, strB.Length - indexB);
				if (num == num2 && strA == strB)
				{
					return 0;
				}
				char* ptr = strA;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = strB;
				if (ptr2 != null)
				{
					ptr2 += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr3 = ptr + indexA;
				char* ptr4 = ptr3 + Math.Min(num, num2);
				char* ptr5 = ptr2 + indexB;
				while (ptr3 < ptr4)
				{
					if (*ptr3 != *ptr5)
					{
						char c = char.ToUpperInvariant(*ptr3);
						char c2 = char.ToUpperInvariant(*ptr5);
						if (c != c2)
						{
							return (int)(c - c2);
						}
					}
					ptr3++;
					ptr5++;
				}
				return num - num2;
			}
		}

		// Token: 0x06005E69 RID: 24169 RVA: 0x00143044 File Offset: 0x00141244
		internal unsafe void ToLowerAsciiInvariant(ReadOnlySpan<char> source, Span<char> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = TextInfo.ToLowerAsciiInvariant((char)(*source[i]));
			}
		}

		// Token: 0x06005E6A RID: 24170 RVA: 0x0014307C File Offset: 0x0014127C
		internal unsafe void ToUpperAsciiInvariant(ReadOnlySpan<char> source, Span<char> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = TextInfo.ToUpperAsciiInvariant((char)(*source[i]));
			}
		}

		// Token: 0x06005E6B RID: 24171 RVA: 0x001430B4 File Offset: 0x001412B4
		internal unsafe void ChangeCase(ReadOnlySpan<char> source, Span<char> destination, bool toUpper)
		{
			if (source.IsEmpty)
			{
				return;
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(destination))
				{
					char* ptr2 = reference2;
					int i = 0;
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					if (toUpper)
					{
						while (i < source.Length)
						{
							*(ptr4++) = this.ToUpper(*(ptr3++));
							i++;
						}
					}
					else
					{
						while (i < source.Length)
						{
							*(ptr4++) = this.ToLower(*(ptr3++));
							i++;
						}
					}
				}
			}
		}

		// Token: 0x040038D7 RID: 14551
		[OptionalField(VersionAdded = 2)]
		private string m_listSeparator;

		// Token: 0x040038D8 RID: 14552
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		// Token: 0x040038D9 RID: 14553
		[OptionalField(VersionAdded = 3)]
		private string m_cultureName;

		// Token: 0x040038DA RID: 14554
		[NonSerialized]
		private CultureData m_cultureData;

		// Token: 0x040038DB RID: 14555
		[NonSerialized]
		private string m_textInfoName;

		// Token: 0x040038DC RID: 14556
		[NonSerialized]
		private bool? m_IsAsciiCasingSameAsInvariant;

		// Token: 0x040038DD RID: 14557
		internal static volatile TextInfo s_Invariant;

		// Token: 0x040038DE RID: 14558
		[OptionalField(VersionAdded = 2)]
		private string customCultureName;

		// Token: 0x040038DF RID: 14559
		[OptionalField(VersionAdded = 1)]
		internal int m_nDataItem;

		// Token: 0x040038E0 RID: 14560
		[OptionalField(VersionAdded = 1)]
		internal bool m_useUserOverride;

		// Token: 0x040038E1 RID: 14561
		[OptionalField(VersionAdded = 1)]
		internal int m_win32LangID;

		// Token: 0x040038E2 RID: 14562
		private const int wordSeparatorMask = 536672256;
	}
}
