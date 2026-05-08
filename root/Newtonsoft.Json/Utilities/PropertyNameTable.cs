using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000053 RID: 83
	internal class PropertyNameTable
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x00011B9A File Offset: 0x0000FD9A
		static PropertyNameTable()
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00011BA6 File Offset: 0x0000FDA6
		public PropertyNameTable()
		{
			this._entries = new PropertyNameTable.Entry[this._mask + 1];
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00011BCC File Offset: 0x0000FDCC
		public string Get(char[] key, int start, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			int num = length + PropertyNameTable.HashCodeRandomizer;
			num += (num << 7) ^ (int)key[start];
			int num2 = start + length;
			for (int i = start + 1; i < num2; i++)
			{
				num += (num << 7) ^ (int)key[i];
			}
			num -= num >> 17;
			num -= num >> 11;
			num -= num >> 5;
			for (PropertyNameTable.Entry entry = this._entries[num & this._mask]; entry != null; entry = entry.Next)
			{
				if (entry.HashCode == num && PropertyNameTable.TextEquals(entry.Value, key, start, length))
				{
					return entry.Value;
				}
			}
			return null;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00011C64 File Offset: 0x0000FE64
		public string Add(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int length = key.Length;
			if (length == 0)
			{
				return string.Empty;
			}
			int num = length + PropertyNameTable.HashCodeRandomizer;
			for (int i = 0; i < key.Length; i++)
			{
				num += (num << 7) ^ (int)key.get_Chars(i);
			}
			num -= num >> 17;
			num -= num >> 11;
			num -= num >> 5;
			for (PropertyNameTable.Entry entry = this._entries[num & this._mask]; entry != null; entry = entry.Next)
			{
				if (entry.HashCode == num && entry.Value.Equals(key))
				{
					return entry.Value;
				}
			}
			return this.AddEntry(key, num);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00011D0C File Offset: 0x0000FF0C
		private string AddEntry(string str, int hashCode)
		{
			int num = hashCode & this._mask;
			PropertyNameTable.Entry entry = new PropertyNameTable.Entry(str, hashCode, this._entries[num]);
			this._entries[num] = entry;
			int count = this._count;
			this._count = count + 1;
			if (count == this._mask)
			{
				this.Grow();
			}
			return entry.Value;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00011D60 File Offset: 0x0000FF60
		private void Grow()
		{
			PropertyNameTable.Entry[] entries = this._entries;
			int num = this._mask * 2 + 1;
			PropertyNameTable.Entry[] array = new PropertyNameTable.Entry[num + 1];
			foreach (PropertyNameTable.Entry entry in entries)
			{
				while (entry != null)
				{
					int num2 = entry.HashCode & num;
					PropertyNameTable.Entry next = entry.Next;
					entry.Next = array[num2];
					array[num2] = entry;
					entry = next;
				}
			}
			this._entries = array;
			this._mask = num;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00011DD8 File Offset: 0x0000FFD8
		private static bool TextEquals(string str1, char[] str2, int str2Start, int str2Length)
		{
			if (str1.Length != str2Length)
			{
				return false;
			}
			for (int i = 0; i < str1.Length; i++)
			{
				if (str1.get_Chars(i) != str2[str2Start + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040001FE RID: 510
		private static readonly int HashCodeRandomizer = Environment.TickCount;

		// Token: 0x040001FF RID: 511
		private int _count;

		// Token: 0x04000200 RID: 512
		private PropertyNameTable.Entry[] _entries;

		// Token: 0x04000201 RID: 513
		private int _mask = 31;

		// Token: 0x02000126 RID: 294
		private class Entry
		{
			// Token: 0x06000CA5 RID: 3237 RVA: 0x00030F4C File Offset: 0x0002F14C
			internal Entry(string value, int hashCode, PropertyNameTable.Entry next)
			{
				this.Value = value;
				this.HashCode = hashCode;
				this.Next = next;
			}

			// Token: 0x0400047B RID: 1147
			internal readonly string Value;

			// Token: 0x0400047C RID: 1148
			internal readonly int HashCode;

			// Token: 0x0400047D RID: 1149
			internal PropertyNameTable.Entry Next;
		}
	}
}
