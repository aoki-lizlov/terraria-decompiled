using System;

namespace Terraria.GameContent.UI.ResourceSets
{
	// Token: 0x020003B9 RID: 953
	public class CommonResourceBarMethods
	{
		// Token: 0x06002CE8 RID: 11496 RVA: 0x005A0A34 File Offset: 0x0059EC34
		public static void DrawLifeMouseOver()
		{
			if (!Main.mouseText)
			{
				Player localPlayer = Main.LocalPlayer;
				localPlayer.cursorItemIconEnabled = false;
				string text = localPlayer.statLife + "/" + localPlayer.statLifeMax2;
				Main.instance.MouseTextHackZoom(text, null);
				Main.mouseText = true;
			}
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x005A0A88 File Offset: 0x0059EC88
		public static void DrawManaMouseOver()
		{
			if (!Main.mouseText)
			{
				Player localPlayer = Main.LocalPlayer;
				localPlayer.cursorItemIconEnabled = false;
				string text = localPlayer.statMana + "/" + localPlayer.statManaMax2;
				Main.instance.MouseTextHackZoom(text, null);
				Main.mouseText = true;
			}
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x0000357B File Offset: 0x0000177B
		public CommonResourceBarMethods()
		{
		}
	}
}
