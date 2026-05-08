using System;

namespace Terraria.Social.Base
{
	// Token: 0x02000160 RID: 352
	public abstract class OverlaySocialModule : ISocialModule
	{
		// Token: 0x06001D78 RID: 7544
		public abstract void Initialize();

		// Token: 0x06001D79 RID: 7545
		public abstract void Shutdown();

		// Token: 0x06001D7A RID: 7546
		public abstract bool IsGamepadTextInputActive();

		// Token: 0x06001D7B RID: 7547
		public abstract bool ShowGamepadTextInput(string description, uint maxLength, bool multiLine = false, string existingText = "", bool password = false);

		// Token: 0x06001D7C RID: 7548
		public abstract string GetGamepadText();

		// Token: 0x06001D7D RID: 7549 RVA: 0x0000357B File Offset: 0x0000177B
		protected OverlaySocialModule()
		{
		}
	}
}
