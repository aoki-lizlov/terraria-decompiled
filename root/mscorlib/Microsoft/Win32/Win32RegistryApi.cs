using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x0200008F RID: 143
	internal class Win32RegistryApi : IRegistryApi
	{
		// Token: 0x06000464 RID: 1124
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegCreateKeyEx(IntPtr keyBase, string keyName, int reserved, IntPtr lpClass, int options, int access, IntPtr securityAttrs, out IntPtr keyHandle, out int disposition);

		// Token: 0x06000465 RID: 1125
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegCloseKey(IntPtr keyHandle);

		// Token: 0x06000466 RID: 1126
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegConnectRegistry(string machineName, IntPtr hKey, out IntPtr keyHandle);

		// Token: 0x06000467 RID: 1127
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegFlushKey(IntPtr keyHandle);

		// Token: 0x06000468 RID: 1128
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegOpenKeyEx(IntPtr keyBase, string keyName, IntPtr reserved, int access, out IntPtr keyHandle);

		// Token: 0x06000469 RID: 1129
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegDeleteKey(IntPtr keyHandle, string valueName);

		// Token: 0x0600046A RID: 1130
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegDeleteValue(IntPtr keyHandle, string valueName);

		// Token: 0x0600046B RID: 1131
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegEnumKeyExW")]
		internal unsafe static extern int RegEnumKeyEx(IntPtr keyHandle, int dwIndex, char* lpName, ref int lpcbName, int[] lpReserved, [Out] StringBuilder lpClass, int[] lpcbClass, long[] lpftLastWriteTime);

		// Token: 0x0600046C RID: 1132
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern int RegEnumValue(IntPtr hKey, int dwIndex, char* lpValueName, ref int lpcbValueName, IntPtr lpReserved_MustBeZero, int[] lpType, byte[] lpData, int[] lpcbData);

		// Token: 0x0600046D RID: 1133
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegSetValueEx(IntPtr keyBase, string valueName, IntPtr reserved, RegistryValueKind type, string data, int rawDataLength);

		// Token: 0x0600046E RID: 1134
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegSetValueEx(IntPtr keyBase, string valueName, IntPtr reserved, RegistryValueKind type, byte[] rawData, int rawDataLength);

		// Token: 0x0600046F RID: 1135
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegSetValueEx(IntPtr keyBase, string valueName, IntPtr reserved, RegistryValueKind type, ref int data, int rawDataLength);

		// Token: 0x06000470 RID: 1136
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegSetValueEx(IntPtr keyBase, string valueName, IntPtr reserved, RegistryValueKind type, ref long data, int rawDataLength);

		// Token: 0x06000471 RID: 1137
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, IntPtr zero, ref int dataSize);

		// Token: 0x06000472 RID: 1138
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, [Out] byte[] data, ref int dataSize);

		// Token: 0x06000473 RID: 1139
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, ref int data, ref int dataSize);

		// Token: 0x06000474 RID: 1140
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		private static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, ref long data, ref int dataSize);

		// Token: 0x06000475 RID: 1141
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegQueryInfoKeyW")]
		internal static extern int RegQueryInfoKey(IntPtr hKey, [Out] StringBuilder lpClass, int[] lpcbClass, IntPtr lpReserved_MustBeZero, ref int lpcSubKeys, int[] lpcbMaxSubKeyLen, int[] lpcbMaxClassLen, ref int lpcValues, int[] lpcbMaxValueNameLen, int[] lpcbMaxValueLen, int[] lpcbSecurityDescriptor, int[] lpftLastWriteTime);

		// Token: 0x06000476 RID: 1142 RVA: 0x00017725 File Offset: 0x00015925
		public IntPtr GetHandle(RegistryKey key)
		{
			return (IntPtr)key.InternalHandle;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00017732 File Offset: 0x00015932
		private static bool IsHandleValid(RegistryKey key)
		{
			return key.InternalHandle != null;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00017740 File Offset: 0x00015940
		public RegistryValueKind GetValueKind(RegistryKey rkey, string name)
		{
			RegistryValueKind registryValueKind = RegistryValueKind.Unknown;
			int num = 0;
			int num2 = Win32RegistryApi.RegQueryValueEx(this.GetHandle(rkey), name, IntPtr.Zero, ref registryValueKind, IntPtr.Zero, ref num);
			if (num2 == 2 || num2 == 1018)
			{
				return RegistryValueKind.Unknown;
			}
			return registryValueKind;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001777C File Offset: 0x0001597C
		public object GetValue(RegistryKey rkey, string name, object defaultValue, RegistryValueOptions options)
		{
			RegistryValueKind registryValueKind = RegistryValueKind.Unknown;
			int num = 0;
			IntPtr handle = this.GetHandle(rkey);
			int num2 = Win32RegistryApi.RegQueryValueEx(handle, name, IntPtr.Zero, ref registryValueKind, IntPtr.Zero, ref num);
			if (num2 == 2 || num2 == 1018)
			{
				return defaultValue;
			}
			if (num2 != 234 && num2 != 0)
			{
				this.GenerateException(num2);
			}
			object obj;
			if (registryValueKind == RegistryValueKind.String)
			{
				byte[] array;
				num2 = this.GetBinaryValue(rkey, name, registryValueKind, out array, num);
				obj = RegistryKey.DecodeString(array);
			}
			else if (registryValueKind == RegistryValueKind.ExpandString)
			{
				byte[] array2;
				num2 = this.GetBinaryValue(rkey, name, registryValueKind, out array2, num);
				obj = RegistryKey.DecodeString(array2);
				if ((options & RegistryValueOptions.DoNotExpandEnvironmentNames) == RegistryValueOptions.None)
				{
					obj = Environment.ExpandEnvironmentVariables((string)obj);
				}
			}
			else if (registryValueKind == RegistryValueKind.DWord)
			{
				int num3 = 0;
				num2 = Win32RegistryApi.RegQueryValueEx(handle, name, IntPtr.Zero, ref registryValueKind, ref num3, ref num);
				obj = num3;
			}
			else if (registryValueKind == RegistryValueKind.QWord)
			{
				long num4 = 0L;
				num2 = Win32RegistryApi.RegQueryValueEx(handle, name, IntPtr.Zero, ref registryValueKind, ref num4, ref num);
				obj = num4;
			}
			else if (registryValueKind == RegistryValueKind.Binary)
			{
				byte[] array3;
				num2 = this.GetBinaryValue(rkey, name, registryValueKind, out array3, num);
				obj = array3;
			}
			else
			{
				if (registryValueKind != RegistryValueKind.MultiString)
				{
					throw new SystemException();
				}
				obj = null;
				byte[] array4;
				num2 = this.GetBinaryValue(rkey, name, registryValueKind, out array4, num);
				if (num2 == 0)
				{
					obj = RegistryKey.DecodeString(array4).Split('\0', StringSplitOptions.None);
				}
			}
			if (num2 != 0)
			{
				this.GenerateException(num2);
			}
			return obj;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000178C8 File Offset: 0x00015AC8
		public void SetValue(RegistryKey rkey, string name, object value, RegistryValueKind valueKind)
		{
			Type type = value.GetType();
			IntPtr handle = this.GetHandle(rkey);
			switch (valueKind)
			{
			case RegistryValueKind.String:
			case RegistryValueKind.ExpandString:
				if (type == typeof(string))
				{
					string text = string.Format("{0}{1}", value, '\0');
					this.CheckResult(Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, valueKind, text, text.Length * this.NativeBytesPerCharacter));
					return;
				}
				goto IL_01B7;
			case RegistryValueKind.Binary:
				goto IL_009C;
			case RegistryValueKind.DWord:
				break;
			case (RegistryValueKind)5:
			case (RegistryValueKind)6:
			case (RegistryValueKind)8:
			case (RegistryValueKind)9:
			case (RegistryValueKind)10:
				goto IL_01A4;
			case RegistryValueKind.MultiString:
				if (type == typeof(string[]))
				{
					string[] array = (string[])value;
					StringBuilder stringBuilder = new StringBuilder();
					foreach (string text2 in array)
					{
						stringBuilder.Append(text2);
						stringBuilder.Append('\0');
					}
					stringBuilder.Append('\0');
					byte[] bytes = Encoding.Unicode.GetBytes(stringBuilder.ToString());
					this.CheckResult(Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.MultiString, bytes, bytes.Length));
					return;
				}
				goto IL_01B7;
			case RegistryValueKind.QWord:
				try
				{
					long num = Convert.ToInt64(value);
					this.CheckResult(Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.QWord, ref num, 8));
					return;
				}
				catch (OverflowException)
				{
					goto IL_01B7;
				}
				break;
			default:
				goto IL_01A4;
			}
			try
			{
				int num2 = Convert.ToInt32(value);
				this.CheckResult(Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.DWord, ref num2, 4));
				return;
			}
			catch (OverflowException)
			{
				goto IL_01B7;
			}
			IL_009C:
			if (type == typeof(byte[]))
			{
				byte[] array3 = (byte[])value;
				this.CheckResult(Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.Binary, array3, array3.Length));
				return;
			}
			goto IL_01B7;
			IL_01A4:
			if (type.IsArray)
			{
				throw new ArgumentException("Only string and byte arrays can written as registry values");
			}
			IL_01B7:
			throw new ArgumentException("Type does not match the valueKind");
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00017AB4 File Offset: 0x00015CB4
		public void SetValue(RegistryKey rkey, string name, object value)
		{
			Type type = value.GetType();
			IntPtr handle = this.GetHandle(rkey);
			int num2;
			if (type == typeof(int))
			{
				int num = (int)value;
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.DWord, ref num, 4);
			}
			else if (type == typeof(byte[]))
			{
				byte[] array = (byte[])value;
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.Binary, array, array.Length);
			}
			else if (type == typeof(string[]))
			{
				string[] array2 = (string[])value;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in array2)
				{
					stringBuilder.Append(text);
					stringBuilder.Append('\0');
				}
				stringBuilder.Append('\0');
				byte[] bytes = Encoding.Unicode.GetBytes(stringBuilder.ToString());
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.MultiString, bytes, bytes.Length);
			}
			else
			{
				if (type.IsArray)
				{
					throw new ArgumentException("Only string and byte arrays can written as registry values");
				}
				string text2 = string.Format("{0}{1}", value, '\0');
				num2 = Win32RegistryApi.RegSetValueEx(handle, name, IntPtr.Zero, RegistryValueKind.String, text2, text2.Length * this.NativeBytesPerCharacter);
			}
			if (num2 != 0)
			{
				this.GenerateException(num2);
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00017C00 File Offset: 0x00015E00
		private int GetBinaryValue(RegistryKey rkey, string name, RegistryValueKind type, out byte[] data, int size)
		{
			byte[] array = new byte[size];
			int num = Win32RegistryApi.RegQueryValueEx(this.GetHandle(rkey), name, IntPtr.Zero, ref type, array, ref size);
			data = array;
			return num;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00017C30 File Offset: 0x00015E30
		public int SubKeyCount(RegistryKey rkey)
		{
			int num = 0;
			int num2 = 0;
			int num3 = Win32RegistryApi.RegQueryInfoKey(this.GetHandle(rkey), null, null, IntPtr.Zero, ref num, null, null, ref num2, null, null, null, null);
			if (num3 != 0)
			{
				this.GenerateException(num3);
			}
			return num;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00017C6C File Offset: 0x00015E6C
		public int ValueCount(RegistryKey rkey)
		{
			int num = 0;
			int num2 = 0;
			int num3 = Win32RegistryApi.RegQueryInfoKey(this.GetHandle(rkey), null, null, IntPtr.Zero, ref num2, null, null, ref num, null, null, null, null);
			if (num3 != 0)
			{
				this.GenerateException(num3);
			}
			return num;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00017CA8 File Offset: 0x00015EA8
		public RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName)
		{
			IntPtr intPtr = new IntPtr((int)hKey);
			IntPtr intPtr2;
			int num = Win32RegistryApi.RegConnectRegistry(machineName, intPtr, out intPtr2);
			if (num != 0)
			{
				this.GenerateException(num);
			}
			return new RegistryKey(hKey, intPtr2, true);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00017CDC File Offset: 0x00015EDC
		public RegistryKey OpenSubKey(RegistryKey rkey, string keyName, bool writable)
		{
			int num = 131097;
			if (writable)
			{
				num |= 131078;
			}
			IntPtr intPtr;
			int num2 = Win32RegistryApi.RegOpenKeyEx(this.GetHandle(rkey), keyName, IntPtr.Zero, num, out intPtr);
			if (num2 == 2 || num2 == 1018)
			{
				return null;
			}
			if (num2 != 0)
			{
				this.GenerateException(num2);
			}
			return new RegistryKey(intPtr, Win32RegistryApi.CombineName(rkey, keyName), writable);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00017D3B File Offset: 0x00015F3B
		public void Flush(RegistryKey rkey)
		{
			if (!Win32RegistryApi.IsHandleValid(rkey))
			{
				return;
			}
			Win32RegistryApi.RegFlushKey(this.GetHandle(rkey));
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00017D54 File Offset: 0x00015F54
		public void Close(RegistryKey rkey)
		{
			if (!Win32RegistryApi.IsHandleValid(rkey))
			{
				return;
			}
			SafeRegistryHandle handle = rkey.Handle;
			if (handle != null)
			{
				handle.Close();
				return;
			}
			Win32RegistryApi.RegCloseKey(this.GetHandle(rkey));
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00017D88 File Offset: 0x00015F88
		public RegistryKey FromHandle(SafeRegistryHandle handle)
		{
			return new RegistryKey(handle.DangerousGetHandle(), string.Empty, true);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00017DA0 File Offset: 0x00015FA0
		public RegistryKey CreateSubKey(RegistryKey rkey, string keyName)
		{
			IntPtr intPtr;
			int num2;
			int num = Win32RegistryApi.RegCreateKeyEx(this.GetHandle(rkey), keyName, 0, IntPtr.Zero, 0, 131103, IntPtr.Zero, out intPtr, out num2);
			if (num != 0)
			{
				this.GenerateException(num);
			}
			return new RegistryKey(intPtr, Win32RegistryApi.CombineName(rkey, keyName), true);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00017DF0 File Offset: 0x00015FF0
		public RegistryKey CreateSubKey(RegistryKey rkey, string keyName, RegistryOptions options)
		{
			IntPtr intPtr;
			int num2;
			int num = Win32RegistryApi.RegCreateKeyEx(this.GetHandle(rkey), keyName, 0, IntPtr.Zero, (options == RegistryOptions.Volatile) ? 1 : 0, 131103, IntPtr.Zero, out intPtr, out num2);
			if (num != 0)
			{
				this.GenerateException(num);
			}
			return new RegistryKey(intPtr, Win32RegistryApi.CombineName(rkey, keyName), true);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00017E44 File Offset: 0x00016044
		public void DeleteKey(RegistryKey rkey, string keyName, bool shouldThrowWhenKeyMissing)
		{
			int num = Win32RegistryApi.RegDeleteKey(this.GetHandle(rkey), keyName);
			if (num != 2)
			{
				if (num != 0)
				{
					this.GenerateException(num);
				}
				return;
			}
			if (shouldThrowWhenKeyMissing)
			{
				throw new ArgumentException("key " + keyName);
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00017E84 File Offset: 0x00016084
		public void DeleteValue(RegistryKey rkey, string value, bool shouldThrowWhenKeyMissing)
		{
			int num = Win32RegistryApi.RegDeleteValue(this.GetHandle(rkey), value);
			if (num == 1018)
			{
				return;
			}
			if (num != 2)
			{
				if (num != 0)
				{
					this.GenerateException(num);
				}
				return;
			}
			if (shouldThrowWhenKeyMissing)
			{
				throw new ArgumentException("value " + value);
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00017ECC File Offset: 0x000160CC
		public unsafe string[] GetSubKeyNames(RegistryKey rkey)
		{
			int num = this.SubKeyCount(rkey);
			string[] array = new string[num];
			if (num > 0)
			{
				IntPtr handle = this.GetHandle(rkey);
				char[] array2 = new char[256];
				fixed (char* ptr = &array2[0])
				{
					char* ptr2 = ptr;
					for (int i = 0; i < num; i++)
					{
						int num2 = array2.Length;
						int num3 = Win32RegistryApi.RegEnumKeyEx(handle, i, ptr2, ref num2, null, null, null, null);
						if (num3 != 0)
						{
							this.GenerateException(num3);
						}
						array[i] = new string(ptr2);
					}
				}
			}
			return array;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00017F50 File Offset: 0x00016150
		public unsafe string[] GetValueNames(RegistryKey rkey)
		{
			int num = this.ValueCount(rkey);
			string[] array = new string[num];
			if (num > 0)
			{
				IntPtr handle = this.GetHandle(rkey);
				char[] array2 = new char[16384];
				fixed (char* ptr = &array2[0])
				{
					char* ptr2 = ptr;
					for (int i = 0; i < num; i++)
					{
						int num2 = array2.Length;
						int num3 = Win32RegistryApi.RegEnumValue(handle, i, ptr2, ref num2, IntPtr.Zero, null, null, null);
						if (num3 != 0 && num3 != 234)
						{
							this.GenerateException(num3);
						}
						array[i] = new string(ptr2);
					}
				}
			}
			return array;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00017FE1 File Offset: 0x000161E1
		private void CheckResult(int result)
		{
			if (result != 0)
			{
				this.GenerateException(result);
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00017FF0 File Offset: 0x000161F0
		private void GenerateException(int errorCode)
		{
			if (errorCode <= 53)
			{
				switch (errorCode)
				{
				case 2:
					break;
				case 3:
				case 4:
					goto IL_0072;
				case 5:
					throw new SecurityException();
				case 6:
					throw new IOException("Invalid handle.");
				default:
					if (errorCode != 53)
					{
						goto IL_0072;
					}
					throw new IOException("The network path was not found.");
				}
			}
			else if (errorCode != 87)
			{
				if (errorCode == 1018)
				{
					throw RegistryKey.CreateMarkedForDeletionException();
				}
				if (errorCode != 1021)
				{
					goto IL_0072;
				}
				throw new IOException("Cannot create a stable subkey under a volatile parent key.");
			}
			throw new ArgumentException();
			IL_0072:
			throw new SystemException();
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000176B0 File Offset: 0x000158B0
		public string ToString(RegistryKey rkey)
		{
			return rkey.Name;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00018074 File Offset: 0x00016274
		internal static string CombineName(RegistryKey rkey, string localName)
		{
			return rkey.Name + "\\" + localName;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00018087 File Offset: 0x00016287
		public Win32RegistryApi()
		{
		}

		// Token: 0x04000E7E RID: 3710
		private const int OpenRegKeyRead = 131097;

		// Token: 0x04000E7F RID: 3711
		private const int OpenRegKeyWrite = 131078;

		// Token: 0x04000E80 RID: 3712
		private const int Int32ByteSize = 4;

		// Token: 0x04000E81 RID: 3713
		private const int Int64ByteSize = 8;

		// Token: 0x04000E82 RID: 3714
		private readonly int NativeBytesPerCharacter = Marshal.SystemDefaultCharSize;

		// Token: 0x04000E83 RID: 3715
		private const int RegOptionsNonVolatile = 0;

		// Token: 0x04000E84 RID: 3716
		private const int RegOptionsVolatile = 1;

		// Token: 0x04000E85 RID: 3717
		private const int MaxKeyLength = 255;

		// Token: 0x04000E86 RID: 3718
		private const int MaxValueLength = 16383;
	}
}
