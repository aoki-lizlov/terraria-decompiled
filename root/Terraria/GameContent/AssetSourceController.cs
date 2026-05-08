using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using ReLogic.Content;
using ReLogic.Content.Sources;
using Terraria.Audio;
using Terraria.IO;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x02000276 RID: 630
	public class AssetSourceController
	{
		// Token: 0x14000043 RID: 67
		// (add) Token: 0x0600243B RID: 9275 RVA: 0x0054BE54 File Offset: 0x0054A054
		// (remove) Token: 0x0600243C RID: 9276 RVA: 0x0054BE8C File Offset: 0x0054A08C
		public event Action<ResourcePackList> OnResourcePackChange
		{
			[CompilerGenerated]
			add
			{
				Action<ResourcePackList> action = this.OnResourcePackChange;
				Action<ResourcePackList> action2;
				do
				{
					action2 = action;
					Action<ResourcePackList> action3 = (Action<ResourcePackList>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ResourcePackList>>(ref this.OnResourcePackChange, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ResourcePackList> action = this.OnResourcePackChange;
				Action<ResourcePackList> action2;
				do
				{
					action2 = action;
					Action<ResourcePackList> action3 = (Action<ResourcePackList>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ResourcePackList>>(ref this.OnResourcePackChange, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600243D RID: 9277 RVA: 0x0054BEC1 File Offset: 0x0054A0C1
		// (set) Token: 0x0600243E RID: 9278 RVA: 0x0054BEC9 File Offset: 0x0054A0C9
		public ResourcePackList ActiveResourcePackList
		{
			[CompilerGenerated]
			get
			{
				return this.<ActiveResourcePackList>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ActiveResourcePackList>k__BackingField = value;
			}
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x0054BED2 File Offset: 0x0054A0D2
		public AssetSourceController(IAssetRepository assetRepository, IEnumerable<IContentSource> staticSources)
		{
			this._assetRepository = assetRepository;
			this._staticSources = staticSources.ToList<IContentSource>();
			this.ActiveResourcePackList = new ResourcePackList();
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x0054BEF8 File Offset: 0x0054A0F8
		public void Refresh()
		{
			foreach (ResourcePack resourcePack in this.ActiveResourcePackList.AllPacks)
			{
				resourcePack.GetContentSource().Refresh();
			}
			this.UseResourcePacks(this.ActiveResourcePackList);
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x0054BF58 File Offset: 0x0054A158
		public void UseResourcePacks(ResourcePackList resourcePacks)
		{
			if (this.OnResourcePackChange != null)
			{
				this.OnResourcePackChange(resourcePacks);
			}
			this.ActiveResourcePackList = resourcePacks;
			List<IContentSource> list = new List<IContentSource>(from pack in resourcePacks.EnabledPacks
				orderby pack.SortingOrder
				select pack.GetContentSource());
			list.AddRange(this._staticSources);
			foreach (IContentSource contentSource in list)
			{
				contentSource.ClearRejections();
			}
			List<IContentSource> list2 = new List<IContentSource>();
			for (int i = list.Count - 1; i >= 0; i--)
			{
				list2.Add(list[i]);
			}
			this._assetRepository.SetSources(list, 1);
			LanguageManager.Instance.UseSources(list2);
			Main.audioSystem.UseSources(list2);
			SoundEngine.Reload();
			Main.changeTheTitle = true;
		}

		// Token: 0x04004DE9 RID: 19945
		[CompilerGenerated]
		private Action<ResourcePackList> OnResourcePackChange;

		// Token: 0x04004DEA RID: 19946
		[CompilerGenerated]
		private ResourcePackList <ActiveResourcePackList>k__BackingField;

		// Token: 0x04004DEB RID: 19947
		private readonly List<IContentSource> _staticSources;

		// Token: 0x04004DEC RID: 19948
		private readonly IAssetRepository _assetRepository;

		// Token: 0x020007F5 RID: 2037
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004296 RID: 17046 RVA: 0x006BF1EF File Offset: 0x006BD3EF
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004297 RID: 17047 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004298 RID: 17048 RVA: 0x00693182 File Offset: 0x00691382
			internal int <UseResourcePacks>b__11_0(ResourcePack pack)
			{
				return pack.SortingOrder;
			}

			// Token: 0x06004299 RID: 17049 RVA: 0x006BF1FB File Offset: 0x006BD3FB
			internal IContentSource <UseResourcePacks>b__11_1(ResourcePack pack)
			{
				return pack.GetContentSource();
			}

			// Token: 0x0400717C RID: 29052
			public static readonly AssetSourceController.<>c <>9 = new AssetSourceController.<>c();

			// Token: 0x0400717D RID: 29053
			public static Func<ResourcePack, int> <>9__11_0;

			// Token: 0x0400717E RID: 29054
			public static Func<ResourcePack, IContentSource> <>9__11_1;
		}
	}
}
