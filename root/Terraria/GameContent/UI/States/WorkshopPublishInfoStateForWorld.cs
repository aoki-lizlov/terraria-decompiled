using System;
using System.Collections.Generic;
using Terraria.IO;
using Terraria.Social;
using Terraria.Social.Base;
using Terraria.UI;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003A1 RID: 929
	public class WorkshopPublishInfoStateForWorld : AWorkshopPublishInfoState<WorldFileData>
	{
		// Token: 0x06002AB0 RID: 10928 RVA: 0x00587008 File Offset: 0x00585208
		public WorkshopPublishInfoStateForWorld(UIState stateToGoBackTo, WorldFileData world)
			: base(stateToGoBackTo, world)
		{
			this._instructionsTextKey = "Workshop.WorldPublishDescription";
			this._publishedObjectNameDescriptorTexKey = "Workshop.WorldName";
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x00587028 File Offset: 0x00585228
		protected override string GetPublishedObjectDisplayName()
		{
			if (this._dataObject == null)
			{
				return "null";
			}
			return this._dataObject.Name;
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x00587044 File Offset: 0x00585244
		protected override void GoToPublishConfirmation()
		{
			if (SocialAPI.Workshop != null && this._dataObject != null)
			{
				SocialAPI.Workshop.PublishWorld(this._dataObject, base.GetPublishSettings());
			}
			Main.menuMode = 888;
			Main.MenuUI.SetState(this._previousUIState);
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x00587090 File Offset: 0x00585290
		protected override List<WorkshopTagOption> GetTagsToShow()
		{
			return SocialAPI.Workshop.SupportedTags.WorldTags;
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x005870A1 File Offset: 0x005852A1
		protected override bool TryFindingTags(out FoundWorkshopEntryInfo info)
		{
			return SocialAPI.Workshop.TryGetInfoForWorld(this._dataObject, out info);
		}
	}
}
