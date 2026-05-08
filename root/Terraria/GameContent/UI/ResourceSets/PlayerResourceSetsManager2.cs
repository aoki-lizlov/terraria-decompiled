using System;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.IO;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003BC RID: 956
	public class PlayerResourceSetsManager2 : SelectionHolder<IPlayerResourcesDisplaySet>
	{
		// Token: 0x06002D0A RID: 11530 RVA: 0x005A1FFF File Offset: 0x005A01FF
		protected override void Configuration_Save(Preferences obj)
		{
			obj.Put("PlayerResourcesSet", this.ActiveSelectionConfigKey);
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x005A2012 File Offset: 0x005A0212
		protected override void Configuration_OnLoad(Preferences obj)
		{
			this.ActiveSelectionConfigKey = Main.Configuration.Get<string>("PlayerResourcesSet", "New");
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x005A2030 File Offset: 0x005A0230
		protected override void PopulateOptionsAndLoadContent(AssetRequestMode mode)
		{
			this.Options["New"] = new FancyClassicPlayerResourcesDisplaySet("New", "New", "FancyClassic", mode);
			this.Options["Default"] = new ClassicPlayerResourcesDisplaySet("Default", "Default");
			this.Options["HorizontalBarsWithFullText"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBarsWithFullText", "HorizontalBarsWithFullText", "HorizontalBars", mode);
			this.Options["HorizontalBarsWithText"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBarsWithText", "HorizontalBarsWithText", "HorizontalBars", mode);
			this.Options["HorizontalBars"] = new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBars", "HorizontalBars", "HorizontalBars", mode);
			this.Options["NewWithText"] = new FancyClassicPlayerResourcesDisplaySet("NewWithText", "NewWithText", "FancyClassic", mode);
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x005A2115 File Offset: 0x005A0315
		public void TryToHoverOverResources()
		{
			this.ActiveSelection.TryToHover();
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x005A2122 File Offset: 0x005A0322
		public void Draw()
		{
			this.ActiveSelection.Draw();
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x005A212F File Offset: 0x005A032F
		public PlayerResourceSetsManager2()
		{
		}
	}
}
