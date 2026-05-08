using System;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.Bestiary
{
	// Token: 0x0200035B RID: 859
	public class NamePlateInfoElement : IBestiaryInfoElement, IProvideSearchFilterString
	{
		// Token: 0x060028B7 RID: 10423 RVA: 0x00573F6F File Offset: 0x0057216F
		public NamePlateInfoElement(string languageKey, int npcNetId)
		{
			this._key = languageKey;
			this._npcNetId = npcNetId;
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x00573F88 File Offset: 0x00572188
		public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
		{
			UIElement uielement;
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				uielement = new UIText("???", 1f, false);
			}
			else
			{
				uielement = new UIText(Language.GetText(this._key), 1f, false);
			}
			uielement.HAlign = 0.5f;
			uielement.VAlign = 0.5f;
			uielement.Top = new StyleDimension(2f, 0f);
			uielement.IgnoresMouseInteraction = true;
			UIElement uielement2 = new UIElement();
			uielement2.Width = new StyleDimension(0f, 1f);
			uielement2.Height = new StyleDimension(24f, 0f);
			uielement2.Append(uielement);
			return uielement2;
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x0057402F File Offset: 0x0057222F
		public string GetSearchString(ref BestiaryUICollectionInfo info)
		{
			if (info.UnlockState == BestiaryEntryUnlockState.NotKnownAtAll_0)
			{
				return null;
			}
			return Language.GetText(this._key).Value;
		}

		// Token: 0x04005164 RID: 20836
		private string _key;

		// Token: 0x04005165 RID: 20837
		private int _npcNetId;
	}
}
