using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Terraria.Localization
{
	// Token: 0x02000185 RID: 389
	public class GameCulture
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x005104B3 File Offset: 0x0050E6B3
		// (set) Token: 0x06001E87 RID: 7815 RVA: 0x005104BA File Offset: 0x0050E6BA
		public static GameCulture DefaultCulture
		{
			[CompilerGenerated]
			get
			{
				return GameCulture.<DefaultCulture>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				GameCulture.<DefaultCulture>k__BackingField = value;
			}
		} = GameCulture._NamedCultures[GameCulture.CultureName.English];

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x005104C2 File Offset: 0x0050E6C2
		public bool IsActive
		{
			get
			{
				return Language.ActiveCulture == this;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x005104CC File Offset: 0x0050E6CC
		public string Name
		{
			get
			{
				return this.CultureInfo.Name;
			}
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x005104D9 File Offset: 0x0050E6D9
		public static GameCulture FromCultureName(GameCulture.CultureName name)
		{
			if (!GameCulture._NamedCultures.ContainsKey(name))
			{
				return GameCulture.DefaultCulture;
			}
			return GameCulture._NamedCultures[name];
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x005104F9 File Offset: 0x0050E6F9
		public static GameCulture FromLegacyId(int id)
		{
			if (id < 1)
			{
				id = 1;
			}
			if (!GameCulture._legacyCultures.ContainsKey(id))
			{
				return GameCulture.DefaultCulture;
			}
			return GameCulture._legacyCultures[id];
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00510520 File Offset: 0x0050E720
		public static GameCulture FromName(string name)
		{
			return GameCulture._legacyCultures.Values.SingleOrDefault((GameCulture culture) => culture.Name == name) ?? GameCulture.DefaultCulture;
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x00510560 File Offset: 0x0050E760
		static GameCulture()
		{
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x00510667 File Offset: 0x0050E867
		public GameCulture(string name, int legacyId)
		{
			this.CultureInfo = new CultureInfo(name);
			this.LegacyId = legacyId;
			GameCulture.RegisterLegacyCulture(this, legacyId);
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x00510689 File Offset: 0x0050E889
		private static void RegisterLegacyCulture(GameCulture culture, int legacyId)
		{
			if (GameCulture._legacyCultures == null)
			{
				GameCulture._legacyCultures = new Dictionary<int, GameCulture>();
			}
			GameCulture._legacyCultures.Add(legacyId, culture);
		}

		// Token: 0x040016E6 RID: 5862
		private static Dictionary<GameCulture.CultureName, GameCulture> _NamedCultures = new Dictionary<GameCulture.CultureName, GameCulture>
		{
			{
				GameCulture.CultureName.English,
				new GameCulture("en-US", 1)
			},
			{
				GameCulture.CultureName.German,
				new GameCulture("de-DE", 2)
			},
			{
				GameCulture.CultureName.Italian,
				new GameCulture("it-IT", 3)
			},
			{
				GameCulture.CultureName.French,
				new GameCulture("fr-FR", 4)
			},
			{
				GameCulture.CultureName.Spanish,
				new GameCulture("es-ES", 5)
			},
			{
				GameCulture.CultureName.Russian,
				new GameCulture("ru-RU", 6)
			},
			{
				GameCulture.CultureName.Chinese,
				new GameCulture("zh-Hans", 7)
			},
			{
				GameCulture.CultureName.Portuguese,
				new GameCulture("pt-BR", 8)
			},
			{
				GameCulture.CultureName.Polish,
				new GameCulture("pl-PL", 9)
			},
			{
				GameCulture.CultureName.Japanese,
				new GameCulture("ja-JP", 10)
			},
			{
				GameCulture.CultureName.Korean,
				new GameCulture("ko-KR", 11)
			},
			{
				GameCulture.CultureName.ChineseTraditional,
				new GameCulture("zh-Hant", 12)
			}
		};

		// Token: 0x040016E7 RID: 5863
		[CompilerGenerated]
		private static GameCulture <DefaultCulture>k__BackingField;

		// Token: 0x040016E8 RID: 5864
		private static Dictionary<int, GameCulture> _legacyCultures;

		// Token: 0x040016E9 RID: 5865
		public readonly CultureInfo CultureInfo;

		// Token: 0x040016EA RID: 5866
		public readonly int LegacyId;

		// Token: 0x02000755 RID: 1877
		public enum CultureName
		{
			// Token: 0x040069E4 RID: 27108
			English = 1,
			// Token: 0x040069E5 RID: 27109
			German,
			// Token: 0x040069E6 RID: 27110
			Italian,
			// Token: 0x040069E7 RID: 27111
			French,
			// Token: 0x040069E8 RID: 27112
			Spanish,
			// Token: 0x040069E9 RID: 27113
			Russian,
			// Token: 0x040069EA RID: 27114
			Chinese,
			// Token: 0x040069EB RID: 27115
			Portuguese,
			// Token: 0x040069EC RID: 27116
			Polish,
			// Token: 0x040069ED RID: 27117
			Japanese,
			// Token: 0x040069EE RID: 27118
			Korean,
			// Token: 0x040069EF RID: 27119
			ChineseTraditional,
			// Token: 0x040069F0 RID: 27120
			Unknown = 9999
		}

		// Token: 0x02000756 RID: 1878
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x060040F3 RID: 16627 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x060040F4 RID: 16628 RVA: 0x0069EFAC File Offset: 0x0069D1AC
			internal bool <FromName>b__0(GameCulture culture)
			{
				return culture.Name == this.name;
			}

			// Token: 0x040069F1 RID: 27121
			public string name;
		}
	}
}
