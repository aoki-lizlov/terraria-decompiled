using System;
using System.Collections.Generic;
using Terraria.GameInput;
using Terraria.Localization;

namespace Terraria.GameContent.UI
{
	// Token: 0x02000377 RID: 887
	public class GameTipsProvider : ITipProvider
	{
		// Token: 0x06002967 RID: 10599 RVA: 0x0057B8C8 File Offset: 0x00579AC8
		public GameTipsProvider()
		{
			this._tipsDefault = Language.FindAll(Lang.CreateDialogFilter("LoadingTips_Default.", false));
			this._tipsGamepad = Language.FindAll(Lang.CreateDialogFilter("LoadingTips_GamePad.", false));
			this._tipsKeyboard = Language.FindAll(Lang.CreateDialogFilter("LoadingTips_Keyboard.", false));
			this._lastTip = null;
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x0057B924 File Offset: 0x00579B24
		public LocalizedText RollAvailableTip()
		{
			List<LocalizedText> list = new List<LocalizedText>();
			list.AddRange(this._tipsDefault);
			if (PlayerInput.UsingGamepad)
			{
				list.AddRange(this._tipsGamepad);
			}
			else
			{
				list.AddRange(this._tipsKeyboard);
			}
			do
			{
				list.Remove(this._lastTip);
				if (list.Count == 0)
				{
					this._lastTip = LocalizedText.Empty;
				}
				else
				{
					this._lastTip = list[Main.rand.Next(list.Count)];
				}
			}
			while (!this._lastTip.ConditionsMet);
			return this._lastTip;
		}

		// Token: 0x040051ED RID: 20973
		private LocalizedText[] _tipsDefault;

		// Token: 0x040051EE RID: 20974
		private LocalizedText[] _tipsGamepad;

		// Token: 0x040051EF RID: 20975
		private LocalizedText[] _tipsKeyboard;

		// Token: 0x040051F0 RID: 20976
		private LocalizedText _lastTip;
	}
}
