using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x02000088 RID: 136
	internal interface IRegistryApi
	{
		// Token: 0x060003CC RID: 972
		RegistryKey CreateSubKey(RegistryKey rkey, string keyname);

		// Token: 0x060003CD RID: 973
		RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName);

		// Token: 0x060003CE RID: 974
		RegistryKey OpenSubKey(RegistryKey rkey, string keyname, bool writtable);

		// Token: 0x060003CF RID: 975
		void Flush(RegistryKey rkey);

		// Token: 0x060003D0 RID: 976
		void Close(RegistryKey rkey);

		// Token: 0x060003D1 RID: 977
		object GetValue(RegistryKey rkey, string name, object default_value, RegistryValueOptions options);

		// Token: 0x060003D2 RID: 978
		RegistryValueKind GetValueKind(RegistryKey rkey, string name);

		// Token: 0x060003D3 RID: 979
		void SetValue(RegistryKey rkey, string name, object value);

		// Token: 0x060003D4 RID: 980
		int SubKeyCount(RegistryKey rkey);

		// Token: 0x060003D5 RID: 981
		int ValueCount(RegistryKey rkey);

		// Token: 0x060003D6 RID: 982
		void DeleteValue(RegistryKey rkey, string value, bool throw_if_missing);

		// Token: 0x060003D7 RID: 983
		void DeleteKey(RegistryKey rkey, string keyName, bool throw_if_missing);

		// Token: 0x060003D8 RID: 984
		string[] GetSubKeyNames(RegistryKey rkey);

		// Token: 0x060003D9 RID: 985
		string[] GetValueNames(RegistryKey rkey);

		// Token: 0x060003DA RID: 986
		string ToString(RegistryKey rkey);

		// Token: 0x060003DB RID: 987
		void SetValue(RegistryKey rkey, string name, object value, RegistryValueKind valueKind);

		// Token: 0x060003DC RID: 988
		RegistryKey CreateSubKey(RegistryKey rkey, string keyname, RegistryOptions options);

		// Token: 0x060003DD RID: 989
		RegistryKey FromHandle(SafeRegistryHandle handle);

		// Token: 0x060003DE RID: 990
		IntPtr GetHandle(RegistryKey key);
	}
}
