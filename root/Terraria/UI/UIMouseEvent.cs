using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x020000FF RID: 255
	public class UIMouseEvent : UIEvent
	{
		// Token: 0x06001A27 RID: 6695 RVA: 0x004F4F72 File Offset: 0x004F3172
		public UIMouseEvent(UIElement target, Vector2 mousePosition)
			: base(target)
		{
			this.MousePosition = mousePosition;
		}

		// Token: 0x0400139B RID: 5019
		public readonly Vector2 MousePosition;
	}
}
