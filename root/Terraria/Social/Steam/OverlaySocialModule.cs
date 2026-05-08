using System;
using Steamworks;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
	// Token: 0x02000143 RID: 323
	public class OverlaySocialModule : OverlaySocialModule
	{
		// Token: 0x06001CA7 RID: 7335 RVA: 0x004FF598 File Offset: 0x004FD798
		public override void Initialize()
		{
			this._gamepadTextInputDismissed = Callback<GamepadTextInputDismissed_t>.Create(new Callback<GamepadTextInputDismissed_t>.DispatchDelegate(this.OnGamepadTextInputDismissed));
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00009E46 File Offset: 0x00008046
		public override void Shutdown()
		{
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x004FF5B1 File Offset: 0x004FD7B1
		public override bool IsGamepadTextInputActive()
		{
			return this._gamepadTextInputActive;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x004FF5B9 File Offset: 0x004FD7B9
		public override bool ShowGamepadTextInput(string description, uint maxLength, bool multiLine = false, string existingText = "", bool password = false)
		{
			if (this._gamepadTextInputActive)
			{
				return false;
			}
			bool flag = SteamUtils.ShowGamepadTextInput(password ? 1 : 0, multiLine ? 1 : 0, description, maxLength, existingText);
			if (flag)
			{
				this._gamepadTextInputActive = true;
			}
			return flag;
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x004FF5E8 File Offset: 0x004FD7E8
		public override string GetGamepadText()
		{
			uint enteredGamepadTextLength = SteamUtils.GetEnteredGamepadTextLength();
			string text;
			SteamUtils.GetEnteredGamepadTextInput(ref text, enteredGamepadTextLength);
			return text;
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x004FF605 File Offset: 0x004FD805
		private void OnGamepadTextInputDismissed(GamepadTextInputDismissed_t result)
		{
			this._gamepadTextInputActive = false;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x004FD761 File Offset: 0x004FB961
		public OverlaySocialModule()
		{
		}

		// Token: 0x040015E7 RID: 5607
		private Callback<GamepadTextInputDismissed_t> _gamepadTextInputDismissed;

		// Token: 0x040015E8 RID: 5608
		private bool _gamepadTextInputActive;
	}
}
