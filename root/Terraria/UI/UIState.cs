using System;

namespace Terraria.UI
{
	// Token: 0x02000101 RID: 257
	public class UIState : UIElement
	{
		// Token: 0x06001A29 RID: 6697 RVA: 0x004F4F93 File Offset: 0x004F3193
		public UIState()
		{
			this.Width.Precent = 1f;
			this.Height.Precent = 1f;
			this.Recalculate();
		}

		// Token: 0x0400139D RID: 5021
		public bool NoGamepadSupport;
	}
}
