using System;
using System.Buffers;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;

namespace System.IO
{
	// Token: 0x02000987 RID: 2439
	[ComVisible(true)]
	public static class Path
	{
		// Token: 0x060058E2 RID: 22754 RVA: 0x0012C8DC File Offset: 0x0012AADC
		public static string ChangeExtension(string path, string extension)
		{
			if (path == null)
			{
				return null;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			int num = Path.findExtension(path);
			if (extension == null)
			{
				if (num >= 0)
				{
					return path.Substring(0, num);
				}
				return path;
			}
			else if (extension.Length == 0)
			{
				if (num >= 0)
				{
					return path.Substring(0, num + 1);
				}
				return path + ".";
			}
			else
			{
				if (path.Length != 0)
				{
					if (extension.Length > 0 && extension[0] != '.')
					{
						extension = "." + extension;
					}
				}
				else
				{
					extension = string.Empty;
				}
				if (num < 0)
				{
					return path + extension;
				}
				if (num > 0)
				{
					return path.Substring(0, num) + extension;
				}
				return extension;
			}
		}

		// Token: 0x060058E3 RID: 22755 RVA: 0x0012C998 File Offset: 0x0012AB98
		public static string Combine(string path1, string path2)
		{
			if (path1 == null)
			{
				throw new ArgumentNullException("path1");
			}
			if (path2 == null)
			{
				throw new ArgumentNullException("path2");
			}
			if (path1.Length == 0)
			{
				return path2;
			}
			if (path2.Length == 0)
			{
				return path1;
			}
			if (path1.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			if (path2.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			if (Path.IsPathRooted(path2))
			{
				return path2;
			}
			char c = path1[path1.Length - 1];
			if (c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar && c != Path.VolumeSeparatorChar)
			{
				return path1 + Path.DirectorySeparatorStr + path2;
			}
			return path1 + path2;
		}

		// Token: 0x060058E4 RID: 22756 RVA: 0x0012CA4C File Offset: 0x0012AC4C
		internal static string CleanPath(string s)
		{
			int length = s.Length;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			char c = s[0];
			if (length > 2 && c == '\\' && s[1] == '\\')
			{
				num3 = 2;
			}
			if (length == 1 && (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar))
			{
				return s;
			}
			for (int i = num3; i < length; i++)
			{
				char c2 = s[i];
				if (c2 == Path.DirectorySeparatorChar || c2 == Path.AltDirectorySeparatorChar)
				{
					if (Path.DirectorySeparatorChar != Path.AltDirectorySeparatorChar && c2 == Path.AltDirectorySeparatorChar)
					{
						num2++;
					}
					if (i + 1 == length)
					{
						num++;
					}
					else
					{
						c2 = s[i + 1];
						if (c2 == Path.DirectorySeparatorChar || c2 == Path.AltDirectorySeparatorChar)
						{
							num++;
						}
					}
				}
			}
			if (num == 0 && num2 == 0)
			{
				return s;
			}
			char[] array = new char[length - num];
			if (num3 != 0)
			{
				array[0] = '\\';
				array[1] = '\\';
			}
			int j = num3;
			int num4 = num3;
			while (j < length && num4 < array.Length)
			{
				char c3 = s[j];
				if (c3 != Path.DirectorySeparatorChar && c3 != Path.AltDirectorySeparatorChar)
				{
					array[num4++] = c3;
				}
				else if (num4 + 1 != array.Length)
				{
					array[num4++] = Path.DirectorySeparatorChar;
					while (j < length - 1)
					{
						c3 = s[j + 1];
						if (c3 != Path.DirectorySeparatorChar && c3 != Path.AltDirectorySeparatorChar)
						{
							break;
						}
						j++;
					}
				}
				j++;
			}
			return new string(array);
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x0012CBC8 File Offset: 0x0012ADC8
		public static string GetDirectoryName(string path)
		{
			if (path == string.Empty)
			{
				throw new ArgumentException("Invalid path");
			}
			if (path == null || Path.GetPathRoot(path) == path)
			{
				return null;
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("Argument string consists of whitespace characters only.");
			}
			if (path.IndexOfAny(Path.InvalidPathChars) > -1)
			{
				throw new ArgumentException("Path contains invalid characters");
			}
			int num = path.LastIndexOfAny(Path.PathSeparatorChars);
			if (num == 0)
			{
				num++;
			}
			if (num <= 0)
			{
				return string.Empty;
			}
			string text = path.Substring(0, num);
			int length = text.Length;
			if (length >= 2 && Path.DirectorySeparatorChar == '\\' && text[length - 1] == Path.VolumeSeparatorChar)
			{
				return text + Path.DirectorySeparatorChar.ToString();
			}
			if (length == 1 && Path.DirectorySeparatorChar == '\\' && path.Length >= 2 && path[num] == Path.VolumeSeparatorChar)
			{
				return text + Path.VolumeSeparatorChar.ToString();
			}
			return Path.CleanPath(text);
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x0012CCC7 File Offset: 0x0012AEC7
		public static ReadOnlySpan<char> GetDirectoryName(ReadOnlySpan<char> path)
		{
			return Path.GetDirectoryName(path.ToString()).AsSpan();
		}

		// Token: 0x060058E7 RID: 22759 RVA: 0x0012CCE0 File Offset: 0x0012AEE0
		public static string GetExtension(string path)
		{
			if (path == null)
			{
				return null;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			int num = Path.findExtension(path);
			if (num > -1 && num < path.Length - 1)
			{
				return path.Substring(num);
			}
			return string.Empty;
		}

		// Token: 0x060058E8 RID: 22760 RVA: 0x0012CD30 File Offset: 0x0012AF30
		public static string GetFileName(string path)
		{
			if (path == null || path.Length == 0)
			{
				return path;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			int num = path.LastIndexOfAny(Path.PathSeparatorChars);
			if (num >= 0)
			{
				return path.Substring(num + 1);
			}
			return path;
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x0012CD7E File Offset: 0x0012AF7E
		public static string GetFileNameWithoutExtension(string path)
		{
			return Path.ChangeExtension(Path.GetFileName(path), null);
		}

		// Token: 0x060058EA RID: 22762 RVA: 0x0012CD8C File Offset: 0x0012AF8C
		public static string GetFullPath(string path)
		{
			return Path.InsecureGetFullPath(path);
		}

		// Token: 0x060058EB RID: 22763 RVA: 0x0012CD8C File Offset: 0x0012AF8C
		internal static string GetFullPathInternal(string path)
		{
			return Path.InsecureGetFullPath(path);
		}

		// Token: 0x060058EC RID: 22764
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int GetFullPathName(string path, int numBufferChars, StringBuilder buffer, ref IntPtr lpFilePartOrNull);

		// Token: 0x060058ED RID: 22765 RVA: 0x0012CD94 File Offset: 0x0012AF94
		internal static string GetFullPathName(string path)
		{
			StringBuilder stringBuilder = new StringBuilder(260);
			IntPtr zero = IntPtr.Zero;
			int fullPathName = Path.GetFullPathName(path, 260, stringBuilder, ref zero);
			if (fullPathName == 0)
			{
				throw new IOException("Windows API call to GetFullPathName failed, Windows error code: " + Marshal.GetLastWin32Error().ToString());
			}
			if (fullPathName > 260)
			{
				stringBuilder = new StringBuilder(fullPathName);
				Path.GetFullPathName(path, fullPathName, stringBuilder, ref zero);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060058EE RID: 22766 RVA: 0x0012CE04 File Offset: 0x0012B004
		internal static string WindowsDriveAdjustment(string path)
		{
			if (path.Length < 2)
			{
				if (path.Length == 1 && (path[0] == '\\' || path[0] == '/'))
				{
					return Path.GetPathRoot(Directory.GetCurrentDirectory());
				}
				return path;
			}
			else
			{
				if (path[1] != ':' || !char.IsLetter(path[0]))
				{
					return path;
				}
				string text = Directory.InsecureGetCurrentDirectory();
				if (path.Length == 2)
				{
					if (text[0] == path[0])
					{
						path = text;
					}
					else
					{
						path = Path.GetFullPathName(path);
					}
				}
				else if (path[2] != Path.DirectorySeparatorChar && path[2] != Path.AltDirectorySeparatorChar)
				{
					if (text[0] == path[0])
					{
						path = Path.Combine(text, path.Substring(2, path.Length - 2));
					}
					else
					{
						path = Path.GetFullPathName(path);
					}
				}
				return path;
			}
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x0012CEE0 File Offset: 0x0012B0E0
		internal static string InsecureGetFullPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException(Locale.GetText("The specified path is not of a legal form (empty)."));
			}
			if (Environment.IsRunningOnWindows)
			{
				path = Path.WindowsDriveAdjustment(path);
			}
			char c = path[path.Length - 1];
			bool flag = true;
			if (path.Length >= 2 && Path.IsDirectorySeparator(path[0]) && Path.IsDirectorySeparator(path[1]))
			{
				if (path.Length == 2 || path.IndexOf(path[0], 2) < 0)
				{
					throw new ArgumentException("UNC paths should be of the form \\\\server\\share.");
				}
				if (path[0] != Path.DirectorySeparatorChar)
				{
					path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
				}
			}
			else if (!Path.IsPathRooted(path))
			{
				if (!Environment.IsRunningOnWindows)
				{
					int num = 0;
					while ((num = path.IndexOf('.', num)) != -1 && ++num != path.Length && path[num] != Path.DirectorySeparatorChar && path[num] != Path.AltDirectorySeparatorChar)
					{
					}
					flag = num > 0;
				}
				string text = Directory.InsecureGetCurrentDirectory();
				if (text[text.Length - 1] == Path.DirectorySeparatorChar)
				{
					path = text + path;
				}
				else
				{
					path = text + Path.DirectorySeparatorChar.ToString() + path;
				}
			}
			else if (Path.DirectorySeparatorChar == '\\' && path.Length >= 2 && Path.IsDirectorySeparator(path[0]) && !Path.IsDirectorySeparator(path[1]))
			{
				string text2 = Directory.InsecureGetCurrentDirectory();
				if (text2[1] == Path.VolumeSeparatorChar)
				{
					path = text2.Substring(0, 2) + path;
				}
				else
				{
					path = text2.Substring(0, text2.IndexOf('\\', text2.IndexOfUnchecked("\\\\", 0, text2.Length) + 1));
				}
			}
			if (flag)
			{
				path = Path.CanonicalizePath(path);
			}
			if (Path.IsDirectorySeparator(c) && path[path.Length - 1] != Path.DirectorySeparatorChar)
			{
				path += Path.DirectorySeparatorChar.ToString();
			}
			return path;
		}

		// Token: 0x060058F0 RID: 22768 RVA: 0x0012D0F8 File Offset: 0x0012B2F8
		internal static bool IsDirectorySeparator(char c)
		{
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x0012D10C File Offset: 0x0012B30C
		public static string GetPathRoot(string path)
		{
			if (path == null)
			{
				return null;
			}
			if (path.Trim().Length == 0)
			{
				throw new ArgumentException("The specified path is not of a legal form.");
			}
			if (!Path.IsPathRooted(path))
			{
				return string.Empty;
			}
			if (Path.DirectorySeparatorChar == '/')
			{
				if (!Path.IsDirectorySeparator(path[0]))
				{
					return string.Empty;
				}
				return Path.DirectorySeparatorStr;
			}
			else
			{
				int num = 2;
				if (path.Length == 1 && Path.IsDirectorySeparator(path[0]))
				{
					return Path.DirectorySeparatorStr;
				}
				if (path.Length < 2)
				{
					return string.Empty;
				}
				if (Path.IsDirectorySeparator(path[0]) && Path.IsDirectorySeparator(path[1]))
				{
					while (num < path.Length && !Path.IsDirectorySeparator(path[num]))
					{
						num++;
					}
					if (num < path.Length)
					{
						num++;
						while (num < path.Length && !Path.IsDirectorySeparator(path[num]))
						{
							num++;
						}
					}
					return Path.DirectorySeparatorStr + Path.DirectorySeparatorStr + path.Substring(2, num - 2).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
				}
				if (Path.IsDirectorySeparator(path[0]))
				{
					return Path.DirectorySeparatorStr;
				}
				if (path[1] == Path.VolumeSeparatorChar)
				{
					if (path.Length >= 3 && Path.IsDirectorySeparator(path[2]))
					{
						num++;
					}
					return path.Substring(0, num);
				}
				return Directory.GetCurrentDirectory().Substring(0, 2);
			}
		}

		// Token: 0x060058F2 RID: 22770 RVA: 0x0012D278 File Offset: 0x0012B478
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		public static string GetTempFileName()
		{
			FileStream fileStream = null;
			int num = 0;
			Random random = new Random();
			string tempPath = Path.GetTempPath();
			string text;
			do
			{
				int num2 = random.Next();
				text = Path.Combine(tempPath, "tmp" + (num2 + 1).ToString("x", CultureInfo.InvariantCulture) + ".tmp");
				try
				{
					fileStream = new FileStream(text, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read, 8192, false, (FileOptions)1);
				}
				catch (IOException ex)
				{
					if (ex._HResult != -2147024816 || num++ > 65536)
					{
						throw;
					}
				}
				catch (UnauthorizedAccessException ex2)
				{
					if (num++ > 65536)
					{
						throw new IOException(ex2.Message, ex2);
					}
				}
			}
			while (fileStream == null);
			fileStream.Close();
			return text;
		}

		// Token: 0x060058F3 RID: 22771 RVA: 0x0012D348 File Offset: 0x0012B548
		[EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
		public static string GetTempPath()
		{
			string temp_path = Path.get_temp_path();
			if (temp_path.Length > 0 && temp_path[temp_path.Length - 1] != Path.DirectorySeparatorChar)
			{
				return temp_path + Path.DirectorySeparatorChar.ToString();
			}
			return temp_path;
		}

		// Token: 0x060058F4 RID: 22772
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_temp_path();

		// Token: 0x060058F5 RID: 22773 RVA: 0x0012D38C File Offset: 0x0012B58C
		public static bool HasExtension(string path)
		{
			if (path == null || path.Trim().Length == 0)
			{
				return false;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			int num = Path.findExtension(path);
			return 0 <= num && num < path.Length - 1;
		}

		// Token: 0x060058F6 RID: 22774 RVA: 0x0012D3DC File Offset: 0x0012B5DC
		public unsafe static bool IsPathRooted(ReadOnlySpan<char> path)
		{
			if (path.Length == 0)
			{
				return false;
			}
			char c = (char)(*path[0]);
			return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar || (!Path.dirEqualsVolume && path.Length > 1 && *path[1] == (ushort)Path.VolumeSeparatorChar);
		}

		// Token: 0x060058F7 RID: 22775 RVA: 0x0012D433 File Offset: 0x0012B633
		public static bool IsPathRooted(string path)
		{
			if (path == null || path.Length == 0)
			{
				return false;
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException("Illegal characters in path.");
			}
			return Path.IsPathRooted(path.AsSpan());
		}

		// Token: 0x060058F8 RID: 22776 RVA: 0x0012D466 File Offset: 0x0012B666
		public static char[] GetInvalidFileNameChars()
		{
			if (Environment.IsRunningOnWindows)
			{
				return new char[]
				{
					'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
					'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
					'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d',
					'\u001e', '\u001f', '"', '<', '>', '|', ':', '*', '?', '\\',
					'/'
				};
			}
			return new char[] { '\0', '/' };
		}

		// Token: 0x060058F9 RID: 22777 RVA: 0x0012D48D File Offset: 0x0012B68D
		public static char[] GetInvalidPathChars()
		{
			if (Environment.IsRunningOnWindows)
			{
				return new char[]
				{
					'"', '<', '>', '|', '\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005',
					'\u0006', '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\u000e', '\u000f',
					'\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019',
					'\u001a', '\u001b', '\u001c', '\u001d', '\u001e', '\u001f'
				};
			}
			return new char[1];
		}

		// Token: 0x060058FA RID: 22778 RVA: 0x0012D4B0 File Offset: 0x0012B6B0
		public static string GetRandomFileName()
		{
			StringBuilder stringBuilder = new StringBuilder(12);
			RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
			byte[] array = new byte[11];
			randomNumberGenerator.GetBytes(array);
			for (int i = 0; i < array.Length; i++)
			{
				if (stringBuilder.Length == 8)
				{
					stringBuilder.Append('.');
				}
				int num = (int)(array[i] % 36);
				char c = (char)((num < 26) ? (num + 97) : (num - 26 + 48));
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x0012D524 File Offset: 0x0012B724
		private static int findExtension(string path)
		{
			if (path != null)
			{
				int num = path.LastIndexOf('.');
				int num2 = path.LastIndexOfAny(Path.PathSeparatorChars);
				if (num > num2)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x060058FC RID: 22780 RVA: 0x0012D550 File Offset: 0x0012B750
		static Path()
		{
		}

		// Token: 0x060058FD RID: 22781 RVA: 0x0012D5F4 File Offset: 0x0012B7F4
		private static string GetServerAndShare(string path)
		{
			int num = 2;
			while (num < path.Length && !Path.IsDirectorySeparator(path[num]))
			{
				num++;
			}
			if (num < path.Length)
			{
				num++;
				while (num < path.Length && !Path.IsDirectorySeparator(path[num]))
				{
					num++;
				}
			}
			return path.Substring(2, num - 2).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		}

		// Token: 0x060058FE RID: 22782 RVA: 0x0012D664 File Offset: 0x0012B864
		private static bool SameRoot(string root, string path)
		{
			if (root.Length < 2 || path.Length < 2)
			{
				return false;
			}
			if (!Path.IsDirectorySeparator(root[0]) || !Path.IsDirectorySeparator(root[1]))
			{
				return root[0].Equals(path[0]) && path[1] == Path.VolumeSeparatorChar && (root.Length <= 2 || path.Length <= 2 || (Path.IsDirectorySeparator(root[2]) && Path.IsDirectorySeparator(path[2])));
			}
			if (!Path.IsDirectorySeparator(path[0]) || !Path.IsDirectorySeparator(path[1]))
			{
				return false;
			}
			string serverAndShare = Path.GetServerAndShare(root);
			string serverAndShare2 = Path.GetServerAndShare(path);
			return string.Compare(serverAndShare, serverAndShare2, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x060058FF RID: 22783 RVA: 0x0012D738 File Offset: 0x0012B938
		private static string CanonicalizePath(string path)
		{
			if (path == null)
			{
				return path;
			}
			if (Environment.IsRunningOnWindows)
			{
				path = path.Trim();
			}
			if (path.Length == 0)
			{
				return path;
			}
			string pathRoot = Path.GetPathRoot(path);
			string[] array = path.Split(new char[]
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar
			});
			int num = 0;
			bool flag = Environment.IsRunningOnWindows && pathRoot.Length > 2 && Path.IsDirectorySeparator(pathRoot[0]) && Path.IsDirectorySeparator(pathRoot[1]);
			int num2 = (flag ? 3 : 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (Environment.IsRunningOnWindows)
				{
					array[i] = array[i].TrimEnd();
				}
				if (((flag && i == 2) || !(array[i] == ".")) && (i == 0 || array[i].Length != 0))
				{
					if (array[i] == "..")
					{
						if (num > num2)
						{
							num--;
						}
					}
					else
					{
						array[num++] = array[i];
					}
				}
			}
			if (num == 0 || (num == 1 && array[0] == ""))
			{
				return pathRoot;
			}
			string text = string.Join(Path.DirectorySeparatorStr, array, 0, num);
			if (!Environment.IsRunningOnWindows)
			{
				if (pathRoot != "" && text.Length > 0 && text[0] != '/')
				{
					text = pathRoot + text;
				}
				return text;
			}
			if (flag)
			{
				text = Path.DirectorySeparatorStr + text;
			}
			if (!Path.SameRoot(pathRoot, text))
			{
				text = pathRoot + text;
			}
			if (flag)
			{
				return text;
			}
			if (!Path.IsDirectorySeparator(path[0]) && Path.SameRoot(pathRoot, path))
			{
				if (text.Length <= 2 && !text.EndsWith(Path.DirectorySeparatorStr))
				{
					text += Path.DirectorySeparatorChar.ToString();
				}
				return text;
			}
			string currentDirectory = Directory.GetCurrentDirectory();
			if (currentDirectory.Length > 1 && currentDirectory[1] == Path.VolumeSeparatorChar)
			{
				if (text.Length == 0 || Path.IsDirectorySeparator(text[0]))
				{
					text += "\\";
				}
				return currentDirectory.Substring(0, 2) + text;
			}
			if (Path.IsDirectorySeparator(currentDirectory[currentDirectory.Length - 1]) && Path.IsDirectorySeparator(text[0]))
			{
				return currentDirectory + text.Substring(1);
			}
			return currentDirectory + text;
		}

		// Token: 0x06005900 RID: 22784 RVA: 0x0012D99C File Offset: 0x0012BB9C
		internal static bool IsPathSubsetOf(string subset, string path)
		{
			if (subset.Length > path.Length)
			{
				return false;
			}
			int num = subset.LastIndexOfAny(Path.PathSeparatorChars);
			if (string.Compare(subset, 0, path, 0, num) != 0)
			{
				return false;
			}
			num++;
			int num2 = path.IndexOfAny(Path.PathSeparatorChars, num);
			if (num2 >= num)
			{
				return string.Compare(subset, num, path, num, path.Length - num2) == 0;
			}
			return subset.Length == path.Length && string.Compare(subset, num, path, num, subset.Length - num) == 0;
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x0012DA24 File Offset: 0x0012BC24
		public static string Combine(params string[] paths)
		{
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = paths.Length;
			bool flag = false;
			foreach (string text in paths)
			{
				if (text == null)
				{
					throw new ArgumentNullException("One of the paths contains a null value", "paths");
				}
				if (text.Length != 0)
				{
					if (text.IndexOfAny(Path.InvalidPathChars) != -1)
					{
						throw new ArgumentException("Illegal characters in path.");
					}
					if (flag)
					{
						flag = false;
						stringBuilder.Append(Path.DirectorySeparatorStr);
					}
					num--;
					if (Path.IsPathRooted(text))
					{
						stringBuilder.Length = 0;
					}
					stringBuilder.Append(text);
					int length = text.Length;
					if (length > 0 && num > 0)
					{
						char c = text[length - 1];
						if (c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar && c != Path.VolumeSeparatorChar)
						{
							flag = true;
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005902 RID: 22786 RVA: 0x0012DB18 File Offset: 0x0012BD18
		public static string Combine(string path1, string path2, string path3)
		{
			if (path1 == null)
			{
				throw new ArgumentNullException("path1");
			}
			if (path2 == null)
			{
				throw new ArgumentNullException("path2");
			}
			if (path3 == null)
			{
				throw new ArgumentNullException("path3");
			}
			return Path.Combine(new string[] { path1, path2, path3 });
		}

		// Token: 0x06005903 RID: 22787 RVA: 0x0012DB68 File Offset: 0x0012BD68
		public static string Combine(string path1, string path2, string path3, string path4)
		{
			if (path1 == null)
			{
				throw new ArgumentNullException("path1");
			}
			if (path2 == null)
			{
				throw new ArgumentNullException("path2");
			}
			if (path3 == null)
			{
				throw new ArgumentNullException("path3");
			}
			if (path4 == null)
			{
				throw new ArgumentNullException("path4");
			}
			return Path.Combine(new string[] { path1, path2, path3, path4 });
		}

		// Token: 0x06005904 RID: 22788 RVA: 0x0012DBC8 File Offset: 0x0012BDC8
		internal static void Validate(string path)
		{
			Path.Validate(path, "path");
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x0012DBD8 File Offset: 0x0012BDD8
		internal static void Validate(string path, string parameterName)
		{
			if (path == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentException(Locale.GetText("Path is empty"));
			}
			if (path.IndexOfAny(Path.InvalidPathChars) != -1)
			{
				throw new ArgumentException(Locale.GetText("Path contains invalid chars"));
			}
			if (Environment.IsRunningOnWindows)
			{
				int num = path.IndexOf(':');
				if (num >= 0 && num != 1)
				{
					throw new ArgumentException(parameterName);
				}
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06005906 RID: 22790 RVA: 0x0012DC44 File Offset: 0x0012BE44
		internal static string DirectorySeparatorCharAsString
		{
			get
			{
				return Path.DirectorySeparatorStr;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06005907 RID: 22791 RVA: 0x0012DC4B File Offset: 0x0012BE4B
		internal static char[] TrimEndChars
		{
			get
			{
				if (!Environment.IsRunningOnWindows)
				{
					return Path.trimEndCharsUnix;
				}
				return Path.trimEndCharsWindows;
			}
		}

		// Token: 0x06005908 RID: 22792 RVA: 0x0012DC60 File Offset: 0x0012BE60
		internal static void CheckSearchPattern(string searchPattern)
		{
			int num;
			while ((num = searchPattern.IndexOf("..", StringComparison.Ordinal)) != -1)
			{
				if (num + 2 == searchPattern.Length)
				{
					throw new ArgumentException(Environment.GetResourceString("Search pattern cannot contain \"..\" to move up directories and can be contained only internally in file/directory names, as in \"a..b\"."));
				}
				if (searchPattern[num + 2] == Path.DirectorySeparatorChar || searchPattern[num + 2] == Path.AltDirectorySeparatorChar)
				{
					throw new ArgumentException(Environment.GetResourceString("Search pattern cannot contain \"..\" to move up directories and can be contained only internally in file/directory names, as in \"a..b\"."));
				}
				searchPattern = searchPattern.Substring(num + 2);
			}
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x0012DCD6 File Offset: 0x0012BED6
		internal static void CheckInvalidPathChars(string path, bool checkAdditional = false)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (PathInternal.HasIllegalCharacters(path, checkAdditional))
			{
				throw new ArgumentException(Environment.GetResourceString("Illegal characters in path."));
			}
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x0012DD00 File Offset: 0x0012BF00
		internal static string InternalCombine(string path1, string path2)
		{
			if (path1 == null || path2 == null)
			{
				throw new ArgumentNullException((path1 == null) ? "path1" : "path2");
			}
			Path.CheckInvalidPathChars(path1, false);
			Path.CheckInvalidPathChars(path2, false);
			if (path2.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Path cannot be the empty string or all whitespace."), "path2");
			}
			if (Path.IsPathRooted(path2))
			{
				throw new ArgumentException(Environment.GetResourceString("Second path fragment must not be a drive or UNC name."), "path2");
			}
			int length = path1.Length;
			if (length == 0)
			{
				return path2;
			}
			char c = path1[length - 1];
			if (c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar && c != Path.VolumeSeparatorChar)
			{
				return path1 + Path.DirectorySeparatorCharAsString + path2;
			}
			return path1 + path2;
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x0012DDB4 File Offset: 0x0012BFB4
		public unsafe static ReadOnlySpan<char> GetFileName(ReadOnlySpan<char> path)
		{
			int length = Path.GetPathRoot(new string(path)).Length;
			int num = path.Length;
			while (--num >= 0)
			{
				if (num < length || Path.IsDirectorySeparator((char)(*path[num])))
				{
					return path.Slice(num + 1, path.Length - num - 1);
				}
			}
			return path;
		}

		// Token: 0x0600590C RID: 22796 RVA: 0x0012DE0F File Offset: 0x0012C00F
		public static string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2)
		{
			if (path1.Length == 0)
			{
				return new string(path2);
			}
			if (path2.Length == 0)
			{
				return new string(path1);
			}
			return Path.JoinInternal(path1, path2);
		}

		// Token: 0x0600590D RID: 22797 RVA: 0x0012DE38 File Offset: 0x0012C038
		public static string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3)
		{
			if (path1.Length == 0)
			{
				return Path.Join(path2, path3);
			}
			if (path2.Length == 0)
			{
				return Path.Join(path1, path3);
			}
			if (path3.Length == 0)
			{
				return Path.Join(path1, path2);
			}
			return Path.JoinInternal(path1, path2, path3);
		}

		// Token: 0x0600590E RID: 22798 RVA: 0x0012DE78 File Offset: 0x0012C078
		public unsafe static bool TryJoin(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, Span<char> destination, out int charsWritten)
		{
			charsWritten = 0;
			if (path1.Length == 0 && path2.Length == 0)
			{
				return true;
			}
			if (path1.Length == 0 || path2.Length == 0)
			{
				ref ReadOnlySpan<char> ptr = (ref path1.Length == 0 ? ref path2 : ref path1);
				if (destination.Length < ptr.Length)
				{
					return false;
				}
				ptr.CopyTo(destination);
				charsWritten = ptr.Length;
				return true;
			}
			else
			{
				bool flag = !PathInternal.EndsInDirectorySeparator(path1) && !PathInternal.StartsWithDirectorySeparator(path2);
				int num = path1.Length + path2.Length + (flag ? 1 : 0);
				if (destination.Length < num)
				{
					return false;
				}
				path1.CopyTo(destination);
				if (flag)
				{
					*destination[path1.Length] = Path.DirectorySeparatorChar;
				}
				path2.CopyTo(destination.Slice(path1.Length + (flag ? 1 : 0)));
				charsWritten = num;
				return true;
			}
		}

		// Token: 0x0600590F RID: 22799 RVA: 0x0012DF5C File Offset: 0x0012C15C
		public unsafe static bool TryJoin(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3, Span<char> destination, out int charsWritten)
		{
			charsWritten = 0;
			if (path1.Length == 0 && path2.Length == 0 && path3.Length == 0)
			{
				return true;
			}
			if (path1.Length == 0)
			{
				return Path.TryJoin(path2, path3, destination, out charsWritten);
			}
			if (path2.Length == 0)
			{
				return Path.TryJoin(path1, path3, destination, out charsWritten);
			}
			if (path3.Length == 0)
			{
				return Path.TryJoin(path1, path2, destination, out charsWritten);
			}
			int num = ((PathInternal.EndsInDirectorySeparator(path1) || PathInternal.StartsWithDirectorySeparator(path2)) ? 0 : 1);
			bool flag = !PathInternal.EndsInDirectorySeparator(path2) && !PathInternal.StartsWithDirectorySeparator(path3);
			if (flag)
			{
				num++;
			}
			int num2 = path1.Length + path2.Length + path3.Length + num;
			if (destination.Length < num2)
			{
				return false;
			}
			Path.TryJoin(path1, path2, destination, out charsWritten);
			if (flag)
			{
				int num3 = charsWritten;
				charsWritten = num3 + 1;
				*destination[num3] = Path.DirectorySeparatorChar;
			}
			path3.CopyTo(destination.Slice(charsWritten));
			charsWritten += path3.Length;
			return true;
		}

		// Token: 0x06005910 RID: 22800 RVA: 0x0012E064 File Offset: 0x0012C264
		private unsafe static string JoinInternal(ReadOnlySpan<char> first, ReadOnlySpan<char> second)
		{
			bool flag = PathInternal.IsDirectorySeparator((char)(*first[first.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*second[0]));
			fixed (char* reference = MemoryMarshal.GetReference<char>(first))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(second))
				{
					char* ptr2 = reference2;
					return string.Create<ValueTuple<IntPtr, int, IntPtr, int, bool>>(first.Length + second.Length + (flag ? 0 : 1), new ValueTuple<IntPtr, int, IntPtr, int, bool>((IntPtr)((void*)ptr), first.Length, (IntPtr)((void*)ptr2), second.Length, flag), delegate(Span<char> destination, [TupleElementNames(new string[] { "First", "FirstLength", "Second", "SecondLength", "HasSeparator" })] ValueTuple<IntPtr, int, IntPtr, int, bool> state)
					{
						Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
						span.CopyTo(destination);
						if (!state.Item5)
						{
							*destination[state.Item2] = '/';
						}
						span = new Span<char>((void*)state.Item3, state.Item4);
						span.CopyTo(destination.Slice(state.Item2 + (state.Item5 ? 0 : 1)));
					});
				}
			}
		}

		// Token: 0x06005911 RID: 22801 RVA: 0x0012E10C File Offset: 0x0012C30C
		private unsafe static string JoinInternal(ReadOnlySpan<char> first, ReadOnlySpan<char> second, ReadOnlySpan<char> third)
		{
			bool flag = PathInternal.IsDirectorySeparator((char)(*first[first.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*second[0]));
			bool flag2 = PathInternal.IsDirectorySeparator((char)(*second[second.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*third[0]));
			fixed (char* reference = MemoryMarshal.GetReference<char>(first))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(second))
				{
					char* ptr2 = reference2;
					fixed (char* reference3 = MemoryMarshal.GetReference<char>(third))
					{
						char* ptr3 = reference3;
						return string.Create<ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, bool, ValueTuple<bool>>>(first.Length + second.Length + third.Length + (flag ? 0 : 1) + (flag2 ? 0 : 1), new ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, bool, ValueTuple<bool>>((IntPtr)((void*)ptr), first.Length, (IntPtr)((void*)ptr2), second.Length, (IntPtr)((void*)ptr3), third.Length, flag, new ValueTuple<bool>(flag2)), delegate(Span<char> destination, [TupleElementNames(new string[] { "First", "FirstLength", "Second", "SecondLength", "Third", "ThirdLength", "FirstHasSeparator", "ThirdHasSeparator", null })] ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, bool, ValueTuple<bool>> state)
						{
							Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
							span.CopyTo(destination);
							if (!state.Item7)
							{
								*destination[state.Item2] = '/';
							}
							span = new Span<char>((void*)state.Item3, state.Item4);
							span.CopyTo(destination.Slice(state.Item2 + (state.Item7 ? 0 : 1)));
							if (!state.Rest.Item1)
							{
								*destination[destination.Length - state.Item6 - 1] = '/';
							}
							span = new Span<char>((void*)state.Item5, state.Item6);
							span.CopyTo(destination.Slice(destination.Length - state.Item6));
						});
					}
				}
			}
		}

		// Token: 0x06005912 RID: 22802 RVA: 0x0012E214 File Offset: 0x0012C414
		private unsafe static string JoinInternal(ReadOnlySpan<char> first, ReadOnlySpan<char> second, ReadOnlySpan<char> third, ReadOnlySpan<char> fourth)
		{
			bool flag = PathInternal.IsDirectorySeparator((char)(*first[first.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*second[0]));
			bool flag2 = PathInternal.IsDirectorySeparator((char)(*second[second.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*third[0]));
			bool flag3 = PathInternal.IsDirectorySeparator((char)(*third[third.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*fourth[0]));
			fixed (char* reference = MemoryMarshal.GetReference<char>(first))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(second))
				{
					char* ptr2 = reference2;
					fixed (char* reference3 = MemoryMarshal.GetReference<char>(third))
					{
						char* ptr3 = reference3;
						fixed (char* reference4 = MemoryMarshal.GetReference<char>(fourth))
						{
							char* ptr4 = reference4;
							return string.Create<ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, IntPtr, ValueTuple<int, bool, bool, bool>>>(first.Length + second.Length + third.Length + fourth.Length + (flag ? 0 : 1) + (flag2 ? 0 : 1) + (flag3 ? 0 : 1), new ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, IntPtr, ValueTuple<int, bool, bool, bool>>((IntPtr)((void*)ptr), first.Length, (IntPtr)((void*)ptr2), second.Length, (IntPtr)((void*)ptr3), third.Length, (IntPtr)((void*)ptr4), new ValueTuple<int, bool, bool, bool>(fourth.Length, flag, flag2, flag3)), delegate(Span<char> destination, [TupleElementNames(new string[]
							{
								"First", "FirstLength", "Second", "SecondLength", "Third", "ThirdLength", "Fourth", "FourthLength", "FirstHasSeparator", "ThirdHasSeparator",
								"FourthHasSeparator", null, null, null, null
							})] ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, IntPtr, ValueTuple<int, bool, bool, bool>> state)
							{
								Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
								span.CopyTo(destination);
								if (!state.Rest.Item2)
								{
									*destination[state.Item2] = '/';
								}
								span = new Span<char>((void*)state.Item3, state.Item4);
								span.CopyTo(destination.Slice(state.Item2 + (state.Rest.Item2 ? 0 : 1)));
								if (!state.Rest.Item3)
								{
									*destination[state.Item2 + state.Item4 + (state.Rest.Item2 ? 0 : 1)] = '/';
								}
								span = new Span<char>((void*)state.Item5, state.Item6);
								span.CopyTo(destination.Slice(state.Item2 + state.Item4 + (state.Rest.Item2 ? 0 : 1) + (state.Rest.Item3 ? 0 : 1)));
								if (!state.Rest.Item4)
								{
									*destination[destination.Length - state.Rest.Item1 - 1] = '/';
								}
								span = new Span<char>((void*)state.Item7, state.Rest.Item1);
								span.CopyTo(destination.Slice(destination.Length - state.Rest.Item1));
							});
						}
					}
				}
			}
		}

		// Token: 0x06005913 RID: 22803 RVA: 0x0012E371 File Offset: 0x0012C571
		public static ReadOnlySpan<char> GetExtension(ReadOnlySpan<char> path)
		{
			return Path.GetExtension(path.ToString()).AsSpan();
		}

		// Token: 0x06005914 RID: 22804 RVA: 0x0012E38A File Offset: 0x0012C58A
		public static ReadOnlySpan<char> GetFileNameWithoutExtension(ReadOnlySpan<char> path)
		{
			return Path.GetFileNameWithoutExtension(path.ToString()).AsSpan();
		}

		// Token: 0x06005915 RID: 22805 RVA: 0x0012E3A3 File Offset: 0x0012C5A3
		public static ReadOnlySpan<char> GetPathRoot(ReadOnlySpan<char> path)
		{
			return Path.GetPathRoot(path.ToString()).AsSpan();
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x0012E3BC File Offset: 0x0012C5BC
		public static bool HasExtension(ReadOnlySpan<char> path)
		{
			return Path.HasExtension(path.ToString());
		}

		// Token: 0x06005917 RID: 22807 RVA: 0x0012E3D0 File Offset: 0x0012C5D0
		public static string GetRelativePath(string relativeTo, string path)
		{
			return Path.GetRelativePath(relativeTo, path, Path.StringComparison);
		}

		// Token: 0x06005918 RID: 22808 RVA: 0x0012E3E0 File Offset: 0x0012C5E0
		private static string GetRelativePath(string relativeTo, string path, StringComparison comparisonType)
		{
			if (string.IsNullOrEmpty(relativeTo))
			{
				throw new ArgumentNullException("relativeTo");
			}
			if (PathInternal.IsEffectivelyEmpty(path.AsSpan()))
			{
				throw new ArgumentNullException("path");
			}
			relativeTo = Path.GetFullPath(relativeTo);
			path = Path.GetFullPath(path);
			if (!PathInternal.AreRootsEqual(relativeTo, path, comparisonType))
			{
				return path;
			}
			int num = PathInternal.GetCommonPathLength(relativeTo, path, comparisonType == StringComparison.OrdinalIgnoreCase);
			if (num == 0)
			{
				return path;
			}
			int num2 = relativeTo.Length;
			if (PathInternal.EndsInDirectorySeparator(relativeTo.AsSpan()))
			{
				num2--;
			}
			bool flag = PathInternal.EndsInDirectorySeparator(path.AsSpan());
			int num3 = path.Length;
			if (flag)
			{
				num3--;
			}
			if (num2 == num3 && num >= num2)
			{
				return ".";
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(Math.Max(relativeTo.Length, path.Length));
			if (num < num2)
			{
				stringBuilder.Append("..");
				for (int i = num + 1; i < num2; i++)
				{
					if (PathInternal.IsDirectorySeparator(relativeTo[i]))
					{
						stringBuilder.Append(Path.DirectorySeparatorChar);
						stringBuilder.Append("..");
					}
				}
			}
			else if (PathInternal.IsDirectorySeparator(path[num]))
			{
				num++;
			}
			int num4 = num3 - num;
			if (flag)
			{
				num4++;
			}
			if (num4 > 0)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(Path.DirectorySeparatorChar);
				}
				stringBuilder.Append(path, num, num4);
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06005919 RID: 22809 RVA: 0x0012E53A File Offset: 0x0012C73A
		internal static StringComparison StringComparison
		{
			get
			{
				if (!Path.IsCaseSensitive)
				{
					return StringComparison.OrdinalIgnoreCase;
				}
				return StringComparison.Ordinal;
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x0600591A RID: 22810 RVA: 0x0012E546 File Offset: 0x0012C746
		internal static bool IsCaseSensitive
		{
			get
			{
				return !Path.IsWindows;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x0600591B RID: 22811 RVA: 0x0012E550 File Offset: 0x0012C750
		private static bool IsWindows
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform == PlatformID.Win32S || platform == PlatformID.Win32Windows || platform == PlatformID.Win32NT || platform == PlatformID.WinCE;
			}
		}

		// Token: 0x0600591C RID: 22812 RVA: 0x0012E57A File Offset: 0x0012C77A
		public static bool IsPathFullyQualified(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Path.IsPathFullyQualified(path.AsSpan());
		}

		// Token: 0x0600591D RID: 22813 RVA: 0x0012E595 File Offset: 0x0012C795
		public static bool IsPathFullyQualified(ReadOnlySpan<char> path)
		{
			return !PathInternal.IsPartiallyQualified(path);
		}

		// Token: 0x0600591E RID: 22814 RVA: 0x0012E5A0 File Offset: 0x0012C7A0
		public static string GetFullPath(string path, string basePath)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (basePath == null)
			{
				throw new ArgumentNullException("basePath");
			}
			if (!Path.IsPathFullyQualified(basePath))
			{
				throw new ArgumentException("Basepath argument is not fully qualified.", "basePath");
			}
			if (basePath.Contains('\0') || path.Contains('\0'))
			{
				throw new ArgumentException("Illegal characters in path '{0}'.");
			}
			if (Path.IsPathFullyQualified(path))
			{
				return Path.GetFullPath(path);
			}
			return Path.GetFullPath(Path.CombineInternal(basePath, path));
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x0012E619 File Offset: 0x0012C819
		private static string CombineInternal(string first, string second)
		{
			if (string.IsNullOrEmpty(first))
			{
				return second;
			}
			if (string.IsNullOrEmpty(second))
			{
				return first;
			}
			if (Path.IsPathRooted(second.AsSpan()))
			{
				return second;
			}
			return Path.JoinInternal(first.AsSpan(), second.AsSpan());
		}

		// Token: 0x0400355C RID: 13660
		[Obsolete("see GetInvalidPathChars and GetInvalidFileNameChars methods.")]
		public static readonly char[] InvalidPathChars = Path.GetInvalidPathChars();

		// Token: 0x0400355D RID: 13661
		public static readonly char AltDirectorySeparatorChar = MonoIO.AltDirectorySeparatorChar;

		// Token: 0x0400355E RID: 13662
		public static readonly char DirectorySeparatorChar = MonoIO.DirectorySeparatorChar;

		// Token: 0x0400355F RID: 13663
		public static readonly char PathSeparator = MonoIO.PathSeparator;

		// Token: 0x04003560 RID: 13664
		internal static readonly string DirectorySeparatorStr = Path.DirectorySeparatorChar.ToString();

		// Token: 0x04003561 RID: 13665
		public static readonly char VolumeSeparatorChar = MonoIO.VolumeSeparatorChar;

		// Token: 0x04003562 RID: 13666
		internal static readonly char[] PathSeparatorChars = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar,
			Path.VolumeSeparatorChar
		};

		// Token: 0x04003563 RID: 13667
		private static readonly bool dirEqualsVolume = Path.DirectorySeparatorChar == Path.VolumeSeparatorChar;

		// Token: 0x04003564 RID: 13668
		internal const int MAX_PATH = 260;

		// Token: 0x04003565 RID: 13669
		internal static readonly char[] trimEndCharsWindows = new char[] { '\t', '\n', '\v', '\f', '\r', ' ', '\u0085', '\u00a0' };

		// Token: 0x04003566 RID: 13670
		internal static readonly char[] trimEndCharsUnix = new char[0];

		// Token: 0x02000988 RID: 2440
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06005920 RID: 22816 RVA: 0x0012E64F File Offset: 0x0012C84F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06005921 RID: 22817 RVA: 0x000025BE File Offset: 0x000007BE
			public <>c()
			{
			}

			// Token: 0x06005922 RID: 22818 RVA: 0x0012E65C File Offset: 0x0012C85C
			internal unsafe void <JoinInternal>b__59_0(Span<char> destination, [TupleElementNames(new string[] { "First", "FirstLength", "Second", "SecondLength", "HasSeparator" })] ValueTuple<IntPtr, int, IntPtr, int, bool> state)
			{
				Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
				span.CopyTo(destination);
				if (!state.Item5)
				{
					*destination[state.Item2] = '/';
				}
				span = new Span<char>((void*)state.Item3, state.Item4);
				span.CopyTo(destination.Slice(state.Item2 + (state.Item5 ? 0 : 1)));
			}

			// Token: 0x06005923 RID: 22819 RVA: 0x0012E6D8 File Offset: 0x0012C8D8
			internal unsafe void <JoinInternal>b__60_0(Span<char> destination, [TupleElementNames(new string[] { "First", "FirstLength", "Second", "SecondLength", "Third", "ThirdLength", "FirstHasSeparator", "ThirdHasSeparator", null })] ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, bool, ValueTuple<bool>> state)
			{
				Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
				span.CopyTo(destination);
				if (!state.Item7)
				{
					*destination[state.Item2] = '/';
				}
				span = new Span<char>((void*)state.Item3, state.Item4);
				span.CopyTo(destination.Slice(state.Item2 + (state.Item7 ? 0 : 1)));
				if (!state.Rest.Item1)
				{
					*destination[destination.Length - state.Item6 - 1] = '/';
				}
				span = new Span<char>((void*)state.Item5, state.Item6);
				span.CopyTo(destination.Slice(destination.Length - state.Item6));
			}

			// Token: 0x06005924 RID: 22820 RVA: 0x0012E7B0 File Offset: 0x0012C9B0
			internal unsafe void <JoinInternal>b__61_0(Span<char> destination, [TupleElementNames(new string[]
			{
				"First", "FirstLength", "Second", "SecondLength", "Third", "ThirdLength", "Fourth", "FourthLength", "FirstHasSeparator", "ThirdHasSeparator",
				"FourthHasSeparator", null, null, null, null
			})] ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, IntPtr, ValueTuple<int, bool, bool, bool>> state)
			{
				Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
				span.CopyTo(destination);
				if (!state.Rest.Item2)
				{
					*destination[state.Item2] = '/';
				}
				span = new Span<char>((void*)state.Item3, state.Item4);
				span.CopyTo(destination.Slice(state.Item2 + (state.Rest.Item2 ? 0 : 1)));
				if (!state.Rest.Item3)
				{
					*destination[state.Item2 + state.Item4 + (state.Rest.Item2 ? 0 : 1)] = '/';
				}
				span = new Span<char>((void*)state.Item5, state.Item6);
				span.CopyTo(destination.Slice(state.Item2 + state.Item4 + (state.Rest.Item2 ? 0 : 1) + (state.Rest.Item3 ? 0 : 1)));
				if (!state.Rest.Item4)
				{
					*destination[destination.Length - state.Rest.Item1 - 1] = '/';
				}
				span = new Span<char>((void*)state.Item7, state.Rest.Item1);
				span.CopyTo(destination.Slice(destination.Length - state.Rest.Item1));
			}

			// Token: 0x04003567 RID: 13671
			public static readonly Path.<>c <>9 = new Path.<>c();

			// Token: 0x04003568 RID: 13672
			[TupleElementNames(new string[] { "First", "FirstLength", "Second", "SecondLength", "HasSeparator" })]
			public static SpanAction<char, ValueTuple<IntPtr, int, IntPtr, int, bool>> <>9__59_0;

			// Token: 0x04003569 RID: 13673
			[TupleElementNames(new string[] { "First", "FirstLength", "Second", "SecondLength", "Third", "ThirdLength", "FirstHasSeparator", "ThirdHasSeparator", null })]
			public static SpanAction<char, ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, bool, ValueTuple<bool>>> <>9__60_0;

			// Token: 0x0400356A RID: 13674
			[TupleElementNames(new string[]
			{
				"First", "FirstLength", "Second", "SecondLength", "Third", "ThirdLength", "Fourth", "FourthLength", "FirstHasSeparator", "ThirdHasSeparator",
				"FourthHasSeparator", null, null, null, null
			})]
			public static SpanAction<char, ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, IntPtr, ValueTuple<int, bool, bool, bool>>> <>9__61_0;
		}
	}
}
