using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x02000330 RID: 816
	public static class Filters
	{
		// Token: 0x020008B0 RID: 2224
		public class BySearch : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>, ISearchFilter<BestiaryEntry>
		{
			// Token: 0x1700055A RID: 1370
			// (get) Token: 0x060045DE RID: 17886 RVA: 0x006C5F57 File Offset: 0x006C4157
			public bool? ForcedDisplay
			{
				get
				{
					return new bool?(true);
				}
			}

			// Token: 0x060045DF RID: 17887 RVA: 0x0000357B File Offset: 0x0000177B
			public BySearch()
			{
			}

			// Token: 0x060045E0 RID: 17888 RVA: 0x006C5F60 File Offset: 0x006C4160
			public bool FitsFilter(BestiaryEntry entry)
			{
				if (this._search == null)
				{
					return true;
				}
				BestiaryUICollectionInfo entryUICollectionInfo = entry.UIInfoProvider.GetEntryUICollectionInfo();
				for (int i = 0; i < entry.Info.Count; i++)
				{
					IProvideSearchFilterString provideSearchFilterString = entry.Info[i] as IProvideSearchFilterString;
					if (provideSearchFilterString != null)
					{
						string searchString = provideSearchFilterString.GetSearchString(ref entryUICollectionInfo);
						if (searchString != null && searchString.ToLower().IndexOf(this._search, StringComparison.OrdinalIgnoreCase) != -1)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x060045E1 RID: 17889 RVA: 0x006C5FD2 File Offset: 0x006C41D2
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.IfSearched";
			}

			// Token: 0x060045E2 RID: 17890 RVA: 0x006C2B1B File Offset: 0x006C0D1B
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Rank_Light", 1);
				return new UIImageFramed(asset, asset.Frame(1, 1, 0, 0, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x060045E3 RID: 17891 RVA: 0x006C5FD9 File Offset: 0x006C41D9
			public void SetSearch(string searchText)
			{
				this._search = searchText;
			}

			// Token: 0x04007330 RID: 29488
			private string _search;
		}

		// Token: 0x020008B1 RID: 2225
		public class ByUnlockState : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>
		{
			// Token: 0x1700055B RID: 1371
			// (get) Token: 0x060045E4 RID: 17892 RVA: 0x006C5F57 File Offset: 0x006C4157
			public bool? ForcedDisplay
			{
				get
				{
					return new bool?(true);
				}
			}

			// Token: 0x060045E5 RID: 17893 RVA: 0x006C5FE4 File Offset: 0x006C41E4
			public bool FitsFilter(BestiaryEntry entry)
			{
				BestiaryUICollectionInfo entryUICollectionInfo = entry.UIInfoProvider.GetEntryUICollectionInfo();
				return entry.Icon.GetUnlockState(entryUICollectionInfo);
			}

			// Token: 0x060045E6 RID: 17894 RVA: 0x006C6009 File Offset: 0x006C4209
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.IfUnlocked";
			}

			// Token: 0x060045E7 RID: 17895 RVA: 0x006C6010 File Offset: 0x006C4210
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow", 1);
				return new UIImageFramed(asset, asset.Frame(16, 5, 14, 3, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x060045E8 RID: 17896 RVA: 0x0000357B File Offset: 0x0000177B
			public ByUnlockState()
			{
			}
		}

		// Token: 0x020008B2 RID: 2226
		public class ByRareCreature : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>
		{
			// Token: 0x1700055C RID: 1372
			// (get) Token: 0x060045E9 RID: 17897 RVA: 0x006C604C File Offset: 0x006C424C
			public bool? ForcedDisplay
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060045EA RID: 17898 RVA: 0x006C6064 File Offset: 0x006C4264
			public bool FitsFilter(BestiaryEntry entry)
			{
				for (int i = 0; i < entry.Info.Count; i++)
				{
					if (entry.Info[i] is RareSpawnBestiaryInfoElement)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060045EB RID: 17899 RVA: 0x006C609D File Offset: 0x006C429D
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.IsRare";
			}

			// Token: 0x060045EC RID: 17900 RVA: 0x006C2B1B File Offset: 0x006C0D1B
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Rank_Light", 1);
				return new UIImageFramed(asset, asset.Frame(1, 1, 0, 0, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x060045ED RID: 17901 RVA: 0x0000357B File Offset: 0x0000177B
			public ByRareCreature()
			{
			}
		}

		// Token: 0x020008B3 RID: 2227
		public class ByBoss : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>
		{
			// Token: 0x1700055D RID: 1373
			// (get) Token: 0x060045EE RID: 17902 RVA: 0x006C60A4 File Offset: 0x006C42A4
			public bool? ForcedDisplay
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060045EF RID: 17903 RVA: 0x006C60BC File Offset: 0x006C42BC
			public bool FitsFilter(BestiaryEntry entry)
			{
				for (int i = 0; i < entry.Info.Count; i++)
				{
					if (entry.Info[i] is BossBestiaryInfoElement)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060045F0 RID: 17904 RVA: 0x006C60F5 File Offset: 0x006C42F5
			public string GetDisplayNameKey()
			{
				return "BestiaryInfo.IsBoss";
			}

			// Token: 0x060045F1 RID: 17905 RVA: 0x006C60FC File Offset: 0x006C42FC
			public UIElement GetImage()
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Tags_Shadow", 1);
				return new UIImageFramed(asset, asset.Frame(16, 5, 15, 3, 0, 0))
				{
					HAlign = 0.5f,
					VAlign = 0.5f
				};
			}

			// Token: 0x060045F2 RID: 17906 RVA: 0x0000357B File Offset: 0x0000177B
			public ByBoss()
			{
			}
		}

		// Token: 0x020008B4 RID: 2228
		public class ByInfoElement : IBestiaryEntryFilter, IEntryFilter<BestiaryEntry>
		{
			// Token: 0x1700055E RID: 1374
			// (get) Token: 0x060045F3 RID: 17907 RVA: 0x006C6138 File Offset: 0x006C4338
			public bool? ForcedDisplay
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060045F4 RID: 17908 RVA: 0x006C614E File Offset: 0x006C434E
			public ByInfoElement(IBestiaryInfoElement element)
			{
				this._element = element;
			}

			// Token: 0x060045F5 RID: 17909 RVA: 0x006C615D File Offset: 0x006C435D
			public bool FitsFilter(BestiaryEntry entry)
			{
				return entry.Info.Contains(this._element);
			}

			// Token: 0x060045F6 RID: 17910 RVA: 0x006C6170 File Offset: 0x006C4370
			public string GetDisplayNameKey()
			{
				IFilterInfoProvider filterInfoProvider = this._element as IFilterInfoProvider;
				if (filterInfoProvider == null)
				{
					return null;
				}
				return filterInfoProvider.GetDisplayNameKey();
			}

			// Token: 0x060045F7 RID: 17911 RVA: 0x006C6194 File Offset: 0x006C4394
			public UIElement GetImage()
			{
				IFilterInfoProvider filterInfoProvider = this._element as IFilterInfoProvider;
				if (filterInfoProvider == null)
				{
					return null;
				}
				return filterInfoProvider.GetFilterImage();
			}

			// Token: 0x04007331 RID: 29489
			private IBestiaryInfoElement _element;
		}
	}
}
