using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x0200008A RID: 138
	[ComVisible(true)]
	public sealed class RegistryKey : MarshalByRefObject, IDisposable
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x00015686 File Offset: 0x00013886
		static RegistryKey()
		{
			if (Path.DirectorySeparatorChar == '\\')
			{
				RegistryKey.RegistryApi = new Win32RegistryApi();
				return;
			}
			RegistryKey.RegistryApi = new UnixRegistryApi();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000156A6 File Offset: 0x000138A6
		internal RegistryKey(RegistryHive hiveId)
			: this(hiveId, new IntPtr((int)hiveId), false)
		{
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000156B6 File Offset: 0x000138B6
		internal RegistryKey(RegistryHive hiveId, IntPtr keyHandle, bool remoteRoot)
		{
			this.hive = hiveId;
			this.handle = keyHandle;
			this.qname = RegistryKey.GetHiveName(hiveId);
			this.isRemoteRoot = remoteRoot;
			this.isWritable = true;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000156F0 File Offset: 0x000138F0
		internal RegistryKey(object data, string keyName, bool writable)
		{
			this.handle = data;
			this.qname = keyName;
			this.isWritable = writable;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00015710 File Offset: 0x00013910
		internal static bool IsEquals(RegistryKey a, RegistryKey b)
		{
			return a.hive == b.hive && a.handle == b.handle && a.qname == b.qname && a.isRemoteRoot == b.isRemoteRoot && a.isWritable == b.isWritable;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001576A File Offset: 0x0001396A
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Close();
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00015778 File Offset: 0x00013978
		public string Name
		{
			get
			{
				return this.qname;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00015780 File Offset: 0x00013980
		public void Flush()
		{
			RegistryKey.RegistryApi.Flush(this);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001578D File Offset: 0x0001398D
		public void Close()
		{
			this.Flush();
			if (!this.isRemoteRoot && this.IsRoot)
			{
				return;
			}
			RegistryKey.RegistryApi.Close(this);
			this.handle = null;
			this.safe_handle = null;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x000157BF File Offset: 0x000139BF
		public int SubKeyCount
		{
			get
			{
				this.AssertKeyStillValid();
				return RegistryKey.RegistryApi.SubKeyCount(this);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x000157D2 File Offset: 0x000139D2
		public int ValueCount
		{
			get
			{
				this.AssertKeyStillValid();
				return RegistryKey.RegistryApi.ValueCount(this);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000157E8 File Offset: 0x000139E8
		[ComVisible(false)]
		[MonoTODO("Not implemented in Unix")]
		public SafeRegistryHandle Handle
		{
			get
			{
				this.AssertKeyStillValid();
				if (this.safe_handle == null)
				{
					IntPtr intPtr = RegistryKey.RegistryApi.GetHandle(this);
					this.safe_handle = new SafeRegistryHandle(intPtr, true);
				}
				return this.safe_handle;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000408A File Offset: 0x0000228A
		[ComVisible(false)]
		[MonoLimitation("View is ignored in Mono.")]
		public RegistryView View
		{
			get
			{
				return RegistryView.Default;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00015822 File Offset: 0x00013A22
		public void SetValue(string name, object value)
		{
			this.AssertKeyStillValid();
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name != null)
			{
				this.AssertKeyNameLength(name);
			}
			if (!this.IsWritable)
			{
				throw new UnauthorizedAccessException("Cannot write to the registry key.");
			}
			RegistryKey.RegistryApi.SetValue(this, name, value);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00015864 File Offset: 0x00013A64
		[ComVisible(false)]
		public void SetValue(string name, object value, RegistryValueKind valueKind)
		{
			this.AssertKeyStillValid();
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name != null)
			{
				this.AssertKeyNameLength(name);
			}
			if (!this.IsWritable)
			{
				throw new UnauthorizedAccessException("Cannot write to the registry key.");
			}
			RegistryKey.RegistryApi.SetValue(this, name, value, valueKind);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000158B0 File Offset: 0x00013AB0
		public RegistryKey OpenSubKey(string name)
		{
			return this.OpenSubKey(name, false);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000158BA File Offset: 0x00013ABA
		public RegistryKey OpenSubKey(string name, bool writable)
		{
			this.AssertKeyStillValid();
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.AssertKeyNameLength(name);
			return RegistryKey.RegistryApi.OpenSubKey(this, name, writable);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000158E4 File Offset: 0x00013AE4
		public object GetValue(string name)
		{
			return this.GetValue(name, null);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x000158EE File Offset: 0x00013AEE
		public object GetValue(string name, object defaultValue)
		{
			this.AssertKeyStillValid();
			return RegistryKey.RegistryApi.GetValue(this, name, defaultValue, RegistryValueOptions.None);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00015904 File Offset: 0x00013B04
		[ComVisible(false)]
		public object GetValue(string name, object defaultValue, RegistryValueOptions options)
		{
			this.AssertKeyStillValid();
			return RegistryKey.RegistryApi.GetValue(this, name, defaultValue, options);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001591A File Offset: 0x00013B1A
		[ComVisible(false)]
		public RegistryValueKind GetValueKind(string name)
		{
			return RegistryKey.RegistryApi.GetValueKind(this, name);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00015928 File Offset: 0x00013B28
		public RegistryKey CreateSubKey(string subkey)
		{
			this.AssertKeyStillValid();
			this.AssertKeyNameNotNull(subkey);
			this.AssertKeyNameLength(subkey);
			if (!this.IsWritable)
			{
				throw new UnauthorizedAccessException("Cannot write to the registry key.");
			}
			return RegistryKey.RegistryApi.CreateSubKey(this, subkey);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001595D File Offset: 0x00013B5D
		[ComVisible(false)]
		[MonoLimitation("permissionCheck is ignored in Mono")]
		public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck)
		{
			return this.CreateSubKey(subkey);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001595D File Offset: 0x00013B5D
		[ComVisible(false)]
		[MonoLimitation("permissionCheck and registrySecurity are ignored in Mono")]
		public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistrySecurity registrySecurity)
		{
			return this.CreateSubKey(subkey);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00015966 File Offset: 0x00013B66
		[ComVisible(false)]
		[MonoLimitation("permissionCheck is ignored in Mono")]
		public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions options)
		{
			this.AssertKeyStillValid();
			this.AssertKeyNameNotNull(subkey);
			this.AssertKeyNameLength(subkey);
			if (!this.IsWritable)
			{
				throw new UnauthorizedAccessException("Cannot write to the registry key.");
			}
			return RegistryKey.RegistryApi.CreateSubKey(this, subkey, options);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001599C File Offset: 0x00013B9C
		[ComVisible(false)]
		[MonoLimitation("permissionCheck and registrySecurity are ignored in Mono")]
		public RegistryKey CreateSubKey(string subkey, RegistryKeyPermissionCheck permissionCheck, RegistryOptions registryOptions, RegistrySecurity registrySecurity)
		{
			return this.CreateSubKey(subkey, permissionCheck, registryOptions);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000159A7 File Offset: 0x00013BA7
		[ComVisible(false)]
		public RegistryKey CreateSubKey(string subkey, bool writable)
		{
			return this.CreateSubKey(subkey, writable ? RegistryKeyPermissionCheck.ReadWriteSubTree : RegistryKeyPermissionCheck.ReadSubTree);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000159B7 File Offset: 0x00013BB7
		[ComVisible(false)]
		public RegistryKey CreateSubKey(string subkey, bool writable, RegistryOptions options)
		{
			return this.CreateSubKey(subkey, writable ? RegistryKeyPermissionCheck.ReadWriteSubTree : RegistryKeyPermissionCheck.ReadSubTree, options);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000159C8 File Offset: 0x00013BC8
		public void DeleteSubKey(string subkey)
		{
			this.DeleteSubKey(subkey, true);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000159D4 File Offset: 0x00013BD4
		public void DeleteSubKey(string subkey, bool throwOnMissingSubKey)
		{
			this.AssertKeyStillValid();
			this.AssertKeyNameNotNull(subkey);
			this.AssertKeyNameLength(subkey);
			if (!this.IsWritable)
			{
				throw new UnauthorizedAccessException("Cannot write to the registry key.");
			}
			RegistryKey registryKey = this.OpenSubKey(subkey);
			if (registryKey == null)
			{
				if (throwOnMissingSubKey)
				{
					throw new ArgumentException("Cannot delete a subkey tree because the subkey does not exist.");
				}
				return;
			}
			else
			{
				if (registryKey.SubKeyCount > 0)
				{
					throw new InvalidOperationException("Registry key has subkeys and recursive removes are not supported by this method.");
				}
				registryKey.Close();
				RegistryKey.RegistryApi.DeleteKey(this, subkey, throwOnMissingSubKey);
				return;
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00015A49 File Offset: 0x00013C49
		public void DeleteSubKeyTree(string subkey)
		{
			this.DeleteSubKeyTree(subkey, true);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00015A54 File Offset: 0x00013C54
		public void DeleteSubKeyTree(string subkey, bool throwOnMissingSubKey)
		{
			this.AssertKeyStillValid();
			this.AssertKeyNameNotNull(subkey);
			this.AssertKeyNameLength(subkey);
			RegistryKey registryKey = this.OpenSubKey(subkey, true);
			if (registryKey != null)
			{
				registryKey.DeleteChildKeysAndValues();
				registryKey.Close();
				this.DeleteSubKey(subkey, false);
				return;
			}
			if (!throwOnMissingSubKey)
			{
				return;
			}
			throw new ArgumentException("Cannot delete a subkey tree because the subkey does not exist.");
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00015AA4 File Offset: 0x00013CA4
		public void DeleteValue(string name)
		{
			this.DeleteValue(name, true);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00015AAE File Offset: 0x00013CAE
		public void DeleteValue(string name, bool throwOnMissingValue)
		{
			this.AssertKeyStillValid();
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (!this.IsWritable)
			{
				throw new UnauthorizedAccessException("Cannot write to the registry key.");
			}
			RegistryKey.RegistryApi.DeleteValue(this, name, throwOnMissingValue);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00015AE4 File Offset: 0x00013CE4
		public RegistrySecurity GetAccessControl()
		{
			return this.GetAccessControl(AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00015AEE File Offset: 0x00013CEE
		public RegistrySecurity GetAccessControl(AccessControlSections includeSections)
		{
			return new RegistrySecurity(this.Name, includeSections);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00015AFC File Offset: 0x00013CFC
		public string[] GetSubKeyNames()
		{
			this.AssertKeyStillValid();
			return RegistryKey.RegistryApi.GetSubKeyNames(this);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00015B0F File Offset: 0x00013D0F
		public string[] GetValueNames()
		{
			this.AssertKeyStillValid();
			return RegistryKey.RegistryApi.GetValueNames(this);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00015B22 File Offset: 0x00013D22
		[ComVisible(false)]
		[MonoTODO("Not implemented on unix")]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static RegistryKey FromHandle(SafeRegistryHandle handle)
		{
			if (handle == null)
			{
				throw new ArgumentNullException("handle");
			}
			return RegistryKey.RegistryApi.FromHandle(handle);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00015B3D File Offset: 0x00013D3D
		[ComVisible(false)]
		[MonoTODO("Not implemented on unix")]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static RegistryKey FromHandle(SafeRegistryHandle handle, RegistryView view)
		{
			return RegistryKey.FromHandle(handle);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00015B45 File Offset: 0x00013D45
		[MonoTODO("Not implemented on unix")]
		public static RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			return RegistryKey.RegistryApi.OpenRemoteBaseKey(hKey, machineName);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00015B45 File Offset: 0x00013D45
		[ComVisible(false)]
		[MonoTODO("Not implemented on unix")]
		public static RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName, RegistryView view)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			return RegistryKey.RegistryApi.OpenRemoteBaseKey(hKey, machineName);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00015B64 File Offset: 0x00013D64
		[ComVisible(false)]
		[MonoLimitation("View is ignored in Mono")]
		public static RegistryKey OpenBaseKey(RegistryHive hKey, RegistryView view)
		{
			switch (hKey)
			{
			case RegistryHive.ClassesRoot:
				return Registry.ClassesRoot;
			case RegistryHive.CurrentUser:
				return Registry.CurrentUser;
			case RegistryHive.LocalMachine:
				return Registry.LocalMachine;
			case RegistryHive.Users:
				return Registry.Users;
			case RegistryHive.PerformanceData:
				return Registry.PerformanceData;
			case RegistryHive.CurrentConfig:
				return Registry.CurrentConfig;
			case RegistryHive.DynData:
				return Registry.DynData;
			default:
				throw new ArgumentException("hKey");
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00015BCF File Offset: 0x00013DCF
		[ComVisible(false)]
		public RegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck)
		{
			return this.OpenSubKey(name, permissionCheck == RegistryKeyPermissionCheck.ReadWriteSubTree);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00015BDC File Offset: 0x00013DDC
		[ComVisible(false)]
		[MonoLimitation("rights are ignored in Mono")]
		public RegistryKey OpenSubKey(string name, RegistryRights rights)
		{
			return this.OpenSubKey(name);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00015BCF File Offset: 0x00013DCF
		[ComVisible(false)]
		[MonoLimitation("rights are ignored in Mono")]
		public RegistryKey OpenSubKey(string name, RegistryKeyPermissionCheck permissionCheck, RegistryRights rights)
		{
			return this.OpenSubKey(name, permissionCheck == RegistryKeyPermissionCheck.ReadWriteSubTree);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00015BE5 File Offset: 0x00013DE5
		public void SetAccessControl(RegistrySecurity registrySecurity)
		{
			if (registrySecurity == null)
			{
				throw new ArgumentNullException("registrySecurity");
			}
			registrySecurity.PersistModifications(this.Name);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00015C01 File Offset: 0x00013E01
		public override string ToString()
		{
			this.AssertKeyStillValid();
			return RegistryKey.RegistryApi.ToString(this);
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00015C14 File Offset: 0x00013E14
		internal bool IsRoot
		{
			get
			{
				return this.hive != null;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00015C1F File Offset: 0x00013E1F
		private bool IsWritable
		{
			get
			{
				return this.isWritable;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00015C27 File Offset: 0x00013E27
		internal RegistryHive Hive
		{
			get
			{
				if (!this.IsRoot)
				{
					throw new NotSupportedException();
				}
				return (RegistryHive)this.hive;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00015C42 File Offset: 0x00013E42
		internal object InternalHandle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00015C4A File Offset: 0x00013E4A
		private void AssertKeyStillValid()
		{
			if (this.handle == null)
			{
				throw new ObjectDisposedException("Microsoft.Win32.RegistryKey");
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00015C5F File Offset: 0x00013E5F
		private void AssertKeyNameNotNull(string subKeyName)
		{
			if (subKeyName == null)
			{
				throw new ArgumentNullException("name");
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00015C6F File Offset: 0x00013E6F
		private void AssertKeyNameLength(string name)
		{
			if (name.Length > 255)
			{
				throw new ArgumentException("Name of registry key cannot be greater than 255 characters");
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00015C8C File Offset: 0x00013E8C
		private void DeleteChildKeysAndValues()
		{
			if (this.IsRoot)
			{
				return;
			}
			foreach (string text in this.GetSubKeyNames())
			{
				RegistryKey registryKey = this.OpenSubKey(text, true);
				registryKey.DeleteChildKeysAndValues();
				registryKey.Close();
				this.DeleteSubKey(text, false);
			}
			foreach (string text2 in this.GetValueNames())
			{
				this.DeleteValue(text2, false);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00015CF8 File Offset: 0x00013EF8
		internal static string DecodeString(byte[] data)
		{
			string text = Encoding.Unicode.GetString(data);
			if (text.IndexOf('\0') != -1)
			{
				text = text.TrimEnd('\0');
			}
			return text;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00015D24 File Offset: 0x00013F24
		internal static IOException CreateMarkedForDeletionException()
		{
			throw new IOException("Illegal operation attempted on a registry key that has been marked for deletion.");
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00015D30 File Offset: 0x00013F30
		private static string GetHiveName(RegistryHive hive)
		{
			switch (hive)
			{
			case RegistryHive.ClassesRoot:
				return "HKEY_CLASSES_ROOT";
			case RegistryHive.CurrentUser:
				return "HKEY_CURRENT_USER";
			case RegistryHive.LocalMachine:
				return "HKEY_LOCAL_MACHINE";
			case RegistryHive.Users:
				return "HKEY_USERS";
			case RegistryHive.PerformanceData:
				return "HKEY_PERFORMANCE_DATA";
			case RegistryHive.CurrentConfig:
				return "HKEY_CURRENT_CONFIG";
			case RegistryHive.DynData:
				return "HKEY_DYN_DATA";
			default:
				throw new NotImplementedException(string.Format("Registry hive '{0}' is not implemented.", hive.ToString()));
			}
		}

		// Token: 0x04000E6B RID: 3691
		private object handle;

		// Token: 0x04000E6C RID: 3692
		private SafeRegistryHandle safe_handle;

		// Token: 0x04000E6D RID: 3693
		private object hive;

		// Token: 0x04000E6E RID: 3694
		private readonly string qname;

		// Token: 0x04000E6F RID: 3695
		private readonly bool isRemoteRoot;

		// Token: 0x04000E70 RID: 3696
		private readonly bool isWritable;

		// Token: 0x04000E71 RID: 3697
		private static readonly IRegistryApi RegistryApi;
	}
}
