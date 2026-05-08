using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using Terraria.Localization;

namespace Terraria.IO
{
	// Token: 0x02000074 RID: 116
	public class Preferences
	{
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06001510 RID: 5392 RVA: 0x004BD508 File Offset: 0x004BB708
		// (remove) Token: 0x06001511 RID: 5393 RVA: 0x004BD540 File Offset: 0x004BB740
		public event Action<Preferences> OnSave
		{
			[CompilerGenerated]
			add
			{
				Action<Preferences> action = this.OnSave;
				Action<Preferences> action2;
				do
				{
					action2 = action;
					Action<Preferences> action3 = (Action<Preferences>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Preferences>>(ref this.OnSave, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Preferences> action = this.OnSave;
				Action<Preferences> action2;
				do
				{
					action2 = action;
					Action<Preferences> action3 = (Action<Preferences>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Preferences>>(ref this.OnSave, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06001512 RID: 5394 RVA: 0x004BD578 File Offset: 0x004BB778
		// (remove) Token: 0x06001513 RID: 5395 RVA: 0x004BD5B0 File Offset: 0x004BB7B0
		public event Action<Preferences> OnLoad
		{
			[CompilerGenerated]
			add
			{
				Action<Preferences> action = this.OnLoad;
				Action<Preferences> action2;
				do
				{
					action2 = action;
					Action<Preferences> action3 = (Action<Preferences>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Preferences>>(ref this.OnLoad, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Preferences> action = this.OnLoad;
				Action<Preferences> action2;
				do
				{
					action2 = action;
					Action<Preferences> action3 = (Action<Preferences>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Preferences>>(ref this.OnLoad, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06001514 RID: 5396 RVA: 0x004BD5E8 File Offset: 0x004BB7E8
		// (remove) Token: 0x06001515 RID: 5397 RVA: 0x004BD620 File Offset: 0x004BB820
		public event Preferences.TextProcessAction OnProcessText
		{
			[CompilerGenerated]
			add
			{
				Preferences.TextProcessAction textProcessAction = this.OnProcessText;
				Preferences.TextProcessAction textProcessAction2;
				do
				{
					textProcessAction2 = textProcessAction;
					Preferences.TextProcessAction textProcessAction3 = (Preferences.TextProcessAction)Delegate.Combine(textProcessAction2, value);
					textProcessAction = Interlocked.CompareExchange<Preferences.TextProcessAction>(ref this.OnProcessText, textProcessAction3, textProcessAction2);
				}
				while (textProcessAction != textProcessAction2);
			}
			[CompilerGenerated]
			remove
			{
				Preferences.TextProcessAction textProcessAction = this.OnProcessText;
				Preferences.TextProcessAction textProcessAction2;
				do
				{
					textProcessAction2 = textProcessAction;
					Preferences.TextProcessAction textProcessAction3 = (Preferences.TextProcessAction)Delegate.Remove(textProcessAction2, value);
					textProcessAction = Interlocked.CompareExchange<Preferences.TextProcessAction>(ref this.OnProcessText, textProcessAction3, textProcessAction2);
				}
				while (textProcessAction != textProcessAction2);
			}
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x004BD658 File Offset: 0x004BB858
		public Preferences(string path, bool parseAllTypes = false, bool useBson = false)
		{
			this._path = path;
			this.UseBson = useBson;
			if (parseAllTypes)
			{
				this._serializerSettings = new JsonSerializerSettings
				{
					TypeNameHandling = 4,
					MetadataPropertyHandling = 1,
					Formatting = 1
				};
				return;
			}
			this._serializerSettings = new JsonSerializerSettings
			{
				Formatting = 1
			};
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x004BD6C8 File Offset: 0x004BB8C8
		public bool Load()
		{
			object @lock = this._lock;
			bool flag2;
			lock (@lock)
			{
				if (!File.Exists(this._path))
				{
					flag2 = false;
				}
				else
				{
					try
					{
						if (!this.UseBson)
						{
							string text = File.ReadAllText(this._path);
							this._data = JsonConvert.DeserializeObject<Dictionary<string, object>>(text, this._serializerSettings);
						}
						else
						{
							using (FileStream fileStream = File.OpenRead(this._path))
							{
								using (BsonReader bsonReader = new BsonReader(fileStream))
								{
									JsonSerializer jsonSerializer = JsonSerializer.Create(this._serializerSettings);
									this._data = jsonSerializer.Deserialize<Dictionary<string, object>>(bsonReader);
								}
							}
						}
						if (this._data == null)
						{
							this._data = new Dictionary<string, object>();
						}
						if (this.OnLoad != null)
						{
							this.OnLoad(this);
						}
						flag2 = true;
					}
					catch (Exception)
					{
						flag2 = false;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x004BD7E0 File Offset: 0x004BB9E0
		public bool Save(bool canCreateFile = true)
		{
			object @lock = this._lock;
			bool flag2;
			lock (@lock)
			{
				try
				{
					if (this.OnSave != null)
					{
						this.OnSave(this);
					}
					if (!canCreateFile && !File.Exists(this._path))
					{
						return false;
					}
					Directory.GetParent(this._path).Create();
					if (File.Exists(this._path))
					{
						File.SetAttributes(this._path, FileAttributes.Normal);
					}
					if (!this.UseBson)
					{
						string text = JsonConvert.SerializeObject(this._data, this._serializerSettings);
						if (this.OnProcessText != null)
						{
							this.OnProcessText(ref text);
						}
						File.WriteAllText(this._path, text);
						File.SetAttributes(this._path, FileAttributes.Normal);
					}
					else
					{
						using (FileStream fileStream = File.Create(this._path))
						{
							using (BsonWriter bsonWriter = new BsonWriter(fileStream))
							{
								File.SetAttributes(this._path, FileAttributes.Normal);
								JsonSerializer.Create(this._serializerSettings).Serialize(bsonWriter, this._data);
							}
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(Language.GetTextValue("Error.UnableToWritePreferences", this._path));
					Console.WriteLine(ex.ToString());
					return false;
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x004BD998 File Offset: 0x004BBB98
		public void Clear()
		{
			this._data.Clear();
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x004BD9A8 File Offset: 0x004BBBA8
		public void Put(string name, object value)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this._data[name] = value;
				if (this.AutoSave)
				{
					this.Save(true);
				}
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x004BDA00 File Offset: 0x004BBC00
		public bool Contains(string name)
		{
			object @lock = this._lock;
			bool flag2;
			lock (@lock)
			{
				flag2 = this._data.ContainsKey(name);
			}
			return flag2;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x004BDA48 File Offset: 0x004BBC48
		public T Get<T>(string name, T defaultValue)
		{
			object @lock = this._lock;
			T t;
			lock (@lock)
			{
				try
				{
					object obj;
					if (this._data.TryGetValue(name, out obj))
					{
						if (obj is T)
						{
							t = (T)((object)obj);
						}
						else if (obj is JObject)
						{
							t = JsonConvert.DeserializeObject<T>(((JObject)obj).ToString());
						}
						else if (typeof(T).IsEnum)
						{
							t = (T)((object)Convert.ChangeType(obj, Enum.GetUnderlyingType(typeof(T))));
						}
						else
						{
							t = (T)((object)Convert.ChangeType(obj, typeof(T)));
						}
					}
					else
					{
						t = defaultValue;
					}
				}
				catch
				{
					t = defaultValue;
				}
			}
			return t;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x004BDB18 File Offset: 0x004BBD18
		public void Get<T>(string name, ref T currentValue)
		{
			currentValue = this.Get<T>(name, currentValue);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x004BDB2D File Offset: 0x004BBD2D
		public List<string> GetAllKeys()
		{
			return this._data.Keys.ToList<string>();
		}

		// Token: 0x040010CA RID: 4298
		private Dictionary<string, object> _data = new Dictionary<string, object>();

		// Token: 0x040010CB RID: 4299
		private readonly string _path;

		// Token: 0x040010CC RID: 4300
		private readonly JsonSerializerSettings _serializerSettings;

		// Token: 0x040010CD RID: 4301
		public readonly bool UseBson;

		// Token: 0x040010CE RID: 4302
		private readonly object _lock = new object();

		// Token: 0x040010CF RID: 4303
		public bool AutoSave;

		// Token: 0x040010D0 RID: 4304
		[CompilerGenerated]
		private Action<Preferences> OnSave;

		// Token: 0x040010D1 RID: 4305
		[CompilerGenerated]
		private Action<Preferences> OnLoad;

		// Token: 0x040010D2 RID: 4306
		[CompilerGenerated]
		private Preferences.TextProcessAction OnProcessText;

		// Token: 0x02000668 RID: 1640
		// (Invoke) Token: 0x06003DB8 RID: 15800
		public delegate void TextProcessAction(ref string text);
	}
}
