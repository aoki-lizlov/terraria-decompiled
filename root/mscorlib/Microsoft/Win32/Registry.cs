using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32
{
	// Token: 0x02000089 RID: 137
	[ComVisible(true)]
	public static class Registry
	{
		// Token: 0x060003DF RID: 991 RVA: 0x000153DC File Offset: 0x000135DC
		private static RegistryKey ToKey(string keyName, bool setting)
		{
			if (keyName == null)
			{
				throw new ArgumentException("Not a valid registry key name", "keyName");
			}
			string[] array = keyName.Split('\\', StringSplitOptions.None);
			string text = array[0];
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			RegistryKey registryKey;
			if (num <= 1097425318U)
			{
				if (num != 126972219U)
				{
					if (num != 457190004U)
					{
						if (num == 1097425318U)
						{
							if (text == "HKEY_CLASSES_ROOT")
							{
								registryKey = Registry.ClassesRoot;
								goto IL_013E;
							}
						}
					}
					else if (text == "HKEY_LOCAL_MACHINE")
					{
						registryKey = Registry.LocalMachine;
						goto IL_013E;
					}
				}
				else if (text == "HKEY_CURRENT_CONFIG")
				{
					registryKey = Registry.CurrentConfig;
					goto IL_013E;
				}
			}
			else if (num <= 1568329430U)
			{
				if (num != 1198714601U)
				{
					if (num == 1568329430U)
					{
						if (text == "HKEY_CURRENT_USER")
						{
							registryKey = Registry.CurrentUser;
							goto IL_013E;
						}
					}
				}
				else if (text == "HKEY_USERS")
				{
					registryKey = Registry.Users;
					goto IL_013E;
				}
			}
			else if (num != 2823865611U)
			{
				if (num == 3554990456U)
				{
					if (text == "HKEY_PERFORMANCE_DATA")
					{
						registryKey = Registry.PerformanceData;
						goto IL_013E;
					}
				}
			}
			else if (text == "HKEY_DYN_DATA")
			{
				registryKey = Registry.DynData;
				goto IL_013E;
			}
			throw new ArgumentException("Keyname does not start with a valid registry root", "keyName");
			IL_013E:
			for (int i = 1; i < array.Length; i++)
			{
				RegistryKey registryKey2 = registryKey.OpenSubKey(array[i], setting);
				if (registryKey2 == null)
				{
					if (!setting)
					{
						return null;
					}
					registryKey2 = registryKey.CreateSubKey(array[i]);
				}
				registryKey = registryKey2;
			}
			return registryKey;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001555F File Offset: 0x0001375F
		public static void SetValue(string keyName, string valueName, object value)
		{
			RegistryKey registryKey = Registry.ToKey(keyName, true);
			if (valueName.Length > 255)
			{
				throw new ArgumentException("valueName is larger than 255 characters", "valueName");
			}
			if (registryKey == null)
			{
				throw new ArgumentException("cant locate that keyName", "keyName");
			}
			registryKey.SetValue(valueName, value);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000155A0 File Offset: 0x000137A0
		public static void SetValue(string keyName, string valueName, object value, RegistryValueKind valueKind)
		{
			RegistryKey registryKey = Registry.ToKey(keyName, true);
			if (valueName.Length > 255)
			{
				throw new ArgumentException("valueName is larger than 255 characters", "valueName");
			}
			if (registryKey == null)
			{
				throw new ArgumentException("cant locate that keyName", "keyName");
			}
			registryKey.SetValue(valueName, value, valueKind);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000155EC File Offset: 0x000137EC
		public static object GetValue(string keyName, string valueName, object defaultValue)
		{
			RegistryKey registryKey = Registry.ToKey(keyName, false);
			if (registryKey == null)
			{
				return defaultValue;
			}
			return registryKey.GetValue(valueName, defaultValue);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00015610 File Offset: 0x00013810
		// Note: this type is marked as 'beforefieldinit'.
		static Registry()
		{
		}

		// Token: 0x04000E64 RID: 3684
		public static readonly RegistryKey ClassesRoot = new RegistryKey(RegistryHive.ClassesRoot);

		// Token: 0x04000E65 RID: 3685
		public static readonly RegistryKey CurrentConfig = new RegistryKey(RegistryHive.CurrentConfig);

		// Token: 0x04000E66 RID: 3686
		public static readonly RegistryKey CurrentUser = new RegistryKey(RegistryHive.CurrentUser);

		// Token: 0x04000E67 RID: 3687
		[Obsolete("Use PerformanceData instead")]
		public static readonly RegistryKey DynData = new RegistryKey(RegistryHive.DynData);

		// Token: 0x04000E68 RID: 3688
		public static readonly RegistryKey LocalMachine = new RegistryKey(RegistryHive.LocalMachine);

		// Token: 0x04000E69 RID: 3689
		public static readonly RegistryKey PerformanceData = new RegistryKey(RegistryHive.PerformanceData);

		// Token: 0x04000E6A RID: 3690
		public static readonly RegistryKey Users = new RegistryKey(RegistryHive.Users);
	}
}
