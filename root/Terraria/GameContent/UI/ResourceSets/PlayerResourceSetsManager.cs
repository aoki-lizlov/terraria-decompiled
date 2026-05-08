using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ReLogic.Content;
using Terraria.IO;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003BD RID: 957
	public class PlayerResourceSetsManager
	{
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06002D10 RID: 11536 RVA: 0x005A2137 File Offset: 0x005A0337
		// (set) Token: 0x06002D11 RID: 11537 RVA: 0x005A213F File Offset: 0x005A033F
		public string ActiveSetKeyName
		{
			[CompilerGenerated]
			get
			{
				return this.<ActiveSetKeyName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ActiveSetKeyName>k__BackingField = value;
			}
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x005A2148 File Offset: 0x005A0348
		public void BindTo(Preferences preferences)
		{
			preferences.OnLoad += this.Configuration_OnLoad;
			preferences.OnSave += this.Configuration_OnSave;
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x005A216E File Offset: 0x005A036E
		private void Configuration_OnLoad(Preferences obj)
		{
			this._activeSetConfigKey = obj.Get<string>("PlayerResourcesSet", "New");
			if (this._loadedContent)
			{
				this.SetActiveFromLoadedConfigKey();
			}
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x005A2194 File Offset: 0x005A0394
		private void Configuration_OnSave(Preferences obj)
		{
			obj.Put("PlayerResourcesSet", this._activeSetConfigKey);
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x005A21A8 File Offset: 0x005A03A8
		public void LoadContent(AssetRequestMode mode)
		{
			this._sets["New"] = new FancyClassicPlayerResourcesDisplaySet("New", "New", "FancyClassic", mode);
			this._sets["Default"] = new ClassicPlayerResourcesDisplaySet("Default", "Default");
			this._sets["HorizontalBarsWithFullText"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBarsWithFullText", "HorizontalBarsWithFullText", "HorizontalBars", mode);
			this._sets["HorizontalBarsWithText"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBarsWithText", "HorizontalBarsWithText", "HorizontalBars", mode);
			this._sets["HorizontalBars"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBars", "HorizontalBars", "HorizontalBars", mode);
			this._sets["NewWithText"] = new FancyClassicPlayerResourcesDisplaySet("NewWithText", "NewWithText", "FancyClassic", mode);
			this._loadedContent = true;
			this.SetActiveFromLoadedConfigKey();
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x005A229A File Offset: 0x005A049A
		public void SetActiveFromLoadedConfigKey()
		{
			this.SetActive(this._activeSetConfigKey);
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x005A22A8 File Offset: 0x005A04A8
		private void SetActive(string name)
		{
			IPlayerResourcesDisplaySet playerResourcesDisplaySet = this._sets.FirstOrDefault((KeyValuePair<string, IPlayerResourcesDisplaySet> pair) => pair.Key == name).Value;
			if (playerResourcesDisplaySet == null)
			{
				playerResourcesDisplaySet = this._sets.Values.First<IPlayerResourcesDisplaySet>();
			}
			this.SetActiveFrame(playerResourcesDisplaySet);
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x005A22FD File Offset: 0x005A04FD
		private void SetActiveFrame(IPlayerResourcesDisplaySet set)
		{
			this._activeSet = set;
			this._activeSetConfigKey = set.ConfigKey;
			this.ActiveSetKeyName = set.NameKey;
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x005A231E File Offset: 0x005A051E
		public void TryToHoverOverResources()
		{
			this._activeSet.TryToHover();
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x005A232B File Offset: 0x005A052B
		public void Draw()
		{
			this._activeSet.Draw();
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x005A2338 File Offset: 0x005A0538
		public void CycleResourceSet()
		{
			IPlayerResourcesDisplaySet lastFrame = null;
			this._sets.Values.FirstOrDefault(delegate(IPlayerResourcesDisplaySet frame)
			{
				if (frame == this._activeSet)
				{
					return true;
				}
				lastFrame = frame;
				return false;
			});
			if (lastFrame == null)
			{
				lastFrame = this._sets.Values.Last<IPlayerResourcesDisplaySet>();
			}
			this.SetActiveFrame(lastFrame);
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x005A23A0 File Offset: 0x005A05A0
		public PlayerResourceSetsManager()
		{
		}

		// Token: 0x04005486 RID: 21638
		private Dictionary<string, IPlayerResourcesDisplaySet> _sets = new Dictionary<string, IPlayerResourcesDisplaySet>();

		// Token: 0x04005487 RID: 21639
		private IPlayerResourcesDisplaySet _activeSet;

		// Token: 0x04005488 RID: 21640
		private string _activeSetConfigKey;

		// Token: 0x04005489 RID: 21641
		private bool _loadedContent;

		// Token: 0x0400548A RID: 21642
		[CompilerGenerated]
		private string <ActiveSetKeyName>k__BackingField;

		// Token: 0x02000920 RID: 2336
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x060047E4 RID: 18404 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x060047E5 RID: 18405 RVA: 0x006CC679 File Offset: 0x006CA879
			internal bool <SetActive>b__0(KeyValuePair<string, IPlayerResourcesDisplaySet> pair)
			{
				return pair.Key == this.name;
			}

			// Token: 0x040074E8 RID: 29928
			public string name;
		}

		// Token: 0x02000921 RID: 2337
		[CompilerGenerated]
		private sealed class <>c__DisplayClass17_0
		{
			// Token: 0x060047E6 RID: 18406 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass17_0()
			{
			}

			// Token: 0x060047E7 RID: 18407 RVA: 0x006CC68D File Offset: 0x006CA88D
			internal bool <CycleResourceSet>b__0(IPlayerResourcesDisplaySet frame)
			{
				if (frame == this.<>4__this._activeSet)
				{
					return true;
				}
				this.lastFrame = frame;
				return false;
			}

			// Token: 0x040074E9 RID: 29929
			public PlayerResourceSetsManager <>4__this;

			// Token: 0x040074EA RID: 29930
			public IPlayerResourcesDisplaySet lastFrame;
		}
	}
}
