using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.IO.Enumeration
{
	// Token: 0x020009A0 RID: 2464
	internal static class FileSystemEnumerableFactory
	{
		// Token: 0x060059EE RID: 23022 RVA: 0x00130D2C File Offset: 0x0012EF2C
		internal static void NormalizeInputs(ref string directory, ref string expression, EnumerationOptions options)
		{
			if (Path.IsPathRooted(expression))
			{
				throw new ArgumentException("Second path fragment must not be a drive or UNC name.", "expression");
			}
			ReadOnlySpan<char> directoryName = Path.GetDirectoryName(expression.AsSpan());
			if (directoryName.Length != 0)
			{
				directory = Path.Join(directory, directoryName);
				expression = expression.Substring(directoryName.Length + 1);
			}
			MatchType matchType = options.MatchType;
			if (matchType == MatchType.Simple)
			{
				return;
			}
			if (matchType != MatchType.Win32)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if (string.IsNullOrEmpty(expression) || expression == "." || expression == "*.*")
			{
				expression = "*";
				return;
			}
			if (Path.DirectorySeparatorChar != '\\' && expression.IndexOfAny(FileSystemEnumerableFactory.s_unixEscapeChars) != -1)
			{
				expression = expression.Replace("\\", "\\\\");
				expression = expression.Replace("\"", "\\\"");
				expression = expression.Replace(">", "\\>");
				expression = expression.Replace("<", "\\<");
			}
			expression = FileSystemName.TranslateWin32Expression(expression);
		}

		// Token: 0x060059EF RID: 23023 RVA: 0x00130E44 File Offset: 0x0012F044
		private static bool MatchesPattern(string expression, ReadOnlySpan<char> name, EnumerationOptions options)
		{
			bool flag = (options.MatchCasing == MatchCasing.PlatformDefault && !PathInternal.IsCaseSensitive) || options.MatchCasing == MatchCasing.CaseInsensitive;
			MatchType matchType = options.MatchType;
			if (matchType == MatchType.Simple)
			{
				return FileSystemName.MatchesSimpleExpression(expression, name, flag);
			}
			if (matchType != MatchType.Win32)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			return FileSystemName.MatchesWin32Expression(expression, name, flag);
		}

		// Token: 0x060059F0 RID: 23024 RVA: 0x00130EA4 File Offset: 0x0012F0A4
		internal static IEnumerable<string> UserFiles(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<string>(directory, delegate(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return !entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x060059F1 RID: 23025 RVA: 0x00130F04 File Offset: 0x0012F104
		internal static IEnumerable<string> UserDirectories(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<string>(directory, delegate(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x060059F2 RID: 23026 RVA: 0x00130F64 File Offset: 0x0012F164
		internal static IEnumerable<string> UserEntries(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<string>(directory, delegate(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x060059F3 RID: 23027 RVA: 0x00130FC4 File Offset: 0x0012F1C4
		internal static IEnumerable<FileInfo> FileInfos(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<FileInfo>(directory, delegate(ref FileSystemEntry entry)
			{
				return (FileInfo)entry.ToFileSystemInfo();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return !entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x060059F4 RID: 23028 RVA: 0x00131024 File Offset: 0x0012F224
		internal static IEnumerable<DirectoryInfo> DirectoryInfos(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<DirectoryInfo>(directory, delegate(ref FileSystemEntry entry)
			{
				return (DirectoryInfo)entry.ToFileSystemInfo();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x060059F5 RID: 23029 RVA: 0x00131084 File Offset: 0x0012F284
		internal static IEnumerable<FileSystemInfo> FileSystemInfos(string directory, string expression, EnumerationOptions options)
		{
			return new FileSystemEnumerable<FileSystemInfo>(directory, delegate(ref FileSystemEntry entry)
			{
				return entry.ToFileSystemInfo();
			}, options)
			{
				ShouldIncludePredicate = delegate(ref FileSystemEntry entry)
				{
					return FileSystemEnumerableFactory.MatchesPattern(expression, entry.FileName, options);
				}
			};
		}

		// Token: 0x060059F6 RID: 23030 RVA: 0x001310E2 File Offset: 0x0012F2E2
		// Note: this type is marked as 'beforefieldinit'.
		static FileSystemEnumerableFactory()
		{
		}

		// Token: 0x0400359D RID: 13725
		private static readonly char[] s_unixEscapeChars = new char[] { '\\', '"', '<', '>' };

		// Token: 0x020009A1 RID: 2465
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x060059F7 RID: 23031 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x060059F8 RID: 23032 RVA: 0x001310FA File Offset: 0x0012F2FA
			internal bool <UserFiles>b__1(ref FileSystemEntry entry)
			{
				return !entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(this.expression, entry.FileName, this.options);
			}

			// Token: 0x0400359E RID: 13726
			public string expression;

			// Token: 0x0400359F RID: 13727
			public EnumerationOptions options;
		}

		// Token: 0x020009A2 RID: 2466
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060059F9 RID: 23033 RVA: 0x0013111D File Offset: 0x0012F31D
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060059FA RID: 23034 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x060059FB RID: 23035 RVA: 0x00131129 File Offset: 0x0012F329
			internal string <UserFiles>b__3_0(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}

			// Token: 0x060059FC RID: 23036 RVA: 0x00131129 File Offset: 0x0012F329
			internal string <UserDirectories>b__4_0(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}

			// Token: 0x060059FD RID: 23037 RVA: 0x00131129 File Offset: 0x0012F329
			internal string <UserEntries>b__5_0(ref FileSystemEntry entry)
			{
				return entry.ToSpecifiedFullPath();
			}

			// Token: 0x060059FE RID: 23038 RVA: 0x00131131 File Offset: 0x0012F331
			internal FileInfo <FileInfos>b__6_0(ref FileSystemEntry entry)
			{
				return (FileInfo)entry.ToFileSystemInfo();
			}

			// Token: 0x060059FF RID: 23039 RVA: 0x0013113E File Offset: 0x0012F33E
			internal DirectoryInfo <DirectoryInfos>b__7_0(ref FileSystemEntry entry)
			{
				return (DirectoryInfo)entry.ToFileSystemInfo();
			}

			// Token: 0x06005A00 RID: 23040 RVA: 0x0013114B File Offset: 0x0012F34B
			internal FileSystemInfo <FileSystemInfos>b__8_0(ref FileSystemEntry entry)
			{
				return entry.ToFileSystemInfo();
			}

			// Token: 0x040035A0 RID: 13728
			public static readonly FileSystemEnumerableFactory.<>c <>9 = new FileSystemEnumerableFactory.<>c();

			// Token: 0x040035A1 RID: 13729
			public static FileSystemEnumerable<string>.FindTransform <>9__3_0;

			// Token: 0x040035A2 RID: 13730
			public static FileSystemEnumerable<string>.FindTransform <>9__4_0;

			// Token: 0x040035A3 RID: 13731
			public static FileSystemEnumerable<string>.FindTransform <>9__5_0;

			// Token: 0x040035A4 RID: 13732
			public static FileSystemEnumerable<FileInfo>.FindTransform <>9__6_0;

			// Token: 0x040035A5 RID: 13733
			public static FileSystemEnumerable<DirectoryInfo>.FindTransform <>9__7_0;

			// Token: 0x040035A6 RID: 13734
			public static FileSystemEnumerable<FileSystemInfo>.FindTransform <>9__8_0;
		}

		// Token: 0x020009A3 RID: 2467
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x06005A01 RID: 23041 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06005A02 RID: 23042 RVA: 0x00131153 File Offset: 0x0012F353
			internal bool <UserDirectories>b__1(ref FileSystemEntry entry)
			{
				return entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(this.expression, entry.FileName, this.options);
			}

			// Token: 0x040035A7 RID: 13735
			public string expression;

			// Token: 0x040035A8 RID: 13736
			public EnumerationOptions options;
		}

		// Token: 0x020009A4 RID: 2468
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06005A03 RID: 23043 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x06005A04 RID: 23044 RVA: 0x00131176 File Offset: 0x0012F376
			internal bool <UserEntries>b__1(ref FileSystemEntry entry)
			{
				return FileSystemEnumerableFactory.MatchesPattern(this.expression, entry.FileName, this.options);
			}

			// Token: 0x040035A9 RID: 13737
			public string expression;

			// Token: 0x040035AA RID: 13738
			public EnumerationOptions options;
		}

		// Token: 0x020009A5 RID: 2469
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06005A05 RID: 23045 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06005A06 RID: 23046 RVA: 0x0013118F File Offset: 0x0012F38F
			internal bool <FileInfos>b__1(ref FileSystemEntry entry)
			{
				return !entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(this.expression, entry.FileName, this.options);
			}

			// Token: 0x040035AB RID: 13739
			public string expression;

			// Token: 0x040035AC RID: 13740
			public EnumerationOptions options;
		}

		// Token: 0x020009A6 RID: 2470
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x06005A07 RID: 23047 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x06005A08 RID: 23048 RVA: 0x001311B2 File Offset: 0x0012F3B2
			internal bool <DirectoryInfos>b__1(ref FileSystemEntry entry)
			{
				return entry.IsDirectory && FileSystemEnumerableFactory.MatchesPattern(this.expression, entry.FileName, this.options);
			}

			// Token: 0x040035AD RID: 13741
			public string expression;

			// Token: 0x040035AE RID: 13742
			public EnumerationOptions options;
		}

		// Token: 0x020009A7 RID: 2471
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x06005A09 RID: 23049 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x06005A0A RID: 23050 RVA: 0x001311D5 File Offset: 0x0012F3D5
			internal bool <FileSystemInfos>b__1(ref FileSystemEntry entry)
			{
				return FileSystemEnumerableFactory.MatchesPattern(this.expression, entry.FileName, this.options);
			}

			// Token: 0x040035AF RID: 13743
			public string expression;

			// Token: 0x040035B0 RID: 13744
			public EnumerationOptions options;
		}
	}
}
