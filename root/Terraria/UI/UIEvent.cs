using System;

namespace Terraria.UI
{
	// Token: 0x020000FE RID: 254
	public class UIEvent
	{
		// Token: 0x06001A26 RID: 6694 RVA: 0x004F4F63 File Offset: 0x004F3163
		public UIEvent(UIElement target)
		{
			this.Target = target;
		}

		// Token: 0x0400139A RID: 5018
		public readonly UIElement Target;
	}
}
