using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;

namespace Microsoft.Win32
{
	// Token: 0x0200008D RID: 141
	internal class KeyHandler
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x00015EBC File Offset: 0x000140BC
		static KeyHandler()
		{
			KeyHandler.CleanVolatileKeys();
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00015EE6 File Offset: 0x000140E6
		private KeyHandler(RegistryKey rkey, string basedir)
			: this(rkey, basedir, false)
		{
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00015EF4 File Offset: 0x000140F4
		private KeyHandler(RegistryKey rkey, string basedir, bool is_volatile)
		{
			string volatileDir = KeyHandler.GetVolatileDir(basedir);
			string text = basedir;
			if (Directory.Exists(basedir))
			{
				is_volatile = false;
			}
			else if (Directory.Exists(volatileDir))
			{
				text = volatileDir;
				is_volatile = true;
			}
			else if (is_volatile)
			{
				text = volatileDir;
			}
			if (!Directory.Exists(text))
			{
				try
				{
					Directory.CreateDirectory(text);
				}
				catch (UnauthorizedAccessException ex)
				{
					throw new SecurityException("No access to the given key", ex);
				}
			}
			this.Dir = basedir;
			this.ActualDir = text;
			this.IsVolatile = is_volatile;
			this.file = Path.Combine(this.ActualDir, "values.xml");
			this.Load();
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00015F90 File Offset: 0x00014190
		public void Load()
		{
			this.values = new Hashtable();
			if (!File.Exists(this.file))
			{
				return;
			}
			try
			{
				using (FileStream fileStream = File.OpenRead(this.file))
				{
					string text = new StreamReader(fileStream).ReadToEnd();
					if (text.Length != 0)
					{
						SecurityElement securityElement = SecurityElement.FromString(text);
						if (securityElement.Tag == "values" && securityElement.Children != null)
						{
							foreach (object obj in securityElement.Children)
							{
								SecurityElement securityElement2 = (SecurityElement)obj;
								if (securityElement2.Tag == "value")
								{
									this.LoadKey(securityElement2);
								}
							}
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				this.values.Clear();
				throw new SecurityException("No access to the given key");
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("While loading registry key at {0}: {1}", this.file, ex);
				this.values.Clear();
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000160D0 File Offset: 0x000142D0
		private void LoadKey(SecurityElement se)
		{
			Hashtable attributes = se.Attributes;
			try
			{
				string text = (string)attributes["name"];
				if (text != null)
				{
					string text2 = (string)attributes["type"];
					if (text2 != null)
					{
						if (!(text2 == "int"))
						{
							if (!(text2 == "bytearray"))
							{
								if (!(text2 == "string"))
								{
									if (!(text2 == "expand"))
									{
										if (!(text2 == "qword"))
										{
											if (text2 == "string-array")
											{
												List<string> list = new List<string>();
												if (se.Children != null)
												{
													foreach (object obj in se.Children)
													{
														SecurityElement securityElement = (SecurityElement)obj;
														list.Add(securityElement.Text);
													}
												}
												this.values[text] = list.ToArray();
											}
										}
										else
										{
											this.values[text] = long.Parse(se.Text);
										}
									}
									else
									{
										this.values[text] = new ExpandString(se.Text);
									}
								}
								else
								{
									this.values[text] = ((se.Text == null) ? string.Empty : se.Text);
								}
							}
							else
							{
								this.values[text] = Convert.FromBase64String(se.Text);
							}
						}
						else
						{
							this.values[text] = int.Parse(se.Text);
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000162B4 File Offset: 0x000144B4
		public RegistryKey Ensure(RegistryKey rkey, string extra, bool writable)
		{
			return this.Ensure(rkey, extra, writable, false);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000162C0 File Offset: 0x000144C0
		public RegistryKey Ensure(RegistryKey rkey, string extra, bool writable, bool is_volatile)
		{
			Type typeFromHandle = typeof(KeyHandler);
			RegistryKey registryKey2;
			lock (typeFromHandle)
			{
				string text = Path.Combine(this.Dir, extra);
				KeyHandler keyHandler = (KeyHandler)KeyHandler.dir_to_handler[text];
				if (keyHandler == null)
				{
					keyHandler = new KeyHandler(rkey, text, is_volatile);
				}
				RegistryKey registryKey = new RegistryKey(keyHandler, KeyHandler.CombineName(rkey, extra), writable);
				KeyHandler.key_to_handler[registryKey] = keyHandler;
				KeyHandler.dir_to_handler[text] = keyHandler;
				registryKey2 = registryKey;
			}
			return registryKey2;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00016358 File Offset: 0x00014558
		public RegistryKey Probe(RegistryKey rkey, string extra, bool writable)
		{
			RegistryKey registryKey = null;
			Type typeFromHandle = typeof(KeyHandler);
			RegistryKey registryKey2;
			lock (typeFromHandle)
			{
				string text = Path.Combine(this.Dir, extra);
				KeyHandler keyHandler = (KeyHandler)KeyHandler.dir_to_handler[text];
				if (keyHandler != null)
				{
					registryKey = new RegistryKey(keyHandler, KeyHandler.CombineName(rkey, extra), writable);
					KeyHandler.key_to_handler[registryKey] = keyHandler;
				}
				else if (Directory.Exists(text) || KeyHandler.VolatileKeyExists(text))
				{
					keyHandler = new KeyHandler(rkey, text);
					registryKey = new RegistryKey(keyHandler, KeyHandler.CombineName(rkey, extra), writable);
					KeyHandler.dir_to_handler[text] = keyHandler;
					KeyHandler.key_to_handler[registryKey] = keyHandler;
				}
				registryKey2 = registryKey;
			}
			return registryKey2;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00016424 File Offset: 0x00014624
		private static string CombineName(RegistryKey rkey, string extra)
		{
			if (extra.IndexOf('/') != -1)
			{
				extra = extra.Replace('/', '\\');
			}
			return rkey.Name + "\\" + extra;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00016450 File Offset: 0x00014650
		private static long GetSystemBootTime()
		{
			if (!File.Exists("/proc/stat"))
			{
				return -1L;
			}
			string text = null;
			try
			{
				using (StreamReader streamReader = new StreamReader("/proc/stat", Encoding.ASCII))
				{
					string text2;
					while ((text2 = streamReader.ReadLine()) != null)
					{
						if (text2.StartsWith("btime"))
						{
							text = text2;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("While reading system info {0}", ex);
			}
			if (text == null)
			{
				return -1L;
			}
			int num = text.IndexOf(' ');
			long num2;
			if (!long.TryParse(text.Substring(num, text.Length - num), out num2))
			{
				return -1L;
			}
			return num2;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001650C File Offset: 0x0001470C
		private static long GetRegisteredBootTime(string path)
		{
			if (!File.Exists(path))
			{
				return -1L;
			}
			string text = null;
			try
			{
				using (StreamReader streamReader = new StreamReader(path, Encoding.ASCII))
				{
					text = streamReader.ReadLine();
				}
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("While reading registry data at {0}: {1}", path, ex);
			}
			if (text == null)
			{
				return -1L;
			}
			long num;
			if (!long.TryParse(text, out num))
			{
				return -1L;
			}
			return num;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001658C File Offset: 0x0001478C
		private static void SaveRegisteredBootTime(string path, long btime)
		{
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.ASCII))
				{
					streamWriter.WriteLine(btime.ToString());
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000165E0 File Offset: 0x000147E0
		private static void CleanVolatileKeys()
		{
			long systemBootTime = KeyHandler.GetSystemBootTime();
			foreach (string text in new string[]
			{
				KeyHandler.UserStore,
				KeyHandler.MachineStore
			})
			{
				if (Directory.Exists(text))
				{
					string text2 = Path.Combine(text, "last-btime");
					string text3 = Path.Combine(text, "volatile-keys");
					if (Directory.Exists(text3))
					{
						long registeredBootTime = KeyHandler.GetRegisteredBootTime(text2);
						if (systemBootTime < 0L || registeredBootTime < 0L || registeredBootTime != systemBootTime)
						{
							Directory.Delete(text3, true);
						}
					}
					KeyHandler.SaveRegisteredBootTime(text2, systemBootTime);
				}
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00016670 File Offset: 0x00014870
		public static bool VolatileKeyExists(string dir)
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				KeyHandler keyHandler = (KeyHandler)KeyHandler.dir_to_handler[dir];
				if (keyHandler != null)
				{
					return keyHandler.IsVolatile;
				}
			}
			return !Directory.Exists(dir) && Directory.Exists(KeyHandler.GetVolatileDir(dir));
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000166E4 File Offset: 0x000148E4
		public static string GetVolatileDir(string dir)
		{
			string rootFromDir = KeyHandler.GetRootFromDir(dir);
			return dir.Replace(rootFromDir, Path.Combine(rootFromDir, "volatile-keys"));
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001670C File Offset: 0x0001490C
		public static KeyHandler Lookup(RegistryKey rkey, bool createNonExisting)
		{
			Type typeFromHandle = typeof(KeyHandler);
			KeyHandler keyHandler2;
			lock (typeFromHandle)
			{
				KeyHandler keyHandler = (KeyHandler)KeyHandler.key_to_handler[rkey];
				if (keyHandler != null)
				{
					keyHandler2 = keyHandler;
				}
				else if (!rkey.IsRoot || !createNonExisting)
				{
					keyHandler2 = null;
				}
				else
				{
					RegistryHive hive = rkey.Hive;
					switch (hive)
					{
					case RegistryHive.ClassesRoot:
					case RegistryHive.LocalMachine:
					case RegistryHive.Users:
					case RegistryHive.PerformanceData:
					case RegistryHive.CurrentConfig:
					case RegistryHive.DynData:
					{
						string text = Path.Combine(KeyHandler.MachineStore, hive.ToString());
						keyHandler = new KeyHandler(rkey, text);
						KeyHandler.dir_to_handler[text] = keyHandler;
						break;
					}
					case RegistryHive.CurrentUser:
					{
						string text2 = Path.Combine(KeyHandler.UserStore, hive.ToString());
						keyHandler = new KeyHandler(rkey, text2);
						KeyHandler.dir_to_handler[text2] = keyHandler;
						break;
					}
					default:
						throw new Exception("Unknown RegistryHive");
					}
					KeyHandler.key_to_handler[rkey] = keyHandler;
					keyHandler2 = keyHandler;
				}
			}
			return keyHandler2;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00016828 File Offset: 0x00014A28
		private static string GetRootFromDir(string dir)
		{
			if (dir.IndexOf(KeyHandler.UserStore) > -1)
			{
				return KeyHandler.UserStore;
			}
			if (dir.IndexOf(KeyHandler.MachineStore) > -1)
			{
				return KeyHandler.MachineStore;
			}
			throw new Exception("Could not get root for dir " + dir);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00016864 File Offset: 0x00014A64
		public static void Drop(RegistryKey rkey)
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				KeyHandler keyHandler = (KeyHandler)KeyHandler.key_to_handler[rkey];
				if (keyHandler != null)
				{
					KeyHandler.key_to_handler.Remove(rkey);
					int num = 0;
					foreach (object obj in KeyHandler.key_to_handler)
					{
						if (((DictionaryEntry)obj).Value == keyHandler)
						{
							num++;
						}
					}
					if (num == 0)
					{
						KeyHandler.dir_to_handler.Remove(keyHandler.Dir);
					}
				}
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00016930 File Offset: 0x00014B30
		public static void Drop(string dir)
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				KeyHandler keyHandler = (KeyHandler)KeyHandler.dir_to_handler[dir];
				if (keyHandler != null)
				{
					KeyHandler.dir_to_handler.Remove(dir);
					ArrayList arrayList = new ArrayList();
					foreach (object obj in KeyHandler.key_to_handler)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (dictionaryEntry.Value == keyHandler)
						{
							arrayList.Add(dictionaryEntry.Key);
						}
					}
					foreach (object obj2 in arrayList)
					{
						KeyHandler.key_to_handler.Remove(obj2);
					}
				}
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00016A44 File Offset: 0x00014C44
		public static bool Delete(string dir)
		{
			if (!Directory.Exists(dir))
			{
				string volatileDir = KeyHandler.GetVolatileDir(dir);
				if (!Directory.Exists(volatileDir))
				{
					return false;
				}
				dir = volatileDir;
			}
			Directory.Delete(dir, true);
			KeyHandler.Drop(dir);
			return true;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00016A7C File Offset: 0x00014C7C
		public RegistryValueKind GetValueKind(string name)
		{
			if (name == null)
			{
				return RegistryValueKind.Unknown;
			}
			Hashtable hashtable = this.values;
			object obj;
			lock (hashtable)
			{
				obj = this.values[name];
			}
			if (obj == null)
			{
				return RegistryValueKind.Unknown;
			}
			if (obj is int)
			{
				return RegistryValueKind.DWord;
			}
			if (obj is string[])
			{
				return RegistryValueKind.MultiString;
			}
			if (obj is long)
			{
				return RegistryValueKind.QWord;
			}
			if (obj is byte[])
			{
				return RegistryValueKind.Binary;
			}
			if (obj is string)
			{
				return RegistryValueKind.String;
			}
			if (obj is ExpandString)
			{
				return RegistryValueKind.ExpandString;
			}
			return RegistryValueKind.Unknown;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00016B0C File Offset: 0x00014D0C
		public object GetValue(string name, RegistryValueOptions options)
		{
			if (this.IsMarkedForDeletion)
			{
				return null;
			}
			if (name == null)
			{
				name = string.Empty;
			}
			Hashtable hashtable = this.values;
			object obj;
			lock (hashtable)
			{
				obj = this.values[name];
			}
			ExpandString expandString = obj as ExpandString;
			if (expandString == null)
			{
				return obj;
			}
			if ((options & RegistryValueOptions.DoNotExpandEnvironmentNames) == RegistryValueOptions.None)
			{
				return expandString.Expand();
			}
			return expandString.ToString();
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00016B88 File Offset: 0x00014D88
		public void SetValue(string name, object value)
		{
			this.AssertNotMarkedForDeletion();
			if (name == null)
			{
				name = string.Empty;
			}
			Hashtable hashtable = this.values;
			lock (hashtable)
			{
				if (value is int || value is string || value is byte[] || value is string[])
				{
					this.values[name] = value;
				}
				else
				{
					this.values[name] = value.ToString();
				}
			}
			this.SetDirty();
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00016C1C File Offset: 0x00014E1C
		public string[] GetValueNames()
		{
			this.AssertNotMarkedForDeletion();
			Hashtable hashtable = this.values;
			string[] array2;
			lock (hashtable)
			{
				ICollection keys = this.values.Keys;
				string[] array = new string[keys.Count];
				keys.CopyTo(array, 0);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00016C80 File Offset: 0x00014E80
		public int GetSubKeyCount()
		{
			return this.GetSubKeyNames().Length;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00016C8C File Offset: 0x00014E8C
		public string[] GetSubKeyNames()
		{
			DirectoryInfo[] directories = new DirectoryInfo(this.ActualDir).GetDirectories();
			string[] array;
			if (this.IsVolatile || !Directory.Exists(KeyHandler.GetVolatileDir(this.Dir)))
			{
				array = new string[directories.Length];
				for (int i = 0; i < directories.Length; i++)
				{
					DirectoryInfo directoryInfo = directories[i];
					array[i] = directoryInfo.Name;
				}
				return array;
			}
			DirectoryInfo[] directories2 = new DirectoryInfo(KeyHandler.GetVolatileDir(this.Dir)).GetDirectories();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (DirectoryInfo directoryInfo2 in directories)
			{
				dictionary[directoryInfo2.Name] = directoryInfo2.Name;
			}
			foreach (DirectoryInfo directoryInfo3 in directories2)
			{
				dictionary[directoryInfo3.Name] = directoryInfo3.Name;
			}
			array = new string[dictionary.Count];
			int num = 0;
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				array[num++] = keyValuePair.Value;
			}
			return array;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00016DCC File Offset: 0x00014FCC
		public void SetValue(string name, object value, RegistryValueKind valueKind)
		{
			this.SetDirty();
			if (name == null)
			{
				name = string.Empty;
			}
			Hashtable hashtable = this.values;
			lock (hashtable)
			{
				switch (valueKind)
				{
				case RegistryValueKind.String:
					if (value is string)
					{
						this.values[name] = value;
						return;
					}
					goto IL_0116;
				case RegistryValueKind.ExpandString:
					if (value is string)
					{
						this.values[name] = new ExpandString((string)value);
						return;
					}
					goto IL_0116;
				case RegistryValueKind.Binary:
					if (value is byte[])
					{
						this.values[name] = value;
						return;
					}
					goto IL_0116;
				case RegistryValueKind.DWord:
					try
					{
						this.values[name] = Convert.ToInt32(value);
						return;
					}
					catch (OverflowException)
					{
						goto IL_0122;
					}
					break;
				case (RegistryValueKind)5:
				case (RegistryValueKind)6:
				case (RegistryValueKind)8:
				case (RegistryValueKind)9:
				case (RegistryValueKind)10:
					goto IL_0106;
				case RegistryValueKind.MultiString:
					break;
				case RegistryValueKind.QWord:
					try
					{
						this.values[name] = Convert.ToInt64(value);
						return;
					}
					catch (OverflowException)
					{
						goto IL_0122;
					}
					goto IL_0106;
				default:
					goto IL_0106;
				}
				if (value is string[])
				{
					this.values[name] = value;
					return;
				}
				goto IL_0116;
				IL_0106:
				throw new ArgumentException("unknown value", "valueKind");
				IL_0116:;
			}
			IL_0122:
			throw new ArgumentException("Value could not be converted to specified type", "valueKind");
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00016F34 File Offset: 0x00015134
		private void SetDirty()
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				if (!this.dirty)
				{
					this.dirty = true;
					new Timer(new TimerCallback(this.DirtyTimeout), null, 3000, -1);
				}
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00016F9C File Offset: 0x0001519C
		public void DirtyTimeout(object state)
		{
			try
			{
				this.Flush();
			}
			catch
			{
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00016FC4 File Offset: 0x000151C4
		public void Flush()
		{
			Type typeFromHandle = typeof(KeyHandler);
			lock (typeFromHandle)
			{
				if (this.dirty)
				{
					this.Save();
					this.dirty = false;
				}
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00017018 File Offset: 0x00015218
		public bool ValueExists(string name)
		{
			if (name == null)
			{
				name = string.Empty;
			}
			Hashtable hashtable = this.values;
			bool flag2;
			lock (hashtable)
			{
				flag2 = this.values.Contains(name);
			}
			return flag2;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0001706C File Offset: 0x0001526C
		public int ValueCount
		{
			get
			{
				Hashtable hashtable = this.values;
				int count;
				lock (hashtable)
				{
					count = this.values.Keys.Count;
				}
				return count;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x000170B8 File Offset: 0x000152B8
		public bool IsMarkedForDeletion
		{
			get
			{
				return !KeyHandler.dir_to_handler.Contains(this.Dir);
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000170D0 File Offset: 0x000152D0
		public void RemoveValue(string name)
		{
			this.AssertNotMarkedForDeletion();
			Hashtable hashtable = this.values;
			lock (hashtable)
			{
				this.values.Remove(name);
			}
			this.SetDirty();
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00017124 File Offset: 0x00015324
		~KeyHandler()
		{
			this.Flush();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00017150 File Offset: 0x00015350
		private void Save()
		{
			if (this.IsMarkedForDeletion)
			{
				return;
			}
			SecurityElement securityElement = new SecurityElement("values");
			Hashtable hashtable = this.values;
			lock (hashtable)
			{
				if (!File.Exists(this.file) && this.values.Count == 0)
				{
					return;
				}
				foreach (object obj in this.values)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					object value = dictionaryEntry.Value;
					SecurityElement securityElement2 = new SecurityElement("value");
					securityElement2.AddAttribute("name", SecurityElement.Escape((string)dictionaryEntry.Key));
					if (value is string)
					{
						securityElement2.AddAttribute("type", "string");
						securityElement2.Text = SecurityElement.Escape((string)value);
					}
					else if (value is int)
					{
						securityElement2.AddAttribute("type", "int");
						securityElement2.Text = value.ToString();
					}
					else if (value is long)
					{
						securityElement2.AddAttribute("type", "qword");
						securityElement2.Text = value.ToString();
					}
					else if (value is byte[])
					{
						securityElement2.AddAttribute("type", "bytearray");
						securityElement2.Text = Convert.ToBase64String((byte[])value);
					}
					else if (value is ExpandString)
					{
						securityElement2.AddAttribute("type", "expand");
						securityElement2.Text = SecurityElement.Escape(value.ToString());
					}
					else if (value is string[])
					{
						securityElement2.AddAttribute("type", "string-array");
						foreach (string text in (string[])value)
						{
							securityElement2.AddChild(new SecurityElement("string")
							{
								Text = SecurityElement.Escape(text)
							});
						}
					}
					securityElement.AddChild(securityElement2);
				}
			}
			using (FileStream fileStream = File.Create(this.file))
			{
				StreamWriter streamWriter = new StreamWriter(fileStream);
				streamWriter.Write(securityElement.ToString());
				streamWriter.Flush();
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000173F0 File Offset: 0x000155F0
		private void AssertNotMarkedForDeletion()
		{
			if (this.IsMarkedForDeletion)
			{
				throw RegistryKey.CreateMarkedForDeletionException();
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00017400 File Offset: 0x00015600
		private static string UserStore
		{
			get
			{
				if (KeyHandler.user_store == null)
				{
					KeyHandler.user_store = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ".mono/registry");
				}
				return KeyHandler.user_store;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00017424 File Offset: 0x00015624
		private static string MachineStore
		{
			get
			{
				if (KeyHandler.machine_store == null)
				{
					KeyHandler.machine_store = Environment.GetEnvironmentVariable("MONO_REGISTRY_PATH");
					if (KeyHandler.machine_store == null)
					{
						string machineConfigPath = Environment.GetMachineConfigPath();
						int num = machineConfigPath.IndexOf("machine.config");
						KeyHandler.machine_store = Path.Combine(Path.Combine(machineConfigPath.Substring(0, num - 1), ".."), "registry");
					}
				}
				return KeyHandler.machine_store;
			}
		}

		// Token: 0x04000E73 RID: 3699
		private static Hashtable key_to_handler = new Hashtable(new RegistryKeyComparer());

		// Token: 0x04000E74 RID: 3700
		private static Hashtable dir_to_handler = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());

		// Token: 0x04000E75 RID: 3701
		private const string VolatileDirectoryName = "volatile-keys";

		// Token: 0x04000E76 RID: 3702
		public string Dir;

		// Token: 0x04000E77 RID: 3703
		private string ActualDir;

		// Token: 0x04000E78 RID: 3704
		public bool IsVolatile;

		// Token: 0x04000E79 RID: 3705
		private Hashtable values;

		// Token: 0x04000E7A RID: 3706
		private string file;

		// Token: 0x04000E7B RID: 3707
		private bool dirty;

		// Token: 0x04000E7C RID: 3708
		private static string user_store;

		// Token: 0x04000E7D RID: 3709
		private static string machine_store;
	}
}
