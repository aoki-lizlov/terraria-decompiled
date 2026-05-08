using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.IO
{
	// Token: 0x0200092C RID: 2348
	internal static class PathInternal
	{
		// Token: 0x060053C0 RID: 21440 RVA: 0x00119B0E File Offset: 0x00117D0E
		internal unsafe static int GetRootLength(ReadOnlySpan<char> path)
		{
			if (path.Length <= 0 || !PathInternal.IsDirectorySeparator((char)(*path[0])))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x00119B2D File Offset: 0x00117D2D
		internal static bool IsDirectorySeparator(char c)
		{
			return c == '/';
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x00119B34 File Offset: 0x00117D34
		internal static string NormalizeDirectorySeparators(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return path;
			}
			bool flag = true;
			for (int i = 0; i < path.Length; i++)
			{
				if (PathInternal.IsDirectorySeparator(path[i]) && i + 1 < path.Length && PathInternal.IsDirectorySeparator(path[i + 1]))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				return path;
			}
			StringBuilder stringBuilder = new StringBuilder(path.Length);
			for (int j = 0; j < path.Length; j++)
			{
				char c = path[j];
				if (!PathInternal.IsDirectorySeparator(c) || j + 1 >= path.Length || !PathInternal.IsDirectorySeparator(path[j + 1]))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060053C3 RID: 21443 RVA: 0x00119BE9 File Offset: 0x00117DE9
		internal static bool IsPartiallyQualified(ReadOnlySpan<char> path)
		{
			return !Path.IsPathRooted(path);
		}

		// Token: 0x060053C4 RID: 21444 RVA: 0x00119BF4 File Offset: 0x00117DF4
		internal static bool IsEffectivelyEmpty(string path)
		{
			return string.IsNullOrEmpty(path);
		}

		// Token: 0x060053C5 RID: 21445 RVA: 0x00119BFC File Offset: 0x00117DFC
		internal static bool IsEffectivelyEmpty(ReadOnlySpan<char> path)
		{
			return path.IsEmpty;
		}

		// Token: 0x060053C6 RID: 21446 RVA: 0x00119C05 File Offset: 0x00117E05
		internal unsafe static bool EndsInDirectorySeparator(ReadOnlySpan<char> path)
		{
			return path.Length > 0 && PathInternal.IsDirectorySeparator((char)(*path[path.Length - 1]));
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x00119C29 File Offset: 0x00117E29
		internal unsafe static bool StartsWithDirectorySeparator(ReadOnlySpan<char> path)
		{
			return path.Length > 0 && PathInternal.IsDirectorySeparator((char)(*path[0]));
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x00119C45 File Offset: 0x00117E45
		internal static string EnsureTrailingSeparator(string path)
		{
			if (!PathInternal.EndsInDirectorySeparator(path))
			{
				return path + "/";
			}
			return path;
		}

		// Token: 0x060053C9 RID: 21449 RVA: 0x00119C61 File Offset: 0x00117E61
		internal static string TrimEndingDirectorySeparator(string path)
		{
			if (!PathInternal.EndsInDirectorySeparator(path) || PathInternal.IsRoot(path))
			{
				return path;
			}
			return path.Substring(0, path.Length - 1);
		}

		// Token: 0x060053CA RID: 21450 RVA: 0x00119C8E File Offset: 0x00117E8E
		internal static ReadOnlySpan<char> TrimEndingDirectorySeparator(ReadOnlySpan<char> path)
		{
			if (!PathInternal.EndsInDirectorySeparator(path) || PathInternal.IsRoot(path))
			{
				return path;
			}
			return path.Slice(0, path.Length - 1);
		}

		// Token: 0x060053CB RID: 21451 RVA: 0x00119CB3 File Offset: 0x00117EB3
		internal static bool IsRoot(ReadOnlySpan<char> path)
		{
			return path.Length == PathInternal.GetRootLength(path);
		}

		// Token: 0x060053CC RID: 21452 RVA: 0x00119CC4 File Offset: 0x00117EC4
		internal static int GetCommonPathLength(string first, string second, bool ignoreCase)
		{
			int num = PathInternal.EqualStartingCharacterCount(first, second, ignoreCase);
			if (num == 0)
			{
				return num;
			}
			if (num == first.Length && (num == second.Length || PathInternal.IsDirectorySeparator(second[num])))
			{
				return num;
			}
			if (num == second.Length && PathInternal.IsDirectorySeparator(first[num]))
			{
				return num;
			}
			while (num > 0 && !PathInternal.IsDirectorySeparator(first[num - 1]))
			{
				num--;
			}
			return num;
		}

		// Token: 0x060053CD RID: 21453 RVA: 0x00119D34 File Offset: 0x00117F34
		internal unsafe static int EqualStartingCharacterCount(string first, string second, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(second))
			{
				return 0;
			}
			int num = 0;
			fixed (string text = first)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text2 = second)
				{
					char* ptr2 = text2;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					char* ptr5 = ptr3 + first.Length;
					char* ptr6 = ptr4 + second.Length;
					while (ptr3 != ptr5 && ptr4 != ptr6 && (*ptr3 == *ptr4 || (ignoreCase && char.ToUpperInvariant(*ptr3) == char.ToUpperInvariant(*ptr4))))
					{
						num++;
						ptr3++;
						ptr4++;
					}
				}
			}
			return num;
		}

		// Token: 0x060053CE RID: 21454 RVA: 0x00119DDC File Offset: 0x00117FDC
		internal static bool AreRootsEqual(string first, string second, StringComparison comparisonType)
		{
			int rootLength = PathInternal.GetRootLength(first);
			int rootLength2 = PathInternal.GetRootLength(second);
			return rootLength == rootLength2 && string.Compare(first, 0, second, 0, rootLength, comparisonType) == 0;
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x00119E18 File Offset: 0x00118018
		internal unsafe static string RemoveRelativeSegments(string path, int rootLength)
		{
			bool flag = false;
			int num = rootLength;
			if (PathInternal.IsDirectorySeparator(path[num - 1]))
			{
				num--;
			}
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)520], 260);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(span);
			if (num > 0)
			{
				valueStringBuilder.Append(path.AsSpan(0, num));
			}
			int i = num;
			while (i < path.Length)
			{
				char c = path[i];
				if (!PathInternal.IsDirectorySeparator(c) || i + 1 >= path.Length)
				{
					goto IL_0165;
				}
				if (!PathInternal.IsDirectorySeparator(path[i + 1]))
				{
					if ((i + 2 == path.Length || PathInternal.IsDirectorySeparator(path[i + 2])) && path[i + 1] == '.')
					{
						i++;
					}
					else
					{
						if (i + 2 >= path.Length || (i + 3 != path.Length && !PathInternal.IsDirectorySeparator(path[i + 3])) || path[i + 1] != '.' || path[i + 2] != '.')
						{
							goto IL_0165;
						}
						int j;
						for (j = valueStringBuilder.Length - 1; j >= num; j--)
						{
							if (PathInternal.IsDirectorySeparator(*valueStringBuilder[j]))
							{
								valueStringBuilder.Length = ((i + 3 >= path.Length && j == num) ? (j + 1) : j);
								break;
							}
						}
						if (j < num)
						{
							valueStringBuilder.Length = num;
						}
						i += 2;
					}
				}
				IL_0180:
				i++;
				continue;
				IL_0165:
				if (c != '/' && c == '/')
				{
					c = '/';
					flag = true;
				}
				valueStringBuilder.Append(c);
				goto IL_0180;
			}
			if (!flag && valueStringBuilder.Length == path.Length)
			{
				valueStringBuilder.Dispose();
				return path;
			}
			if (valueStringBuilder.Length >= rootLength)
			{
				return valueStringBuilder.ToString();
			}
			return path.Substring(0, rootLength);
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x060053D0 RID: 21456 RVA: 0x00119FF3 File Offset: 0x001181F3
		internal static StringComparison StringComparison
		{
			get
			{
				if (!PathInternal.s_isCaseSensitive)
				{
					return StringComparison.OrdinalIgnoreCase;
				}
				return StringComparison.Ordinal;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x060053D1 RID: 21457 RVA: 0x00119FFF File Offset: 0x001181FF
		internal static bool IsCaseSensitive
		{
			get
			{
				return PathInternal.s_isCaseSensitive;
			}
		}

		// Token: 0x060053D2 RID: 21458 RVA: 0x0011A008 File Offset: 0x00118208
		private static bool GetIsCaseSensitive()
		{
			bool flag;
			try
			{
				string text = Path.Combine(Path.GetTempPath(), "CASESENSITIVETEST" + Guid.NewGuid().ToString("N"));
				using (new FileStream(text, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.DeleteOnClose))
				{
					flag = !File.Exists(text.ToLowerInvariant());
				}
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060053D3 RID: 21459 RVA: 0x0000408A File Offset: 0x0000228A
		public static bool IsPartiallyQualified(string path)
		{
			return false;
		}

		// Token: 0x060053D4 RID: 21460 RVA: 0x0011A090 File Offset: 0x00118290
		public static bool HasIllegalCharacters(string path, bool checkAdditional)
		{
			return path.IndexOfAny(Path.InvalidPathChars) != -1;
		}

		// Token: 0x060053D5 RID: 21461 RVA: 0x0011A0A3 File Offset: 0x001182A3
		// Note: this type is marked as 'beforefieldinit'.
		static PathInternal()
		{
		}

		// Token: 0x0400333A RID: 13114
		internal const char DirectorySeparatorChar = '/';

		// Token: 0x0400333B RID: 13115
		internal const char AltDirectorySeparatorChar = '/';

		// Token: 0x0400333C RID: 13116
		internal const char VolumeSeparatorChar = '/';

		// Token: 0x0400333D RID: 13117
		internal const char PathSeparator = ':';

		// Token: 0x0400333E RID: 13118
		internal const string DirectorySeparatorCharAsString = "/";

		// Token: 0x0400333F RID: 13119
		private const char InvalidPathChar = '\0';

		// Token: 0x04003340 RID: 13120
		internal const string ParentDirectoryPrefix = "../";

		// Token: 0x04003341 RID: 13121
		private static readonly bool s_isCaseSensitive = PathInternal.GetIsCaseSensitive();
	}
}
