using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000050 RID: 80
	internal class MSCompatUnicodeTable
	{
		// Token: 0x0600014E RID: 334 RVA: 0x000057EC File Offset: 0x000039EC
		public static TailoringInfo GetTailoringInfo(int lcid)
		{
			for (int i = 0; i < MSCompatUnicodeTable.tailoringInfos.Length; i++)
			{
				if (MSCompatUnicodeTable.tailoringInfos[i].LCID == lcid)
				{
					return MSCompatUnicodeTable.tailoringInfos[i];
				}
			}
			return null;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005824 File Offset: 0x00003A24
		public unsafe static void BuildTailoringTables(CultureInfo culture, TailoringInfo t, ref Contraction[] contractions, ref Level2Map[] diacriticals)
		{
			List<Contraction> list = new List<Contraction>();
			List<Level2Map> list2 = new List<Level2Map>();
			int num = 0;
			char[] array;
			char* ptr;
			if ((array = MSCompatUnicodeTable.tailoringArr) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			int i = t.TailoringIndex;
			int num2 = i + t.TailoringCount;
			while (i < num2)
			{
				int num3 = i + 1;
				switch (ptr[i])
				{
				case '\u0001':
				{
					i++;
					while (ptr[num3] != '\0')
					{
						num3++;
					}
					char[] array2 = new char[num3 - i];
					Marshal.Copy((IntPtr)((void*)(ptr + i)), array2, 0, num3 - i);
					byte[] array3 = new byte[4];
					for (int j = 0; j < 4; j++)
					{
						array3[j] = (byte)ptr[num3 + 1 + j];
					}
					list.Add(new Contraction(num, array2, null, array3));
					i = num3 + 6;
					num++;
					break;
				}
				case '\u0002':
					list2.Add(new Level2Map((byte)ptr[i + 1], (byte)ptr[i + 2]));
					i += 3;
					break;
				case '\u0003':
				{
					i++;
					while (ptr[num3] != '\0')
					{
						num3++;
					}
					char[] array2 = new char[num3 - i];
					Marshal.Copy((IntPtr)((void*)(ptr + i)), array2, 0, num3 - i);
					num3++;
					int num4 = num3;
					while (ptr[num4] != '\0')
					{
						num4++;
					}
					string text = new string(ptr, num3, num4 - num3);
					list.Add(new Contraction(num, array2, text, null));
					i = num4 + 1;
					num++;
					break;
				}
				default:
					throw new NotImplementedException(string.Format("Mono INTERNAL ERROR (Should not happen): Collation tailoring table is broken for culture {0} ({1}) at 0x{2:X}", culture.LCID, culture.Name, i));
				}
			}
			array = null;
			list.Sort(ContractionComparer.Instance);
			list2.Sort((Level2Map a, Level2Map b) => (int)(a.Source - b.Source));
			contractions = list.ToArray();
			diacriticals = list2.ToArray();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005A50 File Offset: 0x00003C50
		private unsafe static void SetCJKReferences(string name, ref CodePointIndexer cjkIndexer, ref byte* catTable, ref byte* lv1Table, ref CodePointIndexer lv2Indexer, ref byte* lv2Table)
		{
			if (name == "zh-CHS")
			{
				catTable = MSCompatUnicodeTable.cjkCHScategory;
				lv1Table = MSCompatUnicodeTable.cjkCHSlv1;
				cjkIndexer = MSCompatUnicodeTableUtil.CjkCHS;
				return;
			}
			if (name == "zh-CHT")
			{
				catTable = MSCompatUnicodeTable.cjkCHTcategory;
				lv1Table = MSCompatUnicodeTable.cjkCHTlv1;
				cjkIndexer = MSCompatUnicodeTableUtil.Cjk;
				return;
			}
			if (name == "ja")
			{
				catTable = MSCompatUnicodeTable.cjkJAcategory;
				lv1Table = MSCompatUnicodeTable.cjkJAlv1;
				cjkIndexer = MSCompatUnicodeTableUtil.Cjk;
				return;
			}
			if (!(name == "ko"))
			{
				return;
			}
			catTable = MSCompatUnicodeTable.cjkKOcategory;
			lv1Table = MSCompatUnicodeTable.cjkKOlv1;
			lv2Table = MSCompatUnicodeTable.cjkKOlv2;
			cjkIndexer = MSCompatUnicodeTableUtil.Cjk;
			lv2Indexer = MSCompatUnicodeTableUtil.Cjk;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005AF9 File Offset: 0x00003CF9
		public unsafe static byte Category(int cp)
		{
			return MSCompatUnicodeTable.categories[MSCompatUnicodeTableUtil.Category.ToIndex(cp)];
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005B0D File Offset: 0x00003D0D
		public unsafe static byte Level1(int cp)
		{
			return MSCompatUnicodeTable.level1[MSCompatUnicodeTableUtil.Level1.ToIndex(cp)];
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005B21 File Offset: 0x00003D21
		public unsafe static byte Level2(int cp)
		{
			return MSCompatUnicodeTable.level2[MSCompatUnicodeTableUtil.Level2.ToIndex(cp)];
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005B35 File Offset: 0x00003D35
		public unsafe static byte Level3(int cp)
		{
			return MSCompatUnicodeTable.level3[MSCompatUnicodeTableUtil.Level3.ToIndex(cp)];
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005B4C File Offset: 0x00003D4C
		public static bool IsSortable(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (!MSCompatUnicodeTable.IsSortable((int)s[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005B80 File Offset: 0x00003D80
		public static bool IsSortable(int cp)
		{
			return !MSCompatUnicodeTable.IsIgnorable(cp) || (cp == 0 || cp == 1600 || cp == 65279) || (6155 <= cp && cp <= 6158) || (8204 <= cp && cp <= 8207) || (8234 <= cp && cp <= 8238) || (8298 <= cp && cp <= 8303) || (8204 <= cp && cp <= 8207) || (65529 <= cp && cp <= 65533);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005C13 File Offset: 0x00003E13
		public static bool IsIgnorable(int cp)
		{
			return MSCompatUnicodeTable.IsIgnorable(cp, 1);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005C1C File Offset: 0x00003E1C
		public unsafe static bool IsIgnorable(int cp, byte flag)
		{
			if (cp == 0)
			{
				return true;
			}
			if ((flag & 1) != 0)
			{
				if (char.GetUnicodeCategory((char)cp) == UnicodeCategory.OtherNotAssigned)
				{
					return true;
				}
				if (55424 <= cp && cp < 56192)
				{
					return true;
				}
			}
			int num = MSCompatUnicodeTableUtil.Ignorable.ToIndex(cp);
			return num >= 0 && (MSCompatUnicodeTable.ignorableFlags[num] & flag) > 0;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005C71 File Offset: 0x00003E71
		public static bool IsIgnorableSymbol(int cp)
		{
			return MSCompatUnicodeTable.IsIgnorable(cp, 2);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005C7A File Offset: 0x00003E7A
		public static bool IsIgnorableNonSpacing(int cp)
		{
			return MSCompatUnicodeTable.IsIgnorable(cp, 4);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005C83 File Offset: 0x00003E83
		public static int ToKanaTypeInsensitive(int i)
		{
			if (12353 > i || i > 12436)
			{
				return i;
			}
			return i + 96;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005C9C File Offset: 0x00003E9C
		public static int ToWidthCompat(int i)
		{
			if (i < 8592)
			{
				return i;
			}
			if (i > 65280)
			{
				if (i <= 65374)
				{
					return i - 65280 + 32;
				}
				switch (i)
				{
				case 65504:
					return 162;
				case 65505:
					return 163;
				case 65506:
					return 172;
				case 65507:
					return 175;
				case 65508:
					return 166;
				case 65509:
					return 165;
				case 65510:
					return 8361;
				}
			}
			if (i > 13054)
			{
				return i;
			}
			if (i <= 8595)
			{
				return 56921 + i;
			}
			if (i < 9474)
			{
				return i;
			}
			if (i <= 9675)
			{
				if (i == 9474)
				{
					return 65512;
				}
				if (i == 9632)
				{
					return 65517;
				}
				if (i != 9675)
				{
					return i;
				}
				return 65518;
			}
			else
			{
				if (i < 12288)
				{
					return i;
				}
				if (i < 12593)
				{
					if (i <= 12300)
					{
						switch (i)
						{
						case 12288:
							return 32;
						case 12289:
							return 65380;
						case 12290:
							return 65377;
						default:
							if (i == 12300)
							{
								return 65378;
							}
							break;
						}
					}
					else
					{
						if (i == 12301)
						{
							return 65379;
						}
						if (i == 12539)
						{
							return 65381;
						}
					}
					return i;
				}
				if (i < 12644)
				{
					return i - 12592 + 65440;
				}
				if (i == 12644)
				{
					return 65440;
				}
				return i;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005E14 File Offset: 0x00004014
		public static bool HasSpecialWeight(char c)
		{
			if (c < 'ぁ')
			{
				return false;
			}
			if ('ｦ' <= c && c < 'ﾞ')
			{
				return true;
			}
			if ('㌀' <= c)
			{
				return false;
			}
			if (c < 'ゝ')
			{
				return c < '\u3099';
			}
			if (c < '\u3100')
			{
				return c != '・';
			}
			return c >= '㋐' && c < '㋿';
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005E84 File Offset: 0x00004084
		public static byte GetJapaneseDashType(char c)
		{
			if (c <= 'ゞ')
			{
				if (c != 'ゝ' && c != 'ゞ')
				{
					return 3;
				}
			}
			else
			{
				switch (c)
				{
				case 'ー':
					return 5;
				case 'ヽ':
				case 'ヾ':
					break;
				default:
					if (c != 'ｰ')
					{
						return 3;
					}
					break;
				}
			}
			return 4;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005ED0 File Offset: 0x000040D0
		public static bool IsHalfWidthKana(char c)
		{
			return 'ｦ' <= c && c <= 'ﾝ';
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005EE7 File Offset: 0x000040E7
		public static bool IsHiragana(char c)
		{
			return 'ぁ' <= c && c <= 'ゔ';
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005F00 File Offset: 0x00004100
		public static bool IsJapaneseSmallLetter(char c)
		{
			if ('ｧ' <= c && c <= 'ｯ')
			{
				return true;
			}
			if ('\u3040' < c && c < 'ヺ')
			{
				if (c <= 'ォ')
				{
					if (c <= 'っ')
					{
						switch (c)
						{
						case 'ぁ':
						case 'ぃ':
						case 'ぅ':
						case 'ぇ':
						case 'ぉ':
							break;
						case 'あ':
						case 'い':
						case 'う':
						case 'え':
							return false;
						default:
							if (c != 'っ')
							{
								return false;
							}
							break;
						}
					}
					else
					{
						switch (c)
						{
						case 'ゃ':
						case 'ゅ':
						case 'ょ':
							break;
						case 'や':
						case 'ゆ':
							return false;
						default:
							if (c != 'ゎ')
							{
								switch (c)
								{
								case 'ァ':
								case 'ィ':
								case 'ゥ':
								case 'ェ':
								case 'ォ':
									break;
								case 'ア':
								case 'イ':
								case 'ウ':
								case 'エ':
									return false;
								default:
									return false;
								}
							}
							break;
						}
					}
				}
				else if (c <= 'ョ')
				{
					if (c != 'ッ')
					{
						switch (c)
						{
						case 'ャ':
						case 'ュ':
						case 'ョ':
							break;
						case 'ヤ':
						case 'ユ':
							return false;
						default:
							return false;
						}
					}
				}
				else if (c != 'ヮ' && c != 'ヵ' && c != 'ヶ')
				{
					return false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000602F File Offset: 0x0000422F
		public static bool IsReady
		{
			get
			{
				return MSCompatUnicodeTable.isReady;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006038 File Offset: 0x00004238
		private static IntPtr GetResource(string name)
		{
			int num;
			Module module;
			return ((RuntimeAssembly)Assembly.GetExecutingAssembly()).GetManifestResourceInternal(name, out num, out module);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006059 File Offset: 0x00004259
		private unsafe static uint UInt32FromBytePtr(byte* raw, uint idx)
		{
			return (uint)((int)raw[idx] + ((int)raw[idx + 1U] << 8) + ((int)raw[idx + 2U] << 16) + ((int)raw[idx + 3U] << 24));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006080 File Offset: 0x00004280
		unsafe static MSCompatUnicodeTable()
		{
			IntPtr intPtr = MSCompatUnicodeTable.GetResource("collation.core.bin");
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			byte* ptr = (byte*)(void*)intPtr;
			intPtr = MSCompatUnicodeTable.GetResource("collation.tailoring.bin");
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			byte* ptr2 = (byte*)(void*)intPtr;
			if (ptr == null || ptr2 == null)
			{
				return;
			}
			if (*ptr != 3 || *ptr2 != 3)
			{
				return;
			}
			uint num = 1U;
			uint num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.ignorableFlags = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.categories = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.level1 = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.level2 = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.level3 = ptr + num;
			num += num2;
			num = 1U;
			uint num3 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
			num += 4U;
			MSCompatUnicodeTable.tailoringInfos = new TailoringInfo[num3];
			int num4 = 0;
			while ((long)num4 < (long)((ulong)num3))
			{
				int num5 = (int)MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
				num += 4U;
				int num6 = (int)MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
				num += 4U;
				int num7 = (int)MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
				num += 4U;
				TailoringInfo tailoringInfo = new TailoringInfo(num5, num6, num7, ptr2[(UIntPtr)(num++)] > 0);
				MSCompatUnicodeTable.tailoringInfos[num4] = tailoringInfo;
				num4++;
			}
			num += 2U;
			num3 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
			num += 4U;
			MSCompatUnicodeTable.tailoringArr = new char[num3];
			int num8 = 0;
			while ((long)num8 < (long)((ulong)num3))
			{
				MSCompatUnicodeTable.tailoringArr[num8] = (char)((int)ptr2[num] + ((int)ptr2[num + 1U] << 8));
				num8++;
				num += 2U;
			}
			MSCompatUnicodeTable.isReady = true;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006240 File Offset: 0x00004440
		public unsafe static void FillCJK(string culture, ref CodePointIndexer cjkIndexer, ref byte* catTable, ref byte* lv1Table, ref CodePointIndexer lv2Indexer, ref byte* lv2Table)
		{
			object obj = MSCompatUnicodeTable.forLock;
			lock (obj)
			{
				MSCompatUnicodeTable.FillCJKCore(culture, ref cjkIndexer, ref catTable, ref lv1Table, ref lv2Indexer, ref lv2Table);
				MSCompatUnicodeTable.SetCJKReferences(culture, ref cjkIndexer, ref catTable, ref lv1Table, ref lv2Indexer, ref lv2Table);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006294 File Offset: 0x00004494
		private unsafe static void FillCJKCore(string culture, ref CodePointIndexer cjkIndexer, ref byte* catTable, ref byte* lv1Table, ref CodePointIndexer cjkLv2Indexer, ref byte* lv2Table)
		{
			if (!MSCompatUnicodeTable.IsReady)
			{
				return;
			}
			string text = null;
			if (!(culture == "zh-CHS"))
			{
				if (!(culture == "zh-CHT"))
				{
					if (!(culture == "ja"))
					{
						if (culture == "ko")
						{
							text = "cjkKO";
							catTable = MSCompatUnicodeTable.cjkKOcategory;
							lv1Table = MSCompatUnicodeTable.cjkKOlv1;
						}
					}
					else
					{
						text = "cjkJA";
						catTable = MSCompatUnicodeTable.cjkJAcategory;
						lv1Table = MSCompatUnicodeTable.cjkJAlv1;
					}
				}
				else
				{
					text = "cjkCHT";
					catTable = MSCompatUnicodeTable.cjkCHTcategory;
					lv1Table = MSCompatUnicodeTable.cjkCHTlv1;
				}
			}
			else
			{
				text = "cjkCHS";
				catTable = MSCompatUnicodeTable.cjkCHScategory;
				lv1Table = MSCompatUnicodeTable.cjkCHSlv1;
			}
			if (text == null || lv1Table != (IntPtr)((UIntPtr)0))
			{
				return;
			}
			uint num = 0U;
			IntPtr intPtr = MSCompatUnicodeTable.GetResource(string.Format("collation.{0}.bin", text));
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			byte* ptr = (byte*)(void*)intPtr;
			num += 1U;
			uint num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			catTable = ptr + num;
			lv1Table = ptr + num + num2;
			if (!(culture == "zh-CHS"))
			{
				if (!(culture == "zh-CHT"))
				{
					if (!(culture == "ja"))
					{
						if (culture == "ko")
						{
							MSCompatUnicodeTable.cjkKOcategory = catTable;
							MSCompatUnicodeTable.cjkKOlv1 = lv1Table;
						}
					}
					else
					{
						MSCompatUnicodeTable.cjkJAcategory = catTable;
						MSCompatUnicodeTable.cjkJAlv1 = lv1Table;
					}
				}
				else
				{
					MSCompatUnicodeTable.cjkCHTcategory = catTable;
					MSCompatUnicodeTable.cjkCHTlv1 = lv1Table;
				}
			}
			else
			{
				MSCompatUnicodeTable.cjkCHScategory = catTable;
				MSCompatUnicodeTable.cjkCHSlv1 = lv1Table;
			}
			if (text != "cjkKO")
			{
				return;
			}
			intPtr = MSCompatUnicodeTable.GetResource("collation.cjkKOlv2.bin");
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			ptr = (byte*)(void*)intPtr;
			num = 5U;
			MSCompatUnicodeTable.cjkKOlv2 = ptr + num;
			lv2Table = MSCompatUnicodeTable.cjkKOlv2;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000025BE File Offset: 0x000007BE
		public MSCompatUnicodeTable()
		{
		}

		// Token: 0x04000D3B RID: 3387
		public static int MaxExpansionLength = 3;

		// Token: 0x04000D3C RID: 3388
		private unsafe static readonly byte* ignorableFlags;

		// Token: 0x04000D3D RID: 3389
		private unsafe static readonly byte* categories;

		// Token: 0x04000D3E RID: 3390
		private unsafe static readonly byte* level1;

		// Token: 0x04000D3F RID: 3391
		private unsafe static readonly byte* level2;

		// Token: 0x04000D40 RID: 3392
		private unsafe static readonly byte* level3;

		// Token: 0x04000D41 RID: 3393
		private unsafe static byte* cjkCHScategory;

		// Token: 0x04000D42 RID: 3394
		private unsafe static byte* cjkCHTcategory;

		// Token: 0x04000D43 RID: 3395
		private unsafe static byte* cjkJAcategory;

		// Token: 0x04000D44 RID: 3396
		private unsafe static byte* cjkKOcategory;

		// Token: 0x04000D45 RID: 3397
		private unsafe static byte* cjkCHSlv1;

		// Token: 0x04000D46 RID: 3398
		private unsafe static byte* cjkCHTlv1;

		// Token: 0x04000D47 RID: 3399
		private unsafe static byte* cjkJAlv1;

		// Token: 0x04000D48 RID: 3400
		private unsafe static byte* cjkKOlv1;

		// Token: 0x04000D49 RID: 3401
		private unsafe static byte* cjkKOlv2;

		// Token: 0x04000D4A RID: 3402
		private const int ResourceVersionSize = 1;

		// Token: 0x04000D4B RID: 3403
		private static readonly char[] tailoringArr;

		// Token: 0x04000D4C RID: 3404
		private static readonly TailoringInfo[] tailoringInfos;

		// Token: 0x04000D4D RID: 3405
		private static object forLock = new object();

		// Token: 0x04000D4E RID: 3406
		public static readonly bool isReady;

		// Token: 0x02000051 RID: 81
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000169 RID: 361 RVA: 0x0000643F File Offset: 0x0000463F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600016A RID: 362 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x0600016B RID: 363 RVA: 0x0000644B File Offset: 0x0000464B
			internal int <BuildTailoringTables>b__17_0(Level2Map a, Level2Map b)
			{
				return (int)(a.Source - b.Source);
			}

			// Token: 0x04000D4F RID: 3407
			public static readonly MSCompatUnicodeTable.<>c <>9 = new MSCompatUnicodeTable.<>c();

			// Token: 0x04000D50 RID: 3408
			public static Comparison<Level2Map> <>9__17_0;
		}
	}
}
