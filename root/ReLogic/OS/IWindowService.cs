using System;
using Microsoft.Xna.Framework;

namespace ReLogic.OS
{
	// Token: 0x02000061 RID: 97
	public interface IWindowService
	{
		// Token: 0x06000214 RID: 532
		void SetUnicodeTitle(GameWindow window, string title);

		// Token: 0x06000215 RID: 533
		void StartFlashingIcon(GameWindow window);

		// Token: 0x06000216 RID: 534
		void StopFlashingIcon(GameWindow window);

		// Token: 0x06000217 RID: 535
		float GetScaling();

		// Token: 0x06000218 RID: 536
		void SetQuickEditEnabled(bool enabled);

		// Token: 0x06000219 RID: 537
		void Activate(GameWindow window);

		// Token: 0x0600021A RID: 538
		bool IsSizeable(GameWindow window);

		// Token: 0x0600021B RID: 539
		void SetPosition(GameWindow window, int x, int y);

		// Token: 0x0600021C RID: 540
		Rectangle GetBounds(GameWindow window);
	}
}
