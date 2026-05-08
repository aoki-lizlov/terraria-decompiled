using System;
using System.Buffers;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using Mono.Globalization.Unicode;

namespace System.Globalization
{
	// Token: 0x020009AE RID: 2478
	[Serializable]
	public class CompareInfo : IDeserializationCallback
	{
		// Token: 0x06005A46 RID: 23110 RVA: 0x001321EC File Offset: 0x001303EC
		internal unsafe static int InvariantIndexOf(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			char* ptr = source;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = value;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = CompareInfo.InvariantFindString(ptr + startIndex, count, ptr2, value.Length, ignoreCase, true);
			if (num >= 0)
			{
				return num + startIndex;
			}
			return -1;
		}

		// Token: 0x06005A47 RID: 23111 RVA: 0x00132240 File Offset: 0x00130440
		internal unsafe static int InvariantIndexOf(ReadOnlySpan<char> source, ReadOnlySpan<char> value, bool ignoreCase)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(value))
				{
					char* ptr2 = reference2;
					return CompareInfo.InvariantFindString(ptr, source.Length, ptr2, value.Length, ignoreCase, true);
				}
			}
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x00132278 File Offset: 0x00130478
		internal unsafe static int InvariantLastIndexOf(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			char* ptr = source;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = value;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = CompareInfo.InvariantFindString(ptr + (startIndex - count + 1), count, ptr2, value.Length, ignoreCase, false);
			if (num >= 0)
			{
				return num + startIndex - count + 1;
			}
			return -1;
		}

		// Token: 0x06005A49 RID: 23113 RVA: 0x001322D4 File Offset: 0x001304D4
		private unsafe static int InvariantFindString(char* source, int sourceCount, char* value, int valueCount, bool ignoreCase, bool start)
		{
			if (valueCount == 0)
			{
				if (!start)
				{
					return sourceCount - 1;
				}
				return 0;
			}
			else
			{
				if (sourceCount < valueCount)
				{
					return -1;
				}
				if (start)
				{
					int num = sourceCount - valueCount;
					if (ignoreCase)
					{
						char c = CompareInfo.InvariantToUpper(*value);
						for (int i = 0; i <= num; i++)
						{
							if (CompareInfo.InvariantToUpper(source[i]) == c)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									char c2 = CompareInfo.InvariantToUpper(source[i + j]);
									char c3 = CompareInfo.InvariantToUpper(value[j]);
									if (c2 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
					else
					{
						char c4 = *value;
						for (int i = 0; i <= num; i++)
						{
							if (source[i] == c4)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									char c5 = source[i + j];
									char c3 = value[j];
									if (c5 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
				}
				else
				{
					int num = sourceCount - valueCount;
					if (ignoreCase)
					{
						char c6 = CompareInfo.InvariantToUpper(*value);
						for (int i = num; i >= 0; i--)
						{
							if (CompareInfo.InvariantToUpper(source[i]) == c6)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									char c7 = CompareInfo.InvariantToUpper(source[i + j]);
									char c3 = CompareInfo.InvariantToUpper(value[j]);
									if (c7 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
					else
					{
						char c8 = *value;
						for (int i = num; i >= 0; i--)
						{
							if (source[i] == c8)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									char c9 = source[i + j];
									char c3 = value[j];
									if (c9 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
				}
				return -1;
			}
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x00132448 File Offset: 0x00130648
		private static char InvariantToUpper(char c)
		{
			if (c - 'a' > '\u0019')
			{
				return c;
			}
			return c - ' ';
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x0013245C File Offset: 0x0013065C
		private unsafe SortKey InvariantCreateSortKey(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			byte[] array;
			if (source.Length == 0)
			{
				array = Array.Empty<byte>();
			}
			else
			{
				array = new byte[source.Length * 2];
				fixed (string text = source)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					byte[] array2;
					byte* ptr2;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array2[0];
					}
					if ((options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) != CompareOptions.None)
					{
						short* ptr3 = (short*)ptr2;
						for (int i = 0; i < source.Length; i++)
						{
							ptr3[i] = (short)CompareInfo.InvariantToUpper(source[i]);
						}
					}
					else
					{
						Buffer.MemoryCopy((void*)ptr, (void*)ptr2, (long)array.Length, (long)array.Length);
					}
					array2 = null;
				}
			}
			return new SortKey(this.Name, source, options, array);
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x00132538 File Offset: 0x00130738
		internal CompareInfo(CultureInfo culture)
		{
			this.m_name = culture._name;
			this.InitSort(culture);
		}

		// Token: 0x06005A4D RID: 23117 RVA: 0x00132554 File Offset: 0x00130754
		public static CompareInfo GetCompareInfo(int culture, Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (assembly != typeof(object).Module.Assembly)
			{
				throw new ArgumentException("Only mscorlib's assembly is valid.");
			}
			return CompareInfo.GetCompareInfo(culture);
		}

		// Token: 0x06005A4E RID: 23118 RVA: 0x001325A4 File Offset: 0x001307A4
		public static CompareInfo GetCompareInfo(string name, Assembly assembly)
		{
			if (name == null || assembly == null)
			{
				throw new ArgumentNullException((name == null) ? "name" : "assembly");
			}
			if (assembly != typeof(object).Module.Assembly)
			{
				throw new ArgumentException("Only mscorlib's assembly is valid.");
			}
			return CompareInfo.GetCompareInfo(name);
		}

		// Token: 0x06005A4F RID: 23119 RVA: 0x001325FF File Offset: 0x001307FF
		public static CompareInfo GetCompareInfo(int culture)
		{
			if (CultureData.IsCustomCultureId(culture))
			{
				throw new ArgumentException("Customized cultures cannot be passed by LCID, only by name.", "culture");
			}
			return CultureInfo.GetCultureInfo(culture).CompareInfo;
		}

		// Token: 0x06005A50 RID: 23120 RVA: 0x00132624 File Offset: 0x00130824
		public static CompareInfo GetCompareInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return CultureInfo.GetCultureInfo(name).CompareInfo;
		}

		// Token: 0x06005A51 RID: 23121 RVA: 0x0013263F File Offset: 0x0013083F
		public unsafe static bool IsSortable(char ch)
		{
			return GlobalizationMode.Invariant || CompareInfo.IsSortable(&ch, 1);
		}

		// Token: 0x06005A52 RID: 23122 RVA: 0x00132654 File Offset: 0x00130854
		public unsafe static bool IsSortable(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (text.Length == 0)
			{
				return false;
			}
			if (GlobalizationMode.Invariant)
			{
				return true;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return CompareInfo.IsSortable(ptr, text.Length);
		}

		// Token: 0x06005A53 RID: 23123 RVA: 0x0013269E File Offset: 0x0013089E
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_name = null;
		}

		// Token: 0x06005A54 RID: 23124 RVA: 0x001326A7 File Offset: 0x001308A7
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialized();
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x001326A7 File Offset: 0x001308A7
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x06005A56 RID: 23126 RVA: 0x001326B0 File Offset: 0x001308B0
		private void OnDeserialized()
		{
			if (this.m_name == null)
			{
				CultureInfo cultureInfo = CultureInfo.GetCultureInfo(this.culture);
				this.m_name = cultureInfo._name;
				return;
			}
			this.InitSort(CultureInfo.GetCultureInfo(this.m_name));
		}

		// Token: 0x06005A57 RID: 23127 RVA: 0x001326EF File Offset: 0x001308EF
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.culture = CultureInfo.GetCultureInfo(this.Name).LCID;
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06005A58 RID: 23128 RVA: 0x00132707 File Offset: 0x00130907
		public virtual string Name
		{
			get
			{
				if (this.m_name == "zh-CHT" || this.m_name == "zh-CHS")
				{
					return this.m_name;
				}
				return this._sortName;
			}
		}

		// Token: 0x06005A59 RID: 23129 RVA: 0x0013273A File Offset: 0x0013093A
		public virtual int Compare(string string1, string string2)
		{
			return this.Compare(string1, string2, CompareOptions.None);
		}

		// Token: 0x06005A5A RID: 23130 RVA: 0x00132748 File Offset: 0x00130948
		public virtual int Compare(string string1, string string2, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return string.Compare(string1, string2, StringComparison.OrdinalIgnoreCase);
			}
			if ((options & CompareOptions.Ordinal) != CompareOptions.None)
			{
				if (options != CompareOptions.Ordinal)
				{
					throw new ArgumentException("CompareOption.Ordinal cannot be used with other options.", "options");
				}
				return string.CompareOrdinal(string1, string2);
			}
			else
			{
				if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
				{
					throw new ArgumentException("Value of flags is invalid.", "options");
				}
				if (string1 == null)
				{
					if (string2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (string2 == null)
					{
						return 1;
					}
					if (!GlobalizationMode.Invariant)
					{
						return this.internal_compare_switch(string1, 0, string1.Length, string2, 0, string2.Length, options);
					}
					if ((options & CompareOptions.IgnoreCase) != CompareOptions.None)
					{
						return CompareInfo.CompareOrdinalIgnoreCase(string1, string2);
					}
					return string.CompareOrdinal(string1, string2);
				}
			}
		}

		// Token: 0x06005A5B RID: 23131 RVA: 0x001327F4 File Offset: 0x001309F4
		internal int Compare(ReadOnlySpan<char> string1, string string2, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return CompareInfo.CompareOrdinalIgnoreCase(string1, string2.AsSpan());
			}
			if ((options & CompareOptions.Ordinal) != CompareOptions.None)
			{
				if (options != CompareOptions.Ordinal)
				{
					throw new ArgumentException("CompareOption.Ordinal cannot be used with other options.", "options");
				}
				return string.CompareOrdinal(string1, string2.AsSpan());
			}
			else
			{
				if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
				{
					throw new ArgumentException("Value of flags is invalid.", "options");
				}
				if (string2 == null)
				{
					return 1;
				}
				if (!GlobalizationMode.Invariant)
				{
					return this.CompareString(string1, string2, options);
				}
				if ((options & CompareOptions.IgnoreCase) == CompareOptions.None)
				{
					return string.CompareOrdinal(string1, string2.AsSpan());
				}
				return CompareInfo.CompareOrdinalIgnoreCase(string1, string2.AsSpan());
			}
		}

		// Token: 0x06005A5C RID: 23132 RVA: 0x00132891 File Offset: 0x00130A91
		internal int CompareOptionNone(ReadOnlySpan<char> string1, ReadOnlySpan<char> string2)
		{
			if (string1.Length == 0 || string2.Length == 0)
			{
				return string1.Length - string2.Length;
			}
			if (!GlobalizationMode.Invariant)
			{
				return this.CompareString(string1, string2, CompareOptions.None);
			}
			return string.CompareOrdinal(string1, string2);
		}

		// Token: 0x06005A5D RID: 23133 RVA: 0x001328CD File Offset: 0x00130ACD
		internal int CompareOptionIgnoreCase(ReadOnlySpan<char> string1, ReadOnlySpan<char> string2)
		{
			if (string1.Length == 0 || string2.Length == 0)
			{
				return string1.Length - string2.Length;
			}
			if (!GlobalizationMode.Invariant)
			{
				return this.CompareString(string1, string2, CompareOptions.IgnoreCase);
			}
			return CompareInfo.CompareOrdinalIgnoreCase(string1, string2);
		}

		// Token: 0x06005A5E RID: 23134 RVA: 0x00132909 File Offset: 0x00130B09
		public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2)
		{
			return this.Compare(string1, offset1, length1, string2, offset2, length2, CompareOptions.None);
		}

		// Token: 0x06005A5F RID: 23135 RVA: 0x0013291B File Offset: 0x00130B1B
		public virtual int Compare(string string1, int offset1, string string2, int offset2, CompareOptions options)
		{
			return this.Compare(string1, offset1, (string1 == null) ? 0 : (string1.Length - offset1), string2, offset2, (string2 == null) ? 0 : (string2.Length - offset2), options);
		}

		// Token: 0x06005A60 RID: 23136 RVA: 0x00132947 File Offset: 0x00130B47
		public virtual int Compare(string string1, int offset1, string string2, int offset2)
		{
			return this.Compare(string1, offset1, string2, offset2, CompareOptions.None);
		}

		// Token: 0x06005A61 RID: 23137 RVA: 0x00132958 File Offset: 0x00130B58
		public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				int num = string.Compare(string1, offset1, string2, offset2, (length1 < length2) ? length1 : length2, StringComparison.OrdinalIgnoreCase);
				if (length1 == length2 || num != 0)
				{
					return num;
				}
				if (length1 <= length2)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				if (length1 < 0 || length2 < 0)
				{
					throw new ArgumentOutOfRangeException((length1 < 0) ? "length1" : "length2", "Positive number required.");
				}
				if (offset1 < 0 || offset2 < 0)
				{
					throw new ArgumentOutOfRangeException((offset1 < 0) ? "offset1" : "offset2", "Positive number required.");
				}
				if (offset1 > ((string1 == null) ? 0 : string1.Length) - length1)
				{
					throw new ArgumentOutOfRangeException("string1", "Offset and length must refer to a position in the string.");
				}
				if (offset2 > ((string2 == null) ? 0 : string2.Length) - length2)
				{
					throw new ArgumentOutOfRangeException("string2", "Offset and length must refer to a position in the string.");
				}
				if ((options & CompareOptions.Ordinal) != CompareOptions.None)
				{
					if (options != CompareOptions.Ordinal)
					{
						throw new ArgumentException("CompareOption.Ordinal cannot be used with other options.", "options");
					}
				}
				else if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
				{
					throw new ArgumentException("Value of flags is invalid.", "options");
				}
				if (string1 == null)
				{
					if (string2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (string2 == null)
					{
						return 1;
					}
					ReadOnlySpan<char> readOnlySpan = string1.AsSpan(offset1, length1);
					ReadOnlySpan<char> readOnlySpan2 = string2.AsSpan(offset2, length2);
					if (options == CompareOptions.Ordinal)
					{
						return string.CompareOrdinal(readOnlySpan, readOnlySpan2);
					}
					if (!GlobalizationMode.Invariant)
					{
						return this.internal_compare_switch(string1, offset1, length1, string2, offset2, length2, options);
					}
					if ((options & CompareOptions.IgnoreCase) != CompareOptions.None)
					{
						return CompareInfo.CompareOrdinalIgnoreCase(readOnlySpan, readOnlySpan2);
					}
					return string.CompareOrdinal(readOnlySpan, readOnlySpan2);
				}
			}
		}

		// Token: 0x06005A62 RID: 23138 RVA: 0x00132AC8 File Offset: 0x00130CC8
		internal static int CompareOrdinalIgnoreCase(string strA, int indexA, int lengthA, string strB, int indexB, int lengthB)
		{
			return CompareInfo.CompareOrdinalIgnoreCase(strA.AsSpan(indexA, lengthA), strB.AsSpan(indexB, lengthB));
		}

		// Token: 0x06005A63 RID: 23139 RVA: 0x00132AE4 File Offset: 0x00130CE4
		internal unsafe static int CompareOrdinalIgnoreCase(ReadOnlySpan<char> strA, ReadOnlySpan<char> strB)
		{
			int num = Math.Min(strA.Length, strB.Length);
			int num2 = num;
			fixed (char* reference = MemoryMarshal.GetReference<char>(strA))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(strB))
				{
					char* ptr2 = reference2;
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					char c = (GlobalizationMode.Invariant ? char.MaxValue : '\u007f');
					while (num != 0 && *ptr3 <= c && *ptr4 <= c)
					{
						int num3 = (int)(*ptr3);
						int num4 = (int)(*ptr4);
						if (num3 == num4)
						{
							ptr3++;
							ptr4++;
							num--;
						}
						else
						{
							if (num3 - 97 <= 25)
							{
								num3 -= 32;
							}
							if (num4 - 97 <= 25)
							{
								num4 -= 32;
							}
							if (num3 != num4)
							{
								return num3 - num4;
							}
							ptr3++;
							ptr4++;
							num--;
						}
					}
					if (num == 0)
					{
						return strA.Length - strB.Length;
					}
					num2 -= num;
					return CompareInfo.CompareStringOrdinalIgnoreCase(ptr3, strA.Length - num2, ptr4, strB.Length - num2);
				}
			}
		}

		// Token: 0x06005A64 RID: 23140 RVA: 0x00132BD8 File Offset: 0x00130DD8
		public virtual bool IsPrefix(string source, string prefix, CompareOptions options)
		{
			if (source == null || prefix == null)
			{
				throw new ArgumentNullException((source == null) ? "source" : "prefix", "String reference not set to an instance of a String.");
			}
			if (prefix.Length == 0)
			{
				return true;
			}
			if (source.Length == 0)
			{
				return false;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.StartsWith(prefix, StringComparison.Ordinal);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (GlobalizationMode.Invariant)
			{
				return source.StartsWith(prefix, ((options & CompareOptions.IgnoreCase) != CompareOptions.None) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			}
			return this.StartsWith(source, prefix, options);
		}

		// Token: 0x06005A65 RID: 23141 RVA: 0x00132C72 File Offset: 0x00130E72
		internal bool IsPrefix(ReadOnlySpan<char> source, ReadOnlySpan<char> prefix, CompareOptions options)
		{
			return this.StartsWith(source, prefix, options);
		}

		// Token: 0x06005A66 RID: 23142 RVA: 0x00132C7D File Offset: 0x00130E7D
		public virtual bool IsPrefix(string source, string prefix)
		{
			return this.IsPrefix(source, prefix, CompareOptions.None);
		}

		// Token: 0x06005A67 RID: 23143 RVA: 0x00132C88 File Offset: 0x00130E88
		public virtual bool IsSuffix(string source, string suffix, CompareOptions options)
		{
			if (source == null || suffix == null)
			{
				throw new ArgumentNullException((source == null) ? "source" : "suffix", "String reference not set to an instance of a String.");
			}
			if (suffix.Length == 0)
			{
				return true;
			}
			if (source.Length == 0)
			{
				return false;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.EndsWith(suffix, StringComparison.Ordinal);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (GlobalizationMode.Invariant)
			{
				return source.EndsWith(suffix, ((options & CompareOptions.IgnoreCase) != CompareOptions.None) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			}
			return this.EndsWith(source, suffix, options);
		}

		// Token: 0x06005A68 RID: 23144 RVA: 0x00132D22 File Offset: 0x00130F22
		internal bool IsSuffix(ReadOnlySpan<char> source, ReadOnlySpan<char> suffix, CompareOptions options)
		{
			return this.EndsWith(source, suffix, options);
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x00132D2D File Offset: 0x00130F2D
		public virtual bool IsSuffix(string source, string suffix)
		{
			return this.IsSuffix(source, suffix, CompareOptions.None);
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x00132D38 File Offset: 0x00130F38
		public virtual int IndexOf(string source, char value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, CompareOptions.None);
		}

		// Token: 0x06005A6B RID: 23147 RVA: 0x00132D58 File Offset: 0x00130F58
		public virtual int IndexOf(string source, string value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, CompareOptions.None);
		}

		// Token: 0x06005A6C RID: 23148 RVA: 0x00132D78 File Offset: 0x00130F78
		public virtual int IndexOf(string source, char value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, options);
		}

		// Token: 0x06005A6D RID: 23149 RVA: 0x00132D98 File Offset: 0x00130F98
		public virtual int IndexOf(string source, string value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, options);
		}

		// Token: 0x06005A6E RID: 23150 RVA: 0x00132DB8 File Offset: 0x00130FB8
		public virtual int IndexOf(string source, char value, int startIndex)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, CompareOptions.None);
		}

		// Token: 0x06005A6F RID: 23151 RVA: 0x00132DDA File Offset: 0x00130FDA
		public virtual int IndexOf(string source, string value, int startIndex)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, CompareOptions.None);
		}

		// Token: 0x06005A70 RID: 23152 RVA: 0x00132DFC File Offset: 0x00130FFC
		public virtual int IndexOf(string source, char value, int startIndex, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x00132E1F File Offset: 0x0013101F
		public virtual int IndexOf(string source, string value, int startIndex, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
		}

		// Token: 0x06005A72 RID: 23154 RVA: 0x00132E42 File Offset: 0x00131042
		public virtual int IndexOf(string source, char value, int startIndex, int count)
		{
			return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		// Token: 0x06005A73 RID: 23155 RVA: 0x00132E50 File Offset: 0x00131050
		public virtual int IndexOf(string source, string value, int startIndex, int count)
		{
			return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		// Token: 0x06005A74 RID: 23156 RVA: 0x00132E60 File Offset: 0x00131060
		public virtual int IndexOf(string source, char value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (startIndex < 0 || startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > source.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			if (source.Length == 0)
			{
				return -1;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.IndexOf(value.ToString(), startIndex, count, StringComparison.OrdinalIgnoreCase);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (GlobalizationMode.Invariant)
			{
				return this.IndexOfOrdinal(source, new string(value, 1), startIndex, count, (options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) > CompareOptions.None);
			}
			return this.IndexOfCore(source, new string(value, 1), startIndex, count, options, null);
		}

		// Token: 0x06005A75 RID: 23157 RVA: 0x00132F38 File Offset: 0x00131138
		public virtual int IndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (source.Length == 0)
			{
				if (value.Length == 0)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (count < 0 || startIndex > source.Length - count)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return this.IndexOfOrdinal(source, value, startIndex, count, true);
				}
				if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal)
				{
					throw new ArgumentException("Value of flags is invalid.", "options");
				}
				if (GlobalizationMode.Invariant)
				{
					return this.IndexOfOrdinal(source, value, startIndex, count, (options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) > CompareOptions.None);
				}
				return this.IndexOfCore(source, value, startIndex, count, options, null);
			}
		}

		// Token: 0x06005A76 RID: 23158 RVA: 0x00133026 File Offset: 0x00131226
		internal int IndexOfOrdinal(ReadOnlySpan<char> source, ReadOnlySpan<char> value, bool ignoreCase)
		{
			return this.IndexOfOrdinalCore(source, value, ignoreCase);
		}

		// Token: 0x06005A77 RID: 23159 RVA: 0x00133031 File Offset: 0x00131231
		internal int IndexOf(ReadOnlySpan<char> source, ReadOnlySpan<char> value, CompareOptions options)
		{
			return this.IndexOfCore(source, value, options, null);
		}

		// Token: 0x06005A78 RID: 23160 RVA: 0x00133040 File Offset: 0x00131240
		internal unsafe int IndexOf(string source, string value, int startIndex, int count, CompareOptions options, int* matchLengthPtr)
		{
			*matchLengthPtr = 0;
			if (source.Length == 0)
			{
				if (value.Length == 0)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (startIndex >= source.Length)
				{
					return -1;
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					int num = this.IndexOfOrdinal(source, value, startIndex, count, true);
					if (num >= 0)
					{
						*matchLengthPtr = value.Length;
					}
					return num;
				}
				if (GlobalizationMode.Invariant)
				{
					int num2 = this.IndexOfOrdinal(source, value, startIndex, count, (options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) > CompareOptions.None);
					if (num2 >= 0)
					{
						*matchLengthPtr = value.Length;
					}
					return num2;
				}
				return this.IndexOfCore(source, value, startIndex, count, options, matchLengthPtr);
			}
		}

		// Token: 0x06005A79 RID: 23161 RVA: 0x001330CD File Offset: 0x001312CD
		internal int IndexOfOrdinal(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			if (GlobalizationMode.Invariant)
			{
				return CompareInfo.InvariantIndexOf(source, value, startIndex, count, ignoreCase);
			}
			return CompareInfo.IndexOfOrdinalCore(source, value, startIndex, count, ignoreCase);
		}

		// Token: 0x06005A7A RID: 23162 RVA: 0x001330EF File Offset: 0x001312EF
		public virtual int LastIndexOf(string source, char value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, CompareOptions.None);
		}

		// Token: 0x06005A7B RID: 23163 RVA: 0x00133116 File Offset: 0x00131316
		public virtual int LastIndexOf(string source, string value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, CompareOptions.None);
		}

		// Token: 0x06005A7C RID: 23164 RVA: 0x0013313D File Offset: 0x0013133D
		public virtual int LastIndexOf(string source, char value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, options);
		}

		// Token: 0x06005A7D RID: 23165 RVA: 0x00133164 File Offset: 0x00131364
		public virtual int LastIndexOf(string source, string value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, options);
		}

		// Token: 0x06005A7E RID: 23166 RVA: 0x0013318B File Offset: 0x0013138B
		public virtual int LastIndexOf(string source, char value, int startIndex)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, CompareOptions.None);
		}

		// Token: 0x06005A7F RID: 23167 RVA: 0x0013319A File Offset: 0x0013139A
		public virtual int LastIndexOf(string source, string value, int startIndex)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, CompareOptions.None);
		}

		// Token: 0x06005A80 RID: 23168 RVA: 0x001331A9 File Offset: 0x001313A9
		public virtual int LastIndexOf(string source, char value, int startIndex, CompareOptions options)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, options);
		}

		// Token: 0x06005A81 RID: 23169 RVA: 0x001331B9 File Offset: 0x001313B9
		public virtual int LastIndexOf(string source, string value, int startIndex, CompareOptions options)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, options);
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x001331C9 File Offset: 0x001313C9
		public virtual int LastIndexOf(string source, char value, int startIndex, int count)
		{
			return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		// Token: 0x06005A83 RID: 23171 RVA: 0x001331D7 File Offset: 0x001313D7
		public virtual int LastIndexOf(string source, string value, int startIndex, int count)
		{
			return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		// Token: 0x06005A84 RID: 23172 RVA: 0x001331E8 File Offset: 0x001313E8
		public virtual int LastIndexOf(string source, char value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal && options != CompareOptions.OrdinalIgnoreCase)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (source.Length == 0 && (startIndex == -1 || startIndex == 0))
			{
				return -1;
			}
			if (startIndex < 0 || startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (startIndex == source.Length)
			{
				startIndex--;
				if (count > 0)
				{
					count--;
				}
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.LastIndexOf(value.ToString(), startIndex, count, StringComparison.OrdinalIgnoreCase);
			}
			if (GlobalizationMode.Invariant)
			{
				return CompareInfo.InvariantLastIndexOf(source, new string(value, 1), startIndex, count, (options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) > CompareOptions.None);
			}
			return this.LastIndexOfCore(source, value.ToString(), startIndex, count, options);
		}

		// Token: 0x06005A85 RID: 23173 RVA: 0x001332E4 File Offset: 0x001314E4
		public virtual int LastIndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal && options != CompareOptions.OrdinalIgnoreCase)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (source.Length == 0 && (startIndex == -1 || startIndex == 0))
			{
				if (value.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (startIndex < 0 || startIndex > source.Length)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (startIndex == source.Length)
				{
					startIndex--;
					if (count > 0)
					{
						count--;
					}
					if (value.Length == 0 && count >= 0 && startIndex - count + 1 >= 0)
					{
						return startIndex;
					}
				}
				if (count < 0 || startIndex - count + 1 < 0)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return this.LastIndexOfOrdinal(source, value, startIndex, count, true);
				}
				if (GlobalizationMode.Invariant)
				{
					return CompareInfo.InvariantLastIndexOf(source, value, startIndex, count, (options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) > CompareOptions.None);
				}
				return this.LastIndexOfCore(source, value, startIndex, count, options);
			}
		}

		// Token: 0x06005A86 RID: 23174 RVA: 0x001333FD File Offset: 0x001315FD
		internal int LastIndexOfOrdinal(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			if (GlobalizationMode.Invariant)
			{
				return CompareInfo.InvariantLastIndexOf(source, value, startIndex, count, ignoreCase);
			}
			return CompareInfo.LastIndexOfOrdinalCore(source, value, startIndex, count, ignoreCase);
		}

		// Token: 0x06005A87 RID: 23175 RVA: 0x0013341F File Offset: 0x0013161F
		public virtual SortKey GetSortKey(string source, CompareOptions options)
		{
			if (GlobalizationMode.Invariant)
			{
				return this.InvariantCreateSortKey(source, options);
			}
			return this.CreateSortKey(source, options);
		}

		// Token: 0x06005A88 RID: 23176 RVA: 0x00133439 File Offset: 0x00131639
		public virtual SortKey GetSortKey(string source)
		{
			if (GlobalizationMode.Invariant)
			{
				return this.InvariantCreateSortKey(source, CompareOptions.None);
			}
			return this.CreateSortKey(source, CompareOptions.None);
		}

		// Token: 0x06005A89 RID: 23177 RVA: 0x00133454 File Offset: 0x00131654
		public override bool Equals(object value)
		{
			CompareInfo compareInfo = value as CompareInfo;
			return compareInfo != null && this.Name == compareInfo.Name;
		}

		// Token: 0x06005A8A RID: 23178 RVA: 0x0013347E File Offset: 0x0013167E
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06005A8B RID: 23179 RVA: 0x0013348C File Offset: 0x0013168C
		internal unsafe static int GetIgnoreCaseHash(string source)
		{
			if (source.Length == 0)
			{
				return source.GetHashCode();
			}
			char[] array = null;
			Span<char> span;
			if (source.Length <= 255)
			{
				span = new Span<char>(stackalloc byte[(UIntPtr)510], 255);
			}
			else
			{
				span = (array = ArrayPool<char>.Shared.Rent(source.Length));
			}
			Span<char> span2 = span;
			int num = source.AsSpan().ToUpperInvariant(span2);
			int num2 = Marvin.ComputeHash32(MemoryMarshal.AsBytes<char>(span2.Slice(0, num)), Marvin.DefaultSeed);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return num2;
		}

		// Token: 0x06005A8C RID: 23180 RVA: 0x00133520 File Offset: 0x00131720
		internal int GetHashCodeOfString(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (!GlobalizationMode.Invariant)
			{
				return this.GetHashCodeOfStringCore(source, options);
			}
			if ((options & CompareOptions.IgnoreCase) == CompareOptions.None)
			{
				return source.GetHashCode();
			}
			return CompareInfo.GetIgnoreCaseHash(source);
		}

		// Token: 0x06005A8D RID: 23181 RVA: 0x00133573 File Offset: 0x00131773
		public virtual int GetHashCode(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.GetHashCode();
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return CompareInfo.GetIgnoreCaseHash(source);
			}
			return this.GetHashCodeOfString(source, options);
		}

		// Token: 0x06005A8E RID: 23182 RVA: 0x001335A9 File Offset: 0x001317A9
		public override string ToString()
		{
			return "CompareInfo - " + this.Name;
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06005A8F RID: 23183 RVA: 0x001335BC File Offset: 0x001317BC
		public SortVersion Version
		{
			get
			{
				if (this.m_SortVersion == null)
				{
					if (GlobalizationMode.Invariant)
					{
						this.m_SortVersion = new SortVersion(0, 127, new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 127));
					}
					else
					{
						this.m_SortVersion = this.GetSortVersion();
					}
				}
				return this.m_SortVersion;
			}
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06005A90 RID: 23184 RVA: 0x00133611 File Offset: 0x00131811
		public int LCID
		{
			get
			{
				return CultureInfo.GetCultureInfo(this.Name).LCID;
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06005A91 RID: 23185 RVA: 0x00133623 File Offset: 0x00131823
		private static bool UseManagedCollation
		{
			get
			{
				if (!CompareInfo.managedCollationChecked)
				{
					CompareInfo.managedCollation = Environment.internalGetEnvironmentVariable("MONO_DISABLE_MANAGED_COLLATION") != "yes" && MSCompatUnicodeTable.IsReady;
					CompareInfo.managedCollationChecked = true;
				}
				return CompareInfo.managedCollation;
			}
		}

		// Token: 0x06005A92 RID: 23186 RVA: 0x0013365C File Offset: 0x0013185C
		private ISimpleCollator GetCollator()
		{
			if (this.collator != null)
			{
				return this.collator;
			}
			if (CompareInfo.collators == null)
			{
				Interlocked.CompareExchange<Dictionary<string, ISimpleCollator>>(ref CompareInfo.collators, new Dictionary<string, ISimpleCollator>(StringComparer.Ordinal), null);
			}
			Dictionary<string, ISimpleCollator> dictionary = CompareInfo.collators;
			lock (dictionary)
			{
				if (!CompareInfo.collators.TryGetValue(this._sortName, out this.collator))
				{
					this.collator = new SimpleCollator(CultureInfo.GetCultureInfo(this.m_name));
					CompareInfo.collators[this._sortName] = this.collator;
				}
			}
			return this.collator;
		}

		// Token: 0x06005A93 RID: 23187 RVA: 0x0013370C File Offset: 0x0013190C
		private SortKey CreateSortKeyCore(string source, CompareOptions options)
		{
			if (CompareInfo.UseManagedCollation)
			{
				return this.GetCollator().GetSortKey(source, options);
			}
			return new SortKey(this.culture, source, options);
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x00133730 File Offset: 0x00131930
		private int internal_index_switch(string s1, int sindex, int count, string s2, CompareOptions opt, bool first)
		{
			if (opt == CompareOptions.Ordinal)
			{
				if (!first)
				{
					return s1.LastIndexOfUnchecked(s2, sindex, count);
				}
				return s1.IndexOfUnchecked(s2, sindex, count);
			}
			else
			{
				if (!CompareInfo.UseManagedCollation)
				{
					return CompareInfo.internal_index(s1, sindex, count, s2, first);
				}
				return this.internal_index_managed(s1, sindex, count, s2, opt, first);
			}
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x00133783 File Offset: 0x00131983
		private int internal_compare_switch(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			if (!CompareInfo.UseManagedCollation)
			{
				return CompareInfo.internal_compare(str1, offset1, length1, str2, offset2, length2, options);
			}
			return this.internal_compare_managed(str1, offset1, length1, str2, offset2, length2, options);
		}

		// Token: 0x06005A96 RID: 23190 RVA: 0x001337AE File Offset: 0x001319AE
		private int internal_compare_managed(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			return this.GetCollator().Compare(str1, offset1, length1, str2, offset2, length2, options);
		}

		// Token: 0x06005A97 RID: 23191 RVA: 0x001337C6 File Offset: 0x001319C6
		private int internal_index_managed(string s, int sindex, int count, char c, CompareOptions opt, bool first)
		{
			if (!first)
			{
				return this.GetCollator().LastIndexOf(s, c, sindex, count, opt);
			}
			return this.GetCollator().IndexOf(s, c, sindex, count, opt);
		}

		// Token: 0x06005A98 RID: 23192 RVA: 0x001337F1 File Offset: 0x001319F1
		private int internal_index_managed(string s1, int sindex, int count, string s2, CompareOptions opt, bool first)
		{
			if (!first)
			{
				return this.GetCollator().LastIndexOf(s1, s2, sindex, count, opt);
			}
			return this.GetCollator().IndexOf(s1, s2, sindex, count, opt);
		}

		// Token: 0x06005A99 RID: 23193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int internal_compare_icall(char* str1, int length1, char* str2, int length2, CompareOptions options);

		// Token: 0x06005A9A RID: 23194 RVA: 0x0013381C File Offset: 0x00131A1C
		private unsafe static int internal_compare(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			char* ptr = str1;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = str2;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return CompareInfo.internal_compare_icall(ptr + offset1, length1, ptr2 + offset2, length2, options);
		}

		// Token: 0x06005A9B RID: 23195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int internal_index_icall(char* source, int sindex, int count, char* value, int value_length, bool first);

		// Token: 0x06005A9C RID: 23196 RVA: 0x00133860 File Offset: 0x00131A60
		private unsafe static int internal_index(string source, int sindex, int count, string value, bool first)
		{
			char* ptr = source;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = value;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return CompareInfo.internal_index_icall(ptr, sindex, count, ptr2, (value != null) ? value.Length : 0, first);
		}

		// Token: 0x06005A9D RID: 23197 RVA: 0x001338A4 File Offset: 0x00131AA4
		private void InitSort(CultureInfo culture)
		{
			this._sortName = culture.SortName;
		}

		// Token: 0x06005A9E RID: 23198 RVA: 0x001338B4 File Offset: 0x00131AB4
		private unsafe static int CompareStringOrdinalIgnoreCase(char* pString1, int length1, char* pString2, int length2)
		{
			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
			int num = 0;
			while (num < length1 && num < length2 && textInfo.ToUpper(*pString1) == textInfo.ToUpper(*pString2))
			{
				num++;
				pString1++;
				pString2++;
			}
			if (num >= length1)
			{
				if (num >= length2)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (num >= length2)
				{
					return 1;
				}
				return (int)(textInfo.ToUpper(*pString1) - textInfo.ToUpper(*pString2));
			}
		}

		// Token: 0x06005A9F RID: 23199 RVA: 0x0013391B File Offset: 0x00131B1B
		internal static int IndexOfOrdinalCore(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			if (!ignoreCase)
			{
				return source.IndexOfUnchecked(value, startIndex, count);
			}
			return source.IndexOfUncheckedIgnoreCase(value, startIndex, count);
		}

		// Token: 0x06005AA0 RID: 23200 RVA: 0x00133934 File Offset: 0x00131B34
		internal static int LastIndexOfOrdinalCore(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			if (!ignoreCase)
			{
				return source.LastIndexOfUnchecked(value, startIndex, count);
			}
			return source.LastIndexOfUncheckedIgnoreCase(value, startIndex, count);
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x0013394D File Offset: 0x00131B4D
		private int LastIndexOfCore(string source, string target, int startIndex, int count, CompareOptions options)
		{
			return this.internal_index_switch(source, startIndex, count, target, options, false);
		}

		// Token: 0x06005AA2 RID: 23202 RVA: 0x0013395D File Offset: 0x00131B5D
		private unsafe int IndexOfCore(string source, string target, int startIndex, int count, CompareOptions options, int* matchLengthPtr)
		{
			if (matchLengthPtr != null)
			{
				*matchLengthPtr = target.Length;
			}
			return this.internal_index_switch(source, startIndex, count, target, options, true);
		}

		// Token: 0x06005AA3 RID: 23203 RVA: 0x0013397C File Offset: 0x00131B7C
		private unsafe int IndexOfCore(ReadOnlySpan<char> source, ReadOnlySpan<char> target, CompareOptions options, int* matchLengthPtr)
		{
			string text = new string(source);
			string text2 = new string(target);
			return this.IndexOfCore(text, text2, 0, text.Length, options, matchLengthPtr);
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x001339AC File Offset: 0x00131BAC
		private int IndexOfOrdinalCore(ReadOnlySpan<char> source, ReadOnlySpan<char> value, bool ignoreCase)
		{
			string text = new string(source);
			string text2 = new string(value);
			if (!ignoreCase)
			{
				return text.IndexOfUnchecked(text2, 0, text.Length);
			}
			return text.IndexOfUncheckedIgnoreCase(text2, 0, text.Length);
		}

		// Token: 0x06005AA5 RID: 23205 RVA: 0x001339E8 File Offset: 0x00131BE8
		private int CompareString(ReadOnlySpan<char> string1, string string2, CompareOptions options)
		{
			string text = new string(string1);
			return this.internal_compare_switch(text, 0, text.Length, string2, 0, string2.Length, options);
		}

		// Token: 0x06005AA6 RID: 23206 RVA: 0x00133A14 File Offset: 0x00131C14
		private int CompareString(ReadOnlySpan<char> string1, ReadOnlySpan<char> string2, CompareOptions options)
		{
			string text = new string(string1);
			string text2 = new string(string2);
			return this.internal_compare_switch(text, 0, text.Length, new string(text2), 0, text2.Length, options);
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x00133A50 File Offset: 0x00131C50
		private unsafe static bool IsSortable(char* text, int length)
		{
			return MSCompatUnicodeTable.IsSortable(new string(text, 0, length));
		}

		// Token: 0x06005AA8 RID: 23208 RVA: 0x00133A5F File Offset: 0x00131C5F
		private SortKey CreateSortKey(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			return this.CreateSortKeyCore(source, options);
		}

		// Token: 0x06005AA9 RID: 23209 RVA: 0x00133A90 File Offset: 0x00131C90
		private bool StartsWith(string source, string prefix, CompareOptions options)
		{
			if (CompareInfo.UseManagedCollation)
			{
				return this.GetCollator().IsPrefix(source, prefix, options);
			}
			return source.Length >= prefix.Length && this.Compare(source, 0, prefix.Length, prefix, 0, prefix.Length, options) == 0;
		}

		// Token: 0x06005AAA RID: 23210 RVA: 0x00133ADD File Offset: 0x00131CDD
		private bool StartsWith(ReadOnlySpan<char> source, ReadOnlySpan<char> prefix, CompareOptions options)
		{
			return this.StartsWith(new string(source), new string(prefix), options);
		}

		// Token: 0x06005AAB RID: 23211 RVA: 0x00133AF4 File Offset: 0x00131CF4
		private bool EndsWith(string source, string suffix, CompareOptions options)
		{
			if (CompareInfo.UseManagedCollation)
			{
				return this.GetCollator().IsSuffix(source, suffix, options);
			}
			return source.Length >= suffix.Length && this.Compare(source, source.Length - suffix.Length, suffix.Length, suffix, 0, suffix.Length, options) == 0;
		}

		// Token: 0x06005AAC RID: 23212 RVA: 0x00133B4D File Offset: 0x00131D4D
		private bool EndsWith(ReadOnlySpan<char> source, ReadOnlySpan<char> suffix, CompareOptions options)
		{
			return this.EndsWith(new string(source), new string(suffix), options);
		}

		// Token: 0x06005AAD RID: 23213 RVA: 0x00133B62 File Offset: 0x00131D62
		internal int GetHashCodeOfStringCore(string source, CompareOptions options)
		{
			return this.GetSortKey(source, options).GetHashCode();
		}

		// Token: 0x06005AAE RID: 23214 RVA: 0x000174FB File Offset: 0x000156FB
		private SortVersion GetSortVersion()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005AAF RID: 23215 RVA: 0x00133B71 File Offset: 0x00131D71
		// Note: this type is marked as 'beforefieldinit'.
		static CompareInfo()
		{
		}

		// Token: 0x040035E9 RID: 13801
		private const CompareOptions ValidIndexMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);

		// Token: 0x040035EA RID: 13802
		private const CompareOptions ValidCompareMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort);

		// Token: 0x040035EB RID: 13803
		private const CompareOptions ValidHashCodeOfStringMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);

		// Token: 0x040035EC RID: 13804
		private const CompareOptions ValidSortkeyCtorMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort);

		// Token: 0x040035ED RID: 13805
		internal static readonly CompareInfo Invariant = CultureInfo.InvariantCulture.CompareInfo;

		// Token: 0x040035EE RID: 13806
		[OptionalField(VersionAdded = 2)]
		private string m_name;

		// Token: 0x040035EF RID: 13807
		[NonSerialized]
		private string _sortName;

		// Token: 0x040035F0 RID: 13808
		[OptionalField(VersionAdded = 3)]
		private SortVersion m_SortVersion;

		// Token: 0x040035F1 RID: 13809
		private int culture;

		// Token: 0x040035F2 RID: 13810
		[NonSerialized]
		private ISimpleCollator collator;

		// Token: 0x040035F3 RID: 13811
		private static Dictionary<string, ISimpleCollator> collators;

		// Token: 0x040035F4 RID: 13812
		private static bool managedCollation;

		// Token: 0x040035F5 RID: 13813
		private static bool managedCollationChecked;
	}
}
