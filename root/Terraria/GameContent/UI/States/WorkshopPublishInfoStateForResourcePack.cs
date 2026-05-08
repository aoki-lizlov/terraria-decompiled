using System;
using System.Collections.Generic;
using Terraria.IO;
using Terraria.Social;
using Terraria.Social.Base;
using Terraria.UI;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003A2 RID: 930
	public class WorkshopPublishInfoStateForResourcePack : AWorkshopPublishInfoState<ResourcePack>
	{
		// Token: 0x06002AB5 RID: 10933 RVA: 0x005870B4 File Offset: 0x005852B4
		public WorkshopPublishInfoStateForResourcePack(UIState stateToGoBackTo, ResourcePack resourcePack)
			: base(stateToGoBackTo, resourcePack)
		{
			this._instructionsTextKey = "Workshop.ResourcePackPublishDescription";
			this._publishedObjectNameDescriptorTexKey = "Workshop.ResourcePackName";
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x005870D4 File Offset: 0x005852D4
		protected override string GetPublishedObjectDisplayName()
		{
			if (this._dataObject == null)
			{
				return "null";
			}
			return this._dataObject.Name;
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x005870F0 File Offset: 0x005852F0
		protected override void GoToPublishConfirmation()
		{
			if (SocialAPI.Workshop != null && this._dataObject != null)
			{
				SocialAPI.Workshop.PublishResourcePack(this._dataObject, base.GetPublishSettings());
			}
			Main.menuMode = 888;
			Main.MenuUI.SetState(this._previousUIState);
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x0058713C File Offset: 0x0058533C
		protected override List<WorkshopTagOption> GetTagsToShow()
		{
			return SocialAPI.Workshop.SupportedTags.ResourcePackTags;
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x0058714D File Offset: 0x0058534D
		protected override bool TryFindingTags(out FoundWorkshopEntryInfo info)
		{
			return SocialAPI.Workshop.TryGetInfoForResourcePack(this._dataObject, out info);
		}
	}
}
