using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using NVorbis.Contracts;

namespace NVorbis
{
	// Token: 0x02000017 RID: 23
	internal class TagData : ITagData
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x00005C88 File Offset: 0x00003E88
		public TagData(string vendor, string[] comments)
		{
			this.EncoderVendor = vendor;
			Dictionary<string, IList<string>> dictionary = new Dictionary<string, IList<string>>();
			for (int i = 0; i < comments.Length; i++)
			{
				string[] array = comments[i].Split(new char[] { '=' });
				if (array.Length == 1)
				{
					array = new string[]
					{
						array[0],
						string.Empty
					};
				}
				int num = array[0].IndexOf('[');
				if (num > -1)
				{
					array[1] = array[0].Substring(num + 1, array[0].Length - num - 2).ToUpper(CultureInfo.CurrentCulture) + ": " + array[1];
					array[0] = array[0].Substring(0, num);
				}
				IList<string> list;
				if (dictionary.TryGetValue(array[0].ToUpperInvariant(), ref list))
				{
					list.Add(array[1]);
				}
				else
				{
					Dictionary<string, IList<string>> dictionary2 = dictionary;
					string text = array[0].ToUpperInvariant();
					List<string> list2 = new List<string>();
					list2.Add(array[1]);
					dictionary2.Add(text, list2);
				}
			}
			this._tags = dictionary;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005D7C File Offset: 0x00003F7C
		public string GetTagSingle(string key, bool concatenate = false)
		{
			IList<string> tagMulti = this.GetTagMulti(key);
			if (tagMulti.Count <= 0)
			{
				return string.Empty;
			}
			if (concatenate)
			{
				return string.Join(Environment.NewLine, Enumerable.ToArray<string>(tagMulti));
			}
			return tagMulti[tagMulti.Count - 1];
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005DC4 File Offset: 0x00003FC4
		public IList<string> GetTagMulti(string key)
		{
			IList<string> list;
			if (this._tags.TryGetValue(key.ToUpperInvariant(), ref list))
			{
				return list;
			}
			return TagData.s_emptyList;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00005DED File Offset: 0x00003FED
		public IDictionary<string, IList<string>> All
		{
			get
			{
				return this._tags;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005DF5 File Offset: 0x00003FF5
		public string EncoderVendor
		{
			[CompilerGenerated]
			get
			{
				return this.<EncoderVendor>k__BackingField;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00005DFD File Offset: 0x00003FFD
		public string Title
		{
			get
			{
				return this.GetTagSingle("TITLE", false);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005E0B File Offset: 0x0000400B
		public string Version
		{
			get
			{
				return this.GetTagSingle("VERSION", false);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00005E19 File Offset: 0x00004019
		public string Album
		{
			get
			{
				return this.GetTagSingle("ALBUM", false);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00005E27 File Offset: 0x00004027
		public string TrackNumber
		{
			get
			{
				return this.GetTagSingle("TRACKNUMBER", false);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005E35 File Offset: 0x00004035
		public string Artist
		{
			get
			{
				return this.GetTagSingle("ARTIST", false);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00005E43 File Offset: 0x00004043
		public IList<string> Performers
		{
			get
			{
				return this.GetTagMulti("PERFORMER");
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005E50 File Offset: 0x00004050
		public string Copyright
		{
			get
			{
				return this.GetTagSingle("COPYRIGHT", false);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005E5E File Offset: 0x0000405E
		public string License
		{
			get
			{
				return this.GetTagSingle("LICENSE", false);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005E6C File Offset: 0x0000406C
		public string Organization
		{
			get
			{
				return this.GetTagSingle("ORGANIZATION", false);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005E7A File Offset: 0x0000407A
		public string Description
		{
			get
			{
				return this.GetTagSingle("DESCRIPTION", false);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005E88 File Offset: 0x00004088
		public IList<string> Genres
		{
			get
			{
				return this.GetTagMulti("GENRE");
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005E95 File Offset: 0x00004095
		public IList<string> Dates
		{
			get
			{
				return this.GetTagMulti("DATE");
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005EA2 File Offset: 0x000040A2
		public IList<string> Locations
		{
			get
			{
				return this.GetTagMulti("LOCATION");
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00005EAF File Offset: 0x000040AF
		public string Contact
		{
			get
			{
				return this.GetTagSingle("CONTACT", false);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005EBD File Offset: 0x000040BD
		public string Isrc
		{
			get
			{
				return this.GetTagSingle("ISRC", false);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005ECB File Offset: 0x000040CB
		// Note: this type is marked as 'beforefieldinit'.
		static TagData()
		{
		}

		// Token: 0x0400007C RID: 124
		private static IList<string> s_emptyList = new List<string>();

		// Token: 0x0400007D RID: 125
		private Dictionary<string, IList<string>> _tags;

		// Token: 0x0400007E RID: 126
		[CompilerGenerated]
		private readonly string <EncoderVendor>k__BackingField;
	}
}
