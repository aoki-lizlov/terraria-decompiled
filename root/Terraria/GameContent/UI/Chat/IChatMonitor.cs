using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.UI.Chat
{
	// Token: 0x0200037F RID: 895
	public interface IChatMonitor
	{
		// Token: 0x0600299B RID: 10651
		void NewText(string newText, byte R = 255, byte G = 255, byte B = 255);

		// Token: 0x0600299C RID: 10652
		void NewTextMultiline(string text, bool force = false, Color c = default(Color), int WidthLimit = -1);

		// Token: 0x0600299D RID: 10653
		void DrawChat(bool drawingPlayerChat);

		// Token: 0x0600299E RID: 10654
		void Clear();

		// Token: 0x0600299F RID: 10655
		void Update();

		// Token: 0x060029A0 RID: 10656
		void Offset(int linesOffset);

		// Token: 0x060029A1 RID: 10657
		void ResetOffset();
	}
}
