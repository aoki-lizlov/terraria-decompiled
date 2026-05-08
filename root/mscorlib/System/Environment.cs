using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Mono;

namespace System
{
	// Token: 0x020001F2 RID: 498
	[ComVisible(true)]
	public static class Environment
	{
		// Token: 0x060017FE RID: 6142 RVA: 0x000025CE File Offset: 0x000007CE
		internal static string GetResourceString(string key)
		{
			return key;
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x000025CE File Offset: 0x000007CE
		internal static string GetResourceString(string key, CultureInfo culture)
		{
			return key;
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0005D49B File Offset: 0x0005B69B
		internal static string GetResourceString(string key, params object[] values)
		{
			return string.Format(CultureInfo.InvariantCulture, key, values);
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x000025CE File Offset: 0x000007CE
		internal static string GetRuntimeResourceString(string key)
		{
			return key;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0005D49B File Offset: 0x0005B69B
		internal static string GetRuntimeResourceString(string key, params object[] values)
		{
			return string.Format(CultureInfo.InvariantCulture, key, values);
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0005D4AC File Offset: 0x0005B6AC
		internal static string GetResourceStringEncodingName(int codePage)
		{
			if (codePage <= 12000)
			{
				if (codePage == 1200)
				{
					return Environment.GetResourceString("Unicode");
				}
				if (codePage == 1201)
				{
					return Environment.GetResourceString("Unicode (Big-Endian)");
				}
				if (codePage == 12000)
				{
					return Environment.GetResourceString("Unicode (UTF-32)");
				}
			}
			else if (codePage <= 20127)
			{
				if (codePage == 12001)
				{
					return Environment.GetResourceString("Unicode (UTF-32 Big-Endian)");
				}
				if (codePage == 20127)
				{
					return Environment.GetResourceString("US-ASCII");
				}
			}
			else
			{
				if (codePage == 65000)
				{
					return Environment.GetResourceString("Unicode (UTF-7)");
				}
				if (codePage == 65001)
				{
					return Environment.GetResourceString("Unicode (UTF-8)");
				}
			}
			return codePage.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x0000408A File Offset: 0x0000228A
		internal static bool IsWindows8OrAbove
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0005D560 File Offset: 0x0005B760
		public static string CommandLine
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in Environment.GetCommandLineArgs())
				{
					bool flag = false;
					string text2 = "";
					string text3 = text;
					for (int j = 0; j < text3.Length; j++)
					{
						if (text2.Length == 0 && char.IsWhiteSpace(text3[j]))
						{
							text2 = "\"";
						}
						else if (text3[j] == '"')
						{
							flag = true;
						}
					}
					if (flag && text2.Length != 0)
					{
						text3 = text3.Replace("\"", "\\\"");
					}
					stringBuilder.AppendFormat("{0}{1}{0} ", text2, text3);
				}
				if (stringBuilder.Length > 0)
				{
					StringBuilder stringBuilder2 = stringBuilder;
					int i = stringBuilder2.Length;
					stringBuilder2.Length = i - 1;
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x0005D630 File Offset: 0x0005B830
		// (set) Token: 0x06001807 RID: 6151 RVA: 0x0005D637 File Offset: 0x0005B837
		public static string CurrentDirectory
		{
			get
			{
				return Directory.InsecureGetCurrentDirectory();
			}
			set
			{
				Directory.InsecureSetCurrentDirectory(value);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x0005D63F File Offset: 0x0005B83F
		public static int CurrentManagedThreadId
		{
			get
			{
				return Thread.CurrentThread.ManagedThreadId;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001809 RID: 6153
		// (set) Token: 0x0600180A RID: 6154
		public static extern int ExitCode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600180B RID: 6155
		public static extern bool HasShutdownStarted
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600180C RID: 6156
		public static extern string MachineName
		{
			[EnvironmentPermission(SecurityAction.Demand, Read = "COMPUTERNAME")]
			[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600180D RID: 6157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetNewLine();

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x0005D64B File Offset: 0x0005B84B
		public static string NewLine
		{
			get
			{
				if (Environment.nl != null)
				{
					return Environment.nl;
				}
				Environment.nl = Environment.GetNewLine();
				return Environment.nl;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600180F RID: 6159
		internal static extern PlatformID Platform
		{
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06001810 RID: 6160
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetOSVersionString();

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0005D66C File Offset: 0x0005B86C
		public static OperatingSystem OSVersion
		{
			get
			{
				if (Environment.os == null)
				{
					Version version = Environment.CreateVersionFromString(Environment.GetOSVersionString());
					PlatformID platformID = Environment.Platform;
					if (platformID == PlatformID.MacOSX)
					{
						platformID = PlatformID.Unix;
					}
					Environment.os = new OperatingSystem(platformID, version);
				}
				return Environment.os;
			}
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0005D6A8 File Offset: 0x0005B8A8
		internal static Version CreateVersionFromString(string info)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 1;
			int num6 = -1;
			if (info == null)
			{
				return new Version(0, 0, 0, 0);
			}
			foreach (char c in info)
			{
				if (char.IsDigit(c))
				{
					if (num6 < 0)
					{
						num6 = (int)(c - '0');
					}
					else
					{
						num6 = num6 * 10 + (int)(c - '0');
					}
				}
				else if (num6 >= 0)
				{
					switch (num5)
					{
					case 1:
						num = num6;
						break;
					case 2:
						num2 = num6;
						break;
					case 3:
						num3 = num6;
						break;
					case 4:
						num4 = num6;
						break;
					}
					num6 = -1;
					num5++;
				}
				if (num5 == 5)
				{
					break;
				}
			}
			if (num6 >= 0)
			{
				switch (num5)
				{
				case 1:
					num = num6;
					break;
				case 2:
					num2 = num6;
					break;
				case 3:
					num3 = num6;
					break;
				case 4:
					num4 = num6;
					break;
				}
			}
			return new Version(num, num2, num3, num4);
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x0005D790 File Offset: 0x0005B990
		public static string StackTrace
		{
			[EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
			get
			{
				return new StackTrace(0, true).ToString();
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x0005D79E File Offset: 0x0005B99E
		public static string SystemDirectory
		{
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.System);
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06001815 RID: 6165
		public static extern int TickCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x0005D7A7 File Offset: 0x0005B9A7
		public static string UserDomainName
		{
			[EnvironmentPermission(SecurityAction.Demand, Read = "USERDOMAINNAME")]
			get
			{
				return Environment.MachineName;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x0000408A File Offset: 0x0000228A
		[MonoTODO("Currently always returns false, regardless of interactive state")]
		public static bool UserInteractive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001818 RID: 6168
		public static extern string UserName
		{
			[EnvironmentPermission(SecurityAction.Demand, Read = "USERNAME;USER")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x0005D7AE File Offset: 0x0005B9AE
		public static Version Version
		{
			get
			{
				return new Version("4.0.30319.42000");
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x0000408D File Offset: 0x0000228D
		[MonoTODO("Currently always returns zero")]
		public static long WorkingSet
		{
			[EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
			get
			{
				return 0L;
			}
		}

		// Token: 0x0600181B RID: 6171
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Exit(int exitCode);

		// Token: 0x0600181C RID: 6172 RVA: 0x0005D7BA File Offset: 0x0005B9BA
		internal static void _Exit(int exitCode)
		{
			Environment.Exit(exitCode);
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0005D7C4 File Offset: 0x0005B9C4
		public static string ExpandEnvironmentVariables(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			int num = name.IndexOf('%');
			if (num == -1)
			{
				return name;
			}
			int length = name.Length;
			int num2;
			if (num == length - 1 || (num2 = name.IndexOf('%', num + 1)) == -1)
			{
				return name;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(name, 0, num);
			Hashtable hashtable = null;
			do
			{
				string text = name.Substring(num + 1, num2 - num - 1);
				string text2 = Environment.GetEnvironmentVariable(text);
				if (text2 == null && Environment.IsRunningOnWindows)
				{
					if (hashtable == null)
					{
						hashtable = Environment.GetEnvironmentVariablesNoCase();
					}
					text2 = hashtable[text] as string;
				}
				int num3 = num2;
				if (text2 == null)
				{
					stringBuilder.Append('%');
					stringBuilder.Append(text);
					num2--;
				}
				else
				{
					stringBuilder.Append(text2);
				}
				int num4 = num2;
				num = name.IndexOf('%', num2 + 1);
				num2 = ((num == -1 || num2 > length - 1) ? (-1) : name.IndexOf('%', num + 1));
				int num5;
				if (num == -1 || num2 == -1)
				{
					num5 = length - num4 - 1;
				}
				else if (text2 != null)
				{
					num5 = num - num4 - 1;
				}
				else
				{
					num5 = num - num3;
				}
				if (num >= num4 || num == -1)
				{
					stringBuilder.Append(name, num4 + 1, num5);
				}
			}
			while (num2 > -1 && num2 < length);
			return stringBuilder.ToString();
		}

		// Token: 0x0600181E RID: 6174
		[EnvironmentPermission(SecurityAction.Demand, Read = "PATH")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetCommandLineArgs();

		// Token: 0x0600181F RID: 6175
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string internalGetEnvironmentVariable_native(IntPtr variable);

		// Token: 0x06001820 RID: 6176 RVA: 0x0005D900 File Offset: 0x0005BB00
		internal static string internalGetEnvironmentVariable(string variable)
		{
			if (variable == null)
			{
				return null;
			}
			string text;
			using (SafeStringMarshal safeStringMarshal = RuntimeMarshal.MarshalString(variable))
			{
				text = Environment.internalGetEnvironmentVariable_native(safeStringMarshal.Value);
			}
			return text;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0005D948 File Offset: 0x0005BB48
		public static string GetEnvironmentVariable(string variable)
		{
			return Environment.internalGetEnvironmentVariable(variable);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0005D950 File Offset: 0x0005BB50
		private static Hashtable GetEnvironmentVariablesNoCase()
		{
			Hashtable hashtable = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			foreach (string text in Environment.GetEnvironmentVariableNames())
			{
				hashtable[text] = Environment.internalGetEnvironmentVariable(text);
			}
			return hashtable;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0005D994 File Offset: 0x0005BB94
		public static IDictionary GetEnvironmentVariables()
		{
			StringBuilder stringBuilder = null;
			if (SecurityManager.SecurityEnabled)
			{
				stringBuilder = new StringBuilder();
			}
			Hashtable hashtable = new Hashtable();
			foreach (string text in Environment.GetEnvironmentVariableNames())
			{
				hashtable[text] = Environment.internalGetEnvironmentVariable(text);
				if (stringBuilder != null)
				{
					stringBuilder.Append(text);
					stringBuilder.Append(";");
				}
			}
			if (stringBuilder != null)
			{
				new EnvironmentPermission(EnvironmentPermissionAccess.Read, stringBuilder.ToString()).Demand();
			}
			return hashtable;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0005DA0B File Offset: 0x0005BC0B
		public static string GetFolderPath(Environment.SpecialFolder folder)
		{
			return Environment.GetFolderPath(folder, Environment.SpecialFolderOption.None);
		}

		// Token: 0x06001825 RID: 6181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetWindowsFolderPath(int folder);

		// Token: 0x06001826 RID: 6182 RVA: 0x0005DA14 File Offset: 0x0005BC14
		public static string GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
		{
			string text;
			if (Environment.IsRunningOnWindows)
			{
				text = Environment.GetWindowsFolderPath((int)folder);
			}
			else
			{
				text = Environment.UnixGetFolderPath(folder, option);
			}
			return text;
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0005DA3C File Offset: 0x0005BC3C
		private static string ReadXdgUserDir(string config_dir, string home_dir, string key, string fallback)
		{
			string text = Environment.internalGetEnvironmentVariable(key);
			if (text != null && text != string.Empty)
			{
				return text;
			}
			string text2 = Path.Combine(config_dir, "user-dirs.dirs");
			if (!File.Exists(text2))
			{
				return Path.Combine(home_dir, fallback);
			}
			try
			{
				using (StreamReader streamReader = new StreamReader(text2))
				{
					string text3;
					while ((text3 = streamReader.ReadLine()) != null)
					{
						text3 = text3.Trim();
						int num = text3.IndexOf('=');
						if (num > 8 && text3.Substring(0, num) == key)
						{
							string text4 = text3.Substring(num + 1).Trim('"');
							bool flag = false;
							if (text4.StartsWithOrdinalUnchecked("$HOME/"))
							{
								flag = true;
								text4 = text4.Substring(6);
							}
							else if (!text4.StartsWithOrdinalUnchecked("/"))
							{
								flag = true;
							}
							return flag ? Path.Combine(home_dir, text4) : text4;
						}
					}
				}
			}
			catch
			{
			}
			return Path.Combine(home_dir, fallback);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0005DB4C File Offset: 0x0005BD4C
		internal static string UnixGetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
		{
			string text = Environment.internalGetHome();
			string text2 = Environment.internalGetEnvironmentVariable("XDG_DATA_HOME");
			if (text2 == null || text2 == string.Empty)
			{
				text2 = Path.Combine(text, ".local");
				text2 = Path.Combine(text2, "share");
			}
			string text3 = Environment.internalGetEnvironmentVariable("XDG_CONFIG_HOME");
			if (text3 == null || text3 == string.Empty)
			{
				text3 = Path.Combine(text, ".config");
			}
			switch (folder)
			{
			case Environment.SpecialFolder.Desktop:
			case Environment.SpecialFolder.DesktopDirectory:
				return Environment.ReadXdgUserDir(text3, text, "XDG_DESKTOP_DIR", "Desktop");
			case Environment.SpecialFolder.Programs:
			case Environment.SpecialFolder.Startup:
			case Environment.SpecialFolder.Recent:
			case Environment.SpecialFolder.SendTo:
			case Environment.SpecialFolder.StartMenu:
			case Environment.SpecialFolder.NetworkShortcuts:
			case Environment.SpecialFolder.CommonStartMenu:
			case Environment.SpecialFolder.CommonPrograms:
			case Environment.SpecialFolder.CommonStartup:
			case Environment.SpecialFolder.CommonDesktopDirectory:
			case Environment.SpecialFolder.PrinterShortcuts:
			case Environment.SpecialFolder.Cookies:
			case Environment.SpecialFolder.History:
			case Environment.SpecialFolder.Windows:
			case Environment.SpecialFolder.System:
			case Environment.SpecialFolder.SystemX86:
			case Environment.SpecialFolder.ProgramFilesX86:
			case Environment.SpecialFolder.CommonProgramFiles:
			case Environment.SpecialFolder.CommonProgramFilesX86:
			case Environment.SpecialFolder.CommonDocuments:
			case Environment.SpecialFolder.CommonAdminTools:
			case Environment.SpecialFolder.AdminTools:
			case Environment.SpecialFolder.CommonMusic:
			case Environment.SpecialFolder.CommonPictures:
			case Environment.SpecialFolder.CommonVideos:
			case Environment.SpecialFolder.Resources:
			case Environment.SpecialFolder.LocalizedResources:
			case Environment.SpecialFolder.CommonOemLinks:
			case Environment.SpecialFolder.CDBurning:
				return string.Empty;
			case Environment.SpecialFolder.MyDocuments:
				return text;
			case Environment.SpecialFolder.Favorites:
				if (Environment.Platform == PlatformID.MacOSX)
				{
					return Path.Combine(text, "Library", "Favorites");
				}
				return string.Empty;
			case Environment.SpecialFolder.MyMusic:
				if (Environment.Platform == PlatformID.MacOSX)
				{
					return Path.Combine(text, "Music");
				}
				return Environment.ReadXdgUserDir(text3, text, "XDG_MUSIC_DIR", "Music");
			case Environment.SpecialFolder.MyVideos:
				return Environment.ReadXdgUserDir(text3, text, "XDG_VIDEOS_DIR", "Videos");
			case Environment.SpecialFolder.MyComputer:
				return string.Empty;
			case Environment.SpecialFolder.Fonts:
				if (Environment.Platform == PlatformID.MacOSX)
				{
					return Path.Combine(text, "Library", "Fonts");
				}
				return Path.Combine(text, ".fonts");
			case Environment.SpecialFolder.Templates:
				return Environment.ReadXdgUserDir(text3, text, "XDG_TEMPLATES_DIR", "Templates");
			case Environment.SpecialFolder.ApplicationData:
				return text3;
			case Environment.SpecialFolder.LocalApplicationData:
				return text2;
			case Environment.SpecialFolder.InternetCache:
				if (Environment.Platform == PlatformID.MacOSX)
				{
					return Path.Combine(text, "Library", "Caches");
				}
				return string.Empty;
			case Environment.SpecialFolder.CommonApplicationData:
				return "/usr/share";
			case Environment.SpecialFolder.ProgramFiles:
				if (Environment.Platform == PlatformID.MacOSX)
				{
					return "/Applications";
				}
				return string.Empty;
			case Environment.SpecialFolder.MyPictures:
				if (Environment.Platform == PlatformID.MacOSX)
				{
					return Path.Combine(text, "Pictures");
				}
				return Environment.ReadXdgUserDir(text3, text, "XDG_PICTURES_DIR", "Pictures");
			case Environment.SpecialFolder.UserProfile:
				return text;
			case Environment.SpecialFolder.CommonTemplates:
				return "/usr/share/templates";
			}
			throw new ArgumentException("Invalid SpecialFolder");
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0005DDD7 File Offset: 0x0005BFD7
		[EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
		public static string[] GetLogicalDrives()
		{
			return Environment.GetLogicalDrivesInternal();
		}

		// Token: 0x0600182A RID: 6186
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void internalBroadcastSettingChange();

		// Token: 0x0600182B RID: 6187 RVA: 0x0005DDE0 File Offset: 0x0005BFE0
		public static string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
		{
			switch (target)
			{
			case EnvironmentVariableTarget.Process:
				return Environment.GetEnvironmentVariable(variable);
			case EnvironmentVariableTarget.User:
				break;
			case EnvironmentVariableTarget.Machine:
			{
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				if (!Environment.IsRunningOnWindows)
				{
					return null;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment"))
				{
					object value = registryKey.GetValue(variable);
					return (value == null) ? null : value.ToString();
				}
				break;
			}
			default:
				goto IL_00AC;
			}
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			if (!Environment.IsRunningOnWindows)
			{
				return null;
			}
			using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Environment", false))
			{
				object value2 = registryKey2.GetValue(variable);
				return (value2 == null) ? null : value2.ToString();
			}
			IL_00AC:
			throw new ArgumentException("target");
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0005DEC4 File Offset: 0x0005C0C4
		public static IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target)
		{
			IDictionary dictionary = new Hashtable();
			switch (target)
			{
			case EnvironmentVariableTarget.Process:
				return Environment.GetEnvironmentVariables();
			case EnvironmentVariableTarget.User:
				break;
			case EnvironmentVariableTarget.Machine:
			{
				new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				if (!Environment.IsRunningOnWindows)
				{
					return dictionary;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment"))
				{
					foreach (string text in registryKey.GetValueNames())
					{
						dictionary.Add(text, registryKey.GetValue(text));
					}
					return dictionary;
				}
				break;
			}
			default:
				goto IL_00E0;
			}
			new EnvironmentPermission(PermissionState.Unrestricted).Demand();
			if (!Environment.IsRunningOnWindows)
			{
				return dictionary;
			}
			using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Environment"))
			{
				foreach (string text2 in registryKey2.GetValueNames())
				{
					dictionary.Add(text2, registryKey2.GetValue(text2));
				}
				return dictionary;
			}
			IL_00E0:
			throw new ArgumentException("target");
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0005DFDC File Offset: 0x0005C1DC
		[EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void SetEnvironmentVariable(string variable, string value)
		{
			Environment.SetEnvironmentVariable(variable, value, EnvironmentVariableTarget.Process);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0005DFE8 File Offset: 0x0005C1E8
		[EnvironmentPermission(SecurityAction.Demand, Unrestricted = true)]
		public static void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (variable == string.Empty)
			{
				throw new ArgumentException("String cannot be of zero length.", "variable");
			}
			if (variable.IndexOf('=') != -1)
			{
				throw new ArgumentException("Environment variable name cannot contain an equal character.", "variable");
			}
			if (variable[0] == '\0')
			{
				throw new ArgumentException("The first char in the string is the null character.", "variable");
			}
			switch (target)
			{
			case EnvironmentVariableTarget.Process:
				Environment.InternalSetEnvironmentVariable(variable, value);
				return;
			case EnvironmentVariableTarget.User:
				break;
			case EnvironmentVariableTarget.Machine:
			{
				if (!Environment.IsRunningOnWindows)
				{
					return;
				}
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment", true))
				{
					if (string.IsNullOrEmpty(value))
					{
						registryKey.DeleteValue(variable, false);
					}
					else
					{
						registryKey.SetValue(variable, value);
					}
					Environment.internalBroadcastSettingChange();
					return;
				}
				break;
			}
			default:
				goto IL_0106;
			}
			if (!Environment.IsRunningOnWindows)
			{
				return;
			}
			using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Environment", true))
			{
				if (string.IsNullOrEmpty(value))
				{
					registryKey2.DeleteValue(variable, false);
				}
				else
				{
					registryKey2.SetValue(variable, value);
				}
				Environment.internalBroadcastSettingChange();
				return;
			}
			IL_0106:
			throw new ArgumentException("target");
		}

		// Token: 0x0600182F RID: 6191
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void InternalSetEnvironmentVariable(char* variable, int variable_length, char* value, int value_length);

		// Token: 0x06001830 RID: 6192 RVA: 0x0005E124 File Offset: 0x0005C324
		internal unsafe static void InternalSetEnvironmentVariable(string variable, string value)
		{
			fixed (string text = variable)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text2 = value)
				{
					char* ptr2 = text2;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					Environment.InternalSetEnvironmentVariable(ptr, (variable != null) ? variable.Length : 0, ptr2, (value != null) ? value.Length : 0);
				}
			}
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0005E174 File Offset: 0x0005C374
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public static void FailFast(string message)
		{
			Environment.FailFast(message, null, null);
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0005E174 File Offset: 0x0005C374
		internal static void FailFast(string message, uint exitCode)
		{
			Environment.FailFast(message, null, null);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0005E17E File Offset: 0x0005C37E
		[SecurityCritical]
		public static void FailFast(string message, Exception exception)
		{
			Environment.FailFast(message, exception, null);
		}

		// Token: 0x06001834 RID: 6196
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FailFast(string message, Exception exception, string errorSource);

		// Token: 0x06001835 RID: 6197
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIs64BitOperatingSystem();

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x0005E188 File Offset: 0x0005C388
		public static bool Is64BitOperatingSystem
		{
			get
			{
				return Environment.GetIs64BitOperatingSystem();
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x0005E18F File Offset: 0x0005C38F
		public static int SystemPageSize
		{
			get
			{
				return Environment.GetPageSize();
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x0005E196 File Offset: 0x0005C396
		public static bool Is64BitProcess
		{
			get
			{
				return IntPtr.Size == 8;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001839 RID: 6201
		public static extern int ProcessorCount
		{
			[EnvironmentPermission(SecurityAction.Demand, Read = "NUMBER_OF_PROCESSORS")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x0005E1A0 File Offset: 0x0005C3A0
		internal static bool IsRunningOnWindows
		{
			get
			{
				return Environment.Platform < PlatformID.Unix;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x0005E1AC File Offset: 0x0005C3AC
		private static string GacPath
		{
			get
			{
				if (Environment.IsRunningOnWindows)
				{
					return Path.Combine(Path.Combine(new DirectoryInfo(Path.GetDirectoryName(typeof(int).Assembly.Location)).Parent.Parent.FullName, "mono"), "gac");
				}
				return Path.Combine(Path.Combine(Environment.internalGetGacPath(), "mono"), "gac");
			}
		}

		// Token: 0x0600183C RID: 6204
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string internalGetGacPath();

		// Token: 0x0600183D RID: 6205
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string[] GetLogicalDrivesInternal();

		// Token: 0x0600183E RID: 6206
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetEnvironmentVariableNames();

		// Token: 0x0600183F RID: 6207
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetMachineConfigPath();

		// Token: 0x06001840 RID: 6208
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string internalGetHome();

		// Token: 0x06001841 RID: 6209
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetPageSize();

		// Token: 0x06001842 RID: 6210
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_bundled_machine_config();

		// Token: 0x06001843 RID: 6211 RVA: 0x0005E21B File Offset: 0x0005C41B
		internal static string GetBundledMachineConfig()
		{
			return Environment.get_bundled_machine_config();
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x0005E224 File Offset: 0x0005C424
		internal static bool IsUnix
		{
			get
			{
				int platform = (int)Environment.Platform;
				return platform == 4 || platform == 128 || platform == 6;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x0005E249 File Offset: 0x0005C449
		internal static bool IsMacOS
		{
			get
			{
				return Environment.Platform == PlatformID.MacOSX;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x0000408A File Offset: 0x0000228A
		internal static bool IsCLRHosted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00004088 File Offset: 0x00002288
		internal static void TriggerCodeContractFailure(ContractFailureKind failureKind, string message, string condition, string exceptionAsString)
		{
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0005E254 File Offset: 0x0005C454
		internal static string GetStackTrace(Exception e, bool needFileInfo)
		{
			StackTrace stackTrace;
			if (e == null)
			{
				stackTrace = new StackTrace(needFileInfo);
			}
			else
			{
				stackTrace = new StackTrace(e, needFileInfo);
			}
			return stackTrace.ToString(global::System.Diagnostics.StackTrace.TraceFormat.Normal);
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x00003FB7 File Offset: 0x000021B7
		internal static bool IsWinRTSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001560 RID: 5472
		private const string mono_corlib_version = "1A5E0066-58DC-428A-B21C-0AD6CDAE2789";

		// Token: 0x04001561 RID: 5473
		private static string nl;

		// Token: 0x04001562 RID: 5474
		private static OperatingSystem os;

		// Token: 0x04001563 RID: 5475
		[CompilerGenerated]
		private static readonly PlatformID <Platform>k__BackingField;

		// Token: 0x020001F3 RID: 499
		[ComVisible(true)]
		public enum SpecialFolder
		{
			// Token: 0x04001565 RID: 5477
			MyDocuments = 5,
			// Token: 0x04001566 RID: 5478
			Desktop = 0,
			// Token: 0x04001567 RID: 5479
			MyComputer = 17,
			// Token: 0x04001568 RID: 5480
			Programs = 2,
			// Token: 0x04001569 RID: 5481
			Personal = 5,
			// Token: 0x0400156A RID: 5482
			Favorites,
			// Token: 0x0400156B RID: 5483
			Startup,
			// Token: 0x0400156C RID: 5484
			Recent,
			// Token: 0x0400156D RID: 5485
			SendTo,
			// Token: 0x0400156E RID: 5486
			StartMenu = 11,
			// Token: 0x0400156F RID: 5487
			MyMusic = 13,
			// Token: 0x04001570 RID: 5488
			DesktopDirectory = 16,
			// Token: 0x04001571 RID: 5489
			Templates = 21,
			// Token: 0x04001572 RID: 5490
			ApplicationData = 26,
			// Token: 0x04001573 RID: 5491
			LocalApplicationData = 28,
			// Token: 0x04001574 RID: 5492
			InternetCache = 32,
			// Token: 0x04001575 RID: 5493
			Cookies,
			// Token: 0x04001576 RID: 5494
			History,
			// Token: 0x04001577 RID: 5495
			CommonApplicationData,
			// Token: 0x04001578 RID: 5496
			System = 37,
			// Token: 0x04001579 RID: 5497
			ProgramFiles,
			// Token: 0x0400157A RID: 5498
			MyPictures,
			// Token: 0x0400157B RID: 5499
			CommonProgramFiles = 43,
			// Token: 0x0400157C RID: 5500
			MyVideos = 14,
			// Token: 0x0400157D RID: 5501
			NetworkShortcuts = 19,
			// Token: 0x0400157E RID: 5502
			Fonts,
			// Token: 0x0400157F RID: 5503
			CommonStartMenu = 22,
			// Token: 0x04001580 RID: 5504
			CommonPrograms,
			// Token: 0x04001581 RID: 5505
			CommonStartup,
			// Token: 0x04001582 RID: 5506
			CommonDesktopDirectory,
			// Token: 0x04001583 RID: 5507
			PrinterShortcuts = 27,
			// Token: 0x04001584 RID: 5508
			Windows = 36,
			// Token: 0x04001585 RID: 5509
			UserProfile = 40,
			// Token: 0x04001586 RID: 5510
			SystemX86,
			// Token: 0x04001587 RID: 5511
			ProgramFilesX86,
			// Token: 0x04001588 RID: 5512
			CommonProgramFilesX86 = 44,
			// Token: 0x04001589 RID: 5513
			CommonTemplates,
			// Token: 0x0400158A RID: 5514
			CommonDocuments,
			// Token: 0x0400158B RID: 5515
			CommonAdminTools,
			// Token: 0x0400158C RID: 5516
			AdminTools,
			// Token: 0x0400158D RID: 5517
			CommonMusic = 53,
			// Token: 0x0400158E RID: 5518
			CommonPictures,
			// Token: 0x0400158F RID: 5519
			CommonVideos,
			// Token: 0x04001590 RID: 5520
			Resources,
			// Token: 0x04001591 RID: 5521
			LocalizedResources,
			// Token: 0x04001592 RID: 5522
			CommonOemLinks,
			// Token: 0x04001593 RID: 5523
			CDBurning
		}

		// Token: 0x020001F4 RID: 500
		public enum SpecialFolderOption
		{
			// Token: 0x04001595 RID: 5525
			None,
			// Token: 0x04001596 RID: 5526
			DoNotVerify = 16384,
			// Token: 0x04001597 RID: 5527
			Create = 32768
		}
	}
}
