using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ReLogic.Reflection
{
	// Token: 0x0200000F RID: 15
	public class IdDictionary
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003B55 File Offset: 0x00001D55
		public IEnumerable<string> Names
		{
			get
			{
				return this._nameToId.Keys;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003B62 File Offset: 0x00001D62
		private IdDictionary(int count)
		{
			this.Count = count;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003B7C File Offset: 0x00001D7C
		public bool TryGetName(int id, out string name)
		{
			return this._idToName.TryGetValue(id, ref name);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003B8B File Offset: 0x00001D8B
		public bool TryGetId(string name, out int id)
		{
			return this._nameToId.TryGetValue(name, ref id);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003B9A File Offset: 0x00001D9A
		public bool ContainsName(string name)
		{
			return this._nameToId.ContainsKey(name);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003BA8 File Offset: 0x00001DA8
		public bool ContainsId(int id)
		{
			return this._idToName.ContainsKey(id);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003BB6 File Offset: 0x00001DB6
		public string GetName(int id)
		{
			return this._idToName[id];
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public int GetId(string name)
		{
			return this._nameToId[name];
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003BD2 File Offset: 0x00001DD2
		public void Add(string name, int id)
		{
			this._idToName.Add(id, name);
			this._nameToId.Add(name, id);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003BEE File Offset: 0x00001DEE
		public void Remove(string name)
		{
			this._idToName.Remove(this._nameToId[name]);
			this._nameToId.Remove(name);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003C15 File Offset: 0x00001E15
		public void Remove(int id)
		{
			this._nameToId.Remove(this._idToName[id]);
			this._idToName.Remove(id);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003C3C File Offset: 0x00001E3C
		public static IdDictionary Create(Type idClass, Type idType)
		{
			int num = int.MaxValue;
			FieldInfo fieldInfo = Enumerable.FirstOrDefault<FieldInfo>(idClass.GetFields(), (FieldInfo field) => field.Name == "Count");
			if (fieldInfo != null)
			{
				num = Convert.ToInt32(fieldInfo.GetValue(null));
				if (num == 0)
				{
					throw new Exception("IdDictionary cannot be created before Count field is initialized. Move to bottom of static class");
				}
			}
			IdDictionary dictionary = new IdDictionary(num);
			Enumerable.ToList<FieldInfo>(Enumerable.Where<FieldInfo>(idClass.GetFields(24), (FieldInfo f) => f.FieldType == idType)).ForEach(delegate(FieldInfo field)
			{
				int num2 = Convert.ToInt32(field.GetValue(null));
				if (num2 < dictionary.Count)
				{
					dictionary._nameToId.Add(field.Name, num2);
				}
			});
			dictionary._idToName = Enumerable.ToDictionary<KeyValuePair<string, int>, int, string>(dictionary._nameToId, (KeyValuePair<string, int> kp) => kp.Value, (KeyValuePair<string, int> kp) => kp.Key);
			return dictionary;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003D45 File Offset: 0x00001F45
		public static IdDictionary Create<IdClass, IdType>()
		{
			return IdDictionary.Create(typeof(IdClass), typeof(IdType));
		}

		// Token: 0x04000020 RID: 32
		private readonly Dictionary<string, int> _nameToId = new Dictionary<string, int>();

		// Token: 0x04000021 RID: 33
		private Dictionary<int, string> _idToName;

		// Token: 0x04000022 RID: 34
		public readonly int Count;

		// Token: 0x020000B1 RID: 177
		[CompilerGenerated]
		private sealed class <>c__DisplayClass15_0
		{
			// Token: 0x06000412 RID: 1042 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass15_0()
			{
			}

			// Token: 0x06000413 RID: 1043 RVA: 0x0000DF64 File Offset: 0x0000C164
			internal bool <Create>b__1(FieldInfo f)
			{
				return f.FieldType == this.idType;
			}

			// Token: 0x06000414 RID: 1044 RVA: 0x0000DF78 File Offset: 0x0000C178
			internal void <Create>b__2(FieldInfo field)
			{
				int num = Convert.ToInt32(field.GetValue(null));
				if (num < this.dictionary.Count)
				{
					this.dictionary._nameToId.Add(field.Name, num);
				}
			}

			// Token: 0x0400054E RID: 1358
			public Type idType;

			// Token: 0x0400054F RID: 1359
			public IdDictionary dictionary;
		}

		// Token: 0x020000B2 RID: 178
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000415 RID: 1045 RVA: 0x0000DFB7 File Offset: 0x0000C1B7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000416 RID: 1046 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c()
			{
			}

			// Token: 0x06000417 RID: 1047 RVA: 0x0000DFC3 File Offset: 0x0000C1C3
			internal bool <Create>b__15_0(FieldInfo field)
			{
				return field.Name == "Count";
			}

			// Token: 0x06000418 RID: 1048 RVA: 0x0000DFD5 File Offset: 0x0000C1D5
			internal int <Create>b__15_3(KeyValuePair<string, int> kp)
			{
				return kp.Value;
			}

			// Token: 0x06000419 RID: 1049 RVA: 0x0000DFDE File Offset: 0x0000C1DE
			internal string <Create>b__15_4(KeyValuePair<string, int> kp)
			{
				return kp.Key;
			}

			// Token: 0x04000550 RID: 1360
			public static readonly IdDictionary.<>c <>9 = new IdDictionary.<>c();

			// Token: 0x04000551 RID: 1361
			public static Func<FieldInfo, bool> <>9__15_0;

			// Token: 0x04000552 RID: 1362
			public static Func<KeyValuePair<string, int>, int> <>9__15_3;

			// Token: 0x04000553 RID: 1363
			public static Func<KeyValuePair<string, int>, string> <>9__15_4;
		}
	}
}
