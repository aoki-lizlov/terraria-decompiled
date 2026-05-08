using System;
using Terraria.Localization;

namespace Terraria.GameContent.UI
{
	// Token: 0x02000378 RID: 888
	public class CharacterCreationTipsProvider : ITipProvider
	{
		// Token: 0x06002969 RID: 10601 RVA: 0x0057B9B5 File Offset: 0x00579BB5
		public LocalizedText RollAvailableTip()
		{
			return Language.SelectRandom(Lang.CreateDialogFilter("LoadingTips_CharacterCreation.", true), null);
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x0000357B File Offset: 0x0000177B
		public CharacterCreationTipsProvider()
		{
		}
	}
}
