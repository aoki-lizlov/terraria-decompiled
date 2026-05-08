using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x020009F6 RID: 2550
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class SortKey
	{
		// Token: 0x06005EAE RID: 24238 RVA: 0x00143B18 File Offset: 0x00141D18
		public static int Compare(SortKey sortkey1, SortKey sortkey2)
		{
			if (sortkey1 == null)
			{
				throw new ArgumentNullException("sortkey1");
			}
			if (sortkey2 == null)
			{
				throw new ArgumentNullException("sortkey2");
			}
			if (sortkey1 == sortkey2 || sortkey1.OriginalString == sortkey2.OriginalString)
			{
				return 0;
			}
			byte[] keyData = sortkey1.KeyData;
			byte[] keyData2 = sortkey2.KeyData;
			int num = ((keyData.Length > keyData2.Length) ? keyData2.Length : keyData.Length);
			int i = 0;
			while (i < num)
			{
				if (keyData[i] != keyData2[i])
				{
					if (keyData[i] >= keyData2[i])
					{
						return 1;
					}
					return -1;
				}
				else
				{
					i++;
				}
			}
			if (keyData.Length == keyData2.Length)
			{
				return 0;
			}
			if (keyData.Length >= keyData2.Length)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06005EAF RID: 24239 RVA: 0x00143BAC File Offset: 0x00141DAC
		internal SortKey(int lcid, string source, CompareOptions opt)
		{
			this.lcid = lcid;
			this.source = source;
			this.options = opt;
			int length = source.Length;
			byte[] array = new byte[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = (byte)source[i];
			}
			this.key = array;
		}

		// Token: 0x06005EB0 RID: 24240 RVA: 0x00143C00 File Offset: 0x00141E00
		internal SortKey(int lcid, string source, byte[] buffer, CompareOptions opt, int lv1Length, int lv2Length, int lv3Length, int kanaSmallLength, int markTypeLength, int katakanaLength, int kanaWidthLength, int identLength)
		{
			this.lcid = lcid;
			this.source = source;
			this.key = buffer;
			this.options = opt;
		}

		// Token: 0x06005EB1 RID: 24241 RVA: 0x00143C25 File Offset: 0x00141E25
		internal SortKey(string localeName, string str, CompareOptions options, byte[] keyData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06005EB2 RID: 24242 RVA: 0x00143C32 File Offset: 0x00141E32
		public virtual string OriginalString
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06005EB3 RID: 24243 RVA: 0x00143C3A File Offset: 0x00141E3A
		public virtual byte[] KeyData
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x06005EB4 RID: 24244 RVA: 0x00143C44 File Offset: 0x00141E44
		public override bool Equals(object value)
		{
			SortKey sortKey = value as SortKey;
			return sortKey != null && this.lcid == sortKey.lcid && this.options == sortKey.options && SortKey.Compare(this, sortKey) == 0;
		}

		// Token: 0x06005EB5 RID: 24245 RVA: 0x00143C84 File Offset: 0x00141E84
		public override int GetHashCode()
		{
			if (this.key.Length == 0)
			{
				return 0;
			}
			int num = (int)this.key[0];
			for (int i = 1; i < this.key.Length; i++)
			{
				num ^= (int)this.key[i] << (i & 3);
			}
			return num;
		}

		// Token: 0x06005EB6 RID: 24246 RVA: 0x00143CCC File Offset: 0x00141ECC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"SortKey - ",
				this.lcid.ToString(),
				", ",
				this.options.ToString(),
				", ",
				this.source
			});
		}

		// Token: 0x040038F5 RID: 14581
		private readonly string source;

		// Token: 0x040038F6 RID: 14582
		private readonly byte[] key;

		// Token: 0x040038F7 RID: 14583
		private readonly CompareOptions options;

		// Token: 0x040038F8 RID: 14584
		private readonly int lcid;
	}
}
