using System;
using Terraria.Social.Base;

namespace Terraria.Social.WeGame
{
	// Token: 0x0200012D RID: 301
	public class OverlaySocialModule : OverlaySocialModule
	{
		// Token: 0x06001C1A RID: 7194 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Initialize()
		{
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Shutdown()
		{
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x004FD752 File Offset: 0x004FB952
		public override bool IsGamepadTextInputActive()
		{
			return this._gamepadTextInputActive;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x001DAC3B File Offset: 0x001D8E3B
		public override bool ShowGamepadTextInput(string description, uint maxLength, bool multiLine = false, string existingText = "", bool password = false)
		{
			return false;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x004FD75A File Offset: 0x004FB95A
		public override string GetGamepadText()
		{
			return "";
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x004FD761 File Offset: 0x004FB961
		public OverlaySocialModule()
		{
		}

		// Token: 0x040015B2 RID: 5554
		private bool _gamepadTextInputActive;
	}
}
