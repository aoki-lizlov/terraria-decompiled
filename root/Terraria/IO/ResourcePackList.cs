using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terraria.Social;
using Terraria.Social.Base;

namespace Terraria.IO
{
	// Token: 0x0200006C RID: 108
	public class ResourcePackList
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x004BBC74 File Offset: 0x004B9E74
		public IEnumerable<ResourcePack> EnabledPacks
		{
			get
			{
				return from pack in this._resourcePacks
					where pack.IsEnabled
					orderby pack.SortingOrder, pack.Name, pack.Version, pack.FileName
					select pack;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x004BBD3C File Offset: 0x004B9F3C
		public IEnumerable<ResourcePack> DisabledPacks
		{
			get
			{
				return from pack in this._resourcePacks
					where !pack.IsEnabled
					orderby pack.Name, pack.Version, pack.FileName
					select pack;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x004BBDE0 File Offset: 0x004B9FE0
		public IEnumerable<ResourcePack> AllPacks
		{
			get
			{
				return from pack in this._resourcePacks
					orderby pack.Name, pack.Version, pack.FileName
					select pack;
			}
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x004BBE5F File Offset: 0x004BA05F
		public ResourcePackList()
		{
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x004BBE72 File Offset: 0x004BA072
		public ResourcePackList(IEnumerable<ResourcePack> resourcePacks)
		{
			this._resourcePacks.AddRange(resourcePacks);
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x004BBE94 File Offset: 0x004BA094
		public JArray ToJson()
		{
			List<ResourcePackList.ResourcePackEntry> list = new List<ResourcePackList.ResourcePackEntry>(this._resourcePacks.Count);
			list.AddRange(this._resourcePacks.Select((ResourcePack pack) => new ResourcePackList.ResourcePackEntry(pack.FileName, pack.IsEnabled, pack.SortingOrder)));
			return JArray.FromObject(list);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x004BBEE8 File Offset: 0x004BA0E8
		public static ResourcePackList FromJson(JArray serializedState, IServiceProvider services, string searchPath)
		{
			if (!Directory.Exists(searchPath))
			{
				return new ResourcePackList();
			}
			List<ResourcePack> list = new List<ResourcePack>();
			ResourcePackList.CreatePacksFromSavedJson(serializedState, services, searchPath, list);
			ResourcePackList.CreatePacksFromZips(services, searchPath, list);
			ResourcePackList.CreatePacksFromDirectories(services, searchPath, list);
			ResourcePackList.CreatePacksFromWorkshopFolders(services, list);
			return new ResourcePackList(list);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x004BBF30 File Offset: 0x004BA130
		public static ResourcePackList Publishable(JArray serializedState, IServiceProvider services, string searchPath)
		{
			if (!Directory.Exists(searchPath))
			{
				return new ResourcePackList();
			}
			List<ResourcePack> list = new List<ResourcePack>();
			ResourcePackList.CreatePacksFromZips(services, searchPath, list);
			ResourcePackList.CreatePacksFromDirectories(services, searchPath, list);
			return new ResourcePackList(list);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x004BBF68 File Offset: 0x004BA168
		private static void CreatePacksFromSavedJson(JArray serializedState, IServiceProvider services, string searchPath, List<ResourcePack> resourcePacks)
		{
			foreach (ResourcePackList.ResourcePackEntry resourcePackEntry in ResourcePackList.CreatePackEntryListFromJson(serializedState))
			{
				if (resourcePackEntry.FileName != null)
				{
					string text = Path.Combine(searchPath, resourcePackEntry.FileName);
					try
					{
						bool flag = File.Exists(text) || Directory.Exists(text);
						ResourcePack.BrandingType brandingType = ResourcePack.BrandingType.None;
						string text2;
						if (!flag && SocialAPI.Workshop != null && SocialAPI.Workshop.TryGetPath(resourcePackEntry.FileName, out text2))
						{
							text = text2;
							flag = true;
							brandingType = SocialAPI.Workshop.Branding.ResourcePackBrand;
						}
						if (flag)
						{
							ResourcePack resourcePack = new ResourcePack(services, text, brandingType)
							{
								IsEnabled = resourcePackEntry.Enabled,
								SortingOrder = resourcePackEntry.SortingOrder
							};
							resourcePacks.Add(resourcePack);
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine("Failed to read resource pack {0}: {1}", text, ex);
					}
				}
			}
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x004BC064 File Offset: 0x004BA264
		private static void CreatePacksFromDirectories(IServiceProvider services, string searchPath, List<ResourcePack> resourcePacks)
		{
			foreach (string text in Directory.GetDirectories(searchPath))
			{
				try
				{
					string folderName = Path.GetFileName(text);
					if (resourcePacks.All((ResourcePack pack) => pack.FileName != folderName))
					{
						resourcePacks.Add(new ResourcePack(services, text, ResourcePack.BrandingType.None));
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Failed to read resource pack {0}: {1}", text, ex);
				}
			}
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x004BC0E4 File Offset: 0x004BA2E4
		private static void CreatePacksFromZips(IServiceProvider services, string searchPath, List<ResourcePack> resourcePacks)
		{
			foreach (string text in Directory.GetFiles(searchPath, "*.zip"))
			{
				try
				{
					string fileName = Path.GetFileName(text);
					if (resourcePacks.All((ResourcePack pack) => pack.FileName != fileName))
					{
						resourcePacks.Add(new ResourcePack(services, text, ResourcePack.BrandingType.None));
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Failed to read resource pack {0}: {1}", text, ex);
				}
			}
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x004BC168 File Offset: 0x004BA368
		private static void CreatePacksFromWorkshopFolders(IServiceProvider services, List<ResourcePack> resourcePacks)
		{
			WorkshopSocialModule workshop = SocialAPI.Workshop;
			if (workshop == null)
			{
				return;
			}
			List<string> listOfSubscribedResourcePackPaths = workshop.GetListOfSubscribedResourcePackPaths();
			ResourcePack.BrandingType resourcePackBrand = workshop.Branding.ResourcePackBrand;
			foreach (string text in listOfSubscribedResourcePackPaths)
			{
				try
				{
					string folderName = Path.GetFileName(text);
					if (resourcePacks.All((ResourcePack pack) => pack.FileName != folderName))
					{
						resourcePacks.Add(new ResourcePack(services, text, resourcePackBrand));
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Failed to read resource pack {0}: {1}", text, ex);
				}
			}
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x004BC228 File Offset: 0x004BA428
		private static IEnumerable<ResourcePackList.ResourcePackEntry> CreatePackEntryListFromJson(JArray serializedState)
		{
			try
			{
				if (serializedState != null && serializedState.Count != 0)
				{
					return serializedState.ToObject<List<ResourcePackList.ResourcePackEntry>>();
				}
			}
			catch (JsonReaderException ex)
			{
				Console.WriteLine("Failed to parse configuration entry for resource pack list. {0}", ex);
			}
			return new List<ResourcePackList.ResourcePackEntry>();
		}

		// Token: 0x04001092 RID: 4242
		private readonly List<ResourcePack> _resourcePacks = new List<ResourcePack>();

		// Token: 0x02000660 RID: 1632
		private struct ResourcePackEntry
		{
			// Token: 0x06003D99 RID: 15769 RVA: 0x00693157 File Offset: 0x00691357
			public ResourcePackEntry(string name, bool enabled, int sortingOrder)
			{
				this.FileName = name;
				this.Enabled = enabled;
				this.SortingOrder = sortingOrder;
			}

			// Token: 0x04006661 RID: 26209
			public string FileName;

			// Token: 0x04006662 RID: 26210
			public bool Enabled;

			// Token: 0x04006663 RID: 26211
			public int SortingOrder;
		}

		// Token: 0x02000661 RID: 1633
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003D9A RID: 15770 RVA: 0x0069316E File Offset: 0x0069136E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003D9B RID: 15771 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003D9C RID: 15772 RVA: 0x0069317A File Offset: 0x0069137A
			internal bool <get_EnabledPacks>b__2_0(ResourcePack pack)
			{
				return pack.IsEnabled;
			}

			// Token: 0x06003D9D RID: 15773 RVA: 0x00693182 File Offset: 0x00691382
			internal int <get_EnabledPacks>b__2_1(ResourcePack pack)
			{
				return pack.SortingOrder;
			}

			// Token: 0x06003D9E RID: 15774 RVA: 0x0069318A File Offset: 0x0069138A
			internal string <get_EnabledPacks>b__2_2(ResourcePack pack)
			{
				return pack.Name;
			}

			// Token: 0x06003D9F RID: 15775 RVA: 0x00693192 File Offset: 0x00691392
			internal ResourcePackVersion <get_EnabledPacks>b__2_3(ResourcePack pack)
			{
				return pack.Version;
			}

			// Token: 0x06003DA0 RID: 15776 RVA: 0x0069319A File Offset: 0x0069139A
			internal string <get_EnabledPacks>b__2_4(ResourcePack pack)
			{
				return pack.FileName;
			}

			// Token: 0x06003DA1 RID: 15777 RVA: 0x006931A2 File Offset: 0x006913A2
			internal bool <get_DisabledPacks>b__4_0(ResourcePack pack)
			{
				return !pack.IsEnabled;
			}

			// Token: 0x06003DA2 RID: 15778 RVA: 0x0069318A File Offset: 0x0069138A
			internal string <get_DisabledPacks>b__4_1(ResourcePack pack)
			{
				return pack.Name;
			}

			// Token: 0x06003DA3 RID: 15779 RVA: 0x00693192 File Offset: 0x00691392
			internal ResourcePackVersion <get_DisabledPacks>b__4_2(ResourcePack pack)
			{
				return pack.Version;
			}

			// Token: 0x06003DA4 RID: 15780 RVA: 0x0069319A File Offset: 0x0069139A
			internal string <get_DisabledPacks>b__4_3(ResourcePack pack)
			{
				return pack.FileName;
			}

			// Token: 0x06003DA5 RID: 15781 RVA: 0x0069318A File Offset: 0x0069138A
			internal string <get_AllPacks>b__6_0(ResourcePack pack)
			{
				return pack.Name;
			}

			// Token: 0x06003DA6 RID: 15782 RVA: 0x00693192 File Offset: 0x00691392
			internal ResourcePackVersion <get_AllPacks>b__6_1(ResourcePack pack)
			{
				return pack.Version;
			}

			// Token: 0x06003DA7 RID: 15783 RVA: 0x0069319A File Offset: 0x0069139A
			internal string <get_AllPacks>b__6_2(ResourcePack pack)
			{
				return pack.FileName;
			}

			// Token: 0x06003DA8 RID: 15784 RVA: 0x006931AD File Offset: 0x006913AD
			internal ResourcePackList.ResourcePackEntry <ToJson>b__9_0(ResourcePack pack)
			{
				return new ResourcePackList.ResourcePackEntry(pack.FileName, pack.IsEnabled, pack.SortingOrder);
			}

			// Token: 0x04006664 RID: 26212
			public static readonly ResourcePackList.<>c <>9 = new ResourcePackList.<>c();

			// Token: 0x04006665 RID: 26213
			public static Func<ResourcePack, bool> <>9__2_0;

			// Token: 0x04006666 RID: 26214
			public static Func<ResourcePack, int> <>9__2_1;

			// Token: 0x04006667 RID: 26215
			public static Func<ResourcePack, string> <>9__2_2;

			// Token: 0x04006668 RID: 26216
			public static Func<ResourcePack, ResourcePackVersion> <>9__2_3;

			// Token: 0x04006669 RID: 26217
			public static Func<ResourcePack, string> <>9__2_4;

			// Token: 0x0400666A RID: 26218
			public static Func<ResourcePack, bool> <>9__4_0;

			// Token: 0x0400666B RID: 26219
			public static Func<ResourcePack, string> <>9__4_1;

			// Token: 0x0400666C RID: 26220
			public static Func<ResourcePack, ResourcePackVersion> <>9__4_2;

			// Token: 0x0400666D RID: 26221
			public static Func<ResourcePack, string> <>9__4_3;

			// Token: 0x0400666E RID: 26222
			public static Func<ResourcePack, string> <>9__6_0;

			// Token: 0x0400666F RID: 26223
			public static Func<ResourcePack, ResourcePackVersion> <>9__6_1;

			// Token: 0x04006670 RID: 26224
			public static Func<ResourcePack, string> <>9__6_2;

			// Token: 0x04006671 RID: 26225
			public static Func<ResourcePack, ResourcePackList.ResourcePackEntry> <>9__9_0;
		}

		// Token: 0x02000662 RID: 1634
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x06003DA9 RID: 15785 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x06003DAA RID: 15786 RVA: 0x006931C6 File Offset: 0x006913C6
			internal bool <CreatePacksFromDirectories>b__0(ResourcePack pack)
			{
				return pack.FileName != this.folderName;
			}

			// Token: 0x04006672 RID: 26226
			public string folderName;
		}

		// Token: 0x02000663 RID: 1635
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x06003DAB RID: 15787 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x06003DAC RID: 15788 RVA: 0x006931D9 File Offset: 0x006913D9
			internal bool <CreatePacksFromZips>b__0(ResourcePack pack)
			{
				return pack.FileName != this.fileName;
			}

			// Token: 0x04006673 RID: 26227
			public string fileName;
		}

		// Token: 0x02000664 RID: 1636
		[CompilerGenerated]
		private sealed class <>c__DisplayClass15_0
		{
			// Token: 0x06003DAD RID: 15789 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass15_0()
			{
			}

			// Token: 0x06003DAE RID: 15790 RVA: 0x006931EC File Offset: 0x006913EC
			internal bool <CreatePacksFromWorkshopFolders>b__0(ResourcePack pack)
			{
				return pack.FileName != this.folderName;
			}

			// Token: 0x04006674 RID: 26228
			public string folderName;
		}
	}
}
