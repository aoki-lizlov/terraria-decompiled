using System;
using System.Globalization;
using System.IO;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x0200008E RID: 142
	internal class UnixRegistryApi : IRegistryApi
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x00017486 File Offset: 0x00015686
		private static string ToUnix(string keyname)
		{
			if (keyname.IndexOf('\\') != -1)
			{
				keyname = keyname.Replace('\\', '/');
			}
			return keyname.ToLower();
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000174A5 File Offset: 0x000156A5
		private static bool IsWellKnownKey(string parentKeyName, string keyname)
		{
			return (parentKeyName == Registry.CurrentUser.Name || parentKeyName == Registry.LocalMachine.Name) && string.Compare("software", keyname, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000174E1 File Offset: 0x000156E1
		public RegistryKey CreateSubKey(RegistryKey rkey, string keyname)
		{
			return this.CreateSubKey(rkey, keyname, true);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000174EC File Offset: 0x000156EC
		public RegistryKey CreateSubKey(RegistryKey rkey, string keyname, RegistryOptions options)
		{
			return this.CreateSubKey(rkey, keyname, true, options == RegistryOptions.Volatile);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000174FB File Offset: 0x000156FB
		public RegistryKey OpenRemoteBaseKey(RegistryHive hKey, string machineName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00017504 File Offset: 0x00015704
		public RegistryKey OpenSubKey(RegistryKey rkey, string keyname, bool writable)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				return null;
			}
			RegistryKey registryKey = keyHandler.Probe(rkey, UnixRegistryApi.ToUnix(keyname), writable);
			if (registryKey == null && UnixRegistryApi.IsWellKnownKey(rkey.Name, keyname))
			{
				registryKey = this.CreateSubKey(rkey, keyname, writable);
			}
			return registryKey;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000174FB File Offset: 0x000156FB
		public RegistryKey FromHandle(SafeRegistryHandle handle)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001754C File Offset: 0x0001574C
		public void Flush(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, false);
			if (keyHandler == null)
			{
				return;
			}
			keyHandler.Flush();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001756B File Offset: 0x0001576B
		public void Close(RegistryKey rkey)
		{
			KeyHandler.Drop(rkey);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00017574 File Offset: 0x00015774
		public object GetValue(RegistryKey rkey, string name, object default_value, RegistryValueOptions options)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				return default_value;
			}
			if (keyHandler.ValueExists(name))
			{
				return keyHandler.GetValue(name, options);
			}
			return default_value;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000175A2 File Offset: 0x000157A2
		public void SetValue(RegistryKey rkey, string name, object value)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			keyHandler.SetValue(name, value);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000175BB File Offset: 0x000157BB
		public void SetValue(RegistryKey rkey, string name, object value, RegistryValueKind valueKind)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			keyHandler.SetValue(name, value, valueKind);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000175D6 File Offset: 0x000157D6
		public int SubKeyCount(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			return keyHandler.GetSubKeyCount();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000175ED File Offset: 0x000157ED
		public int ValueCount(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			return keyHandler.ValueCount;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00017604 File Offset: 0x00015804
		public void DeleteValue(RegistryKey rkey, string name, bool throw_if_missing)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				return;
			}
			if (throw_if_missing && !keyHandler.ValueExists(name))
			{
				throw new ArgumentException("the given value does not exist");
			}
			keyHandler.RemoveValue(name);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001763C File Offset: 0x0001583C
		public void DeleteKey(RegistryKey rkey, string keyname, bool throw_if_missing)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				if (!throw_if_missing)
				{
					return;
				}
				throw new ArgumentException("the given value does not exist");
			}
			else
			{
				if (!KeyHandler.Delete(Path.Combine(keyHandler.Dir, UnixRegistryApi.ToUnix(keyname))) && throw_if_missing)
				{
					throw new ArgumentException("the given value does not exist");
				}
				return;
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001768B File Offset: 0x0001588B
		public string[] GetSubKeyNames(RegistryKey rkey)
		{
			return KeyHandler.Lookup(rkey, true).GetSubKeyNames();
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00017699 File Offset: 0x00015899
		public string[] GetValueNames(RegistryKey rkey)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			return keyHandler.GetValueNames();
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000176B0 File Offset: 0x000158B0
		public string ToString(RegistryKey rkey)
		{
			return rkey.Name;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000176B8 File Offset: 0x000158B8
		private RegistryKey CreateSubKey(RegistryKey rkey, string keyname, bool writable)
		{
			return this.CreateSubKey(rkey, keyname, writable, false);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000176C4 File Offset: 0x000158C4
		private RegistryKey CreateSubKey(RegistryKey rkey, string keyname, bool writable, bool is_volatile)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler == null)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
			if (KeyHandler.VolatileKeyExists(keyHandler.Dir) && !is_volatile)
			{
				throw new IOException("Cannot create a non volatile subkey under a volatile key.");
			}
			return keyHandler.Ensure(rkey, UnixRegistryApi.ToUnix(keyname), writable, is_volatile);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00017704 File Offset: 0x00015904
		public RegistryValueKind GetValueKind(RegistryKey rkey, string name)
		{
			KeyHandler keyHandler = KeyHandler.Lookup(rkey, true);
			if (keyHandler != null)
			{
				return keyHandler.GetValueKind(name);
			}
			return RegistryValueKind.Unknown;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000174FB File Offset: 0x000156FB
		public IntPtr GetHandle(RegistryKey key)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000025BE File Offset: 0x000007BE
		public UnixRegistryApi()
		{
		}
	}
}
